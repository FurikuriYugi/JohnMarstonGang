
## `ResearchNotes.md`

# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Distinguish verified facts from hypotheses and test plans so uncertain API details do not become assumed contracts.

Update this file when a native signature is checked, an in-game test is performed, or a design decision changes. Keep the larger gameplay design in `StoryBoard.md`.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note, Object Browser, or installed API documentation, but not necessarily confirmed in game.
- **Documented in external research:** Reported by external native research; not yet confirmed against this project's installed wrapper or in game.
- **Hypothesis:** Plausible usage or interpretation; do not depend on it in production code.
- **Tested in game:** Confirmed in game; record the environment and steps.
- **Open:** Needs further investigation.

## Focus: native interaction-lock-on and recruitment prompt

### Goal

Use the game's right-mouse lock-on interaction while unarmed to focus an eligible NPC, then show a recruitment action prompt using **G** as the test key. The native interaction-lock-on (ILO) prompt and the recruitment action prompt are separate:

- ILO registration makes an entity available for interaction lock-on and controls its approach/focus prompt.
- A UI prompt attached to the target's prompt group displays the recruitment action and handles acceptance.
- The existing recruitment path remains the source of truth for recruiting the NPC.
- F9 remains the separate companion-follow toggle.

### Findings from the current repository

- **Verified in repository:** The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- **Verified in repository:** The existing target-selection path uses `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- **Verified in repository:** **G** is the current test recruitment key. The existing recruitment action should be reused rather than duplicated when the native UI prompt is added.
- **Verified in repository:** F9 toggles companion follow; it is not the recruit action.
- **Verified in repository:** The current custom interaction UI is temporary. Do not treat its current presentation as the intended native prompt.
- **Verified in repository:** The native right-mouse interaction-lock-on recruitment prompt is not yet implemented in the production path.
- **Verified in repository:** `Test.cs` is a separate harness for native target and prompt-state checks. Its target getter must not be confused with the target-selection path in `Main.cs`.
- **Verified in repository:** `Main.cs` currently references `RDR2.UI.Prompt.CreatePrompt(...)`. The project bootstrap says `RDR2.UI.Prompt` is internal, so this is a source/API accessibility conflict, not proof that the prompt API is usable in the current build.
- **Correction:** An earlier version of this note described F7 as the current interaction/recruitment control. That is stale for the current G-key recruitment test and has been corrected.

### ScriptHook API findings

- **Documented in ScriptHook release notes:** v2.1.0.0 added `Player.GetInteractionLockonTargetEntity()`, `Player.GetFreeAimEntity()`, and `Player.IsFreeFocusing`.
- **Verified in game:** In the earlier target-getter test, `PLAYER.GET_PLAYER_TARGET_ENTITY` returned a positive handle corresponding to the NPC selected by right-clicking.
- **Not verified in game:** `Game.Player.GetInteractionLockonTargetEntity()` was used in a later prompt attempt, but that attempt did not establish that this accessor returned the targeted NPC.
- **Correction for API-version notes:** v2.1.1.0 release notes say `RDR2.Native.Function` was made internal. The project bootstrap and v2.2.0 Visual Studio Object Browser report `RDR2.Native.Function` is public for the selected v2.2.0 API. Do not apply the v2.1.1.0 restriction to the v2.2.0 test project without checking the installed assembly.
- **Documented in ScriptHook release notes:** Use the ASI and API DLL files from the same release archive because internal structure may change.
- **Documented in ScriptHook release notes:** v2.1.0.1 noted that prompt text did not work in that release. The v2.2.0 release notes do not state whether this issue was resolved.
- **Open:** Confirm the API/runtime DLL pairing used in game and whether prompt-text rendering works in that pairing.
- **Open:** Confirm the exact v2.2.0 generated wrapper signature for `PLAYER.GET_PLAYER_TARGET_ENTITY` against the installed Object Browser/XML. The test uses the native with an `out` target handle.

### Native-research findings

#### `REGISTER_INTERACTION_LOCKON_PROMPT`

- **Documented in external research:** Native hash `0x870708A6E147A9AD`.
- **Documented in local reference:** Visual Studio Object Browser signature:

  ```csharp
  RDR2.Native.MISC.REGISTER\_INTERACTION\_LOCKON\_PROMPT(
      int entity,
      string text,
      float radius,
      float p3,
      int flag,
      float p5,
      float p6,
      int prompt,
      bool p8,
      int p9
  );
  ```