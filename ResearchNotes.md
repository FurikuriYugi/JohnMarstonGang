# Research Notes

## Current test: focused-target Recruit prompt

### Goal

Add the G glyph and the text `Recruit` to the unarmed focus menu for a focused human NPC. This is a prompt-display test only; it does not recruit the NPC or change gang membership.

### Established

- `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` can return the focused target.
- The target can be checked with `PED.IS_PED_HUMAN`.
- The target’s prompt group can be obtained with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`.
- A prompt can be assigned to that group with `_UI_PROMPT_SET_GROUP`.
- The prompt control action is currently `INPUT_INTERACT_OPTION1`, hash `0x760A9C6F`.
- The API wrapper declaration verified for `VAR_STRING` is:
  `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- The test creates the literal prompt text with
  `RDR2.Native.MISC.VAR_STRING(10, "LITERAL_STRING", "Recruit")`
  and passes the returned string to `_UI_PROMPT_SET_TEXT`.

### Latest test result

- `Test_Log.txt` confirms the VAR_STRING version ran.
- The log shows a focused human target, prompt group assignment, an active prompt, and a detected prompt press.
- The user reports that `Recruit` did not appear in the unarmed focus menu.
- The log does not establish that the prompt group itself was active or that the prompt was visibly rendered. Prompt-active and prompt-pressed states do not by themselves confirm visual display.

### Prompt-group research

- Prompt groups must be activated every frame to display.
- Active groups use `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`. Active groups are exclusive, so forcing one may interfere with other menus.
- Ambient groups use `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME`; entity-attached ambient groups are activated every frame and are commonly used for nearby entity prompts.
- The Interaction Lock-On (ILO) guidance gives a separate procedure for prompts on a focused entity: obtain its group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`, then assign the prompt with `_UI_PROMPT_SET_GROUP`.
- The current test follows that ILO procedure. It does not yet force an active or ambient group.

### Current diagnostic test

- The test now queries `_UI_PROMPT_GET_GROUP_ACTIVE_PAGE` for the focused target’s prompt group.
- A return value of `0` or greater is logged as an active group page; `-1` is logged as inactive.
- This is a diagnostic only. The test does not call `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME`, avoiding an exclusive active-group change before confirming the target group’s state.

### Next result to record

After testing, record the `PROMPT GROUP ACTIVE PAGE` line from `Test_Log.txt`, whether the G glyph and `Recruit` appear, and whether activating the prompt is detected.
