# Research Notes

## Current test objective

Testing the focused NPC prompt path in Red Dead Redemption 2. The immediate goal is to determine which input-query path detects a press on the displayed G prompt. No recruitment behavior is enabled in the test.

## Confirmed in-game result

The user reports that, while right-click focusing an NPC, a `G View` option appeared. Pressing G produced an animation.

This is a user-reported visual/gameplay observation. The test code does not explicitly start an animation or recruit the NPC.

## Latest checked-in test log

`Test_Log.txt` is the focused prompt-group baseline run, not an older unrelated test. It records:

- Prompt handle `217346` created.
- Control action set to `INPUT_INTERACT_OPTION1`, hash `0x760A9C6F`.
- Registered text key `WARDROBE_INSPECT_PROMPT` set.
- Target `208642` selected at distance `1.560238` metres.
- Target focus began at `21:39:57.457`.
- Target prompt group `268566635` obtained and assigned to the test prompt.
- Prompt enabled while focused and detached/disabled after focus ended.
- The direct control availability query reported `enabled=True`.
- Register and unregister wrappers for the interaction lock-on prompt were not found.
- No `FOCUSED PROMPT PRESSED` event appears in this run.

The log confirms the prompt setup and target-group attachment. The user’s report confirms that `G View` was visible and that pressing G produced an animation. The missing pressed event means the test did not record the press through its existing `UI_PROMPT_IS_PRESSED` check; it does not invalidate the user’s observation.

## Research findings

- A common prompt lifecycle is register-begin, configure a control and text, register-end, then enable/disable and delete as needed.
- Prompt libraries distinguish prompt-level states such as pressed, just-pressed, and standard-mode completion. They poll these states every frame while the prompt is enabled.
- Raw control checks are a separate observation channel from prompt-state checks. The next test samples both channels to identify which one detects the G press.
- Prompt groups have different activation patterns. Ordinary active groups may require per-frame activation, while entity-associated/ambient prompt groups are managed differently. Keep the currently visible target-group method unchanged during the input-diagnostic test.
- The installed v2.2.0 API assembly is authoritative for native wrapper availability and signatures. The previous log did not print resolved signatures for the prompt-state probes.
- The ScriptHookRDR2DotNet-V2 issue tracker records an unresolved custom-prompt-string issue. The registered `WARDROBE_INSPECT_PROMPT` baseline is therefore retained; custom `Recruit` text is not part of this test.
- The current log could not resolve `REGISTER_INTERACTION_LOCKON_PROMPT` or `UNREGISTER_INTERACTION_LOCKON_PROMPT` wrappers. Do not treat the ILO approach prompt as working.

## Next test

Use the new `Test.cs` input-diagnostic version.

1. Build and run with the registered `View` prompt unchanged.
2. Focus one human NPC and tap G once while the prompt is visible.
3. Check `Test_Log.txt` for `INPUT EVENT` and `INPUT STATE` entries from both the prompt and raw-control probes.
4. Compare the heartbeat values to determine whether the prompt was active when the key was pressed.
5. Report whether the animation occurred, separately from the diagnostic log.
6. Do not enable recruitment or animation calls until the input path is understood.
