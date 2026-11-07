using System;
using RDR2;

public class NativeLockOnPromptTest : Script
{
    private const ulong PromptRegisterBeginHash = 0x04F97DE45A519419UL;
    private const ulong PromptRegisterEndHash = 0xF7AA2696A22AD8B9UL;
    private const ulong PromptSetControlActionHash = 0xB5352B7494A08258UL;
    private const ulong PromptSetTextHash = 0x5DD02A8318420DD7UL;
    private const ulong PromptSetEnabledHash = 0x8A0FB4D03A630D21UL;
    private const ulong PromptSetVisibleHash = 0x71215ACCFDE075EEUL;
    private const ulong PromptGetGroupIdForTargetEntityHash = 0xB796970BD125FCE8UL;
    private const ulong PromptSetGroupHash = 0x2F11D3A254169EA4UL;
    private const ulong PromptRemoveGroupHash = 0x4E52C800A28F7BE8UL;
    private const ulong PromptIsJustPressedHash = 0x2787CC611D3FACC5UL;
    private const ulong PromptSetActiveGroupThisFrameHash = 0xC65A45D4453C2627UL;
    private const ulong PromptUnregisterInteractionLockonHash = 0xE98D55C5983F2509UL;
    private const ulong VarStringHash = 0xFA925AC00EB830B9UL;

    private const int InputInteractOption1 = 0x760A9C6F;
    private const int PromptTabIndex = 0;
    private const int PromptTabCount = 1;

    private const float IloRadius = 5.0f;
    private const float IloFocusDistance = 5.0f;
    private const int IloFlags = 0;
    private const float IloUnknownP5 = 0.0f;
    private const float IloUnknownP6 = 0.0f;
    private const int IloPromptHandle = 0;
    private const bool IloShowApproachPrompt = true;
    private const int IloUnknownP9 = -1;

    private const string IloText = "Recruitment test";
    private const string ActionPromptText = "Recruit (test only)";
    private const string PromptGroupText = "Recruitment test";

    private int _trackedTargetHandle;
    private int _promptHandle = -1;
    private int _promptGroupHandle = -1;
    private bool _iloRegistrationAttempted;
    private bool _reportedNoTarget;
    private bool _reportedPromptAccepted;
    private bool _reportedGroupActivationFailure;
    private DateTime _nextStatusReminderUtc = DateTime.MinValue;

