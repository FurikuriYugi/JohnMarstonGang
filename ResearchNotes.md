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
- **Tested in game, result incomplete — 2026-11-07:** The user-provided `Test_Log.txt` captured candidate NPC handle `198658` from free aim. The `REGISTER_INTERACTION_LOCKON_PROMPT` wrapper invocation returned without an exception, but this does not establish that the game displayed the prompt or accepted right-mouse focus.
- **Tested in game, result incomplete — 2026-11-07:** On G-key checks where the candidate was still valid and the wrapper-call flag was true, `GetInteractionLockonTargetEntity()` returned no target. The log did not record `IsFreeFocusing`, so it cannot establish whether right-mouse focus was active at those moments.
- **Verified from test log — 2026-11-07:** The previous harness cleared the candidate when neither free aim nor an interaction target was present, then recaptured and re-registered the same NPC. The next test retains a valid candidate until it becomes invalid or another NPC is selected, and logs changes to focus and interaction-target state.
- **Verified from test log — 2026-11-07:** The prior log path was `...\scripts\scripts\Test_Log.txt`. The revised harness writes to `AppDomain.CurrentDomain.BaseDirectory\Test_Log.txt`, which is the scripts folder in the tested setup.
- **Open:** Re-run the test and check whether `IsFreeFocusing` becomes true and whether the interaction-target handle changes to the registered candidate. An ILO wrapper call returning without exception is not proof of visible prompt behavior.

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
- **Tested in game:** In an earlier target-getter test, `PLAYER.GET_PLAYER_TARGET_ENTITY` returned a positive handle corresponding to the NPC selected by right-clicking.
- **Tested in game:** A later attempt using `Game.Player.GetInteractionLockonTargetEntity()` displayed “GetInteractionLockonTargetEntity returned no target” after the user right-clicked an NPC and pressed G. No prompt or G-action result was observed; target detection did not reach the prompt test.
- **Tested in game, result incomplete:** A subsequent `Test.cs` attempt used `Game.Player.GetFreeAimEntity()`. The user reported “Still no result” but did not provide the exact subtitle from that run. Do not infer from this report alone whether the new DLL was loaded or what the accessor returned.
- **Correction for API-version notes:** v2.1.1.0 release notes say `RDR2.Native.Function` was made internal. The project bootstrap and v2.2.0 Visual Studio Object Browser report `RDR2.Native.Function` is public for the selected v2.2.0 API. Do not apply the v2.1.1.0 restriction to the v2.2.0 test project without checking the installed assembly.
- **Documented in ScriptHook release notes:** Use the ASI and API DLL files from the same release archive because internal structure may change.
- **Documented in ScriptHook release notes:** v2.1.0.1 noted that prompt text did not work in that release. The v2.2.0 release notes do not state whether this issue was resolved.
- **Open:** Confirm the API/runtime DLL pairing used in game and whether prompt-text rendering works in that pairing.
- **Open:** Confirm the exact v2.2.0 generated wrapper signature for `PLAYER.GET_PLAYER_TARGET_ENTITY` against the installed Object Browser/XML. The attempted call using `out` produced compiler error CS1615 (“Argument 2 may not be passed with the 'out' keyword”). Do not reuse that `out` call until the wrapper signature is confirmed.
- **Open:** Re-establish target detection in `Test.cs` using the previously successful `PLAYER.GET_PLAYER_TARGET_ENTITY` path before drawing conclusions about prompt registration, prompt visibility, or G-input detection.
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

### Interaction lock-on test — 2026-11-07

- **Tested in game:** Aiming a firearm selected candidate NPC handle `151810`.
- **Tested in game:** `REGISTER_INTERACTION_LOCKON_PROMPT` was invoked for that candidate and returned without an exception. This confirms only that the wrapper call completed; it does not confirm visible prompt registration.
- **Tested in game:** `Game.Player.IsFreeFocusing` changed to `True` while the user held right-mouse focus and returned to `False` after release. Right-mouse focus was active during the G-key test.
- **Tested in game:** During the G-key test, the candidate remained `151810` and `GetInteractionLockonTargetEntity()` returned no target. G was blocked; no recruitment occurred.
- **Open:** The run does not establish whether the ILO prompt was visible. Test the separate `PLAYER.GET_PLAYER_TARGET_ENTITY` path during right-mouse focus. Confirm its exact v2.2.0 wrapper signature in the installed API reference before implementing it; do not assume an `out` parameter.
- **Tested in game — 2026-11-07:** `PLAYER.GET_PLAYER_TARGET_ENTITY` resolved at runtime as `bool(int, int*)`. While `IsFreeFocusing=True`, it returned the same handle as the candidate captured from free aim in both test cycles (`178178` and `197890`); G accepted both matches.
- **Tested in game:** After right-mouse focus ended, `IsFreeFocusing=False` and the target native returned no target. `GetInteractionLockonTargetEntity()` remained `none` in the G snapshots.
- **Tested in game, result incomplete:** `REGISTER_INTERACTION_LOCKON_PROMPT` returned without an exception for both candidates. The log does not establish that a prompt was visible. G was diagnostic only; no recruitment occurred.
- **Next:** Test unarmed, record whether the prompt is visibly displayed, and check whether the target/helper behavior changes. Keep prompt visibility separate from target-native success.
- **Tested in game — 2026-11-07:** `IsFreeFocusing` became `True`, and `PLAYER.GET_PLAYER_TARGET_ENTITY` returned the focused human NPC handle. `REGISTER_INTERACTION_LOCKON_PROMPT` returned `True`.
- **Tested in game — 2026-11-07:** The temporary G action prompt was attached to the target’s prompt group. Its logged state changed from `Visible=False` to `Visible=True`, but the user confirmed that **“G Recruit” did not appear in game**. Prompt properties and a successful native return do not establish rendering.
- **Tested in game — 2026-11-07:** G was not pressed because the Recruit option was not visible. No recruitment or gang-data change occurred.
- **Documented in external research; not yet confirmed against the installed wrapper or in game:** Prompt groups may need to be activated every frame. Test per-frame activation of the target’s ambient prompt group before changing the prompt’s registration arguments.
- **Next:** Verify the exact v2.2.0 ambient-group native wrapper signature in the installed API reference. Retest unarmed with per-frame group activation; record the ILO text and G action prompt visibility separately. Keep the test action non-recruiting.
