using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const string PromptText = "Recruit";
    private const int InputInteractOption1 = 0x760A9C6F;
    private const int PromptAttribute = 0x12;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _targetHandle;
    private int _targetGroupId;
    private int _promptHandle;
    private int _lastLoggedActiveGroupId;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused entity-group activation test ===");
        Log("Prompt: G Recruit");
        Log("A standard HUD prompt is created and assigned to the focused human's entity group.");
        Log("The focused entity group is activated every tick while the target remains focused.");
        Log("No ILO registration or recruitment action is used.");
        Log("Display test only. Do not press G.");

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

        if (targetHandle != _targetHandle)
        {
            DeleteCurrentPrompt();
            CreatePromptForTarget(targetHandle);
        }

        if (_promptHandle > 0 && _targetGroupId != 0)
        {
            ActivateTargetGroupThisFrame();
        }
    }

    private void CreatePromptForTarget(int targetHandle)
    {
        try
        {
            _targetHandle = targetHandle;
            _targetGroupId =
                RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                    targetHandle);

            Log(
                "TARGET GROUP: target=" +
                targetHandle +
                "; group=" +
                _targetGroupId);

            if (_targetGroupId == 0)
            {
                Log("PROMPT CREATION SKIPPED: target group ID is 0.");
                return;
            }

            _promptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_promptHandle <= 0)
            {
                Log(
                    "PROMPT CREATION FAILED: register-begin returned " +
                    _promptHandle);
                _promptHandle = 0;
                return;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _promptHandle,
                InputInteractOption1);

            var promptText =
                RDR2.Native.MISC.VAR_STRING(
                    10,
                    "LITERAL_STRING",
                    PromptText);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                promptText);

            RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(
                _promptHandle,
                PromptAttribute,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                _targetGroupId,
                0);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                true);

            Log(
                "PROMPT CREATED: target=" +
                _targetHandle +
                "; group=" +
                _targetGroupId +
                "; prompt=" +
                _promptHandle);

            _lastLoggedActiveGroupId = 0;
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT CREATION ERROR: " +
                InnermostMessage(exception));

            DeleteCurrentPrompt();
        }
    }

    private void ActivateTargetGroupThisFrame()
    {
        try
        {
            var groupName =
                RDR2.Native.MISC.VAR_STRING(
                    10,
                    "LITERAL_STRING",
                    "");

            RDR2.Native.HUD._UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME(
                (uint)_targetGroupId,
                groupName,
                1,
                0,
                0,
                0);

            if (_lastLoggedActiveGroupId != _targetGroupId)
            {
                Log(
                    "ACTIVE GROUP SET THIS FRAME: target=" +
                    _targetHandle +
                    "; group=" +
                    _targetGroupId);

                _lastLoggedActiveGroupId = _targetGroupId;
            }
        }
        catch (Exception exception)
        {
            Log(
                "ACTIVE GROUP ERROR: group=" +
                _targetGroupId +
                "; " +
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
                return true;
            }

            targetHandle = focusedTargetHandle;
            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            Log(
                "FOCUSED TARGET: target=" +
                targetHandle +
                "; human=" +
                targetIsHuman);

            return true;
        }
        catch (Exception exception)
        {
            Log(
                "TARGET LOOKUP ERROR: " +
                InnermostMessage(exception));

            return false;
        }
    }

    private void DeleteCurrentPrompt()
    {
        if (_promptHandle > 0)
        {
            try
            {
                RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                    _promptHandle,
                    false);

                RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                    _promptHandle,
                    false);

                RDR2.Native.HUD._UI_PROMPT_DELETE(
                    _promptHandle);

                Log(
                    "PROMPT DELETED: target=" +
                    _targetHandle +
                    "; prompt=" +
                    _promptHandle);
            }
            catch (Exception exception)
            {
                Log(
                    "PROMPT DELETE ERROR: " +
                    InnermostMessage(exception));
            }
        }

        _targetHandle = 0;
        _targetGroupId = 0;
        _promptHandle = 0;
        _lastLoggedActiveGroupId = 0;
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