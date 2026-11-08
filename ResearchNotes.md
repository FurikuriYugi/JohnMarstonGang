# Research Notes

## NPC recruitment prompt test

The current test is isolated to displaying a native prompt while the player focuses a nearby living human NPC. Pressing the prompt key only records input; it does not recruit the NPC, change gang membership, or trigger an animation.

## Previous test result

The previous ambient-group test reported that no **G Recruit** prompt appeared after right-clicking an NPC.

The committed `Test_Log.txt` for that run showed:

- The prompt handle was created.
- The prompt text was set to `"Recruit"`.
- The prompt was attached to the target entity’s prompt group.
- The prompt was enabled, and ambient-group activation was invoked while focused.
- `UI_PROMPT_IS_ACTIVE` became true.
- Both the prompt-level and raw-control input probes registered presses.

Those log entries establish that the prompt and input code ran, but they do **not** establish that the prompt was drawn. The in-game observation is the authoritative visual result. The visual failure points to prompt-group rendering or activation, rather than target focus or G input detection.

## Prompt text and input

The test uses:

- Control: `INPUT_INTERACT_OPTION1` (`0x760A9C6F`).
- Prompt text: `"Recruit"`, passed to the v2.2.0 prompt-text wrapper as a `string`.
- Prompt-level probes: `UI_PROMPT_IS_ACTIVE`, `UI_PROMPT_IS_PRESSED`, and `UI_PROMPT_IS_JUST_PRESSED`.
- Raw-control probes: `IS_CONTROL_ENABLED`, `IS_CONTROL_PRESSED`, and `IS_CONTROL_JUST_PRESSED`.

The wrapper exposed by the project’s v2.2.0 build expects `System.String` for `_UI_PROMPT_SET_TEXT`. Do not pass an `IntPtr` from `VAR_STRING` to that wrapper.

## Prompt-group research

RDR2 prompt research describes two group activation patterns:

- **Ambient groups** are associated with an entity or volume and activated with the ambient-group native each frame.
- **Active groups** are explicitly activated each frame and are exclusive: only one active group can be active at a time.

The failed test assigned the prompt to an NPC’s target group and tried to activate that ambient group. The replacement test assigns the prompt to a dedicated test group and activates that group with `_UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME` on each focused tick. This isolates active-group rendering from the NPC’s ambient prompt group.

The active-group parameters used by the test are:

- Dedicated group ID: `0x4A4D47`.
- Group label: `"NPC"`.
- Tab count: `0`.
- Default tab: `0`.
- Remaining parameters: `0`.

The native research describes the group label as a `VAR_STRING` value. The test uses the generated wrapper’s declared parameter type and should be treated as an experiment until the build and in-game visual result confirm it works. A successful native invocation or active-state return alone is not proof that the UI rendered.

Active groups are exclusive and may interfere with another script that also activates an active group. Run this isolated test without other custom prompt tests when possible.

## Next test

1. Replace `Test.cs` with the dedicated active-group version.
2. Build against the project’s ScriptHookRDR2DotNet-V2 v2.2.0 API DLL.
3. If the build succeeds, focus a nearby living human NPC.
4. Record separately whether **G Recruit** is visible and whether pressing G logs prompt-level and raw-control input.
5. Check `Test_Log.txt` for `ACTIVE GROUP ACTIVATION ERROR`, prompt state changes, and input events.
6. Report the visual result and any build/runtime errors.

Do not update `StoryBoard.md` during this testing phase.
