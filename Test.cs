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
    private const ulong VarStringHash = 0xFA925AC00EB830B9UL;

    private const int InputInteractOption1 = 0x760A9C6F;
    private const int PromptTabCount = 1;
    private const int DefaultPromptTab = 0;
    private const string PromptText = "Recruit (test only)";
    private const string PromptGroupText = "Recruitment test";

    private int _trackedPedHandle;
    private int _promptGroupHandle = -1;
    private int _promptHandle = -1;

    private DateTime _nextGroupLookupAttemptUtc = DateTime.MinValue;
    private DateTime _nextStatusReminderUtc = DateTime.MinValue;

    private bool _reportedNoTarget;
    private bool _reportedGroupLabelFailure;
    private bool _reportedPromptPressed;

    public NativeLockOnPromptTest()
    {
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        int currentTargetHandle = GetFocusedPedHandle();

        if (currentTargetHandle != _trackedPedHandle)
        {
            int previousTargetHandle = _trackedPedHandle;

            DetachPromptFromCurrentTarget();

            _trackedPedHandle = currentTargetHandle;
            _nextGroupLookupAttemptUtc = DateTime.MinValue;
            _nextStatusReminderUtc = DateTime.MinValue;
            _reportedNoTarget = false;
            _reportedGroupLabelFailure = false;
            _reportedPromptPressed = false;

            if (_trackedPedHandle <= 0)
            {
                if (previousTargetHandle > 0)
                {
                    RDR2.UI.Screen.PrintSubtitle(
                        "Lock-on target lost; test prompt detached.");
                }
                else
                {
                    ReportNoTarget();
                }

                return;
            }

            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Lock-on target found: {0}. Starting prompt test.",
                    _trackedPedHandle));
        }

        if (_trackedPedHandle <= 0)
        {
            ReportNoTarget();
            return;
        }

        if (_promptGroupHandle < 0 &&
            DateTime.UtcNow >= _nextGroupLookupAttemptUtc)
        {
            _nextGroupLookupAttemptUtc = DateTime.UtcNow.AddSeconds(2);
            TryAttachPromptToTrackedTarget();
        }

        if (_promptHandle <= 0 || _promptGroupHandle < 0)
        {
            return;
        }

        ActivatePromptGroupThisFrame();

        if (CallNative<bool>(
                PromptIsJustPressedHash,
                _promptHandle))
        {
            if (!_reportedPromptPressed)
            {
                _reportedPromptPressed = true;
                RDR2.UI.Screen.PrintSubtitle(
                    "G prompt accepted; test only. No recruitment action is connected.");
            }
        }

        if (DateTime.UtcNow >= _nextStatusReminderUtc)
        {
            _nextStatusReminderUtc = DateTime.UtcNow.AddSeconds(5);

            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Prompt test active: target {0}, group {1}. Check for the G prompt.",
                    _trackedPedHandle,
                    _promptGroupHandle));
        }
    }

    private static int GetFocusedPedHandle()
    {
        Ped focusedPed =
            Game.Player.GetInteractionLockonTargetEntity() as Ped;

        if (focusedPed == null || !focusedPed.Exists())
        {
            return 0;
        }

        return focusedPed.Handle;
    }

    private void TryAttachPromptToTrackedTarget()
    {
        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Looking up prompt group for target {0}.",
                _trackedPedHandle));

        int groupHandle = CallNative<int>(
            PromptGetGroupIdForTargetEntityHash,
            _trackedPedHandle);

        if (groupHandle < 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Prompt-group lookup returned {0} for target {1}.",
                    groupHandle,
                    _trackedPedHandle));
            return;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Prompt-group lookup returned {0}.",
                groupHandle));

        if (_promptHandle <= 0 && !CreateNativePrompt())
        {
            return;
        }

        CallNative(
            PromptSetGroupHash,
            _promptHandle,
            groupHandle,
            0);

        _promptGroupHandle = groupHandle;

        CallNative(
            PromptSetEnabledHash,
            _promptHandle,
            true);

        CallNative(
            PromptSetVisibleHash,
            _promptHandle,
            true);

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Prompt configured and attached to group {0}; activating group each frame.",
                _promptGroupHandle));
    }

    private bool CreateNativePrompt()
    {
        RDR2.UI.Screen.PrintSubtitle(
            "Prompt registration: calling REGISTER_BEGIN.");

        int promptHandle = CallNative<int>(
            PromptRegisterBeginHash);

        if (promptHandle <= 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Prompt registration failed; handle was {0}.",
                    promptHandle));
            return false;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Prompt registration returned handle {0}.",
                promptHandle));

        ulong promptTextPointer = CallNative<ulong>(
            VarStringHash,
            10,
            "LITERAL_STRING",
            PromptText);

        if (promptTextPointer == 0UL)
        {
            RDR2.Native.Function.Call(
                PromptRegisterEndHash,
                promptHandle);

            RDR2.UI.Screen.PrintSubtitle(
                "Prompt text setup failed: VAR_STRING returned zero.");
            return false;
        }

        RDR2.UI.Screen.PrintSubtitle(
            "Prompt text pointer created; setting control and text.");

        CallNative(
            PromptSetControlActionHash,
            promptHandle,
            InputInteractOption1);

        CallNative(
            PromptSetTextHash,
            promptHandle,
            promptTextPointer);

        RDR2.UI.Screen.PrintSubtitle(
            "Prompt control and text calls made; calling REGISTER_END.");

        RDR2.Native.Function.Call(
            PromptRegisterEndHash,
            promptHandle);

        CallNative(
            PromptSetEnabledHash,
            promptHandle,
            false);

        CallNative(
            PromptSetVisibleHash,
            promptHandle,
            false);

        _promptHandle = promptHandle;

        RDR2.UI.Screen.PrintSubtitle(
            "Prompt registration completed; waiting to attach it to a target group.");

        return true;
    }

    private void ActivatePromptGroupThisFrame()
    {
        ulong groupTextPointer = CallNative<ulong>(
            VarStringHash,
            10,
            "LITERAL_STRING",
            PromptGroupText);

        if (groupTextPointer == 0UL)
        {
            if (!_reportedGroupLabelFailure)
            {
                _reportedGroupLabelFailure = true;
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
            DefaultPromptTab,
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
            "No lock-on target returned. Aim at an NPC and hold right mouse.");
    }

    private void DetachPromptFromCurrentTarget()
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

        _promptGroupHandle = -1;
    }

    private static void CallNative(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        RDR2.Native.Function.Call(
            nativeHash,
            arguments);
    }

    private static T CallNative<T>(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        return RDR2.Native.Function.Call<T>(
            nativeHash,
            arguments);
    }
}