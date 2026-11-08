# Research Notes

## Current mode

The project is in test mode. Do not edit `StoryBoard.md` during this phase. Keep test findings in this file and native/API findings in `NativeReferenceGuide.md`.

## NPC interaction prompt test

The test is intended to determine whether the game can show an interaction-lock-on prompt for a nearby human NPC and a separate G-button diagnostic prompt when the player focuses that NPC.

Pressing G must only be logged. This test must not recruit the NPC or change gang membership.

## Prompt systems and entity targeting

There are two related but distinct operations:

1. `REGISTER_INTERACTION_LOCKON_PROMPT` registers an interaction-lock-on prompt on a specific entity. For this test, that entity is a nearby human NPC. Its radius controls the approach prompt, and the native's separate focus-distance argument controls how far away the entity can still be focused.
2. `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` returns the prompt group for an entity. The diagnostic UI prompt is assigned to that group with `_UI_PROMPT_SET_GROUP`, so it can appear while that NPC is focused.

The NPC is the target entity in both cases. The distinction is not “NPC versus target”; it is the per-entity lock-on registration versus the regular UI prompt assigned to that entity's prompt group.

The prompt research describes `REGISTER_INTERACTION_LOCKON_PROMPT` as taking an entity, text, radius, focus distance, flags, additional parameters, and an approach-prompt boolean. It also describes `UNREGISTER_INTERACTION_LOCKON_PROMPT` as removing the registration for an entity. The test registers only nearby human peds and unregisters them when they leave the registration range. The native research indicates the approximate ten-object limit does not appear to apply to peds, but the test still limits registrations by distance to avoid unnecessary global registrations.

## Diagnostic label and G control

The test uses `CUTSCENE_SKIP` as a diagnostic game string key. It is not the desired final “Recruit” label. The native approach label and the UI prompt text are separate text routes; seeing one does not prove the other works.

The prompt control is `INPUT_INTERACT_OPTION1` (`0x760A9C6F`). This is a game control, not a hard-coded keyboard key. The displayed key depends on the player's current control mapping and input device.

The test records both raw control presses and the UI prompt's own just-pressed state. Neither log entry indicates recruitment; the test intentionally performs no recruitment action.

## Previous in-game report

The previous report was: right-clicking an NPC did not show a “G Recruit” option. The previous diagnostic used a standard UI prompt and assigned it to the focused entity's prompt group, but did not register `REGISTER_INTERACTION_LOCKON_PROMPT`. It also used `CUTSCENE_SKIP`, not the text “Recruit.” Therefore, that result did not establish whether the entity-specific approach/lock-on prompt registration works, nor whether a custom “Recruit” label works.

The previous log reported `rawTarget=none` during the attempt. That means the focused-target getter did not provide an entity at those times. It does not mean an NPC cannot be the target entity for `REGISTER_INTERACTION_LOCKON_PROMPT`; that native is registered directly against the NPC handle.

## Current diagnostic behavior

The updated `Test.cs`:

- Creates a standard UI prompt using `CUTSCENE_SKIP` and `INPUT_INTERACT_OPTION1`.
- Periodically enumerates peds and registers the interaction-lock-on prompt on nearby human NPCs.
- Uses `REGISTER_INTERACTION_LOCKON_PROMPT` with a 10.0-unit approach radius and a 12.0-unit focus distance.
- Enables the approach label in the native registration.
- Unregisters entities that are no longer nearby and unregisters all remaining entities when the script aborts.
- Independently reads the focused entity through `Game.Player.GetInteractionLockonTargetEntity()`.
- Gets that focused NPC's prompt group and assigns the standard UI diagnostic prompt to it.
- Logs target state, registration calls, input presses, prompt state, group assignment, and a two-second heartbeat.
- Does not recruit or modify gang state.

A registration log means the native call was issued without throwing an exception; it does not prove the game rendered the label.

## Interpreting the next test

- `ILO REGISTRATION ERROR` means the native call or managed wrapper failed.
- `ILO REGISTRATION ISSUED` means only that the call returned without a managed exception; check in-game whether an approach label appears.
- `focusedTarget=none` means the current focus-target getter returned no entity at that tick.
- `human=false` or `inRange=false` means the focused entity is not eligible for the diagnostic G prompt.
- `GROUP LOOKUP/ASSIGNMENT ERROR` means the target-group lookup or assignment failed.
- `groupAssigned=true` with `uiPromptActive=false` means a target and group were found, but the diagnostic UI prompt was not active.
- `RAW G-CONTROL PRESS DETECTED` confirms the selected game control was detected; it does not prove the prompt was visible.
- `UI PROMPT PRESS DETECTED` confirms the prompt's own pressed state; it still does not recruit.
- The absence of “Recruit” is expected in this diagnostic. The displayed label is the game key `CUTSCENE_SKIP`.

Record separately whether the outer lock-on/approach label appears, whether the diagnostic G prompt appears while focused, and whether G is detected. Do not claim recruitment works until a later test displays the intended label and verifies a recruitment action.

## Test results

- Previous attempt: right-clicked an NPC; no “G Recruit” option appeared.
- Interpretation: inconclusive for the current native test because the previous test did not call `REGISTER_INTERACTION_LOCKON_PROMPT` and did not use “Recruit” as its prompt text.
- Updated test result: pending in-game build and test.
