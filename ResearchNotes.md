# G Recruit lock-on prompt research

## Objective

Display **G Recruit** in the unarmed lock-on menu when the player focuses on a human NPC.

This is a prompt-display test only. Do not recruit the NPC or change gang membership.

## Safety constraints

- Do not press G during display tests.
- Do not add recruitment or gang-membership actions to the test.
- Unregister any ILO registration when the target changes or the script aborts.
- Save each test run's log separately before running another test.

## API and reference findings

The ScriptHookRDRNetAPI inventory used for this research is version `2.2.0.0`.

The Dog Companion Ghidra trace shows a standard prompt being created with a `VAR_STRING` label, assigned to the target entity's prompt group, and then made visible and enabled. No ILO-registration call appears in the traced creation-and-attachment path.

The API inventory documents:

- `RDR2.Native.HUD._UI_PROMPT_SET_GROUP(int prompt, int groupId, int tab)`
- `RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(int entity)`
- `RDR2.Native.HUD._UI_PROMPT_SET_TEXT(int prompt, string text)`
- `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(int entity, string text, float radius, float p3, int flag, float p5, float p6, int prompt, bool p8, int p9)`, returning `bool`
- `RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(int entity)`, returning `bool`

The target prompt group and ILO registration are separate mechanisms. A prompt being registered or active does not prove that it is visibly rendered in the focused lock-on menu.

Passing a registered HUD prompt handle as the ILO `prompt` argument was an experimental variation, not an established requirement.

## Test history and results

### Standard HUD prompt test

The test created a standard prompt with:

- `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) for G.
- `VAR_STRING(10, "LITERAL_STRING", "Recruit")`, with the returned string passed to `_UI_PROMPT_SET_TEXT`.
- Prompt attribute `0x12` enabled.
- The focused human NPC's target prompt group, tab 0.
- Visible-then-enabled ordering.

The prompt was registered, assigned to a target group, and reported active. The user observed that **G Recruit did not appear** in the unarmed lock-on menu.

### Combined HUD prompt and ILO test

The test added `REGISTER_INTERACTION_LOCKON_PROMPT` while preserving the standard HUD prompt setup.

The ILO prompt argument was tested as both:

- The registered HUD prompt handle.
- `0`.

The approach-prompt flag was also tested in the reported runs. The user reported that **G Recruit never appeared**.

The test used these experimental values:

- Radius: `3.0f`
- Focus distance (`p3`): `3.0f`
- Flag: `0`
- `p5`, `p6`: `0.0f`
- `p8`: tested as an approach-prompt flag
- `p9`: `-1`

These values have not been established as correct by the Dog Companion trace.

### Current conclusion

- Changing the ILO prompt argument did not make G Recruit appear.
- The reported approach-prompt test also did not make it appear.
- The available results do not establish whether ILO registration succeeded; check and record its return value.
- The combined test did not isolate ILO registration from the standard HUD prompt setup.

## Next test: ILO only

`Test.cs` now performs an ILO-only test:

- It creates no standard HUD prompt.
- It does not assign a HUD prompt group.
- It registers the ILO prompt with argument `0`.
- It uses the same fixed ILO parameters for the run.
- It unregisters the ILO prompt when the target changes or the script aborts.
- It does not perform any action when G is pressed.

Use the same unarmed lock-on conditions. Do not press G.

Record:

- Whether G Recruit appears.
- The focused target handle and whether the target is human.
- `ILO REGISTER RESULT`, including its return value.
- `ILO UNREGISTER RESULT`, including its return value.
- Any change to the NPC's existing lock-on prompts.

Save the log from this run separately.

## Interpreting the ILO-only test

- If G Recruit appears only in ILO-only mode, the standard HUD prompt setup may have interfered with the combined test.
- If G Recruit does not appear and registration returns `false`, investigate why the ILO registration is rejected.
- If G Recruit does not appear and registration returns `true`, the tested call and parameters still do not produce the desired visible entry. A `true` return value is not proof that the prompt was rendered.
