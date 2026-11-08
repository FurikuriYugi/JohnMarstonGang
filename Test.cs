using System;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int PromptGroupId = 0x4A4D47;

    // Existing game string key used only to test prompt rendering.
    // This diagnostic intentionally does not use a custom "Recruit" string.
    private const string PromptTextKey = "CUTSCENE_SKIP";
    private const string PromptGroupNameKey = "CUTSCENE_SKIP";

    private const float MaximumTargetDistance = 8.0f;
    private const int HeartbeatMilliseconds = 2000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _lastTargetHandle;
    private DateTime _lastHeartbeatUtc;
    private bool _lastPromptActive;
    private bool _hasLoggedPromptState;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Registered-string prompt rendering diagnostic ===");
        Log("Prompt text key: " + PromptTextKey);
        Log("Prompt group-name key: " + PromptGroupNameKey);
        Log("Prompt group ID: " + PromptGroupId);
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("G input only logs an event. It does not recruit.");

        CreatePrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a nearby human NPC; check for the G prompt.");

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
                PromptTextKey);

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                PromptGroupId,
                0);

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
            Log("PROMPT CREATION ERROR: " + InnermostMessage(exception));
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            if (_promptHandle <= 0)
            {
                return;
            }

            int targetHandle = GetFocusedHumanTargetHandle();

            if (targetHandle != _lastTargetHandle)
            {
                _lastTargetHandle = targetHandle;

                if (targetHandle > 0)
                {
                    Log("FOCUSED HUMAN TARGET: " + targetHandle);
                }
                else
                {
                    Log("NO QUALIFYING FOCUSED HUMAN TARGET");
                }
            }

            bool hasTarget = targetHandle > 0;

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                hasTarget);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                hasTarget);

            if (!hasTarget)
            {
                return;
            }

            // Active groups must be activated every frame they should display.
            RDR2.Native.HUD._UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME(
                PromptGroupId,
                PromptGroupNameKey,
                0,
                0,
                0,
                0);

            bool promptActive =
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(_promptHandle);

            if (!_hasLoggedPromptState ||
                promptActive != _lastPromptActive)
            {
                _hasLoggedPromptState = true;
                _lastPromptActive = promptActive;
                Log("PROMPT STATE: active=" + promptActive);
            }

            bool controlJustPressed =
                RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                    0,
                    InputInteractOption1);

            bool promptJustPressed =
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    _promptHandle);

            if (controlJustPressed)
            {
                Log("RAW G-CONTROL PRESS DETECTED");
            }

            if (promptJustPressed)
            {
                Log("PROMPT PRESS DETECTED");
            }

            DateTime now = DateTime.UtcNow;
            if (_lastHeartbeatUtc == DateTime.MinValue ||
                (now - _lastHeartbeatUtc).TotalMilliseconds >=
                    HeartbeatMilliseconds)
            {
                _lastHeartbeatUtc = now;

                Log("HEARTBEAT: target=" + targetHandle +
                    "; promptActive=" + promptActive +
                    "; controlJustPressed=" + controlJustPressed +
                    "; promptJustPressed=" + promptJustPressed);
            }
        }
        catch (Exception exception)
        {
            Log("TICK ERROR: " + InnermostMessage(exception));
        }
    }

    private int GetFocusedHumanTargetHandle()
    {
        try
        {
            Entity target =
                Game.Player.GetInteractionLockonTargetEntity();

            if (target == null ||
                !target.Exists() ||
                !RDR2.Native.PED.IS_PED_HUMAN(target.Handle))
            {
                return 0;
            }

            Ped playerPed = Game.Player.Character;
            if (playerPed == null || !playerPed.Exists())
            {
                return 0;
            }

            float distance =
                target.Position.DistanceTo(playerPed.Position);

            if (distance > MaximumTargetDistance)
            {
                return 0;
            }

            return target.Handle;
        }
        catch (Exception exception)
        {
            Log("TARGET RESOLUTION ERROR: " +
                InnermostMessage(exception));
            return 0;
        }
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
            Log("CLEANUP ERROR: " + InnermostMessage(exception));
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
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
            " " + message + Environment.NewLine);
    }
}