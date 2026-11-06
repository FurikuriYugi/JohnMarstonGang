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
    private const ulong VarStringHash = 0xFA925AC00EB830B9UL;

    private const int InputInteractOption1 = 0x760A9C6F;
    private const string PromptText = "Recruit (test only)";

    private int _trackedPedHandle;
    private int _promptGroupHandle = -1;
    private int _promptHandle = -1;
    private int _lastGroupLookupFailureTargetHandle = -1;

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
            _lastGroupLookupFailureTargetHandle = -1;

            if (_trackedPedHandle <= 0)
            {
                if (previousTargetHandle > 0)
                {
                    RDR2.UI.Screen.PrintSubtitle(
                        "Lock-on target lost; test prompt detached.");
                }

                return;
            }
        }

        if (_trackedPedHandle <= 0)
        {
            return;
        }

        if (_promptGroupHandle < 0)
        {
            TryAttachPromptToTrackedTarget();
        }

        if (_promptHandle <= 0 || _promptGroupHandle < 0)
        {
            return;
        }

        if (CallNative<bool>(PromptIsJustPressedHash, _promptHandle))
        {
            RDR2.UI.Screen.PrintSubtitle(
                "G prompt pressed; test only. No recruitment action is connected.");
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
        int groupHandle = CallNative<int>(
            PromptGetGroupIdForTargetEntityHash,
            _trackedPedHandle);

        if (groupHandle < 0)
        {
            if (_lastGroupLookupFailureTargetHandle != _trackedPedHandle)
            {
                _lastGroupLookupFailureTargetHandle = _trackedPedHandle;
                RDR2.UI.Screen.PrintSubtitle(
                    string.Format(
                        "Could not get a prompt group for target {0}.",
                        _trackedPedHandle));
            }

            return;
        }

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
                "Target {0}; test prompt attached to group {1}. Press G.",
                _trackedPedHandle,
                _promptGroupHandle));
    }

    private bool CreateNativePrompt()
    {
        int promptHandle = CallNative<int>(PromptRegisterBeginHash);

        if (promptHandle <= 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "Native prompt registration did not return a valid handle.");
            return false;
        }

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
                "VAR_STRING returned an empty prompt-text pointer.");
            return false;
        }

        CallNative(
            PromptSetControlActionHash,
            promptHandle,
            InputInteractOption1);

        CallNative(
            PromptSetTextHash,
            promptHandle,
            promptTextPointer);

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
        return true;
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

        _trackedPedHandle = 0;
        _promptGroupHandle = -1;
    }

    private static void CallNative(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        RDR2.Native.Function.Call(nativeHash, arguments);
    }

    private static T CallNative<T>(
        ulong nativeHash,
        params RDR2.Native.InputArgument[] arguments)
    {
        return RDR2.Native.Function.Call<T>(nativeHash, arguments);
    }
}