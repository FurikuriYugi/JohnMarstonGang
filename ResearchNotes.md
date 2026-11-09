# Research Notes

This file tracks goals, tests, instrumented results, user observations, conclusions, and next steps. Keep log evidence separate from visual user observations. Do not repeat a test under unchanged conditions.

## Current objective

Add **G Recruit** to the unarmed lock-on menu for a focused human NPC. This is a prompt-display test only: pressing G must not recruit the NPC or change gang membership.

## Current status

The latest tested version combined a standard G prompt with ILO registration using `prompt=0`. `Test_Log.txt` dated `2026-11-08 22:38` records ILO registration, target-group assignment, visible/enabled calls, and the prompt becoming active. The user reported that **G Recruit did not appear** in the unarmed lock-on menu.

The `prompt=0` ILO test did not solve the display problem. The earlier ILO-handle test also failed visually. Do not repeat either ILO variant unchanged.

The next test removes ILO registration and tests only the Dog Companion standard-prompt path: create with `VAR_STRING`, assign the focused target’s prompt group, then set visible and enabled. It does not change the ped’s displayed name or add an outer-radius label. This test has not yet been built or run.

## Established test findings

### Focused target and group

- `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` found the focused target in the tested scenario.
- `PED.IS_PED_HUMAN` identified the tested NPC as human.
- `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` returned the target’s prompt group.
- `_UI_PROMPT_SET_GROUP` assigned the test prompt to that group.
- The active-page query changed from `-1` to `0` in earlier logged tests.
- Active-page and prompt-active results do not prove that the prompt was visibly rendered.

### Earlier `VAR_STRING` test

`Test_Log.txt` dated `2026-11-08 13:16` records prompt creation, a focused human target, prompt assignment to the target’s group, and an active-page transition. It contains no G input or prompt-pressed entry.

**User observation:** `Recruit` did not appear in the menu.

**Conclusion:** That test did not establish that `VAR_STRING` text rendered in the prompt menu or that G activated the prompt.

### Screen-display comparison

The test passed `VAR_STRING(10, "LITERAL_STRING", "Recruit")` to `UI._DISPLAY_TEXT` while a human target was focused.

**User observation:** `Recruit` appeared near the top center of the screen; it did not appear in the prompt menu.

**Conclusion:** The string was visible through the screen-display path in that run. This does not establish that it will display through `_UI_PROMPT_SET_TEXT`.

**Do not repeat:** This screen-display comparison.

### Registered-label and focused-ped tests

- A registered-label test used `CUTSCENE_SKIP` with G.
- Logs recorded a focused target, group assignment, active-page transition, active prompt state, raw G input, and a prompt-pressed event.
- The user reported that **G Skip did not appear** in the unarmed lock-on menu.
- `_SET_PED_PROMPT_NAME` made the focused NPC name display as **Recruit** in the tested setup. The user also saw **Recruit** as the outer-radius label.
- The action prompt still did not visibly appear.

These are separate display elements. The focused-ped name and outer-radius label are not evidence that the action prompt rendered.

### ILO prompt-handle test

`Test_Log.txt` dated `2026-11-08 17:05` records:

- Created G prompt handle `189442`.
- Focused human target `184322`.
- ILO registration succeeded with handle `189442` as the prompt argument.
- The prompt was assigned to group `268566577`.
- The group’s active page changed from `-1` to `0`.
- The prompt became active; raw G input and `G PROMPT PRESSED` were logged.

**User observation:** **G Skip did not appear.**

**Conclusion:** The handle variant did not produce a visible action prompt under the tested conditions. Registration, active-page, active-prompt, and input logs do not prove visible rendering.

**Do not repeat:** The same ILO-handle test unchanged.

### ILO `prompt=0` test

`Test_Log.txt` dated `2026-11-08 22:38` records:

- ILO registration for target `203266`, with `promptArg=0`.
- Prompt group `268566611` assigned to that target.
- Visible and enabled state applied.
- Prompt active state changed from `false` to `true`.

**User observation:** **G Recruit did not appear** in the unarmed lock-on menu.

**Conclusion:** The `prompt=0` ILO variant did not produce a visible action prompt under the tested conditions. Do not treat successful registration or active state as proof of rendering.

### Dog Companion Ghidra findings

The user reports that Dog Companion displays **B Bond**. Its `Lang.data` file, beside `DogCompanion.asi`, contains:

```text
interaction.accompany.promptLabel=Bond
```