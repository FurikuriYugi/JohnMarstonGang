# Native Reference Guide

## Purpose
Record the ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the current interaction prototype. Use the API version referenced by the project and the matching installed API documentation as the implementation contract.

## Project API and lifecycle
- The mod's entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Use the `RDR2.Native` wrappers exposed by the referenced ScriptHookRDR2DotNet-V2 API.
- Do not use `Function.Call`.
- Use API DLLs from the same ScriptHookRDR2DotNet-V2 release archive; do not mix files from different releases.
- Keep native invocation, target selection, prompt state, input acceptance, and action execution in separate, testable steps.
- Do not assume a native's parameter meaning from a placeholder parameter name such as `p4`.
- Confirm exact C# names and parameter types against the installed API wrapper/XML before writing calls.

## Player interaction target
ScriptHookRDR2DotNet-V2 release notes for v2.1.0.0 list:
- `Player.GetFreeAimEntity()`
- `Player.GetInteractionLockonTargetEntity()`
- `Player.IsFreeFocusing`

Use `Game.Player.GetInteractionLockonTargetEntity()` for the focused NPC in this test. Do not treat free aim as equivalent to the player's focused interaction target. Do not add a free-aim fallback to the recruitment prompt test.

## Interaction-lock-on prompt
The installed XML reference recorded in this project documents this signature:
```csharp
RDR2.Native.MISC.REGISTER\_INTERACTION\_LOCKON\_PROMPT(
    int p0,
    string p1,
    float p2,
    float p3,
    int p4,
    float p5,
    float p6,
    int p7,
    bool p8,
    int p9
);
```