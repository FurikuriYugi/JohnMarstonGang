# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Distinguish verified facts from hypotheses and test plans so uncertain API details do not become assumed contracts. Keep the larger gameplay design in `StoryBoard.md`.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note, Object Browser, or installed API documentation, but not necessarily confirmed in game.
- **Documented in external research:** Reported by external native research; not yet confirmed against this project's installed wrapper or in game.
- **Hypothesis:** Plausible usage or interpretation; do not depend on it in production code.
- **Tested in game:** Confirmed in game; record the environment and steps.
- **Open:** Needs further investigation.

## Current focus: Add one action option to NPC interaction prompt

### Goal

While the player is unarmed and right-mouse focusing a human NPC, display a **G Recruit** action in that NPC's interaction prompt group. This is a presentation test only. It must not recruit the NPC or change gang data.

### Scope guard

The current test is only about displaying and completing the temporary action prompt. Do not add recruitment logic, companion behavior, general UI diagnostics, other interaction options, or unrelated game systems to `Test.cs`.

The test may log only what is useful to this question:

- Whether a focused human target was found.
- The target prompt group and action prompt handle.
- ILO registration/unregistration and any setup error.
- Whether the action appeared in game and whether the test G prompt completed.

### Findings from the repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The separate test harness uses `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse it with `Main.cs` target selection.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment prompt is not implemented in the production path.
- `Main.cs` references `RDR2.UI.Prompt.CreatePrompt(...)`. The project bootstrap reports `RDR2.UI.Prompt` is internal, so that API accessibility issue must be resolved before relying on it in production.
- G is the temporary prompt input in this test. The test input must never call the production recruitment path.
- The current project file explicitly lists source files; the updated project file includes `Test.cs` for this isolated test.

### Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; the isolated test uses an unsafe pointer delegate.
- While `IsFreeFocusing=True`, the player-target native returned the focused NPC handle in earlier test cycles. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters: `int, string, float, float, int, float, float, int, bool, int`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` as `UInt64(int, float, int, int, uint, string, int)`.
- The v2.2.0 wrapper resolved `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` as `UInt64(uint, string, int, int, int, int)`.
- These wrapper signatures are not proof that parameter meanings or in-game behavior are correct.

### In-game findings — 2026-11-07

The user tested while unarmed and right-mouse focusing human NPCs.

- Earlier target-detection tests found focused NPC handles.
- Earlier prompt tests assigned custom action prompts to target prompt groups.
- Earlier prompt properties reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`; those properties do not prove that the prompt rendered.
- Ambient-group activation did not make **G Recruit** visible.
- Explicit active-group activation also did not make **G Recruit** visible.
- The latest target-group-only test found focused human targets `197634` and `197378`.
- It created prompts with handles `239874` and `270082` and assigned them to target groups `268566612` and `268566611`.
- The latest test made no ambient-group or active-group activation call.
- The user did not see **G Recruit** in the menu; other menu options continued to work.
- No recruitment or gang-data changes were performed.

**Conclusion:** Focused-target detection and the prompt-creation/group-assignment path ran without a reported setup error, but the action did not visibly render in the tested setup. This rejects the hypothesis that target-group assignment alone is sufficient in this test. It does not identify whether the failure is in prompt construction, the wrapper/runtime interaction, or another display requirement.

### External prompt research

External RDR2 prompt research describes these as separate operations:

1. Create and configure a native UI prompt with a control action and text.
2. Get the prompt group for the target entity and assign the custom prompt to that group.
3. Register an interaction-lock-on prompt on an entity when needed for its focus/approach interaction.

The research reports that peds can be focused by default, and that custom action prompts should be added to the entity's prompt group to show while focused. It does not establish that ambient- or active-group activation is required. It describes the `prompt` parameter of `REGISTER_INTERACTION_LOCKON_PROMPT` as not working with a custom prompt in that research setup. Parameter meanings are partly unknown.

Reference: https://gist.github.com/umaruru/1cdbfc302dda20d8c5601f0ce8f0e03c

Treat these as **documented in external research**, not proof about this project's installed v2.2.0 wrapper or game setup.

### Current test hypothesis

Test whether registering the already-focused human NPC with `REGISTER_INTERACTION_LOCKON_PROMPT` affects display of the same custom action prompt.

Keep the action prompt creation, text, input, target-group assignment, and test conditions unchanged. Do not call ambient-group or active-group activation natives. The only added experiment variable is ILO registration.

Test-only arguments currently used:

- `entity`: focused human target.
- `text`: `"Recruit"`.
- `radius`: `2.0f`.
- `p3`: `2.0f` (described externally as a possible focus distance; meaning unverified).
- `flag`: `0`.
- `p5`, `p6`: `0.0f`.
- `prompt`: `0` (external research reports the custom prompt use could not be made to work).
- `p8`: `false` (external research describes this as suppressing the approach prompt while permitting focus).
- `p9`: `-1`.

These values are an experiment, not a verified native contract. The test unregisters the ILO when focus changes or the test prompt is cleared. Do not use this code in production.

Record only:

1. Whether a focused human target was found.
2. The target prompt group and action prompt handle.
3. Whether ILO registration and cleanup calls resolved and ran.
4. Whether **G Recruit** appeared on screen.
5. If it appeared, whether pressing G produced the test-only completion log.

### Open questions

- Does ILO registration affect display of a custom action prompt for an already-focusable ped?
- Does `Prompt.CreatePrompt` in the installed v2.2.0 API produce a prompt accepted by `_UI_PROMPT_SET_GROUP`?
- Does the action prompt text render consistently with the installed API/runtime DLL pairing?
- Can the same interaction be implemented through a supported public prompt API rather than reflection?
- Which `REGISTER_INTERACTION_LOCKON_PROMPT` parameters are required for an NPC already focusable by the base game?

## ScriptHook API findings

- ScriptHookRDR2DotNet-V2 v2.1.0.0 release notes list `Player.GetInteractionLockonTargetEntity()`, `Player.GetFreeAimEntity()`, and `Player.IsFreeFocusing`.
- Earlier runs found that `Game.Player.GetInteractionLockonTargetEntity()` could return no target after right-clicking an NPC. This does not disprove the separate `PLAYER.GET_PLAYER_TARGET_ENTITY` result.
- The project bootstrap and v2.2.0 Visual Studio Object Browser report `RDR2.Native.Function` is public for the selected v2.2.0 API. Do not apply the v2.1.1.0 release note that made it internal to this v2.2.0 setup without checking the installed assembly.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- v2.1.0.1 release notes mention a prompt-text issue. The v2.2.0 release notes do not establish whether it was resolved.

## Native research findings

### `REGISTER_INTERACTION_LOCKON_PROMPT`

The Visual Studio Object Browser reports this v2.2.0 wrapper signature:

```csharp
RDR2.Native.MISC.REGISTER\_INTERACTION\_LOCKON\_PROMPT(
    int entity,
    string text,
    float radius,
    float p3,
    int flag,
    float p5,
    float p6,
    int prompt,
    bool p8,
    int p9
);
```