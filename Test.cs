using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;

    // This is a game string key used as a diagnostic label, not the final
    // custom "Recruit" text.
    private const string PromptTextKey = "CUTSCENE_SKIP";

    // Register the outer interaction-lock-on label for nearby NPCs.
    private const float RegistrationRange = 12.0f;
    private const float LockOnPromptRadius = 10.0f;
    private const float LockOnFocusDistance = 12.0f;

    // The diagnostic G prompt is only shown for a focused NPC within this range.
    private const float MaximumTargetDistance = 12.0f;

    private const int RegistrationRefreshMilliseconds = 500;
    private const int HeartbeatMilliseconds = 2000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly Dictionary<int, Ped> _registeredPeds =
        new Dictionary<int, Ped>();

    private int _promptHandle;
    private int _lastObservedTargetHandle = int.MinValue;
    private bool _lastObservedTargetIsHuman;
    private bool _lastObservedTargetInRange;
    private int _lastAssignedGroupId = int.MinValue;
    private int _lastGroupTargetHandle = int.MinValue;
    private DateTime _lastRegistrationRefreshUtc;
    private DateTime _lastHeartbeatUtc;
    private bool _lastPromptActive;
    private bool _hasLoggedPromptState;

    private string _lastTargetError;
    private string _lastGroupError;
    private string _lastInputError;
    private string _lastPromptError;
    private string _lastRegistrationError;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Interaction-lock-on plus entity-group prompt diagnostic ===");
        Log("Diagnostic label key: " + PromptTextKey);
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("ILO prompt: registered per nearby human NPC.");
        Log("G prompt: assigned to the focused NPC's prompt group.");
        Log("This test does not recruit or change gang membership.");

        CreatePrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a nearby human NPC; check its lock-on label and G prompt.");

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

        RefreshNearbyInteractionLockonPromptsIfDue();

        Entity target;
        int targetHandle;
        bool targetIsHuman;
        float targetDistance;

        bool hasFocusedTarget = TryGetFocusedTarget(
            out target,
            out targetHandle,
            out targetIsHuman,
            out targetDistance);

        bool targetInRange =
            hasFocusedTarget &&
            targetDistance >= 0.0f &&
            targetDistance <= MaximumTargetDistance;

        bool eligibleTarget =
            hasFocusedTarget &&
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
            TryAssignPromptToTargetGroup(
                targetHandle,
                out targetGroupId,
                out groupAssignedForCurrentTarget);
        }

        UpdateDiagnosticPrompt(
            eligibleTarget,
            targetGroupId,
            groupAssignedForCurrentTarget);

        LogHeartbeatIfDue(
            targetHandle,
            targetIsHuman,
            targetDistance,
            targetInRange,
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

    private void RefreshNearbyInteractionLockonPromptsIfDue()
    {
        DateTime now = DateTime.UtcNow;

        if (_lastRegistrationRefreshUtc != DateTime.MinValue &&
            (now - _lastRegistrationRefreshUtc).TotalMilliseconds <
            RegistrationRefreshMilliseconds)
        {
            return;
        }

        _lastRegistrationRefreshUtc = now;

        try
        {
            Ped playerPed = Game.Player.Character;

            if (playerPed == null || !playerPed.Exists())
            {
                LogErrorOnce(
                    ref _lastRegistrationError,
                    "ILO REGISTRATION SKIPPED: player ped is unavailable");
                return;
            }

            IEnumerable<Ped> nearbyPeds = World.GetAllPeds();

            if (nearbyPeds == null)
            {
                LogErrorOnce(
                    ref _lastRegistrationError,
                    "ILO REGISTRATION SKIPPED: World.GetAllPeds returned null");
                return;
            }

            HashSet<int> desiredRegistrations = new HashSet<int>();

            foreach (Ped ped in nearbyPeds)
            {
                if (ped == null || !ped.Exists())
                {
                    continue;
                }

                int pedHandle = ped.Handle;

                if (pedHandle <= 0 || pedHandle == playerPed.Handle)
                {
                    continue;
                }

                if (!RDR2.Native.PED.IS_PED_HUMAN(pedHandle))
                {
                    continue;
                }

                float distance =
                    ped.Position.DistanceTo(playerPed.Position);

                if (distance < 0.0f ||
                    distance > RegistrationRange)
                {
                    continue;
                }

                desiredRegistrations.Add(pedHandle);

                if (!_registeredPeds.ContainsKey(pedHandle))
                {
                    RegisterInteractionLockonPrompt(ped);
                }
                else
                {
                    // Keep the current object reference fresh.
                    _registeredPeds[pedHandle] = ped;
                }
            }

            UnregisterPedsNoLongerInRange(desiredRegistrations);

            _lastRegistrationError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastRegistrationError,
                "ILO REGISTRATION REFRESH ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void RegisterInteractionLockonPrompt(Ped ped)
    {
        if (ped == null || !ped.Exists())
        {
            return;
        }

        int pedHandle = ped.Handle;

        try
        {
            // This is the entity-specific interaction-lock-on prompt
            // registration. The final boolean enables the approach label.
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                pedHandle,
                PromptTextKey,
                LockOnPromptRadius,
                LockOnFocusDistance,
                0,
                0.0f,
                0.0f,
                0,
                true,
                -1);

            _registeredPeds[pedHandle] = ped;

            Log(
                "ILO REGISTRATION ISSUED: entity=" +
                pedHandle +
                "; radius=" +
                FormatDistance(LockOnPromptRadius) +
                "; focusDistance=" +
                FormatDistance(LockOnFocusDistance) +
                "; approachLabel=true");
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastRegistrationError,
                "ILO REGISTRATION ERROR for entity " +
                pedHandle +
                ": " +
                InnermostMessage(exception));
        }
    }

    private void UnregisterPedsNoLongerInRange(
        HashSet<int> desiredRegistrations)
    {
        List<int> handlesToRemove = new List<int>();

        foreach (KeyValuePair<int, Ped> entry in _registeredPeds)
        {
            if (!desiredRegistrations.Contains(entry.Key))
            {
                handlesToRemove.Add(entry.Key);
            }
        }

        for (int i = 0; i < handlesToRemove.Count; i++)
        {
            UnregisterInteractionLockonPrompt(handlesToRemove[i]);
        }
    }

    private void UnregisterInteractionLockonPrompt(int pedHandle)
    {
        try
        {
            RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(
                pedHandle);

            Log("ILO UNREGISTERED: entity=" + pedHandle);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastRegistrationError,
                "ILO UNREGISTER ERROR for entity " +
                pedHandle +
                ": " +
                InnermostMessage(exception));
        }
        finally
        {
            _registeredPeds.Remove(pedHandle);
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
                "FOCUSED TARGET RESOLUTION ERROR: " +
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
                    0);

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

    private void UpdateDiagnosticPrompt(
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
                Log("UI PROMPT PRESS DETECTED; no action taken");
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
            "TARGET STATE: focusedTarget=" +
            FormatTargetHandle(targetHandle) +
            "; human=" +
            targetIsHuman +
            "; distance=" +
            FormatDistance(targetDistance) +
            "; inRange=" +
            targetInRange);
    }

    private void LogHeartbeatIfDue(
        int targetHandle,
        bool targetIsHuman,
        float targetDistance,
        bool targetInRange,
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
            "; human=" +
            targetIsHuman +
            "; distance=" +
            FormatDistance(targetDistance) +
            "; inRange=" +
            targetInRange +
            "; eligible=" +
            eligibleTarget +
            "; group=" +
            targetGroupId +
            "; groupAssigned=" +
            groupAssignedForCurrentTarget +
            "; uiPromptActive=" +
            _lastPromptActive +
            "; registeredIloPeds=" +
            _registeredPeds.Count +
            "; rawG=" +
            rawControlJustPressed);
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
            ? distance.ToString(
                "0.00",
                CultureInfo.InvariantCulture)
            : "unknown";
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
        List<int> registeredHandles =
            new List<int>(_registeredPeds.Keys);

        for (int i = 0; i < registeredHandles.Count; i++)
        {
            UnregisterInteractionLockonPrompt(
                registeredHandles[i]);
        }

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