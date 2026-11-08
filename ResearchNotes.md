# Research Notes

## Focused NPC prompt test

The test is isolated to finding a nearby living human NPC, detecting when the player focuses that NPC, displaying a custom prompt, and recording input. It does not recruit the NPC or change gang membership.

The available `Test_Log.txt` records prompt creation, target-group attachment, prompt-active state, and raw control input. The user reported that **G Recruit was not visible**. A prompt-active result and detection of G do not by themselves establish that the prompt group was rendered.

The prior test attached the prompt with `UI_PROMPT_SET_GROUP` and enabled/visibilized the prompt, but did not activate the target’s ambient prompt group each frame. The replacement test activates the ambient group on every tick while the same NPC remains focused, using `UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME`.

The test uses `UI_PROMPT_SET_STANDARD_MODE` during prompt setup and logs whether the v2.2.0 API wrapper was found and invoked. The log also records the resolved wrapper signatures so a mismatch can be diagnosed.

## Prompt text and input

The prompt control and prompt text are configured separately:

- Control: `INPUT_INTERACT_OPTION1` (`0x760A9C6F`).
- Prompt text: `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- Ambient group label: `VAR_STRING(10, "LITERAL_STRING", "NPC")`.

The test logs prompt-level state (`UI_PROMPT_IS_ACTIVE`, `UI_PROMPT_IS_PRESSED`, and `UI_PROMPT_IS_JUST_PRESSED`) separately from raw control state (`IS_CONTROL_ENABLED`, `IS_CONTROL_PRESSED`, and `IS_CONTROL_JUST_PRESSED`).

`UI_PROMPT_IS_ACTIVE=True` is not treated as proof that the prompt was drawn on screen. The user’s visual observation is recorded separately from native return values.

## Target prompt group activation

Prompts assigned to a group must have that group activated for the frame. Since this test attaches its prompt to a target NPC’s group, it uses the entity ambient-group native rather than an exclusive active group.

The replacement test calls `UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` during each tick in which the focused target and prompt group are valid. It passes the target handle, a radius matching the test’s target-selection range, the documented/common `p2` value of `2`, a single-tab configuration, the target prompt-group ID, a `VAR_STRING` group label, and flags `0`.

The native invocation result only confirms the wrapper was invoked. It does not confirm that the game rendered the prompt. The in-game observation remains necessary.

## Next test

- Build and run the replacement `Test.cs`.
- Focus a nearby living human NPC.
- Record whether **G Recruit** appears on screen.
- Press G once while focused and report whether the prompt-level just-pressed event and raw control just-pressed event are logged.
- Check `Test_Log.txt` for the resolved `UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` signature and `AMBIENT GROUP ACTIVATION INVOKED`.
- Record any animation separately. This test does not initiate recruitment or an animation.
