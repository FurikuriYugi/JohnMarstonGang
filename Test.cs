using System;
using System.Windows.Forms;
using RDR2;

public class NativeLockOnPromptTest : Script
{
    // PP_INTERACTION_LOCKON
    private const int InteractionLockOnPromptType = 9;

    // PP_MODE_BLOCK
    private const int PromptModeBlock = 0;

    private readonly int _playerIndex;

    private int _trackedPedHandle;
    private bool _originalPromptEnabled;

    public NativeLockOnPromptTest()
    {
        _playerIndex = RDR2.Native.PLAYER.PLAYER_ID();

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        UpdateTrackedTarget(GetPlayerTargetHandle());
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.G)
        {
            return;
        }

        int currentTargetHandle = GetPlayerTargetHandle();
        UpdateTrackedTarget(currentTargetHandle);

        if (currentTargetHandle <= 0)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "G test: GET_PLAYER_TARGET_ENTITY returned no target.");
            e.Handled = true;
            return;
        }

        bool pedPromptEnabled =
            RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_FOR_PED_IS_ENABLED(
                _playerIndex,
                currentTargetHandle,
                InteractionLockOnPromptType,
                PromptModeBlock);

        bool playerPromptDisabled =
            RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_IS_DISABLED(
                _playerIndex,
                InteractionLockOnPromptType,
                PromptModeBlock);

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "G test: target {0}; ped prompt enabled {1}; player prompt disabled {2}.",
                currentTargetHandle,
                pedPromptEnabled,
                playerPromptDisabled));

        e.Handled = true;
    }

    private void UpdateTrackedTarget(int targetHandle)
    {
        if (targetHandle == _trackedPedHandle)
        {
            return;
        }

        RestoreTrackedPrompt();

        if (targetHandle <= 0)
        {
            return;
        }

        _trackedPedHandle = targetHandle;

        _originalPromptEnabled =
            RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_FOR_PED_IS_ENABLED(
                _playerIndex,
                _trackedPedHandle,
                InteractionLockOnPromptType,
                PromptModeBlock);

        if (!_originalPromptEnabled)
        {
            RDR2.Native.PLAYER._MODIFY_PLAYER_UI_PROMPT_FOR_PED(
                _playerIndex,
                _trackedPedHandle,
                InteractionLockOnPromptType,
                PromptModeBlock,
                true);
        }

        bool promptEnabled =
            RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_FOR_PED_IS_ENABLED(
                _playerIndex,
                _trackedPedHandle,
                InteractionLockOnPromptType,
                PromptModeBlock);

        bool playerPromptDisabled =
            RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_IS_DISABLED(
                _playerIndex,
                InteractionLockOnPromptType,
                PromptModeBlock);

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Target {0}; prompt enabled {1}; player prompt disabled {2}. Press G to test.",
                _trackedPedHandle,
                promptEnabled,
                playerPromptDisabled));
    }

    private void RestoreTrackedPrompt()
    {
        if (_trackedPedHandle > 0 && !_originalPromptEnabled)
        {
            RDR2.Native.PLAYER._MODIFY_PLAYER_UI_PROMPT_FOR_PED(
                _playerIndex,
                _trackedPedHandle,
                InteractionLockOnPromptType,
                PromptModeBlock,
                false);
        }

        _trackedPedHandle = 0;
        _originalPromptEnabled = false;
    }

    private unsafe int GetPlayerTargetHandle()
    {
        int targetHandle = 0;

        RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
            _playerIndex,
            &targetHandle);

        return targetHandle;
    }
}