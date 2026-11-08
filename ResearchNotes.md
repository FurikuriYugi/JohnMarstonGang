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
**In progress.** Target detection and adding a prompt to the focused target’s prompt group have been demonstrated. The user reports that `Recruit` did not appear. The next test compares a custom `VAR_STRING` prompt with a database-label diagnostic prompt.

### Established from prior user testing
The user has confirmed:
- An approach label can be added.
- The focused target can be detected.
- Items can be added to the menu.

These are settled project findings. Do not repeat those tests absent a specific reason to do so.

### Established by the current test source and log
- The test gets the focused target using `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`.
- The test checks human status using `PED.IS_PED_HUMAN`.
- The test obtains the target’s prompt group using `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`.
- The test assigns its prompt with `_UI_PROMPT_SET_GROUP`.
- The configured test control `INPUT_INTERACT_OPTION1` has hash `0x760A9C6F` and is the G control in the current test.
- The test builds `Recruit` with `MISC.VAR_STRING(10, "LITERAL_STRING", "Recruit")` and passes the resulting string to `_UI_PROMPT_SET_TEXT`.
- `VAR_STRING` wrapper declaration recorded from the API research: `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- `Test_Log.txt` dated `2026-11-08 12:05` records a focused human target, prompt-group assignment, the group active page changing from `-1` to `0`, and the prompt becoming active.
- The test does not call `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- An active prompt/group does not prove the prompt was visibly rendered or that its text or glyph was correct.

### Latest test record

**Instrumented/logged results from `Test_Log.txt`:**
- Prompt creation was logged.
- The target changed from none to a focused human NPC.
- The prompt was assigned to the target’s prompt group.
- The group active-page query changed from `-1` to `0`.
- The prompt reported active while the target was focused and inactive after focus was lost.
- The log has no `RAW INPUT_INTERACT_OPTION1 PRESS DETECTED` or `RECRUIT PROMPT PRESSED` entry. Therefore this log does not establish that G was pressed or the prompt was activated by input.

**User observations:**
- The user reports that `Recruit` did not appear in the unarmed focus menu.
- The user has not reported that the G glyph appeared.

**Correction to an earlier note:**
An earlier note said the log showed a detected prompt press and did not establish whether the group was active. The current `Test_Log.txt` contradicts those statements: it contains no prompt-press entry, and it records the group active page changing to `0`. The note is corrected here based on the current log.

### Prompt-group research retained with scope
- General prompt-group research says active or ambient groups are activated each frame to display them.
- Active groups are exclusive and may interfere with other active groups; ambient groups are another activation mechanism.
- Interaction Lock-On (ILO) guidance uses a separate process: get the focused entity’s group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign a prompt with `_UI_PROMPT_SET_GROUP`.
- In this test, the focused entity’s group became active without a call to `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. This is an observed result for the tested ILO case; it does not replace or disprove the general active/ambient-group guidance.
- Avoid repeating group-activation experiments unless a later result shows the ILO group is no longer active or the test conditions change.

### New research lead
An upstream ScriptHookRDR2DotNet-V2 report titled “Prompt Text doesn't work with custom strings” says custom text made with `VAR_STRING` does not work with `_UI_PROMPT_SET_TEXT` in the reported setup; it names database-registered strings such as `CUTSCENE_SKIP` as a working alternative. This is an upstream report, not yet verified with this project’s installed API version. The current test compares the `Recruit` `VAR_STRING` prompt with a direct `CUTSCENE_SKIP` token diagnostic before treating that as the cause. <citation src="21"></citation>

### Do not repeat without a specific reason
- Adding an approach label.
- Focused-target detection.
- Adding a prompt to the focused target’s group.
- The observed active-group-page transition in the logged ILO test.

### Next result to record
For the comparison test, separately record:
1. Whether the `Recruit`/G prompt is visible.
2. Whether the `CUTSCENE_SKIP`/H diagnostic prompt is visible.
3. Whether each prompt’s glyph is shown.
4. Whether G or H input is detected in the log.
5. Whether either prompt reports active.

Do not treat one of these results as proof of the others.

## Template for future test entries
For each new or materially changed test, add an entry with:

### [Short test name]
- **Question/goal:**
- **Status:** Not tested / In progress / Confirmed / Failed / Inconclusive
- **Test conditions:** Test source version, relevant setup, and log/date.
- **Instrumented results:** What the code or log directly established.
- **User observations:** What the user saw or experienced; state “None reported” if applicable.
- **Conclusion:** What is established and what remains unknown.
- **Do not repeat:** Findings settled for this test.
- **Next test:**
- **API references:** Related `NativeReferenceGuide.md` entries.
