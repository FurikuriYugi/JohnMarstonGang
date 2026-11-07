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
The Visual Studio Object Browser documents this signature:
```csharp
RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
	int entity,
	string text,
	float radius,
	float p3,
	int flag,
	float p5,
	float p6,
	int prompt,
	bool p8,
	int p9
);
```

### Separate UI action-prompt test status

- ScriptHookRDR2DotNet-V2 v2.2.0 release notes state that `RDR2.Native.Function` is public again, while `RDR2.UI.Prompt` remains internal. Use the native-call path for this prototype; do not rely on `Prompt.CreatePrompt(...)`.
- The current test attempt—right-clicking an NPC and pressing G—showed no visible prompt and triggered no recruitment.
- This is a failed end-to-end display attempt, not confirmation of any individual native call. Prompt registration, text conversion, control assignment, group lookup, group attachment, visibility, and acceptance remain unverified in game.
- Keep the test action disconnected from recruitment. Instrument and report each stage separately before changing or integrating the prompt behavior.
