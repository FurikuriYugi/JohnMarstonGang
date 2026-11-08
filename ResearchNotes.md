# Research Notes

## Read this first: purpose and preservation rules

This file tracks the project’s investigations: goals, test status, instrumented results, user observations, conclusions, and next steps. It should be understandable without prior chat context.

- Preserve existing findings by default. Do not delete or overwrite a finding for formatting, brevity, or reorganization.
- Change or remove a finding only when verified evidence establishes the correction. Record the evidence and what changed.
- If new evidence conflicts but does not settle the question, preserve both findings and mark the issue conflicting or inconclusive.
- Keep instrumented/logged results separate from user observations. Record the user’s observation in its own section; do not silently turn it into a general API claim.
- At the start of a session, read this file, `NativeReferenceGuide.md`, the current test source, and the latest relevant test log.
- Do not repeat a solved test unless code, environment, or test conditions changed, or a new result contradicts it.
- Put reusable ScriptHookRDR2DotNet-V2 API findings in `NativeReferenceGuide.md` and cross-reference them here.
- While in test mode, do not change `StoryBoard.md`.

## Current test: focused-target prompt in the unarmed focus menu

### Goal

Add the G glyph and the string `Recruit` to the unarmed focus menu for a focused human NPC. This prompt is a test only; pressing it must not recruit the NPC or change gang membership.

### Status

**In progress.** Focused-target detection, assignment to the focused target’s prompt group, and an active-page transition have been demonstrated. The user reports that `Recruit` did not appear. The user has also confirmed that a prompt configured with G and the known `CUTSCENE_SKIP` text displays **G Skip**. The current test now isolates the `VAR_STRING` call sequence for `Recruit`; it has not yet been run.

### Established from prior user testing

The user has confirmed:

- An approach label can be added.
- The focused target can be detected.
- Items can be added to the menu.
- A prompt using G with the known `CUTSCENE_SKIP` text displays **G Skip**.

These are settled project findings. Do not repeat those tests absent a specific reason to do so.

### Established by the focused-target test source and log

- The test gets the focused target using `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`.
- The test checks human status using `PED.IS_PED_HUMAN`.
- The test obtains the target’s prompt group using `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`.
- The test assigns its prompt with `_UI_PROMPT_SET_GROUP`.
- The configured test control `INPUT_INTERACT_OPTION1` has hash `0x760A9C6F` and is the G control used by the test.
- The test builds `Recruit` with `MISC.VAR_STRING(10, "LITERAL_STRING", "Recruit")` and passes the result to `_UI_PROMPT_SET_TEXT`.
- The `VAR_STRING` wrapper declaration recorded from API research is `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- The test does not call `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- An active prompt/group does not prove the prompt was visibly rendered or that its text or glyph was correct.

### Latest comparison-test log

`Test_Log.txt` dated `2026-11-08 12:45` records:

- Both comparison prompts were created.
- The target changed from none to a focused human NPC.
- Both prompts were assigned to the focused target’s prompt group.
- The group active-page query changed from `-1` to `0`.
- Both prompts reported active while the target was focused and inactive after focus was lost.
- The log records raw G and H input detections and corresponding prompt-pressed entries.

These log entries establish input detection and reported prompt-active state. They do **not** establish what text or glyph was visible.

### User observations

- The user reports that `Recruit` did not appear in the unarmed focus menu.
- The user confirms that the known `CUTSCENE_SKIP` label with the G control appears as **G Skip**.
- The user has not reported that the `Recruit` text appeared in the menu.

Keep these as user observations; do not represent them as logged results from the latest comparison-test log.

### Prompt-group research retained with scope

- General prompt-group research says active or ambient groups are activated each frame to display them.
- Active groups are exclusive and may interfere with other active groups; ambient groups are another activation mechanism.
- Interaction Lock-On (ILO) guidance uses a separate process: get the focused entity’s group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign a prompt with `_UI_PROMPT_SET_GROUP`.
- In the focused-target test, the entity’s group became active without a call to `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. This is an observed result for the tested ILO case; it does not replace or disprove the general active/ambient-group guidance.
- Avoid repeating group-activation experiments unless a later result shows the ILO group is no longer active or the test conditions change.

### Upstream research lead

An upstream ScriptHookRDR2DotNet-V2 report titled “Prompt Text doesn't work with custom strings” says custom text made with `VAR_STRING` does not work with `_UI_PROMPT_SET_TEXT` in the reported setup; it names database-registered strings such as `CUTSCENE_SKIP` as a working alternative. This is an upstream report, not verified behavior for this project’s installed API version.

### Current test: direct `VAR_STRING` result

- **Question/goal:** Does passing the `VAR_STRING` result directly to `_UI_PROMPT_SET_TEXT`, after setting the G control and with no intervening native call, make `Recruit` display?
- **Status:** In progress; test source prepared, not yet run.
- **Test conditions:** One `Recruit` prompt using G, assigned to the focused human target’s prompt group. The test sets the control action first and calls `VAR_STRING(10, "LITERAL_STRING", "Recruit")` directly as the `_UI_PROMPT_SET_TEXT` argument.
- **Instrumented results:** None yet for this test.
- **User observations:** Prior `Recruit` text was not visible. User confirms the known `CUTSCENE_SKIP` label displays as **G Skip** when assigned to G.
- **Conclusion:** G display with a known label is already confirmed by the user. The remaining question is whether the custom text path or the method of passing its result is responsible for the missing `Recruit` text.
- **Do not repeat:** Adding an approach label, focused-target detection, adding a prompt to the target’s group, the observed active-page transition, or the known G Skip test.
- **Next test:** Run the current `Test.cs`. Report separately whether the `Recruit` text is visible, whether the G glyph is visible, whether G input is logged, whether the prompt reports active, and whether the prompt-pressed entry is logged.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`, focused-target prompt-group natives; see `NativeReferenceGuide.md`.

## Do not repeat without a specific reason

- Adding an approach label.
- Focused-target detection.
- Adding a prompt to the focused target’s group.
- The observed active-page transition in the logged ILO test.
- The known G Skip display test.

## Template for future test entries

For each new or materially changed test, add an entry with:

- **Question/goal:**
- **Status:** Not tested / In progress / Confirmed / Failed / Inconclusive
- **Test conditions:** Test source version, relevant setup, and log/date.
- **Instrumented results:** What the code or log directly established.
- **User observations:** What the user saw or experienced; state “None reported” if applicable.
- **Conclusion:** What is established and what remains unknown.
- **Do not repeat:** Findings settled for this test.
- **Next test:**
- **API references:** Related `NativeReferenceGuide.md` entries.
