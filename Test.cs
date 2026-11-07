using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int RecruitInputHash = unchecked((int)0x760A9C6F);
    private const float TestPromptRadius = 3.0f;
    private const int AmbientPromptP2 = 2;
    private const int AmbientPromptTabAmount = 1;
    private const int AmbientPromptFlags = 0;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExistMethod;
    private MethodInfo _isPedHumanMethod;
    private MethodInfo _registerInteractionLockonPromptMethod;
    private MethodInfo _unregisterInteractionLockonPromptMethod;
    private MethodInfo _setAmbientGroupThisFrameMethod;
    private MethodInfo _varStringMethod;

    private object _recruitPrompt;
    private int _recruitPromptHandle = -1;
    private int _recruitPromptGroup = -1;
    private int _targetHandle;
    private int _iloTargetHandle = -1;

    private bool _iloRegistrationAttempted;
    private bool _registrationAttemptedForTarget;
    private bool _promptAttached;
    private bool _selectionHandledForTarget;
    private bool _errorReported;
    private bool _hasLoggedFocusState;
    private bool _lastLoggedFocusState;

    private int _lastLoggedTargetHandle = -1;
    private string _lastPromptState;

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        ClearLog();

        Log("=== Test.cs Initialized ===");
        Log("Log file: " + _logPath);
        Log(
            "TEST: Holster your weapon, stand close to a human NPC, " +
            "and right-mouse focus them.");
        Log(
            "Observe the separate 'Recruit test' lock-on text and " +
            "'G Recruit' action prompt.");
        Log(
            "Press G only if 'G Recruit' is visible. G is a test " +
            "placeholder; it will not recruit or change gang data.");
        Log(
            "The ambient prompt group is invoked every tick while " +
            "the focused target and attached action prompt are valid.");
        Log(
            "Native invocation, prompt properties, and visible " +
            "rendering are logged as separate evidence.");

        ResolvePlayerTargetEntityWrapper();
        ResolveTargetValidationMethods();
        ResolveInteractionLockonPromptMethods();
        ResolveAmbientPromptMethods();

        RDR2.UI.Screen.PrintSubtitle(
            "Holster weapon; focus a human NPC to test Recruit.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateTargetAndPrompts();
        }
        catch (Exception exception)
        {
            if (!_errorReported)
            {
                _errorReported = true;

                string message =
                    "Recruit prompt test error: " +
                    GetExceptionMessage(exception);

                RDR2.UI.Screen.PrintSubtitle(message);
                Log("ERROR: " + message);
                Log("StackTrace: " + exception.StackTrace);
            }
        }
    }

    private void UpdateTargetAndPrompts()
    {
        bool isFreeFocusing = Game.Player.IsFreeFocusing;
        LogFocusStateWhenChanged(isFreeFocusing);

        int observedTargetHandle = 0;

        if (isFreeFocusing)
        {
            int nativeTargetHandle;

            if (TryGetPlayerTargetEntity(out nativeTargetHandle) &&
                nativeTargetHandle > 0 &&
                IsHumanNpc(nativeTargetHandle))
            {
                observedTargetHandle = nativeTargetHandle;
            }
        }

        LogTargetWhenChanged(observedTargetHandle);

        if (observedTargetHandle != _targetHandle)
        {
            ClearCurrentTargetPrompts();

            _targetHandle = observedTargetHandle;
            _iloRegistrationAttempted = false;
            _registrationAttemptedForTarget = false;
            _selectionHandledForTarget = false;
            _lastPromptState = null;

            if (_targetHandle > 0)
            {
                Log(
                    "INTERACTION-LOCK-ON TARGET: human NPC handle " +
                    _targetHandle);
            }
            else
            {
                Log("INTERACTION-LOCK-ON TARGET: none");
            }
        }

        if (_targetHandle <= 0)
        {
            return;
        }

        if (!_iloRegistrationAttempted)
        {
            _iloRegistrationAttempted = true;
            RegisterInteractionLockonPrompt(_targetHandle);
        }

        if (!_registrationAttemptedForTarget &&
            !_selectionHandledForTarget)
        {
            _registrationAttemptedForTarget = true;
            CreateAndAttachRecruitPrompt(_targetHandle);
        }

        if (_promptAttached &&
            _recruitPrompt != null &&
            _recruitPromptGroup >= 0)
        {
            ActivateAmbientPromptGroupThisFrame(
                _targetHandle,
                _recruitPromptGroup);
        }

        LogPromptStateWhenChanged();
        CheckPromptSelection();
    }

    private void RegisterInteractionLockonPrompt(int targetHandle)
    {
        Log(
            "REGISTERING TEST ILO PROMPT for target " +
            targetHandle);

        if (_registerInteractionLockonPromptMethod == null)
        {
            Log(
                "ERROR: REGISTER_INTERACTION_LOCKON_PROMPT wrapper " +
                "was not found.");
            return;
        }

        try
        {
            ParameterInfo[] parameters =
                _registerInteractionLockonPromptMethod.GetParameters();

            if (parameters.Length != 10)
            {
                throw new InvalidOperationException(
                    "Expected 10 parameters; found " +
                    parameters.Length +
                    ".");
            }

            object[] arguments =
            {
                targetHandle,
                "Recruit test",
                TestPromptRadius,
                TestPromptRadius,
                0,
                0.0f,
                0.0f,
                0,
                true,
                -1
            };

            ConvertArgumentsInPlace(arguments, parameters);

            object result =
                _registerInteractionLockonPromptMethod.Invoke(
                    null,
                    arguments);

            _iloTargetHandle = targetHandle;

            if (_registerInteractionLockonPromptMethod.ReturnType ==
                typeof(bool))
            {
                bool succeeded =
                    result != null && Convert.ToBoolean(result);

                Log("ILO NATIVE RESULT: " + succeeded);
            }
            else
            {
                Log(
                    "ILO NATIVE CALL RETURNED; wrapper has no bool result.");
            }

            Log(
                "ILO TEST ARGUMENTS: radius=" +
                TestPromptRadius +
                "; p3=" +
                TestPromptRadius +
                "; flag=0; p5=0; p6=0; prompt=0; p8=true; p9=-1");
        }
        catch (Exception exception)
        {
            _iloTargetHandle = -1;

            Log(
                "ERROR: ILO registration failed for target " +
                targetHandle +
                ": " +
                GetExceptionMessage(exception));
        }
    }

    private void CreateAndAttachRecruitPrompt(int targetHandle)
    {
        Log(
            "REGISTERING TEMPORARY G RECRUIT ACTION for target " +
            targetHandle);

        try
        {
            _recruitPrompt = CreatePromptObject();

            if (_recruitPrompt == null)
            {
                throw new InvalidOperationException(
                    "Prompt.CreatePrompt returned null.");
            }

            _recruitPromptHandle =
                TryGetNativePromptHandle(_recruitPrompt);

            if (_recruitPromptHandle < 0)
            {
                throw new InvalidOperationException(
                    "Could not resolve the native prompt handle.");
            }

            _recruitPromptGroup =
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle);

            if (_recruitPromptGroup < 0)
            {
                throw new InvalidOperationException(
                    "The target entity returned an invalid prompt group.");
            }

            SetPromptProperty(
                _recruitPrompt,
                "Text",
                "Recruit");

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _recruitPromptHandle,
                _recruitPromptGroup,
                0);

            _promptAttached = true;

            SetPromptProperty(
                _recruitPrompt,
                "Enabled",
                true);

            SetPromptProperty(
                _recruitPrompt,
                "Visible",
                true);

            Log(
                "CUSTOM ACTION ATTACHED: prompt handle " +
                _recruitPromptHandle +
                "; target " +
                targetHandle +
                "; target prompt group " +
                _recruitPromptGroup);

            Log(
                "AMBIENT GROUP TEST: will invoke the attached target " +
                "group every tick; radius=" +
                TestPromptRadius +
                "; p2=" +
                AmbientPromptP2 +
                "; tabAmount=" +
                AmbientPromptTabAmount +
                "; flags=" +
                AmbientPromptFlags +
                ".");

            Log(
                "IN-GAME CHECK REQUIRED: separately confirm whether " +
                "'Recruit test' and 'G Recruit' are visible. Prompt " +
                "properties do not prove rendering.");
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: Recruit action setup failed for target " +
                targetHandle +
                ": " +
                GetExceptionMessage(exception));

            RDR2.UI.Screen.PrintSubtitle(
                "Recruit test setup failed. Check Test_Log.txt.");

            ClearRecruitActionPrompt();
            UnregisterInteractionLockonPrompt();
        }
    }

    private object CreatePromptObject()
    {
        Assembly assembly = typeof(Game).Assembly;

        Type promptType =
            assembly.GetType("RDR2.UI.Prompt", false);

        if (promptType == null)
        {
            throw new InvalidOperationException(
                "RDR2.UI.Prompt type was not found in the loaded API.");
        }

        MethodInfo createPromptMethod = null;

        MethodInfo[] methods =
            promptType.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            if (!string.Equals(
                method.Name,
                "CreatePrompt",
                StringComparison.Ordinal))
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length == 8 &&
                parameters[2].ParameterType == typeof(string))
            {
                createPromptMethod = method;
                break;
            }
        }

        if (createPromptMethod == null)
        {
            throw new InvalidOperationException(
                "The eight-parameter Prompt.CreatePrompt method " +
                "was not found.");
        }

        ParameterInfo[] promptParameters =
            createPromptMethod.GetParameters();

        object[] arguments =
            BuildPromptCreateArguments(promptParameters);

        return createPromptMethod.Invoke(null, arguments);
    }

    private static object[] BuildPromptCreateArguments(
        ParameterInfo[] parameters)
    {
        object[] arguments = new object[parameters.Length];

        arguments[0] =
            ConvertArgument(
                RecruitInputHash,
                parameters[0].ParameterType);

        if (!parameters[1].ParameterType.IsEnum)
        {
            throw new InvalidOperationException(
                "The second CreatePrompt parameter is not an enum; " +
                "expected the Press use-context value.");
        }

        arguments[1] =
            Enum.Parse(
                parameters[1].ParameterType,
                "Press",
                true);

        arguments[2] = "Recruit";

        for (int i = 3; i <= 6; i++)
        {
            arguments[i] =
                ConvertArgument(
                    0,
                    parameters[i].ParameterType);
        }

        Type timingType = parameters[7].ParameterType;

        arguments[7] =
            timingType.IsValueType
                ? Activator.CreateInstance(timingType)
                : null;

        return arguments;
    }

    private void ActivateAmbientPromptGroupThisFrame(
        int targetHandle,
        int promptGroup)
    {
        if (_setAmbientGroupThisFrameMethod == null)
        {
            return;
        }

        if (_varStringMethod == null)
        {
            LogOnce(
                "AMBIENT ACTIVATION SKIPPED: VAR_STRING wrapper is " +
                "unavailable.");
            return;
        }

        try
        {
            ParameterInfo[] parameters =
                _setAmbientGroupThisFrameMethod.GetParameters();

            if (parameters.Length != 7)
            {
                throw new InvalidOperationException(
                    "Expected seven ambient-group parameters; found " +
                    parameters.Length +
                    ".");
            }

            string groupLabel = CreateVarString("Recruit");

            object[] arguments =
            {
                targetHandle,
                TestPromptRadius,
                AmbientPromptP2,
                AmbientPromptTabAmount,
                promptGroup,
                groupLabel,
                AmbientPromptFlags
            };

            ConvertArgumentsInPlace(arguments, parameters);

            _setAmbientGroupThisFrameMethod.Invoke(
                null,
                arguments);

            LogFirstSuccessfulAmbientActivation(
                targetHandle,
                promptGroup);
        }
        catch (Exception exception)
        {
            LogOnce(
                "ERROR: Ambient prompt-group activation failed: " +
                GetExceptionMessage(exception));
        }
    }

    private string CreateVarString(string text)
    {
        if (_varStringMethod == null)
        {
            throw new InvalidOperationException(
                "The VAR_STRING wrapper was not resolved.");
        }

        ParameterInfo[] parameters =
            _varStringMethod.GetParameters();

        if (parameters.Length != 2 ||
            !parameters[1].ParameterType.IsArray)
        {
            throw new InvalidOperationException(
                "Expected VAR_STRING(int, InputArgument[]); found " +
                DescribeMethod(_varStringMethod) +
                ".");
        }

        Type inputArgumentType =
            parameters[1].ParameterType.GetElementType();

        Array inputArguments =
            Array.CreateInstance(inputArgumentType, 2);

        inputArguments.SetValue(
            ConvertToInputArgument(
                "LITERAL_STRING",
                inputArgumentType),
            0);

        inputArguments.SetValue(
            ConvertToInputArgument(
                text,
                inputArgumentType),
            1);

        object[] invokeArguments =
        {
            ConvertArgument(10, parameters[0].ParameterType),
            inputArguments
        };

        object result =
            _varStringMethod.Invoke(
                null,
                invokeArguments);

        if (!(result is string))
        {
            throw new InvalidOperationException(
                "VAR_STRING did not return a string.");
        }

        return (string)result;
    }

    private static object ConvertToInputArgument(
        object value,
        Type inputArgumentType)
    {
        if (inputArgumentType.IsInstanceOfType(value))
        {
            return value;
        }

        MethodInfo[] methods =
            inputArgumentType.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            if (method.Name != "op_Implicit" &&
                method.Name != "op_Explicit")
            {
                continue;
            }

            if (method.ReturnType != inputArgumentType)
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length == 1 &&
                parameters[0].ParameterType.IsInstanceOfType(value))
            {
                return method.Invoke(
                    null,
                    new object[] { value });
            }
        }

        throw new InvalidOperationException(
            "Could not convert " +
            value.GetType().FullName +
            " to " +
            inputArgumentType.FullName +
            " using an InputArgument conversion operator.");
    }

    private void CheckPromptSelection()
    {
        if (_recruitPrompt == null ||
            !_promptAttached ||
            _selectionHandledForTarget)
        {
            return;
        }

        if (!GetPromptBooleanProperty(
            _recruitPrompt,
            "HasCompleted"))
        {
            return;
        }

        _selectionHandledForTarget = true;

        Log(
            "G PROMPT COMPLETED — TEST ONLY; target handle " +
            _targetHandle +
            "; no recruitment or gang-data changes made.");

        RDR2.UI.Screen.PrintSubtitle(
            "G prompt completed; no recruitment performed.");

        ClearCurrentTargetPrompts();
    }

    private void LogPromptStateWhenChanged()
    {
        if (_recruitPrompt == null)
        {
            return;
        }

        bool enabled =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "Enabled");

        bool visible =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "Visible");

        bool completed =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "HasCompleted");

        string state =
            "Enabled=" + enabled +
            "; Visible=" + visible +
            "; HasCompleted=" + completed;

        if (!string.Equals(
            state,
            _lastPromptState,
            StringComparison.Ordinal))
        {
            Log(
                "PROMPT STATE: handle=" +
                _recruitPromptHandle +
                "; target=" +
                _targetHandle +
                "; group=" +
                _recruitPromptGroup +
                "; " +
                state);

            _lastPromptState = state;
        }
    }

    private void ResolvePlayerTargetEntityWrapper()
    {
        MethodInfo method =
            FindNativeMethod(
                "GET_PLAYER_TARGET_ENTITY",
                2);

        if (method == null)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY wrapper " +
                "was not found.");
            return;
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        bool signatureMatches =
            method.ReturnType == typeof(bool) &&
            parameters[0].ParameterType == typeof(int) &&
            parameters[1].ParameterType.IsPointer &&
            parameters[1].ParameterType.GetElementType() ==
                typeof(int);

        if (!signatureMatches)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY signature " +
                "did not match bool(int, int*). Found: " +
                DescribeMethod(method));
            return;
        }

        try
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)
                Delegate.CreateDelegate(
                    typeof(PlayerTargetEntityNative),
                    method);

            Log(
                "PLAYER TARGET WRAPPER RESOLVED: " +
                DescribeMethod(method));
        }
        catch (Exception exception)
        {
            _getPlayerTargetEntity = null;

            Log(
                "ERROR: Could not create player-target delegate: " +
                GetExceptionMessage(exception));
        }
    }

    private void ResolveTargetValidationMethods()
    {
        _doesEntityExistMethod =
            FindNativeMethod(
                "DOES_ENTITY_EXIST",
                1);

        _isPedHumanMethod =
            FindNativeMethod(
                "IS_PED_HUMAN",
                1);

        if (_doesEntityExistMethod == null)
        {
            Log(
                "ERROR: ENTITY.DOES_ENTITY_EXIST wrapper was not found.");
        }

        if (_isPedHumanMethod == null)
        {
            Log("ERROR: PED.IS_PED_HUMAN wrapper was not found.");
        }
    }

    private void ResolveInteractionLockonPromptMethods()
    {
        _registerInteractionLockonPromptMethod =
            FindNativeMethod(
                "REGISTER_INTERACTION_LOCKON_PROMPT",
                10);

        _unregisterInteractionLockonPromptMethod =
            FindNativeMethod(
                "UNREGISTER_INTERACTION_LOCKON_PROMPT",
                1);

        LogResolvedOrMissing(
            "ILO REGISTER WRAPPER",
            _registerInteractionLockonPromptMethod);

        LogResolvedOrMissing(
            "ILO UNREGISTER WRAPPER",
            _unregisterInteractionLockonPromptMethod);
    }

    private void ResolveAmbientPromptMethods()
    {
        _setAmbientGroupThisFrameMethod =
            FindNativeMethod(
                "_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                7);

        if (_setAmbientGroupThisFrameMethod == null)
        {
            _setAmbientGroupThisFrameMethod =
                FindNativeMethod(
                    "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                    7);
        }

        _varStringMethod =
            FindNativeMethod(
                "VAR_STRING",
                2);

        LogResolvedOrMissing(
            "AMBIENT GROUP WRAPPER",
            _setAmbientGroupThisFrameMethod);

        LogResolvedOrMissing(
            "VAR_STRING WRAPPER",
            _varStringMethod);

        if (_varStringMethod != null)
        {
            ParameterInfo[] parameters =
                _varStringMethod.GetParameters();

            if (parameters.Length != 2 ||
                !parameters[1].ParameterType.IsArray)
            {
                Log(
                    "ERROR: VAR_STRING wrapper does not match " +
                    "int, InputArgument[]. Ambient activation will " +
                    "not be attempted.");

                _varStringMethod = null;
            }
        }
    }

    private unsafe bool TryGetPlayerTargetEntity(
        out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        int localTargetHandle = 0;

        bool found =
            _getPlayerTargetEntity(
                Game.Player.Handle,
                &localTargetHandle);

        targetHandle = localTargetHandle;

        return found && targetHandle > 0;
    }

    private bool IsHumanNpc(int entityHandle)
    {
        if (entityHandle <= 0 ||
            _doesEntityExistMethod == null ||
            _isPedHumanMethod == null)
        {
            return false;
        }

        try
        {
            bool exists =
                Convert.ToBoolean(
                    InvokeNative(
                        _doesEntityExistMethod,
                        entityHandle));

            if (!exists)
            {
                return false;
            }

            if (entityHandle ==
                Game.Player.Character.Handle)
            {
                return false;
            }

            return Convert.ToBoolean(
                InvokeNative(
                    _isPedHumanMethod,
                    entityHandle));
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: Human-target validation failed for handle " +
                entityHandle +
                ": " +
                GetExceptionMessage(exception));

            return false;
        }
    }

    private void ClearCurrentTargetPrompts()
    {
        ClearRecruitActionPrompt();
        UnregisterInteractionLockonPrompt();

        _iloRegistrationAttempted = false;
        _registrationAttemptedForTarget = false;
        _selectionHandledForTarget = false;
        _lastPromptState = null;
    }

    private void ClearRecruitActionPrompt()
    {
        if (_recruitPrompt != null)
        {
            TryDisposePrompt(_recruitPrompt);
        }

        _recruitPrompt = null;
        _recruitPromptHandle = -1;
        _recruitPromptGroup = -1;
        _promptAttached = false;
    }

    private void TryDisposePrompt(object prompt)
    {
        try
        {
            if (prompt is IDisposable)
            {
                ((IDisposable)prompt).Dispose();
                return;
            }

            MethodInfo disposeMethod =
                prompt.GetType().GetMethod(
                    "Dispose",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance,
                    null,
                    Type.EmptyTypes,
                    null);

            if (disposeMethod != null)
            {
                disposeMethod.Invoke(prompt, null);
            }
        }
        catch (Exception exception)
        {
            Log(
                "WARNING: Prompt cleanup failed: " +
                GetExceptionMessage(exception));
        }
    }

    private void UnregisterInteractionLockonPrompt()
    {
        if (_iloTargetHandle <= 0 ||
            _unregisterInteractionLockonPromptMethod == null)
        {
            _iloTargetHandle = -1;
            return;
        }

        try
        {
            object result =
                InvokeNative(
                    _unregisterInteractionLockonPromptMethod,
                    _iloTargetHandle);

            Log(
                "ILO PROMPT UNREGISTERED for target " +
                _iloTargetHandle +
                "; result=" +
                (result == null ? "null" : result.ToString()));
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: ILO unregistration failed for target " +
                _iloTargetHandle +
                ": " +
                GetExceptionMessage(exception));
        }

        _iloTargetHandle = -1;
    }

    private void LogFocusStateWhenChanged(bool isFreeFocusing)
    {
        if (!_hasLoggedFocusState ||
            _lastLoggedFocusState != isFreeFocusing)
        {
            Log(
                "STATE: IsFreeFocusing=" +
                isFreeFocusing);

            _lastLoggedFocusState = isFreeFocusing;
            _hasLoggedFocusState = true;
        }
    }

    private void LogTargetWhenChanged(int targetHandle)
    {
        if (_lastLoggedTargetHandle != targetHandle)
        {
            Log(
                "STATE: eligible unarmed interaction-lock-on " +
                "target=" +
                (targetHandle > 0
                    ? targetHandle.ToString()
                    : "none"));

            _lastLoggedTargetHandle = targetHandle;
        }
    }

    private void LogFirstSuccessfulAmbientActivation(
        int targetHandle,
        int promptGroup)
    {
        if (_ambientActivationLogged)
        {
            return;
        }

        _ambientActivationLogged = true;

        Log(
            "AMBIENT GROUP ACTIVATION INVOKED: method=" +
            DescribeMethod(_setAmbientGroupThisFrameMethod) +
            "; target=" +
            targetHandle +
            "; group=" +
            promptGroup +
            "; radius=" +
            TestPromptRadius +
            "; p2=" +
            AmbientPromptP2 +
            "; tabAmount=" +
            AmbientPromptTabAmount +
            "; flags=" +
            AmbientPromptFlags +
            ".");
    }

    private bool _ambientActivationLogged;

    private void LogOnce(string message)
    {
        if (_ambientActivationLogged)
        {
            return;
        }

        _ambientActivationLogged = true;
        Log(message);
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
    {
        Assembly assembly = typeof(Game).Assembly;
        Type[] types;

        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            types = exception.Types;
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
                methods =
                    type.GetMethods(
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
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
                        StringComparison.Ordinal) &&
                    method.GetParameters().Length ==
                        parameterCount)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static object InvokeNative(
        MethodInfo method,
        params object[] values)
    {
        ParameterInfo[] parameters =
            method.GetParameters();

        if (parameters.Length != values.Length)
        {
            throw new InvalidOperationException(
                "Native argument count mismatch for " +
                DescribeMethod(method) +
                ".");
        }

        object[] converted =
            new object[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            converted[i] =
                ConvertArgument(
                    values[i],
                    parameters[i].ParameterType);
        }

        return method.Invoke(null, converted);
    }

    private static void ConvertArgumentsInPlace(
        object[] arguments,
        ParameterInfo[] parameters)
    {
        if (arguments.Length != parameters.Length)
        {
            throw new InvalidOperationException(
                "Argument count does not match method signature.");
        }

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] =
                ConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType);
        }
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

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            targetType = nullableType;
        }

        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(
                targetType,
                value);
        }

        return Convert.ChangeType(
            value,
            targetType);
    }

    private static object GetPromptMember(
        object prompt,
        string memberName)
    {
        Type type = prompt.GetType();

        PropertyInfo property =
            type.GetProperty(
                memberName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (property != null &&
            property.CanRead)
        {
            return property.GetValue(prompt, null);
        }

        FieldInfo field =
            type.GetField(
                memberName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        return field == null
            ? null
            : field.GetValue(prompt);
    }

    private static void SetPromptProperty(
        object prompt,
        string propertyName,
        object value)
    {
        Type type = prompt.GetType();

        PropertyInfo property =
            type.GetProperty(
                propertyName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (property != null &&
            property.CanWrite)
        {
            property.SetValue(
                prompt,
                ConvertArgument(
                    value,
                    property.PropertyType),
                null);
            return;
        }

        FieldInfo field =
            type.GetField(
                propertyName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (field != null)
        {
            field.SetValue(
                prompt,
                ConvertArgument(
                    value,
                    field.FieldType));
            return;
        }

        throw new InvalidOperationException(
            "Prompt member '" +
            propertyName +
            "' was not found or is not writable.");
    }

    private static bool GetPromptBooleanProperty(
        object prompt,
        string propertyName)
    {
        object value =
            GetPromptMember(
                prompt,
                propertyName);

        return value != null &&
            Convert.ToBoolean(value);
    }

    private static int TryGetNativePromptHandle(
        object prompt)
    {
        string[] memberNames =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "Id"
        };

        foreach (string memberName in memberNames)
        {
            object value =
                GetPromptMember(
                    prompt,
                    memberName);

            if (value != null)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    // Try the next candidate member.
                }
            }
        }

        Type type = prompt.GetType();

        MethodInfo[] methods =
            type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        foreach (MethodInfo method in methods)
        {
            if (method.GetParameters().Length != 0 ||
                method.ReturnType != typeof(int))
            {
                continue;
            }

            if (method.Name.IndexOf(
                    "Handle",
                    StringComparison.OrdinalIgnoreCase) >= 0)
            {
                try
                {
                    return Convert.ToInt32(
                        method.Invoke(prompt, null));
                }
                catch
                {
                    // Continue looking for another handle accessor.
                }
            }
        }

        return -1;
    }

    private static string DescribeMethod(MethodInfo method)
    {
        if (method == null)
        {
            return "null";
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        string[] parameterNames =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterNames[i] =
                parameters[i].ParameterType.FullName;
        }

        return method.DeclaringType.FullName +
            "." +
            method.Name +
            "(" +
            string.Join(", ", parameterNames) +
            ") -> " +
            method.ReturnType.FullName;
    }

    private static string GetExceptionMessage(
        Exception exception)
    {
        if (exception is TargetInvocationException &&
            exception.InnerException != null)
        {
            return exception.InnerException.Message;
        }

        return exception.Message;
    }

    private void LogResolvedOrMissing(
        string label,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("WARNING: " + label + " was not found.");
            return;
        }

        Log(
            label +
            " RESOLVED: " +
            DescribeMethod(method));
    }

    private void ClearLog()
    {
        try
        {
            File.WriteAllText(
                _logPath,
                string.Empty);
        }
        catch
        {
            // Logging errors should not prevent the test script loading.
        }
    }

    private void Log(string message)
    {
        string line =
            "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " +
            message;

        try
        {
            File.AppendAllText(
                _logPath,
                line +
                Environment.NewLine);
        }
        catch
        {
            // Keep test behavior running if the log file is unavailable.
        }
    }
}