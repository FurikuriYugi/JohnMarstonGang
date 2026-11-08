using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const string PromptText = "Recruit";
    private const int PromptGroupTab = 0;
    private const int HeartbeatMilliseconds = 2000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _lastTargetHandle = int.MinValue;
    private bool _lastTargetIsHuman;
    private int _lastAssignedGroupId = int.MinValue;
    private int _lastGroupTargetHandle = int.MinValue;
    private DateTime _lastHeartbeatUtc;
    private bool _lastPromptActive;
    private bool _hasLoggedPromptState;
    private string _lastTargetError;
    private string _lastGroupError;
    private string _lastInputError;
    private string _lastPromptError;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused-target Recruit prompt diagnostic ===");
        Log("Focused target native: PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY");
        Log("Prompt text: " + PromptText);
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("Prompt is assigned to the focused entity's prompt group.");
        Log("This test does not recruit or change gang membership.");

        CreatePrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC; check for the G Recruit prompt.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void CreatePrompt()
    {
        try
        {
            _promptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_promptHandle <= 0)
            {
                Log("ERROR: UI_PROMPT_REGISTER_BEGIN returned " +
                    _promptHandle);
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

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(_promptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                false);

            Log("UI PROMPT CREATED: handle=" + _promptHandle);
        }
        catch (Exception exception)
        {
            Log("UI PROMPT CREATION ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        bool rawControlJustPressed = ReadControlPress();
        
        int targetHandle = 0;
        bool targetIsHuman = false;

        bool hasFocusedTarget = TryGetFocusedTarget(
            targetHandle,
            out targetIsHuman);

        LogTargetStateIfChanged(
            targetHandle,
            targetIsHuman);


        if (rawControlJustPressed)
        {
            Log("RAW INPUT_INTERACT_OPTION1 PRESS DETECTED; no action taken");
        }

        bool eligibleTarget =
            hasFocusedTarget &&
            targetIsHuman;

        int targetGroupId = -1;
        bool groupAssignedForCurrentTarget = false;

        if (_promptHandle > 0 && eligibleTarget)
        {
            TryAssignPromptToTargetGroup(
                targetHandle,
                out targetGroupId,
                out groupAssignedForCurrentTarget);
        }

        UpdateRecruitPrompt(
            eligibleTarget,
            targetGroupId,
            groupAssignedForCurrentTarget);

        LogHeartbeatIfDue(
            targetHandle,
            targetIsHuman,
            hasFocusedTarget,
            eligibleTarget,
            targetGroupId,
            groupAssignedForCurrentTarget,
            rawControlJustPressed);
    }

    private bool ReadControlPress()
    {
        try
        {
            bool pressed =
                RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                    0,
                    InputInteractOption1);

            _lastInputError = null;
            return pressed;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastInputError,
                "INPUT READ ERROR: " + InnermostMessage(exception));

            return false;
        }
    }

    private bool TryGetFocusedTarget(
        int targetHandle,
        out bool targetIsHuman)
    {
        targetHandle = 0;
        targetIsHuman = false;

        try
        {
            unsafe
            {
                bool nativeFoundTarget =
                    RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
                        Game.Player.Handle,
                        &targetHandle,
                        true,
                        true);


                if (!nativeFoundTarget || targetHandle <= 0)
                {
                    targetHandle = 0;
                    _lastTargetError = null;
                    return false;
                }

                targetIsHuman =
                    RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

                _lastTargetError = null;
                return true;
            }
        }
        catch (Exception exception)
        {
            targetHandle = 0;
            targetIsHuman = false;

            LogErrorOnce(
                ref _lastTargetError,
                "INTERACTION TARGET NATIVE ERROR: " +
                InnermostMessage(exception));

            return false;
        }
    }

    private void TryAssignPromptToTargetGroup(
        int targetHandle,
        out int targetGroupId,
        out bool groupAssignedForCurrentTarget)
    {
        targetGroupId = -1;
        groupAssignedForCurrentTarget = false;

        try
        {
            targetGroupId =
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle);

            _lastGroupError = null;

            if (targetGroupId < 0)
            {
                LogErrorOnce(
                    ref _lastGroupError,
                    "GROUP LOOKUP RETURNED INVALID VALUE: " +
                    targetGroupId);

                return;
            }

            if (_lastGroupTargetHandle != targetHandle ||
                _lastAssignedGroupId != targetGroupId)
            {
                RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                    _promptHandle,
                    targetGroupId,
                    PromptGroupTab);

                _lastGroupTargetHandle = targetHandle;
                _lastAssignedGroupId = targetGroupId;

                Log(
                    "UI PROMPT ASSIGNED TO FOCUSED ENTITY GROUP: target=" +
                    targetHandle +
                    "; group=" +
                    targetGroupId);
            }

            groupAssignedForCurrentTarget =
                _lastGroupTargetHandle == targetHandle &&
                _lastAssignedGroupId == targetGroupId;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastGroupError,
                "GROUP LOOKUP/ASSIGNMENT ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void UpdateRecruitPrompt(
        bool eligibleTarget,
        int targetGroupId,
        bool groupAssignedForCurrentTarget)
    {
        if (_promptHandle <= 0)
        {
            return;
        }

        bool showPrompt =
            eligibleTarget &&
            targetGroupId >= 0 &&
            groupAssignedForCurrentTarget;

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                showPrompt);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                showPrompt);

            bool promptActive =
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(
                    _promptHandle);

            bool promptJustPressed =
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    _promptHandle);

            if (!_hasLoggedPromptState ||
                promptActive != _lastPromptActive)
            {
                _hasLoggedPromptState = true;
                _lastPromptActive = promptActive;

                Log("UI PROMPT STATE: active=" + promptActive);
            }

            if (promptJustPressed)
            {
                Log("RECRUIT PROMPT PRESS DETECTED; no action taken");
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "UI PROMPT UPDATE ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogTargetStateIfChanged(
        int targetHandle,
        bool targetIsHuman)
    {
        if (targetHandle == _lastTargetHandle &&
            targetIsHuman == _lastTargetIsHuman)
        {
            return;
        }

        _lastTargetHandle = targetHandle;
        _lastTargetIsHuman = targetIsHuman;

        Log(
            "TARGET STATE: focusedTarget=" +
            FormatTargetHandle(targetHandle) +
            "; human=" +
            targetIsHuman);
    }

    private void LogHeartbeatIfDue(
        int targetHandle,
        bool targetIsHuman,
        bool hasFocusedTarget,
        bool eligibleTarget,
        int targetGroupId,
        bool groupAssignedForCurrentTarget,
        bool rawControlJustPressed)
    {
        DateTime now = DateTime.UtcNow;

        if (_lastHeartbeatUtc != DateTime.MinValue &&
            (now - _lastHeartbeatUtc).TotalMilliseconds <
            HeartbeatMilliseconds)
        {
            return;
        }

        _lastHeartbeatUtc = now;

        Log(
            "HEARTBEAT: focusedTarget=" +
            FormatTargetHandle(targetHandle) +
            "; nativeFoundTarget=" +
            hasFocusedTarget +
            "; human=" +
            targetIsHuman +
            "; eligible=" +
            eligibleTarget +
            "; group=" +
            targetGroupId +
            "; groupAssigned=" +
            groupAssignedForCurrentTarget +
            "; uiPromptActive=" +
            _lastPromptActive +
            "; rawInputInteractOption1=" +
            rawControlJustPressed);
    }

    private string FormatTargetHandle(int targetHandle)
    {
        return targetHandle > 0
            ? targetHandle.ToString()
            : "none";
    }

    private void LogErrorOnce(
        ref string lastError,
        string message)
    {
        if (lastError == message)
        {
            return;
        }

        lastError = message;
        Log(message);
    }

    private void OnAborted(object sender, EventArgs e)
    {
        if (_promptHandle <= 0)
        {
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

            RDR2.Native.HUD._UI_PROMPT_DELETE(_promptHandle);

            Log("UI PROMPT DELETED");
            _promptHandle = 0;
        }
        catch (Exception exception)
        {
            Log("UI PROMPT CLEANUP ERROR: " +
                InnermostMessage(exception));
        }
    }

    private string InnermostMessage(Exception exception)
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
        File.AppendAllText(
            _logPath,
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture) +
            " " +
            message +
            Environment.NewLine);
    }
}