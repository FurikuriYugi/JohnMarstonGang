# Native Reference Guide

## Purpose

Record ScriptHookRDR2DotNet-V2 API patterns and native declarations relevant to the current interaction test. Use the project API version and matching installed API documentation as the implementation contract.

## Project API and lifecycle

- The mod's entry class derives from `RDR2.Script`.
- `Script.Tick` is an event. Subscribe to it; do not implement `public override void Tick()`.
- Prefer generated `RDR2.Native` wrappers when the installed API exposes and confirms the exact signature.
- The project bootstrap and Visual Studio Object Browser report `RDR2.Native.Function` is public in v2.2.0. Do not apply the v2.1.1.0 restriction to this API version without checking the installed assembly.
- Use the ASI and API DLL files from the same ScriptHookRDR2DotNet-V2 release archive.
- Do not assume a native's parameter meaning from a placeholder such as `p2`, `p3`, `p5`, or `p9`.
- Keep target selection, ILO registration, action-prompt creation, prompt-group attachment, prompt-group activation, prompt input detection, and recruitment execution as separate testable steps.
- The user reports that the only `Prompt`-named API type in the installed API is `RDR2.UI.PromptTimingEvent`. Do not use `RDR2.UI.Prompt.CreatePrompt` as an assumed prompt-construction API.
- For diagnostic code, reflection may be used to resolve optional wrappers at runtime. Log the resolved method signature and do not treat a wrapper's presence or a native return value as proof of in-game behavior.

## Player interaction target

ScriptHookRDR2DotNet-V2 v2.1.0.0 release notes list:

- `Player.GetFreeAimEntity()`
- `Player.GetInteractionLockonTargetEntity()`
- `Player.IsFreeFocusing`

The production target-selection path in `Main.cs` uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.

In the isolated test, the v2.2.0 wrapper was resolved as:

```csharp
public unsafe static bool GET\_PLAYER\_TARGET\_ENTITY(
    int player,
    int* entity
);
```

## Read-only interaction sniffer

The current v2.2.0 test log resolved these wrappers:

- `PAD.IS_CONTROL_JUST_PRESSED(int, uint)`
- `PAD.IS_CONTROL_PRESSED(int, uint)`
- `PAD.IS_CONTROL_ENABLED(int, uint)`
- `ENTITY.GET_ENTITY_HEALTH(int)`
- `PED.IS_PED_IN_COMBAT(int, int)`
- `PED.IS_PED_FLEEING(int)`
- `PED.IS_PED_SHOOTING(int)`
- `PED.IS_PED_RAGDOLL(int)`

`PLAYER.GET_PLAYER_TARGET_ENTITY` was previously validated in this project as an unsafe pointer call with signature `bool(int, int*)`. The current sniffer uses it only while the player is free-focusing and then checks that the entity exists and is human.

The test log reported `_GET_CURRENT_CONTROL_CONTEXT` as unavailable. Do not depend on that wrapper unless the installed v2.2.0 assembly is checked again and a compatible method is found.

The listed control hashes are diagnostic candidates. Enabled/pressed observations and the user’s visible menu/action report should be recorded separately; a hash label or observed NPC reaction alone does not establish the game’s internal action mapping.
