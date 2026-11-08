# Native Reference Guide

## Purpose

Record API and native patterns relevant to the NPC recruitment-prompt test. The installed ScriptHookRDR2DotNet-V2 v2.2.0 assembly is the final authority for wrapper availability and signatures.

## Project API and lifecycle

- The mod’s entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the required signature.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not infer meanings for parameters named `p3`, `p5`, or similar.
- Keep target selection, ILO registration, UI-prompt creation, prompt-group attachment, prompt-group activation, input detection, and recruitment execution as separate testable operations.
- For diagnostics, optional native wrappers can be resolved by reflection. Log the resolved signature; wrapper presence and return value do not establish in-game effect.
- Do not use `System.Text.Json`; use Newtonsoft.Json when JSON is needed.

## Target selection and nearby peds

The production path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

The isolated test previously resolved the v2.2.0 wrapper as:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```