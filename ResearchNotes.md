# Research Notes

## Purpose

Record findings about the project, its API, and in-game tests. Distinguish code/API observations from claims that have actually been confirmed in game.

## Status labels

- **Verified in repository:** Confirmed in project files.
- **Documented in local reference:** Listed in the project’s API/native documentation; not necessarily verified in game.
- **Documented externally:** Reported by external research; not necessarily verified against this project’s wrapper or in game.
- **Tested in game:** Confirmed by an in-game test and its log.
- **User-reported in game:** Reported by the user; no matching fresh log was supplied.
- **Open:** Needs more investigation.

## Current test: ILO approach text and focused NPC prompt group

### Goal

Keep the `Recruit` ILO approach label and test a separate standard UI prompt on the focused NPC. The custom prompt is assigned to the target entity’s prompt group and uses `INPUT_INTERACT_OPTION1` as the test control.

This test uses the registered game text key `WARDROBE_INSPECT_PROMPT` as a baseline instead of custom `Recruit` text. It does not recruit an NPC, create a gang member, or alter saved data.

### Research that changed the test

External RDR2 prompt research distinguishes:

- An ILO approach label, which is shown while approaching an entity.
- The text shown when focusing a ped, such as “Stranger” or a role name.
- Prompts attached to the entity’s prompt group, which are shown with the entity’s focused prompt options.

The documented native for obtaining the focused entity’s prompt group is `UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY`. A custom prompt can be assigned to that group with `UI_PROMPT_SET_GROUP`. This is a more targeted path than relying on an unrelated ambient or active prompt group.

The ScriptHookRDR2DotNet-V2 repository has an open issue reporting that custom prompt text created with `MISC.VAR_STRING` does not work with `_UI_PROMPT_SET_TEXT`; that issue says registered string-database entries work. The new test uses the documented registered key `WARDROBE_INSPECT_PROMPT` to test the focused-menu prompt plumbing independently from custom text.

These are external findings, not proof that the native wrappers or UI behavior work in this project’s installed build.

### Latest user-reported in-game observation

**User-reported in game; fresh log not supplied.**

The user reports that:

- `Recruit` appears as the outer-radius/approach label.
- Moving closer and holding right mouse button to focus the NPC changes the display to the NPC’s name or “Stranger” and shows the existing actions/options.
- Pressing G did not make a `G Recruit` glyph/action appear in that focused menu.

Interpretation:

- The approach label and focused NPC prompt menu are distinct UI states.
- The ILO approach label appeared, but it did not add a custom recruitment action to the focused menu.
- Pressing G does not prove recruitment was bound or executed.
- This observation is what prompted the new target-prompt-group test.
- No new test log was supplied with this visual result.

### New test behavior

The replacement `Test.cs`:

- Finds the nearest living human NPC within 8 metres, using `World.GetAllPeds()`.
- Keeps the current target while it remains within 9 metres to reduce target switching.
- Registers ILO text `Recruit` before the NPC is aimed at, with a 2.5-metre radius and `showApproachPrompt=true`.
- Creates one standard UI prompt with the candidate control `INPUT_INTERACT_OPTION1`.
- Uses the registered game text key `WARDROBE_INSPECT_PROMPT` as a baseline prompt label; it does not yet test custom `Recruit` UI text.
- When the tracked NPC is focused, gets that NPC’s prompt group and attaches the standard prompt to it.
- Enables and shows the custom prompt only while the tracked NPC is focused, then disables it and attempts to remove it from the group when focus ends.
- Logs native wrapper resolution, target prompt-group ID, prompt attachment, focus transitions, and prompt-pressed events.
- Does not execute recruitment or modify gang data.

### What this test can tell us

- If the G/Inspect prompt appears in the focused NPC menu, target-group attachment and the prompt lifecycle are working; custom `Recruit` text remains a separate issue.
- If the wrapper lookup or invocation fails, the log identifies which native operation needs signature or API-wrapper investigation.
- If group attachment succeeds but the prompt remains invisible, the log and in-game observation help isolate a remaining native behavior, control, or prompt-text issue.
- A wrapper returning without an exception does not prove that the prompt rendered.
- A prompt pressed event does not mean recruitment occurred; the test has no recruitment action.

