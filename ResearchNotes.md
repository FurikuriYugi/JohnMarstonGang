# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Distinguish verified facts from hypotheses and test plans so uncertain API details do not become assumed contracts.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note, Object Browser, or installed API documentation, but not necessarily confirmed in game.
- **Documented in external research:** Reported by external native/control research; not yet confirmed against this project's installed wrapper or in game.
- **Hypothesis:** Plausible usage or interpretation; do not depend on it in production code.
- **Tested in game:** Confirmed in game; record the environment and steps.
- **Open:** Needs further investigation.

## Current focus: trace built-in NPC lock-on interactions

### Goal

Observe the inputs and visible target-state changes associated with the built-in NPC interaction actions, including Aim at, Rob, Greet, and Antagonize. The user will focus a target and perform one action at a time.

This is a diagnostic test only. It must not add prompts, register ILOs, trigger interactions, recruit NPCs, or change gang data.

### Scope and interpretation

The new `Test.cs` is a read-only sniffer. It samples:

- Focused human target handles.
- Candidate control-action states and just-pressed events.
- The current control context when a candidate control is pressed, if the installed wrapper exposes that native.
- Available, observable target properties: existence, human status, health, combat, fleeing, shooting, and ragdoll state.
- Target-state changes during focus and briefly after focus ends.

It does **not** hook or intercept Rockstar's internal script/native calls, menu payloads, text rendering, dialogue events, or animation requests. It cannot prove the exact internal action event that the game handled. The user's visual observation of the NPC's response remains part of the test result.

### Test procedure

1. Load the replacement `Test.cs` in the project's test setup.
2. Start the game in Story Mode with the mod loaded.
3. Focus a human NPC.
4. Note the currently visible built-in lock-on options.
5. Perform one action only: Aim at, Rob, Greet, or Antagonize.
6. Wait briefly for the NPC's response before trying another action.
7. Repeat with the same NPC when practical; record any weapon, distance, or other condition that changes.
8. Send `Test_Log.txt` and a short note for each action describing what appeared and what the NPC did.

Do not use a second diagnostic prompt during this run. The sniffer does not modify inputs or suppress the game's controls.

## Findings from the repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The isolated test harness uses `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse it with `Main.cs` target selection.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment prompt is not implemented in the production path.
- The user checked the installed API and reports the only `Prompt`-named API type is `RDR2.UI.PromptTimingEvent`. Do not rely on `RDR2.UI.Prompt.CreatePrompt`; it is not the prompt-construction API available in this project.
- The project file explicitly lists source files and includes `Test.cs` for the isolated test.
- The previous `Test.cs` was a custom action-prompt visibility test. The replacement is a read-only control/target-state sniffer and does not create a prompt or register an ILO.

## Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; use the unsafe pointer call already validated by the previous test.
- While `IsFreeFocusing=True`, the player-target native returned the focused NPC handle in earlier test cycles. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters: `int, string, float, float, int, float, float, int, bool, int`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` as `UInt64(int, float, int, int, uint, string, int)`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` as `UInt64(uint, string, int, int, int, int)`.
- The v2.2.0 wrapper exposes `MISC.VAR_STRING` as `string(int, InputArgument[])`.
- Runtime reflection reported `REGISTER_INTERACTION_LOCKON_PROMPT` and `UNREGISTER_INTERACTION_LOCKON_PROMPT` as returning `System.Boolean`.
- Those wrapper signatures do not establish parameter meanings or in-game behavior.
- The replacement sniffer resolves PAD and target-state methods by reflection. Its startup log records which compatible methods were found in the installed API assembly.

## Previous in-game prompt findings

### Earlier prompt tests — 2026-11-07

