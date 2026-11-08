# Research Notes

## Focused NPC prompt test

The current `Test_Log.txt` is an 87-line focused prompt input-diagnostic run. It records that the test created a prompt using the `WARDROBE_INSPECT_PROMPT` key, attached it to a focused NPC's prompt group, and observed the prompt become active.

During the focused interval, the log records:

- `UI_PROMPT_IS_ACTIVE=True`.
- `IS_CONTROL_ENABLED=True`.
- A raw control press and just-pressed event for `INPUT_INTERACT_OPTION1` (hash `0x760A9C6F`).
- `UI_PROMPT_IS_PRESSED=False` and `UI_PROMPT_IS_JUST_PRESSED=False` at the recorded heartbeat after that input.
- Prompt-group detach when focus ended.

This verifies that the test observed the prompt active and detected the raw configured control. It does not establish that the prompt itself registered the press or caused the reported animation. The user separately reports seeing `G View` and an animation; retain those as user observations, not as confirmed log outcomes or mod effects.

The current test does not register `REGISTER_INTERACTION_LOCKON_PROMPT` or implement recruitment. It creates a UI prompt and adds it to the focused NPC's prompt group.

## Text and glyph investigation

`WARDROBE_INSPECT_PROMPT` is being used as a game text-label key. The user reports that the displayed wording was `View`; the log shows the key was passed to `UI_PROMPT_SET_TEXT`, but cannot verify the rendered screen text. Treat the key as a candidate label, not as a glyph-plus-text package.

A prompt's control and prompt text are configured separately. The control is `INPUT_INTERACT_OPTION1` (`0x760A9C6F`); the new test uses `VAR_STRING(10, "LITERAL_STRING", "Recruit")` to create custom prompt text. The resulting string handle/value and setter call are logged for diagnosis.

`PP_GREET` did not establish a working custom recruitment label in the previous investigation. `WARDROBE_INSPECT_PROMPT` has a user-reported `View` result, so it serves as the previous game-label baseline. The next test replaces that label with the literal text `Recruit`.

## Next test

- Build and run the replacement `Test.cs`.
- Focus a nearby living human NPC.
- Record whether `G Recruit` appears on screen.
- Press G once while the prompt is visible.
- Record whether the log shows `UI_PROMPT_IS_ACTIVE=True`, a raw `IS_CONTROL_JUST_PRESSED` event, and a prompt-level `UI_PROMPT_IS_JUST_PRESSED` event.
- Record any animation separately. The test does not initiate recruitment or an animation.
- Send the resulting `Test_Log.txt` and in-game observations before changing the test again.