### Test procedure

1. Replace `Test.cs` with the complete file supplied alongside this update and build the test project.
2. Start RDR2 in Story Mode, on foot, near a calm human NPC. Avoid combat or a mission interaction.
3. Approach the NPC without aiming first. Observe whether the outer-radius `Recruit` label appears.
4. Focus the same NPC. Look for the extra G prompt using the baseline Inspect label alongside the existing actions/options.
5. Press G only to test the prompt. The code does not recruit the NPC.
6. Save and provide the new `Test_Log.txt` and report what appeared while approaching and while focused.

The test log is written to `Test_Log.txt` under the game’s base directory. To have the game write through to the repository copy on Windows, create a symbolic link in the game directory pointing to the repository’s `Test_Log.txt`. Remove or rename an existing regular file there first. The link must be created in the local checkout.

## Verified project/API observations

- The mod’s entry class derives from `RDR2.Script` and subscribes to the `Script.Tick` event. Do not implement `public override void Tick()`.
- `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()` in its production target-selection path.
- `Main.cs` uses `World.GetAllPeds()` to enumerate peds.
- The isolated test uses `PLAYER.GET_PLAYER_TARGET_ENTITY` and `Game.Player.GetFreeAimEntity()` to identify a focused/aimed human NPC.
- F9 remains the separate companion-follow toggle.
- The recruitment interaction is not implemented in the production path.
- The API release page identifies v2.2.0 as the latest listed release in the fetched release results. The project bootstrap specifies `ScriptHookRDRNetAPI.dll` version 2.2.0. Use the installed project assembly as the final authority for exact signatures.
- The project’s source file list includes `Test.cs`.
- In test mode, do not modify `StoryBoard.md`.

## ILO signatures and test values

The installed v2.2.0 wrapper was resolved in the test log as:

- `REGISTER_INTERACTION_LOCKON_PROMPT(int, string, float, float, int, float, float, int, bool, int) -> bool`
- `UNREGISTER_INTERACTION_LOCKON_PROMPT(int) -> bool`

The checked-in test log records both ILO registration and unregistration returning `true`. That does not establish that the text appeared.

The ILO test passes:

- Text: `Recruit`
- Radius: `2.5f`
- Focus distance: `2.5f`
- Flags: `0`
- Unknown floats: `0.0f`, `0.0f`
- Custom prompt handle: `0`
- Approach-prompt argument: `true`
- Unknown integer: `-1`

The ILO native’s signature does not contain a G/control argument. The meanings of unknown parameters remain unverified.

The test observes:

- `INPUT_INTERACT_OPTION1` — `0x760A9C6F`

An external control mapping reports this as G in a listed PC mapping. Treat the mapping as a candidate because bindings and control context can vary.

## New focused-prompt test natives

The new test resolves the prompt wrappers reflectively from the installed API assembly. It searches by normalized native name and argument count, logs the resolved methods through its invocation diagnostics, and does not assume that a method exists merely because external native research lists it.

The expected operations, based on external prompt research, are:

- Begin a prompt registration.
- Set the control action to `INPUT_INTERACT_OPTION1`.
- Set prompt text using the registered string key `WARDROBE_INSPECT_PROMPT`.
- End prompt registration.
- Get the target entity’s prompt group.
- Assign the prompt to that group.
- Enable/show it while the tracked entity is focused.
- Disable/hide and remove it from the group when focus ends.
- Read whether the prompt was pressed.
- Delete the prompt when the script stops.

The exact wrapper namespace, overloads, parameter types, and return types must be taken from the installed v2.2.0 assembly and confirmed in the next test log. Reflection is used so the test can report missing wrappers instead of assuming a guessed signature.

## Previous ILO timing test result

The checked-in `Test_Log.txt` for the ILO timing test records:

