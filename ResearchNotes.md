# Research Notes

## Purpose
Record what has been checked about the repository, ScriptHookRDR2DotNet-V2, RDR2 natives, and in-game behavior. Distinguish verified facts from hypotheses and test plans so uncertain API details do not become assumed contracts.

Update this file when a native signature is checked, an in-game test is performed, or a design decision changes. Keep the larger gameplay design in `StoryBoard.md`.

## Status labels
- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note or installed API documentation, but not necessarily confirmed in game.
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
- **Verified in repository:** The native right-mouse interaction-lock-on recruitment prompt is not yet implemented.
- **Correction:** An earlier version of this note described F7 as the current interaction/recruitment control. That is stale for the current G-key recruitment test and has been corrected.

### ScriptHook API findings
- **Documented in ScriptHook release notes:** v2.1.0.0 added `Player.GetInteractionLockonTargetEntity()`, `Player.GetFreeAimEntity()`, and `Player.IsFreeFocusing`.
- **Implementation direction:** Use `Game.Player.GetInteractionLockonTargetEntity()` to read the player's actual focused lock-on target for this test. Do not use free-aim as a fallback for this recruitment-prompt test.
- **Documented in ScriptHook release notes:** From v2.1.1.0 onward, `Function.Call()` is not a valid script-level native invocation. Use functions exposed by `RDR2.Native`.
- **Documented in ScriptHook release notes:** Use the ASI and API DLL files from the same release archive because internal structure may change.
- **Documented in ScriptHook release notes:** v2.1.0.1 noted that prompt text did not work in that release. The v2.2.0 release notes do not state whether this issue was resolved.
- **Open:** Confirm the API version referenced by the project and whether the prompt-text limitation affects that installed version.

### Native-research findings
- **Documented in external research:** `REGISTER_INTERACTION_LOCKON_PROMPT` is identified by hash `0x870708A6E147A9AD`.
- **Documented in local reference:** The project records its wrapper signature as:
  `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(int, string, float, float, int, float, float, int, bool, int)`.
- **Documented in external research:** Its arguments are reported as entity, approach/focus prompt text, prompt radius, focus distance, flags, two unknown numeric values, prompt handle, whether the approach prompt is shown, and a final unknown value.
- **Documented in external research:** The fourth integer argument is an interaction-lock-on flag value. Reported meanings include `1` (changes prompt priority among nearby peds), `8` and `1024` (suppress the focus prompt while allowing focus), `64` (disable camera lock while allowing look-around), `256` (show prompts without looking directly at the target in third person), and `512` (hide the prompt icon border). Other values remain unknown for this prototype.
- **Documented in external research:** The unknown numeric values are commonly reported as `0.0`, the prompt handle as `0`, and the final value as `-1`. Treat these as test values, not confirmed meanings.
- **Documented in external research:** The boolean argument controls whether the approach prompt appears. This is distinct from accepting the G recruitment action.
- **Documented in external research:** Peds can be focused by default; explicit ILO registration may not be necessary for the basic NPC lock-on test.
- **Documented in external research:** `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` has hash `0xB796970BD125FCE8`. It returns the prompt group associated with the target entity.
- **Documented in external research:** `_UI_PROMPT_SET_GROUP` has hash `0x2F11D3A254169EA4`. It associates a prompt with a group; its third argument selects the prompt's tab. The reported test call uses tab index `0`.
- **Documented in external research:** `PROMPT_REMOVE_GROUP` removes a prompt from a group. Use it when switching away from the previous target or cleaning up the prompt.
- **Documented in external research:** The reported ILO unregister native hash is `0xE98D55C5983F2509`.
- **Open:** Confirm the generated `RDR2.Native` wrapper names and exact C# signatures for group lookup, setting, removal, and ILO unregistration against the project's v2.2.0 API.
- **Open:** Test whether explicit ILO registration is needed for the desired ped approach/focus behavior.
- **Open:** Test registration cadence and cleanup. Do not repeatedly register a target or leave stale registrations behind.

### UI prompt and group findings
- (Developer comment: This is incorrect. Prompt is internal.)**Documented in local reference:** The installed prompt API reference documents `Prompt.CreatePrompt(...)` and prompt completion states. Use the API prompt instead of polling physical keyboard `KeyDown` for G.
- **Implementation direction:** Create the G action prompt through the prompt API, obtain the group for the lock-on target, and associate the action prompt with that group. When the prompt reports completion, call the existing recruitment method once.
- **Documented in external research:** The native group operations are described as `PromptGetGroupIdForTargetEntity(entity)`, `PromptSetGroup(prompt, group, tabIndex)`, and `PromptRemoveGroup(prompt, group)`. The test tab index is `0`.
- **Open:** Confirm the exact v2.2.0 C# wrapper/class method spellings and parameter types before relying on them.
- **Open:** Confirm whether an entity-associated prompt group requires any additional per-frame activation in this API. Do not substitute an active or ambient group unless testing shows it is required.
- **Important separation:** `REGISTER_INTERACTION_LOCKON_PROMPT` registers the target's approach/focus interaction. It is not the G recruitment action prompt. Do not treat its prompt-handle argument as the recruitment prompt.
- **Cleanup direction:** Remove the action prompt from the old target's group and disable or delete it when the target changes, becomes invalid, is no longer eligible, or leaves the relevant interaction state.

