# Native Reference Guide

## Read this first: purpose and preservation rules

This file is the project’s growing database of ScriptHookRDR2DotNet-V2 API findings. It should be understandable without prior chat context.

- Preserve existing API findings by default. Do not remove or overwrite data for formatting, brevity, or reorganization.
- Change or remove an API finding only when verified API-source evidence or a verified test establishes the correction. Record the evidence and what changed.
- If new evidence conflicts but does not settle the question, preserve both results and mark the issue conflicting or inconclusive.
- Clearly distinguish verified wrapper signatures, runtime-tested behavior, upstream-reported behavior, and user observations.
- For each entry, record API name, exact signature if verified, status, tested conditions, evidence, caveats, and user observations.
- Do not promote an upstream report or a user observation into a general API behavior claim without verification.
- Read this guide alongside `ResearchNotes.md`, current test source, and the latest test log before researching or repeating a test.
- Use `ResearchNotes.md` for overall test progress and next steps; cross-reference relevant entries here.

## API and project-wide notes retained from the prior guide

- Project API version: ScriptHookRDR2DotNet-V2 **2.2.0**.
- Generated native wrappers are in `RDR2.Native`.
- `Script.Tick` is an event; subscribe to it rather than overriding it.
- Use the API DLL and ASI from the same release archive.
- Verify exact wrapper signatures against the project’s API context files or Visual Studio Object Browser before documenting them as verified.
- The prior guide records that `System.Text.Json` must not be used and Newtonsoft.Json should be used if JSON is needed. This is a project constraint, not a native API result.
- The prior guide records API release-note information that implicit conversions exist between `int`, `PoolObject`-derived types such as `Entity` and `Ped`, and `Player`; check the generated declaration when using output parameters.

## Focused interaction target

### `RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`

- **Verified wrapper signature:** `public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(int player, int* outEntity, bool p2, bool p3)`
- **Signature evidence:** Previously checked in Visual Studio Object Browser.
- **Runtime status:** Confirmed working in the focused-target test case.
- **Observed behavior:** The test found a focused target; the logged target was identified as human.
- **Conditions/caveat:** This finding applies to the tested focused-target scenario, not every game state.
- **User observations:** The user has confirmed focused-target detection works. The comparison-test log records `nativeFound=True` for a focused NPC.
- **Project test record:** See the focused-target test in `ResearchNotes.md`.

### `RDR2.Native.PED.IS_PED_HUMAN`

- **Signature:** Not recorded here; verify in the API context or Object Browser before adding it.
- **Runtime status:** Confirmed for the tested focused NPC.
- **Evidence:** Test source calls the native and the comparison-test log records `human=True`.
- **Conditions/caveat:** This confirms the tested NPC classification result, not every possible ped type.
- **User observations:** No separate observation recorded.

## Prompt text creation

### `RDR2.Native.MISC.VAR_STRING`

- **Verified wrapper declaration recorded from API research:** `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- **Test usage:** `VAR_STRING(10, "LITERAL_STRING", "Recruit")`, with the result passed to `_UI_PROMPT_SET_TEXT`.
- **Runtime status:** The comparison test created and activated a prompt. The log does not prove that the returned string was displayed.
- **Current test:** The new test sets the G control first, then passes the direct `VAR_STRING` result as the `_UI_PROMPT_SET_TEXT` argument, with no intervening native call.
- **Caveat:** Successful invocation and an active prompt do not establish that custom prompt text rendered correctly. The direct-call sequencing hypothesis is untested until the new test is run.
- **User observations:** The user reports that `Recruit` did not appear in the menu.
- **Project test record:** See `ResearchNotes.md`.

### `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`

- **Signature:** Not recorded here; verify in the API context or Object Browser before adding it.
- **Prior test usage:** Passed the string returned by `VAR_STRING` to this native.
- **Local runtime status:** Prompt creation and active state were logged, but the user reports that `Recruit` was not visible. The comparison-test log does not establish the displayed text.
- **Upstream-reported status:** An upstream ScriptHookRDR2DotNet-V2 issue titled “Prompt Text doesn't work with custom strings” reports that custom strings created with `VAR_STRING` do not work with `_UI_PROMPT_SET_TEXT` in the reported setup. The report names database-registered labels including `CUTSCENE_SKIP`. This has not been verified against the project’s installed API version.
- **User observation:** The user confirms that the known `CUTSCENE_SKIP` label with G displays as **G Skip**. This confirms the G-and-label case in the user’s test, not custom `VAR_STRING` behavior.
- **Next verification:** Run the direct-result `VAR_STRING` test and separately observe the text, glyph, input detection, and prompt-active state.

## Focused-entity prompt group

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Confirmed working in the focused-target test case; the call returned a group ID used by the test.
- **Evidence:** Test source and comparison-test log.
- **Caveat:** The numeric group ID is runtime-specific; do not treat the logged value as a constant.

### `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Prompt assignment was logged for the focused target; the prompt subsequently reported active.
- **Caveat:** Assignment and active state do not prove that text or a glyph was visibly rendered.
- **User observations:** The user reports that the expected `Recruit` entry did not appear.

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ACTIVE_PAGE`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Observed in-game in the focused-target test.
- **Observed result:** The comparison-test log records active page `-1`, then `0` while the focused target’s group is active. The test treats values below zero as inactive and zero or greater as active.
- **Caveat:** An active page is not proof that the prompt text or glyph was rendered.
- **Evidence:** Test source and `Test_Log.txt` dated `2026-11-08`.

## Prompt controls and state

### `INPUT_INTERACT_OPTION1`

- **Hash used by test:** `0x760A9C6F`.
- **Test mapping:** G.
- **Runtime status:** The current test configures this for the `Recruit` prompt.
- **Input evidence:** The comparison-test log records raw G input and a prompt-pressed entry.
- **User observations:** The user confirms the G glyph appears with the known `CUTSCENE_SKIP` label as **G Skip**.

### `INPUT_INTERACT_OPTION2`

- **Hash used by the previous comparison test:** `0x84543902`.
- **Test mapping:** H.
- **Status:** Previously configured as the database-label diagnostic prompt’s control.
- **Caveat:** The current test no longer uses this control.

### Prompt functions used by the test

The focused-target prompt test uses:

- `_UI_PROMPT_REGISTER_BEGIN`
- `_UI_PROMPT_SET_CONTROL_ACTION`
- `_UI_PROMPT_SET_TEXT`
- `_UI_PROMPT_SET_STANDARD_MODE`
- `_UI_PROMPT_REGISTER_END`
- `_UI_PROMPT_SET_ENABLED`
- `_UI_PROMPT_SET_VISIBLE`
- `_UI_PROMPT_IS_ACTIVE`
- `_UI_PROMPT_IS_JUST_PRESSED`
- `_UI_PROMPT_DELETE`
- **Signatures:** Not recorded here; verify in the API context or Object Browser before documenting them.
- **Runtime status:** The prior focused-target test logged prompt creation and an active state while the target was focused.
- **Unverified:** The new test’s visible `Recruit` text and glyph.
- **Caveat:** Prompt creation or active state alone is not proof of visible output.
- **User observations:** The user reports that `Recruit` did not appear.

## Prompt-group activation context

- General prompt-group research says active and ambient groups are activated every frame to display them.
- Active groups are exclusive; ambient groups have different activation behavior.
- The focused-target test uses the ILO procedure: obtain the entity’s prompt group and assign the prompt to it. In the logged run, that group’s active page changed to `0` without the test calling `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- Treat the last item as a result observed in the tested ILO case, not as proof that all groups activate automatically.
