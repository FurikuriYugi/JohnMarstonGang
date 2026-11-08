using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const string PromptText = "Recruit";
    private const int PromptGroupTab = 0;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _lastTargetHandle = int.MinValue;
    private bool _lastTargetIsHuman;
    private int _lastAssignedGroupId = int.MinValue;
    private int _lastGroupTargetHandle = int.MinValue;
    private bool _lastPromptActive;
    private bool _hasLoggedPromptState;

    private string _lastTargetError;
    private string _lastGroupError;
    private string _lastPromptError;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused-target Recruit prompt test ===");
        Log("Target native: PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY");
        Log("Prompt text: " + PromptText);
        Log(
            "Prompt control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("No recruitment or gang membership changes are performed.");

        CreatePrompt();

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
                Log(
                    "ERROR: UI_PROMPT_REGISTER_BEGIN returned " +
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

            Log("PROMPT CREATED: handle=" + _promptHandle);
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT CREATION ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        int targetHandle;
        bool targetIsHuman;

        bool hasFocusedTarget = TryGetFocusedTarget(
            out targetHandle,
            out targetIsHuman);

        LogTargetStateIfChanged(
            targetHandle,
            targetIsHuman);

        int targetGroupId = -1;
        bool groupAssignedForCurrentTarget = false;

        if (_promptHandle > 0 &&
            hasFocusedTarget &&
            targetIsHuman)
        {
            TryAssignPromptToTargetGroup(
                targetHandle,
                out targetGroupId,
                out groupAssignedForCurrentTarget);
        }

        bool showPrompt =
            hasFocusedTarget &&
            targetIsHuman &&
            targetGroupId >= 0 &&
            groupAssignedForCurrentTarget;

        UpdatePrompt(showPrompt);
    }

    private bool TryGetFocusedTarget(
        out int targetHandle,
        out bool targetIsHuman)
    {
        targetHandle = 0;
        targetIsHuman = false;

        try
        {
            int focusedTargetHandle = 0;
            bool nativeFoundTarget;

            unsafe
            {
                nativeFoundTarget =
                    RDR2.Native.PLAYER
                        .GET_PLAYER_INTERACTION_TARGET_ENTITY(
                            Game.Player.Handle,
                            &focusedTargetHandle,
                            true,
                            true);
            }

            if (!nativeFoundTarget || focusedTargetHandle <= 0)
            {
                _lastTargetError = null;
                return false;
            }

            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(focusedTargetHandle);

            targetHandle = focusedTargetHandle;
            _lastTargetError = null;
            return true;
        }
        catch (Exception exception)
        {
            targetHandle = 0;
            targetIsHuman = false;

            LogErrorOnce(
                ref _lastTargetError,
                "TARGET LOOKUP ERROR: " +
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

            if (targetGroupId < 0)
            {
                LogErrorOnce(
                    ref _lastGroupError,
                    "GROUP LOOKUP RETURNED INVALID VALUE: " +
                    targetGroupId);
                return;
            }

            _lastGroupError = null;

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
                    "PROMPT GROUP ASSIGNED: target=" +
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

    private void UpdatePrompt(bool showPrompt)
    {
        if (_promptHandle <= 0)
        {
            return;
        }

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

            if (!_hasLoggedPromptState ||
                promptActive != _lastPromptActive)
            {
                _hasLoggedPromptState = true;
                _lastPromptActive = promptActive;

                Log("PROMPT ACTIVE: " + promptActive);
            }

            if (promptActive &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    _promptHandle))
            {
                Log("RECRUIT PROMPT PRESSED; no recruitment performed.");
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT UPDATE ERROR: " +
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
            "TARGET: handle=" +
            (targetHandle > 0 ? targetHandle.ToString() : "none") +
            "; human=" +
            targetIsHuman);
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

            Log("PROMPT DELETED");
            _promptHandle = 0;
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT CLEANUP ERROR: " +
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