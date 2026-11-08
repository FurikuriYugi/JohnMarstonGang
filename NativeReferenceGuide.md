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

## API and project-wide notes

- Project API version: ScriptHookRDR2DotNet-V2 **2.2.0**.
- Generated native wrappers are in `RDR2.Native`.
- `Script.Tick` is an event; subscribe to it rather than overriding it.
- Use the API DLL and ASI from the same release archive.
- Verify exact wrapper signatures against the project’s API context files or Visual Studio Object Browser before documenting them as verified.
- `System.Text.Json` must not be used; use Newtonsoft.Json if JSON is needed. This is a project constraint, not a native API result.
- API release notes record implicit conversions between `int`, `PoolObject`-derived types such as `Entity` and `Ped`, and `Player`; check the generated declaration when using output parameters.

## Focused interaction target

### `RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`

- **Verified wrapper signature:** `public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(int player, int* outEntity, bool p2, bool p3)`
- **Signature evidence:** Previously checked in Visual Studio Object Browser.
- **Runtime status:** Confirmed working in the focused-target test case.
- **Observed behavior:** The test found a focused target; the logged target was identified as human.
- **Conditions/caveat:** This finding applies to the tested focused-target scenario, not every game state.
- **User observations:** The user has confirmed focused-target detection works.
- **Project test record:** See the focused-target test in `ResearchNotes.md`.

### `RDR2.Native.PED.IS_PED_HUMAN`

- **Signature:** Not recorded here; verify in the API context or Object Browser before adding it.
- **Runtime status:** Confirmed for the tested focused NPC.
- **Evidence:** Test source calls the native; earlier `Test_Log.txt` records `human=True`.
- **Conditions/caveat:** This confirms the tested NPC classification result, not every possible ped type.

## Prompt text creation

### `RDR2.Native.MISC.VAR_STRING`

- **Verified wrapper declaration recorded from API research:** `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- **Custom prompt test usage:** `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- **User-observed result:** `Recruit` appeared in the screen diagnostic drawn through `UI._DISPLAY_TEXT`, but did not appear in the focused-target prompt menu.
- **Upstream-reported limitation:** ScriptHookRDR2DotNet-V2 issue #4 reports that strings created with `VAR_STRING` do not work when passed to `_UI_PROMPT_SET_TEXT`; it cites registered labels such as `CUTSCENE_SKIP` as working examples.
- **Current diagnostic:** A static registered-label prompt is being tested. It sets `VAR_STRING(10, "CUTSCENE_SKIP")` once before registration.
- **Caveat:** The upstream issue is not a runtime verification of every installed 2.2.0 setup.
- **Project test record:** See `ResearchNotes.md`.

### `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`

- **Signature:** Not recorded here; verify in the API context or Object Browser before adding it.
- **Custom-text test usage:** Passes the string returned by `VAR_STRING(10, "LITERAL_STRING", "Recruit")` to this native.
- **Local runtime status:** The user reports that custom `Recruit` did not appear. In a later phase test that alternated custom `Recruit` and registered `CUTSCENE_SKIP`, the user reports seeing no prompt and pressing G; the log for that run has not been provided.
- **Upstream-reported behavior:** Issue #4 says custom `VAR_STRING` strings do not work with this native, while already-registered string-database labels can work.
- **Current follow-up test:** Sets `CUTSCENE_SKIP` before prompt registration, then tests it in the focused-target group.
- **Caveat:** The static registered-label test has not yet been reported. The upstream issue does not establish the result for every local API/runtime setup.
- **Project test record:** See `ResearchNotes.md`.

### `RDR2.Native.UI._DISPLAY_TEXT`

- **Test usage:** Receives `VAR_STRING(10, "LITERAL_STRING", "Recruit")` directly, with screen coordinates `0.5f, 0.08f`.
- **User-observed status:** The user saw `Recruit` at the top center while focused on a human NPC.
- **Caveat:** The log records a call attempt or exception; visual display is a user-reported result, not something the log can establish.
- **Signature:** Verify against the API context or Object Browser if needed.

## Focused-entity prompt group

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Confirmed working in the focused-target test case; the call returned a group ID used by the test.
- **Evidence:** Earlier test source and log.
- **Caveat:** The numeric group ID is runtime-specific; do not treat the logged value as a constant.

### `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Prompt assignment was logged for the focused target; the prompt subsequently reported active in the earlier logged run.
- **Caveat:** Assignment and active state do not prove that text or a glyph was visibly rendered.
- **User observations:** The user reports that custom `Recruit` did not appear and that the later alternating-label test showed no prompt.

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ACTIVE_PAGE`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Observed in the focused-target test.
- **Observed result:** The earlier log records active page `-1`, then `0` while the focused target’s group is active. The test treats values below zero as inactive and zero or greater as active.
- **Caveat:** An active page is not proof that the prompt text or glyph was rendered.
- **Evidence:** Earlier `Test.cs` and `Test_Log.txt` dated `2026-11-08`.

## Prompt controls and state

### `INPUT_INTERACT_OPTION1`

- **Hash used by test:** `0x760A9C6F`.
- **Test mapping:** G.
- **Runtime status:** Configured for the `Recruit` prompt and the new static-label prompt.
- **User observation:** The user reports pressing G during the recent run.
- **Caveat:** No log for that run has been supplied, so it is unknown whether raw G input or a prompt-pressed event was detected.

### `INPUT_INTERACT_OPTION2`

- **Hash used by an earlier comparison test:** `0x84543902`.
- **Test mapping:** H.
- **Status:** Used by an earlier diagnostic prompt; not used by the current single-prompt test.

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
- **Runtime status:** Prompt creation and active state were logged in an earlier focused-target run.
- **Unverified:** Visible rendering of the current static registered-label prompt and successful G activation.
- **Caveat:** Prompt creation or active state alone is not proof of visible output.

## Prompt-group activation context

- General prompt-group research says active and ambient groups are activated every frame to display them.
- Active groups are exclusive; ambient groups have different activation behavior.
- The focused-target test uses the ILO procedure: obtain the entity’s prompt group and assign the prompt to it. In the earlier logged run, that group’s active page changed to `0` without the test calling `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- Treat that as a result observed in the tested ILO case, not as proof that all groups activate automatically.
