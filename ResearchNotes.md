# Research Notes

## Objective

Display **G Recruit** in the unarmed lock-on menu when the player focuses on a human NPC.

This is a prompt-display test only. Pressing G must not recruit the NPC or change gang membership.

## Current repository state

The current `Test.cs` uses:

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) for G.
- `VAR_STRING(10, "LITERAL_STRING", "Recruit")`, with the returned managed string passed to `_UI_PROMPT_SET_TEXT`.
- A standard prompt with attribute `0x12` enabled.
- The focused human NPC's target prompt group, tab 0.
- Visible-then-enabled ordering.
- No ILO registration.

The current test is display-only: pressing G only logs that the prompt was pressed.

The current `Test_Log.txt` is from the `UseVarStringResult = true` run. It records the prompt being registered, the target group being assigned, and the prompt becoming active (`active=False`, then `active=True`). It does not record ILO registration. The Bootstrap records the latest user-verified visual result: **G Recruit did not appear in the unarmed lock-on menu**.

An active prompt is not proof that it is visibly rendered in the focused lock-on menu.

## Context and API findings

The ScriptHookRDRNetAPI inventory is for version `2.2.0.0`.

The Dog Companion Ghidra trace shows a standard prompt being created with a `VAR_STRING` label, assigned to the target entity's prompt group, and then made visible and enabled. No ILO-registration call appears in that traced creation-and-attachment path.

The API inventory documents:

- `RDR2.Native.HUD._UI_PROMPT_SET_GROUP(int prompt, int groupId, int tab)`
- `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(int entity)`
- `RDR2.Native.HUD._UI_PROMPT_SET_TEXT(int prompt, string text)`
- `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(int entity, string text, float radius, float p3, int flag, float p5, float p6, int prompt, bool p8, int p9)`, returning `bool`
- `RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(int entity)`, returning `bool`

The entity group and ILO registration are separate mechanisms. The group prompt is the candidate action shown when the entity is focused. The ILO registration has separate approach-prompt parameters; its `prompt` argument is the eighth parameter.

The proposal to pass the registered G prompt handle as that ILO `prompt` argument is unverified. Do not treat it as a confirmed requirement or native behavior.

## Next test: ILO prompt-argument comparison

The new `Test.cs` adds ILO registration while preserving the current prompt creation and target-group setup. It defaults to `UsePromptHandleForIlo = true`.

Run the test with that setting, record the log and the visual result, then change only that constant to `false` and repeat under the same conditions. The false run registers the ILO with prompt argument `0`; the true run uses the registered G prompt handle.

The two runs keep the other ILO arguments fixed:

- Radius: `3.0f`
- Focus distance (`p3`): `3.0f`
- Flag: `0`
- `p5`, `p6`: `0.0f`
- `p8`: `false`, suppressing the approach prompt during this focus-menu test
- `p9`: `-1`

These are controlled test values, not values established by the Dog Companion trace. The test logs the registration and unregistration return values, target handle, prompt argument, prompt-group ID, active state, and G press. It does not recruit or alter gang membership.

Unregister the test ILO and delete the prompt on target change or script abort. Because ILO behavior and the meaning of several parameters are not fully established here, observe any change to the NPC's existing focus prompts and report it with the log.

## Interpretation

- If only the handle run displays G Recruit, the result supports the handle-argument hypothesis for this setup; it does not establish that all ILO prompt handles work.
- If both runs display it, the test does not show that the handle is needed.
- If neither run displays it, the ILO argument alone is not the fix. Continue investigating why the entity-group prompt is active but not visible.
- If registration returns `false`, record that separately from whether the HUD prompt appears.
