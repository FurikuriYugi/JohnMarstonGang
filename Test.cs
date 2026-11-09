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

    private bool _promptStateApplied;
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

        Log("=== Dog Companion standard-prompt sequence: G Recruit ===");
        Log("Prompt label: " + PromptText);
        Log("Control: INPUT_INTERACT_OPTION1; hash=0x760A9C6F.");
        Log("No ILO registration; no ped prompt-name change; no outer-radius label.");
        Log("Display-only test: pressing G does not recruit or change gang membership.");

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

            // Dog Companion initializes the prompt in group (0, 0).
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                0,
                PromptGroupTab);

            // Dog Companion's factory selects standard mode.
            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(
                _promptHandle,
                PromptAttribute,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            // Explicitly start hidden and disabled.
            SetPromptShown(false, true);

            Log(
                "PROMPT CREATED: handle=" +
                _promptHandle +
                "; standard mode; attribute=0x12:true");
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
        if (_promptHandle <= 0)
        {
            LogRawGPress();
            return;
        }

        bool targetFound;
        bool targetIsHuman;
        int targetHandle;

        if (!TryGetFocusedHuman(
            out targetFound,
            out targetHandle,
            out targetIsHuman))
        {
            ClearCurrentTarget();
            LogRawGPress();
            return;
        }

        if (!targetFound || targetHandle <= 0 || !targetIsHuman)
        {
            ClearCurrentTarget();
            LogRawGPress();
            return;
        }

        int groupId;
        if (!TryGetTargetPromptGroup(targetHandle, out groupId))
        {
            ClearCurrentTarget();
            LogRawGPress();
            return;
        }

        bool targetOrGroupChanged =
            _targetHandle != targetHandle ||
            _targetGroupId != groupId;

        if (targetOrGroupChanged)
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

                ClearCurrentTarget();
                LogRawGPress();
                return;
            }
        }

        // Apply visible, then enabled, after assigning the target group.
        SetPromptShown(true, targetOrGroupChanged);

        LogPromptState();
        LogPromptPress();
        LogRawGPress();
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
                    "GROUP LOOKUP INVALID: target=" +
                    targetHandle +
                    "; group=" +
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

    private void SetPromptShown(bool shown, bool forceApply)
    {
        if (_promptHandle <= 0)
        {
            return;
        }

        if (!forceApply &&
            _promptStateApplied &&
            _promptShown == shown)
        {
            return;
        }

        try
        {
            if (shown)
            {
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
            _promptStateApplied = true;
            _lastPromptError = null;

            Log("PROMPT VISIBLE-THEN-ENABLED STATE: " + shown);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT VISIBILITY/ENABLE ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void ClearCurrentTarget()
    {
        SetPromptShown(false, false);

        if (_promptHandle > 0 &&
            (_targetHandle != 0 || _targetGroupId >= 0))
        {
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
            }
            catch (Exception exception)
            {
                LogErrorOnce(
                    ref _lastPromptError,
                    "PROMPT GROUP CLEAR ERROR: " +
                    InnermostMessage(exception));
            }
        }

        _targetHandle = 0;
        _targetGroupId = -1;
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
                    "PROMPT ACTIVE: target=" +
                    _targetHandle +
                    "; group=" +
                    _targetGroupId +
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

    private void LogPromptPress()
    {
        try
        {
            if (RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(_promptHandle) &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(_promptHandle))
            {
                Log("G RECRUIT PROMPT PRESSED; no recruitment performed.");
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "PROMPT PRESS CHECK ERROR: " +
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
                Log("RAW INPUT_INTERACT_OPTION1 PRESS DETECTED");
            }

            _lastInputError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastInputError,
                "RAW INPUT CHECK ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void OnAborted(object sender, EventArgs e)
    {
        ClearCurrentTarget();

        if (_promptHandle <= 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_DELETE(_promptHandle);
            Log("PROMPT DELETED: " + _promptHandle);
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