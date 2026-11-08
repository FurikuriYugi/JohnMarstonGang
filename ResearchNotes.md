### Latest user-reported in-game result: focused G prompt

**User-reported in game; the matching updated log is not present in the repository copy reviewed.**

The user reports that focusing an NPC showed a new `G View` option. Pressing G appeared to trigger an animation.

The current baseline test uses `WARDROBE_INSPECT_PROMPT` as its prompt text. The observation is consistent with the focused prompt rendering and the configured control responding. However, the test’s prompt-pressed handler only logs the press; it does not explicitly invoke an animation or recruitment action. Do not attribute the animation to the test code until a log or controlled comparison establishes the cause.

The `Test_Log.txt` currently available for review is the earlier ILO timing log. It contains a `G CANDIDATE JUST PRESSED` entry but no focused prompt-group, `View`, or animation entry. Treat the new visual result as user-reported until the updated log is available.

### Research update: prompt text and control

External prompt research describes assigning a prompt to a group with `UI_PROMPT_SET_GROUP`. The latest user observation is consistent with the focused-target-group path working, but the matching updated log is needed to confirm the exact runtime path.

ScriptHookRDR2DotNet-V2 issue #4 reports that custom strings created with `VAR_STRING` do not work with `_UI_PROMPT_SET_TEXT` in that framework, while registered string-database keys such as `WARDROBE_INSPECT_PROMPT` do. Therefore, the working `View` baseline does not yet establish that custom `Recruit` text can be displayed.

External control data maps `INPUT_INTERACT_OPTION1` (`0x760A9C6F`) to G for QWERTY PC and D-pad Up for Xbox. Treat this as reference data; the user’s in-game result is the relevant confirmation for their setup.

### Next test

- Preserve the working baseline.
- Capture the updated `Test_Log.txt`, including prompt creation, target group, prompt press, and any animation observation.
- Compare pressing G with the test prompt enabled and disabled to isolate other game interactions on that control.
- Test a custom `Recruit` label separately from prompt visibility and input handling.
- Keep recruitment execution disabled until the prompt and input behavior are isolated.
