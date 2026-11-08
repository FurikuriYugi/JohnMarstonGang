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
- The existing test log showed the focused NPC was found, a group was assigned, the prompt became active, and the prompt press was detected.
- Those results establish that target detection, group assignment, and input detection work. They do not establish that the prompt label or glyph appeared in the focus menu.

### Current text-formatting test

- The API wrapper declaration verified for `VAR_STRING` is:
  `public static string VAR_STRING(int flags, params RDR2.Native.InputArgument[] args)`.
- The updated test creates the literal prompt text with `RDR2.Native.MISC.VAR_STRING(10, "LITERAL_STRING", "Recruit")` and passes the returned string to `_UI_PROMPT_SET_TEXT`.
- This test keeps the target lookup, prompt group, and control action unchanged so the text conversion is the isolated change.
- In-game result for the VAR_STRING version: pending.

### Next result to record

After testing, record whether the unarmed focus menu displays both the G glyph and `Recruit`, and whether activating the prompt is detected in `Test_Log.txt`.
