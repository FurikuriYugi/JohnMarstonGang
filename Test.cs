using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int PromptGroupTab = 0;
    private const int PromptAttribute = 0x12;
    private const string PromptText = "Recruit";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _targetHandle;
    private int _targetGroupId = -1;

    private bool _promptShown;
    private bool _hasLoggedActiveState;
    private bool _lastActiveState;

    private string _lastTargetError;
    private string _lastGroupError;
    private string _lastPromptError;
    private string _lastInputError;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Dog Companion-style G Recruit prompt test ===");
        Log("Text source: VAR_STRING(10, \"LITERAL_STRING\", \"Recruit\").");
        Log("Control: INPUT_INTERACT_OPTION1; hash=0x760A9C6F; expected key: G.");
        Log("Prompt is assigned to the focused human target's prompt group.");
        Log("No ILO registration call; no recruitment is performed.");

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
                Log("PROMPT CREATION FAILED: handle=" + _promptHandle);
                return;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _promptHandle,
                InputInteractOption1);

            string text = RDR2.Native.MISC.VAR_STRING(
                10,
                "LITERAL_STRING",
                PromptText);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                text);

            // Match Dog Companion's prompt-construction sequence.
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                0,
                PromptGroupTab);

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _promptHandle,
                true);

            // Expected wrapper signature: (prompt, attribute, bool).
            RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(
                _promptHandle,
                PromptAttribute,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            SetPromptShown(false);

            Log(
                "PROMPT CREATED: handle=" +
                _promptHandle +
                "; text=" +
                PromptText +
                "; mode=standard; attribute=0x12:1");
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
        bool targetFound;
        bool targetIsHuman;

        if (!TryGetFocusedHuman(
                out targetFound,
                out targetHandle,
                out targetIsHuman))
        {
            HideAndDetachPrompt();
            LogRawGPress();
            return;
        }

        if (!targetFound || targetHandle <= 0 || !targetIsHuman)
        {
            HideAndDetachPrompt();
            LogRawGPress();
            return;
        }

        int groupId;
        if (!TryGetTargetPromptGroup(targetHandle, out groupId))
        {
            HideAndDetachPrompt();
            LogRawGPress();
            return;
        }

        if (_targetHandle != targetHandle || _targetGroupId != groupId)
        {
            try
            {
                RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                    _promptHandle,
                    groupId,
                    PromptGroupTab);

                _targetHandle = targetHandle;
                _targetGroupId = groupId;

                Log(
                    "PROMPT GROUP ASSIGNED: target=" +
                    targetHandle +
                    "; group=" +
                    groupId +
                    "; tab=" +
                    PromptGroupTab);
            }
            catch (Exception exception)
            {
                LogErrorOnce(
                    ref _lastPromptError,
                    "PROMPT GROUP ASSIGNMENT ERROR: " +
                    InnermostMessage(exception));

                HideAndDetachPrompt();
                LogRawGPress();
                return;
            }
        }

        SetPromptShown(true);
        LogPromptState();
        LogRawGPress();

        try
        {
            if (_promptHandle > 0 &&
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(_promptHandle) &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(_promptHandle))
            {
                Log("G RECRUIT PROMPT PRESSED: no recruitment performed.");
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT STATE ERROR: " +
                InnermostMessage(exception));
        }
    }

    private bool TryGetFocusedHuman(
        out bool targetFound,
        out int targetHandle,
        out bool targetIsHuman)
    {
        targetFound = false;
        targetHandle = 0;
        targetIsHuman = false;

        try
        {
            int focusedTargetHandle = 0;

            unsafe
            {
                targetFound =
                    RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
                        Game.Player.Handle,
                        &focusedTargetHandle,
                        true,
                        true);
            }

            if (!targetFound || focusedTargetHandle <= 0)
            {
                _lastTargetError = null;
                return true;
            }

            targetHandle = focusedTargetHandle;
            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            _lastTargetError = null;
            return true;
        }
        catch (Exception exception)
        {
            targetFound = false;
            targetHandle = 0;
            targetIsHuman = false;

            LogErrorOnce(
                ref _lastTargetError,
                "TARGET LOOKUP ERROR: " +
                InnermostMessage(exception));

            return false;
        }
    }

    private bool TryGetTargetPromptGroup(
        int targetHandle,
        out int groupId)
    {
        groupId = -1;

        try
        {
            groupId =
                RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                    targetHandle);

            if (groupId < 0)
            {
                LogErrorOnce(
                    ref _lastGroupError,
                    "GROUP LOOKUP RETURNED INVALID VALUE: " +
                    groupId);

                return false;
            }

            _lastGroupError = null;
            return true;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastGroupError,
                "GROUP LOOKUP ERROR: " +
                InnermostMessage(exception));

            return false;
        }
    }

    private void SetPromptShown(bool shown)
    {
        if (_promptHandle <= 0 || _promptShown == shown)
        {
            return;
        }

        try
        {
            if (shown)
            {
                // Match Dog Companion's order: visible, then enabled.
                RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                    _promptHandle,
                    true);

                RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                    _promptHandle,
                    true);
            }
            else
            {
                RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                    _promptHandle,
                    false);

                RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                    _promptHandle,
                    false);
            }

            _promptShown = shown;
            Log("PROMPT SHOWN/ENABLED STATE: " + shown);
            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT VISIBILITY/ENABLE ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void HideAndDetachPrompt()
    {
        SetPromptShown(false);

        if (_promptHandle <= 0 ||
            (_targetHandle == 0 && _targetGroupId < 0))
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                0,
                PromptGroupTab);

            Log(
                "PROMPT GROUP CLEARED: previousTarget=" +
                _targetHandle +
                "; previousGroup=" +
                _targetGroupId);

            _targetHandle = 0;
            _targetGroupId = -1;
            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT GROUP CLEAR ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogPromptState()
    {
        try
        {
            bool active =
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(_promptHandle);

            if (!_hasLoggedActiveState || active != _lastActiveState)
            {
                _hasLoggedActiveState = true;
                _lastActiveState = active;

                Log(
                    "PROMPT ACTIVE: text=" +
                    PromptText +
                    "; active=" +
                    active);
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT ACTIVE CHECK ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogRawGPress()
    {
        try
        {
            if (RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                    0,
                    InputInteractOption1))
            {
                Log("RAW G / INPUT_INTERACT_OPTION1 PRESS DETECTED");
            }

            _lastInputError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastInputError,
                "INPUT READ ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void OnAborted(object sender, EventArgs e)
    {
        HideAndDetachPrompt();

        if (_promptHandle <= 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_DELETE(_promptHandle);
            Log("PROMPT DELETED: " + PromptText);
            _promptHandle = 0;
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT CLEANUP ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogErrorOnce(ref string lastError, string message)
    {
        if (lastError == message)
        {
            return;
        }

        lastError = message;
        Log(message);
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