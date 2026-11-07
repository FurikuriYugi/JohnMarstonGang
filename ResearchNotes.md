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
- The prompt-creation or group-assignment setup error.
- Whether the action appeared in game and whether the test G prompt completed.

### Findings from the repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The separate test harness uses `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse it with `Main.cs` target selection.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment prompt is not implemented in the production path.
- `Main.cs` references `RDR2.UI.Prompt.CreatePrompt(...)`. The project bootstrap reports `RDR2.UI.Prompt` is internal, so that API accessibility issue must be resolved before relying on it in production.
- G is the temporary prompt input in this test. The test input must never call the production recruitment path.
- The current project file explicitly lists source files and previously omitted `Test.cs`; the updated project file adds it to the compile list for this test.

### Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; the isolated test uses an unsafe pointer delegate.
- In earlier test cycles, while `IsFreeFocusing=True`, the player-target native returned the focused NPC handle. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters: `int, string, float, float, int, float, float, int, bool, int`.
- The wrapper for `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` resolved during the prior test as: `UInt64(int, float, int, int, uint, string, int)`.
- The user’s latest log resolved `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` as:
  `UInt64(uint, string, int, int, int, int)`.
- The active-group test did not make **G Recruit** visible. The previous `Test.cs` called the active-group wrapper in the tick loop; the log line printed when the prompt was created only identified the wrapper signature, not a rendering result.
- These are wrapper signatures, not proof that parameter meanings or in-game behavior are correct.

### In-game findings — 2026-11-07

The user tested while unarmed and right-mouse focusing human NPCs.

- The player-target wrapper found focused NPC handles in two cycles.
- The custom action prompt was assigned to each target's prompt group.
- The prompt properties reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`.
- The ambient-group activation attempt did not make **G Recruit** visible.
- The subsequent explicit active-group activation attempt also did not make **G Recruit** visible.
- Other native interaction-menu options continued to work.
- No recruitment occurred and no gang data changed.

**Conclusion:** Target detection, prompt creation, and prompt-group assignment proceeded without reported setup errors, but the action was not visually confirmed. Prompt properties and native invocation are not proof that the game rendered the action. Explicit active-group activation did not solve the display problem.

### External prompt research

External RDR2 prompt research describes the following as separate operations:

1. Create and configure a native UI prompt with a control action and text.
2. Get the prompt group for the target entity and assign the custom prompt to that group.
3. Register an interaction-lock-on prompt on an entity when needed for its focus/approach interaction.

The research reports that assigning a custom action prompt to the target entity's prompt group is the relevant route for showing it while the entity is focused. It does not establish that `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` is needed for that case. The research also says the `prompt` parameter of `REGISTER_INTERACTION_LOCKON_PROMPT` could not be made to work with a custom prompt; its parameter meanings are partly unknown.

Reference: https://gist.github.com/umaruru/1cdbfc302dda20d8c5601f0ce8f0e03c

Treat these as **documented in external research**, not as proof about the installed v2.2.0 wrapper or this game setup.

### Current test hypothesis

Try the target-group prompt without calling either ambient-group or active-group activation natives. Keep prompt creation, target-group assignment, text, input, and test conditions unchanged. This isolates whether the entity group itself displays the custom prompt while focused.

Do not add recruitment logic, call a fallback interaction system, or change multiple prompt-display variables in the same test.

Record only:

1. Whether a focused human target was found.
2. The target prompt group and action prompt handle.
3. Whether **G Recruit** appeared on screen.
4. If it appeared, whether pressing G produced the test-only completion log.

### Open questions

- Does the entity-target prompt group display a custom prompt without explicit group activation in this installed game/API pairing?
- Does `Prompt.CreatePrompt` in the installed v2.2.0 API produce a prompt that is accepted by `_UI_PROMPT_SET_GROUP`?
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