### Input-control findings
- **Documented in external research:** `INPUT_INTERACT_LOCKON` has hash `0xF8982F00` and default bindings Mouse Right on QWERTY keyboard and LT on Xbox.
- **Documented in external research:** `INPUT_INTERACT_OPTION1` has hash `0x760A9C6F` and default bindings G on QWERTY keyboard and D-pad Up on Xbox.
- **Documented in external research:** `INPUT_INTERACT_LOCKON_ANIMAL` has hash `0x5415BE48` and default bindings G on QWERTY keyboard and X on Xbox.
- **Implementation direction:** For a generic NPC recruitment action, test `INPUT_INTERACT_OPTION1` as the prompt control. Verify the actual label/glyph and avoid conflating a game control with a physical keyboard `KeyDown` event.
- **Open:** Confirm the prompt's displayed binding and behavior after player key remapping.

### Details still requiring in-game verification
- Whether right-mouse lock-on and the approach prompt appear as intended while the player is unarmed.
- Whether `Game.Player.GetInteractionLockonTargetEntity()` returns the intended NPC while the player focuses them.
- Whether explicit ILO registration is needed for NPCs in this test.
- Whether the G action prompt appears only for eligible NPCs and displays its text correctly.
- Whether the prompt displays the intended G binding and correct glyph.
- How prompt acceptance is detected in the installed wrapper.
- Confirm the action runs once per acceptance, not repeatedly while a control is held.
- How prompt-group removal and ILO cleanup behave when the target changes, dies, leaves range, flees, enters combat, or becomes invalid.
- Whether the prompt behaves correctly with mouse/keyboard and controller.
- Whether repeated registration or multiple nearby targets cause priority or cleanup problems.

## Verification plan
1. Confirm the ScriptHookRDR2DotNet-V2 API version referenced by the project and use matching runtime/API files.
2. Check the installed wrapper/XML for the ILO registration, unregister, prompt-group, UI-prompt, prompt-control, and prompt-completion functions.
3. Implement an isolated prototype for one eligible NPC. Keep target selection, ILO registration, UI prompt state, action completion, and cleanup separate.
4. Test while unarmed: focus the NPC with right mouse and confirm the intended approach/focus behavior.
5. Confirm the lock-on target accessor returns the focused NPC, not the free-aim entity.
6. Confirm the action prompt is attached to the focused target and displays the intended G test binding.
7. Accept the prompt and confirm recruitment runs once.
8. Change targets, leave range, invalidate the target, and enter combat; confirm the prompt is hidden/removed and cannot act on a stale target.
9. Repeat the test and verify prompt registration does not accumulate or create duplicates.
10. Test controller behavior if it is in scope.
11. Record the observed result in the test record below. Do not mark behavior **Tested in game** until it has actually been tested.

## Current decision
**Planned:** Prototype the native interaction before building the full conversation system.

Use the game lock-on target accessor for the focused NPC. Attach the separate G action prompt to that target's prompt group and dispatch the existing recruitment logic once on prompt completion. Keep F9 as the follow toggle. Do not use a free-aim fallback for this test. Do not remove or repurpose controls based only on native-research findings. No in-game results have been reported yet.

## Follow-up research topics
These remain open research items, not confirmed capabilities:
- Native interaction prompt registration, input acceptance, and cleanup.
- Conversation, dialogue, subtitle, and menu presentation supported by this ScriptHook version.
- Whether any base-game scene or cutscene assets can be safely reused.
- Companion task and movement APIs for realistic travel to and return from camp.
- Body, horse, container, weapon pickup, and plant collection interactions.
- Persistent identity for lootable world entities and duplicate-loot prevention.
- Inventory transfer and item ownership limitations imposed by the game.
- Reliable marker or notification presentation for job discussions and pending payouts.
- Camp gathering and safe-scene detection.
- Faction and distant-NPC simulation constraints.

For each topic, record the exact file, native, API version, or in-game test that supports a claim. Leave unsupported items marked **Open** or **Hypothesis**.

## Test record template
Copy this block for each experiment:
- **Date:**
- **Game version/build:**
- **ScriptHookRDR2DotNet-V2 version:**
- **API DLL/XML version or path:**
- **Repository commit/branch:**
- **Target and game state:**
- **Steps:**
- **Expected result:**
- **Observed result:**
- **Mouse/keyboard or controller:**
- **Failure, cleanup, and repeat behavior:**
- **Conclusion:** Verified in repository / Documented in local reference / Documented in external research / Hypothesis / Tested in game / Open
- **Follow-up:**

## Change log
- **2026-11-06:** Added the lock-on target accessor and documented default right-mouse/G control mappings. Recorded the separate roles of ILO registration and the UI action prompt, plus the unresolved prompt-text limitation. No in-game test has yet been performed.
- **2026-11-06:** Added prompt-group lookup, association, tab-index, and removal findings. Clarified that these group natives manage the separate G action prompt, while ILO registration handles approach/focus. Exact project wrapper signatures and all in-game behavior remain unverified.