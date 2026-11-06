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

Use the lock-on target accessor for the native lock-on test. Do not treat free aim as equivalent to the player's focused interaction target. A fallback to free aim may remain in other project behavior, but the lock-on prompt test should distinguish those paths.

## Interaction-lock-on prompt

The installed XML reference recorded in this project documents this signature:

```csharp
RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
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

The native research identifies its arguments as follows:

- `p0`: entity handle.
- `p1`: text shown for the approach/focus prompt.
- `p2`: prompt radius.
- `p3`: focus distance; commonly set to the same value as `p2`.
- `p4`: interaction-lock-on flags.
- `p5`, `p6`: unknown; reported as `0.0` in common game usage.
- `p7`: prompt handle; custom prompt use has not been confirmed. Common usage is `0`.
- `p8`: whether the approach prompt is shown. Focus may still be possible when this is false.
- `p9`: unknown; common usage is `-1`.

Do not treat this native as the G recruitment action prompt. Its role is to register an entity for interaction lock-on and control its approach/focus prompt. The action prompt and its acceptance need to be implemented separately.

### Flags reported by native research

These values are reported for `p4`; only use a flag after confirming that it suits the desired behavior:

- `1`: makes peds less likely to have their prompts shown when multiple interaction-lock-on targets are nearby.
- `8`: suppresses the focus prompt while still allowing focus.
- `64`: prevents the camera from locking to the entity while still allowing the player to look around.
- `256`: allows interaction-lock-on prompts to show without looking directly at the target in third person.
- `512`: hides the prompt icon border.
- `1024`: also reported to suppress the focus prompt while still allowing focus.
- Other flag values remain unknown for this prototype.

### Registration lifecycle

- Register interaction-lock-on for eligible targets as needed; do not continuously accumulate registrations.
- Unregister it when a target is no longer eligible, changes, dies, leaves range, or otherwise needs cleanup.
- The unregister native hash reported by native research is `0xE98D55C5983F2509`; confirm the installed API wrapper name and signature before use.
- The register native hash reported by native research is `0x870708A6E147A9AD`.
- Native research reports that peds can be focused by default; vehicle and object behavior has additional requirements and is outside the current NPC recruitment test.
- Multiple registrations, prioritization, and the exact best registration cadence still require in-game testing.

## Separate UI action prompt

To show and accept a G recruitment action while the target is focused:

1. Create a UI prompt using the installed API's prompt functions.
2. Configure its control and label.
3. Obtain the prompt group associated with the target entity.
4. Add the UI prompt to that group.
5. Detect a single completed/just-completed action and dispatch the existing recruitment logic once.
6. Remove the prompt from the group and delete or disable it when the target is no longer eligible.

The native research reports `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` as hash `0xB796970BD125FCE8` and `_UI_PROMPT_SET_GROUP` as hash `0x2F11D3A254169EA4`. Confirm the installed `RDR2.Native` wrapper names and signatures before using them.

Do not infer that `p7` in `REGISTER_INTERACTION_LOCKON_PROMPT` is the G action prompt. Research could not confirm custom prompt use through that parameter. The UI action prompt is distinct and needs its own input-acceptance check.

### Test control

The controls reference reports:

- `INPUT_INTERACT_LOCKON`: hash `0xF8982F00`; default QWERTY binding Mouse Right and Xbox binding LT.
- `INPUT_INTERACT_OPTION1`: hash `0x760A9C6F`; default QWERTY binding G and Xbox binding D-pad Up.
- `INPUT_INTERACT_LOCKON_ANIMAL`: hash `0x5415BE48`; default QWERTY binding G and Xbox binding X.

For the generic NPC recruit test, `INPUT_INTERACT_OPTION1` is the candidate prompt control because its default keyboard binding is G. Confirm the installed API's supported prompt-control setup and the resulting glyph in game. Do not assume that the physical `G` key and a game control action are interchangeable in every input mode.

Ensure the existing G `KeyDown` recruitment path and the prompt completion path cannot recruit the same target twice.

## API-version notes

- ScriptHookRDR2DotNet-V2 release notes state that `Function.Call()` is not a valid script-level native invocation in v2.1.1.0 and later; use the generated `RDR2.Native` functions.
- Releases advise using the ASI and API DLL files from the same release archive because internal structure may change.
- The repository lists v2.2.0 as a release. Confirm the version actually referenced by this project before implementing native calls.
- v2.1.0.1 release notes state that prompt text did not work in that version. The v2.2.0 release notes do not say whether that issue was resolved. Treat custom prompt text display as unverified until tested in the project’s actual runtime.

## Sources and verification status

The signature above is recorded in the project's installed-XML notes. Argument explanations, flag descriptions, hashes, and prompt-group behavior are documented by external RDR2 native research; they are not confirmed by an in-game test in this project.

- **Documented in local reference:** ILO native signature and `RDR2.Native` usage pattern.
- **Documented in external research:** Argument interpretations, flag notes, hashes, control defaults, and prompt-group behavior.
- **Documented in ScriptHook release notes:** Lock-on target accessor, native-call API changes, and release pairing guidance.
- **Open:** Exact wrapper signatures for prompt-related natives in this project's currently referenced API.
- **Open:** Prompt label display, acceptance detection, cleanup, and controller behavior in game.

## Sources

- Halen84/ScriptHookRDR2DotNet-V2 repository and release notes.
- Published RDR2 prompt research dated December 15, 2023.
- Published RDR2 controls reference.