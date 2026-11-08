# Native Reference Guide

## Purpose

Record ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the NPC interaction test. Use the installed API version and matching documentation as the implementation contract.

## Project API and lifecycle

- The mod’s entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the exact signature.
- The project bootstrap and Visual Studio Object Browser report `RDR2.Native.Function` is public in v2.2.0. Verify the installed assembly before relying on that.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not infer parameter meanings from placeholder names such as `p3`, `p5`, or `p9`.
- Keep target selection, ILO registration, UI-prompt creation, prompt-group attachment, prompt-group activation, input detection, and recruitment execution as separate testable steps.
- The user reports that the only `Prompt`-named API type in the installed API is `RDR2.UI.PromptTimingEvent`. Do not assume `RDR2.UI.Prompt.CreatePrompt` is the prompt-construction API available in this project.
- For diagnostic code, reflection may be used to resolve optional wrappers at runtime. Log the resolved method signature; do not treat the wrapper’s presence or a native return value as proof of in-game behavior.

## Player interaction target

The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

In the isolated test, the v2.2.0 wrapper was resolved as:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```