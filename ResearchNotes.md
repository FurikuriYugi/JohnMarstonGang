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
**In progress.** Focused-target detection, target-group assignment, and a group active-page transition have been demonstrated. The user confirms that a known `CUTSCENE_SKIP` label can display as **G Skip**. The custom `Recruit` text made with `VAR_STRING` has not appeared in the prompt menu. The screen-display comparison showed `Recruit` at the top center, but not in the prompt menu. Upstream research found a matching ScriptHookRDR2DotNet-V2 issue reporting that custom `VAR_STRING` text does not work in `_UI_PROMPT_SET_TEXT`, while registered string-database labels can work. The next test alternates custom `Recruit` and registered `CUTSCENE_SKIP` on the same prompt in the same focused-target setup.

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

### Screen-display comparison
- **Question/goal:** Does the same `VAR_STRING(10, "LITERAL_STRING", "Recruit")` display through `UI._DISPLAY_TEXT` while a focused human target is present?
- **Status:** User-tested; visual result reported.
- **Test conditions:** The test kept the focused-target `Recruit/G` prompt and called `VAR_STRING` directly as the `UI._DISPLAY_TEXT` text argument while a human target was focused. Text was drawn near the top center of the screen.
- **Instrumented results:** The test source logged the screen-native call attempt. The log alone cannot verify visual rendering.
- **User observations:** The user saw `Recruit` at the top center while focused on a human NPC and unarmed. The user reports pressing G. `Recruit` did not appear in the prompt menu.
- **Conclusion:** In this run, the screen diagnostic rendered, but custom `Recruit` did not appear in the focused-target prompt menu. This narrows the observed difference to the prompt-text/menu path; it does not establish whether G activated the prompt. No matching G-press or prompt-pressed entry is present in the referenced earlier log.
- **Do not repeat:** The same screen-display comparison. It has produced a user-reported visual result.
- **Next test:** Compare custom `Recruit` with the known `CUTSCENE_SKIP` registered label on the same prompt and focused-target setup, while logging the current phase and G input.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.UI._DISPLAY_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`; see `NativeReferenceGuide.md`.

### Current test: custom-versus-registered prompt text
- **Question/goal:** Does a known registered label render through `_UI_PROMPT_SET_TEXT` in the current focused-target setup while custom `Recruit` does not? Does G produce raw-input and prompt-pressed log entries?
- **Status:** New test provided; awaiting an in-game run.
- **Test conditions:** One G prompt is assigned to the focused human target’s prompt group. While the prompt can be shown, its text alternates every six seconds between `VAR_STRING(10, "LITERAL_STRING", "Recruit")` and `VAR_STRING(10, "CUTSCENE_SKIP")`. The top-center screen diagnostic is removed so it cannot be mistaken for prompt-menu text.
- **Instrumented results:** Pending. `Test_Log.txt` will identify each phase, report target/group/prompt state, and report raw G presses and prompt-pressed events. It cannot establish visual rendering by itself.
- **User observations:** In the preceding run, the user saw screen `Recruit` but no `Recruit` menu entry, and reports pressing G. The user has previously confirmed that the known label displays as **G Skip** in a prompt.
- **Conclusion:** Pending this same-context comparison.
- **Do not repeat:** Focused-target detection, group assignment, the active-page test, the separate screen-display comparison, or the previously working G Skip test in its original setup. The new test uses the registered label only as a controlled comparison under the current focused-target setup.
- **Next result to record:** For both six-second phases, report whether a menu entry appears, its displayed text and glyph, and whether G produces a raw input and/or prompt-pressed log entry. Include the new `Test_Log.txt`.
- **API references:** `RDR2.Native.MISC.VAR_STRING`, `RDR2.Native.HUD._UI_PROMPT_SET_TEXT`, `RDR2.Native.HUD._UI_PROMPT_SET_GROUP`, `RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE`, and `RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED`; see `NativeReferenceGuide.md`.

### Prompt-group research retained with scope
- General prompt-group research says active or ambient groups are activated each frame to display them.
- Active groups are exclusive and may interfere with other active groups; ambient groups are another activation mechanism.
- Interaction Lock-On (ILO) guidance uses a separate process: get the focused entity’s group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign a prompt with `_UI_PROMPT_SET_GROUP`.
- In the focused-target test, the entity’s group became active without a call to `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. This is an observed result for the tested ILO case; it does not replace or disprove the general active/ambient-group guidance.
- Avoid repeating group-activation experiments unless a later result shows the ILO group is no longer active or the test conditions change.

### Upstream API release-note lead
ScriptHookRDR2DotNet-V2 release notes for 2.1.0.1 state that the prompt implementation was updated and that prompt text did not yet work. The 2.2.0 notes mention general internal updates but do not explicitly state that prompt text was fixed. Upstream issue #4 specifically reports that strings created from `VAR_STRING` do not work with `_UI_PROMPT_SET_TEXT`, while labels already present in the string database (including `CUTSCENE_SKIP`) can work. This is an upstream-reported limitation, not proof of the exact behavior of every 2.2.0 installation. The same-context comparison is intended to test the current project setup.

## Do not repeat without a specific reason
- Adding an approach label.
- Focused-target detection.
- Adding a prompt to the focused target’s group.
- The observed active-page transition in the logged ILO test.
- The known G Skip display test in its original setup.
- The direct `VAR_STRING`-to-prompt sequencing test.
- The screen-display comparison.

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