- Earlier target-detection tests found focused NPC handles.
- Earlier prompt tests assigned custom action prompts to target prompt groups.
- Earlier prompt properties reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`; those properties do not prove that the prompt rendered.
- Ambient-group activation did not make **G Recruit** visible.
- Explicit active-group activation also did not make **G Recruit** visible.
- An earlier ILO-registration test found focused human targets `202754` and `202498`.
- That log reported created prompt handles `252162`, `300802`, and `314114`, assigned to target groups `268566617` and `268566616`.
- The user did not see **G Recruit** in that menu; other menu options continued to work.
- That test did not log the Boolean returns from ILO registration and unregistration, so whether those calls returned `true` is unknown.
- No recruitment or gang-data changes were performed.

### Diagnostic `INPUT_CONTEXT_X` run — 2026-11-07

- The harness identified focused human targets `203010` and `201730`.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `true` for the focused targets.
- `UNREGISTER_INTERACTION_LOCKON_PROMPT` returned `true` for the targets when the diagnostic prompt was cleared.
- The harness created prompt handles `275970`, `302594`, `321282`, and `329730`, and assigned them to target groups `268566622` and `268566617`.
- The diagnostic prompt used `INPUT_CONTEXT_X` (`0xE3BF959B`), reported in that test as commonly mapped to keyboard R.
- The user did not see the Recruit prompt. The existing R “Greet” option disappeared while this diagnostic action was used; other menu options remained usable.
- No recruitment or gang-data changes were performed.

**Conclusion:** Focus detection, ILO calls, prompt creation, and target-group assignment ran without a logged setup error, but the custom prompt was not visibly rendered. The `true` native returns do not prove display. Using `INPUT_CONTEXT_X` interfered with the existing R “Greet” option in that test setup.

### Failed reflective `VAR_STRING` attempt — 2026-11-07

- The test found focused human target `198914`.
- The log resolved `MISC.VAR_STRING` as `RDR2.Native.MISC.VAR_STRING(System.Int32, RDR2.Native.InputArgument[]) -> System.String`.
- The test's reflective resolver reported that it could not find a compatible `VAR_STRING` wrapper.
- Prompt setup stopped with `PROMPT SETUP FAILED: No compatible VAR_STRING wrapper was resolved.`
- This happened before prompt registration began. No action prompt was created or assigned to the target group.
- No recruitment or gang-data changes were performed.

**Conclusion:** This was not a prompt-visibility result. The reflection check rejected the observed wrapper signature. The next test bypassed that resolver and called the typed wrapper with an explicit `InputArgument[]`.

### Typed G prompt setup attempt — 2026-11-07

- The user tested while unarmed and right-mouse focusing a human NPC.
- The test found focused human target `201218`.
- Prompt handle `267010` was registered and assigned to target group `268566621`.
- The prompt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`), reported as G in the `InteractionLockOn` context.
- The prompt text used the typed `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })` call.
- The user did not see **G Recruit** in the focused NPC's lock-on menu.
- The test did not register an ILO and did not activate ambient or active prompt groups.
- The optional standard-mode completion wrapper was not resolved. The prompt remained active until focus changed and was then deleted.
- No recruitment or gang-data changes were performed.

**Conclusion:** This was a visibility failure for the typed G prompt setup. Target detection, prompt registration, and target-group assignment succeeded, but the user did not see the action. The missing completion wrapper does not explain the missing display.

### ILO-enabled G prompt attempt — date not supplied

- The user tested while unarmed and right-mouse focusing a human NPC.
- The harness found focused human target `206338`.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `true`.
- Prompt handle `325890` was registered and assigned to target group `268566628`.
- The prompt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`), reported as G in the `InteractionLockOn` context.
- The prompt text used the typed `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })` call.
- The user did not see **G Recruit** in the focused NPC's lock-on menu.
- The ILO was unregistered when focus changed; unregistration returned `true`.
- The optional standard-mode completion wrapper was not resolved. The prompt remained active until focus changed and was then deleted.
- No recruitment or gang-data changes were performed.

**Conclusion:** ILO registration returned `true`, but this did not make the action visible. The prompt's registration and group assignment calls completed, but their success does not prove that the menu rendered the prompt.

### Group-order test — date not supplied

- The user tested while unarmed and right-mouse focusing a human NPC.
- The harness found focused human target `204546`.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `true`.
- Prompt handle `242690` was registered and assigned to target group `268566627`.
- The test called `_UI_PROMPT_SET_GROUP` after `_UI_PROMPT_REGISTER_END`.
- The prompt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`), reported as G in the `InteractionLockOn` context.
- The prompt text used the typed `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })` call.
- The user did not see **G Recruit** in the focused NPC's lock-on menu.
- The ILO was unregistered when focus changed; unregistration returned `true`.
- The optional standard-mode completion wrapper was not resolved. The prompt remained active until focus changed and was then deleted.
- No recruitment or gang-data changes were performed.

**Conclusion:** Assigning the target group after prompt registration did not make the action visible. ILO registration, prompt registration, and group assignment returning successfully still do not prove that the menu rendered the prompt.

### Ordered, ILO-enabled G prompt attempt — date not supplied

The supplied log reported:

