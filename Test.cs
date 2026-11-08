using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const int StateSampleIntervalMilliseconds = 250;
    private const int FocusLostCaptureMilliseconds = 4000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly List<ControlProbe> _controls =
        new List<ControlProbe>();

    private readonly List<Type> _nativeTypes =
        new List<Type>();

    private readonly HashSet<string> _loggedNativeErrors =
        new HashSet<string>();

    private MethodInfo _isControlJustPressed;
    private MethodInfo _isControlPressed;
    private MethodInfo _isControlEnabled;
    private MethodInfo _getCurrentControlContext;

    private MethodInfo _getEntityHealth;
    private MethodInfo _isPedInCombat;
    private MethodInfo _isPedFleeing;
    private MethodInfo _isPedShooting;
    private MethodInfo _isPedRagdoll;

    private int _lastFocusedTarget;
    private int _sessionTarget;
    private DateTime _captureUntil;
    private DateTime _nextStateSample;
    private string _lastTargetState;
    private bool _generalErrorLogged;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        AddControls();
        ResolveNativeWrappers();

        Log("=== Read-only NPC interaction sniffer initialized ===");
        Log("TEST ONLY: focus an NPC and perform one built-in interaction at a time.");
        Log("The sniffer does not create prompts, register ILOs, or trigger actions.");
        Log("It records focused-human target handles, candidate input-control states,");
        Log("and available observable target-state changes.");
        Log("It does not intercept Rockstar's internal script/native calls or menu payloads.");
        Log("After focus ends, the last target is sampled for up to " +
            FocusLostCaptureMilliseconds + " ms.");
        Log("Use Test_Log.txt and your visual observations together.");

        LogControlWrapperStatus();
        LogTargetStateWrapperStatus();

        RDR2.UI.Screen.PrintSubtitle(
            "Interaction sniffer ready. Focus an NPC and test one action at a time.");

        Tick += OnTick;
    }

    private void AddControls()
    {
        _controls.Add(new ControlProbe(
            "INPUT_AIM",
            0xF84FA74F,
            "candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON",
            0xF8982F00,
            "focus/lock-on"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON_ROB",
            0x9FA5AD07,
            "rob candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON_POS",
            0xF63A17F9,
            "positive interaction candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON_NEG",
            0x26A18F47,
            "negative interaction candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_OPTION1",
            0x760A9C6F,
            "G in the reported InteractionLockOn mapping"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_OPTION2",
            0x84543902,
            "H in the reported InteractionLockOn mapping"));

        _controls.Add(new ControlProbe(
            "INPUT_EMOTE_GREET",
            0x72BAD5AA,
            "greet/emote candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_EMOTE_TAUNT",
            0x470DC190,
            "taunt/emote candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_EMOTE_ACTION",
            0x13C42BB2,
            "emote action candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON_Y",
            0x09A92B8B,
            "lock-on action candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_INTERACT_LOCKON_A",
            0xD10A3A36,
            "lock-on action candidate"));

        _controls.Add(new ControlProbe(
            "INPUT_CONTEXT_X",
            0xE3BF959B,
            "unverified candidate from the earlier test"));
    }

    private void ResolveNativeWrappers()
    {
        try
        {
            Type[] types = typeof(RDR2.Native.HUD).Assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                if (type == null)
                {
                    continue;
                }

                string fullName = type.FullName;

                if (fullName != null &&
                    fullName.StartsWith(
                        "RDR2.Native.",
                        StringComparison.Ordinal))
                {
                    _nativeTypes.Add(type);
                }
            }
        }
        catch (ReflectionTypeLoadException exception)
        {
            Type[] types = exception.Types;

            if (types != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    Type type = types[i];

                    if (type == null)
                    {
                        continue;
                    }

                    string fullName = type.FullName;

                    if (fullName != null &&
                        fullName.StartsWith(
                            "RDR2.Native.",
                            StringComparison.Ordinal))
                    {
                        _nativeTypes.Add(type);
                    }
                }
            }

            Log("NOTE: Native wrapper type scan was partial: " +
                GetExceptionMessage(exception));
        }
        catch (Exception exception)
        {
            Log("NATIVE WRAPPER SCAN FAILED: " +
                GetExceptionMessage(exception));
        }

        _isControlJustPressed =
            FindNativeMethod("IS_CONTROL_JUST_PRESSED", 2);

        _isControlPressed =
            FindNativeMethod("IS_CONTROL_PRESSED", 2);

        _isControlEnabled =
            FindNativeMethod("IS_CONTROL_ENABLED", 2);

        _getCurrentControlContext =
            FindNativeMethod("_GET_CURRENT_CONTROL_CONTEXT", 1);

        _getEntityHealth =
            FindNativeMethod("GET_ENTITY_HEALTH", 1);

        _isPedInCombat =
            FindNativeMethod("IS_PED_IN_COMBAT", 2);

        _isPedFleeing =
            FindNativeMethod("IS_PED_FLEEING", 1);

        _isPedShooting =
            FindNativeMethod("IS_PED_SHOOTING", 1);

        _isPedRagdoll =
            FindNativeMethod("IS_PED_RAGDOLL", 1);
    }

    private MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
    {
        for (int typeIndex = 0;
            typeIndex < _nativeTypes.Count;
            typeIndex++)
        {
            MethodInfo[] methods;

            try
            {
                methods = _nativeTypes[typeIndex].GetMethods(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static);
            }
            catch
            {
                continue;
            }

            for (int methodIndex = 0;
                methodIndex < methods.Length;
                methodIndex++)
            {
                MethodInfo method = methods[methodIndex];

                if (!string.Equals(
                    method.Name,
                    methodName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length != parameterCount)
                {
                    continue;
                }

                bool supportedParameters = true;

                for (int parameterIndex = 0;
                    parameterIndex < parameters.Length;
                    parameterIndex++)
                {
                    if (!IsSupportedNumericType(
                        parameters[parameterIndex].ParameterType))
                    {
                        supportedParameters = false;
                        break;
                    }
                }

                if (supportedParameters)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static bool IsSupportedNumericType(Type type)
    {
        if (type.IsEnum)
        {
            return true;
        }

        return type == typeof(byte) ||
            type == typeof(sbyte) ||
            type == typeof(short) ||
            type == typeof(ushort) ||
            type == typeof(int) ||
            type == typeof(uint) ||
            type == typeof(long) ||
            type == typeof(ulong);
    }

    private void LogControlWrapperStatus()
    {
        Log("CONTROL WRAPPER IS_CONTROL_JUST_PRESSED: " +
            GetMethodSignatureOrUnavailable(_isControlJustPressed));

        Log("CONTROL WRAPPER IS_CONTROL_PRESSED: " +
            GetMethodSignatureOrUnavailable(_isControlPressed));

        Log("CONTROL WRAPPER IS_CONTROL_ENABLED: " +
            GetMethodSignatureOrUnavailable(_isControlEnabled));

        Log("CONTROL WRAPPER _GET_CURRENT_CONTROL_CONTEXT: " +
            GetMethodSignatureOrUnavailable(_getCurrentControlContext));
    }

    private void LogTargetStateWrapperStatus()
    {
        Log("STATE WRAPPER GET_ENTITY_HEALTH: " +
            GetMethodSignatureOrUnavailable(_getEntityHealth));

        Log("STATE WRAPPER IS_PED_IN_COMBAT: " +
            GetMethodSignatureOrUnavailable(_isPedInCombat));

        Log("STATE WRAPPER IS_PED_FLEEING: " +
            GetMethodSignatureOrUnavailable(_isPedFleeing));

        Log("STATE WRAPPER IS_PED_SHOOTING: " +
            GetMethodSignatureOrUnavailable(_isPedShooting));

        Log("STATE WRAPPER IS_PED_RAGDOLL: " +
            GetMethodSignatureOrUnavailable(_isPedRagdoll));
    }

    private static string GetMethodSignatureOrUnavailable(
        MethodInfo method)
    {
        return method == null
            ? "unavailable"
            : GetMethodSignature(method);
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateFocusSession();
            SampleCurrentSession();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("SNIFFER ERROR: " +
                    GetExceptionMessage(exception));

                RDR2.UI.Screen.PrintSubtitle(
                    "Interaction sniffer error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdateFocusSession()
    {
        int focusedTarget = 0;

        if (Game.Player.IsFreeFocusing)
        {
            TryGetFocusedHuman(out focusedTarget);
        }

        if (focusedTarget == _lastFocusedTarget)
        {
            if (focusedTarget > 0)
            {
                _captureUntil = DateTime.Now.AddMilliseconds(
                    FocusLostCaptureMilliseconds);
            }

            return;
        }

        if (_lastFocusedTarget > 0)
        {
            Log("FOCUS END: target=" + _lastFocusedTarget +
                "; continue sampling briefly for response changes.");

            _captureUntil = DateTime.Now.AddMilliseconds(
                FocusLostCaptureMilliseconds);
        }

        _lastFocusedTarget = focusedTarget;

        if (focusedTarget > 0)
        {
            _sessionTarget = focusedTarget;
            _captureUntil = DateTime.Now.AddMilliseconds(
                FocusLostCaptureMilliseconds);
            _nextStateSample = DateTime.MinValue;
            _lastTargetState = null;

            Log("FOCUS BEGIN: target=" + focusedTarget);
            LogControlAvailability(focusedTarget);
            CaptureTargetState(focusedTarget, "focus-begin");
        }
        else if (_sessionTarget > 0)
        {
            Log("FOCUSED HUMAN TARGET: none; previous target=" +
                _sessionTarget +
                "; response sampling remains active briefly.");
        }
    }

    private bool TryGetFocusedHuman(out int targetHandle)
    {
        targetHandle = 0;

        if (!Game.Player.IsFreeFocusing)
        {
            return false;
        }

        int foundHandle = 0;
        bool found;

        unsafe
        {
            found = RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                Game.Player.Handle,
                &foundHandle);
        }

        if (!found ||
            foundHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(foundHandle) ||
            foundHandle == Game.Player.Character.Handle ||
            !RDR2.Native.PED.IS_PED_HUMAN(foundHandle))
        {
            return false;
        }

        targetHandle = foundHandle;
        return true;
    }

    private void SampleCurrentSession()
    {
        if (_sessionTarget <= 0)
        {
            return;
        }

        DateTime now = DateTime.Now;

        if (now > _captureUntil)
        {
            Log("CAPTURE END: target=" + _sessionTarget);
            _sessionTarget = 0;
            _lastTargetState = null;
            return;
        }

        SampleControlInputs(_sessionTarget);

        if (now >= _nextStateSample)
        {
            CaptureTargetState(_sessionTarget, "periodic");
            _nextStateSample = now.AddMilliseconds(
                StateSampleIntervalMilliseconds);
        }
    }

    private void LogControlAvailability(int targetHandle)
    {
        if (_isControlEnabled == null)
        {
            Log("CONTROL AVAILABILITY: IS_CONTROL_ENABLED wrapper unavailable.");
            return;
        }

        for (int i = 0; i < _controls.Count; i++)
        {
            ControlProbe control = _controls[i];

            bool enabled;
            string error;

            if (TryInvokeControlBoolean(
                _isControlEnabled,
                control.Hash,
                out enabled,
                out error))
            {
                control.LastEnabled = enabled;
                control.HasEnabledSample = true;

                Log("CONTROL AVAILABLE: target=" + targetHandle +
                    "; name=" + control.Name +
                    "; hash=0x" + control.Hash.ToString("X8") +
                    "; enabled=" + enabled +
                    "; note=" + control.Note);
            }
            else
            {
                LogNativeErrorOnce(
                    "IS_CONTROL_ENABLED",
                    error);

                Log("CONTROL AVAILABLE: target=" + targetHandle +
                    "; name=" + control.Name +
                    "; hash=0x" + control.Hash.ToString("X8") +
                    "; enabled=unknown" +
                    "; note=" + control.Note);
            }
        }
    }

    private void SampleControlInputs(int targetHandle)
    {
        if (_isControlJustPressed == null &&
            _isControlPressed == null)
        {
            return;
        }

        for (int i = 0; i < _controls.Count; i++)
        {
            ControlProbe control = _controls[i];

            bool pressed = false;
            bool justPressed = false;
            bool pressedReadSucceeded = false;
            bool justPressedReadSucceeded = false;
            string error;

            if (_isControlPressed != null)
            {
                pressedReadSucceeded =
                    TryInvokeControlBoolean(
                        _isControlPressed,
                        control.Hash,
                        out pressed,
                        out error);

                if (!pressedReadSucceeded)
                {
                    LogNativeErrorOnce(
                        "IS_CONTROL_PRESSED",
                        error);
                }
            }

            if (_isControlJustPressed != null)
            {
                justPressedReadSucceeded =
                    TryInvokeControlBoolean(
                        _isControlJustPressed,
                        control.Hash,
                        out justPressed,
                        out error);

                if (!justPressedReadSucceeded)
                {
                    LogNativeErrorOnce(
                        "IS_CONTROL_JUST_PRESSED",
                        error);
                }
            }

            if (pressedReadSucceeded)
            {
                if (!control.HasPressedSample ||
                    control.LastPressed != pressed)
                {
                    control.HasPressedSample = true;
                    control.LastPressed = pressed;

                    Log("CONTROL STATE: target=" + targetHandle +
                        "; name=" + control.Name +
                        "; hash=0x" + control.Hash.ToString("X8") +
                        "; pressed=" + pressed +
                        "; note=" + control.Note);
                }
            }

            if (justPressedReadSucceeded && justPressed)
            {
                string context = ReadCurrentControlContext(
                    control.Hash);

                Log("CONTROL JUST PRESSED: target=" + targetHandle +
                    "; name=" + control.Name +
                    "; hash=0x" + control.Hash.ToString("X8") +
                    "; pressedNow=" + pressed +
                    "; currentContext=" + context +
                    "; note=" + control.Note);

                CaptureTargetState(
                    targetHandle,
                    "control-" + control.Name);
            }
        }
    }

    private bool TryInvokeControlBoolean(
        MethodInfo method,
        uint controlHash,
        out bool result,
        out string error)
    {
        result = false;
        error = null;

        if (method == null)
        {
            error = "wrapper method unavailable";
            return false;
        }

        try
        {
            ParameterInfo[] parameters = method.GetParameters();

            object[] arguments =
            {
                ConvertNativeNumber(
                    ControlPadIndex,
                    parameters[0].ParameterType),
                ConvertNativeHash(
                    controlHash,
                    parameters[1].ParameterType)
            };

            object nativeResult = method.Invoke(
                null,
                arguments);

            if (nativeResult == null)
            {
                error = "wrapper returned null";
                return false;
            }

            result = Convert.ToBoolean(nativeResult);
            return true;
        }
        catch (Exception exception)
        {
            error = GetExceptionMessage(exception);
            return false;
        }
    }

    private string ReadCurrentControlContext(uint controlHash)
    {
        if (_getCurrentControlContext == null)
        {
            return "wrapper-unavailable";
        }

        try
        {
            ParameterInfo[] parameters =
                _getCurrentControlContext.GetParameters();

            object[] arguments =
            {
                ConvertNativeHash(
                    controlHash,
                    parameters[0].ParameterType)
            };

            object result = _getCurrentControlContext.Invoke(
                null,
                arguments);

            return result == null
                ? "null"
                : result.ToString();
        }
        catch (Exception exception)
        {
            string error = GetExceptionMessage(exception);
            LogNativeErrorOnce(
                "_GET_CURRENT_CONTROL_CONTEXT",
                error);
            return "error";
        }
    }

    private void CaptureTargetState(
        int targetHandle,
        string reason)
    {
        bool exists =
            RDR2.Native.ENTITY.DOES_ENTITY_EXIST(targetHandle);

        bool human =
            exists &&
            RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

        string state =
            "target=" + targetHandle +
            "; exists=" + exists +
            "; human=" + human +
            "; health=" + ReadNativeValue(
                _getEntityHealth,
                new object[] { targetHandle }) +
            "; inCombatWithAny=" + ReadNativeValue(
                _isPedInCombat,
                new object[] { targetHandle, 0 }) +
            "; fleeing=" + ReadNativeValue(
                _isPedFleeing,
                new object[] { targetHandle }) +
            "; shooting=" + ReadNativeValue(
                _isPedShooting,
                new object[] { targetHandle }) +
            "; ragdoll=" + ReadNativeValue(
                _isPedRagdoll,
                new object[] { targetHandle });

        if (!string.Equals(
            state,
            _lastTargetState,
            StringComparison.Ordinal))
        {
            Log("TARGET STATE CHANGE: reason=" + reason +
                "; " + state);
            _lastTargetState = state;
        }
    }

    private string ReadNativeValue(
        MethodInfo method,
        object[] suppliedArguments)
    {
        if (method == null)
        {
            return "unavailable";
        }

        try
        {
            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length != suppliedArguments.Length)
            {
                return "parameter-count-mismatch";
            }

            object[] arguments =
                new object[suppliedArguments.Length];

            for (int i = 0; i < suppliedArguments.Length; i++)
            {
                arguments[i] = ConvertNativeNumber(
                    Convert.ToInt64(suppliedArguments[i]),
                    parameters[i].ParameterType);
            }

            object result = method.Invoke(
                null,
                arguments);

            return result == null
                ? "null"
                : result.ToString();
        }
        catch (Exception exception)
        {
            string key = method.Name;
            string error = GetExceptionMessage(exception);

            LogNativeErrorOnce(key, error);
            return "error";
        }
    }

    private static object ConvertNativeHash(
        uint value,
        Type parameterType)
    {
        if (parameterType.IsEnum)
        {
            return Enum.ToObject(
                parameterType,
                unchecked((int)value));
        }

        if (parameterType == typeof(int))
        {
            return unchecked((int)value);
        }

        if (parameterType == typeof(uint))
        {
            return value;
        }

        if (parameterType == typeof(long))
        {
            return (long)unchecked((int)value);
        }

        if (parameterType == typeof(ulong))
        {
            return (ulong)value;
        }

        return Convert.ChangeType(
            value,
            parameterType);
    }

    private static object ConvertNativeNumber(
        long value,
        Type parameterType)
    {
        if (parameterType.IsEnum)
        {
            return Enum.ToObject(
                parameterType,
                value);
        }

        if (parameterType == typeof(byte))
        {
            return unchecked((byte)value);
        }

        if (parameterType == typeof(sbyte))
        {
            return unchecked((sbyte)value);
        }

        if (parameterType == typeof(short))
        {
            return unchecked((short)value);
        }

        if (parameterType == typeof(ushort))
        {
            return unchecked((ushort)value);
        }

        if (parameterType == typeof(int))
        {
            return unchecked((int)value);
        }

        if (parameterType == typeof(uint))
        {
            return unchecked((uint)value);
        }

        if (parameterType == typeof(long))
        {
            return value;
        }

        if (parameterType == typeof(ulong))
        {
            return unchecked((ulong)value);
        }

        return Convert.ChangeType(
            value,
            parameterType);
    }

    private void LogNativeErrorOnce(
        string key,
        string error)
    {
        string combinedKey = key + "|" + error;

        if (_loggedNativeErrors.Contains(combinedKey))
        {
            return;
        }

        _loggedNativeErrors.Add(combinedKey);

        Log("NATIVE PROBE ERROR: " + key +
            "; " + error);
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }

    private static string GetMethodSignature(
        MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        string[] parameterNames = new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterNames[i] =
                parameters[i].ParameterType.Name;
        }

        return method.DeclaringType.FullName +
            "." +
            method.Name +
            "(" +
            string.Join(", ", parameterNames) +
            ")";
    }

    private static string GetExceptionMessage(
        Exception exception)
    {
        if (exception == null)
        {
            return "Unknown error.";
        }

        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.GetType().FullName +
            ": " +
            current.Message;
    }

    private sealed class ControlProbe
    {
        public readonly string Name;
        public readonly uint Hash;
        public readonly string Note;

        public bool HasPressedSample;
        public bool LastPressed;

        public bool HasEnabledSample;
        public bool LastEnabled;

        public ControlProbe(
            string name,
            uint hash,
            string note)
        {
            Name = name;
            Hash = hash;
            Note = note;
        }
    }
}