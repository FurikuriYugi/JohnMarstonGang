# Research Notes

This file tracks the objective, reverse-engineering evidence, tests, runtime logs, user observations, conclusions, and next steps. Keep log evidence separate from visual observations. Do not treat prompt-active state or input detection as proof that text rendered in the menu.

## Current objective

Display **G Recruit** as an action prompt in the unarmed lock-on menu for a focused human NPC. This is a prompt-display test only. Pressing G must not recruit the NPC or change gang membership.

## Current status

The user reports that **G Recruit did not appear** in the unarmed lock-on menu.

The latest checked-in `Test_Log.txt` records prompt creation, target-group assignment, visible/enabled calls, and the prompt becoming active. Those log entries do not establish that the prompt text rendered.

The current `Test.cs` created and registered the prompt, then assigned it to the target’s group. However, it looked up the target’s group before prompt construction. The Dog Companion trace constructs/registers its prompt first, then obtains and assigns the target group.

A replacement `Test.cs` has been prepared to follow that order. It passes the literal `Recruit` through `VAR_STRING(10, "LITERAL_STRING", PromptText)`, sets the returned text on the prompt, registers it, then looks up and assigns the focused target’s group, and finally makes the prompt visible and enabled.

**The replacement has not been built or run. No new runtime or visual result is recorded.**

## Dog Companion Ghidra findings

The source is `TestingAndResearch/DogCompanion/GhidraTrace.md`, cross-checked against `NativeReferenceGuide.md`.

- Dog Companion’s language file is `Lang.dat`, beside `DogCompanion.asi`.
- The relevant entry is `interaction.accompany.promptLabel=Bond`.
- In `FUN_180008370`, Dog Companion resolves that key with `FUN_18000bb90(...)`.
- It passes the resolved label and a control hash to `FUN_180014510(...)`.
- `FUN_180014510` calls the prompt factory in standard mode.
- The factory’s prompt-construction path formats the text with `VAR_STRING(10, "LITERAL_STRING", text)`, passes the result to `_UI_PROMPT_SET_TEXT`, initializes the prompt group to `(0, 0)`, sets standard mode and attribute `0x12 = 1`, and ends registration.
- After the factory returns, Dog Companion calls `FUN_180014980(prompt, entity)`. That helper obtains the entity’s prompt group and assigns it to the prompt.
- Dog Companion then makes the prompt visible and enabled, in that order.
- This traced path does not show `REGISTER_INTERACTION_LOCKON_PROMPT`.

The replacement test reconstructs this sequence with the literal `Recruit`. It does not call Dog Companion’s private helper.

## Test history and observations

### Focused target and group

Earlier tests established that:

- `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` found the focused target in the tested scenario.
- `PED.IS_PED_HUMAN` identified the tested NPC as human.
- `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` returned a group ID.
- `_UI_PROMPT_SET_GROUP` assigned the test prompt to that group.
- Prompt-active and active-page results did not prove visible rendering.

### Earlier `VAR_STRING` and screen-display comparison

A screen-display comparison passed `VAR_STRING(10, "LITERAL_STRING", "Recruit")` to `UI._DISPLAY_TEXT`.

**User observation:** `Recruit` appeared near the top center of the screen, not in the prompt menu.

This demonstrates the screen-display path for that test only. It does not establish prompt-menu rendering.

### Registered-label and focused-ped tests

A registered-label test used `CUTSCENE_SKIP` with G. Logs recorded a focused target, group assignment, active-page transition, active prompt state, raw G input, and a prompt-pressed event.

**User observation:** G Skip did not appear in the unarmed lock-on menu.

`_SET_PED_PROMPT_NAME` also made the focused NPC name display as `Recruit` in the tested setup, and the user saw `Recruit` as the outer-radius label. Those are separate display elements and are not evidence that the action prompt rendered.

### ILO prompt-handle test

`Test_Log.txt` dated `2026-11-08 17:05` records a created prompt handle, a focused human target, successful ILO registration using the handle, target-group assignment, an active-page change, an active prompt, raw G input, and a prompt-pressed event.

**User observation:** G Skip did not appear.

### ILO `prompt=0` test

`Test_Log.txt` dated `2026-11-08 22:38` records ILO registration with `promptArg=0`, target-group assignment, visible/enabled calls, and the prompt becoming active.

**User observation:** G Recruit did not appear.

Do not repeat either ILO variant unchanged.

### Standard-prompt test log

The checked-in `Test_Log.txt` dated `2026-11-09 11:02–11:03` records a standard G Recruit prompt, target-group assignment, visible/enabled calls, and an active-state change from false to true.

**User observation:** G Recruit did not appear in the unarmed lock-on menu.

The log does not report a visible rendering result; preserve the user observation separately from the runtime state.

## Next step

Build and run the replacement `Test.cs`. Record the fresh log and the user’s visual observation separately. Do not infer visibility from prompt creation, group assignment, active state, or input detection. Do not add recruitment behavior as part of this display test.