- Several nearby human NPC targets were tracked.
- ILO registration returned true for the logged targets.
- ILO unregistration returned true for the logged targets.
- Focus transitions were logged for some tracked targets.
- `INPUT_INTERACT_OPTION1` was reported enabled while focused on some targets.
- A `G CANDIDATE JUST PRESSED` event was logged for target `186370`.

Interpretation:

- The log confirms that the candidate input registered when G was pressed in that recorded test context.
- It does not establish that G was bound to recruitment or that recruitment occurred.
- The ILO registration return does not prove that its text rendered.
- The log belongs to an earlier run and does not verify the latest user-reported visual result.

## Earlier prompt and input findings

- Earlier prompt properties reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`; those properties do not prove that a prompt rendered.
- Ambient-group activation did not make `G Recruit` visible.
- Explicit active-group activation also did not make `G Recruit` visible.
- An ILO-registration test focused human targets `202754` and `202498`. It created prompt handles `252162`, `300802`, and `314114`, assigned to target groups `268566617` and `268566616`, but `G Recruit` remained absent. That test did not log native Boolean returns.
- A diagnostic `INPUT_CONTEXT_X` run focused targets `203010` and `201730`; ILO registration and unregistration returned true. The custom prompt was not visible, and the existing R “Greet” option disappeared during that setup.
- A reflective `VAR_STRING` attempt stopped before prompt registration because its resolver rejected the wrapper signature. This was not a prompt-visibility result.
- A typed G-prompt attempt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) and `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })`; the prompt was not visible.
- Another typed G-prompt attempt registered an ILO and assigned the prompt group; the prompt was still not visible.
- A group-order test called `_UI_PROMPT_SET_GROUP` after registration; `G Recruit` remained absent.
- An ordered ILO-enabled G-prompt attempt found `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE`; invoking it did not make the prompt visible.
- An earlier read-only sniffer log focused target `202242`. It recorded `INPUT_INTERACT_OPTION1` enabled, but not pressed; it recorded `INPUT_INTERACT_LOCKON_POS` and `INPUT_INTERACT_LOCKON_ROB` pressed. The target later fled and entered combat. The log does not establish which input caused those changes.
- The user reported that built-in options tested individually included Rob, Threaten, and Aim At, and that the NPC fled or became hostile. That run did not show a recruitment option and did not test G recruitment.

## External prompt research

External RDR2 prompt research describes:

- Prompt creation as a begin/configure/end lifecycle, using a control action and text.
- Prompt groups as requiring activation each frame when using active or ambient group modes.
- Entity-focused prompts as a distinct case: obtain the target entity’s prompt group and attach the prompt to that group.
- ILO text as the approach prompt, distinct from the text shown for a focused ped.
- `UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` as the way to obtain an entity’s focused prompt group.
- `UI_PROMPT_SET_GROUP` as the way to attach the custom prompt to that group.
- A registered string-database key as a workaround to custom prompt text not working with `VAR_STRING` in a ScriptHookRDR2DotNet prompt.

These are external observations, not proof of behavior or signatures in this project’s wrapper.

## Remaining questions

- Does the baseline G prompt appear in the tracked NPC’s focused prompt menu when assigned to that NPC’s prompt group?
- Which exact prompt wrappers and signatures are available in the installed v2.2.0 assembly?
- Does the registered key `WARDROBE_INSPECT_PROMPT` display correctly when passed through the installed UI prompt wrapper?
- If the baseline prompt appears, what text mechanism reliably displays custom `Recruit` text?
- Does the user’s configured G mapping correspond to `INPUT_INTERACT_OPTION1` in this test context?
- Does prompt visibility depend on NPC type, distance, player state, mission state, or another condition?

## Interpretation rules

- A native wrapper resolving successfully does not prove correct visible behavior.
- A native returning true does not prove prompt display.
- A visible ILO approach label does not prove that a custom action was added to the focused menu.
- A visible prompt does not prove G activates a recruitment action.
- A prompt pressed event does not prove recruitment was triggered.
- A change in fleeing, combat, health, or other NPC state does not identify the cause unless the test isolates that input and records its timing.
