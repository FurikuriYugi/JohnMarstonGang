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
    private bool _hasLoggedActiveState;
    private bool _lastActiveState;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focus-triggered Dog Companion sequence: G Recruit ===");
        Log("Prompt label: " + PromptText);
        Log("Control: INPUT_INTERACT_OPTION1; hash=0x760A9C6F.");
        Log("No ILO registration; no ped prompt-name change; no outer-radius label.");
        Log("Display-only test: pressing G does not recruit or change gang membership.");
        Log("Prompt will be created only after a focused human and target group are found.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        bool targetFound;
        bool targetIsHuman;
        int targetHandle;

        if (!TryGetFocusedHuman(
                out targetFound,
                out targetHandle,
                out targetIsHuman) ||
            !targetFound ||
            targetHandle <= 0 ||
            !targetIsHuman)
        {
            DeleteCurrentPrompt();
            LogRawGPress();
            return;
        }

        int groupId;
        if (!TryGetTargetPromptGroup(targetHandle, out groupId))
        {
            DeleteCurrentPrompt();
            LogRawGPress();
            return;
        }

        bool targetChanged =
            _promptHandle <= 0 ||
            _targetHandle != targetHandle ||
            _targetGroupId != groupId;

        if (targetChanged)
        {
            DeleteCurrentPrompt();
            CreatePromptForTarget(targetHandle, groupId);
        }

        if (_promptHandle > 0)
        {
            LogPromptState();
            LogPromptPress();
        }

        LogRawGPress();
    }

    private void CreatePromptForTarget(int targetHandle, int groupId)
    {
        try
        {
            int promptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (promptHandle <= 0)
            {
                Log("PROMPT CREATION FAILED: handle=" + promptHandle);
                return;
            }

            _promptHandle = promptHandle;

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                promptHandle,
                InputInteractOption1);

            string text = RDR2.Native.MISC.VAR_STRING(
                10,
                "LITERAL_STRING",
                PromptText);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                promptHandle,
                text);

            // Dog Companion's factory initializes group (0, 0).
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                promptHandle,
                0,
                PromptGroupTab);

            // Dog Companion calls FUN_180014510 with mode 0 (standard).
            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(
                promptHandle,
                PromptAttribute,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                promptHandle);

            Log(
                "PROMPT CREATED FOR FOCUSED TARGET: handle=" +
                promptHandle +
                "; target=" +
                targetHandle +
                "; group=" +
                groupId +
                "; standard mode; attribute=0x12:true");

            // Dog Companion assigns the entity's group after register-end.
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                promptHandle,
                groupId,
                PromptGroupTab);

            _targetHandle = targetHandle;
            _targetGroupId = groupId;
            _hasLoggedActiveState = false;

            Log(
                "PROMPT GROUP ASSIGNED: target=" +
                targetHandle +
                "; group=" +
                groupId +
                "; tab=" +
                PromptGroupTab);

            // Match Dog Companion's show-then-enable sequence.
            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                promptHandle,
                true);

            Log("PROMPT VISIBLE-THEN-ENABLED STATE: True");
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT SETUP ERROR: " +
                InnermostMessage(exception));

            DeleteCurrentPrompt();
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
                return true;
            }

            targetHandle = focusedTargetHandle;
            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            return true;
        }
        catch (Exception exception)
        {
            Log("TARGET LOOKUP ERROR: " + InnermostMessage(exception));
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
                Log(
                    "GROUP LOOKUP INVALID: target=" +
                    targetHandle +
                    "; group=" +
                    groupId);

                return false;
            }

            return true;
        }
        catch (Exception exception)
        {
            Log("GROUP LOOKUP ERROR: " + InnermostMessage(exception));
            return false;
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
                    "PROMPT ACTIVE: target=" +
                    _targetHandle +
                    "; group=" +
                    _targetGroupId +
                    "; active=" +
                    active);
            }
        }
        catch (Exception exception)
        {
            Log("PROMPT ACTIVE CHECK ERROR: " + InnermostMessage(exception));
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
        }
        catch (Exception exception)
        {
            Log("PROMPT PRESS CHECK ERROR: " + InnermostMessage(exception));
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
        }
        catch (Exception exception)
        {
            Log("RAW INPUT CHECK ERROR: " + InnermostMessage(exception));
        }
    }

    private void DeleteCurrentPrompt()
    {
        if (_promptHandle <= 0)
        {
            _targetHandle = 0;
            _targetGroupId = -1;
            return;
        }

        int promptHandle = _promptHandle;

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                promptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                promptHandle,
                false);
        }
        catch (Exception exception)
        {
            Log("PROMPT HIDE ERROR: " + InnermostMessage(exception));
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                promptHandle,
                0,
                PromptGroupTab);
        }
        catch (Exception exception)
        {
            Log("PROMPT GROUP CLEAR ERROR: " + InnermostMessage(exception));
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_DELETE(promptHandle);
            Log("PROMPT DELETED: " + promptHandle);
        }
        catch (Exception exception)
        {
            Log("PROMPT DELETE ERROR: " + InnermostMessage(exception));
        }

        _promptHandle = 0;
        _targetHandle = 0;
        _targetGroupId = -1;
        _hasLoggedActiveState = false;
    }

    private void OnAborted(object sender, EventArgs e)
    {
        DeleteCurrentPrompt();
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