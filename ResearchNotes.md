# Research Notes

## Purpose

Record findings about the project, its API, and in-game tests. Distinguish code/API observations from claims that have actually been confirmed in game.

## Status labels

- **Verified in repository:** Confirmed in project files.
- **Documented in local reference:** Listed in the project’s API/native documentation; not necessarily verified in game.
- **Documented externally:** Reported by external research; not necessarily verified against this project’s wrapper or in game.
- **Tested in game:** Confirmed by an in-game test and its log.
- **Open:** Needs more investigation.

## Current test: ILO approach-prompt timing

### Goal

Test whether registering `REGISTER_INTERACTION_LOCKON_PROMPT` on a nearby human NPC **before aiming at it** makes the `Recruit` approach prompt visible. Separately observe whether the candidate control `INPUT_INTERACT_OPTION1` registers a press while that NPC is focused.

This is a diagnostic test only. It does not recruit an NPC, create a gang member, or alter saved data.

### Why the test changed

The previous test registered the ILO only after the NPC was already aimed at or focused. External prompt research describes the ILO’s text as the prompt shown while approaching. Therefore, the previous run did not establish whether registering before approach would display that prompt.

The new test changes registration timing: it registers on the nearest human NPC within 8 metres before focus, uses the same 2.5-metre radius and focus distance, and retains the `showApproachPrompt=true` argument. It tracks one target to reduce interference with nearby NPC prompts.

### Important distinctions

- The ILO native registers prompt text on an entity; it has no G/control argument.
- `INPUT_INTERACT_OPTION1` is sampled as a candidate G input only. It is not bound to recruitment by this test.
- A successful native return does not prove that the prompt rendered.
- A G-candidate press does not prove that the game treated it as recruitment.
- This test does not create a standard UI prompt, assign a prompt group, suppress an input, or perform recruitment.
- An ILO approach label is separate from a custom action in the focused-entity prompt menu.

### Test behavior

The replacement `Test.cs`:

- Finds the nearest living human NPC within 8 metres, using `World.GetAllPeds()`.
- Keeps the current target while it remains within 9 metres to avoid frequent target switching.
- Registers ILO text `Recruit` before the NPC is aimed at, with a 2.5-metre radius and `showApproachPrompt=true`.
- Logs target start/end, native wrapper signatures, ILO registration/unregistration results, focus transitions, and `INPUT_INTERACT_OPTION1` presses while the tracked target is focused.
- Unregisters the ILO when the tracked target changes or ends.
- Does not create or activate UI prompt groups, suppress controls, execute recruitment, or alter gang data.

### Test procedure

1. Replace `Test.cs` with the complete test file provided in this update and build the test project.
2. Start RDR2 in Story Mode with the test mod loaded. Test on foot, with a calm human NPC and no combat.
3. Approach the nearest human NPC without aiming first. Watch for the literal `Recruit` prompt within approximately 2.5 metres.
4. Then aim/focus that same NPC. Record whether the `Recruit` text appears, changes, or disappears.
5. Press G only as an input observation. The test does not recruit the NPC or bind G.
6. Provide the new `Test_Log.txt` and describe what was visible before aiming and while focused.

The test log is written to `Test_Log.txt` under the game’s base directory. To have the game write through to the repository copy on Windows, create a symbolic link in the game directory pointing to the repository’s `Test_Log.txt`. Remove or rename an existing regular file there first. The link must be created in the local checkout.

## Verified project/API observations

- The mod’s entry class derives from `RDR2.Script` and subscribes to the `Script.Tick` event. Do not implement `public override void Tick()`.
- `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()` in its production target-selection path.
- `Main.cs` uses `World.GetAllPeds()` to enumerate peds.
- The isolated test uses `PLAYER.GET_PLAYER_TARGET_ENTITY` and `Game.Player.GetFreeAimEntity()` to identify a focused/aimed human NPC.
- F9 remains the separate companion-follow toggle.
- The recruitment interaction is not implemented in the production path.
- The API release page identifies v2.2.0 as the latest listed release in the fetched release results. The project bootstrap specifies `ScriptHookRDRNetAPI.dll` version 2.2.0. Use the installed project assembly as the final authority for exact signatures. <citation src="1"></citation>
- The project’s source file list includes `Test.cs`.
- In test mode, do not modify `StoryBoard.md`.

## Current native signatures and test values

The installed v2.2.0 wrapper was resolved in the prior test log as:

- `REGISTER_INTERACTION_LOCKON_PROMPT(int, string, float, float, int, float, float, int, bool, int) -> bool`
- `UNREGISTER_INTERACTION_LOCKON_PROMPT(int) -> bool`

The latest checked-in log records both ILO registration and unregistration returning `true`. That does not establish that the text appeared.

The current test passes:

- Text: `Recruit`
- Radius: `2.5f`
- Focus distance: `2.5f`
- Flags: `0`
- Unknown floats: `0.0f`, `0.0f`
- Custom prompt handle: `0`
- Approach-prompt argument: `true`
- Unknown integer: `-1`

The native’s signature does not contain a G/control parameter. The meanings of unknown parameters remain unverified.

The current test samples:

- `INPUT_INTERACT_OPTION1` — `0x760A9C6F`

The external control mapping reports this as G in a listed PC mapping. Treat the mapping as a candidate because bindings and control context can vary.

## Latest previous test result

The latest checked-in `Test_Log.txt` records:

- Focused human target handle `203778`.
- `INPUT_INTERACT_OPTION1` was enabled for the target.
- ILO registration returned `System.Boolean:True`.
- The user pressed G and the test logged `G CANDIDATE JUST PRESSED`.
- ILO unregistration returned `System.Boolean:True`.
- The user reported that the visual result remained unchanged and no `Recruit` action appeared.

Interpretation:

- The log confirms that the candidate input registered when G was pressed in that test context.
- It does not establish that G was bound to recruitment or that recruitment occurred.
- The ILO registration return does not prove the text rendered.
- Because registration happened after target acquisition, the previous run did not test registration before approach. The new test specifically addresses that gap.
- No test date was recorded in the supplied result.

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

External RDR2 prompt research describes ILO as allowing the player to focus an entity to access its prompts. It documents the ILO registration arguments as entity, text, radius, focus distance, flags, two unknown values, prompt handle, an approach-prompt boolean, and an unknown integer.

The research describes:

- The text as the prompt shown when approaching the entity.
- The radius as the distance at which the focusing prompt appears.
- The next distance as a focus-distance value, commonly the same as the radius.
- The approach-prompt boolean as controlling whether the ILO prompt appears while approaching; focus may still be possible when false.
- The custom-prompt argument as not working with a custom prompt in that researcher’s tests and commonly being zero in Rockstar scripts.
- Separate standard UI prompts and prompt groups, which may be attached to a target entity’s prompt group.

These are external observations, not proof of behavior or signatures in this project’s wrapper. <citation src="11"></citation>

## Remaining questions

- Does `Recruit` appear when registered on a nearby NPC before the player aims at it?
- Does the label appear only while approaching, only while focused, or not at all?
- Does the user’s configured G mapping correspond to `INPUT_INTERACT_OPTION1` in the test context?
- What supported mechanism adds an action to the focused-entity prompt menu?
- Does prompt visibility depend on NPC type, distance, player state, mission state, or other conditions?

## Interpretation rules

- A native wrapper resolving successfully does not prove correct visible behavior.
- A native returning true does not prove prompt display.
- A visible label does not prove that G activates it.
- A G-candidate press does not prove recruitment was triggered.
- A change in fleeing, combat, health, or other NPC state does not identify the cause unless the test isolates that input and records its timing.
