# Native Reference Guide

## Project API and lifecycle

- The project targets ScriptHookRDR2DotNet-V2 API version 2.2.0.
- Use generated native wrappers from `RDR2.Native`.
- `Script.Tick` is an event. Subscribe to it; do not override it.
- Use the API DLL and ASI from the same release archive.
- The v2.2.0 release notes state that template natives such as `MISC.VAR_STRING` received proper support. Use the project's API context files to verify exact wrapper signatures before calling a native.
- Do not use `System.Text.Json`; use Newtonsoft.Json if JSON is needed.
- Do not treat a wrapper's managed parameter type as proof that the game accepts arbitrary strings in that native.

## Prompt lifecycle

The prompt research describes this general lifecycle:

1. Begin with `_UI_PROMPT_REGISTER_BEGIN`.
2. Set the control and text.
3. Register the prompt with `_UI_PROMPT_REGISTER_END`.
4. Assign it to a prompt group if required.
5. Enable and show it only in the intended situation.
6. Delete it during script cleanup.

Prompt text must follow the game's string/VarString requirements. The example key `CUTSCENE_SKIP` is a pre-existing game string key used as a diagnostic, not as the final recruitment label.

## Custom prompt text limitation

ScriptHookRDR2DotNet-V2 issue #4, “Prompt Text doesn't work with custom strings,” reports that `_UI_PROMPT_SET_TEXT` does not work with custom strings as expected. Its description says the prompt text works when the supplied string is already registered in the game's string database, but not reliably with text created from `MISC.VAR_STRING`.

Consequently:

- A test that passes `"Recruit"` directly to `_UI_PROMPT_SET_TEXT` is not a valid test of whether the prompt group is visible.
- Passing `VAR_STRING` is required by the general RDR2 prompt research, but the API issue means that does not by itself guarantee custom text will render through this wrapper.
- Use a known in-game string key to test prompt/group rendering separately from custom-label support.
- Do not report the desired **G Recruit** label as implemented until a working custom-string route is established and tested.

## Prompt groups

RDR2 prompt research describes two group activation patterns:

- **Active groups** are activated by `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` every frame they should be visible. Active groups are exclusive, so another script activating one may interfere.
- **Ambient groups** are activated in relation to an entity or volume and can be active concurrently with other ambient groups.

The active-group native's documented argument pattern is:

```text
group, name, tabAmount, tabDefaultIndex, p4, prompt
```