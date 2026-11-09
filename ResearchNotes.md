# Research Notes

## Objective

Display **G Recruit** in the unarmed lock-on menu when the player focuses on a human NPC.

This is a prompt-display test only. Pressing the prompt must not recruit the NPC or alter gang membership. G is the chosen control; control selection is not the current issue.

## Current status

The latest previously checked-in `Test_Log.txt` records a standard prompt being created, assigned to a focused target's prompt group, made visible and enabled, and becoming active. It also records `VAR_STRING` returning `Recruit`.

The user reports that **G Recruit did not appear** in the unarmed lock-on menu.

Prompt-active state, successful target-group lookup, and a `VAR_STRING` result are not proof that the label rendered.

A new differential test is prepared in `Test.cs`. It has two text paths controlled by `UseVarStringResult`:

- `false`: pass the managed string `"Recruit"` directly to `_UI_PROMPT_SET_TEXT`.
- `true`: pass the result of `VAR_STRING(10, "LITERAL_STRING", "Recruit")` to `_UI_PROMPT_SET_TEXT`.

The new test has not yet been run. Compare the two runs while keeping all other setup unchanged.

## Sources reviewed

- `Context/GhidraTrace-001.md` through `Context/GhidraTrace-005.md`
- `TestingAndResearch/DogCompanion/GhidraTrace.md`
- `TestingAndResearch/DogCompanion/Lang.dat`
- The current `Test.cs` and `Test_Log.txt`

The Dog Companion directory listing was not available through the fetch tool, so this note records the individual files reviewed rather than claiming the directory contents are exhaustive.

## Dog Companion localization evidence

`Lang.dat` contains 22 localization entries. The relevant entry is:

```text
interaction.accompany.promptLabel=Bond
```