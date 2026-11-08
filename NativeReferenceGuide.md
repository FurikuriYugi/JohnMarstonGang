# Native Reference Guide

## Project API and lifecycle

- The project targets ScriptHookRDR2DotNet-V2 API version 2.2.0.
- Use generated native wrappers from `RDR2.Native`.
- `Script.Tick` is an event. Subscribe to it; do not override it.
- Use the API DLL and ASI from the same release archive.
- Verify exact wrapper signatures against the project's API context files when adding native calls.
- Do not use `System.Text.Json`; use Newtonsoft.Json if JSON is needed.
- Do not treat a wrapper's managed parameter type as proof that a native accepts arbitrary custom strings.

## General prompt lifecycle

The standard UI prompt lifecycle is:

1. Call `_UI_PROMPT_REGISTER_BEGIN`.
2. Set its control and text.
3. Call `_UI_PROMPT_REGISTER_END`.
4. Assign the prompt to the appropriate prompt group.
5. Enable and show it only in the intended situation.
6. Delete it during script cleanup.

The current test uses the existing game string key `CUTSCENE_SKIP` as a rendering probe. It is not the final recruitment label.

## Prompt text and custom strings

RDR2 prompt research describes prompt text as a `VAR_STRING`. ScriptHookRDR2DotNet-V2 issue #4 reports that `_UI_PROMPT_SET_TEXT` does not work reliably with custom strings and that already registered game strings work where custom text does not.

Consequently:

- Passing `"Recruit"` directly to `_UI_PROMPT_SET_TEXT` is not a valid way to isolate prompt-group rendering.
- A `VAR_STRING`-created value is not guaranteed to render as custom prompt text through this wrapper.
- Use a known game string key to test prompt and group behavior separately from custom-label support.
- Do not report the desired “G Recruit” label as implemented until a working custom-string route is established and tested.

The approach-label text argument to `REGISTER_INTERACTION_LOCKON_PROMPT` is a separate native argument and should not be assumed to have the same custom-string behavior as `_UI_PROMPT_SET_TEXT`. The current test uses `CUTSCENE_SKIP` there as a diagnostic key; its in-game display still needs testing.

## Interaction-lock-on registration

`REGISTER_INTERACTION_LOCKON_PROMPT` (`0x870708A6E147A9AD`) is the entity-specific interaction-lock-on registration native. It is distinct from creating a standard UI prompt and assigning that prompt to an entity's prompt group.

The researched native argument pattern is:

```text
entity, text, radius, focusDistance, flags, p5, p6, prompt, showApproachPrompt, p9
```