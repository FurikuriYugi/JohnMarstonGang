using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int PromptGroupTab = 0;
    private const string RecruitPromptText = "Recruit";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _promptHandle;

    private bool _hasLoggedTargetState;
    private bool _lastNativeFoundTarget;
    private int _lastTargetHandle = int.MinValue;
    private bool _lastTargetIsHuman;
    private string _lastTargetError;

    private bool _hasLoggedGroupActivePage;
    private int _lastActivePageGroupId = int.MinValue;
    private int _lastGroupActivePage = int.MinValue;
    private string _lastGroupError;
    private string _lastGroupActivePageError;

    private string _lastInputError;
    private string _lastPromptError;
    private string _lastScreenTextError;

    private bool _hasLoggedScreenTextAttempt;
    private bool _hasLoggedPromptActiveState;
    private bool _lastPromptActiveState;

    private int _lastAssignedTargetHandle = int.MinValue;
    private int _lastAssignedGroupId = int.MinValue;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== HUD VAR_STRING display comparison test: Recruit/G ===");
        Log("Goal: compare VAR_STRING in HUD._DISPLAY_TEXT and _UI_PROMPT_SET_TEXT.");
        Log("Screen diagnostic appears while a focused human target is present.");
        Log("Recruit/G prompt remains assigned to the focused target's prompt group.");
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
                Log("PROMPT CREATION FAILED: handle=" + _promptHandle);
                return;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _promptHandle,
                InputInteractOption1);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                RDR2.Native.MISC.VAR_STRING(
                    10,
                    "LITERAL_STRING",
                    RecruitPromptText));

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
                "PROMPT CREATED: name=Recruit/G; handle=" +
                _promptHandle +
                "; textSource=direct VAR_STRING result");
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
            _hasLoggedScreenTextAttempt = false;
            _hasLoggedGroupActivePage = false;
            UpdatePrompt(false);
            return;
        }

        DrawVarStringScreenDiagnostic();

        int targetGroupId;
        bool groupFound = TryGetTargetPromptGroup(
            targetHandle,
            out targetGroupId);

        bool promptAssigned = false;

        if (groupFound && targetGroupId >= 0)
        {
            AssignPromptToTargetGroup(
                targetHandle,
                targetGroupId,
                out promptAssigned);

            LogGroupActivePageIfChanged(targetGroupId);
        }
        else
        {
            _hasLoggedGroupActivePage = false;
        }

        bool shouldShowPrompt =
            groupFound &&
            promptAssigned;

        UpdatePrompt(shouldShowPrompt);
    }

    private void DrawVarStringScreenDiagnostic()
    {
        try
        {
            RDR2.Native.HUD.SET_TEXT_SCALE(
                0.35f,
                0.35f);

            RDR2.Native.HUD.SET_TEXT_CENTRE(
                true);

            RDR2.Native.HUD._DISPLAY_TEXT(
                RDR2.Native.MISC.VAR_STRING(
                    10,
                    "LITERAL_STRING",
                    RecruitPromptText),
                0.5f,
                0.08f);

            if (!_hasLoggedScreenTextAttempt)
            {
                _hasLoggedScreenTextAttempt = true;
                Log(
                    "VAR_STRING SCREEN DISPLAY CALLED: " +
                    "direct result passed to HUD._DISPLAY_TEXT");
            }

            _lastScreenTextError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastScreenTextError,
                "VAR_STRING SCREEN DISPLAY ERROR: " +
                InnermostMessage(exception));
        }
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
                    "PROMPT GROUP ASSIGNED: target=" +
                    targetHandle +
                    "; group=" +
                    targetGroupId);
            }

            assignedForCurrentTarget =
                _lastAssignedTargetHandle == targetHandle &&
                _lastAssignedGroupId == targetGroupId;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref _lastPromptError,
                "GROUP ASSIGNMENT ERROR: " +
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
                    "PROMPT ACTIVE: name=Recruit/G; active=" +
                    promptActive);
            }

            if (promptActive &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    _promptHandle))
            {
                Log(
                    "PROMPT PRESSED: name=Recruit/G; " +
                    "no recruitment performed.");
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

            Log("PROMPT DELETED: name=Recruit/G");
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