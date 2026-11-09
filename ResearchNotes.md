# Research Notes

This file tracks the objective, reverse-engineering evidence, tests, runtime logs, user observations, conclusions, and next steps. Keep log evidence separate from visual observations. Do not treat prompt-active state or input detection as proof that text rendered in the menu.

## Current objective

Display **G Recruit** as an action prompt in the unarmed lock-on menu for a focused human NPC. This is a prompt-display test only. Pressing G must not recruit the NPC or change gang membership.

## Current status

The user reports that **G Recruit did not appear** in the unarmed lock-on menu.

The latest checked-in `Test_Log.txt` records prompt creation, target-group assignment, visible/enabled calls, and the prompt becoming active. Those log entries do not establish that the prompt text rendered.

The current `Test.cs` created and registered the prompt, then assigned it to the target's group. However, it looked up the target's group before prompt construction. The Dog Companion trace constructs and registers its prompt first, then obtains and assigns the target group.

A replacement `Test.cs` has been prepared to follow that order. It passes the literal `Recruit` through `VAR_STRING(10, "LITERAL_STRING", PromptText)`, sets the returned text on the prompt, registers it, then looks up and assigns the focused target's group, and finally makes the prompt visible and enabled.

**The replacement has not been built or run. No new runtime or visual result is recorded.**

## Dog Companion research

The source is `TestingAndResearch/DogCompanion/GhidraTrace.md`, cross-checked against the native reference findings below. These findings describe how Dog Companion builds a working prompt. They are not verified wrapper signatures, and prompt-active status does not prove the prompt renders.

### Localization and label

- Dog Companion's language file is `Lang.dat` beside `DogCompanion.asi`.
- The relevant entry is `interaction.accompany.promptLabel=Bond`.
- The user observes this as **B Bond**.
- In `FUN_180008370`, Dog Companion resolves that key with `FUN_18000bb90(...)`.
- It passes the resolved label and a control hash to `FUN_180014510(...)`.

### Control hash

The B control's hash comes from a string constant passed through a string-to-hash native. Do not use Dog Companion's static addresses in project code. Document the literal control-name string from its data address if needed.

### Ghidra-mapped native hashes

These mappings were identified from Dog Companion's Ghidra callsites. Check the managed wrapper signatures against `Context/` or the Object Browser before relying on them.

- `0x04F97DE45A519419`: `_UI_PROMPT_REGISTER_BEGIN`
- `0xB5352B7494A08258`: `_UI_PROMPT_SET_CONTROL_ACTION`
- `0xFA925AC00EB830B9`: `VAR_STRING`
- `0x5DD02A8318420DD7`: `_UI_PROMPT_SET_TEXT`
- `0x2F11D3A254169EA4`: `_UI_PROMPT_SET_GROUP`
- `0xCC6656799977741B`: `_UI_PROMPT_SET_STANDARD_MODE`
- `0x94073D5CA3F16B7B`: `_UI_PROMPT_SET_HOLD_MODE`
- `0x32DF729D8BD3C1C6`: `_UI_PROMPT_SET_MASH_MODE`
- `0x560E76D5E2E1803F`: `_UI_PROMPT_SET_ATTRIBUTE`
- `0xF7AA2696A22AD8B9`: `_UI_PROMPT_REGISTER_END`
- `0x8A0FB4D03A630D21`: `_UI_PROMPT_SET_ENABLED`
- `0x71215ACCFDE075EE`: `_UI_PROMPT_SET_VISIBLE`

Hashes used by project tests:

- `0x4A48B6E03BABB4AC`: `_SET_PED_PROMPT_NAME`. The wrapper signature is not yet verified.
- `0x760A9C6F`: `INPUT_INTERACT_OPTION1`, used for the G control in tests.

### Standard prompt factory

