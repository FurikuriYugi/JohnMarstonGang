using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;

    private const string IloPromptText = "Recruit";
    private const float IloRadius = 2.5f;
    private const float IloFocusDistance = 2.5f;
    private const int IloFlags = 0;
    private const float IloUnknownFloat1 = 0.0f;
    private const float IloUnknownFloat2 = 0.0f;
    private const int IloCustomPromptHandle = 0;
    private const bool IloShowApproachPrompt = true;
    private const int IloUnknownInt = -1;

    // This registered game string is a baseline, not the final "Recruit" label.
    // The API has a known issue with custom prompt text created through VAR_STRING.
    private const string FocusedPromptTextKey = "WARDROBE_INSPECT_PROMPT";

    private const float TargetSelectionDistance = 8.0f;
    private const float TargetRetentionDistance = 9.0f;
    private const int TargetScanIntervalMilliseconds = 500;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly MethodInfo _registerIloMethod;
    private readonly MethodInfo _unregisterIloMethod;

    private int _trackedTargetHandle;
    private int _iloRegisteredTargetHandle;
    private bool _iloRegistrationReturnedTrue;

    private int _focusedPromptHandle;
    private int _focusedPromptGroupId;
    private bool _promptWasPressed;
    private bool _targetWasFocused;

    private bool _hasScannedForTarget;
    private DateTime _lastTargetScanUtc;

    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;
    private bool _promptErrorLogged;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        _registerIloMethod = FindCompatibleNativeMethod(
            "REGISTER_INTERACTION_LOCKON_PROMPT",
            new[]
            {
                typeof(int),
                typeof(string),
                typeof(float),
                typeof(float),
                typeof(int),
                typeof(float),
                typeof(float),
                typeof(int),
                typeof(bool),
                typeof(int)
            });

        _unregisterIloMethod = FindCompatibleNativeMethod(
            "UNREGISTER_INTERACTION_LOCKON_PROMPT",
            new[]
            {
                typeof(int)
            });

        Log("=== Focused NPC prompt-group baseline test initialized ===");
        Log("This test registers the ILO on the nearest human NPC within " +
            TargetSelectionDistance + " metres before focus.");
        Log("The ILO approach text is \"" + IloPromptText + "\".");
        Log("While the NPC is focused, the test tries to attach a G prompt " +
            "using the target entity's prompt group.");
        Log("The focused prompt uses the registered text key \"" +
            FocusedPromptTextKey + "\", not custom Recruit text.");
        Log("No recruitment action is executed and no gang data is changed.");

        Log("ILO arguments: radius=" + IloRadius +
            "; focusDistance=" + IloFocusDistance +
            "; flags=" + IloFlags +
            "; unknownFloat1=" + IloUnknownFloat1 +
            "; unknownFloat2=" + IloUnknownFloat2 +
            "; customPromptHandle=" + IloCustomPromptHandle +
            "; showApproachPrompt=" + IloShowApproachPrompt +
            "; unknownInt=" + IloUnknownInt);

        LogResolvedMethod(
            "REGISTER_INTERACTION_LOCKON_PROMPT",
            _registerIloMethod);

        LogResolvedMethod(
            "UNREGISTER_INTERACTION_LOCKON_PROMPT",
            _unregisterIloMethod);

        if (_registerIloMethod == null)
        {
            Log("ILO SETUP UNAVAILABLE: no compatible register wrapper was found.");
        }

        if (_unregisterIloMethod == null)
        {
            Log("ILO CLEANUP UNAVAILABLE: no compatible unregister wrapper was found.");
        }

        CreateFocusedPrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Prompt test ready. Focus a human NPC.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateNearbyTargetIfNeeded();
            SampleFocusAndPrompt();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));

                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt test error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdateNearbyTargetIfNeeded()
    {
        DateTime now = DateTime.UtcNow;

        if (_hasScannedForTarget &&
            (now - _lastTargetScanUtc).TotalMilliseconds <
            TargetScanIntervalMilliseconds)
        {
            return;
        }

        _hasScannedForTarget = true;
        _lastTargetScanUtc = now;

        Ped selectedPed = FindNearbyHumanTarget();
        int selectedHandle = 0;

        if (selectedPed != null && selectedPed.Exists())
        {
            selectedHandle = selectedPed.Handle;
        }

        if (selectedHandle == _trackedTargetHandle)
        {
            return;
        }

        ClearTrackedTarget();

        if (selectedHandle <= 0)
        {
            return;
        }

        _trackedTargetHandle = selectedHandle;

        float distance = selectedPed.Position.DistanceTo(
            Game.Player.Character.Position);

        Log("HUMAN TARGET BEGIN: target=" + _trackedTargetHandle +
            "; distance=" + distance +
            "; freeFocusing=" + Game.Player.IsFreeFocusing +
            "; registeredBeforeFocus=true");

        RegisterIloForTarget(_trackedTargetHandle);
    }

    private Ped FindNearbyHumanTarget()
    {
        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            return null;
        }

        Ped[] worldPeds = World.GetAllPeds();

        if (worldPeds == null)
        {
            return null;
        }

        Ped retainedPed = null;
        Ped nearestPed = null;
        float nearestDistance = TargetSelectionDistance + 1.0f;

        foreach (Ped ped in worldPeds)
        {
            if (ped == null ||
                !ped.Exists() ||
                ped.Handle == playerPed.Handle ||
                ped.IsDeadOrDying ||
                !IsHumanEntity(ped.Handle))
            {
                continue;
            }

            float distance = ped.Position.DistanceTo(playerPed.Position);

            if (ped.Handle == _trackedTargetHandle &&
                distance <= TargetRetentionDistance)
            {
                retainedPed = ped;
                continue;
            }

            if (distance <= TargetSelectionDistance &&
                distance < nearestDistance)
            {
                nearestPed = ped;
                nearestDistance = distance;
            }
        }

        if (retainedPed != null)
        {
            return retainedPed;
        }

        return nearestPed;
    }

    private bool IsHumanEntity(int entityHandle)
    {
        if (entityHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void SampleFocusAndPrompt()
    {
        if (_trackedTargetHandle <= 0)
        {
            DetachFocusedPrompt();
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" + _trackedTargetHandle);
            ClearTrackedTarget();
            return;
        }

        int focusedTargetHandle = GetCurrentHumanTargetHandle();
        bool targetIsFocused =
            focusedTargetHandle == _trackedTargetHandle;

        if (targetIsFocused != _targetWasFocused)
        {
            _targetWasFocused = targetIsFocused;

            if (targetIsFocused)
            {
                Log("TRACKED TARGET FOCUS BEGIN: target=" +
                    _trackedTargetHandle);

                LogGControlAvailability(_trackedTargetHandle);
                AttachFocusedPrompt(_trackedTargetHandle);
            }
            else
            {
                Log("TRACKED TARGET FOCUS END: target=" +
                    _trackedTargetHandle);

                DetachFocusedPrompt();
            }
        }

        if (!targetIsFocused ||
            _focusedPromptHandle <= 0)
        {
            return;
        }

        bool promptPressed;

        if (!TryInvokeNative(
            "UI_PROMPT_IS_PRESSED",
            new object[] { _focusedPromptHandle },
            out object pressedResult))
        {
            return;
        }

        try
        {
            promptPressed = Convert.ToBoolean(pressedResult);
        }
        catch
        {
            LogPromptErrorOnce(
                "UI_PROMPT_IS_PRESSED returned an unexpected value: " +
                FormatResult(pressedResult));
            return;
        }

        if (promptPressed && !_promptWasPressed)
        {
            Log("FOCUSED PROMPT PRESSED: target=" +
                _trackedTargetHandle +
                "; prompt=" + _focusedPromptHandle +
                "; control=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; note=baseline prompt only; no recruitment action executed");
        }

        _promptWasPressed = promptPressed;
    }

    private int GetCurrentHumanTargetHandle()
    {
        try
        {
            if (Game.Player.IsFreeFocusing)
            {
                int focusedHandle = 0;
                bool foundFocusedTarget;

                unsafe
                {
                    foundFocusedTarget =
                        RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                            Game.Player.Handle,
                            &focusedHandle);
                }

                if (foundFocusedTarget &&
                    IsHumanEntity(focusedHandle))
                {
                    return focusedHandle;
                }
            }

            Entity aimedEntity = Game.Player.GetFreeAimEntity();

            if (aimedEntity != null && aimedEntity.Exists())
            {
                int aimedHandle = aimedEntity.Handle;

                if (IsHumanEntity(aimedHandle))
                {
                    return aimedHandle;
                }
            }
        }
        catch (Exception exception)
        {
            if (!_targetResolutionErrorLogged)
            {
                _targetResolutionErrorLogged = true;
                Log("TARGET RESOLUTION ERROR: " +
                    GetInnermostMessage(exception));
            }
        }

        return 0;
    }

    private void CreateFocusedPrompt()
    {
        if (!TryInvokeNative(
            "UI_PROMPT_REGISTER_BEGIN",
            new object[0],
            out object promptResult))
        {
            LogPromptErrorOnce(
                "Could not find a compatible UI_PROMPT_REGISTER_BEGIN wrapper.");
            return;
        }

        if (!TryConvertToInt(promptResult, out _focusedPromptHandle) ||
            _focusedPromptHandle <= 0)
        {
            LogPromptErrorOnce(
                "UI_PROMPT_REGISTER_BEGIN returned an invalid handle: " +
                FormatResult(promptResult));
            _focusedPromptHandle = 0;
            return;
        }

        Log("FOCUSED PROMPT HANDLE: " + _focusedPromptHandle);

        if (!TryInvokeNative(
            "UI_PROMPT_SET_CONTROL_ACTION",
            new object[]
            {
                _focusedPromptHandle,
                InputInteractOption1
            },
            out object controlResult))
        {
            LogPromptErrorOnce(
                "Could not set the focused prompt control action.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT CONTROL ACTION SET: " +
            "INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8") +
            "; result=" + FormatResult(controlResult));

        if (!TryInvokeNative(
            "UI_PROMPT_SET_TEXT",
            new object[]
            {
                _focusedPromptHandle,
                FocusedPromptTextKey
            },
            out object textResult))
        {
            LogPromptErrorOnce(
                "Could not set prompt text using the registered game string key. " +
                "Check the resolved UI_PROMPT_SET_TEXT wrapper signature.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT TEXT SET: key=\"" + FocusedPromptTextKey +
            "\"; result=" + FormatResult(textResult));

        if (!TryInvokeNative(
            "UI_PROMPT_REGISTER_END",
            new object[] { _focusedPromptHandle },
            out object registerEndResult))
        {
            LogPromptErrorOnce(
                "Could not finish UI_PROMPT_REGISTER_END.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT REGISTER END: result=" +
            FormatResult(registerEndResult));

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, false },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, false },
            out object visibleResult);

        Log("FOCUSED PROMPT CREATED: prompt=" +
            _focusedPromptHandle +
            "; initialEnabled=false; initialVisible=false");
    }

    private void AttachFocusedPrompt(int targetHandle)
    {
        if (_focusedPromptHandle <= 0)
        {
            LogPromptErrorOnce(
                "Cannot attach prompt because prompt creation failed.");
            return;
        }

        if (!TryInvokeNative(
            "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
            new object[] { targetHandle },
            out object groupResult))
        {
            LogPromptErrorOnce(
                "Could not find a compatible " +
                "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY wrapper.");
            return;
        }

        if (!TryConvertToInt(groupResult, out int groupId))
        {
            LogPromptErrorOnce(
                "Target prompt-group native returned an unexpected value: " +
                FormatResult(groupResult));
            return;
        }

        _focusedPromptGroupId = groupId;

        Log("TARGET PROMPT GROUP: target=" + targetHandle +
            "; group=" + _focusedPromptGroupId);

        if (!TryInvokeNative(
            "UI_PROMPT_SET_GROUP",
            new object[]
            {
                _focusedPromptHandle,
                _focusedPromptGroupId,
                0
            },
            out object setGroupResult))
        {
            LogPromptErrorOnce(
                "Could not attach the custom prompt to the target's prompt group.");
            _focusedPromptGroupId = 0;
            return;
        }

        Log("PROMPT ATTACHED TO TARGET GROUP: prompt=" +
            _focusedPromptHandle +
            "; target=" + targetHandle +
            "; group=" + _focusedPromptGroupId +
            "; result=" + FormatResult(setGroupResult));

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, true },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, true },
            out object visibleResult);

        _promptWasPressed = false;

        Log("FOCUSED PROMPT ENABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        RDR2.UI.Screen.PrintSubtitle(
            "G prompt attached to focused NPC.");
    }

    private void DetachFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
            _promptWasPressed = false;
            return;
        }

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, false },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, false },
            out object visibleResult);

        if (_focusedPromptGroupId != 0)
        {
            bool removed = TryInvokeNative(
                "UI_PROMPT_REMOVE_GROUP",
                new object[]
                {
                    _focusedPromptHandle,
                    _focusedPromptGroupId
                },
                out object removeResult);

            Log("PROMPT GROUP DETACH: prompt=" +
                _focusedPromptHandle +
                "; group=" + _focusedPromptGroupId +
                "; wrapperFound=" + removed +
                "; result=" + FormatResult(removeResult));
        }

        Log("FOCUSED PROMPT DISABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        _focusedPromptGroupId = 0;
        _promptWasPressed = false;
    }

    private void DeleteFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            return;
        }

        DetachFocusedPrompt();

        bool deleted = TryInvokeNative(
            "UI_PROMPT_DELETE",
            new object[] { _focusedPromptHandle },
            out object deleteResult);

        Log("FOCUSED PROMPT DELETE: prompt=" +
            _focusedPromptHandle +
            "; wrapperFound=" + deleted +
            "; result=" + FormatResult(deleteResult));

        _focusedPromptHandle = 0;
        _focusedPromptGroupId = 0;
    }

    private void LogGControlAvailability(int targetHandle)
    {
        try
        {
            bool enabled =
                RDR2.Native.PAD.IS_CONTROL_ENABLED(
                    ControlPadIndex,
                    InputInteractOption1);

            Log("G CANDIDATE CONTROL: target=" + targetHandle +
                "; name=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; enabled=" + enabled +
                "; note=reported G mapping");
        }
        catch (Exception exception)
        {
            Log("G CONTROL AVAILABILITY ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void RegisterIloForTarget(int targetHandle)
    {
        if (_registerIloMethod == null)
        {
            return;
        }

        try
        {
            object result = _registerIloMethod.Invoke(
                null,
                new object[]
                {
                    targetHandle,
                    IloPromptText,
                    IloRadius,
                    IloFocusDistance,
                    IloFlags,
                    IloUnknownFloat1,
                    IloUnknownFloat2,
                    IloCustomPromptHandle,
                    IloShowApproachPrompt,
                    IloUnknownInt
                });

            bool registered = result is bool && (bool)result;

            Log("ILO REGISTER RESULT: target=" + targetHandle +
                "; returned=" + FormatResult(result));

            _iloRegisteredTargetHandle = targetHandle;
            _iloRegistrationReturnedTrue = registered;

            if (registered)
            {
                Log("ILO REGISTERED: target=" + targetHandle +
                    "; text=\"" + IloPromptText + "\"" +
                    "; registeredBeforeFocus=true" +
                    "; note=true return does not prove the prompt rendered");
            }
        }
        catch (Exception exception)
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;

            Log("ILO REGISTER ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
        }
    }

    private void ClearTrackedTarget()
    {
        if (_trackedTargetHandle > 0)
        {
            Log("HUMAN TARGET END: target=" + _trackedTargetHandle);
        }

        DetachFocusedPrompt();
        UnregisterIlo();

        _trackedTargetHandle = 0;
        _targetWasFocused = false;
    }

    private void UnregisterIlo()
    {
        if (_iloRegisteredTargetHandle <= 0 ||
            !_iloRegistrationReturnedTrue)
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
            return;
        }

        if (_unregisterIloMethod == null)
        {
            Log("ILO CLEANUP SKIPPED: no compatible unregister wrapper; target=" +
                _iloRegisteredTargetHandle);

            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
            return;
        }

        int targetHandle = _iloRegisteredTargetHandle;

        try
        {
            object result = _unregisterIloMethod.Invoke(
                null,
                new object[] { targetHandle });

            Log("ILO UNREGISTER RESULT: target=" + targetHandle +
                "; returned=" + FormatResult(result));
        }
        catch (Exception exception)
        {
            Log("ILO UNREGISTER ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
        }
        finally
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
        }
    }

    private void OnAborted(object sender, EventArgs e)
    {
        try
        {
            ClearTrackedTarget();
            DeleteFocusedPrompt();
        }
        catch (Exception exception)
        {
            Log("SCRIPT CLEANUP ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private bool TryInvokeNative(
        string nativeName,
        object[] arguments,
        out object result)
    {
        result = null;

        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            arguments);

        if (method == null)
        {
            Log("NATIVE WRAPPER NOT FOUND: " +
                nativeName +
                "; argumentCount=" + arguments.Length);
            return false;
        }

        try
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] convertedArguments = new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!TryConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType,
                    out convertedArguments[i]))
                {
                    Log("NATIVE ARGUMENT CONVERSION FAILED: " +
                        nativeName +
                        "; parameter=" + i +
                        "; type=" + parameters[i].ParameterType.FullName);
                    return false;
                }
            }

            result = method.Invoke(null, convertedArguments);
            return true;
        }
        catch (Exception exception)
        {
            Log("NATIVE INVOKE ERROR: " + nativeName +
                "; " + GetInnermostMessage(exception));
            return false;
        }
    }

    private MethodInfo FindCompatibleNativeMethod(
        string nativeName,
        object[] arguments)
    {
        Assembly apiAssembly = typeof(Game).Assembly;
        Type[] apiTypes;

        try
        {
            apiTypes = apiAssembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            System.Collections.Generic.List<Type> loadedTypes =
                new System.Collections.Generic.List<Type>();

            if (exception.Types != null)
            {
                foreach (Type type in exception.Types)
                {
                    if (type != null)
                    {
                        loadedTypes.Add(type);
                    }
                }
            }

            apiTypes = loadedTypes.ToArray();
        }

        string normalizedWanted = NormalizeNativeName(nativeName);

        foreach (Type type in apiTypes)
        {
            if (type == null)
            {
                continue;
            }

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (!string.Equals(
                    NormalizeNativeName(method.Name),
                    normalizedWanted,
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length != arguments.Length)
                {
                    continue;
                }

                bool compatible = true;

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (!CanConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType))
                    {
                        compatible = false;
                        break;
                    }
                }

                if (compatible)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static string NormalizeNativeName(string name)
    {
        return name.Replace("_", string.Empty);
    }

    private static bool CanConvertArgument(
        object value,
        Type targetType)
    {
        if (value == null)
        {
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
        }

        Type effectiveType =
            Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (effectiveType.IsAssignableFrom(value.GetType()))
        {
            return true;
        }

        if (effectiveType.IsEnum)
        {
            return IsNumericType(value.GetType());
        }

        if (IsNumericType(effectiveType) &&
            IsNumericType(value.GetType()))
        {
            return true;
        }

        if (effectiveType == typeof(IntPtr) ||
            effectiveType == typeof(UIntPtr))
        {
            return IsNumericType(value.GetType());
        }

        return effectiveType == typeof(string) &&
            value is string;
    }

    private static bool TryConvertArgument(
        object value,
        Type targetType,
        out object converted)
    {
        converted = null;

        if (value == null)
        {
            if (!targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null)
            {
                return true;
            }

            return false;
        }

        Type effectiveType =
            Nullable.GetUnderlyingType(targetType) ?? targetType;

        try
        {
            if (effectiveType.IsAssignableFrom(value.GetType()))
            {
                converted = value;
                return true;
            }

            if (effectiveType.IsEnum)
            {
                converted = Enum.ToObject(effectiveType, value);
                return true;
            }

            if (effectiveType == typeof(IntPtr))
            {
                converted = new IntPtr(Convert.ToInt64(value));
                return true;
            }

            if (effectiveType == typeof(UIntPtr))
            {
                converted = new UIntPtr(Convert.ToUInt64(value));
                return true;
            }

            if (effectiveType == typeof(string) &&
                value is string)
            {
                converted = value;
                return true;
            }

            if (IsNumericType(effectiveType) &&
                IsNumericType(value.GetType()))
            {
                converted = Convert.ChangeType(value, effectiveType);
                return true;
            }
        }
        catch
        {
            return false;
        }

        return false;
    }

    private static bool IsNumericType(Type type)
    {
        Type effectiveType =
            Nullable.GetUnderlyingType(type) ?? type;

        return effectiveType == typeof(byte) ||
            effectiveType == typeof(sbyte) ||
            effectiveType == typeof(short) ||
            effectiveType == typeof(ushort) ||
            effectiveType == typeof(int) ||
            effectiveType == typeof(uint) ||
            effectiveType == typeof(long) ||
            effectiveType == typeof(ulong) ||
            effectiveType == typeof(float) ||
            effectiveType == typeof(double) ||
            effectiveType == typeof(decimal);
    }

    private static bool TryConvertToInt(
        object value,
        out int converted)
    {
        converted = 0;

        try
        {
            if (value == null)
            {
                return false;
            }

            converted = Convert.ToInt32(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LogResolvedMethod(
        string nativeName,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("NATIVE WRAPPER " + nativeName + ": not found");
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();
        string signature =
            method.DeclaringType.FullName +
            "." + method.Name + "(";

        for (int i = 0; i < parameters.Length; i++)
        {
            if (i > 0)
            {
                signature += ", ";
            }

            signature += parameters[i].ParameterType.FullName;
        }

        signature += ") -> " + method.ReturnType.FullName;

        Log("NATIVE WRAPPER " + nativeName + ": " + signature);
    }

    private void LogPromptErrorOnce(string message)
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT SETUP/TEST ERROR: " + message);
    }

    private static string FormatResult(object result)
    {
        if (result == null)
        {
            return "null";
        }

        return result.GetType().FullName + ":" + result;
    }

    private static string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.GetType().FullName + ": " + current.Message;
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }
}