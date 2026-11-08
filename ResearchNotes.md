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

**In progress.** Focused-target detection, target-group assignment, and a group active-page transition have been demonstrated. The user confirms that a known `CUTSCENE_SKIP` label can display as **G Skip** in a prior test setup. Custom `Recruit` did not appear in the prompt menu. A screen-display comparison showed `Recruit` at the top center. The follow-up custom-versus-registered phase test produced no visible prompt according to the user, who reports pressing G. The log for that run has not been provided, so the detected target, prompt state, and G input remain unverified for that run.

Upstream research found an open ScriptHookRDR2DotNet-V2 issue reporting that custom `VAR_STRING` text does not work in `_UI_PROMPT_SET_TEXT`, while labels already in the game’s string database can work. The next test sets the registered `CUTSCENE_SKIP` label once before prompt registration, then tests it in the current focused-target group setup. This removes post-registration text changes as a variable.

### Established from prior user testing

The user has confirmed:

- An approach label can be added.
- The focused target can be detected.
- Items can be added to the menu.
- A prompt using G with the known `CUTSCENE_SKIP` label displayed as **G Skip** in a prior test setup.

These are settled project findings for the conditions in which they were tested. Do not assume the same display result in the current focused-target test without observing it.

### Established by the focused-target test source and log

- The test gets the focused target using `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY`.
- The test checks human status using `PED.IS_PED_HUMAN`.
- The test obtains the target’s prompt group using `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`.
- The test assigns its prompt with `_UI_PROMPT_SET_GROUP`.
- The configured test control `INPUT_INTERACT_OPTION1` has hash `0x760A9C6F` and is the G control used by the test.
- The direct-custom-text test builds `Recruit` with `MISC.VAR_STRING(10, "LITERAL_STRING", "Recruit")` and passes the result to `_UI_PROMPT_SET_TEXT`.
- The `VAR_STRING` wrapper declaration recorded from API research is `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- The focused-target test does not call `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`.
- An active prompt/group does not prove the prompt was visibly rendered or that its text or glyph was correct.

### Earlier direct-`VAR_STRING` prompt test

`Test_Log.txt` dated `2026-11-08 13:16` records:

- The `Recruit/G` prompt was created.
- A focused human target was found.
- The prompt was assigned to the target’s prompt group.
- The active-page query changed from `-1` to `0`.
- The prompt reported active while the target was focused and inactive after focus was lost.
- The log contains no G input or prompt-pressed entry.

**User observation:** The user reported that `Recruit` did not appear in the menu.

**Conclusion:** Passing the `VAR_STRING` result directly to `_UI_PROMPT_SET_TEXT`, with no intervening native call, did not make `Recruit` visible according to the user. This does not establish whether `VAR_STRING` works in other text-display natives. That run did not establish whether G input activated the prompt.

### Screen-display comparison

- **Question/goal:** Does the same `VAR_STRING(10, "LITERAL_STRING", "Recruit")` display through `UI._DISPLAY_TEXT` while a focused human target is present?
- **Status:** User-tested; visual result reported.
- **Test conditions:** The test kept the focused-target `Recruit/G` prompt and called `VAR_STRING` directly as the `UI._DISPLAY_TEXT` text argument while a human target was focused. Text was drawn near the top center of the screen.
- **Instrumented results:** The test source logged the screen-native call attempt. The log alone cannot verify visual rendering.
- **User observations:** The user saw `Recruit` at the top center while focused on a human NPC and unarmed. The user also reported pressing G. `Recruit` did not appear in the prompt menu.
- **Conclusion:** In that run, the screen diagnostic rendered, but custom `Recruit` did not appear in the focused-target prompt menu. This narrows the observed difference to the prompt-text/menu path; it does not establish whether G activated the prompt.
- **Do not repeat:** The same screen-display comparison.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.UI._DISPLAY_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`; see `NativeReferenceGuide.md`.

### Custom-versus-registered phase test

