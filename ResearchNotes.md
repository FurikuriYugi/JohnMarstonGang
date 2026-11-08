# Research Notes

## Focused NPC prompt test

The test is isolated to finding a nearby living human NPC, detecting when the player focuses that NPC, displaying a prompt, and recording input. It does not recruit the NPC or change gang membership.

The existing `Test_Log.txt` shows that the prompt handle was created and the control action was set, but the test then failed to resolve `VAR_STRING` and deleted the prompt. The target-focus event occurred afterward, so there was no prompt left to attach or display. The issue is the reflection lookup—not evidence that NPC selection or focus detection failed.

Build errors from the attempted generic call clarified the v2.2.0 wrapper contracts:

- `MISC.VAR_STRING` is a non-generic method with signature `VAR_STRING(int, params InputArgument[])`; it cannot be called with `<string>`.
- The prompt text wrapper expects a `System.String`; it does not accept the `IntPtr` returned by a `VAR_STRING` call.
- The test now avoids `VAR_STRING` and passes `"Recruit"` directly to `_UI_PROMPT_SET_TEXT`.

## Prompt text and input

The prompt control and text are configured separately:

- Control: `INPUT_INTERACT_OPTION1` (`0x760A9C6F`).
- Prompt text: `"Recruit"` passed as a `string`.
- Ambient group label: `"NPC"` passed as the `string` argument expected by the ambient-group wrapper.

The test records prompt-level state (`UI_PROMPT_IS_ACTIVE`, `UI_PROMPT_IS_PRESSED`, and `UI_PROMPT_IS_JUST_PRESSED`) separately from raw control state (`IS_CONTROL_ENABLED`, `IS_CONTROL_PRESSED`, and `IS_CONTROL_JUST_PRESSED`).

A native invocation or an active-state result does not prove the prompt was drawn. The in-game visual observation is still required. The existing ambient-group wrapper signature ends in `System.String`; therefore, the test passes the group label directly as a string.

## Target prompt group activation

Prompts assigned to a group must have that group activated for the frame. Since this test attaches its prompt to a target NPC’s group, it calls `UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` on each tick while the same target is focused and the prompt group is valid.

The test uses the target handle, an 8.0-unit radius, `p2` value `2`, a zero tab count, the target prompt-group ID, the `"NPC"` group label, and flags `0`. Invocation is logged once per focused target. That log confirms the native call was attempted; it does not confirm the prompt rendered.

## Next test

- Build and run the replacement `Test.cs`.
- Focus a nearby living human NPC.
- Record whether **G Recruit** appears on screen.
- Press G once while focused and report whether prompt-level and raw-control just-pressed events are logged.
- Check `Test_Log.txt` for prompt creation, target-group attachment, and `AMBIENT GROUP ACTIVATION INVOKED`.
- Report compile/runtime errors and the visual result separately.
- This test does not initiate recruitment or an animation.
