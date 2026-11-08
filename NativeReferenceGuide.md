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
public unsafe static bool GET_PLAYER_TARGET_ENTITY(
    int player,
    int* entity
);
```

The test uses the unsafe pointer form when the player is free-focusing. A previous `out` call failed to compile with CS1615.

The test may use `Game.Player.GetFreeAimEntity()` to find a human entity while aiming. Validate the entity exists and is human before registering an ILO.

## Interaction-lock-on natives

### `REGISTER_INTERACTION_LOCKON_PROMPT`

- Hash: `0x870708A6E147A9AD`
- Locally observed v2.2.0 wrapper parameters: `int, string, float, float, int, float, float, int, bool, int`
- Locally observed return type: `System.Boolean`
- The test resolves the method by exact parameter types using reflection.

The current diagnostic test supplies:

1. target entity handle
2. literal text `Recruit`
3. radius `2.5f`
4. focus distance `2.5f`
5. flags `0`
6. unknown float `0.0f`
7. unknown float `0.0f`
8. custom prompt handle `0`
9. show approach prompt `true`
10. unknown integer `-1`

The radius, focus distance, and boolean interpretation are based on external prompt research. Unknown parameter meanings remain unverified. The native’s signature does **not** contain an input-control/G parameter.

A `true` return indicates only that the native wrapper returned true; it does not prove the text was drawn or that a G action was created.

### `UNREGISTER_INTERACTION_LOCKON_PROMPT`

- Hash: `0xE98D55C5983F2509`
- Locally observed wrapper: one `int` entity-handle parameter
- Locally observed return type: `System.Boolean`
- The test calls it only when registration returned true and the tracked target is cleared or changes.

Do not assume the native return confirms that all visible UI state was removed; record the result and observe the game.

### Externally documented ILO behavior

External RDR2 prompt research describes ILO registration as enabling an entity’s interaction-lock-on/approach prompt. It distinguishes the approach prompt text from the focused ped’s separate prompt/name label. The research reports:

- `p8` controls whether the ILO prompt appears when approaching the entity.
- Peds can be focused by default.
- The custom-prompt parameter did not work with a custom prompt in that research setup and is commonly zero in Rockstar scripts.
- A separate UI prompt can be attached to a target’s prompt group with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` and `_UI_PROMPT_SET_GROUP`.

These are externally documented observations, not proof of the behavior or signatures in the installed wrapper or this game setup.

## Read-only G-candidate input observation

The test uses the locally recorded wrapper signatures:

```csharp
RDR2.Native.PAD.IS_CONTROL_ENABLED(int, uint)
RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(int, uint)
```

It samples:

- `INPUT_INTERACT_OPTION1` — `0x760A9C6F`

External control research reports that this is mapped to G in a listed PC `InteractionLockOn` mapping. The test treats that as a candidate only. It does not disable, consume, or rebind the input, and it does not execute recruitment.

The current test log previously reported `_GET_CURRENT_CONTROL_CONTEXT` as unavailable. Do not depend on that wrapper unless the installed v2.2.0 assembly is checked again and a compatible method is found.

## Read-only target-state wrappers

The previous sniffer log resolved these wrappers:

```csharp
RDR2.Native.ENTITY.DOES_ENTITY_EXIST(int)
RDR2.Native.PED.IS_PED_HUMAN(int)
RDR2.Native.ENTITY.GET_ENTITY_HEALTH(int)
RDR2.Native.PED.IS_PED_IN_COMBAT(int, int)
RDR2.Native.PED.IS_PED_FLEEING(int)
RDR2.Native.PED.IS_PED_SHOOTING(int)
RDR2.Native.PED.IS_PED_RAGDOLL(int)
```

A successful read reports only the value returned by the wrapper. Target-state changes can be useful observations, but they do not prove which internal game action caused them.

## Prompt API research and test boundary

External prompt research describes a standard UI prompt as a separate object that is registered, configured with a control and text, and then registered/assigned to a prompt group as appropriate. It also describes `_UI_PROMPT_SET_GROUP` and active/ambient prompt groups as separate operations.

Several prior tests in this project created standard UI prompts, assigned target groups, and tried ambient/active groups and ordering. The user did not see **G Recruit** in those tests. The current test therefore does not repeat that setup: it calls the ILO registration native directly and observes the candidate G input separately.

The ILO native cannot by itself establish that G activates recruitment. To test an actual G recruitment action later, a supported action-prompt/control-binding mechanism must first be established, and recruitment execution must remain a separate test.

## Safe test interpretation

- A wrapper resolving successfully does not prove its native call has the intended visible effect.
- A successful native return does not prove prompt display.
- The appearance of `Recruit` does not prove the prompt is bound to G.
- A logged `INPUT_INTERACT_OPTION1` press does not prove recruitment was triggered.
- Do not treat a target’s fleeing/combat response as evidence of which control caused it unless the test isolates that input and records timing.
