# G Recruit lock-on prompt research

## Objective

Display **G Recruit** in the unarmed lock-on menu when the player focuses on a human NPC.

This is a prompt-display test only. Do not recruit the NPC or change gang membership.

## Safety constraints

- Do not press G during display tests.
- Do not add recruitment or gang-membership actions to the test.
- Delete test prompts when the target changes or the script aborts.
- Save each test run's log separately before running another test.

## API map findings

The ScriptHookRDRNetAPI inventory used for this research is version `2.2.0.0`.

A standard HUD prompt and its prompt group are separate pieces of state:

1. Create and register the prompt.
2. Assign it to the focused entity's prompt group with `_UI_PROMPT_SET_GROUP`.
3. Activate that group every frame with `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.

Assigning a prompt to a group does **not** activate the group. The active-group call must be made each frame that the group should be displayed. Active prompt groups are exclusive for that frame, so the test calls the native only while a human entity is the current focused target.

The API map documents these relevant natives:

- `RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN()`
- `RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(int prompt, int control)`
- `RDR2.Native.HUD._UI_PROMPT_SET_TEXT(int prompt, string text)`
- `RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(int prompt, int attribute, bool enabled)`
- `RDR2.Native.HUD._UI_PROMPT_REGISTER_END(int prompt)`
- `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(int entity)`
- `RDR2.Native.HUD._UI_PROMPT_SET_GROUP(int prompt, int groupId, int tab)`
- `RDR2.Native.HUD._UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME(int groupId, string name, int tabAmount, int tabDefaultIndex, int p4, int prompt)`
- `RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(int prompt, bool visible)`
- `RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(int prompt, bool enabled)`
- `RDR2.Native.HUD._UI_PROMPT_DELETE(int prompt)`
- `RDR2.Native.MISC.VAR_STRING(int format, string type, string text)`

The Dog Companion Ghidra trace shows a standard prompt being created with a `VAR_STRING` label, assigned to the target entity's prompt group, and then made visible and enabled. No ILO-registration call appears in the traced creation-and-attachment path.

`REGISTER_INTERACTION_LOCKON_PROMPT` is not the operation that activates a custom G action in the focused entity's HUD prompt group. Earlier ILO-only tests returned success but did not make G Recruit visible.

## Test history and results

### Standard HUD prompt without active-group-per-frame call

The initial standard prompt test used:

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) for G.
- `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- Prompt attribute `0x12` enabled.
- The focused human NPC's target prompt group, tab 0.
- Visible and enabled prompt state.

The prompt was reported active and assigned to the target group, but the user observed that **G Recruit did not appear**.

The test did not activate the target group every frame. Therefore, the prior result does not test whether a per-frame active-group call makes the prompt visible.

### Combined HUD prompt and ILO test

The test added `REGISTER_INTERACTION_LOCKON_PROMPT` while preserving the standard HUD prompt setup.

The ILO prompt argument was tested as both:

- The registered HUD prompt handle.
- `0`.

The approach-prompt flag was also tested in the reported runs. The user observed that **G Recruit did not appear**.

### ILO-only test

The ILO-only run logged:

- Focused target: `199170`
- Human target: `True`
- ILO registration: `True`
- ILO unregister: `True`

The user observed that **G Recruit did not appear**.

This shows that target detection and ILO registration/unregistration succeeded in that run. It does not show that an ILO registration activates or displays a custom HUD prompt group.

## Current conclusion

The most relevant missed step in the earlier HUD prompt test was activating the focused entity's prompt group every frame. The prompt had been assigned to the group, but the group had not been activated with `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.

## Current test: focused entity-group activation

`Test.cs` now:

- Detects the currently focused human entity.
- Gets that entity's prompt-group ID.
- Creates a standard HUD prompt using `INPUT_INTERACT_OPTION1` and the text `Recruit`.
- Assigns the prompt to the entity's group.
- Makes the prompt visible and enabled.
- Calls `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` every tick while that entity remains focused.
- Does not register an ILO prompt.
- Does not perform any action when G is pressed.

Run the test in the same unarmed lock-on scenario. Do not press G.

Record:

- Whether G Recruit appears.
- The `FOCUSED TARGET` line.
- The `TARGET GROUP` line.
- The `PROMPT CREATED` line.
- The `ACTIVE GROUP SET THIS FRAME` line.
- Any native errors.

## Interpreting the current test

- If G Recruit appears, the missing per-frame group activation was the cause of the earlier display failure.
- If it does not appear, check the log first. Confirm that the focused human target and nonzero group ID were found, and that the active-group call ran without error.
- If all those conditions are met but G Recruit remains invisible, investigate prompt-group tab assignment and whether the game's lock-on menu is replacing the activated group on that frame. Do not return to ILO argument variations unless new API-map evidence supports doing so.
