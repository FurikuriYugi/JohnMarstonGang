# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Distinguish verified facts from hypotheses and test plans so uncertain API details do not become assumed contracts.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note, Object Browser, or installed API documentation, but not necessarily confirmed in game.
- **Documented in external research:** Reported by external native research; not yet confirmed against this project's installed wrapper or in game.
- **Hypothesis:** Plausible usage or interpretation; do not depend on it in production code.
- **Tested in game:** Confirmed in game; record the environment and steps.
- **Open:** Needs further investigation.

## Current focus: add one action option to NPC interaction prompt

### Goal

While unarmed and right-mouse focusing a human NPC, display a **G Recruit** action in that NPC's interaction prompt group. This is a presentation test only. It must not recruit the NPC or change gang data.

### Scope guard

The current test is only about displaying and completing the temporary action prompt. Do not add recruitment logic, companion behavior, general UI diagnostics, other interaction options, or unrelated game systems to `Test.cs`.

Log only information relevant to:

- Whether a focused human target was found.
- The target prompt group and action prompt handle.
- ILO registration/unregistration and any setup error.
- Whether the action appeared in game and whether the test G prompt completed.

### Findings from the repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The isolated test harness uses `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse it with `Main.cs` target selection.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment prompt is not implemented in the production path.
- The user checked the installed API and reports the only `Prompt`-named API type is `RDR2.UI.PromptTimingEvent`. Do not rely on `RDR2.UI.Prompt.CreatePrompt`; it is not the prompt-construction API available in this project.
- G is the temporary prompt input in this test. The test input must never call the production recruitment path.
- The current project file explicitly lists source files; the updated project file includes `Test.cs` for this isolated test.

### Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; the isolated test uses an unsafe pointer call.
- While `IsFreeFocusing=True`, the player-target native returned the focused NPC handle in earlier test cycles. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters: `int, string, float, float, int, float, float, int, bool, int`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` as `UInt64(int, float, int, int, uint, string, int)`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` as `UInt64(uint, string, int, int, int, int)`.
- The v2.2.0 wrapper exposes `MISC.VAR_STRING` as `string(int, InputArgument[])`.
- Runtime reflection reported `REGISTER_INTERACTION_LOCKON_PROMPT` and `UNREGISTER_INTERACTION_LOCKON_PROMPT` as returning `System.Boolean`.
- These wrapper signatures do not establish parameter meanings or in-game behavior.
- The latest test file resolves `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` by reflection so the test can log whether the installed HUD wrapper exposes a compatible method. The exact installed wrapper signature remains to be confirmed by that test's log.

### In-game findings — 2026-11-07

The user tested while unarmed and right-mouse focusing human NPCs.

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

### In-game findings — diagnostic `INPUT_CONTEXT_X` run — 2026-11-07

The user tested while unarmed and right-mouse focusing human NPCs.

- The harness identified focused human targets `203010` and `201730`.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `true` for the focused targets.
- `UNREGISTER_INTERACTION_LOCKON_PROMPT` returned `true` for the targets when the diagnostic prompt was cleared.
- The harness created prompt handles `275970`, `302594`, `321282`, and `329730`, and assigned them to target groups `268566622` and `268566617`.
- The diagnostic prompt used `INPUT_CONTEXT_X` (`0xE3BF959B`), commonly mapped to keyboard R.
- The user did not see the Recruit prompt. The existing R “Greet” option disappeared while this diagnostic action was used; other menu options remained usable.
- No recruitment or gang-data changes were performed.

**Conclusion:** Focus detection, ILO calls, prompt creation, and target-group assignment ran without a logged setup error, but the custom prompt was not visibly rendered. The `true` native returns do not prove display. Using `INPUT_CONTEXT_X` interfered with the existing R “Greet” option in that test setup.

### In-game findings — failed reflective `VAR_STRING` attempt — 2026-11-07

- The test found focused human target `198914`.
- The log resolved `MISC.VAR_STRING` as `RDR2.Native.MISC.VAR_STRING(System.Int32, RDR2.Native.InputArgument[]) -> System.String`.
- The test's reflective resolver reported that it could not find a compatible `VAR_STRING` wrapper.
- Prompt setup stopped with `PROMPT SETUP FAILED: No compatible VAR_STRING wrapper was resolved.`
- This happened before prompt registration began. No action prompt was created or assigned to the target group.
- No recruitment or gang-data changes were performed.

