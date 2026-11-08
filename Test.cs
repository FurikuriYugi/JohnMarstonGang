using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const string PromptTextKey = "CUTSCENE_SKIP";
    private const float MaximumTargetDistance = 8.0f;
    private const int HeartbeatMilliseconds = 2000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _lastObservedTargetHandle = int.MinValue;
    private bool _lastObservedTargetIsHuman;
    private bool _lastObservedTargetInRange;
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

        Log("=== Entity-focus prompt diagnostic ===");
        Log("Prompt text key: " + PromptTextKey);
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("This test does not recruit. G is logged only.");
        Log("Prompt is assigned to the focused entity's prompt group.");

        CreatePrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a nearby human NPC; look for the diagnostic G prompt.");

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
            Log("PROMPT CREATION ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        bool rawControlJustPressed = false;

        try
        {
            rawControlJustPressed =
                RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                    0,
                    InputInteractOption1);

            _lastInputError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastInputError,
                "INPUT READ ERROR: " + InnermostMessage(exception));
        }

        Entity target;
        int targetHandle;
        bool targetIsHuman;
        float targetDistance;

        bool hasRawTarget = TryGetFocusedTarget(
            out target,
            out targetHandle,
            out targetIsHuman,
            out targetDistance);

        bool targetInRange =
            hasRawTarget &&
            targetDistance >= 0.0f &&
            targetDistance <= MaximumTargetDistance;

        bool eligibleTarget =
            hasRawTarget &&
            targetIsHuman &&
            targetInRange;

        LogTargetStateIfChanged(
            targetHandle,
            targetIsHuman,
            targetInRange,
            targetDistance);

        if (rawControlJustPressed)
        {
            Log("RAW G-CONTROL PRESS DETECTED; no action taken");
        }

        int targetGroupId = -1;
        bool groupAssignedForCurrentTarget = false;

        if (_promptHandle > 0 && eligibleTarget)
        {
            try
            {
                targetGroupId =
                    RDR2.Native.HUD
                        ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                            targetHandle);

                _lastGroupError = null;

                if (targetGroupId >= 0)
                {
                    if (_lastGroupTargetHandle != targetHandle ||
                        _lastAssignedGroupId != targetGroupId)
                    {
                        RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                            _promptHandle,
                            targetGroupId,
                            0);

                        _lastGroupTargetHandle = targetHandle;
                        _lastAssignedGroupId = targetGroupId;

                        Log("PROMPT ASSIGNED TO ENTITY GROUP: target=" +
                            targetHandle +
                            "; group=" + targetGroupId);
                    }

                    groupAssignedForCurrentTarget =
                        _lastGroupTargetHandle == targetHandle &&
                        _lastAssignedGroupId == targetGroupId;
                }
                else
                {
                    LogErrorOnce(
                        ref _lastGroupError,
                        "GROUP LOOKUP RETURNED INVALID VALUE: " +
                        targetGroupId);
                }
            }
            catch (Exception exception)
            {
                LogErrorOnce(
                    ref _lastGroupError,
                    "GROUP LOOKUP/ASSIGNMENT ERROR: " +
                    InnermostMessage(exception));
            }
        }

        bool showPrompt =
            _promptHandle > 0 &&
            eligibleTarget &&
            targetGroupId >= 0 &&
            groupAssignedForCurrentTarget;

        if (_promptHandle > 0)
        {
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

                    Log("PROMPT STATE: active=" + promptActive);
                }

                if (promptJustPressed)
                {
                    Log("PROMPT PRESS DETECTED; no action taken");
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

        DateTime now = DateTime.UtcNow;

        if (_lastHeartbeatUtc == DateTime.MinValue ||
            (now - _lastHeartbeatUtc).TotalMilliseconds >=
                HeartbeatMilliseconds)
        {
            _lastHeartbeatUtc = now;

            Log(
                "HEARTBEAT: rawTarget=" +
                FormatTargetHandle(targetHandle) +
                "; human=" + targetIsHuman +
                "; distance=" + FormatDistance(targetDistance) +
                "; inRange=" + targetInRange +
                "; eligible=" + eligibleTarget +
                "; group=" + targetGroupId +
                "; groupAssigned=" + groupAssignedForCurrentTarget +
                "; promptActive=" + _lastPromptActive +
                "; rawG=" + rawControlJustPressed);
        }
    }

    private bool TryGetFocusedTarget(
        out Entity target,
        out int targetHandle,
        out bool targetIsHuman,
        out float targetDistance)
    {
        target = null;
        targetHandle = 0;
        targetIsHuman = false;
        targetDistance = -1.0f;

        try
        {
            target = Game.Player.GetInteractionLockonTargetEntity();

            if (target == null || !target.Exists())
            {
                _lastTargetError = null;
                return false;
            }

            targetHandle = target.Handle;

            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            Ped playerPed = Game.Player.Character;

            if (playerPed != null && playerPed.Exists())
            {
                targetDistance =
                    target.Position.DistanceTo(playerPed.Position);
            }

            _lastTargetError = null;
            return true;
        }
        catch (Exception exception)
        {
            target = null;
            targetHandle = 0;
            targetIsHuman = false;
            targetDistance = -1.0f;

            LogErrorOnce(
                ref _lastTargetError,
                "TARGET RESOLUTION ERROR: " +
                InnermostMessage(exception));

            return false;
        }
    }

    private void LogTargetStateIfChanged(
        int targetHandle,
        bool targetIsHuman,
        bool targetInRange,
        float targetDistance)
    {
        if (targetHandle == _lastObservedTargetHandle &&
            targetIsHuman == _lastObservedTargetIsHuman &&
            targetInRange == _lastObservedTargetInRange)
        {
            return;
        }

        _lastObservedTargetHandle = targetHandle;
        _lastObservedTargetIsHuman = targetIsHuman;
        _lastObservedTargetInRange = targetInRange;

        Log(
            "TARGET STATE: rawTarget=" +
            FormatTargetHandle(targetHandle) +
            "; human=" + targetIsHuman +
            "; distance=" + FormatDistance(targetDistance) +
            "; inRange=" + targetInRange);
    }

    private string FormatTargetHandle(int targetHandle)
    {
        return targetHandle > 0
            ? targetHandle.ToString()
            : "none";
    }

    private string FormatDistance(float distance)
    {
        return distance >= 0.0f
            ? distance.ToString("0.00", CultureInfo.InvariantCulture)
            : "unknown";
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
            Log("CLEANUP ERROR: " +
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
            " " + message + Environment.NewLine);
    }
}