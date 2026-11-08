# Research Notes

## Latest focused NPC prompt test

The user reports that focusing an NPC showed a `G View` option and pressing G appeared to trigger an animation. Treat the animation as an observation, not as an effect caused by the test: the test's prompt-pressed handler only logs a press and does not call an animation or recruitment action.

The current `Test_Log.txt` is a 26-line focused prompt-group test log. It records that:

- The test could not find compatible `REGISTER_INTERACTION_LOCKON_PROMPT` or `UNREGISTER_INTERACTION_LOCKON_PROMPT` wrappers. ILO registration and cleanup were therefore unavailable in this run.
- The test created a focused prompt using the registered text key `WARDROBE_INSPECT_PROMPT`.
- The test obtained a prompt group for the tracked NPC, attached the prompt, and enabled it when the NPC became focused.
- The test disabled and detached the prompt when focus ended.
- The log contains no `FOCUSED PROMPT PRESSED` entry. It does not confirm a G press during this run.

This is evidence that the test reached the focused prompt-group setup and cleanup path. It does not, by itself, prove that the prompt was visible on screen, that G was pressed while it was active, or that a native lock-on prompt was registered.

## Prompt text and control

The test uses `WARDROBE_INSPECT_PROMPT` as a registered game-string key for the focused prompt. A reported `View` label is consistent with that baseline, but does not establish that arbitrary custom text such as `Recruit` can be displayed.

The control configured by the test is `INPUT_INTERACT_OPTION1` (`0x760A9C6F`). External control data lists it as G for QWERTY PC and D-pad Up for Xbox; the user's in-game control mapping is the relevant confirmation for their setup.

Keep the interaction prompt and any recruitment action separate. The current test does not recruit an NPC and should remain that way until prompt visibility and input handling have been isolated.

## Next test

- Preserve the current `Test.cs` baseline.
- Run the test with an NPC in focus and capture a fresh `Test_Log.txt`.
- While the prompt is active, press G once and check whether the log records `FOCUSED PROMPT PRESSED`.
- Record separately whether `G View` is visible and whether an animation occurs; neither observation alone proves the other.
- The native ILO registration path remains unconfirmed because the current log reports that compatible register and unregister wrappers were not found.
- Test custom `Recruit` text separately from prompt visibility and input handling.
- Keep recruitment execution disabled until these results are isolated.
