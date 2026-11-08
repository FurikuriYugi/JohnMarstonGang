using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const uint InputInteractOption2 = 0x84543902u;

    private const int PromptGroupTab = 0;

    private const string RecruitPromptText = "Recruit";

    // Diagnostic only: this is a database label cited in the upstream prompt-text report.
    // This test passes the token directly to _UI_PROMPT_SET_TEXT; that usage is not
    // yet verified in this project.
    private const string DatabaseLabelToken = "CUTSCENE_SKIP";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly PromptProbe _recruitPrompt = new PromptProbe(
        "Recruit/G",
        InputInteractOption1,
        true);

    private readonly PromptProbe _databaseLabelPrompt = new PromptProbe(
        "Database-label/H",
        InputInteractOption2,
        false);

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

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused-target prompt text comparison test ===");
        Log("Goal: compare custom Recruit/G text with a database-label/H prompt.");
        Log("Target native: PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY");
        Log("Recruit text source: MISC.VAR_STRING(10, LITERAL_STRING, Recruit)");
        Log("Database-label test token: " + DatabaseLabelToken);
        Log("G control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("H control: INPUT_INTERACT_OPTION2; hash=0x" +
            InputInteractOption2.ToString("X8"));
        Log("Both test prompts are assigned to the focused target's prompt group.");
        Log("No recruitment or gang membership changes are performed.");

        CreatePrompt(_recruitPrompt);
        CreatePrompt(_databaseLabelPrompt);

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void CreatePrompt(PromptProbe probe)
    {
        try
        {
            probe.Handle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (probe.Handle <= 0)
            {
                Log("PROMPT CREATION FAILED: name=" + probe.Name +
                    "; handle=" + probe.Handle);
                return;
            }

            string promptText;

            if (probe.UsesVarString)
            {
                promptText = RDR2.Native.MISC.VAR_STRING(
                    10,
                    "LITERAL_STRING",
                    RecruitPromptText);
            }
            else
            {
                // Diagnostic test for a database-backed label.
                // Whether a direct token works through this API wrapper remains unverified.
                promptText = DatabaseLabelToken;
            }

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                probe.Handle,
                probe.ControlAction);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                probe.Handle,
                promptText);

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                probe.Handle,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                probe.Handle);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                probe.Handle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                probe.Handle,
                false);

            Log("PROMPT CREATED: name=" + probe.Name +
                "; handle=" + probe.Handle +
                "; textSource=" +
                (probe.UsesVarString ? "VAR_STRING literal" : "database token"));
        }
        catch (Exception exception)
        {
            Log("PROMPT CREATION ERROR: name=" + probe.Name +
                "; " + InnermostMessage(exception));
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

        int targetGroupId = -1;
        bool groupFound = false;

        if (hasValidHumanTarget &&
            _recruitPrompt.Handle > 0 &&
            _databaseLabelPrompt.Handle > 0)
        {
            groupFound = TryGetTargetPromptGroup(
                targetHandle,
                out targetGroupId);
        }

        bool recruitPromptAssigned = false;
        bool databasePromptAssigned = false;

        if (groupFound && targetGroupId >= 0)
        {
            AssignPromptToTargetGroup(
                _recruitPrompt,
                targetHandle,
                targetGroupId,
                out recruitPromptAssigned);

            AssignPromptToTargetGroup(
                _databaseLabelPrompt,
                targetHandle,
                targetGroupId,
                out databasePromptAssigned);

            LogGroupActivePageIfChanged(targetGroupId);
        }
        else
        {
            _hasLoggedGroupActivePage = false;
        }

        bool showRecruitPrompt =
            hasValidHumanTarget &&
            groupFound &&
            recruitPromptAssigned;

        bool showDatabaseLabelPrompt =
            hasValidHumanTarget &&
            groupFound &&
            databasePromptAssigned;

        UpdatePrompt(_recruitPrompt, showRecruitPrompt);
        UpdatePrompt(_databaseLabelPrompt, showDatabaseLabelPrompt);
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

            if (RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                0,
                InputInteractOption2))
            {
                Log("RAW H / INPUT_INTERACT_OPTION2 PRESS DETECTED");
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
        PromptProbe probe,
        int targetHandle,
        int targetGroupId,
        out bool assignedForCurrentTarget)
    {
        assignedForCurrentTarget = false;

        try
        {
            if (probe.LastTargetHandle != targetHandle ||
                probe.LastAssignedGroupId != targetGroupId)
            {
                RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                    probe.Handle,
                    targetGroupId,
                    PromptGroupTab);

                probe.LastTargetHandle = targetHandle;
                probe.LastAssignedGroupId = targetGroupId;

                Log("PROMPT GROUP ASSIGNED: name=" + probe.Name +
                    "; target=" + targetHandle +
                    "; group=" + targetGroupId);
            }

            assignedForCurrentTarget =
                probe.LastTargetHandle == targetHandle &&
                probe.LastAssignedGroupId == targetGroupId;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref probe.LastGroupError,
                "GROUP ASSIGNMENT ERROR: name=" + probe.Name +
                "; " + InnermostMessage(exception));
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

                Log("PROMPT GROUP ACTIVE PAGE: group=" +
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

    private void UpdatePrompt(
        PromptProbe probe,
        bool shouldShow)
    {
        if (probe.Handle <= 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                probe.Handle,
                shouldShow);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                probe.Handle,
                shouldShow);

            bool promptActive =
                RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(
                    probe.Handle);

            if (!probe.HasLoggedActiveState ||
                promptActive != probe.LastActiveState)
            {
                probe.HasLoggedActiveState = true;
                probe.LastActiveState = promptActive;

                Log("PROMPT ACTIVE: name=" + probe.Name +
                    "; active=" + promptActive);
            }

            if (promptActive &&
                RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                    probe.Handle))
            {
                Log("PROMPT PRESSED: name=" + probe.Name +
                    "; no recruitment performed.");
            }

            probe.LastPromptError = null;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                ref probe.LastPromptError,
                "PROMPT UPDATE ERROR: name=" + probe.Name +
                "; " + InnermostMessage(exception));
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

        Log("TARGET: nativeFound=" +
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
        DeletePrompt(_recruitPrompt);
        DeletePrompt(_databaseLabelPrompt);
    }

    private void DeletePrompt(PromptProbe probe)
    {
        if (probe.Handle <= 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                probe.Handle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                probe.Handle,
                false);

            RDR2.Native.HUD._UI_PROMPT_DELETE(
                probe.Handle);

            Log("PROMPT DELETED: name=" + probe.Name);
            probe.Handle = 0;
        }
        catch (Exception exception)
        {
            Log("PROMPT CLEANUP ERROR: name=" + probe.Name +
                "; " + InnermostMessage(exception));
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

    private sealed class PromptProbe
    {
        public readonly string Name;
        public readonly uint ControlAction;
        public readonly bool UsesVarString;

        public int Handle;
        public int LastTargetHandle = int.MinValue;
        public int LastAssignedGroupId = int.MinValue;
        public bool LastActiveState;
        public bool HasLoggedActiveState;

        public string LastGroupError;
        public string LastPromptError;

        public PromptProbe(
            string name,
            uint controlAction,
            bool usesVarString)
        {
            Name = name;
            ControlAction = controlAction;
            UsesVarString = usesVarString;
        }
    }
}