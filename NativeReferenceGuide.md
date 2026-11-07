
## `NativeReferenceGuide.md`
# Native Reference Guide

## Purpose

Record the ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the current interaction prototype. Use the project's API version and matching installed API documentation as the implementation contract.

## Project API and lifecycle

- The mod's entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the exact signature.
- The project bootstrap and Visual Studio Object Browser report `RDR2.Native.Function` is public in v2.2.0. Do not apply the v2.1.1.0 restriction to this API version without checking the installed assembly.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not assume a native's parameter meaning from a placeholder name such as `p2`, `p3`, `p5`, or `p9`.
- Keep target selection, ILO registration, action-prompt creation, prompt-group attachment, prompt input detection, and recruitment execution as separate testable steps.

## Player interaction target

ScriptHookRDR2DotNet-V2 v2.1.0.0 release notes list:

- `Player.GetFreeAimEntity()`
- `Player.GetInteractionLockonTargetEntity()`
- `Player.IsFreeFocusing`

The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

In the isolated prompt test, runtime reflection resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as:

```csharp
public unsafe static bool GET_PLAYER_TARGET_ENTITY(
    int player,
    int* entity
);
``*
```