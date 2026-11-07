using System;
using System.Reflection;
using System.Windows.Forms;
using RDR2;

public class Test : Script
{
    private const int ReportIntervalMilliseconds = 750;
    private Entity _candidate;
    private bool _iloRegistered;
    private string _registrationStatus = "waiting for an NPC target";
    private DateTime _nextReportUtc = DateTime.MinValue;
    private bool _errorReported;

    public Test()
    {
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
                RDR2.UI.Screen.PrintSubtitle(
                    string.Format(
                        "Lock-on test error: {0}",
                        exception.Message));
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
        ProcessTestKey();
    }

    private void RefreshCandidate()
    {
        // Reset candidate if it no longer exists
        if (_candidate != null && !_candidate.Exists())
        {
            _candidate = null;
            _iloRegistered = false;
            _registrationStatus = "candidate lost";
            return;
        }

        // Try to capture from free aim first (aiming with gun)
        Entity aimedEntity = Game.Player.GetFreeAimEntity();
        if (aimedEntity != null &&
            aimedEntity.Exists() &&
            aimedEntity.IsPed)
        {
            if (_candidate == null || _candidate.Handle != aimedEntity.Handle)
            {
                _candidate = aimedEntity;
                _iloRegistered = false;
                _registrationStatus = "capturing...";
                RegisterInteractionLockonPrompt(_candidate);
            }
            return;
        }

        // If no free-aim entity, try to capture from interaction lock-on target
        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();
        if (interactionTarget != null &&
            interactionTarget.Exists() &&
            interactionTarget.IsPed)
        {
            if (_candidate == null || _candidate.Handle != interactionTarget.Handle)
            {
                _candidate = interactionTarget;
                _iloRegistered = false;
                _registrationStatus = "capturing...";
                RegisterInteractionLockonPrompt(_candidate);
            }
            return;
        }

        // No valid target found
        if (_candidate != null)
        {
            _candidate = null;
            _iloRegistered = false;
            _registrationStatus = "no target";
        }
    }

    private void RegisterInteractionLockonPrompt(Entity entity)
    {
        MethodInfo method =
            FindNativeMethod("REGISTER_INTERACTION_LOCKON_PROMPT");
        if (method == null)
        {
            _registrationStatus =
                "REGISTER_INTERACTION_LOCKON_PROMPT wrapper not found";
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();
        if (parameters.Length != 10)
        {
            _registrationStatus =
                "ILO wrapper found with unexpected parameter count: " +
                parameters.Length.ToString();
            return;
        }

        try
        {
            object[] arguments =
                BuildLockonArguments(entity, parameters);
            method.Invoke(null, arguments);
            _iloRegistered = true;
            _registrationStatus = "ILO registered";
        }
        catch (Exception exception)
        {
            _registrationStatus =
                "ILO registration failed: " +
                GetExceptionMessage(exception);
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
            5.0f, // focus distance
            0, // flags
            0.0f, // p5
            0.0f, // p6
            0, // prompt handle
            true, // show the prompt when approaching
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
        if (_candidate == null || !_candidate.Exists())
        {
            RDR2.UI.Screen.PrintSubtitle(
                "G test blocked: no valid NPC candidate.");
            return;
        }

        if (!_iloRegistered)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "G test blocked: ILO not registered for candidate.");
            return;
        }

        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();
        int interactionHandle =
            GetExistingHandle(interactionTarget);

        if (interactionHandle != _candidate.Handle)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "G test blocked: candidate={0}; interaction target={1}.",
                    _candidate.Handle,
                    FormatHandle(interactionHandle)));
            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "G TEST ACCEPTED for NPC handle {0}. No recruitment performed.",
                _candidate.Handle));
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

        bool isFreeFocusing = Game.Player.IsFreeFocusing;
        Entity interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity();
        int interactionHandle =
            GetExistingHandle(interactionTarget);

        if (_candidate == null)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "Aim a gun at one NPC or right-click lock-on to arm the ILO test.");
            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "ILO={0} ped={1}; focus={2}; lock={3}; press G to test.",
                _registrationStatus,
                _candidate.Exists()
                    ? _candidate.Handle.ToString()
                    : "gone",
                isFreeFocusing ? "1" : "0",
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
}