    public NativeLockOnPromptTest()
    {
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateTest();
        }
        catch (Exception exception)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Prompt test exception: {0}",
                    exception.Message));
        }
    }

    private void UpdateTest()
    {
        int currentTargetHandle;
        bool hasTarget = TryGetPlayerTargetEntity(out currentTargetHandle);

        if (!hasTarget || currentTargetHandle <= 0)
        {
            if (_trackedTargetHandle > 0)
            {
                DetachFromTrackedTarget();
                _trackedTargetHandle = 0;
                RDR2.UI.Screen.PrintSubtitle(
                    "Confirmed target getter returned no target; prompt detached.");
            }
            else
            {
                ReportNoTarget();
            }

            return;
        }

        if (currentTargetHandle != _trackedTargetHandle)
        {
            DetachFromTrackedTarget();

            _trackedTargetHandle = currentTargetHandle;
            _iloRegistrationAttempted = false;
            _reportedPromptAccepted = false;
            _reportedGroupActivationFailure = false;
            _nextStatusReminderUtc = DateTime.MinValue;

            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "GET_PLAYER_TARGET_ENTITY returned target {0}.",
                    _trackedTargetHandle));

            RegisterInteractionLockonPromptForTarget();
            AttachActionPromptToTarget();
        }

        if (_trackedTargetHandle <= 0 ||
            _promptHandle <= 0 ||
            _promptGroupHandle < 0)
        {
            return;
        }

        ActivateActionPromptGroupThisFrame();

        if (CallNative<bool>(
                PromptIsJustPressedHash,
                _promptHandle))
        {
            if (!_reportedPromptAccepted)
            {
                _reportedPromptAccepted = true;
                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt native reported G just pressed; no recruitment action is connected.");
            }
        }

        if (DateTime.UtcNow >= _nextStatusReminderUtc)
        {
            _nextStatusReminderUtc = DateTime.UtcNow.AddSeconds(5);

            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Prompt test running: target {0}, group {1}. Check for the G prompt.",
                    _trackedTargetHandle,
                    _promptGroupHandle));
        }
    }

    private static bool TryGetPlayerTargetEntity(out int targetHandle)
    {
        targetHandle = 0;

        bool hasTarget =
            RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                Game.Player.Handle,
                out targetHandle);

        return hasTarget && targetHandle > 0;
    }

    private void RegisterInteractionLockonPromptForTarget()
    {
        if (_trackedTargetHandle <= 0 || _iloRegistrationAttempted)
        {
            return;
        }

        _iloRegistrationAttempted = true;

        RDR2.UI.Screen.PrintSubtitle(
            "Calling REGISTER_INTERACTION_LOCKON_PROMPT; action-prompt handle is 0.");

        bool registrationResult =
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                _trackedTargetHandle,
                IloText,
                IloRadius,
                IloFocusDistance,
                IloFlags,
                IloUnknownP5,
                IloUnknownP6,
                IloPromptHandle,
                IloShowApproachPrompt,
                IloUnknownP9);

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "REGISTER_INTERACTION_LOCKON_PROMPT returned {0}.",
                registrationResult));
    }

    private void AttachActionPromptToTarget()
    {
        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            "Looking up the target entity's prompt group.");

        int groupHandle =
            CallNative<int>(
                PromptGetGroupIdForTargetEntityHash,
                _trackedTargetHandle);

        if (groupHandle < 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Target prompt-group lookup returned {0}.",
                    groupHandle));
            return;
        }

        _promptGroupHandle = groupHandle;

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Target prompt-group lookup returned {0}.",
                _promptGroupHandle));

        if (_promptHandle <= 0)
        {
            if (!CreateActionPrompt(_promptGroupHandle))
            {
                _promptGroupHandle = -1;
                return;
            }
        }
        else
        {
            CallNative(
                PromptSetGroupHash,
                _promptHandle,
                _promptGroupHandle,
                PromptTabIndex);

            CallNative(
                PromptSetEnabledHash,
                _promptHandle,
                true);

            CallNative(
                PromptSetVisibleHash,
                _promptHandle,
                true);
        }

        RDR2.UI.Screen.PrintSubtitle(
            "Action prompt was assigned to the target group; per-frame group activation will now be tested.");
    }

    private bool CreateActionPrompt(int groupHandle)
    {
        RDR2.UI.Screen.PrintSubtitle(
            "Calling UI_PROMPT_REGISTER_BEGIN.");

        int promptHandle =
            CallNative<int>(
                PromptRegisterBeginHash);

        if (promptHandle <= 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "UI_PROMPT_REGISTER_BEGIN returned handle {0}.",
                    promptHandle));
            return false;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "UI_PROMPT_REGISTER_BEGIN returned handle {0}.",
                promptHandle));

        ulong promptTextPointer =
            CallNative<ulong>(
                VarStringHash,
                10,
                "LITERAL_STRING",
                ActionPromptText);

        if (promptTextPointer == 0UL)
        {
            RDR2.Native.Function.Call(
                PromptRegisterEndHash,
                promptHandle);

            RDR2.UI.Screen.PrintSubtitle(
                "Action-prompt text setup failed: VAR_STRING returned zero.");
            return false;
        }

        RDR2.UI.Screen.PrintSubtitle(
            "Setting the G control, action text, and target group.");

        CallNative(
            PromptSetControlActionHash,
            promptHandle,
            InputInteractOption1);

        CallNative(
            PromptSetTextHash,
            promptHandle,
            promptTextPointer);

        CallNative(
            PromptSetGroupHash,
            promptHandle,
            groupHandle,
            PromptTabIndex);

        RDR2.Native.Function.Call(
            PromptRegisterEndHash,
            promptHandle);

        CallNative(
            PromptSetEnabledHash,
            promptHandle,
            true);

        CallNative(
            PromptSetVisibleHash,
            promptHandle,
            true);

        _promptHandle = promptHandle;

        RDR2.UI.Screen.PrintSubtitle(
            "Action prompt registration ended; enabled and visible calls were made.");

        return true;
    }

    private void ActivateActionPromptGroupThisFrame()
    {
        ulong groupTextPointer =
            CallNative<ulong>(
                VarStringHash,
                10,
                "LITERAL_STRING",
                PromptGroupText);

        if (groupTextPointer == 0UL)
        {
            if (!_reportedGroupActivationFailure)
            {
                _reportedGroupActivationFailure = true;
                RDR2.UI.Screen.PrintSubtitle(
                    "Group activation skipped: VAR_STRING returned zero for group text.");
            }

            return;
        }

        CallNative(
            PromptSetActiveGroupThisFrameHash,
            _promptGroupHandle,
            groupTextPointer,
            PromptTabCount,
            PromptTabIndex,
            0,
            _promptHandle);
    }

    private void ReportNoTarget()
    {
        if (_reportedNoTarget)
        {
            return;
        }

        _reportedNoTarget = true;

        RDR2.UI.Screen.PrintSubtitle(
            "GET_PLAYER_TARGET_ENTITY returned no target. Right-click an NPC to target them.");
    }

    private void DetachFromTrackedTarget()
    {
        if (_promptHandle > 0)
        {
            if (_promptGroupHandle >= 0)
            {
                CallNative(
                    PromptRemoveGroupHash,
                    _promptHandle,
                    _promptGroupHandle);
            }

            CallNative(
                PromptSetEnabledHash,
                _promptHandle,
                false);

            CallNative(
                PromptSetVisibleHash,
                _promptHandle,
                false);
        }

        if (_trackedTargetHandle > 0 && _iloRegistrationAttempted)
        {
            CallNative(
                PromptUnregisterInteractionLockonHash,
                _trackedTargetHandle);
        }

        _promptGroupHandle = -1;
        _iloRegistrationAttempted = false;
    }

    private static void CallNative(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        RDR2.Native.Function.Call(
            nativeHash,
            arguments);
    }
    //test
    private static T CallNative<T>(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        return RDR2.Native.Function.Call<T>(
            nativeHash,
            arguments);
    }
}