**Conclusion:** This was not a prompt-visibility result. The reflection check rejected the observed wrapper signature. The next test bypassed that resolver and called the typed wrapper with an explicit `InputArgument[]`.

### In-game findings — latest G prompt setup attempt — 2026-11-07

The user tested while unarmed and right-mouse focusing a human NPC.

- The test found focused human target `201218`.
- Prompt handle `267010` was registered and assigned to target group `268566621`.
- The prompt used `INPUT_INTERACT_OPTION1` (`0x760A9C6F`), reported as G in the `InteractionLockOn` context.
- The prompt text used the typed `VAR_STRING(10, InputArgument[] { "LITERAL_STRING", "Recruit" })` call.
- The user did not see **G Recruit** in the focused NPC's lock-on menu.
- The test did not register an ILO and did not activate ambient or active prompt groups.
- The optional standard-mode completion wrapper was not resolved. The prompt remained active until focus changed and was then deleted.
- No recruitment or gang-data changes were performed.

**Conclusion:** This is a visibility failure for the typed G prompt setup: target detection, prompt registration, and target-group assignment succeeded, but the user did not see the action. The missing completion wrapper does not explain the missing display.

### In-game findings — latest ILO-enabled G prompt attempt

The user tested while unarmed and right-mouse focusing a human NPC. The supplied log does not include a calendar date.

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

### In-game findings — latest group-order test

The user tested while unarmed and right-mouse focusing a human NPC. The supplied log does not include a calendar date.

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

### External prompt research

External RDR2 prompt research describes these as separate operations:

1. Create and configure a native UI prompt with a control action and text.
2. Finish registering that prompt.
3. Get the prompt group for the target entity and assign the created prompt to that group.
4. Register an interaction-lock-on prompt on an entity when needed for its focus/approach interaction.

The research reports that peds can be focused by default. It describes the ILO `prompt` parameter as not working with a custom prompt in that research setup, and says peds' focused action prompts are shown by assigning a UI prompt to the entity's prompt group. Its examples demonstrate group assignment as a separate operation on a registered prompt.

External research also documents `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` (`0x2F385ECC5200938D`). It describes this native as choosing the control used for prompt ordering and says to call it during registration or immediately afterward. The documented ordering examples include several context and quick-select controls, but not `INPUT_INTERACT_OPTION1`. Whether this native accepts or helps order the test's G action remains unverified.

The test discussion raised `PP_GREET` as a possible clue to the built-in Greet interaction. The reviewed native/prompt research does not establish that `PP_GREET` is a keyboard glyph, a control-action hash, or a supported way to assign a type to a custom UI prompt. Keep that interpretation unverified.

Treat these findings as **documented in external research**, not proof about this project's installed v2.2.0 wrapper or game behavior.

### Current test hypothesis and next run

The latest group-order test did not display **G Recruit**. The next test keeps the prior setup and adds only an explicit input-ordering call:

- Keep the test's `INPUT_INTERACT_OPTION1` control (`0x760A9C6F`) and its reported default G mapping.
- Keep focused-human target selection, ILO parameters, target prompt group, prompt text, prompt configuration, and group assignment after `_UI_PROMPT_REGISTER_END`.
- Resolve `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` from the installed HUD wrapper and record the resolved method signature.
- If a compatible wrapper is available, call it for the G control during prompt registration, before `_UI_PROMPT_REGISTER_END`.
- If no compatible wrapper is available, log that result and do not treat the run as a prompt-visibility test.
- Do not add ambient-group or active-group activation, another prompt control, or recruitment behavior.
- Record whether ILO registration returns `true`, whether ordering setup succeeds, whether prompt registration ends, whether group assignment completes, whether **G Recruit** is visible, and whether the optional standard-mode completion check reports completion.

This tests whether explicitly setting the prompt's ordering input affects rendering. External research's ordering examples do not establish that `INPUT_INTERACT_OPTION1` is a supported ordering input. A successful native call still does not prove that the action rendered.

### Open questions

- Does `_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE` accept `INPUT_INTERACT_OPTION1` for ordering this focused-entity action?
- Does explicitly setting that input ordering make the custom action visible?
- Does the action prompt text render consistently with the installed API/runtime DLL pairing?
- Can the same interaction be implemented through a supported public prompt API rather than reflection?
- Which `REGISTER_INTERACTION_LOCKON_PROMPT` parameters are required for an NPC already focusable by the base game?
- Does the typed `VAR_STRING` call compile and produce text accepted by `_UI_PROMPT_SET_TEXT` in the installed v2.2.0 wrapper?
