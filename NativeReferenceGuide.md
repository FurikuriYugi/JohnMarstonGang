# Native Reference Guide

## Project API and lifecycle

- The project targets ScriptHookRDR2DotNet-V2 API version 2.2.0.
- Use generated native wrappers from `RDR2.Native`.
- `Script.Tick` is an event. Subscribe to it; do not override it.
- Use the API DLL and ASI from the same release archive.
- Verify exact wrapper signatures against the project's API context files when adding native calls.
- Do not use `System.Text.Json`; use Newtonsoft.Json if JSON is needed.
- API release notes state that implicit conversions exist between `int`, `PoolObject`-derived types such as `Entity` and `Ped`, and `Player`. Still check the generated declaration when using output parameters.

## Standard UI prompt lifecycle

A standard UI prompt generally follows this lifecycle:

1. Call `_UI_PROMPT_REGISTER_BEGIN`.
2. Set the control action and text.
3. Call `_UI_PROMPT_REGISTER_END`.
4. Assign the prompt to the appropriate prompt group.
5. Enable and show it only while the intended target or context is active.
6. Delete it during script cleanup.

`_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` retrieves the prompt group for a target entity. `_UI_PROMPT_SET_GROUP` assigns a prompt to a group. The current test uses the focused NPC’s group so that “Recruit” can appear with the game’s existing contextual options.

## Focused interaction target

The focused interaction target for the current test is obtained with:

```text
RDR2.Native.PLAYER.GET\_PLAYER\_INTERACTION\_TARGET\_ENTITY
```