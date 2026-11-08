# Research Notes

## NPC recruitment prompt test

Testing currently concerns a native G-button prompt shown while the player focuses a nearby human NPC. Pressing G must only be logged during this test; it must not recruit the NPC or change gang membership.

The present diagnostic uses the existing game string key `CUTSCENE_SKIP`. Its visible text is whatever the game associates with that key in the current localization. It is a prompt-rendering probe, not the desired final “Recruit” label.

## Latest in-game report

The player locked onto an NPC and pressed G; no “Recruit” option appeared. That is not a successful recruitment-prompt test: the current test deliberately does not display “Recruit” and does not implement recruitment.

The checked-in log previously recorded prompt creation but did not record target or input activity after startup. The prior test only checked G after it had accepted a target and returned early when no qualifying target was found. This made it impossible to tell from the log whether target resolution, input detection, or prompt rendering was failing.

## Prompt text limitation

RDR2 prompt research describes prompt text as a game `VAR_STRING`. However, the ScriptHookRDR2DotNet-V2 issue about custom prompt text reports that `_UI_PROMPT_SET_TEXT` accepts text reliably only when the string is already present in the game's string database. Passing a C# literal such as `"Recruit"` is therefore not a reliable way to test whether a prompt group is visible.

Use a known game string key to test prompt rendering separately from support for a custom recruitment label. Do not claim the final “G Recruit” label works until a custom-string route has been established and tested.

## Correct group mechanism for a focused entity

For a prompt that should appear when an entity is focused, assign the prompt to that entity's prompt group:

1. Get the focused entity's group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`.
2. Assign the prompt to that group with `_UI_PROMPT_SET_GROUP`.
3. Enable and show the prompt while the eligible entity is focused.
4. Hide or disable the prompt when there is no eligible focused entity.

The earlier diagnostic called `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` for a manually chosen group. That active-group approach is not the entity-specific focus-group pattern described in the prompt research, so the new test uses the focused entity's group instead.

## Current diagnostic behavior

The current `Test.cs`:

- Registers one prompt using the existing game key `CUTSCENE_SKIP`.
- Uses `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) as the prompt control.
- Reads the interaction-lock-on target every tick and logs changes, including no-target state.
- Logs G control presses independently of target detection and prompt visibility.
- Checks for a human target within 8.0 game units before showing the diagnostic prompt.
- Gets the target's prompt group and assigns the prompt to it.
- Logs prompt active state, group assignment, target properties, and a heartbeat every two seconds.
- Does not recruit the target or modify gang state.

The control is a game control, not a hard-coded keyboard key. The displayed key depends on the player's current control mapping and input device.

## Interpreting the next test

- `TARGET STATE: rawTarget=none` throughout the lock-on attempt means the ScriptHook interaction-lock-on target getter is not returning a target in that situation.
- A target handle with `human=false` or `inRange=false` means the candidate is being rejected by the diagnostic's eligibility checks.
- `GROUP LOOKUP/ASSIGNMENT ERROR` indicates a group-native or wrapper problem.
- `groupAssigned=true` with `promptActive=false` means a target and group were found, but the game did not activate the prompt.
- `promptActive=true` means the game considers the prompt active; check whether the known game label is visible.
- `RAW G-CONTROL PRESS DETECTED` confirms the selected control was detected. It does not mean the prompt was visible or recruitment succeeded.
- The absence of “Recruit” is expected in this test. The diagnostic is not a recruitment implementation.

Do not claim recruitment works until a separate test displays the intended label and verifies the recruitment action.

## Storyboard

Do not edit `StoryBoard.md` while in test mode.
