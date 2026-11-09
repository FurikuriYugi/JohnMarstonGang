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

    // Run once with true, then set false and repeat with the same test conditions.
    private const bool UsePromptHandleForIlo = false;

    // Controlled test values; keep identical between the true/false runs.
    private const float IloRadius = 3.0f;
    private const float IloFocusDistance = 3.0f;
    private const int IloFlag = 0;
    private const float IloP5 = 0.0f;
    private const float IloP6 = 0.0f;
    private const bool ShowIloApproachPrompt = false;
    private const int IloP9 = -1;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _targetHandle;
    private int _targetGroupId = -1;
    private bool _iloRegistrationAttempted;
    private bool _iloRegistered;
    private bool _hasLoggedActiveState;
    private bool _lastActiveState;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== G Recruit ILO prompt-argument test ===");
        Log("Prompt label: " + PromptText);
        Log("Control: INPUT_INTERACT_OPTION1 (G).");
        Log("UsePromptHandleForIlo: " + UsePromptHandleForIlo);
        Log(
            "ILO settings: radius=" + IloRadius +
            "; focusDistance=" + IloFocusDistance +
            "; flag=" + IloFlag +
            "; p5=" + IloP5 +
            "; p6=" + IloP6 +
            "; showApproachPrompt=" + ShowIloApproachPrompt +
            "; p9=" + IloP9);
        Log("Prompt-display test only; no recruitment or gang changes.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        bool targetFound;
        bool targetIsHuman;
        int targetHandle;

        bool lookupSucceeded = TryGetFocusedHuman(
            out targetFound,
            out targetHandle,
            out targetIsHuman);

        if (!lookupSucceeded ||
            !targetFound ||
            targetHandle <= 0 ||
            !targetIsHuman)
        {
            DeleteCurrentPrompt();
            return;
        }

        if (_promptHandle <= 0 || _targetHandle != targetHandle)
        {
            DeleteCurrentPrompt();
            CreatePromptForTarget(targetHandle);
        }

        if (_promptHandle > 0)
        {
            LogPromptState();
            LogPromptPress();
        }
    }

    private void CreatePromptForTarget(int targetHandle)
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
            _targetHandle = targetHandle;
            _targetGroupId = -1;
            _iloRegistrationAttempted = false;
            _iloRegistered = false;
            _hasLoggedActiveState = false;

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                promptHandle,
                InputInteractOption1);

            string textToSet = RDR2.Native.MISC.VAR_STRING(
                10,
                "LITERAL_STRING",
                PromptText);

            Log("VAR_STRING RESULT: " + textToSet);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                promptHandle,
                textToSet);

            Log("PROMPT TEXT SET: " + textToSet);

            // Match the Dog Companion factory's initial group setup.
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                promptHandle,
                0,
                PromptGroupTab);

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
                "PROMPT REGISTERED: handle=" +
                promptHandle +
                "; target=" +
                targetHandle +
                "; mode=standard; attribute=0x12:true");

            int groupId;

            if (!TryGetTargetPromptGroup(targetHandle, out groupId))
            {
                DeleteCurrentPrompt();
                return;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                promptHandle,
                groupId,
                PromptGroupTab);

            _targetGroupId = groupId;

            Log(
                "TARGET GROUP ASSIGNED: target=" +
                targetHandle +
                "; group=" +
                groupId +
                "; tab=" +
                PromptGroupTab);

            // Match Dog Companion's show-then-enable order.
            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                promptHandle,
                true);

            Log("PROMPT VISIBLE, THEN ENABLED.");

            RegisterIloForTarget(
                targetHandle,
                promptHandle);
        }
        catch (Exception exception)
        {
            Log("PROMPT SETUP ERROR: " + InnermostMessage(exception));
            DeleteCurrentPrompt();
        }
    }

    private void RegisterIloForTarget(
        int targetHandle,
        int promptHandle)
    {
        int promptArgument = UsePromptHandleForIlo
            ? promptHandle
            : 0;

        try
        {
            _iloRegistrationAttempted = true;

            _iloRegistered =
                RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                    targetHandle,
                    PromptText,
                    IloRadius,
                    IloFocusDistance,
                    IloFlag,
                    IloP5,
                    IloP6,
                    promptArgument,
                    ShowIloApproachPrompt,
                    IloP9);

            Log(
                "ILO REGISTER RESULT: target=" +
                targetHandle +
                "; promptArgument=" +
                promptArgument +
                "; returned=" +
                _iloRegistered);
        }
        catch (Exception exception)
        {
            Log("ILO REGISTER ERROR: " + InnermostMessage(exception));
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
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle);

            Log(
                "TARGET GROUP LOOKUP AFTER REGISTRATION: target=" +
                targetHandle +
                "; group=" +
                groupId);

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

    private void DeleteCurrentPrompt()
    {
        if (_promptHandle <= 0)
        {
            _targetHandle = 0;
            _targetGroupId = -1;
            _iloRegistrationAttempted = false;
            _iloRegistered = false;
            return;
        }

        int promptHandle = _promptHandle;
        int targetHandle = _targetHandle;

        if (_iloRegistrationAttempted && targetHandle > 0)
        {
            try
            {
                bool unregistered =
                    RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(
                        targetHandle);

                Log(
                    "ILO UNREGISTER RESULT: target=" +
                    targetHandle +
                    "; returned=" +
                    unregistered);
            }
            catch (Exception exception)
            {
                Log("ILO UNREGISTER ERROR: " + InnermostMessage(exception));
            }
        }

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
        _iloRegistrationAttempted = false;
        _iloRegistered = false;
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