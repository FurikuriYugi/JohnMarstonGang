# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Keep verified repository/API facts distinct from external research, hypotheses, and in-game observations.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in project notes, Object Browser, or installed API documentation; not necessarily confirmed in game.
- **Documented in external research:** Reported by external native/control research; not necessarily confirmed against the installed wrapper or in game.
- **Hypothesis:** Plausible interpretation that must not be treated as an API contract.
- **Tested in game:** Confirmed in game, with environment and steps recorded.
- **Open:** Needs further investigation.

## Current test: native interaction-lock-on prompt

### Goal

Test the native `REGISTER_INTERACTION_LOCKON_PROMPT` path on a human NPC and observe whether its text, `Recruit`, appears in the game’s approach/focus interaction UI. Also observe the candidate `INPUT_INTERACT_OPTION1` input, externally reported as G in the PC `InteractionLockOn` mapping.

This is a diagnostic test only. It does not recruit an NPC, create a gang member, or change saved data.

### Important distinction

The ILO registration native accepts an entity, display text, distances, flags, and other parameters. Its documented signature has **no G/control argument**. Therefore:

- The test registers the native label `Recruit`.
- The test separately samples `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) and logs just-pressed events.
- A successful native return does not prove the label rendered.
- A logged G candidate press does not prove that G is bound to recruitment.
- The test never executes a recruitment action.

The ILO text is not the same thing as adding a new G action to a focused entity’s built-in action menu. The test isolates the native ILO registration path; it does not claim to solve the separate action-prompt/control-binding problem.

### Test implementation behavior

The replacement `Test.cs`:

- Resolves `REGISTER_INTERACTION_LOCKON_PROMPT` and `UNREGISTER_INTERACTION_LOCKON_PROMPT` by reflection in the loaded API assembly, matching the signatures recorded in the local reference.
- Tracks a human NPC found through the focused-target native or `Game.Player.GetFreeAimEntity()`.
- Registers the ILO text `Recruit` on the tracked human target.
- Samples `INPUT_INTERACT_OPTION1` while a human target is tracked.
- Logs registration/unregistration results, wrapper signatures, target transitions, and G-candidate presses.
- Unregisters an ILO after a successful registration when the tracked target changes or ends.
- Does not create a UI prompt, assign or activate a prompt group, suppress an input, call the recruitment method, or change gang data.

### Test procedure

1. Replace `Test.cs` with the full test file supplied for the test and build the test project.
2. Start RDR2 in Story Mode with the test mod loaded. Use a calm human NPC and approach within approximately 2.5 metres; do not perform the test during combat.
3. Watch the NPC while aiming/focusing. Record whether the literal `Recruit` text appears, where it appears, and whether it appears before or during focus.
4. If the UI visibly offers a G action, record the exact displayed label and whether the G press is logged. The test itself does not bind G or recruit the NPC.
5. If no G action is visibly offered, do not press G expecting recruitment. The test only logs `INPUT_INTERACT_OPTION1`; it does not make that input a recruit action.
6. Send `Test_Log.txt` and a short description of the visible UI and NPC response. Record whether the player was on foot, unarmed, and within close range.

The test log is written to `Test_Log.txt` under the game’s base directory. To have the game write through to the repository copy on Windows, create a symbolic link in the game directory pointing to the repository’s `Test_Log.txt`. Remove or rename an existing regular file there first. The link must be created in the local checkout; it is not created by this chat.

## Findings from the repository

- The mod’s entry class derives from `RDR2.Script` and subscribes to the `Script.Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The isolated test harness previously used `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse that with the production target-selection path.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment action is not implemented in the production path.
- The user reports that the only `Prompt`-named API type in the installed API is `RDR2.UI.PromptTimingEvent`. Do not assume `RDR2.UI.Prompt.CreatePrompt` is an available prompt-construction API.
- The project file explicitly lists source files and includes `Test.cs` for the isolated test.
- In test mode, do not modify `StoryBoard.md`.

## Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; use the unsafe pointer form already validated in the previous test.
- While `Game.Player.IsFreeFocusing` was true, the player-target native returned the focused NPC handle in earlier test cycles. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters: `int, string, float, float, int, float, float, int, bool, int`.
- The v2.2.0 wrapper resolved `UNREGISTER_INTERACTION_LOCKON_PROMPT` as returning `System.Boolean`.
- Runtime reflection previously reported both ILO registration and unregistration methods as returning `System.Boolean`.
- These wrapper signatures do not establish the meaning of undocumented parameters or guarantee in-game display.
- The current sniffer log reported `_GET_CURRENT_CONTROL_CONTEXT` as unavailable. Do not rely on that wrapper unless the installed v2.2.0 assembly is checked again and a compatible method is found.
- The replacement test resolves ILO wrappers by reflection at runtime and logs the exact signature it finds. A wrapper’s presence or native return value is not proof of visible behavior.

## Latest test result: native ILO label and G-candidate input

The latest checked-in test log records:

- A focused human target, handle `203778`.
- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) was enabled for that target.
- ILO registration returned `System.Boolean:True`.
- A `G CANDIDATE JUST PRESSED` event was logged for that target after the user pressed G.
- ILO unregistration returned `System.Boolean:True`.
- The user reported that the visual result remained the same: the `Recruit` label/action was still not visible.

Interpretation:

- This test confirms that the candidate input was detected when G was pressed in that test context.
- The result does **not** confirm that G is bound to recruitment or that the game handled the press as a recruitment action.
- The native registration return does **not** confirm that the `Recruit` label rendered.
- No recruitment action was executed and no gang data was changed.
- The in-game observation has no date recorded in the supplied result.

## Candidate input research

External control research lists these candidate hashes in or around the `InteractionLockOn` context:

- `INPUT_INTERACT_LOCKON_ROB` — `0x9FA5AD07`
- `INPUT_INTERACT_LOCKON_POS` — `0xF63A17F9`
- `INPUT_INTERACT_LOCKON_NEG` — `0x26A18F47`
- `INPUT_INTERACT_OPTION1` — `0x760A9C6F`
- `INPUT_INTERACT_OPTION2` — `0x84543902`
- `INPUT_INTERACT_LOCKON` — `0xF8982F00`
- `INPUT_EMOTE_GREET` — `0x72BAD5AA`
- `INPUT_EMOTE_TAUNT` — `0x470DC190`
- `INPUT_EMOTE_ACTION` — `0x13C42BB2`

The external list reports `INPUT_INTERACT_OPTION1` as G and `INPUT_INTERACT_OPTION2` as H in its listed PC mapping. Bindings and behavior can depend on context and user settings. Treat these as diagnostic candidates, not confirmed bindings for this test setup.

## Previous in-game prompt findings

### Earlier custom action-prompt attempts

The user previously tested custom action prompts assigned to target prompt groups:

- Earlier prompt properties reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`; those properties do not prove that the prompt rendered.
- Ambient-group activation did not make **G Recruit** visible.
- Explicit active-group activation also did not make **G Recruit** visible.
- An ILO-registration test focused human targets `202754` and `202498`. It created prompt handles `252162`, `300802`, and `314114`, assigned to target groups `268566617` and `268566616`, but **G Recruit** remained absent. That test did not log the native Boolean returns.
- A diagnostic `INPUT_CONTEXT_X` run focused targets `203010` and `201730`; ILO registration and unregistration returned `true`. The custom prompt was not visible, and the existing R “Greet” option disappeared during that setup.
- A reflective `VAR_STRING` attempt stopped before prompt registration because its resolver rejected the wrapper signature. This was not a prompt-visibility result.
- A typed G-prompt attempt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) and `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })`; the prompt was not visible.
- Another typed G-prompt attempt registered an ILO and assigned the prompt group; the prompt was still not visible.
- A group-order test called `_UI_PROMPT_SET_GROUP` after registration; **G Recruit** remained absent.
- An ordered ILO-enabled G-prompt attempt found `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE`; invoking it did not make the prompt visible.

