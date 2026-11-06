# Native Reference Guide

## Purpose

Record the ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the current interaction prototype. The installed API XML and the project's own source files are the implementation contract.

## Project API and lifecycle

- The mod's entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Use the `RDR2.Native` wrappers exposed by the referenced ScriptHookRDR2DotNet-V2 API.
- Do not use `Function.Call`.
- Keep native invocation, target selection, prompt state, and action execution in separate, testable steps.
- Do not assume a native's parameter meaning from a placeholder parameter name such as `p4`.

## Candidate lock-on native

The installed XML reference recorded in this project documents:

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