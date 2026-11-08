using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;
using System.IO;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;

    private const int TargetScanIntervalMilliseconds = 250;
    private const int InputHeartbeatIntervalMilliseconds = 2000;
    private const float TargetMaximumDistance = 8.0f;

    // Dedicated test-only prompt group. This is deliberately not the NPC's
    // ambient prompt-group ID.
    private const int PromptGroupId = 0x4A4D47;

    private const int ActiveGroupTabCount = 0;
    private const int ActiveGroupDefaultTab = 0;
    private const int ActiveGroupParameter4 = 0;
    private const int ActiveGroupPromptParameter = 0;

    private const string PromptText = "Recruit";
    private const string PromptGroupName = "NPC";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly Dictionary<string, bool> _lastProbeValues =
        new Dictionary<string, bool>();

    private int _focusedTargetHandle;
    private int _promptHandle;
    private bool _promptAttached;
    private bool _generalErrorLogged;
    private bool _promptErrorLogged;
    private bool _groupActivationErrorLogged;
    private DateTime _lastTargetScanUtc;
    private DateTime _lastInputHeartbeatUtc;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Dedicated active-group Recruit prompt test initialized ===");
        Log("Prompt text: \"" + PromptText + "\".");
        Log("Prompt group name: \"" + PromptGroupName + "\".");
        Log("Dedicated prompt group ID: " + PromptGroupId);
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("Active prompt group is activated every focused tick.");
        Log("This test does not recruit, change gang membership, or trigger an animation.");

        CreatePrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC. Check for G Recruit, then tap G once.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            DateTime now = DateTime.UtcNow;
            if ((now - _lastTargetScanUtc).TotalMilliseconds >=
                TargetScanIntervalMilliseconds)
            {
                _lastTargetScanUtc = now;
                UpdateFocusedTarget();
            }

            if (_focusedTargetHandle <= 0 || !_promptAttached)
            {
                return;
            }

            ActivatePromptGroupThisFrame();
            SampleInput();
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

    private void UpdateFocusedTarget()
    {
        int targetHandle = GetCurrentHumanTargetHandle();

        if (targetHandle > 0 &&
            !IsWithinTargetDistance(targetHandle))
        {
            targetHandle = 0;
        }

        if (targetHandle == _focusedTargetHandle)
        {
            return;
        }

        if (_focusedTargetHandle > 0)
        {
            Log("FOCUSED TARGET END: target=" + _focusedTargetHandle);
            DetachPrompt();
        }

        _focusedTargetHandle = targetHandle;

        if (_focusedTargetHandle > 0)
        {
            Log("FOCUSED HUMAN TARGET BEGIN: target=" +
                _focusedTargetHandle);
            AttachPrompt();
        }
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
            if (aimedEntity != null &&
                aimedEntity.Exists() &&
                IsHumanEntity(aimedEntity.Handle))
            {
                return aimedEntity.Handle;
            }
        }
        catch (Exception exception)
        {
            Log("TARGET RESOLUTION ERROR: " +
                GetInnermostMessage(exception));
        }

        return 0;
    }

    private bool IsWithinTargetDistance(int targetHandle)
    {
        try
        {
            if (targetHandle <= 0 ||
                !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(targetHandle))
            {
                return false;
            }

            Ped playerPed = Game.Player.Character;
            if (playerPed == null || !playerPed.Exists())
            {
                return false;
            }

            Vector3 targetPosition =
                RDR2.Native.ENTITY.GET_ENTITY_COORDS(
                    targetHandle,
                    true,
                    false);

            return targetPosition.DistanceTo(playerPed.Position) <=
                TargetMaximumDistance;
        }
        catch (Exception exception)
        {
            Log("TARGET DISTANCE ERROR: " +
                GetInnermostMessage(exception));
            return false;
        }
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

    private void CreatePrompt()
    {
        try
        {
            _promptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_promptHandle <= 0)
            {
                LogPromptErrorOnce(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid handle: " +
                    _promptHandle);
                _promptHandle = 0;
                return;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _promptHandle,
                InputInteractOption1);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                PromptText);

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                false);

            Log("PROMPT CREATED: prompt=" + _promptHandle +
                "; text=\"" + PromptText + "\"");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt creation failed: " +
                GetInnermostMessage(exception));
            DeletePrompt();
        }
    }

    private void AttachPrompt()
    {
        if (_promptHandle <= 0)
        {
            LogPromptErrorOnce(
                "Cannot attach prompt because creation failed.");
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                PromptGroupId,
                0);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                true);

            _promptAttached = true;
            _lastProbeValues.Clear();
            _lastInputHeartbeatUtc = DateTime.MinValue;

            Log("PROMPT ATTACHED TO DEDICATED GROUP: prompt=" +
                _promptHandle +
                "; target=" + _focusedTargetHandle +
                "; group=" + PromptGroupId);

            RDR2.UI.Screen.PrintSubtitle(
                "Recruit prompt test active. Check for G Recruit.");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt attachment failed: " +
                GetInnermostMessage(exception));
            _promptAttached = false;
        }
    }

    private void ActivatePromptGroupThisFrame()
    {
        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME(
                PromptGroupId,
                PromptGroupName,
                ActiveGroupTabCount,
                ActiveGroupDefaultTab,
                ActiveGroupParameter4,
                ActiveGroupPromptParameter);
        }
        catch (Exception exception)
        {
            if (!_groupActivationErrorLogged)
            {
                _groupActivationErrorLogged = true;
                Log("ACTIVE GROUP ACTIVATION ERROR: " +
                    GetInnermostMessage(exception));
            }
        }
    }

    private void SampleInput()
    {
        bool promptActive =
            RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(_promptHandle);

        bool promptPressed =
            RDR2.Native.HUD._UI_PROMPT_IS_PRESSED(_promptHandle);

        bool controlEnabled =
            RDR2.Native.PAD.IS_CONTROL_ENABLED(
                ControlPadIndex,
                InputInteractOption1);

        bool controlPressed =
            RDR2.Native.PAD.IS_CONTROL_PRESSED(
                ControlPadIndex,
                InputInteractOption1);

        bool controlJustPressed =
            RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                ControlPadIndex,
                InputInteractOption1);

        LogStateChange("UI_PROMPT_IS_ACTIVE", promptActive);
        LogStateChange("UI_PROMPT_IS_PRESSED", promptPressed);
        LogStateChange("IS_CONTROL_ENABLED", controlEnabled);
        LogStateChange("IS_CONTROL_PRESSED", controlPressed);

        if (controlJustPressed)
        {
            Log("INPUT EVENT: IS_CONTROL_JUST_PRESSED=true; target=" +
                _focusedTargetHandle +
                "; prompt=" + _promptHandle);
        }

        if (RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(_promptHandle))
        {
            Log("INPUT EVENT: UI_PROMPT_IS_JUST_PRESSED=true; target=" +
                _focusedTargetHandle +
                "; prompt=" + _promptHandle);
        }

        DateTime now = DateTime.UtcNow;
        if (_lastInputHeartbeatUtc == DateTime.MinValue ||
            (now - _lastInputHeartbeatUtc).TotalMilliseconds >=
                InputHeartbeatIntervalMilliseconds)
        {
            _lastInputHeartbeatUtc = now;

            Log("INPUT HEARTBEAT: promptActive=" + promptActive +
                "; promptPressed=" + promptPressed +
                "; controlEnabled=" + controlEnabled +
                "; controlPressed=" + controlPressed +
                "; controlJustPressed=" + controlJustPressed);
        }
    }

    private void LogStateChange(string name, bool value)
    {
        bool previousValue;
        if (!_lastProbeValues.TryGetValue(name, out previousValue) ||
            previousValue != value)
        {
            _lastProbeValues[name] = value;
            Log("INPUT STATE: " + name + "=" + value +
                "; target=" + _focusedTargetHandle +
                "; prompt=" + _promptHandle);
        }
    }

    private void DetachPrompt()
    {
        if (_promptHandle <= 0)
        {
            _promptAttached = false;
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_REMOVE_GROUP(
                _promptHandle,
                (ulong)PromptGroupId);

            Log("PROMPT DETACHED AND DISABLED: prompt=" +
                _promptHandle +
                "; group=" + PromptGroupId);
        }
        catch (Exception exception)
        {
            Log("PROMPT DETACH ERROR: " +
                GetInnermostMessage(exception));
        }

        _promptAttached = false;
        _lastProbeValues.Clear();
    }

    private void DeletePrompt()
    {
        if (_promptHandle <= 0)
        {
            return;
        }

        try
        {
            DetachPrompt();
            RDR2.Native.HUD._UI_PROMPT_DELETE(_promptHandle);
            Log("PROMPT DELETED: prompt=" + _promptHandle);
        }
        catch (Exception exception)
        {
            Log("PROMPT DELETE ERROR: " +
                GetInnermostMessage(exception));
        }

        _promptHandle = 0;
        _promptAttached = false;
    }

    private void OnAborted(object sender, EventArgs e)
    {
        try
        {
            DetachPrompt();
            DeletePrompt();
        }
        catch (Exception exception)
        {
            Log("ABORT CLEANUP ERROR: " +
                GetInnermostMessage(exception));
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

    private string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }

    private void Log(string message)
    {
        string line =
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
            " " + message + Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }
}