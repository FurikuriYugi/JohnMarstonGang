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
- **Evidence:** Test source calls the native; `Test_Log.txt` records `human=True`.
- **Conditions/caveat:** This confirms the tested NPC classification result, not every possible ped type.

## Prompt text creation

### `RDR2.Native.MISC.VAR_STRING`

- **Verified wrapper declaration recorded from API research:** `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- **Current test usage:** `VAR_STRING(10, "LITERAL_STRING", "Recruit")`.
- **Runtime status:** The direct result passed to `_UI_PROMPT_SET_TEXT` did not produce visible `Recruit` text according to the user. The prompt did report active while its target group was active.
- **Next diagnostic:** The test also passes the direct `VAR_STRING` result to `UI._DISPLAY_TEXT` while the target is focused.
- **Caveat:** A native call attempt or active prompt does not establish visible output. The screen comparison still requires in-game testing.
- **Project test record:** See `ResearchNotes.md`.

### `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`

- **Signature:** Not recorded here; verify in the API context or Object Browser before adding it.
- **Test usage:** Passes the string returned by `VAR_STRING` to this native.
- **Local runtime status:** Prompt creation and active state were logged. The user reports that `Recruit` did not appear.
- **Upstream release-note context:** ScriptHookRDR2DotNet-V2 2.1.0.1 release notes say prompt text did not yet work at that point. Version 2.2.0 notes describe general internal updates but do not explicitly confirm a prompt-text fix.
- **Caveat:** This release history does not establish whether custom prompt text works or fails in the project’s installed 2.2.0 setup.
- **Project test record:** See `ResearchNotes.md`.

### `RDR2.Native.UI._DISPLAY_TEXT`

- **Test usage:** Receives `VAR_STRING(10, "LITERAL_STRING", "Recruit")` directly, with screen coordinates `0.5f, 0.08f`.
- **Test status:** Prepared; not yet run in-game.
- **Purpose:** Compare `VAR_STRING` display in an ordinary screen-text native with its behavior in `_UI_PROMPT_SET_TEXT`.
- **Caveat:** The log records a call attempt or exception; visual display must be reported by the user.
- **Signature:** Verify against the API context or Object Browser if the test does not compile.

## Focused-entity prompt group

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Confirmed working in the focused-target test case; the call returned a group ID used by the test.
- **Evidence:** Test source and log.
- **Caveat:** The numeric group ID is runtime-specific; do not treat the logged value as a constant.

### `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Prompt assignment was logged for the focused target; the prompt subsequently reported active.
- **Caveat:** Assignment and active state do not prove that text or a glyph was visibly rendered.
- **User observations:** The user reports that `Recruit` did not appear.

### `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ACTIVE_PAGE`

- **Signature:** Not recorded here; verify in the API context or Object Browser before documenting it.
- **Runtime status:** Observed in the focused-target test.
- **Observed result:** The latest log records active page `-1`, then `0` while the focused target’s group is active. The test treats values below zero as inactive and zero or greater as active.
- **Caveat:** An active page is not proof that the prompt text or glyph was rendered.
- **Evidence:** `Test.cs` and `Test_Log.txt` dated `2026-11-08`.

## Prompt controls and state

### `INPUT_INTERACT_OPTION1`

- **Hash used by test:** `0x760A9C6F`.
- **Test mapping:** G.
- **Runtime status:** Configured for the `Recruit` prompt.
- **User observation:** The user confirms that the known `CUTSCENE_SKIP` label displays as **G Skip**.
- **Caveat:** The latest `Test_Log.txt` has no raw G-press or prompt-pressed entry. Do not infer input activation from prompt-active state.

### `INPUT_INTERACT_OPTION2`

- **Hash used by the earlier comparison test:** `0x84543902`.
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
- **Runtime status:** Prompt creation and active state were logged in the focused-target case.
- **Unverified:** Visible rendering of custom prompt text and successful G activation.
- **Caveat:** Prompt creation or active state alone is not proof of visible output.

## Prompt-group activation context

- General prompt-group research says active and ambient groups are activated every frame to display them.
- Active groups are exclusive; ambient groups have different activation behavior.
- The focused-target test uses the ILO procedure: obtain the entity’s prompt group and assign the prompt to it. In the logged run, that group’s active page changed to `0` without the test calling `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- Treat the last item as a result observed in the tested ILO case, not as proof that all groups activate automatically.
