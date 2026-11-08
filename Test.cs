using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

    private readonly HashSet<string> _loggedErrors =
        new HashSet<string>();

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
        Log("Prompt text: VAR_STRING<string>(" +
            VarStringType + ", \"" + VarStringFormat +
            "\", \"" + PromptText + "\").");
        Log("Prompt group name: \"" + PromptGroupName + "\".");
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("Ambient group activation is attempted every focused tick.");
        Log("This test does not recruit, change gang membership, or trigger an animation.");

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
        try
        {
            _focusedPromptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_focusedPromptHandle <= 0)
            {
                LogPromptErrorOnce(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid handle: " +
                    _focusedPromptHandle);

                _focusedPromptHandle = 0;
                return;
            }

            Log("FOCUSED PROMPT HANDLE: " + _focusedPromptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _focusedPromptHandle,
                InputInteractOption1);

            Log("PROMPT CONTROL ACTION SET: INPUT_INTERACT_OPTION1; hash=0x" +
                InputInteractOption1.ToString("X8"));

            IntPtr textResult =
                RDR2.Native.MISC.VAR_STRING<string>(
                    VarStringType,
                    VarStringFormat,
                    PromptText);

            Log("VAR_STRING RESULT: type=" +
                textResult.GetType().FullName +
                "; value=" + textResult);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _focusedPromptHandle,
                textResult);

            Log("PROMPT TEXT SET: literal=\"" + PromptText + "\"");

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _focusedPromptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _focusedPromptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                false);

            Log("FOCUSED PROMPT CREATED: prompt=" +
                _focusedPromptHandle +
                "; text=\"" + PromptText +
                "\"; initialEnabled=false; initialVisible=false");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt creation failed: " +
                GetInnermostMessage(exception));

            DeleteFocusedPrompt();
        }
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

        try
        {
            int groupId = Convert.ToInt32(
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle),
                CultureInfo.InvariantCulture);

            if (groupId == 0)
            {
                LogPromptErrorOnce(
                    "Target prompt-group native returned an invalid value: " +
                    groupId);
                return;
            }

            _focusedPromptGroupId = groupId;

            Log("TARGET PROMPT GROUP: target=" + targetHandle +
                "; group=" + _focusedPromptGroupId);

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _focusedPromptHandle,
                _focusedPromptGroupId,
                0);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                true);

            _lastProbeValues.Clear();
            _currentProbeValues.Clear();
            _lastInputHeartbeatUtc = DateTime.MinValue;

            Log("PROMPT ATTACHED TO TARGET GROUP: prompt=" +
                _focusedPromptHandle +
                "; target=" + targetHandle +
                "; group=" + _focusedPromptGroupId);

            Log("FOCUSED PROMPT ENABLED: prompt=" +
                _focusedPromptHandle);

            RDR2.UI.Screen.PrintSubtitle(
                "Recruit prompt attached. Check for G Recruit.");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt attachment failed: " +
                GetInnermostMessage(exception));
            _focusedPromptGroupId = 0;
        }
    }

    private void ActivateAmbientPromptGroupThisFrame()
    {
        if (_trackedTargetHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD
                ._UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME(
                    _trackedTargetHandle,
                    AmbientGroupRadius,
                    AmbientGroupParameter2,
                    AmbientGroupTabCount,
                    (uint)_focusedPromptGroupId,
                    PromptGroupName,
                    AmbientGroupFlags);

            if (!_ambientActivationLoggedForTarget)
            {
                _ambientActivationLoggedForTarget = true;

                Log("AMBIENT GROUP ACTIVATION INVOKED: target=" +
                    _trackedTargetHandle +
                    "; radius=" + AmbientGroupRadius +
                    "; p2=" + AmbientGroupParameter2 +
                    "; tabCount=" + AmbientGroupTabCount +
                    "; group=" + _focusedPromptGroupId +
                    "; groupName=\"" + PromptGroupName +
                    "\"; flags=" + AmbientGroupFlags);
            }
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "ambient_group_activation",
                "AMBIENT GROUP ACTIVATION ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void SampleInputProbes()
    {
        SampleProbe(
            "UI_PROMPT_IS_ACTIVE",
            RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(
                _focusedPromptHandle),
            false);

        SampleProbe(
            "UI_PROMPT_IS_PRESSED",
            RDR2.Native.HUD._UI_PROMPT_IS_PRESSED(
                _focusedPromptHandle),
            false);

        SampleProbe(
            "UI_PROMPT_IS_JUST_PRESSED",
            RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                _focusedPromptHandle),
            true);

        SampleProbe(
            "IS_CONTROL_ENABLED",
            RDR2.Native.PAD.IS_CONTROL_ENABLED(
                ControlPadIndex,
                InputInteractOption1),
            false);

        SampleProbe(
            "IS_CONTROL_PRESSED",
            RDR2.Native.PAD.IS_CONTROL_PRESSED(
                ControlPadIndex,
                InputInteractOption1),
            false);

        SampleProbe(
            "IS_CONTROL_JUST_PRESSED",
            RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                ControlPadIndex,
                InputInteractOption1),
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
        bool value,
        bool isPulse)
    {
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
        bool value;

        if (_currentProbeValues.TryGetValue(nativeName, out value))
        {
            return value.ToString();
        }

        return "not-sampled";
    }

    private void DetachFocusedPrompt()
    {
        _ambientActivationLoggedForTarget = false;

        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                false);

            if (_focusedPromptGroupId != 0)
            {
                RDR2.Native.HUD._UI_PROMPT_REMOVE_GROUP(
                    _focusedPromptHandle,
                    (ulong)_focusedPromptGroupId);

                Log("PROMPT GROUP DETACH: prompt=" +
                    _focusedPromptHandle +
                    "; group=" + _focusedPromptGroupId);
            }

            Log("FOCUSED PROMPT DISABLED: prompt=" +
                _focusedPromptHandle);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "prompt_detach",
                "PROMPT DETACH ERROR: " +
                GetInnermostMessage(exception));
        }

        _focusedPromptGroupId = 0;
        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
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

        try
        {
            DetachFocusedPrompt();

            RDR2.Native.HUD._UI_PROMPT_DELETE(
                _focusedPromptHandle);

            Log("FOCUSED PROMPT DELETE: prompt=" +
                _focusedPromptHandle);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "prompt_delete",
                "PROMPT DELETE ERROR: " +
                GetInnermostMessage(exception));
        }

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

    private void LogPromptErrorOnce(string message)
    {
        LogErrorOnce("prompt_error", "PROMPT ERROR: " + message);
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