# Native Reference Guide

## Project API and lifecycle

- The project targets ScriptHookRDR2DotNet-V2 API version 2.2.0.
- Use generated native wrappers from `RDR2.Native`.
- `Script.Tick` is an event. Subscribe to it; do not override it.
- Use the API DLL and ASI from the same release archive.
- The v2.2.0 release notes include support updates to the native invocation system. Verify exact wrapper signatures against the project's API context files when adding native calls.
- Do not use `System.Text.Json`; use Newtonsoft.Json if JSON is needed.
- Do not treat a wrapper's managed parameter type as proof that a native accepts arbitrary strings.

## Prompt lifecycle

The general prompt lifecycle is:

1. Begin with `_UI_PROMPT_REGISTER_BEGIN`.
2. Set the control and text.
3. Finish with `_UI_PROMPT_REGISTER_END`.
4. Assign the prompt to the appropriate group.
5. Enable and show it only in the intended situation.
6. Delete it during script cleanup.

The test prompt uses the existing game string key `CUTSCENE_SKIP` as a rendering probe. It is not the final recruitment label.

## Prompt text and custom strings

RDR2 prompt research describes prompt text as a `VAR_STRING`. However, ScriptHookRDR2DotNet-V2 issue #4 reports that `_UI_PROMPT_SET_TEXT` does not work reliably with custom strings and that already registered game strings work where custom text does not.

Consequently:

- Passing `"Recruit"` directly to `_UI_PROMPT_SET_TEXT` is not a valid way to isolate prompt-group rendering.
- A `VAR_STRING`-created value is not guaranteed to render as custom prompt text through this wrapper.
- Use a known game string key to test prompt and group behavior separately from custom-label support.
- Do not report the desired “G Recruit” label as implemented until a working custom-string route is established and tested.

## Interaction-lock-on target group

For a prompt intended to appear when an entity is focused, use the entity-specific prompt group:

- `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` (`0xB796970BD125FCE8`) obtains the prompt group for a target entity.
- `_UI_PROMPT_SET_GROUP` (`0x2F11D3A254169EA4`) assigns the prompt to a group.
- The diagnostic obtains the current focused entity, gets its group, assigns the prompt to that group, and enables the prompt only for an eligible human target.

This is distinct from registering a prompt that appears when approaching an entity. `REGISTER_INTERACTION_LOCKON_PROMPT` (`0x870708A6E147A9AD`) is a separate interaction-lock-on prompt mechanism; the current diagnostic does not call it.

## Active prompt groups

`_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` activates an active prompt group each frame. RDR2 prompt research describes entity focus prompts as using the target entity's group instead. The diagnostic therefore does not manually activate a custom active group.

The documented active-group argument pattern is:

```text
group, name, tabAmount, tabDefaultIndex, p4, prompt
```