- **Question/goal:** Does a known registered label render through `_UI_PROMPT_SET_TEXT` in the current focused-target setup while custom `Recruit` does not? Does G produce raw-input and prompt-pressed log entries?
- **Status:** User-tested; visual result reported; log not supplied.
- **Test conditions:** One G prompt alternated every six seconds between custom `Recruit` and registered `CUTSCENE_SKIP` while a focused human target and target group were available.
- **Instrumented results:** The latest test log for this run has not been supplied. Do not infer target detection, phase state, group assignment, prompt state, or G detection from the user observation alone.
- **User observations:** The user reports seeing nothing and pressing G. This does not establish whether the prompt was active, which text phase was displayed, or whether the G input was registered by the script.
- **Conclusion:** The result does not yet distinguish a registered-label issue from prompt-group/display behavior or a failure to reach the visible prompt state.
- **Do not repeat:** The six-second alternating test; it adds a post-registration text-change variable and has already produced the reported result.
- **Next test:** Use one registered `CUTSCENE_SKIP` label set before prompt registration. Keep the focused-target group and G control. Check for **G Skip**, and preserve the new log to confirm target, group, prompt, and input states.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`, `RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE`, and `RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED`; see `NativeReferenceGuide.md`.

### Current test: static registered-label prompt

- **Question/goal:** Does a `CUTSCENE_SKIP` prompt assigned before registration appear in the focused human target’s prompt group, and does G activate it?
- **Status:** New test provided; awaiting an in-game run.
- **Test conditions:** A single prompt is assigned `VAR_STRING(10, "CUTSCENE_SKIP")` before `_UI_PROMPT_REGISTER_END`. It is enabled and assigned to the focused human target’s prompt group. The test performs no recruitment.
- **Instrumented results:** Pending. The test logs target detection, target group assignment, active page, prompt active state, raw G presses, and prompt-pressed events. The log cannot prove visible rendering.
- **User observations:** In the preceding phase test, the user reports seeing nothing and pressing G. The run’s log has not been provided.
- **Conclusion:** Pending the static-label test.
- **Do not repeat:** The approach-label test, focused-target detection, group assignment, active-page test, original known G Skip test, direct-custom-string sequencing test, screen-display comparison, or six-second alternating test.
- **Next result to record:** Report whether **G Skip** appears while focused on an unarmed human NPC, whether G is pressed, and share the new `Test_Log.txt`. The log should show whether the target and prompt were active and whether raw G or prompt-pressed events were detected.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`, `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ACTIVE_PAGE`; see `NativeReferenceGuide.md`.

### Prompt-group research retained with scope

- General prompt-group research says active or ambient groups are activated each frame to display them.
- Active groups are exclusive and may interfere with other active groups; ambient groups are another activation mechanism.
- Interaction Lock-On (ILO) guidance uses a separate process: get the focused entity’s group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign a prompt with `_UI_PROMPT_SET_GROUP`.
- In the focused-target test, the entity’s group became active without a call to `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. This is an observed result for the tested ILO case; it does not replace or disprove the general active/ambient-group guidance.
- Avoid repeating group-activation experiments unless a later result shows the ILO group is no longer active or the test conditions change.

### Upstream API research

ScriptHookRDR2DotNet-V2 issue #4 reports that custom strings created with `VAR_STRING` do not work with `_UI_PROMPT_SET_TEXT`; it names registered string-database labels such as `CUTSCENE_SKIP` as working examples. The issue remains marked open. Version 2.1.0.1 release notes state that prompt text did not yet work at that time. Version 2.2.0 notes do not explicitly confirm that the issue was fixed. This is upstream-reported behavior, not proof of the exact result on every local 2.2.0 setup. The static registered-label test checks the current setup.

## Do not repeat without a specific reason

- Adding an approach label.
- Focused-target detection.
- Adding a prompt to the focused target’s group.
- The observed active-page transition in the logged ILO test.
- The known G Skip display test in its original setup.
- The direct-`VAR_STRING`-to-prompt sequencing test.
- The screen-display comparison.
- The six-second alternating prompt-text test.

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
