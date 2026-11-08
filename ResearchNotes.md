# Research Notes

## NPC recruitment prompt test

Testing currently concerns the visual display of a G prompt while the player focuses a nearby human NPC. Pressing G must only be logged during this test; it must not recruit the NPC or alter gang membership.

## Latest in-game result

The dedicated active-group test ran, but no **G Recruit** prompt was visible.

This result does not establish that the active-group mechanism itself failed. The test assigned the custom text `"Recruit"` to the prompt, and that text was not a valid diagnostic for this API.

## Corrected diagnosis: custom prompt text

RDR2 prompt research says prompt text must be supplied as a `VAR_STRING`. It gives `VAR_STRING`/`CreateVarString` examples for creating prompt text and for active-group labels.

ScriptHookRDR2DotNet-V2 has an open issue documenting that `_UI_PROMPT_SET_TEXT` does not work reliably with custom strings. The issue reports that the prompt text works only when the supplied string is already present in the game's string database. Therefore, passing a C# string literal such as `"Recruit"` to the generated wrapper does not prove that the game can display that text.

The previous test incorrectly treated the wrapper's `string` parameter as proof that arbitrary text was supported. That conclusion was wrong. Its result cannot be used to reject the active-group approach.

## Current diagnostic

The revised test uses the registered game string key `CUTSCENE_SKIP` as a rendering probe, rather than a custom `"Recruit"` label. It continues to use:

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) for the G control.
- A dedicated prompt group (`0x4A4D47`).
- `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` while a nearby human NPC is focused.
- Both prompt-level and raw-control input logging.

This diagnostic tests whether a registered prompt label can render in the custom active group. It does **not** test the desired final `"Recruit"` label. The visible text for `CUTSCENE_SKIP` is whatever the current game localization associates with that key.

## Interpretation of the next result

- If the registered-string prompt appears, the active group can render; the blocker is custom prompt text.
- If it still does not appear, check `Test_Log.txt` for target resolution, prompt creation, prompt active state, and runtime errors. The result then points to an additional group/visibility issue.
- A raw G-control event without a visible prompt means input detection works but does not establish successful rendering.

Do not claim recruitment works until a separate test implements and verifies recruitment.

## Storyboard

Do not edit `StoryBoard.md` while in test mode.
