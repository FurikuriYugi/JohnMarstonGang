using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int PromptGroupTab = 0;
    private const string RegisteredPromptLabel = "CUTSCENE_SKIP";
    private const string LockOnPromptText = "Recruit";
    private const string FocusedPedPromptName = "Recruit";
    private const float LockOnRadius = 8.0f;
    private const float LockOnFocusDistance = 8.0f;
    private const int LockOnFlags = 0;
    private const float LockOnParameter5 = 0.0f;
    private const float LockOnParameter6 = 0.0f;
    private const bool ShowLockOnPromptWhenApproaching = true;
    private const int LockOnParameter9 = -1;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;
    private int _lockOnTargetHandle;
    private bool _lockOnRegistrationSucceeded;
    private int _pedPromptNameTargetHandle = int.MinValue;
    private bool _hasLoggedTargetState;
    private bool _lastNativeFoundTarget;
    private int _lastTargetHandle = int.MinValue;
    private bool _lastTargetIsHuman;
    private bool _hasLoggedGroupActivePage;
    private int _lastActivePageGroupId = int.MinValue;
    private int _lastGroupActivePage = int.MinValue;
    private bool _hasLoggedPromptActiveState;
    private bool _lastPromptActiveState;
    private int _lastAssignedTargetHandle = int.MinValue;
    private int _lastAssignedGroupId = int.MinValue;
    private string _lastTargetError;
    private string _lastInputError;
    private string _lastGroupError;
    private string _lastGroupActivePageError;
    private string _lastPromptError;
    private string _lastLockOnError;
    private string _lastPedPromptNameError;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused NPC prompt-name and G prompt handle test ===");
        Log(
            "Focused ped prompt name: " +
            FocusedPedPromptName);
        Log(
            "ILO approach prompt text: " +
            LockOnPromptText +
            "; radius=" +
            LockOnRadius +
            "; focusDistance=" +
            LockOnFocusDistance +
            "; prompt argument=created G prompt handle.");
        Log(
            "G prompt uses registered label: " +
            RegisteredPromptLabel +
            "; expected action text: G Skip.");
        Log(
            "Control: INPUT_INTERACT_OPTION1; hash=0x760A9C6F; expected key: G.");
        Log(
            "The ILO prompt argument will be the runtime handle returned " +
            "when the G prompt is created.");
        Log(
            "This test changes the focused human ped's prompt name. " +
            "It does not restore the prior name because the original name " +
            "is not available through the current test.");
        Log("G only logs a test press; no recruitment is performed.");

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

            string promptText =
                RDR2.Native.MISC.VAR_STRING(
                    10,
                    RegisteredPromptLabel);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                promptText);

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

            Log(
                "G PROMPT CREATED: handle=" +
                _promptHandle +
                "; label=" +
                RegisteredPromptLabel);
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
        bool nativeFoundTarget;
        int targetHandle;
        bool targetIsHuman;

        ReadFocusedTarget(
            out nativeFoundTarget,
            out targetHandle,
            out targetIsHuman);

        LogTargetStateIfChanged(
            nativeFoundTarget,
            targetHandle,
            targetIsHuman);

        LogInputPresses();

        bool hasValidHumanTarget =
            nativeFoundTarget &&
            targetHandle > 0 &&
            targetIsHuman;

        if (!hasValidHumanTarget)
        {
            UpdatePrompt(false);
            _hasLoggedGroupActivePage = false;
            return;
        }

        ApplyFocusedPedPromptName(targetHandle);
        RegisterLockOnPromptForTarget(targetHandle);

        int targetGroupId;

        if (!TryGetTargetPromptGroup(targetHandle, out targetGroupId))
        {
            UpdatePrompt(false);
            return;
        }

        bool promptAssigned;

        AssignPromptToTargetGroup(
            targetHandle,
            targetGroupId,
            out promptAssigned);

        LogGroupActivePageIfChanged(targetGroupId);
        UpdatePrompt(promptAssigned);
    }

    private void ReadFocusedTarget(
        out bool nativeFoundTarget,
        out int targetHandle,
        out bool targetIsHuman)
    {
        nativeFoundTarget = false;
        targetHandle = 0;
        targetIsHuman = false;

        try
        {
            int focusedTargetHandle = 0;

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
                return;
            }

            targetHandle = focusedTargetHandle;
            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            _lastTargetError = null;
        }
        catch (Exception exception)
        {
            nativeFoundTarget = false;
            targetHandle = 0;
            targetIsHuman = false;

            LogErrorOnce(
                ref _lastTargetError,
                "TARGET LOOKUP ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void ApplyFocusedPedPromptName(int targetHandle)
    {
        if (_pedPromptNameTargetHandle == targetHandle)
        {
            return;
        }

        try
        {
            RDR2.Native.PED._SET_PED_PROMPT_NAME(
                targetHandle,
                FocusedPedPromptName);

            _pedPromptNameTargetHandle = targetHandle;
            _lastPedPromptNameError = null;

            Log(
                "PED FOCUS PROMPT NAME SET: target=" +
                targetHandle +
                "; name=" +
                FocusedPedPromptName);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPedPromptNameError,
                "PED FOCUS PROMPT NAME ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void RegisterLockOnPromptForTarget(int targetHandle)
    {
        if (_lockOnTargetHandle == targetHandle)
        {
            return;
        }

        UnregisterCurrentLockOnTarget();

        try
        {
            bool registered =
                RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                    targetHandle,
                    LockOnPromptText,
                    LockOnRadius,
                    LockOnFocusDistance,
                    LockOnFlags,
                    LockOnParameter5,
                    LockOnParameter6,
                    _promptHandle,
                    ShowLockOnPromptWhenApproaching,
                    LockOnParameter9);

            _lockOnTargetHandle = targetHandle;
            _lockOnRegistrationSucceeded = registered;
            _lastLockOnError = null;

            Log(
                "ILO REGISTRATION: target=" +
                targetHandle +
                "; text=" +
                LockOnPromptText +
                "; promptHandle=" +
                _promptHandle +
                "; succeeded=" +
                registered);
        }
        catch (Exception exception)
        {
            _lockOnTargetHandle = 0;
            _lockOnRegistrationSucceeded = false;

            LogErrorOnce(
                ref _lastLockOnError,
                "ILO REGISTRATION ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void UnregisterCurrentLockOnTarget()
    {
        if (_lockOnTargetHandle <= 0)
        {
            return;
        }

        int targetToUnregister = _lockOnTargetHandle;
        bool registrationSucceeded = _lockOnRegistrationSucceeded;

        _lockOnTargetHandle = 0;
        _lockOnRegistrationSucceeded = false;

        if (!registrationSucceeded)
        {
            Log(
                "ILO UNREGISTER SKIPPED: prior registration did not succeed; " +
                "target=" +
                targetToUnregister);
            return;
        }

        try
        {
            bool unregistered =
                RDR2.Native.MISC
                    .UNREGISTER_INTERACTION_LOCKON_PROMPT(
                        targetToUnregister);

            Log(
                "ILO UNREGISTER: target=" +
                targetToUnregister +
                "; succeeded=" +
                unregistered);

            _lastLockOnError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastLockOnError,
                "ILO UNREGISTER ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogInputPresses()
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

    private bool TryGetTargetPromptGroup(
        int targetHandle,
        out int targetGroupId)
    {
        targetGroupId = -1;

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

    private void AssignPromptToTargetGroup(
        int targetHandle,
        int targetGroupId,
        out bool assignedForCurrentTarget)
    {
        assignedForCurrentTarget = false;

        if (_promptHandle <= 0)
        {
            return;
        }

        try
        {
            if (_lastAssignedTargetHandle != targetHandle ||
                _lastAssignedGroupId != targetGroupId)
            {
                RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                    _promptHandle,
                    targetGroupId,
                    PromptGroupTab);

                _lastAssignedTargetHandle = targetHandle;
                _lastAssignedGroupId = targetGroupId;

                Log(
                    "G PROMPT GROUP ASSIGNED: target=" +
                    targetHandle +
                    "; group=" +
                    targetGroupId);
            }

            assignedForCurrentTarget =
                _lastAssignedTargetHandle == targetHandle &&
                _lastAssignedGroupId == targetGroupId;

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "G PROMPT GROUP ASSIGNMENT ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogGroupActivePageIfChanged(int groupId)
    {
        try
        {
            int activePage =
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ACTIVE_PAGE(
                        (uint)groupId);

            if (!_hasLoggedGroupActivePage ||
                groupId != _lastActivePageGroupId ||
                activePage != _lastGroupActivePage)
            {
                _hasLoggedGroupActivePage = true;
                _lastActivePageGroupId = groupId;
                _lastGroupActivePage = activePage;

                Log(
                    "PROMPT GROUP ACTIVE PAGE: group=" +
                    groupId +
                    "; page=" +
                    activePage +
                    "; active=" +
                    (activePage >= 0));
            }

            _lastGroupActivePageError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastGroupActivePageError,
                "GROUP ACTIVE-PAGE LOOKUP ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void UpdatePrompt(bool shouldShow)
    {
        if (_promptHandle <= 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                shouldShow);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                shouldShow);

            bool promptActive =
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(
                    _promptHandle);

            if (!_hasLoggedPromptActiveState ||
                promptActive != _lastPromptActiveState)
            {
                _hasLoggedPromptActiveState = true;
                _lastPromptActiveState = promptActive;

                Log(
                    "G PROMPT ACTIVE: label=" +
                    RegisteredPromptLabel +
                    "; active=" +
                    promptActive);
            }

            if (promptActive &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    _promptHandle))
            {
                Log(
                    "G PROMPT PRESSED: label=" +
                    RegisteredPromptLabel +
                    "; no recruitment performed.");
            }

            _lastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "G PROMPT UPDATE ERROR: " +
                InnermostMessage(exception));
        }
    }

    private void LogTargetStateIfChanged(
        bool nativeFoundTarget,
        int targetHandle,
        bool targetIsHuman)
    {
        if (_hasLoggedTargetState &&
            nativeFoundTarget == _lastNativeFoundTarget &&
            targetHandle == _lastTargetHandle &&
            targetIsHuman == _lastTargetIsHuman)
        {
            return;
        }

        _hasLoggedTargetState = true;
        _lastNativeFoundTarget = nativeFoundTarget;
        _lastTargetHandle = targetHandle;
        _lastTargetIsHuman = targetIsHuman;

        Log(
            "TARGET: nativeFound=" +
            nativeFoundTarget +
            "; handle=" +
            (targetHandle > 0
                ? targetHandle.ToString()
                : "none") +
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
        UpdatePrompt(false);
        UnregisterCurrentLockOnTarget();
        DeletePrompt();
    }

    private void DeletePrompt()
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

            RDR2.Native.HUD._UI_PROMPT_DELETE(
                _promptHandle);

            Log(
                "G PROMPT DELETED: label=" +
                RegisteredPromptLabel);

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