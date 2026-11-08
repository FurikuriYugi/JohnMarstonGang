# Native Reference Guide

## Purpose

Record API and native patterns relevant to the isolated NPC recruitment-prompt test. The installed ScriptHookRDR2DotNet-V2 v2.2.0 assembly and the repository’s API context files are the authority for wrapper availability and signatures.

## Project API and lifecycle

- The mod’s entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes the required native.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not infer meanings for parameters named `p2`, `p3`, `p4`, or similar.
- Keep target selection, prompt creation, group assignment, group activation, input detection, and recruitment execution as separate testable operations.
- The v2.2.0 wrapper exposed by the compiler is `MISC.VAR_STRING(int, params InputArgument[])`, not a generic method. Do not call it with type arguments.
- The prompt-text wrapper expects a `string`; pass the literal text directly rather than passing an `IntPtr` from `VAR_STRING`.
- Wrapper presence, a successful invocation, or a native return value does not by itself establish an in-game visual effect.
- Do not use `System.Text.Json`; use Newtonsoft.Json when JSON is needed.

## Target selection and nearby peds

The focused test uses `Game.Player.IsFreeFocusing` and `PLAYER.GET_PLAYER_TARGET_ENTITY` to resolve the current human focus target, then falls back to `Game.Player.GetFreeAimEntity()`.

The v2.2.0 wrapper recorded for `GET_PLAYER_TARGET_ENTITY` is:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```