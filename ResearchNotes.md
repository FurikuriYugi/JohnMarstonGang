# Research Notes

## Objective

Display **G Recruit** in the unarmed lock-on menu when the player focuses on a human NPC.

This is a prompt-display test only. Pressing G must not recruit the NPC or change gang membership. Control choice is not the current issue.

## Current status

The latest checked-in `Test.cs` is a prompt-text comparison test. It has a `UseVarStringResult` switch:

- `false`: pass `"Recruit"` directly to `_UI_PROMPT_SET_TEXT`.
- `true`: call `VAR_STRING(10, "LITERAL_STRING", "Recruit")`, then pass the returned managed string to `_UI_PROMPT_SET_TEXT`.

The latest checked-in `Test_Log.txt` is from the `false` run. It records the direct text path, successful prompt registration, target-group assignment, visibility/enabled calls, and the prompt becoming active.

The Bootstrap records the latest user-verified visual result: **G Recruit did not appear in the unarmed lock-on menu**.

The next test is the `true` branch. This tests whether applying `VAR_STRING` formatting changes the result. It does **not** establish whether a native string pointer survives the managed wrapper round trip, because both branches pass a C# `string` to the same `_UI_PROMPT_SET_TEXT` wrapper.

## Latest checked-in test and log

`Test.cs` currently uses:

```text
UseVarStringResult = false
```