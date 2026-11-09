# Research Notes

This file tracks the objective, reverse-engineering evidence, tests, runtime logs, user observations, conclusions, and next steps. Keep log evidence separate from visual observations. Do not treat prompt-active state or input detection as proof that text rendered in the menu.

## Current objective

Display **G Recruit** as an action prompt in the unarmed lock-on menu for a focused human NPC. This is a prompt-display test only. Pressing G must not recruit the NPC or change gang membership.

## Current status

The user reports that **G Recruit did not appear** in the unarmed lock-on menu during the latest standard-prompt test.

The latest checked-in `Test_Log.txt` is dated `2026-11-09 12:46–12:47`. It records:

- The label input as `Recruit`.
- `VAR_STRING(10, "LITERAL_STRING", PromptText)` returning `Recruit`.
- The returned text being passed to `_UI_PROMPT_SET_TEXT`.
- Prompt registration in standard mode.
- Target-group lookup after registration and group assignment.
- Calls to make the prompt visible and enabled.
- The prompt changing from inactive to active.
- Prompt deletion.

These log entries do not establish that the prompt text rendered.

The current test follows the Dog Companion trace’s construction order: create and register the prompt, then obtain and assign the focused target’s prompt group. It does not use ILO registration or change the ped prompt name.

**Latest test result:** the runtime log shows the expected string value and an active prompt, but the user reports that the label is still not visible in the unarmed lock-on menu.

## Dog Companion research

The source is `TestingAndResearch/DogCompanion/GhidraTrace.md`, cross-checked against the native reference findings below. These findings describe how Dog Companion builds a working prompt. They do not, by themselves, verify the managed wrapper signatures or prove that the project test renders.

### Localization and label

- Dog Companion's language file is `Lang.dat` beside `DogCompanion.asi`.
- The relevant entry is `interaction.accompany.promptLabel=Bond`.
- The user observes this as **B Bond**.
- In `FUN_180008370`, Dog Companion resolves that key with `FUN_18000bb90(...)`.
- It passes the resolved label and a control hash to `FUN_180014510(...)`.

### Control hash

- The B control's hash comes from a string constant passed through a string-to-hash native.
- Do not use Dog Companion's static addresses in project code.
- Document the literal control-name string from its data address if needed.
- The project uses G; control selection is not the current problem under investigation.

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
- The factory formats the text with `VAR_STRING(10, "LITERAL_STRING", text)` and passes the native result directly to `_UI_PROMPT_SET_TEXT`.
- After the factory returns, `FUN_180014980(prompt, entity)` obtains the entity's prompt group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` and applies it through `_UI_PROMPT_SET_GROUP`.
- `FUN_1800147F0` directly wraps `_UI_PROMPT_SET_GROUP(prompt, groupId, tab)`. Dog Companion uses tab `0`.

### Visibility and state

- `FUN_180014A30` sets the visible flag and applies it.
- `FUN_180014970` sets the enabled flag and applies it.
- Dog Companion shows the prompt by making it visible, then enabled.
- `FUN_180014870` clears the visible flag and applies the hidden/disabled state.
- `FUN_180014A40` applies the enabled and visible state through the two prompt natives.
- Group assignment and active-page results do not prove visible rendering. Group IDs are runtime-specific.
- This traced path does not show `REGISTER_INTERACTION_LOCKON_PROMPT`.

## Native reference findings

### Verified wrapper signatures

- `RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`: `public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(int player, int* outEntity, bool p2, bool p3)`. Confirmed working in the focused-target test scenario. This is not a claim about every game state.
- `RDR2.Native.MISC.VAR_STRING`: `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`, recorded from API research.

### Unverified wrapper signatures

Verify these against `Context/` or the Object Browser before relying on their managed argument or return types:

- `RDR2.Native.PED.IS_PED_HUMAN`
- `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`
- `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`
- `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`
- `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT`

In particular, the wrapper and `InputArgument` handling for `VAR_STRING` and `_UI_PROMPT_SET_TEXT` have not yet been confirmed against the API map.

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

The latest checked-in `Test_Log.txt` is dated `2026-11-09 12:46–12:47`. It records:

- `VAR_STRING RESULT: Recruit`
- `PROMPT TEXT SET: Recruit`
- A standard-mode prompt registered with attribute `0x12:true`
- Target-group lookup after registration and assignment with tab `0`
- The prompt made visible, then enabled
- The prompt becoming active
- The prompt subsequently being deleted

**User observation:** G Recruit did not appear in the unarmed lock-on menu.

The log does not report a visible rendering result. Preserve the user's observation separately from runtime state.

### Controls

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) is mapped to G in tests.
- Raw G press and prompt-pressed events were recorded in earlier tests.
- Input detection does not prove visible rendering.
- The user has specified that G is the chosen control. Do not treat control selection as the open issue.

## Current assessment

The latest test establishes that the managed `VAR_STRING` call returned the text `Recruit`, and the prompt reached an active state after being assigned to the focused target's group. It does **not** establish that the prompt menu accepted or rendered the label.

A plausible string-specific lead is a difference in how the text reaches the native:

- The Dog Companion trace passes the native result of `VAR_STRING` directly into `_UI_PROMPT_SET_TEXT`.
- The project wrapper exposes `VAR_STRING` as a managed C# `string`; the test then passes that value to `_UI_PROMPT_SET_TEXT`.
- The log confirms the managed value is `Recruit`, but it does not prove that the wrapper passes the same native representation as Dog Companion.

This is a hypothesis, not a confirmed cause. The next check is the API map's exact signatures and argument handling for `VAR_STRING`, `_UI_PROMPT_SET_TEXT`, and `InputArgument`. If the wrapper converts the native result to ordinary managed text and re-marshals it, test a path that preserves the native `VAR_STRING` result through to `_UI_PROMPT_SET_TEXT`, if the project's API allows it.

Other possible causes remain open, including whether a standard prompt assigned to this target group is rendered by the specific unarmed lock-on menu. Active state, group assignment, and input detection alone cannot distinguish that from a string-marshalling issue.

## Open questions

- Does the managed `VAR_STRING` wrapper return ordinary display text, a native string representation, or a wrapper-specific value suitable for passing back to `_UI_PROMPT_SET_TEXT`?
- What exact managed signature and parameter types does `_UI_PROMPT_SET_TEXT` use?
- Does the API wrapper marshal the `VAR_STRING` return value identically to the raw native result used by Dog Companion?
- Is a standard prompt assigned to the focused entity's prompt group expected to render in this specific unarmed lock-on menu?
- Is there a menu-context or registration requirement not present in the Dog Companion trace?

## Next step

1. Verify the `VAR_STRING` and `_UI_PROMPT_SET_TEXT` wrapper signatures and `InputArgument` conversion in `Context/` or the Object Browser.
2. Compare the wrapper's text-marshalling path with the raw native call sequence in `TestingAndResearch/DogCompanion/GhidraTrace.md`.
3. If the wrapper path differs, run a focused test that changes only how the text is passed to `_UI_PROMPT_SET_TEXT`. Keep the control, prompt mode, group, visibility, and target logic unchanged.
4. Record the fresh runtime log and the user's visual observation separately.
5. If the string path is shown to match, investigate whether this prompt type and target group are rendered by the unarmed lock-on menu.

Do not infer visibility from prompt creation, group assignment, active state, or input detection. Do not add recruitment behavior as part of this display test.
