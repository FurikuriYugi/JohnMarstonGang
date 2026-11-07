# Native Reference Guide

## Purpose

Record the ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the current interaction prototype. Use the API version referenced by the project and the matching installed API documentation as the implementation contract.

## Project API and lifecycle

- The mod's entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the exact signature.
- The project bootstrap and Visual Studio Object Browser report that `RDR2.Native.Function` is public in v2.2.0. This permits the native-call-by-hash route used by the isolated `Test.cs` prompt prototype.
- Do not generalize that v2.2.0 behavior to v2.1.1.0: that release note says `RDR2.Native.Function` was made internal.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive; do not mix files from different releases.
- Do not assume a native's parameter meaning from a placeholder parameter name such as `p3`, `p5`, or `p9`.
- Keep target selection, ILO registration, action-prompt creation, prompt-group attachment, prompt input detection, and recruitment execution as separate testable steps.

## Player interaction target

ScriptHookRDR2DotNet-V2 release notes for v2.1.0.0 list:

- `Player.GetFreeAimEntity()`
- `Player.GetInteractionLockonTargetEntity()`
- `Player.IsFreeFocusing`

The repository's current `Main.cs` target-selection path uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

The isolated target/prompt test has a separate baseline: in the earlier in-game test, `PLAYER.GET_PLAYER_TARGET_ENTITY` returned a positive target handle corresponding to the NPC selected by right-clicking. The later failed prompt test used `Game.Player.GetInteractionLockonTargetEntity()` instead, so that run did not verify the known-working getter.

## Native interaction-lock-on prompt

The Visual Studio Object Browser documents this v2.2.0 wrapper signature:

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
````