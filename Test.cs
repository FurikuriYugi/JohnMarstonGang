using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int RecruitInputHash = unchecked((int)0x760A9C6F);
    private const float TestPromptRadius = 3.0f;
    private const int InteractionLockonPromptHash =
        unchecked((int)0x870708A6);

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExistMethod;
    private MethodInfo _isPedHumanMethod;
    private MethodInfo _registerInteractionLockonPromptMethod;
    private MethodInfo _unregisterInteractionLockonPromptMethod;

    private object _recruitPrompt;
    private int _recruitPromptHandle = -1;
    private int _recruitPromptGroup = -1;
    private int _targetHandle;

    private bool _iloRegistrationAttempted;
    private bool _iloRegistrationActive;
    private int _iloTargetHandle = -1;

    private bool _promptAttached;
    private bool _registrationAttemptedForTarget;
    private bool _selectionHandledForTarget;
    private bool _errorReported;

    private int _lastLoggedTargetHandle = -1;
    private bool _lastLoggedFocusState;
    private bool _hasLoggedFocusState;
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
            "Press G only if the Recruit action prompt is visible. " +
            "This test must not recruit the target or change gang data.");
        Log(
            "The interaction-lock-on prompt arguments are test values. " +
            "A successful native call does not prove that the game " +
            "rendered either prompt.");

        ResolvePlayerTargetEntityWrapper();
        ResolveTargetValidationMethods();
        ResolveInteractionLockonPromptMethods();

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

                string errorMessage =
                    "Recruit prompt test error: " +
                    GetExceptionMessage(exception);

                RDR2.UI.Screen.PrintSubtitle(errorMessage);
                Log("ERROR: " + errorMessage);
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
                "was not found. The separate action-prompt test will " +
                "still run.");
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
                    parameters.Length + ".");
            }

            // These values are test probes, not production settings.
            // The meanings of several native parameters are unknown.
            object[] arguments = new object[]
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

            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = ConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType);
            }

            object result =
                _registerInteractionLockonPromptMethod.Invoke(
                    null,
                    arguments);

            _iloTargetHandle = targetHandle;

            if (_registerInteractionLockonPromptMethod.ReturnType ==
                typeof(bool))
            {
                _iloRegistrationActive =
                    result != null &&
                    Convert.ToBoolean(result);

                Log(
                    "ILO NATIVE RESULT: " +
                    _iloRegistrationActive);
            }
            else
            {
                // A void wrapper provides no success result. Record only
                // that the wrapper invocation returned without throwing.
                _iloRegistrationActive = true;
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
            _iloRegistrationActive = false;
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

        MethodInfo[] methods = promptType.GetMethods(
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

        arguments[0] = ConvertArgument(
            RecruitInputHash,
            parameters[0].ParameterType);

        if (parameters[1].ParameterType.IsEnum)
        {
            arguments[1] = Enum.Parse(
                parameters[1].ParameterType,
                "Press",
                true);
        }
        else
        {
            throw new InvalidOperationException(
                "The second CreatePrompt parameter is not an enum; " +
                "expected the Press use-context value.");
        }

        arguments[2] = "Recruit";

        for (int i = 3; i <= 6; i++)
        {
            arguments[i] = ConvertArgument(
                0,
                parameters[i].ParameterType);
        }

        Type timingType = parameters[7].ParameterType;

        arguments[7] = timingType.IsValueType
            ? Activator.CreateInstance(timingType)
            : null;

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

        const string message =
            "RECRUIT PLACEHOLDER SELECTED";

        Log(
            message +
            "; target handle " +
            _targetHandle +
            "; no recruitment or gang-data changes made.");

        RDR2.UI.Screen.PrintSubtitle(message);

        ClearCurrentTargetPrompts();
    }

    private void LogPromptStateWhenChanged()
    {
        if (_recruitPrompt == null)
        {
            return;
        }

        bool enabled = TryGetPromptBooleanProperty(
            _recruitPrompt,
            "Enabled");

        bool visible = TryGetPromptBooleanProperty(
            _recruitPrompt,
            "Visible");

        bool completed = TryGetPromptBooleanProperty(
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

    private static void SetPromptProperty(
        object prompt,
        string propertyName,
        object value)
    {
        if (prompt == null)
        {
            throw new ArgumentNullException("prompt");
        }

        const BindingFlags flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        PropertyInfo property =
            prompt.GetType().GetProperty(propertyName, flags);

        if (property == null)
        {
            throw new InvalidOperationException(
                "Prompt property not found: " + propertyName);
        }

        MethodInfo setter = property.GetSetMethod(true);

        if (setter == null)
        {
            throw new InvalidOperationException(
                "Prompt property has no setter: " + propertyName);
        }

        setter.Invoke(prompt, new object[] { value });
    }

    private static bool GetPromptBooleanProperty(
        object prompt,
        string propertyName)
    {
        if (prompt == null)
        {
            return false;
        }

        const BindingFlags flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        PropertyInfo property =
            prompt.GetType().GetProperty(propertyName, flags);

        if (property == null)
        {
            throw new InvalidOperationException(
                "Prompt property not found: " + propertyName);
        }

        object value = property.GetValue(prompt, null);

        return value != null &&
            Convert.ToBoolean(value);
    }

    private static bool TryGetPromptBooleanProperty(
        object prompt,
        string propertyName)
    {
        try
        {
            return GetPromptBooleanProperty(
                prompt,
                propertyName);
        }
        catch
        {
            return false;
        }
    }

    private void ResolvePlayerTargetEntityWrapper()
    {
        MethodInfo method =
            FindNativeMethod("GET_PLAYER_TARGET_ENTITY");

        if (method == null)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY wrapper " +
                "was not found.");
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();

        bool signatureMatches =
            method.ReturnType == typeof(bool) &&
            parameters.Length == 2 &&
            parameters[0].ParameterType == typeof(int) &&
            parameters[1].ParameterType.IsPointer &&
            parameters[1].ParameterType.GetElementType() ==
                typeof(int);

        if (!signatureMatches)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY wrapper " +
                "signature did not match bool(int, int*). Found: " +
                DescribeMethod(method));
            return;
        }

        try
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)Delegate.CreateDelegate(
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
        _doesEntityExistMethod = FindNativeMethod(
            "DOES_ENTITY_EXIST",
            typeof(bool),
            typeof(int));

        _isPedHumanMethod = FindNativeMethod(
            "IS_PED_HUMAN",
            typeof(bool),
            typeof(int));

        if (_doesEntityExistMethod == null)
        {
            Log(
                "ERROR: ENTITY.DOES_ENTITY_EXIST(bool, int) wrapper " +
                "was not found.");
        }

        if (_isPedHumanMethod == null)
        {
            Log(
                "ERROR: PED.IS_PED_HUMAN(bool, int) wrapper " +
                "was not found.");
        }
    }

    private void ResolveInteractionLockonPromptMethods()
    {
        _registerInteractionLockonPromptMethod =
            FindNativeMethod(
                "REGISTER_INTERACTION_LOCKON_PROMPT");

        _unregisterInteractionLockonPromptMethod =
            FindNativeMethod(
                "UNREGISTER_INTERACTION_LOCKON_PROMPT");

        if (_registerInteractionLockonPromptMethod == null)
        {
            Log(
                "ERROR: MISC.REGISTER_INTERACTION_LOCKON_PROMPT " +
                "wrapper was not found.");
        }
        else
        {
            Log(
                "ILO REGISTER WRAPPER RESOLVED: " +
                DescribeMethod(
                    _registerInteractionLockonPromptMethod));
        }

        if (_unregisterInteractionLockonPromptMethod == null)
        {
            Log(
                "WARNING: MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT " +
                "wrapper was not found. The test will log this if " +
                "cleanup is needed.");
        }
        else
        {
            Log(
                "ILO UNREGISTER WRAPPER RESOLVED: " +
                DescribeMethod(
                    _unregisterInteractionLockonPromptMethod));
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

        bool found = _getPlayerTargetEntity(
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
            bool exists = Convert.ToBoolean(
                _doesEntityExistMethod.Invoke(
                    null,
                    new object[] { entityHandle }));

            if (!exists)
            {
                return false;
            }

            int playerHandle =
                Game.Player.Character.Handle;

            if (entityHandle == playerHandle)
            {
                return false;
            }

            return Convert.ToBoolean(
                _isPedHumanMethod.Invoke(
                    null,
                    new object[] { entityHandle }));
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

    private static MethodInfo FindNativeMethod(
        string methodName)
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

    private static MethodInfo FindNativeMethod(
        string methodName,
        Type returnType,
        Type parameterType)
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
                if (!string.Equals(
                    method.Name,
                    methodName,
                    StringComparison.Ordinal) ||
                    method.ReturnType != returnType)
                {
                    continue;
                }

                ParameterInfo[] parameters =
                    method.GetParameters();

                if (parameters.Length == 1 &&
                    parameters[0].ParameterType == parameterType)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static string DescribeMethod(MethodInfo method)
    {
        ParameterInfo[] parameters =
            method.GetParameters();

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

    private static int TryGetNativePromptHandle(object prompt)
    {
        if (prompt == null)
        {
            return -1;
        }

        const BindingFlags flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        string[] likelyNames =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "_handle",
            "m_handle",
            "_promptHandle",
            "m_promptHandle"
        };

        Type promptType = prompt.GetType();

        foreach (string name in likelyNames)
        {
            PropertyInfo property =
                promptType.GetProperty(name, flags);

            if (property != null &&
                property.GetIndexParameters().Length == 0)
            {
                try
                {
                    int value = Convert.ToInt32(
                        property.GetValue(prompt, null));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Try the next likely handle member.
                }
            }

            FieldInfo field =
                promptType.GetField(name, flags);

            if (field != null)
            {
                try
                {
                    int value = Convert.ToInt32(
                        field.GetValue(prompt));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Try the next likely handle member.
                }
            }
        }

        foreach (FieldInfo field in promptType.GetFields(flags))
        {
            if (field.FieldType != typeof(int) &&
                field.FieldType != typeof(uint))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(
                    field.GetValue(prompt));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible fields.
            }
        }

        foreach (PropertyInfo property in
            promptType.GetProperties(flags))
        {
            if (property.GetIndexParameters().Length != 0 ||
                (property.PropertyType != typeof(int) &&
                 property.PropertyType != typeof(uint)))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(
                    property.GetValue(prompt, null));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible properties.
            }
        }

        return -1;
    }

    private void ClearCurrentTargetPrompts()
    {
        ClearRecruitActionPrompt();
        UnregisterInteractionLockonPrompt();
    }

    private void ClearRecruitActionPrompt()
    {
        if (_recruitPrompt != null)
        {
            try
            {
                SetPromptProperty(
                    _recruitPrompt,
                    "Enabled",
                    false);
            }
            catch
            {
                // Continue cleanup.
            }

            try
            {
                SetPromptProperty(
                    _recruitPrompt,
                    "Visible",
                    false);
            }
            catch
            {
                // Continue cleanup.
            }

            if (_promptAttached &&
                _recruitPromptHandle >= 0 &&
                _recruitPromptGroup >= 0)
            {
                try
                {
                    RDR2.Native.HUD._UI_PROMPT_REMOVE_GROUP(
                        _recruitPromptHandle,
                        (ulong)_recruitPromptGroup);
                }
                catch (Exception exception)
                {
                    Log(
                        "WARNING: Could not remove the temporary " +
                        "Recruit prompt from its target group: " +
                        GetExceptionMessage(exception));
                }
            }
        }

        _recruitPrompt = null;
        _recruitPromptHandle = -1;
        _recruitPromptGroup = -1;
        _promptAttached = false;
        _lastPromptState = null;
    }

    private void UnregisterInteractionLockonPrompt()
    {
        if (!_iloRegistrationActive ||
            _iloTargetHandle <= 0)
        {
            _iloRegistrationActive = false;
            _iloTargetHandle = -1;
            return;
        }

        if (_unregisterInteractionLockonPromptMethod == null)
        {
            Log(
                "WARNING: ILO prompt was registered for target " +
                _iloTargetHandle +
                " but the unregister wrapper is unavailable.");
            _iloRegistrationActive = false;
            _iloTargetHandle = -1;
            return;
        }

        try
        {
            ParameterInfo[] parameters =
                _unregisterInteractionLockonPromptMethod
                    .GetParameters();

            if (parameters.Length != 1)
            {
                throw new InvalidOperationException(
                    "Expected one unregister parameter; found " +
                    parameters.Length + ".");
            }

            object entityArgument = ConvertArgument(
                _iloTargetHandle,
                parameters[0].ParameterType);

            _unregisterInteractionLockonPromptMethod.Invoke(
                null,
                new object[] { entityArgument });

            Log(
                "ILO PROMPT UNREGISTERED for target " +
                _iloTargetHandle);
        }
        catch (Exception exception)
        {
            Log(
                "WARNING: Could not unregister ILO prompt for target " +
                _iloTargetHandle +
                ": " +
                GetExceptionMessage(exception));
        }

        _iloRegistrationActive = false;
        _iloTargetHandle = -1;
    }

    private void LogFocusStateWhenChanged(bool isFreeFocusing)
    {
        if (!_hasLoggedFocusState ||
            isFreeFocusing != _lastLoggedFocusState)
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
        if (targetHandle != _lastLoggedTargetHandle)
        {
            Log(
                "STATE: eligible unarmed interaction-lock-on target=" +
                FormatHandle(targetHandle));

            _lastLoggedTargetHandle = targetHandle;
        }
    }

    private static string FormatHandle(int handle)
    {
        return handle > 0
            ? handle.ToString()
            : "none";
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

    private void Log(string message)
    {
        try
        {
            string directory =
                Path.GetDirectoryName(_logPath);

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
            string directory =
                Path.GetDirectoryName(_logPath);

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