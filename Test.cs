using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int TargetScanIntervalMilliseconds = 500;
    private const int InputHeartbeatIntervalMilliseconds = 2000;
    private const float TargetSelectionDistance = 8.0f;
    private const float TargetRetentionDistance = 9.0f;

    private const int VarStringType = 10;
    private const string VarStringFormat = "LITERAL_STRING";
    private const string PromptText = "Recruit";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly Dictionary<string, MethodInfo> _probeMethods =
        new Dictionary<string, MethodInfo>();

    private readonly Dictionary<string, bool> _lastProbeValues =
        new Dictionary<string, bool>();

    private readonly Dictionary<string, bool> _currentProbeValues =
        new Dictionary<string, bool>();

    private readonly HashSet<string> _loggedErrors =
        new HashSet<string>();

    private int _trackedTargetHandle;
    private int _focusedPromptHandle;
    private int _focusedPromptGroupId;

    private bool _targetWasFocused;
    private bool _hasScannedForTarget;
    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;
    private bool _promptErrorLogged;

    private DateTime _lastTargetScanUtc;
    private DateTime _lastInputHeartbeatUtc;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused NPC custom Recruit prompt test initialized ===");
        Log("Prompt text source: VAR_STRING(" +
            VarStringType + ", \"" + VarStringFormat +
            "\", \"" + PromptText + "\").");
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("The prompt is attached to the focused NPC target's prompt group.");
        Log("This test does not recruit, change gang membership, or trigger an animation.");

        ResolveProbe("UI_PROMPT_IS_ACTIVE", new object[] { 0 });
        ResolveProbe("UI_PROMPT_IS_PRESSED", new object[] { 0 });
        ResolveProbe("UI_PROMPT_IS_JUST_PRESSED", new object[] { 0 });
        ResolveProbe(
            "IS_CONTROL_ENABLED",
            new object[] { ControlPadIndex, InputInteractOption1 });
        ResolveProbe(
            "IS_CONTROL_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 });
        ResolveProbe(
            "IS_CONTROL_JUST_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 });

        CreateFocusedPrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC. Check for G Recruit, then tap G once.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateNearbyTargetIfNeeded();
            SampleFocusAndInput();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Recruit prompt test error. Check Test_Log.txt.");
            }
        }
    }

    private void ResolveProbe(string nativeName, object[] sampleArguments)
    {
        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            sampleArguments);

        _probeMethods[nativeName] = method;

        if (method == null)
        {
            Log("PROBE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + sampleArguments.Length);
            return;
        }

        Log("PROBE WRAPPER: " + nativeName +
            "; signature=" + FormatMethodSignature(method));
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

        Log("HUMAN TARGET BEGIN: target=" +
            _trackedTargetHandle + "; distance=" + distance);
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

        return retainedPed ?? nearestPed;
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

    private void SampleFocusAndInput()
    {
        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" +
                _trackedTargetHandle);
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
            _focusedPromptHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        SampleInputProbes();
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

        Log("PROMPT CONTROL ACTION SET: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8") +
            "; result=" + FormatResult(controlResult));

        if (!TryInvokeNative(
            "VAR_STRING",
            new object[]
            {
                VarStringType,
                VarStringFormat,
                PromptText
            },
            out object textResult))
        {
            LogPromptErrorOnce(
                "Could not create the literal prompt text with VAR_STRING.");
            DeleteFocusedPrompt();
            return;
        }

        Log("VAR_STRING RESULT: type=" +
            (textResult == null ? "null" : textResult.GetType().FullName) +
            "; value=" + FormatResult(textResult));

        if (!TryInvokeNative(
            "UI_PROMPT_SET_TEXT",
            new object[]
            {
                _focusedPromptHandle,
                textResult
            },
            out object setTextResult))
        {
            LogPromptErrorOnce(
                "Could not set the prompt text from the VAR_STRING result.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT TEXT SET: literal=\"" + PromptText +
            "\"; result=" + FormatResult(setTextResult));

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
            "; text=\"" + PromptText +
            "\"; initialEnabled=false; initialVisible=false");
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
                "Could not attach the prompt to the target's prompt group.");
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

        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
        _lastInputHeartbeatUtc = DateTime.MinValue;

        Log("FOCUSED PROMPT ENABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        RDR2.UI.Screen.PrintSubtitle(
            "G Recruit prompt attached. Tap G once.");
    }

    private void DetachFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
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

        if (_focusedPromptGroupId != 0)
        {
            Log("FOCUSED PROMPT DISABLED: prompt=" +
                _focusedPromptHandle +
                "; enabledResult=" + FormatResult(enabledResult) +
                "; visibleResult=" + FormatResult(visibleResult));
        }

        _focusedPromptGroupId = 0;
        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
    }

    private void SampleInputProbes()
    {
        SampleProbe(
            "UI_PROMPT_IS_ACTIVE",
            new object[] { _focusedPromptHandle },
            false);

        SampleProbe(
            "UI_PROMPT_IS_PRESSED",
            new object[] { _focusedPromptHandle },
            false);

        SampleProbe(
            "UI_PROMPT_IS_JUST_PRESSED",
            new object[] { _focusedPromptHandle },
            true);

        SampleProbe(
            "IS_CONTROL_ENABLED",
            new object[] { ControlPadIndex, InputInteractOption1 },
            false);

        SampleProbe(
            "IS_CONTROL_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 },
            false);

        SampleProbe(
            "IS_CONTROL_JUST_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 },
            true);

        DateTime now = DateTime.UtcNow;

        if (_lastInputHeartbeatUtc == DateTime.MinValue ||
            (now - _lastInputHeartbeatUtc).TotalMilliseconds >=
            InputHeartbeatIntervalMilliseconds)
        {
            _lastInputHeartbeatUtc = now;

            Log("INPUT HEARTBEAT: promptActive=" +
                GetProbeValue("UI_PROMPT_IS_ACTIVE") +
                "; promptPressed=" +
                GetProbeValue("UI_PROMPT_IS_PRESSED") +
                "; promptJustPressed=" +
                GetProbeValue("UI_PROMPT_IS_JUST_PRESSED") +
                "; controlEnabled=" +
                GetProbeValue("IS_CONTROL_ENABLED") +
                "; controlPressed=" +
                GetProbeValue("IS_CONTROL_PRESSED") +
                "; controlJustPressed=" +
                GetProbeValue("IS_CONTROL_JUST_PRESSED"));
        }
    }

    private void SampleProbe(
        string nativeName,
        object[] arguments,
        bool isPulse)
    {
        if (!_probeMethods.TryGetValue(
            nativeName,
            out MethodInfo method) ||
            method == null)
        {
            return;
        }

        if (!TryInvokeMethod(method, arguments, out object result))
        {
            return;
        }

        bool value;

        try
        {
            value = Convert.ToBoolean(result);
        }
        catch
        {
            LogErrorOnce(
                "probe_" + nativeName,
                "Probe returned an unexpected value: " +
                FormatResult(result));
            return;
        }

        _currentProbeValues[nativeName] = value;

        if (isPulse)
        {
            if (value)
            {
                Log("INPUT EVENT: " + nativeName +
                    "=true; target=" + _trackedTargetHandle +
                    "; prompt=" + _focusedPromptHandle);
            }

            return;
        }

        bool previousValue;

        if (!_lastProbeValues.TryGetValue(
            nativeName,
            out previousValue) ||
            previousValue != value)
        {
            Log("INPUT STATE: " + nativeName +
                "=" + value +
                "; target=" + _trackedTargetHandle +
                "; prompt=" + _focusedPromptHandle);

            _lastProbeValues[nativeName] = value;
        }
    }

    private string GetProbeValue(string nativeName)
    {
        if (_currentProbeValues.TryGetValue(
            nativeName,
            out bool value))
        {
            return value.ToString();
        }

        if (!_probeMethods.ContainsKey(nativeName) ||
            _probeMethods[nativeName] == null)
        {
            return "wrapper-unavailable";
        }

        return "not-sampled";
    }

    private void ClearTrackedTarget()
    {
        if (_trackedTargetHandle > 0)
        {
            Log("HUMAN TARGET END: target=" +
                _trackedTargetHandle);
        }

        DetachFocusedPrompt();
        _trackedTargetHandle = 0;
        _targetWasFocused = false;
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
        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            arguments);

        if (method == null)
        {
            LogErrorOnce(
                "native_missing_" + nativeName,
                "NATIVE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + arguments.Length);
            result = null;
            return false;
        }

        return TryInvokeMethod(method, arguments, out result);
    }

    private bool TryInvokeMethod(
        MethodInfo method,
        object[] arguments,
        out object result)
    {
        result = null;

        try
        {
            ParameterInfo[] parameters = method.GetParameters();
            bool hasParamArray =
                parameters.Length > 0 &&
                Attribute.IsDefined(
                    parameters[parameters.Length - 1],
                    typeof(ParamArrayAttribute));

            object[] invokeArguments;

            if (hasParamArray)
            {
                int fixedParameterCount = parameters.Length - 1;

                if (arguments.Length < fixedParameterCount)
                {
                    LogErrorOnce(
                        "native_count_" + method.Name,
                        "NATIVE ARGUMENT COUNT MISMATCH: " +
                        FormatMethodSignature(method));
                    return false;
                }

                invokeArguments = new object[parameters.Length];

                for (int i = 0; i < fixedParameterCount; i++)
                {
                    if (!TryConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType,
                        out invokeArguments[i]))
                    {
                        LogArgumentConversionFailure(method, i);
                        return false;
                    }
                }

                Type elementType =
                    parameters[parameters.Length - 1]
                        .ParameterType
                        .GetElementType();

                int extraArgumentCount =
                    arguments.Length - fixedParameterCount;

                Array extraArguments =
                    Array.CreateInstance(elementType, extraArgumentCount);

                for (int i = 0; i < extraArgumentCount; i++)
                {
                    if (!TryConvertArgument(
                        arguments[fixedParameterCount + i],
                        elementType,
                        out object convertedArgument))
                    {
                        LogArgumentConversionFailure(
                            method,
                            fixedParameterCount + i);
                        return false;
                    }

                    extraArguments.SetValue(convertedArgument, i);
                }

                invokeArguments[parameters.Length - 1] = extraArguments;
            }
            else
            {
                if (parameters.Length != arguments.Length)
                {
                    LogErrorOnce(
                        "native_count_" + method.Name,
                        "NATIVE ARGUMENT COUNT MISMATCH: " +
                        FormatMethodSignature(method));
                    return false;
                }

                invokeArguments = new object[arguments.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (!TryConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType,
                        out invokeArguments[i]))
                    {
                        LogArgumentConversionFailure(method, i);
                        return false;
                    }
                }
            }

            result = method.Invoke(null, invokeArguments);
            return true;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "native_invoke_" + method.Name,
                "NATIVE INVOKE ERROR: " +
                FormatMethodSignature(method) +
                "; " + GetInnermostMessage(exception));
            return false;
        }
    }

    private void LogArgumentConversionFailure(
        MethodInfo method,
        int parameterIndex)
    {
        ParameterInfo[] parameters = method.GetParameters();

        LogErrorOnce(
            "native_conversion_" + method.Name + "_" + parameterIndex,
            "NATIVE ARGUMENT CONVERSION FAILED: " +
            method.Name +
            "; parameter=" + parameterIndex +
            "; signature=" + FormatMethodSignature(method));
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
            List<Type> loadedTypes = new List<Type>();

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
                if (method.ContainsGenericParameters ||
                    !string.Equals(
                        NormalizeNativeName(method.Name),
                        normalizedWanted,
                        StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();
                bool hasParamArray =
                    parameters.Length > 0 &&
                    Attribute.IsDefined(
                        parameters[parameters.Length - 1],
                        typeof(ParamArrayAttribute));

                if (!hasParamArray &&
                    parameters.Length != arguments.Length)
                {
                    continue;
                }

                if (hasParamArray &&
                    arguments.Length < parameters.Length - 1)
                {
                    continue;
                }

                bool compatible = true;
                int fixedParameterCount = hasParamArray
                    ? parameters.Length - 1
                    : parameters.Length;

                for (int i = 0; i < fixedParameterCount; i++)
                {
                    if (!CanConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType))
                    {
                        compatible = false;
                        break;
                    }
                }

                if (!compatible)
                {
                    continue;
                }

                if (hasParamArray)
                {
                    Type elementType =
                        parameters[parameters.Length - 1]
                            .ParameterType
                            .GetElementType();

                    for (int i = fixedParameterCount;
                        i < arguments.Length;
                        i++)
                    {
                        if (!CanConvertArgument(
                            arguments[i],
                            elementType))
                        {
                            compatible = false;
                            break;
                        }
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

        return false;
    }

    private static bool TryConvertArgument(
        object value,
        Type targetType,
        out object converted)
    {
        converted = null;

        if (value == null)
        {
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
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

        switch (Type.GetTypeCode(effectiveType))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
                return true;

            default:
                return false;
        }
    }

    private static bool TryConvertToInt(
        object value,
        out int converted)
    {
        converted = 0;

        if (value == null)
        {
            return false;
        }

        try
        {
            if (value is IntPtr intPtr)
            {
                converted = intPtr.ToInt32();
                return true;
            }

            if (value is UIntPtr uintPtr)
            {
                converted = unchecked((int)uintPtr.ToUInt32());
                return true;
            }

            converted = Convert.ToInt32(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LogPromptErrorOnce(string message)
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT ERROR: " + message);
    }

    private void LogErrorOnce(string key, string message)
    {
        if (_loggedErrors.Add(key))
        {
            Log(message);
        }
    }

    private void Log(string message)
    {
        string line = "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " + message + Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }

    private static string FormatResult(object result)
    {
        if (result == null)
        {
            return "null";
        }

        return result.GetType().FullName + ":" + result;
    }

    private static string FormatMethodSignature(MethodInfo method)
    {
        List<string> parameterTypes = new List<string>();

        foreach (ParameterInfo parameter in method.GetParameters())
        {
            parameterTypes.Add(parameter.ParameterType.FullName);
        }

        return method.DeclaringType.FullName +
            "." + method.Name +
            "(" + string.Join(", ", parameterTypes.ToArray()) +
            ") : " + method.ReturnType.FullName;
    }

    private static string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }
}