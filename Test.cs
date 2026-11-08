using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;

    private const int TargetScanIntervalMilliseconds = 500;
    private const int InputHeartbeatIntervalMilliseconds = 2000;

    private const float TargetSelectionDistance = 8.0f;
    private const float TargetRetentionDistance = 9.0f;

    private const float AmbientGroupRadius = 8.0f;
    private const int AmbientGroupParameter2 = 2;
    private const int AmbientGroupTabCount = 0;
    private const int AmbientGroupFlags = 0;

    private const int VarStringType = 10;
    private const string VarStringFormat = "LITERAL_STRING";
    private const string PromptText = "Recruit";
    private const string PromptGroupName = "NPC";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly Dictionary<string, MethodInfo> _nativeMethods =
        new Dictionary<string, MethodInfo>();

    private readonly HashSet<string> _loggedErrors =
        new HashSet<string>();

    private readonly Dictionary<string, MethodInfo> _probeMethods =
        new Dictionary<string, MethodInfo>();

    private readonly Dictionary<string, bool> _lastProbeValues =
        new Dictionary<string, bool>();

    private readonly Dictionary<string, bool> _currentProbeValues =
        new Dictionary<string, bool>();

    private int _trackedTargetHandle;
    private int _focusedPromptHandle;
    private int _focusedPromptGroupId;

    private bool _targetWasFocused;
    private bool _hasScannedForTarget;
    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;
    private bool _promptErrorLogged;
    private bool _ambientActivationLoggedForTarget;

    private DateTime _lastTargetScanUtc;
    private DateTime _lastInputHeartbeatUtc;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused NPC ambient Recruit prompt test initialized ===");
        Log("Prompt text: VAR_STRING(" +
            VarStringType + ", \"" + VarStringFormat +
            "\", \"" + PromptText + "\").");
        Log("Prompt group name: \"" + PromptGroupName + "\".");
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("Ambient group activation is attempted every focused tick.");
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

        ResolveNative(
            "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
            new object[]
            {
                0,
                AmbientGroupRadius,
                AmbientGroupParameter2,
                AmbientGroupTabCount,
                0,
                0UL,
                AmbientGroupFlags
            });

        ResolveNative(
            "UI_PROMPT_SET_STANDARD_MODE",
            new object[] { 0, true });

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
        MethodInfo method = ResolveNative(nativeName, sampleArguments);
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

    private MethodInfo ResolveNative(string nativeName, object[] sampleArguments)
    {
        MethodInfo existingMethod;
        if (_nativeMethods.TryGetValue(nativeName, out existingMethod))
        {
            return existingMethod;
        }

        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            sampleArguments);

        _nativeMethods[nativeName] = method;

        if (method != null)
        {
            Log("NATIVE WRAPPER: " + nativeName +
                "; signature=" + FormatMethodSignature(method));
        }

        return method;
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
            _trackedTargetHandle +
            "; distance=" + distance);
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

            float distance = ped.Position.DistanceTo(
                playerPed.Position);

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

        ActivateAmbientPromptGroupThisFrame();
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
                "Could not find or invoke UI_PROMPT_REGISTER_BEGIN.");
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
                "Could not set INPUT_INTERACT_OPTION1 on the prompt.");

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
                "Could not create literal prompt text with VAR_STRING.");

            DeleteFocusedPrompt();
            return;
        }

        Log("VAR_STRING RESULT: type=" +
            (textResult == null
                ? "null"
                : textResult.GetType().FullName) +
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

        bool standardModeSet = TryInvokeNative(
            "UI_PROMPT_SET_STANDARD_MODE",
            new object[]
            {
                _focusedPromptHandle,
                true
            },
            out object standardModeResult);

        Log("PROMPT STANDARD MODE: wrapperInvoked=" +
            standardModeSet +
            "; result=" + FormatResult(standardModeResult));

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
        _ambientActivationLoggedForTarget = false;

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
                "Could not find or invoke " +
                "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY.");
            return;
        }

        if (!TryConvertToInt(groupResult, out int groupId) ||
            groupId == 0)
        {
            LogPromptErrorOnce(
                "Target prompt-group native returned an invalid value: " +
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
            "Recruit prompt attached. Check for G Recruit.");
    }

    private void ActivateAmbientPromptGroupThisFrame()
    {
        if (_trackedTargetHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        if (!_nativeMethods.ContainsKey(
            "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME"))
        {
            ResolveNative(
                "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                new object[]
                {
                    _trackedTargetHandle,
                    AmbientGroupRadius,
                    AmbientGroupParameter2,
                    AmbientGroupTabCount,
                    _focusedPromptGroupId,
                    0UL,
                    AmbientGroupFlags
                });
        }

        if (!TryInvokeNative(
            "VAR_STRING",
            new object[]
            {
                VarStringType,
                VarStringFormat,
                PromptGroupName
            },
            out object groupNameResult))
        {
            LogPromptErrorOnce(
                "Could not create the ambient prompt-group name.");
            return;
        }

        bool invoked = TryInvokeNative(
            "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
            new object[]
            {
                _trackedTargetHandle,
                AmbientGroupRadius,
                AmbientGroupParameter2,
                AmbientGroupTabCount,
                _focusedPromptGroupId,
                groupNameResult,
                AmbientGroupFlags
            },
            out object activationResult);

        if (!invoked)
        {
            return;
        }

        if (!_ambientActivationLoggedForTarget)
        {
            _ambientActivationLoggedForTarget = true;

            Log("AMBIENT GROUP ACTIVATION INVOKED: target=" +
                _trackedTargetHandle +
                "; radius=" + AmbientGroupRadius +
                "; p2=" + AmbientGroupParameter2 +
                "; tabCount=" + AmbientGroupTabCount +
                "; group=" + _focusedPromptGroupId +
                "; flags=" + AmbientGroupFlags +
                "; result=" + FormatResult(activationResult));
        }
    }

    private void DetachFocusedPrompt()
    {
        _ambientActivationLoggedForTarget = false;

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
                "; wrapperInvoked=" + removed +
                "; result=" + FormatResult(removeResult));

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

    private void ResolveProbeMethodIfNeeded(
        string nativeName,
        object[] arguments)
    {
        if (_probeMethods.ContainsKey(nativeName))
        {
            return;
        }

        MethodInfo method = ResolveNative(nativeName, arguments);
        _probeMethods[nativeName] = method;

        if (method == null)
        {
            Log("PROBE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + arguments.Length);
        }
    }

    private void SampleProbe(
        string nativeName,
        object[] arguments,
        bool isPulse)
    {
        ResolveProbeMethodIfNeeded(nativeName, arguments);

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
            value = Convert.ToBoolean(
                result,
                CultureInfo.InvariantCulture);
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
            "; wrapperInvoked=" + deleted +
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
        MethodInfo method = ResolveNative(nativeName, arguments);

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

            if (parameters.Length != arguments.Length)
            {
                LogErrorOnce(
                    "native_count_" + method.Name,
                    "NATIVE ARGUMENT COUNT MISMATCH: " +
                    FormatMethodSignature(method));
                return false;
            }

            object[] invokeArguments =
                new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!TryConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType,
                    out invokeArguments[i]))
                {
                    LogErrorOnce(
                        "native_conversion_" + method.Name + "_" + i,
                        "NATIVE ARGUMENT CONVERSION FAILED: " +
                        method.Name +
                        "; parameter=" + i +
                        "; signature=" +
                        FormatMethodSignature(method));
                    return false;
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

                if (parameters.Length != arguments.Length)
                {
                    continue;
                }

                bool compatible = true;

                for (int i = 0; i < parameters.Length; i++)
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

    private bool CanConvertArgument(
        object value,
        Type targetType)
    {
        object convertedValue;

        return TryConvertArgument(
            value,
            targetType,
            out convertedValue);
    }

    private bool TryConvertArgument(
        object value,
        Type targetType,
        out object convertedValue)
    {
        convertedValue = null;

        if (targetType == null)
        {
            return false;
        }

        if (targetType.IsByRef)
        {
            targetType = targetType.GetElementType();
        }

        if (value == null)
        {
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
        }

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            targetType = nullableType;
        }

        try
        {
            if (targetType.IsInstanceOfType(value))
            {
                convertedValue = value;
                return true;
            }

            if (targetType.IsEnum)
            {
                convertedValue = Enum.ToObject(
                    targetType,
                    Convert.ToInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            if (targetType == typeof(IntPtr))
            {
                convertedValue = new IntPtr(
                    Convert.ToInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            if (targetType == typeof(UIntPtr))
            {
                convertedValue = new UIntPtr(
                    Convert.ToUInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            convertedValue = Convert.ChangeType(
                value,
                targetType,
                CultureInfo.InvariantCulture);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool TryConvertToInt(object value, out int result)
    {
        result = 0;

        if (value == null)
        {
            return false;
        }

        try
        {
            result = Convert.ToInt32(
                value,
                CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string NormalizeNativeName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        return name
            .Replace("_", string.Empty)
            .Replace(" ", string.Empty)
            .ToUpperInvariant();
    }

    private string FormatMethodSignature(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        List<string> parameterNames = new List<string>();

        foreach (ParameterInfo parameter in parameters)
        {
            parameterNames.Add(
                parameter.ParameterType.FullName);
        }

        return method.DeclaringType.FullName +
            "." + method.Name +
            "(" + string.Join(", ", parameterNames.ToArray()) + ")";
    }

    private string FormatResult(object value)
    {
        if (value == null)
        {
            return "null";
        }

        try
        {
            return Convert.ToString(
                value,
                CultureInfo.InvariantCulture);
        }
        catch
        {
            return value.ToString();
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
        string line =
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture) +
            " " + message +
            Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }

    private string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }
}