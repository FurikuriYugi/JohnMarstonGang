using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RDR2;

public class Test : Script
{
    private const int ReportIntervalMilliseconds = 500;

    // AppDomain.CurrentDomain.BaseDirectory is already the scripts folder
    // in the tested setup.
    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private Entity _candidate;
    private int _lastCandidateHandle = -1;
    private bool _iloRegistered;
    private bool _lastIloRegistered;
    private string _registrationStatus = "waiting for an NPC target";
    private DateTime _nextReportUtc = DateTime.MinValue;
    private bool _errorReported;

    private bool _hasLoggedFreeFocusing;
    private bool _lastFreeFocusing;
    private bool _hasLoggedInteractionHandle;
    private int _lastInteractionHandle;

    public Test()
    {
        ClearLog();
        Log("=== Test.cs Initialized ===");
        Log("Log file: " + _logPath);
        Log("Test: aim at an NPC to register it, then right-click focus and press G.");
        Log("G is diagnostic only; this test does not recruit.");

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
            _iloRegistered = false;
            _registrationStatus = "candidate lost";
        }

        // Capture or switch the test candidate while aiming a firearm.
        Entity aimedEntity = Game.Player.GetFreeAimEntity();

        if (aimedEntity != null &&
            aimedEntity.Exists() &&
            aimedEntity.IsPed)
        {
            SetCandidate(aimedEntity, "FREE AIM");
            return;
        }

        // If the game reports an interaction-lock-on target, use it as
        // the candidate only when it differs from the current candidate.
        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();

        if (interactionTarget != null &&
            interactionTarget.Exists() &&
            interactionTarget.IsPed)
        {
            SetCandidate(interactionTarget, "LOCK-ON");
        }

        // Deliberately do not clear the candidate just because aim or
        // lock-on is temporarily absent. That was causing the previous
        // test to discard the candidate immediately after releasing aim.
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
        _iloRegistered = false;
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
            Log("ERROR: registration skipped; candidate is invalid");
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

            // This means the wrapper call returned without throwing.
            // It does not by itself confirm an in-game prompt appeared.
            _iloRegistered = true;
            _registrationStatus = "ILO wrapper call returned";
            Log("ILO CALL RETURNED WITHOUT EXCEPTION");
        }
        catch (Exception exception)
        {
            _iloRegistered = false;
            _registrationStatus =
                "ILO registration call failed: " +
                GetExceptionMessage(exception);

            Log(
                "ERROR: ILO registration call failed: " +
                GetExceptionMessage(exception));
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

    private void ProcessTestKey()
    {
        Log("--- G KEY PROCESSING ---");

        bool isFreeFocusing = Game.Player.IsFreeFocusing;
        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();
        int interactionHandle = GetExistingHandle(interactionTarget);
        int candidateHandle = GetExistingHandle(_candidate);

        Log(string.Format(
            "G SNAPSHOT: candidate={0}; ILO-call-returned={1}; " +
            "free-focusing={2}; interaction-target={3}",
            FormatHandle(candidateHandle),
            _iloRegistered,
            isFreeFocusing,
            FormatHandle(interactionHandle)));

        if (candidateHandle == 0)
        {
            const string message =
                "G test blocked: no valid NPC candidate. Aim at an NPC first.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (!_iloRegistered)
        {
            const string message =
                "G test blocked: ILO wrapper call did not complete successfully.";

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        if (interactionHandle != candidateHandle)
        {
            string message = string.Format(
                "G test blocked: candidate={0}; focus={1}; target={2}.",
                candidateHandle,
                isFreeFocusing,
                FormatHandle(interactionHandle));

            RDR2.UI.Screen.PrintSubtitle(message);
            Log("BLOCKED: " + message);
            return;
        }

        string successMessage = string.Format(
            "G TEST ACCEPTED for NPC handle {0}. No recruitment performed.",
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

        if (_iloRegistered != _lastIloRegistered)
        {
            Log(string.Format(
                "STATE: ILO-call-returned changed {0} -> {1}",
                _lastIloRegistered,
                _iloRegistered));

            _lastIloRegistered = _iloRegistered;
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
        int interactionHandle = GetExistingHandle(interactionTarget);

        if (!_hasLoggedInteractionHandle ||
            interactionHandle != _lastInteractionHandle)
        {
            Log(string.Format(
                "STATE: interaction target={0}",
                FormatHandle(interactionHandle)));

            _lastInteractionHandle = interactionHandle;
            _hasLoggedInteractionHandle = true;
        }

        if (candidateHandle == 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "Aim a firearm at one NPC to select the test candidate.");

            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "ILO call={0}; candidate={1}; focus={2}; target={3}; press G.",
                _registrationStatus,
                candidateHandle,
                isFreeFocusing,
                FormatHandle(interactionHandle)));
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

            string logEntry = string.Format(
                "[{0}] {1}",
                timestamp,
                message);

            File.AppendAllText(
                _logPath,
                logEntry + Environment.NewLine);
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