- Focused human target `204290`.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `true`.
- Prompt handles `261122` and `268034` were registered in two focus cycles.
- The prompt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`), reported as G in the `InteractionLockOn` context.
- `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` resolved as `RDR2.Native.HUD._UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE(Int32, UInt64)`.
- The ordering call ran during prompt registration and returned `void`.
- The prompt text used the typed `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })` call.
- The prompt was assigned to the focused entity's target prompt group after registration.
- The user did not see **G Recruit** in the focused NPC's lock-on menu.
- ILO unregistration returned `true` when focus changed.
- The optional standard-mode completion wrapper was not resolved. The prompt remained active until focus changed and was then deleted.
- No recruitment or gang-data changes were performed.

**Conclusion:** The ordering wrapper was found and invoked, but the action still did not display. This remains a prompt-visibility failure, not evidence that the control was registered as a usable menu action.

## Documented external prompt research

External RDR2 prompt research describes these as separate operations:

1. Create and configure a native UI prompt with a control action and text.
2. Finish registering that prompt.
3. Get the prompt group for the target entity and assign the created prompt to that group.
4. Register an interaction-lock-on prompt on an entity when needed for its focus/approach interaction.

The research reports that peds can be focused by default. It describes the ILO `prompt` parameter as not working with a custom prompt in that research setup, and says peds' focused action prompts are shown by assigning a UI prompt to the entity's prompt group. Its examples demonstrate group assignment as a separate operation on a registered prompt.

External research also documents `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` (`0x2F385ECC5200938D`). It describes this native as choosing the control used for prompt ordering and says to call it during registration or immediately afterward. The documented ordering examples do not establish that `INPUT_INTERACT_OPTION1` is supported for this use.

The test discussion raised `PP_GREET` as a possible clue to the built-in Greet interaction. The reviewed native/prompt research does not establish that `PP_GREET` is a keyboard glyph, a control-action hash, or a supported way to assign a type to a custom UI prompt. Keep that interpretation unverified.

Treat these findings as **documented in external research**, not proof about this project's installed v2.2.0 wrapper or in-game behavior.

## Documented external control research

External RDR2 control research lists these candidate hashes in or around the `InteractionLockOn` context:

- `INPUT_INTERACT_LOCKON_ROB` — `0x9FA5AD07`
- `INPUT_INTERACT_LOCKON_POS` — `0xF63A17F9`
- `INPUT_INTERACT_LOCKON_NEG` — `0x26A18F47`
- `INPUT_INTERACT_OPTION1` — `0x760A9C6F`
- `INPUT_INTERACT_OPTION2` — `0x84543902`
- `INPUT_INTERACT_LOCKON` — `0xF8982F00`
- `INPUT_EMOTE_GREET` — `0x72BAD5AA`
- `INPUT_EMOTE_TAUNT` — `0x470DC190`
- `INPUT_EMOTE_ACTION` — `0x13C42BB2`

The external list reports `INPUT_INTERACT_OPTION1` as G and `INPUT_INTERACT_OPTION2` as H in its listed PC mapping. Bindings and behavior may vary with context and user settings. These values are diagnostic candidates only; the sniffer log and in-game observations must establish what happens in this setup.

## Current sniffer test plan

- Replace the previous prompt test with the read-only sniffer in `Test.cs`.
- At startup, record which PAD and target-state wrappers were resolved by reflection.
- While focused on a human NPC, sample the candidate controls and log their pressed-state changes and just-pressed events.
- Sample target existence, human status, health, combat, fleeing, shooting, and ragdoll state when the wrappers are available.
- Continue target-state sampling briefly after focus ends to capture immediate response changes.
- Do not create prompts, register ILOs, activate prompt groups, suppress controls, or invoke the built-in actions from script.
- Perform one visible menu action at a time. Send the log plus a description of the displayed option and the NPC's visible response.

A successful control-state read only reports what the control wrapper observed. A target-state change is evidence of an observable response, but absence of a logged state change does not mean the action failed; dialogue, animation, suspicion, or other internal effects may not be represented by the sampled state values.

## Open questions

- Which candidate control edges correspond to Aim at, Rob, Greet, and Antagonize in this installed game/control configuration?
- Does the sniffer resolve the PAD wrappers in the installed v2.2.0 API assembly?
- Which target-state wrappers are available in the installed API and which return useful data in this context?
- What visible NPC response follows each action, and how quickly does it occur?
- Do Greet and Antagonize require a follow-up emote selection after their initial input?
- Can any supported public API expose the built-in menu entries or internal interaction result directly?
- Does the interaction menu change its available controls based on weapon state, distance, honor, NPC type, mission state, or other conditions?
- Which supported public prompt API, if any, can add a new action to the focused-entity interaction menu?

## Clarification from the native-option test

The user confirmed that the built-in interaction options were pressed one at a time. The user observed that Rob, Threaten, and Aim At caused the NPC to flee or become hostile.

The tested NPC did not show a recruitment option. The user therefore did not press G. This run does not test recruitment, does not confirm the G binding, and does not show that a custom recruitment prompt works.

The current `Test_Log.txt` records presses for `INPUT_INTERACT_LOCKON_POS` and `INPUT_INTERACT_LOCKON_ROB`, among other focus/aim input states. It does not record a press of `INPUT_INTERACT_OPTION1` or `INPUT_INTERACT_LOCKON_NEG`. Do not infer that Threaten maps to the negative-interaction hash, or that Aim At maps to a particular candidate hash, from the visible behavior alone.

The user’s visual report supplements the sniffer log. The sniffer observes candidate control states and selected ped properties; it does not intercept the game’s internal interaction result or prove which input caused a state change.

## Current next-test procedure

Use the read-only sniffer. In Story Mode, focus one human NPC and test only an option that is visibly present. Record the exact visible label and the NPC’s response separately for each action. Do not test G when no G option is displayed. Send the generated `Test_Log.txt` with those observations.

The sniffer writes `Test_Log.txt` under the game’s base directory. To have the game write through to the repository copy on Windows, create a symbolic link in the game directory pointing to the repository’s `Test_Log.txt`; remove or rename any existing regular file of that name first. The link must be created in the local checkout and is not created by this chat.