These results establish repeated visibility failures for the tested custom UI-prompt setups. They do not prove that every possible prompt/control combination fails.

### Earlier read-only sniffer log

An earlier checked-in sniffer log recorded a focused human target `202242` and these relevant observations:

- `INPUT_INTERACT_OPTION1` was enabled when the target was focused.
- The log did not record a press of `INPUT_INTERACT_OPTION1` (the reported G candidate).
- `INPUT_INTERACT_LOCKON_POS` and `INPUT_INTERACT_LOCKON_ROB` were pressed.
- The target later changed from not fleeing/not in combat to fleeing, and then to fleeing/in combat.
- The log does not establish which particular input caused the response, nor does it establish the internal action the game handled.

The user reported that the built-in options tested individually included Rob, Threaten, and Aim At, and that the NPC fled or became hostile. The target did not show a recruitment option, so G was not pressed. That run did not test recruitment or confirm a G binding.

## External prompt research

External RDR2 prompt research describes these as separate operations:

1. Register an interaction-lock-on prompt on the target entity.
2. Create/configure a UI prompt if a separate action prompt is needed.
3. Obtain the target entity’s prompt group and assign a UI prompt to that group when appropriate.
4. Activate the appropriate prompt group if the selected prompt type requires per-frame activation.

The external research says the ILO’s text is shown on the prompt when approaching an entity, while a focused ped may show a separate label such as “Stranger” or a role name. It also reports the ILO native’s custom-prompt parameter did not work with a custom prompt in that research setup and is commonly zero in Rockstar scripts.

Therefore, the current test deliberately avoids conflating ILO registration with a G action prompt. The native ILO text is tested directly; `INPUT_INTERACT_OPTION1` is only sampled.

## Open questions

- Does the native `Recruit` ILO text appear before focus, during focus, or not at all in this setup? Latest user report: it remained visually absent.
- Does the ILO native’s `true` return correlate with any visible UI in this setup? Latest result: it returned true while the user reported no visible change.
- Does the G candidate control register as pressed when the user presses G while the target is focused? Latest log: yes, `INPUT_INTERACT_OPTION1` registered a just-pressed event.
- Does the user’s key mapping differ from the external PC mapping?
- Which supported mechanism, if any, adds a new action to the focused-entity interaction menu?
- Does the built-in menu change based on weapon state, distance, honor, NPC type, mission state, or other conditions?

## Rules for interpreting results

- A native Boolean return is not proof of visible UI.
- A visible `Recruit` label is not proof that G is bound to it.
- A G candidate press is not proof that the game treated it as recruitment.
- A change in fleeing/combat/health state is not proof of which input caused it unless the test isolates the input and records the timing.
- Do not claim recruitment occurred unless an explicit recruitment action and its resulting state are tested.
