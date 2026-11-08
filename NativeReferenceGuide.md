# Native Reference Guide

## Project API and lifecycle

- The project targets ScriptHookRDR2DotNet-V2 API version 2.2.0.
- Use generated native wrappers from `RDR2.Native`.
- `Script.Tick` is an event. Subscribe to it; do not override it.
- Use the API DLL and ASI from the same release archive.
- Verify exact wrapper signatures against the project's API context files or Visual Studio Object Browser when adding native calls.
- Do not use `System.Text.Json`; use Newtonsoft.Json if JSON is needed.
- API release notes state that implicit conversions exist between `int`, `PoolObject`-derived types such as `Entity` and `Ped`, and `Player`. Check generated declarations when using output parameters.

## Focused interaction target

The user verified this Object Browser declaration:

```csharp
public unsafe static bool GET\_PLAYER\_INTERACTION\_TARGET\_ENTITY(
    int player,
    int* outEntity,
    bool p2,
    bool p3)
```