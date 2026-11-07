using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
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
    private bool _completionWarningLogged;
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
        Log("No ambient-group or active-group activation is performed.");
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

        CheckPromptCompletion();
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
            RequirePromptSetupWrappers();

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

            if (_promptHasStandardModeCompleted == null)
            {
                Log(
                    "PROMPT COMPLETION CHECK: wrapper unavailable; " +
                    "the prompt will remain until focus changes. " +
                    "Visibility can still be tested in-game.");
            }
            else
            {
                Log(
                    "PROMPT COMPLETION CHECK: standard-mode completion " +
                    "wrapper resolved.");
            }

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

    private void RequirePromptSetupWrappers()
    {
        if (_getTargetPromptGroup == null)
        {
            throw new InvalidOperationException(
                "Target prompt-group native was not resolved.");
        }

        if (_promptRegisterBegin == null)
        {
            throw new InvalidOperationException(
                "Prompt register-begin native was not resolved.");
        }

        if (_promptSetControlAction == null)
        {
            throw new InvalidOperationException(
                "Prompt set-control-action native was not resolved.");
        }

        if (_varString == null)
        {
            throw new InvalidOperationException(
                "No compatible VAR_STRING wrapper was resolved.");
        }

        if (_promptSetText == null)
        {
            throw new InvalidOperationException(
                "Prompt set-text native was not resolved.");
        }

        if (_promptSetStandardMode == null)
        {
            throw new InvalidOperationException(
                "Prompt set-standard-mode native was not resolved.");
        }

        if (_promptSetEnabled == null)
        {
            throw new InvalidOperationException(
                "Prompt set-enabled native was not resolved.");
        }

        if (_promptSetVisible == null)
        {
            throw new InvalidOperationException(
                "Prompt set-visible native was not resolved.");
        }

        if (_promptSetGroup == null)
        {
            throw new InvalidOperationException(
                "Prompt set-group native was not resolved.");
        }

        if (_promptRegisterEnd == null)
        {
            throw new InvalidOperationException(
                "Prompt register-end native was not resolved.");
        }

        if (_promptDelete == null)
        {
            throw new InvalidOperationException(
                "Prompt delete native was not resolved.");
        }
    }

    private void CheckPromptCompletion()
    {
        if (_promptHasStandardModeCompleted == null)
        {
            if (!_completionWarningLogged)
            {
                _completionWarningLogged = true;

                Log(
                    "PROMPT COMPLETION CHECK SKIPPED: wrapper is " +
                    "unavailable. Prompt remains active until focus " +
                    "changes; no recruitment is performed.");
            }

            return;
        }

        try
        {
            object completedResult = InvokeNative(
                _promptHasStandardModeCompleted,
                _promptHandle);

            bool completed =
                completedResult != null &&
                Convert.ToBoolean(completedResult);

            if (!completed)
            {
                return;
            }

            _completedForTarget = true;

            Log(
                "TEST PROMPT COMPLETED: G action detected by native " +
                "standard-mode completion; no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "G Recruit test completed; no recruitment performed.");

            ClearPrompt();
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT COMPLETION CHECK FAILED; prompt is being kept " +
                "active: " +
                GetExceptionMessage(exception));

            _promptHasStandardModeCompleted = null;
            _completionWarningLogged = true;
        }
    }

    private void ResolveMethods()
    {
        MethodInfo targetMethod =
            FindNativeMethod(
                "GET_PLAYER_TARGET_ENTITY",
                2);

        if (targetMethod != null)
        {
            try
            {
                _getPlayerTargetEntity =
                    (PlayerTargetEntityNative)Delegate.CreateDelegate(
                        typeof(PlayerTargetEntityNative),
                        targetMethod);

                Log(
                    "PLAYER TARGET: " +
                    DescribeMethod(targetMethod));
            }
            catch (Exception exception)
            {
                Log(
                    "ERROR: Could not bind player-target delegate: " +
                    GetExceptionMessage(exception));
            }
        }
        else
        {
            Log(
                "ERROR: GET_PLAYER_TARGET_ENTITY wrapper not found.");
        }

        _doesEntityExist =
            FindNativeMethod(
                "DOES_ENTITY_EXIST",
                1);

        _isPedHuman =
            FindNativeMethod(
                "IS_PED_HUMAN",
                1);

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

        ResolveVarString();

        if (_promptHasStandardModeCompleted == null)
        {
            Log(
                "NOTE: Standard-mode completion wrapper is not " +
                "required to create the prompt. Completion detection " +
                "will be skipped if it remains unavailable.");
        }
    }

    private void ResolveVarString()
    {
        Type expectedReturnType = typeof(string);

        if (_promptSetText != null)
        {
            ParameterInfo[] textParameters =
                _promptSetText.GetParameters();

            if (textParameters.Length >= 2)
            {
                expectedReturnType =
                    textParameters[1].ParameterType;
            }
        }

        object[] sampleArguments =
        {
            PromptTextLiteralString,
            "LITERAL_STRING",
            "Recruit"
        };

        MethodInfo[] candidates =
            FindNativeMethods("VAR_STRING");

        MethodInfo bestMethod = null;
        int bestScore = int.MaxValue;

        for (int i = 0; i < candidates.Length; i++)
        {
            MethodInfo candidate = candidates[i];

            MethodInfo closedCandidate =
                CloseGenericMethod(
                    candidate,
                    expectedReturnType);

            if (closedCandidate == null)
            {
                continue;
            }

            object[] convertedArguments;

            if (!TryBuildArguments(
                closedCandidate.GetParameters(),
                sampleArguments,
                out convertedArguments))
            {
                continue;
            }

            int score = ScoreVarStringCandidate(
                closedCandidate,
                expectedReturnType);

            if (score < bestScore)
            {
                bestScore = score;
                bestMethod = closedCandidate;
            }
        }

        _varString = bestMethod;

        if (_varString != null)
        {
            Log(
                "VAR STRING: " +
                DescribeMethod(_varString));

            return;
        }

        Log(
            "ERROR: No compatible VAR_STRING wrapper was resolved. " +
            "Matching-name candidate count=" +
            candidates.Length + ".");

        for (int i = 0; i < candidates.Length; i++)
        {
            Log(
                "VAR STRING CANDIDATE: " +
                DescribeMethod(candidates[i]));
        }
    }

    private static int ScoreVarStringCandidate(
        MethodInfo method,
        Type expectedReturnType)
    {
        int score = 0;

        if (method.ReturnType != expectedReturnType)
        {
            score += 10;
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        if (parameters.Length == 3)
        {
            score -= 2;
        }

        if (parameters.Length > 0 &&
            IsParamArray(parameters[parameters.Length - 1]))
        {
            score += 1;
        }

        return score;
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
        MethodInfo[] methods =
            FindNativeMethods(methodName);

        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];

            if (method.ContainsGenericParameters)
            {
                continue;
            }

            if (method.GetParameters().Length == parameterCount)
            {
                return method;
            }
        }

        return null;
    }

    private static MethodInfo[] FindNativeMethods(
        string methodName)
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

        if (types == null)
        {
            return new MethodInfo[0];
        }

        string normalizedName =
            methodName.TrimStart('_');

        System.Collections.Generic.List<MethodInfo> matches =
            new System.Collections.Generic.List<MethodInfo>();

        for (int i = 0; i < types.Length; i++)
        {
            Type type = types[i];

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
                    BindingFlags.NonPublic |
                    BindingFlags.Static);
            }
            catch
            {
                continue;
            }

            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo method = methods[j];

                if (string.Equals(
                    method.Name.TrimStart('_'),
                    normalizedName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    matches.Add(method);
                }
            }
        }

        return matches.ToArray();
    }

    private static MethodInfo CloseGenericMethod(
        MethodInfo method,
        Type expectedReturnType)
    {
        if (method == null)
        {
            return null;
        }

        if (!method.ContainsGenericParameters)
        {
            return method;
        }

        if (!method.IsGenericMethodDefinition)
        {
            return null;
        }

        Type[] genericParameters =
            method.GetGenericArguments();

        if (genericParameters.Length == 0 ||
            expectedReturnType == null)
        {
            return null;
        }

        Type[] genericArguments =
            new Type[genericParameters.Length];

        for (int i = 0; i < genericArguments.Length; i++)
        {
            genericArguments[i] = expectedReturnType;
        }

        try
        {
            return method.MakeGenericMethod(genericArguments);
        }
        catch
        {
            return null;
        }
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

        object[] convertedArguments;

        if (!TryBuildArguments(
            method.GetParameters(),
            arguments,
            out convertedArguments))
        {
            throw new InvalidOperationException(
                "Native argument mismatch for " +
                DescribeMethod(method) +
                "; supplied=" + arguments.Length + ".");
        }

        try
        {
            return method.Invoke(
                null,
                convertedArguments);
        }
        catch (TargetInvocationException exception)
        {
            throw new InvalidOperationException(
                "Native invocation failed for " +
                DescribeMethod(method) +
                ": " +
                GetExceptionMessage(exception));
        }
    }

    private static bool TryBuildArguments(
        ParameterInfo[] parameters,
        object[] suppliedArguments,
        out object[] convertedArguments)
    {
        convertedArguments = null;

        if (parameters == null)
        {
            return false;
        }

        if (suppliedArguments == null)
        {
            suppliedArguments = new object[0];
        }

        bool hasParamArray =
            parameters.Length > 0 &&
            IsParamArray(parameters[parameters.Length - 1]);

        int fixedParameterCount =
            hasParamArray
                ? parameters.Length - 1
                : parameters.Length;

        if (suppliedArguments.Length < fixedParameterCount)
        {
            return false;
        }

        if (!hasParamArray &&
            suppliedArguments.Length > parameters.Length)
        {
            return false;
        }

        convertedArguments =
            new object[parameters.Length];

        for (int i = 0; i < fixedParameterCount; i++)
        {
            try
            {
                convertedArguments[i] = ConvertArgument(
                    suppliedArguments[i],
                    parameters[i].ParameterType);
            }
            catch
            {
                convertedArguments = null;
                return false;
            }
        }

        if (hasParamArray)
        {
            Type arrayType =
                parameters[parameters.Length - 1].ParameterType;

            Type elementType =
                arrayType.GetElementType();

            int remainingCount =
                suppliedArguments.Length - fixedParameterCount;

            Array packedArguments =
                Array.CreateInstance(
                    elementType,
                    remainingCount);

            for (int i = 0; i < remainingCount; i++)
            {
                try
                {
                    object convertedValue = ConvertArgument(
                        suppliedArguments[fixedParameterCount + i],
                        elementType);

                    packedArguments.SetValue(
                        convertedValue,
                        i);
                }
                catch
                {
                    convertedArguments = null;
                    return false;
                }
            }

            convertedArguments[parameters.Length - 1] =
                packedArguments;

            return true;
        }

        for (int i = suppliedArguments.Length;
            i < parameters.Length;
            i++)
        {
            if (!parameters[i].IsOptional)
            {
                convertedArguments = null;
                return false;
            }

            convertedArguments[i] =
                parameters[i].DefaultValue;
        }

        return true;
    }

    private static bool IsParamArray(
        ParameterInfo parameter)
    {
        return parameter != null &&
            parameter.GetCustomAttributes(
                typeof(ParamArrayAttribute),
                false).Length > 0;
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
        if (targetType == null)
        {
            throw new InvalidOperationException(
                "Native parameter type is unknown.");
        }

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
            long numericValue =
                Convert.ToInt64(value);

            return new IntPtr(numericValue);
        }

        if (targetType == typeof(UIntPtr))
        {
            ulong numericValue =
                Convert.ToUInt64(value);

            return new UIntPtr(numericValue);
        }

        if (targetType == typeof(uint))
        {
            return unchecked(
                (uint)Convert.ToInt64(value));
        }

        if (targetType == typeof(ulong))
        {
            return Convert.ToUInt64(value);
        }

        if (targetType == typeof(int))
        {
            return unchecked(
                (int)Convert.ToInt64(value));
        }

        if (targetType == typeof(long))
        {
            return Convert.ToInt64(value);
        }

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            return Convert.ChangeType(
                value,
                nullableType);
        }

        return Convert.ChangeType(
            value,
            targetType);
    }

    private void ClearPrompt()
    {
        int oldPromptHandle = _promptHandle;

        _promptHandle = -1;
        _promptGroup = -1;

        if (oldPromptHandle <= 0)
        {
            return;
        }

        if (_promptDelete == null)
        {
            Log(
                "PROMPT DELETE SKIPPED: delete wrapper unavailable; " +
                "handle=" + oldPromptHandle);

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

    private static string DescribeMethod(
        MethodInfo method)
    {
        if (method == null)
        {
            return "<null method>";
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        string[] types =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            types[i] =
                parameters[i].ParameterType.FullName;
        }

        string genericInfo = string.Empty;

        if (method.IsGenericMethod)
        {
            Type[] genericArguments =
                method.GetGenericArguments();

            string[] genericNames =
                new string[genericArguments.Length];

            for (int i = 0; i < genericArguments.Length; i++)
            {
                genericNames[i] =
                    genericArguments[i].FullName ??
                    genericArguments[i].Name;
            }

            genericInfo =
                "<" +
                string.Join(", ", genericNames) +
                ">";
        }

        string declaringType =
            method.DeclaringType == null
                ? "<unknown type>"
                : method.DeclaringType.FullName;

        string returnType =
            method.ReturnType == null
                ? "<unknown return type>"
                : method.ReturnType.FullName;

        return declaringType +
            "." +
            method.Name +
            genericInfo +
            "(" +
            string.Join(", ", types) +
            ") -> " +
            returnType;
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

        File.AppendAllText(
            _logPath,
            line);
    }
}