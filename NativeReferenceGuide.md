# Native Reference Guide

This file records ScriptHookRDR2DotNet-V2 API and native findings. Distinguish wrapper signatures, static reverse-engineering evidence, runtime logs, upstream reports, and user observations.

## Project-wide notes

- Project API version: ScriptHookRDR2DotNet-V2 2.2.0.
- Generated wrappers are in `RDR2.Native`.
- `Script.Tick` is an event; subscribe to it rather than overriding it.
- Verify wrapper signatures against the project’s `Context/` mapping or Visual Studio Object Browser before treating them as verified.
- See `ResearchNotes.md` for test history and current status.

## Focused interaction target

### `RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`

- **Verified wrapper signature:** `public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(int player, int* outEntity, bool p2, bool p3)`
- **Runtime status:** Confirmed working in the focused-target test scenario.
- **Caveat:** This is not a claim about every game state.

### `RDR2.Native.PED.IS_PED_HUMAN`

- **Wrapper signature:** Not recorded; verify against `Context/` or Object Browser.
- **Runtime status:** Confirmed for the tested NPC.

### `RDR2.Native.PED._SET_PED_PROMPT_NAME`

- **Native hash:** `0x4A48B6E03BABB4AC`.
- **Purpose reported by prompt research:** Changes the text shown for a ped when focused; distinct from an ILO action prompt.
- **Wrapper signature:** Not yet verified.
- **User-observed result:** The focused NPC name displayed as `Recruit` in the tested setup.
- **Caveat:** The test does not restore the NPC’s prior name.

## Prompt text

### `RDR2.Native.MISC.VAR_STRING`

- **Wrapper declaration recorded from API research:** `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- **Test form:** `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- **Earlier local test:** The user saw `Recruit` through `UI._DISPLAY_TEXT`, but not through the tested prompt-menu path.
- **Dog Companion Ghidra evidence:** `FUN_180014530` calls `VAR_STRING` with `10`, a pointer to the `LITERAL_STRING` format token, and its text parameter; the result is passed directly to `_UI_PROMPT_SET_TEXT`.
- **User observation:** Dog Companion displays B Bond using the localized `Bond` prompt text.
- **Conclusion:** The earlier screen-display and prompt-menu results differ. Do not generalize either result beyond its tested conditions.

### `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`

- **Wrapper signature:** Not recorded; verify.
- **Dog Companion Ghidra usage:** Receives the return value from `VAR_STRING(10, "LITERAL_STRING", text)`.
- **Local test result:** The user reported that custom `Recruit` did not appear in the tested prompt menu.

### `RDR2.Native.UI._DISPLAY_TEXT`

- **Test usage:** Receives `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- **User observation:** `Recruit` appeared near the top center in the screen-display comparison.

## Prompt creation and state

### Ghidra-mapped native hashes

The following mappings were identified from Dog Companion’s Ghidra callsites:

- `0x04F97DE45A519419` — `_UI_PROMPT_REGISTER_BEGIN`
- `0xB5352B7494A08258` — `_UI_PROMPT_SET_CONTROL_ACTION`
- `0xFA925AC00EB830B9` — `VAR_STRING`
- `0x5DD02A8318420DD7` — `_UI_PROMPT_SET_TEXT`
- `0x2F11D3A254169EA4` — `_UI_PROMPT_SET_GROUP`
- `0xCC6656799977741B` — `_UI_PROMPT_SET_STANDARD_MODE`
- `0x94073D5CA3F16B7B` — `_UI_PROMPT_SET_HOLD_MODE`
- `0x32DF729D8BD3C1C6` — `_UI_PROMPT_SET_MASH_MODE`
- `0x560E76D5E2E1803F` — `_UI_PROMPT_SET_ATTRIBUTE`
- `0xF7AA2696A22AD8B9` — `_UI_PROMPT_REGISTER_END`
- `0x8A0FB4D03A630D21` — `_UI_PROMPT_SET_ENABLED`
- `0x71215ACCFDE075EE` — `_UI_PROMPT_SET_VISIBLE`

The exact managed wrapper signatures for these calls should be checked against `Context/` or Object Browser.

### Dog Companion prompt factory

- `FUN_180014530` constructs the prompt with control, `VAR_STRING` text, group `(0,0)`, selected mode, attribute `0x12 = 1`, and register-end.
- `FUN_180014510` calls that factory with mode `0` (standard).
- `FUN_180014980` obtains the entity’s prompt group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` and applies it through `_UI_PROMPT_SET_GROUP`.
- `FUN_1800147F0` directly wraps `_UI_PROMPT_SET_GROUP(prompt, groupId, tab)`.
- `FUN_180014A30` sets the wrapper’s visible flag and applies it.
- `FUN_180014970` sets the enabled flag and applies it.
- `FUN_180014870` clears the visible flag and applies the hidden/disabled state.
- `FUN_180014A40` applies enabled/visible state through the two prompt natives.

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`

- **Wrapper signature:** Not recorded; verify.
- **Ghidra evidence:** Dog Companion uses it with the target entity to obtain a prompt-group ID.
- **Runtime status:** The project test also obtained a group ID for a focused target.
- **Caveat:** Group IDs are runtime-specific.

### `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`

- **Wrapper signature:** Not recorded; verify.
- **Ghidra usage:** Dog Companion assigns the prompt to the target entity’s group with tab `0`.
- **Earlier tests:** Assignment and active-page results were logged, but those do not prove visible rendering.

### Prompt state natives

- `_UI_PROMPT_SET_ENABLED` and `_UI_PROMPT_SET_VISIBLE` are called with a prompt handle and a boolean-like state.
- Dog Companion shows the prompt by setting visible, then enabled; its hidden path disables and hides.
- Prompt-active status does not prove visual rendering.

### `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT`

- **Wrapper signature and argument meanings:** Not verified against the API map.
- **Tests:** Both the prompt-handle argument and `prompt=0` variants were tried. The user reported that neither G Skip nor G Recruit appeared in the unarmed lock-on menu.
- **Conclusion:** ILO registration has not been shown to solve this prompt-display problem. The current Dog Companion comparison test omits it.

## Controls

### `INPUT_INTERACT_OPTION1`

- **Hash used by test:** `0x760A9C6F`.
- **Test mapping:** G.
- **Runtime logs:** Raw G press and prompt-pressed events were recorded in earlier tests.
- **Caveat:** Input detection does not prove visible rendering.

### Dog Companion B control

- The user identified B as the control used for B Bond.
- Ghidra shows the control hash being obtained from a string constant through a string-to-hash native call.
- The literal control-name string should be documented from the corresponding data address if needed; do not use Dog Companion’s static address in project code.

## Dog Companion localization

- **File:** `Lang.data` beside `DogCompanion.asi`.
- **Entry:** `interaction.accompany.promptLabel=Bond`.
- **User observation:** The resulting action appears as B Bond.
- **Ghidra path:** The resolved label is passed to the prompt factory as text.
