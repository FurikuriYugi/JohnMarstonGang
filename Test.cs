using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RDR2;

public class Test : Script
{
    private const int ReportIntervalMilliseconds = 500;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private Entity _candidate;
    private int _lastCandidateHandle = -1;
    private bool _iloCallReturned;
    private bool _lastIloCallReturned;
    private string _registrationStatus = "waiting for an NPC target";
    private DateTime _nextReportUtc = DateTime.MinValue;
    private bool _errorReported;

    private bool _hasLoggedFreeFocusing;
    private bool _lastFreeFocusing;
    private bool _hasLoggedInteractionHandle;
    private int _lastInteractionHandle = -1;
    private bool _hasLoggedPlayerTargetState;
    private bool _lastPlayerTargetFound;
    private int _lastPlayerTargetHandle = -1;

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private string _playerTargetWrapperStatus =
        "PLAYER.GET_PLAYER_TARGET_ENTITY wrapper not resolved";

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        ClearLog();

        Log("=== Test.cs Initialized ===");
        Log("Log file: " + _logPath);
        Log(
            "Test: aim at an NPC once to select it, right-click focus, " +
            "then press G.");
        Log("G is diagnostic only; this test does not recruit.");

        ResolvePlayerTargetEntityWrapper();

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            RefreshCandidate();
            ReportStatusWhenDue();
        }
        catch (Exception exception)
        {
            if (!_errorReported)
            {
                _errorReported = true;

                string errorMessage = string.Format(
                    "Lock-on test error: {0}",
                    exception.Message);

                RDR2.UI.Screen.PrintSubtitle(errorMessage);
                Log("ERROR: " + errorMessage);
                Log("StackTrace: " + exception.StackTrace);
            }
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.G)
        {
            return;
        }

        e.Handled = true;
        Log("G key pressed");
        ProcessTestKey();
    }

    private void RefreshCandidate()
    {
        if (_candidate != null && !_candidate.Exists())
        {
            int lostHandle = _candidate.Handle;

            Log(string.Format(
                "CANDIDATE LOST: handle was {0}",
                lostHandle));

            _candidate = null;
            _iloCallReturned = false;
            _registrationStatus = "candidate lost";
        }

        // A firearm free-aim target selects or changes the test candidate.
        Entity aimedEntity = Game.Player.GetFreeAimEntity();

        if (aimedEntity != null &&
            aimedEntity.Exists() &&
            aimedEntity.IsPed)
        {
            SetCandidate(aimedEntity, "FREE AIM");
            return;
        }

        // Use a reported interaction-lock-on target only if it differs from
        // the current candidate. Do not clear the candidate just because
        // the player releases aim.
        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();

        if (interactionTarget != null &&
            interactionTarget.Exists() &&
            interactionTarget.IsPed)
        {
            SetCandidate(interactionTarget, "INTERACTION LOCK-ON");
        }
    }

    private void SetCandidate(Entity entity, string captureSource)
    {
        if (_candidate != null &&
            _candidate.Exists() &&
            _candidate.Handle == entity.Handle)
        {
            return;
        }

        _candidate = entity;
        _iloCallReturned = false;
        _registrationStatus = "capturing...";

        Log(string.Format(
            "CAPTURED FROM {0}: new handle {1}",
            captureSource,
            entity.Handle));

        RegisterInteractionLockonPrompt(_candidate);
    }

    private void RegisterInteractionLockonPrompt(Entity entity)
    {
        if (entity == null || !entity.Exists())
        {
            _registrationStatus = "candidate invalid before registration";
            Log("ERROR: ILO registration skipped; candidate is invalid");
            return;
        }

        Log(string.Format(
            "REGISTERING ILO: handle {0}",
            entity.Handle));

        MethodInfo method =
            FindNativeMethod("REGISTER_INTERACTION_LOCKON_PROMPT");

        if (method == null)
        {
            _registrationStatus =
                "REGISTER_INTERACTION_LOCKON_PROMPT wrapper not found";

            Log("ERROR: ILO wrapper method not found");
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();

        if (parameters.Length != 10)
        {
            _registrationStatus =
                "ILO wrapper found with unexpected parameter count: " +
                parameters.Length.ToString();

            Log(string.Format(
                "ERROR: ILO wrapper has {0} parameters, expected 10",
                parameters.Length));

            return;
        }

        try
        {
            object[] arguments =
                BuildLockonArguments(entity, parameters);

            method.Invoke(null, arguments);

            // A returned call does not confirm visible in-game behavior.
            _iloCallReturned = true;
            _registrationStatus = "ILO wrapper call returned";
            Log("ILO CALL RETURNED WITHOUT EXCEPTION");
        }
        catch (Exception exception)
        {
            _iloCallReturned = false;
            _registrationStatus =
                "ILO call failed: " +
                GetExceptionMessage(exception);

            Log(
                "ERROR: ILO call failed: " +
                GetExceptionMessage(exception));
        }
    }

    private void ResolvePlayerTargetEntityWrapper()
    {
        MethodInfo method =
            FindNativeMethod("GET_PLAYER_TARGET_ENTITY");

        if (method == null)
        {
            _playerTargetWrapperStatus =
                "PLAYER.GET_PLAYER_TARGET_ENTITY wrapper not found";

            Log("ERROR: " + _playerTargetWrapperStatus);
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();

        bool signatureMatches =
            method.ReturnType == typeof(bool) &&
            parameters.Length == 2 &&
            parameters[0].ParameterType == typeof(int) &&
            parameters[1].ParameterType.IsPointer &&
            parameters[1].ParameterType.GetElementType() == typeof(int);

        Log("PLAYER TARGET WRAPPER FOUND: " + DescribeMethod(method));

        if (!signatureMatches)
        {
            _playerTargetWrapperStatus =
                "wrapper signature mismatch; expected bool(int, int*)";

            Log("ERROR: " + _playerTargetWrapperStatus);
            return;
        }

        try
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)Delegate.CreateDelegate(
                    typeof(PlayerTargetEntityNative),
                    method);

            _playerTargetWrapperStatus =
                "wrapper resolved: bool(int, int*)";

            Log("PLAYER TARGET WRAPPER RESOLVED");
        }
        catch (Exception exception)
        {
            _getPlayerTargetEntity = null;
            _playerTargetWrapperStatus =
                "delegate creation failed: " +
                exception.Message;

            Log(
                "ERROR: player target wrapper delegate creation failed: " +
                exception);
        }
    }

    private static MethodInfo FindNativeMethod(string methodName)
    {
        Assembly assembly = typeof(Game).Assembly;
        Type[] types;

        try
        {
            types = assembly.GetExportedTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            types = exception.Types;
        }

        if (types == null)
        {
            return null;
        }

        foreach (Type type in types)
        {
            if (type == null ||
                type.Namespace == null ||
                !type.Namespace.StartsWith(
                    "RDR2.Native",
                    StringComparison.Ordinal))
            {
                continue;
            }

            MethodInfo[] methods;

            try
            {
                methods = type.GetMethods(
                    BindingFlags.Public |
                    BindingFlags.Static);
            }
            catch
            {
                continue;
            }

            foreach (MethodInfo method in methods)
            {
                if (string.Equals(
                    method.Name,
                    methodName,
                    StringComparison.Ordinal))
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static string DescribeMethod(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        string[] parameterDescriptions =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterDescriptions[i] =
                parameters[i].ParameterType.ToString();
        }

        return string.Format(
            "{0}.{1}({2}) -> {3}",
            method.DeclaringType == null
                ? "unknown type"
                : method.DeclaringType.FullName,
            method.Name,
            string.Join(", ", parameterDescriptions),
            method.ReturnType);
    }

    private static object[] BuildLockonArguments(
        Entity entity,
        ParameterInfo[] parameters)
    {
        object[] values =
        {
            entity.Handle, // entity
            "Recruit test", // text
            3.0f, // radius
            5.0f, // p3
            0, // flag
            0.0f, // p5
            0.0f, // p6
            0, // prompt
            true, // p8
            -1 // p9
        };

        object[] arguments = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            Type parameterType = parameters[i].ParameterType;

            if (i == 0 && parameterType.IsInstanceOfType(entity))
            {
                arguments[i] = entity;
            }
            else
            {
                arguments[i] =
                    ConvertArgument(values[i], parameterType);
            }
        }

        return arguments;
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(
                targetType,
                Convert.ToInt32(value));
        }

        return Convert.ChangeType(value, targetType);
    }

    private unsafe bool TryGetPlayerTargetEntity(
        out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        return _getPlayerTargetEntity(
            Game.Player.Handle,
            &targetHandle);
    }

    private void ProcessTestKey()
    {
        bool isFreeFocusing = Game.Player.IsFreeFocusing;

        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();

        int interactionHandle =
            GetExistingHandle(interactionTarget);

        int candidateHandle =
            GetExistingHandle(_candidate);

        int playerTargetHandle;
        bool playerTargetFound =
            TryGetPlayerTargetEntity(out playerTargetHandle);

        if (!playerTargetFound)
        {
            playerTargetHandle = 0;
        }

        Log(string.Format(
            "G SNAPSHOT: candidate={0}; ILO-call-returned={1}; " +
            "free-focusing={2}; interaction-target={3}; " +
            "player-target-found={4}; player-target={5}; " +
            "player-target-wrapper={6}",
            FormatHandle(candidateHandle),
            _iloCallReturned,
            isFreeFocusing,
            FormatHandle(interactionHandle),
            playerTargetFound,
            FormatHandle(playerTargetHandle),
            _playerTargetWrapperStatus));

        if (candidateHandle == 0)
        {
            const string message =
                "G blocked: no valid NPC candidate. Aim at an NPC first.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (!_iloCallReturned)
        {
            const string message =
                "G blocked: ILO wrapper call did not return successfully.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (_getPlayerTargetEntity == null)
        {
            string message =
                "G blocked: player-target wrapper unavailable. Check log.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (!isFreeFocusing)
        {
            const string message =
                "G blocked: right-mouse focus is not active.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (!playerTargetFound ||
            playerTargetHandle != candidateHandle)
        {
            string message = string.Format(
                "G blocked: candidate={0}; focus={1}; " +
                "player-target-found={2}; player-target={3}; " +
                "interaction-target={4}.",
                candidateHandle,
                isFreeFocusing,
                playerTargetFound,
                FormatHandle(playerTargetHandle),
                FormatHandle(interactionHandle));

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        string successMessage = string.Format(
            "G TEST ACCEPTED: right-mouse target matches NPC {0}.",
            candidateHandle);

        RDR2.UI.Screen.PrintSubtitle(successMessage);
        Log("SUCCESS: " + successMessage);
    }

    private void ReportStatusWhenDue()
    {
        if (DateTime.UtcNow < _nextReportUtc)
        {
            return;
        }

        _nextReportUtc =
            DateTime.UtcNow.AddMilliseconds(
                ReportIntervalMilliseconds);

        int candidateHandle = GetExistingHandle(_candidate);

        if (candidateHandle != _lastCandidateHandle)
        {
            Log(string.Format(
                "STATE: candidate handle changed {0} -> {1}",
                FormatHandle(_lastCandidateHandle),
                FormatHandle(candidateHandle)));

            _lastCandidateHandle = candidateHandle;
        }

        if (_iloCallReturned != _lastIloCallReturned)
        {
            Log(string.Format(
                "STATE: ILO-call-returned changed {0} -> {1}",
                _lastIloCallReturned,
                _iloCallReturned));

            _lastIloCallReturned = _iloCallReturned;
        }

        bool isFreeFocusing = Game.Player.IsFreeFocusing;

        if (!_hasLoggedFreeFocusing ||
            isFreeFocusing != _lastFreeFocusing)
        {
            Log(string.Format(
                "STATE: IsFreeFocusing={0}",
                isFreeFocusing));

            _lastFreeFocusing = isFreeFocusing;
            _hasLoggedFreeFocusing = true;
        }

        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();

        int interactionHandle =
            GetExistingHandle(interactionTarget);

        if (!_hasLoggedInteractionHandle ||
            interactionHandle != _lastInteractionHandle)
        {
            Log(string.Format(
                "STATE: interaction target={0}",
                FormatHandle(interactionHandle)));

            _lastInteractionHandle = interactionHandle;
            _hasLoggedInteractionHandle = true;
        }

        int playerTargetHandle;
        bool playerTargetFound =
            TryGetPlayerTargetEntity(out playerTargetHandle);

        if (!playerTargetFound)
        {
            playerTargetHandle = 0;
        }

        if (!_hasLoggedPlayerTargetState ||
            playerTargetFound != _lastPlayerTargetFound ||
            playerTargetHandle != _lastPlayerTargetHandle)
        {
            Log(string.Format(
                "STATE: player target found={0}; handle={1}",
                playerTargetFound,
                FormatHandle(playerTargetHandle)));

            _lastPlayerTargetFound = playerTargetFound;
            _lastPlayerTargetHandle = playerTargetHandle;
            _hasLoggedPlayerTargetState = true;
        }

        if (candidateHandle == 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "Aim a firearm at one NPC to select the test candidate.");

            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "candidate={0}; focus={1}; target-native={2}; " +
                "target={3}; press G.",
                candidateHandle,
                isFreeFocusing,
                playerTargetFound,
                FormatHandle(playerTargetHandle)));
    }

    private static int GetExistingHandle(Entity entity)
    {
        if (entity == null || !entity.Exists())
        {
            return 0;
        }

        return entity.Handle;
    }

    private static string FormatHandle(int handle)
    {
        return handle > 0
            ? handle.ToString()
            : "none";
    }

    private static string GetExceptionMessage(Exception exception)
    {
        if (exception is TargetInvocationException &&
            exception.InnerException != null)
        {
            return exception.InnerException.Message;
        }

        return exception.Message;
    }

    private void Log(string message)
    {
        try
        {
            string directory = Path.GetDirectoryName(_logPath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string timestamp =
                DateTime.Now.ToString("HH:mm:ss.fff");

            File.AppendAllText(
                _logPath,
                string.Format(
                    "[{0}] {1}{2}",
                    timestamp,
                    message,
                    Environment.NewLine));
        }
        catch
        {
            // Logging must not interrupt the in-game test.
        }
    }

    private void ClearLog()
    {
        try
        {
            string directory = Path.GetDirectoryName(_logPath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(_logPath))
            {
                File.Delete(_logPath);
            }
        }
        catch
        {
            // The test can still run if the log cannot be cleared.
        }
    }
}