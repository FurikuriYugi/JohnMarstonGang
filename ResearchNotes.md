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
- The active-group wrapper signature or a focused setup error.
- Whether the action prompt appeared in game and whether the test G prompt completed.

### Findings from the repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- The separate test harness uses `PLAYER.GET_PLAYER_TARGET_ENTITY`; do not confuse it with `Main.cs` target selection.
- F9 remains the separate companion-follow toggle.
- The native right-mouse interaction-lock-on recruitment prompt is not implemented in the production path.
- `Main.cs` references `RDR2.UI.Prompt.CreatePrompt(...)`. The project bootstrap reports `RDR2.UI.Prompt` is internal, so that API accessibility issue must be resolved before relying on it in production.
- G is the temporary prompt input in this test. The test input must never call the production recruitment path.

### Verified runtime/API observations

- The installed v2.2.0 wrapper resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as `bool(int, int*)`. Passing it using `out` previously failed with CS1615; the isolated test uses an unsafe pointer delegate.
- In earlier test cycles, while `IsFreeFocusing=True`, the player-target native returned the focused NPC handle. After focus ended, it returned no target.
- The v2.2.0 wrapper resolved `REGISTER_INTERACTION_LOCKON_PROMPT` with ten parameters:
  `int, string, float, float, int, float, float, int, bool, int`.
- The wrapper for `_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME` resolved during the prior test as:
  `UInt64(int, float, int, int, uint, string, int)`.
- These are wrapper signatures, not proof that parameter meanings or in-game behavior are correct.

### In-game findings — 2026-11-07

The user tested while unarmed and right-mouse focusing human NPCs.

- The player-target wrapper found focused NPC handles in two cycles.
- `REGISTER_INTERACTION_LOCKON_PROMPT` returned `True`.
- The custom action prompt was attached to the target's prompt group.
- The prompt properties later reported `Enabled=True`, `Visible=True`, and `HasCompleted=False`.
- The ambient-group wrapper was invoked while the target and action prompt were valid.
- The user confirmed **G Recruit did not appear** in the interaction menu. The user reported that other menu options worked.
- The previous log records the ambient call as invoked; it does not capture or establish a meaningful activation result.
- No recruitment occurred and no gang data changed.

**Conclusion:** Target detection, prompt creation, and prompt-group attachment proceeded without reported setup errors, but the action was not visually confirmed. A prompt's `Visible` property and a native call invocation are not proof that the game rendered the action.

### Next test

Use the small `Test.cs` harness to try explicit active prompt-group activation for the attached target group instead of the previously tested ambient-group call. Keep the test non-recruiting.

Record only:

1. Whether the target was a focused human NPC.
2. Whether the active-group wrapper was found and its runtime signature.
3. Whether **G Recruit** appeared on screen.
4. If it appeared, whether pressing G produced the test-only completion log.

Do not add new activation methods or fallback interaction systems until this experiment is reported and reviewed.

## Open questions

- Does the installed v2.2.0 API expose `UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` or an underscore-prefixed equivalent, and what exact wrapper signature does it expose?
- Do the active-group native's parameters and target prompt-group behavior match the test's hypothesis?
- Does the action appear when that group is activated each tick?
- Does the test prompt text render consistently with the installed API/runtime DLL pairing?
- Can the same interaction be implemented through a supported public prompt API rather than reflection?

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