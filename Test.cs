using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    // INPUT_INTERACT_OPTION1 in the InteractionLockOn context.
    // The control reference maps this action to G by default.
    private const int RecruitInputActionHash = unchecked((int)0x760A9C6F);

    private const int PromptTextLiteralString = 10;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExist;
    private MethodInfo _isPedHuman;
    private MethodInfo _getTargetPromptGroup;

    private MethodInfo _promptRegisterBegin;
    private MethodInfo _promptSetControlAction;
    private MethodInfo _promptSetText;
    private MethodInfo _promptSetStandardMode;
    private MethodInfo _promptSetEnabled;
    private MethodInfo _promptSetVisible;
    private MethodInfo _promptSetGroup;
    private MethodInfo _promptRegisterEnd;
    private MethodInfo _promptHasStandardModeCompleted;
    private MethodInfo _promptDelete;
    private MethodInfo _varString;

    private int _targetHandle;
    private int _lastLoggedTarget = -1;
    private int _promptHandle = -1;
    private int _promptGroup = -1;

    private bool _completedForTarget;
    private bool _setupFailedForTarget;
    private bool _errorLogged;

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Native lock-on G Recruit prompt test initialized ===");
        Log(
            "TEST ONLY: create a temporary G Recruit action prompt " +
            "for the focused human NPC.");
        Log(
            "INPUT ACTION: INPUT_INTERACT_OPTION1 " +
            "(0x760A9C6F; InteractionLockOn context; default G).");
        Log(
            "PROMPT CREATION: native register-begin/set-control/" +
            "set-text/register-end sequence.");
        Log(
            "PROMPT GROUP: assign the native prompt to the focused " +
            "entity's target prompt group.");
        Log(
            "ILO registration is deliberately omitted; this test " +
            "uses the already-focused human target.");
        Log(
            "No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log(
            "Unarmed test conditions: stand close to a human NPC, " +
            "right-mouse focus them, and check for G Recruit.");
        Log(
            "Native wrapper signatures and in-game rendering are " +
            "logged separately; successful calls do not prove display.");

        ResolveMethods();

        RDR2.UI.Screen.PrintSubtitle(
            "Unarmed: focus a human NPC to test G Recruit.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdatePrompt();
        }
        catch (Exception exception)
        {
            if (!_errorLogged)
            {
                _errorLogged = true;
                Log("ERROR: " + GetExceptionMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt test error. Check Test_Log.txt.");
            }

            ClearPrompt();
        }
    }

    private void UpdatePrompt()
    {
        int focusedTarget = 0;

        if (Game.Player.IsFreeFocusing)
        {
            TryGetFocusedHuman(out focusedTarget);
        }

        if (focusedTarget != _targetHandle)
        {
            ClearPrompt();

            _targetHandle = focusedTarget;
            _completedForTarget = false;
            _setupFailedForTarget = false;

            if (_lastLoggedTarget != focusedTarget)
            {
                Log(
                    focusedTarget > 0
                        ? "FOCUSED HUMAN TARGET: " + focusedTarget
                        : "FOCUSED HUMAN TARGET: none");

                _lastLoggedTarget = focusedTarget;
            }
        }

        if (_targetHandle <= 0 ||
            _completedForTarget ||
            _setupFailedForTarget)
        {
            return;
        }

        if (_promptHandle <= 0)
        {
            CreateTargetPrompt(_targetHandle);
        }

        if (_promptHandle <= 0)
        {
            return;
        }

        object completedResult = InvokeNative(
            _promptHasStandardModeCompleted,
            _promptHandle);

        bool completed =
            completedResult != null &&
            Convert.ToBoolean(completedResult);

        if (completed)
        {
            _completedForTarget = true;

            Log(
                "TEST PROMPT COMPLETED: G action detected by native " +
                "standard-mode completion; no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "G Recruit test completed; no recruitment performed.");

            ClearPrompt();
        }
    }

    private bool TryGetFocusedHuman(out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        int foundHandle = 0;
        bool found;

        unsafe
        {
            found = _getPlayerTargetEntity(
                Game.Player.Handle,
                &foundHandle);
        }

        if (!found ||
            foundHandle <= 0 ||
            _doesEntityExist == null ||
            _isPedHuman == null)
        {
            return false;
        }

        object existsResult = InvokeNative(
            _doesEntityExist,
            foundHandle);

        bool exists =
            existsResult != null &&
            Convert.ToBoolean(existsResult);

        if (!exists ||
            foundHandle == Game.Player.Character.Handle)
        {
            return false;
        }

        object humanResult = InvokeNative(
            _isPedHuman,
            foundHandle);

        bool isHuman =
            humanResult != null &&
            Convert.ToBoolean(humanResult);

        if (!isHuman)
        {
            return false;
        }

        targetHandle = foundHandle;
        return true;
    }

    private void CreateTargetPrompt(int targetHandle)
    {
        try
        {
            if (_getTargetPromptGroup == null)
            {
                throw new InvalidOperationException(
                    "Target prompt-group native was not resolved.");
            }

            object groupResult = InvokeNative(
                _getTargetPromptGroup,
                targetHandle);

            _promptGroup = Convert.ToInt32(groupResult);

            if (_promptGroup <= 0)
            {
                throw new InvalidOperationException(
                    "Target returned an invalid prompt group: " +
                    _promptGroup);
            }

            object beginResult = InvokeNative(
                _promptRegisterBegin);

            _promptHandle = Convert.ToInt32(beginResult);

            if (_promptHandle <= 0)
            {
                throw new InvalidOperationException(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid " +
                    "prompt handle.");
            }

            Log(
                "PROMPT REGISTER BEGIN: handle=" +
                _promptHandle);

            InvokeNative(
                _promptSetControlAction,
                _promptHandle,
                RecruitInputActionHash);

            object textResult = InvokeNative(
                _varString,
                PromptTextLiteralString,
                "LITERAL_STRING",
                "Recruit");

            if (textResult == null)
            {
                throw new InvalidOperationException(
                    "VAR_STRING returned null.");
            }

            InvokeNative(
                _promptSetText,
                _promptHandle,
                textResult);

            InvokeNative(
                _promptSetStandardMode,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetEnabled,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetVisible,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetGroup,
                _promptHandle,
                _promptGroup,
                0);

            InvokeNative(
                _promptRegisterEnd,
                _promptHandle);

            Log(
                "PROMPT REGISTERED: target=" + targetHandle +
                "; handle=" + _promptHandle +
                "; targetGroup=" + _promptGroup);
            Log(
                "PROMPT CONTROL: INPUT_INTERACT_OPTION1 " +
                "(0x760A9C6F; default G in InteractionLockOn).");
            Log(
                "PROMPT TEXT: VAR_STRING(10, LITERAL_STRING, Recruit).");
            Log(
                "IN-GAME CHECK: confirm whether G Recruit is visible " +
                "in the focused NPC's lock-on menu. Native return " +
                "values alone do not prove rendering.");
        }
        catch (Exception exception)
        {
            _setupFailedForTarget = true;

            Log(
                "PROMPT SETUP FAILED: " +
                GetExceptionMessage(exception));

            ClearPrompt();
        }
    }

    private void ResolveMethods()
    {
        MethodInfo targetMethod =
            FindNativeMethod("GET_PLAYER_TARGET_ENTITY", 2);

        if (targetMethod != null)
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)Delegate.CreateDelegate(
                    typeof(PlayerTargetEntityNative),
                    targetMethod);

            Log(
                "PLAYER TARGET: " +
                DescribeMethod(targetMethod));
        }
        else
        {
            Log(
                "ERROR: GET_PLAYER_TARGET_ENTITY wrapper not found.");
        }

        _doesEntityExist =
            FindNativeMethod("DOES_ENTITY_EXIST", 1);

        _isPedHuman =
            FindNativeMethod("IS_PED_HUMAN", 1);

        _getTargetPromptGroup =
            FindNativeMethod(
                "_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
                1);

        _promptRegisterBegin =
            FindNativeMethod(
                "_UI_PROMPT_REGISTER_BEGIN",
                0);

        _promptSetControlAction =
            FindNativeMethod(
                "_UI_PROMPT_SET_CONTROL_ACTION",
                2);

        _promptSetText =
            FindNativeMethod(
                "_UI_PROMPT_SET_TEXT",
                2);

        _promptSetStandardMode =
            FindNativeMethod(
                "_UI_PROMPT_SET_STANDARD_MODE",
                2);

        _promptSetEnabled =
            FindNativeMethod(
                "_UI_PROMPT_SET_ENABLED",
                2);

        _promptSetVisible =
            FindNativeMethod(
                "_UI_PROMPT_SET_VISIBLE",
                2);

        _promptSetGroup =
            FindNativeMethod(
                "_UI_PROMPT_SET_GROUP",
                3);

        _promptRegisterEnd =
            FindNativeMethod(
                "_UI_PROMPT_REGISTER_END",
                1);

        _promptHasStandardModeCompleted =
            FindNativeMethod(
                "_UI_PROMPT_HAS_STANDARD_MODE_COMPLETED",
                1);

        _promptDelete =
            FindNativeMethod(
                "_UI_PROMPT_DELETE",
                1);

        _varString =
            FindNativeMethod(
                "VAR_STRING",
                3);

        LogResolvedMethod(
            "DOES ENTITY EXIST",
            _doesEntityExist);
        LogResolvedMethod(
            "IS PED HUMAN",
            _isPedHuman);
        LogResolvedMethod(
            "TARGET PROMPT GROUP",
            _getTargetPromptGroup);
        LogResolvedMethod(
            "PROMPT REGISTER BEGIN",
            _promptRegisterBegin);
        LogResolvedMethod(
            "PROMPT SET CONTROL ACTION",
            _promptSetControlAction);
        LogResolvedMethod(
            "PROMPT SET TEXT",
            _promptSetText);
        LogResolvedMethod(
            "PROMPT SET STANDARD MODE",
            _promptSetStandardMode);
        LogResolvedMethod(
            "PROMPT SET ENABLED",
            _promptSetEnabled);
        LogResolvedMethod(
            "PROMPT SET VISIBLE",
            _promptSetVisible);
        LogResolvedMethod(
            "PROMPT SET GROUP",
            _promptSetGroup);
        LogResolvedMethod(
            "PROMPT REGISTER END",
            _promptRegisterEnd);
        LogResolvedMethod(
            "PROMPT HAS STANDARD MODE COMPLETED",
            _promptHasStandardModeCompleted);
        LogResolvedMethod(
            "PROMPT DELETE",
            _promptDelete);
        LogResolvedMethod(
            "VAR STRING",
            _varString);

        if (_doesEntityExist == null ||
            _isPedHuman == null ||
            _getTargetPromptGroup == null ||
            _promptRegisterBegin == null ||
            _promptSetControlAction == null ||
            _promptSetText == null ||
            _promptSetStandardMode == null ||
            _promptSetEnabled == null ||
            _promptSetVisible == null ||
            _promptSetGroup == null ||
            _promptRegisterEnd == null ||
            _promptHasStandardModeCompleted == null ||
            _promptDelete == null ||
            _varString == null)
        {
            Log(
                "ERROR: One or more required native wrappers are " +
                "missing. The test will log setup failure rather " +
                "than switch to another prompt implementation.");
        }
    }

    private void LogResolvedMethod(
        string label,
        MethodInfo method)
    {
        Log(
            method == null
                ? "ERROR: " + label + " wrapper not found."
                : label + ": " + DescribeMethod(method));
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
    {
        Type[] types;

        try
        {
            types = typeof(Game).Assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            types = exception.Types;
        }

        string normalizedName =
            methodName.TrimStart('_');

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

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                string candidateName =
                    method.Name.TrimStart('_');

                if (string.Equals(
                        candidateName,
                        normalizedName,
                        StringComparison.OrdinalIgnoreCase) &&
                    method.GetParameters().Length == parameterCount)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static object InvokeNative(
        MethodInfo method,
        params object[] arguments)
    {
        if (method == null)
        {
            throw new InvalidOperationException(
                "Attempted to invoke an unresolved native wrapper.");
        }

        ParameterInfo[] parameters = method.GetParameters();

        if (parameters.Length != arguments.Length)
        {
            throw new InvalidOperationException(
                "Native argument count mismatch for " +
                DescribeMethod(method) +
                "; supplied=" + arguments.Length);
        }

        object[] convertedArguments =
            new object[arguments.Length];

        for (int i = 0; i < arguments.Length; i++)
        {
            convertedArguments[i] = ConvertArgument(
                arguments[i],
                parameters[i].ParameterType);
        }

        return method.Invoke(
            null,
            convertedArguments);
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
        if (value == null)
        {
            return targetType.IsValueType
                ? Activator.CreateInstance(targetType)
                : null;
        }

        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(targetType, value);
        }

        if (targetType == typeof(IntPtr))
        {
            long numericValue = Convert.ToInt64(value);
            return new IntPtr(numericValue);
        }

        if (targetType == typeof(UIntPtr))
        {
            ulong numericValue = Convert.ToUInt64(value);
            return new UIntPtr(numericValue);
        }

        if (targetType == typeof(uint))
        {
            return unchecked((uint)Convert.ToUInt64(value));
        }

        if (targetType == typeof(ulong))
        {
            return Convert.ToUInt64(value);
        }

        if (targetType == typeof(int))
        {
            return unchecked((int)Convert.ToInt64(value));
        }

        if (targetType == typeof(long))
        {
            return Convert.ToInt64(value);
        }

        return Convert.ChangeType(value, targetType);
    }

    private void ClearPrompt()
    {
        int oldPromptHandle = _promptHandle;

        _promptHandle = -1;
        _promptGroup = -1;

        if (oldPromptHandle <= 0 ||
            _promptDelete == null)
        {
            return;
        }

        try
        {
            InvokeNative(
                _promptDelete,
                oldPromptHandle);

            Log(
                "PROMPT DELETE: handle=" +
                oldPromptHandle);
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT DELETE FAILED: " +
                GetExceptionMessage(exception));
        }
    }

    private static string DescribeMethod(MethodInfo method)
    {
        ParameterInfo[] parameters =
            method.GetParameters();

        string[] types =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            types[i] =
                parameters[i].ParameterType.FullName;
        }

        return method.DeclaringType.FullName +
            "." +
            method.Name +
            "(" +
            string.Join(", ", types) +
            ") -> " +
            method.ReturnType.FullName;
    }

    private static string GetExceptionMessage(
        Exception exception)
    {
        TargetInvocationException invocationException =
            exception as TargetInvocationException;

        return invocationException != null &&
            invocationException.InnerException != null
                ? invocationException.InnerException.Message
                : exception.Message;
    }

    private void Log(string message)
    {
        string line =
            "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " +
            message +
            Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }
}