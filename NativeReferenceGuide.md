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
- Keep target selection, ILO registration, action-prompt creation, prompt-group attachment, prompt-group activation, prompt input detection, and recruitment execution as separate testable steps.

## Player interaction target

ScriptHookRDR2DotNet-V2 v2.1.0.0 release notes list:

- `Player.GetFreeAimEntity()`
- `Player.GetInteractionLockonTargetEntity()`
- `Player.IsFreeFocusing`

The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

In the isolated test, runtime reflection resolved `PLAYER.GET_PLAYER_TARGET_ENTITY` as:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```

### Prompt control value — unresolved

The test has used two different kinds of values:

- `0x760A9C6F`: documented externally as the G keyboard-key hash. It has not been confirmed as a valid prompt input action in the installed v2.2.0 API.
- `INPUT_CONTEXT_X` (`0xE3BF959B`): documented externally as a prompt input action, commonly mapped to R. In the diagnostic in-game run, the prompt did not appear and the existing R “Greet” option disappeared.

A diagnostic test using `INPUT_CONTEXT_B` (`0x3B24C470`), commonly mapped to F, is next. Change only the prompt input action from X to B; retain the same prompt creation, target-group assignment, ILO arguments, and conditions. This remains an external-reference hypothesis until checked in the installed API and in game.

A prompt action’s default keyboard mapping is not a guarantee that the action is conflict-free in a particular interaction context. Do not consider the intended G binding verified until a G prompt is visibly displayed and can be completed in game.
