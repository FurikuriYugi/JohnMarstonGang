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

**In progress.** Focused-target detection, target-group assignment, and a group active-page transition have been demonstrated. The user confirms that a known `CUTSCENE_SKIP` label can display as **G Skip**. The custom `Recruit` text made with `VAR_STRING` has not appeared in the prompt menu. The new test compares that same varstring through a separate screen text native.

### Established from prior user testing

The user has confirmed:

- An approach label can be added.
- The focused target can be detected.
- Items can be added to the menu.
- A prompt using G with the known `CUTSCENE_SKIP` label displays as **G Skip**.

These are settled project findings. Do not repeat those tests absent a specific reason.

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

### Latest direct-`VAR_STRING` prompt test

`Test_Log.txt` dated `2026-11-08 13:16` records:

- The `Recruit/G` prompt was created.
- A focused human target was found.
- The prompt was assigned to the target’s prompt group.
- The active-page query changed from `-1` to `0`.
- The prompt reported active while the target was focused and inactive after focus was lost.
- The log contains no G input or prompt-pressed entry.

**User observation:** The user reports that `Recruit` did not appear in the menu.

**Conclusion:** Passing the `VAR_STRING` result directly to `_UI_PROMPT_SET_TEXT`, with no intervening native call, did not make `Recruit` visible. This does not establish whether `VAR_STRING` works in other text-display natives. The run does not establish whether G input activated the prompt.

### Current test: screen-display comparison

- **Question/goal:** Does the same `VAR_STRING(10, "LITERAL_STRING", "Recruit")` display through `UI._DISPLAY_TEXT` while a focused human target is present?
- **Status:** Prepared; not yet run.
- **Test conditions:** The test keeps the focused-target `Recruit/G` prompt and additionally calls `VAR_STRING` directly as the `UI._DISPLAY_TEXT` text argument each tick while a human target is focused. Text is drawn near the top center of the screen.
- **Instrumented results:** None yet. The log will note the screen-native call attempt and any exception; it cannot verify visual rendering by itself.
- **User observations:** Prior direct-`VAR_STRING` prompt text did not appear. The user confirms the known `CUTSCENE_SKIP` label displays as **G Skip**.
- **Conclusion:** Pending the in-game comparison.
- **Do not repeat:** Focused-target detection, group assignment, the active-page test, the G Skip test, or the direct `VAR_STRING`-to-prompt sequencing test.
- **Next result to record:** Separately note whether the screen `Recruit` appears, whether the prompt-menu `Recruit` appears, whether the G glyph appears, and whether pressing G is logged.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.UI._DISPLAY_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`; see `NativeReferenceGuide.md`.

### Prompt-group research retained with scope

- General prompt-group research says active or ambient groups are activated each frame to display them.
- Active groups are exclusive and may interfere with other active groups; ambient groups are another activation mechanism.
- Interaction Lock-On (ILO) guidance uses a separate process: get the focused entity’s group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign a prompt with `_UI_PROMPT_SET_GROUP`.
- In the focused-target test, the entity’s group became active without a call to `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. This is an observed result for the tested ILO case; it does not replace or disprove the general active/ambient-group guidance.
- Avoid repeating group-activation experiments unless a later result shows the ILO group is no longer active or the test conditions change.

### Upstream API release-note lead

ScriptHookRDR2DotNet-V2 release notes for 2.1.0.1 state that the prompt implementation was updated and that prompt text did not yet work. The 2.2.0 notes mention general internal updates but do not explicitly state that prompt text was fixed. Treat this as a reason to test the prompt setter separately from ordinary screen text, not as proof of current 2.2.0 behavior.

## Do not repeat without a specific reason

- Adding an approach label.
- Focused-target detection.
- Adding a prompt to the focused target’s group.
- The observed active-page transition in the logged ILO test.
- The known G Skip display test.
- The direct-`VAR_STRING`-to-prompt sequencing test.

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
