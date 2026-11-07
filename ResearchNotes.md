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

## Focus: native interaction-lock-on and recruitment prompt

### Goal

Use the game's right-mouse lock-on interaction while unarmed to focus an eligible NPC, then show a recruitment action prompt using **G** as the test key. The native interaction-lock-on (ILO) prompt and the recruitment action prompt are separate:

- ILO registration makes an entity available for interaction lock-on and controls its approach/focus prompt.
- A UI prompt attached to the target's prompt group displays the recruitment action and handles acceptance.
- The existing recruitment path remains the source of truth for recruiting an NPC.
- F9 remains the separate companion-follow toggle.

### Findings from the current repository

- The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- The existing target-selection path uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- **G** is the current test recruitment key. The existing recruitment action should be reused rather than duplicated when the native UI prompt is added.
- F9 toggles companion follow; it is not the recruit action.
- The native right-mouse interaction-lock-on recruitment prompt is not yet implemented in the production path.
- `Test.cs` is a separate harness for native target and prompt-state checks. Its target getter must not be confused with the target-selection path in `Main.cs`.
- `Main.cs` references `RDR2.UI.Prompt.CreatePrompt(...)`. The project bootstrap reports `RDR2.UI.Prompt` is internal, so this is an API accessibility issue to resolve before relying on it in the production path.
- An earlier version of this note described F7 as the current interaction/recruitment control. That is stale for the current G-key recruitment test.

### ScriptHook API findings

- ScriptHookRDR2DotNet-V2 v2.1.0.0 release notes list `Player.GetInteractionLockonTargetEntity()`, `Player.GetFreeAimEntity()`, and `Player.IsFreeFocusing`.
- `PLAYER.GET_PLAYER_TARGET_ENTITY` previously returned a positive handle corresponding to the NPC selected by right-clicking.
- Reflection resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` at runtime as `bool(int, int*)`. Do not call it with `out`; a prior such attempt failed to compile with CS1615.
- In earlier runs, `Game.Player.GetInteractionLockonTargetEntity()` returned no target after right-clicking an NPC. This does not disprove the separate `PLAYER.GET_PLAYER_TARGET_ENTITY` result.
- The project bootstrap and v2.2.0 Visual Studio Object Browser report `RDR2.Native.Function` is public for the selected v2.2.0 API. Do not apply the v2.1.1.0 release note that made it internal to this v2.2.0 setup without checking the installed assembly.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- v2.1.0.1 release notes mention a prompt-text issue. The v2.2.0 release notes do not establish whether it was resolved.
- Confirm that the API/runtime DLL pairing used in game is consistent and continue testing prompt-text rendering.

### Native research findings

#### `REGISTER_INTERACTION_LOCKON_PROMPT`

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