- `FUN_180014530` constructs the prompt with the control, a `VAR_STRING` text argument, group `(0, 0)`, the selected mode, attribute `0x12 = 1`, and then register-end.
- `FUN_180014510` calls that factory with mode `0`, which is the standard mode.
- The factory's text path formats the text with `VAR_STRING(10, "LITERAL_STRING", text)` and passes the result directly to `_UI_PROMPT_SET_TEXT`.
- After the factory returns, `FUN_180014980(prompt, entity)` obtains the entity's prompt group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` and applies it through `_UI_PROMPT_SET_GROUP`.
- `FUN_1800147F0` directly wraps `_UI_PROMPT_SET_GROUP(prompt, groupId, tab)`. Dog Companion uses tab `0`.

### Visibility and state

- `FUN_180014A30` sets the visible flag and applies it.
- `FUN_180014970` sets the enabled flag and applies it.
- Dog Companion shows the prompt by making it visible, then enabled.
- `FUN_180014870` clears the visible flag and applies the hidden/disabled state.
- `FUN_180014A40` applies the enabled and visible state through the two prompt natives.
- Group assignment and active-page results do not prove visible rendering. Group IDs are runtime-specific.

This traced path does not show `REGISTER_INTERACTION_LOCKON_PROMPT`.

The replacement test reconstructs this sequence with the literal `Recruit`. It does not call Dog Companion's private helpers.

## Native reference findings

### Verified wrapper signatures

- `RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`: `public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(int player, int* outEntity, bool p2, bool p3)`. Confirmed working in the focused-target test scenario. This is not a claim about every game state.
- `RDR2.Native.MISC.VAR_STRING`: `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`, recorded from API research.

### Unverified wrapper signatures

- `RDR2.Native.PED.IS_PED_HUMAN`: not recorded; verify against `Context/` or the Object Browser. Confirmed for the tested NPC.
- `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`: not recorded; verify.
- `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`: not recorded; verify.
- `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`: not recorded; verify.
- `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT`: not verified against the API map. Argument meanings are unconfirmed.

### Project-wide notes

- Project API version: ScriptHookRDR2DotNet-V2 2.2.0.
- Generated wrappers are in `RDR2.Native`.
- `Script.Tick` is an event; subscribe to it rather than overriding it.

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

`_SET_PED_PROMPT_NAME` also made the focused NPC name display as `Recruit` in the tested setup. It does not restore the NPC's prior name. The user saw `Recruit` as the outer-radius label. These are separate display elements and are not evidence that the action prompt rendered.

### ILO prompt-handle test

`Test_Log.txt` dated `2026-11-08 17:05` records a created prompt handle, a focused human target, successful ILO registration using the handle, target-group assignment, an active-page change, an active prompt, raw G input, and a prompt-pressed event.

**User observation:** G Skip did not appear.

### ILO `prompt=0` test

`Test_Log.txt` dated `2026-11-08 22:38` records ILO registration with `promptArg=0`, target-group assignment, visible/enabled calls, and the prompt becoming active.

**User observation:** G Recruit did not appear.

Do not repeat either ILO variant unchanged. ILO registration has not been shown to solve this prompt-display problem.

### Standard-prompt test

The checked-in `Test_Log.txt` dated `2026-11-09 11:02–11:03` records a standard G Recruit prompt, target-group assignment, visible/enabled calls, and an active-state change from false to true.

**User observation:** G Recruit did not appear in the unarmed lock-on menu.

The log does not report a visible rendering result. Preserve the user observation separately from the runtime state.

### Controls

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) is mapped to G in tests. Raw G press and prompt-pressed events were recorded. Input detection does not prove visible rendering.

## Open questions

- Earlier screen-display tests showed `Recruit` through `UI._DISPLAY_TEXT`, but the prompt-menu path did not show it. Do not generalize either result beyond its tested conditions.
- The `_SET_PED_PROMPT_NAME` test showed the focused NPC as `Recruit`, but it does not restore the NPC's previous name.
- ILO registration with both the prompt handle and `prompt=0` did not make G Skip or G Recruit appear. The replacement test omits ILO registration.

## Next step

Build and run the replacement `Test.cs`. Record the fresh log and the user's visual observation separately. Do not infer visibility from prompt creation, group assignment, active state, or input detection. Do not add recruitment behavior as part of this display test.
