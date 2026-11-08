# Native Reference Guide

## Purpose

Record API and native patterns relevant to the NPC recruitment-prompt test. The installed ScriptHookRDR2DotNet-V2 v2.2.0 assembly remains the authority for wrapper availability and signatures.

## Project API and lifecycle

- The mod’s entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the required signature.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not infer meanings for parameters named `p3`, `p5`, or similar.
- Keep target selection, prompt creation, prompt-group attachment, group activation, input detection, and recruitment execution as separate testable operations.
- Reflection can diagnose non-generic wrapper signatures, but a resolver that skips methods with `ContainsGenericParameters` cannot resolve template natives such as `MISC.VAR_STRING`. Call the template wrapper directly with an explicit type argument.
- Wrapper presence, a successful invocation, or a native return value does not by itself establish an in-game visual effect.
- Do not use `System.Text.Json`; use Newtonsoft.Json when JSON is needed.

## Target selection and nearby peds

The production path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

The isolated test scans nearby peds for a living human NPC, then checks the player’s lock-on/focus target. The v2.2.0 wrapper recorded for `GET_PLAYER_TARGET_ENTITY` is:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```