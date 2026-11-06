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
- A UI prompt attached to the target's prompt group displays the action and handles its acceptance.
- The existing recruitment path remains the source of truth for recruiting the NPC.

### Findings from the current repository

- **Verified in repository:** The mod's entry class derives from `RDR2.Script` and subscribes to the Script `Tick` event. Do not implement `public override void Tick()`.
- **Verified in repository:** The target-selection path calls `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to `Game.Player.GetFreeAimEntity()`.
- **Verified in repository:** **G** is the current test recruitment key. The existing recruitment action should be reused rather than duplicated when the native UI prompt is added.
- **Verified in repository:** F9 toggles companion follow; it is not the recruit action.
- **Verified in repository:** The current custom interaction UI is temporary. Do not treat its current presentation as the intended native prompt.
- **Verified in repository:** The native right-mouse interaction-lock-on prompt is not yet implemented.
- **Correction:** An earlier version of this note described F7 as the current interaction/recruitment control. That is stale for the current G-key recruitment test and has been corrected here.

### ScriptHook API findings

- **Documented in ScriptHook release notes:** v2.1.0.0 added `Player.GetInteractionLockonTargetEntity()`, `Player.GetFreeAimEntity()`, and `Player.IsFreeFocusing`.
- **Implementation direction:** Use `Game.Player.GetInteractionLockonTargetEntity()` to read the player's actual focused lock-on target for this test. Keep free-aim selection separate; it is not proof that the player has locked onto the NPC.
- **Documented in ScriptHook release notes:** From v2.1.1.0 onward, `Function.Call()` is not a valid script-level native invocation. Use functions exposed by `RDR2.Native`.
- **Documented in ScriptHook release notes:** Use the ASI and API DLL files from the same release archive because internal structure may change.
- **Documented in ScriptHook release notes:** v2.1.0.1 noted that prompt text did not work in that release. The v2.2.0 release notes do not state whether this issue was resolved.
- **Open:** Confirm the API version referenced by the project and whether the prompt-text limitation affects that installed version.

### Native-research findings

- **Documented in external research:** `REGISTER_INTERACTION_LOCKON_PROMPT` is identified by hash `0x870708A6E147A9AD`.
- **Documented in local reference:** The project records its wrapper signature as `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(int, string, float, float, int, float, float, int, bool, int)`.
- **Documented in external research:** Its first argument is the entity; the second is approach/focus prompt text; the third is prompt radius; and the fourth is focus distance, commonly the same as the radius.
- **Documented in external research:** The fourth integer argument is an interaction-lock-on flag value. Reported meanings include `1` (changes prompt priority among nearby peds), `8` and `1024` (suppress the focus prompt while allowing focus), `64` (disable camera lock while allowing look-around), `256` (show prompts without looking directly at the target in third person), and `512` (hide the prompt icon border). Other values remain unknown for this prototype.
- **Documented in external research:** The fifth and sixth numeric arguments are unknown and commonly reported as `0.0`; the seventh is commonly reported as `0` and custom prompt behavior through it has not been confirmed; the final argument is commonly reported as `-1`.
- **Documented in external research:** The boolean argument controls whether the approach prompt appears. Focus may still be possible when it is false. This is distinct from accepting the G recruitment action.
- **Documented in external research:** A UI action prompt can be attached to the prompt group for the target entity. The target's group is obtained with `_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY` (hash `0xB796970BD125FCE8`); `_UI_PROMPT_SET_GROUP` is reported as hash `0x2F11D3A254169EA4`.
- **Documented in external research:** The reported unregister native is `0xE98D55C5983F2509`.
- **Open:** Match these external native names and signatures to the `RDR2.Native` wrappers in this project's referenced API before relying on them.
- **Open:** Test whether explicit ILO registration is needed for the desired ped approach/focus behavior; native research says peds can be focused by default.
- **Open:** Test registration cadence and cleanup. External research recommends registering/unregistering as needed rather than accumulating ILO registrations.

### Input-control findings

- **Documented in external research:** `INPUT_INTERACT_LOCKON` has hash `0xF8982F00` and default bindings Mouse Right on QWERTY keyboard and LT on Xbox.
- **Documented in external research:** `INPUT_INTERACT_OPTION1` has hash `0x760A9C6F` and default bindings G on QWERTY keyboard and D-pad Up on Xbox.
- **Documented in external research:** `INPUT_INTERACT_LOCKON_ANIMAL` has hash `0x5415BE48` and default bindings G on QWERTY keyboard and X on Xbox.
- **Implementation direction:** For a generic NPC recruitment action, test `INPUT_INTERACT_OPTION1` as the prompt control. Verify the actual label/glyph and avoid conflating a game control with a physical keyboard `KeyDown` event.
- **Open:** Confirm the prompt's displayed binding and behavior after player key remapping.

### Details still requiring in-game verification

- Whether right-mouse lock-on and the approach prompt appear as intended while the player is unarmed.
- Whether `Game.Player.GetInteractionLockonTargetEntity()` returns the intended NPC while the player focuses them.
- Whether the G action prompt appears only for eligible NPCs and displays its text correctly.
- Whether the prompt displays the intended G binding and correct glyph.
- How prompt acceptance is detected in the installed wrapper.
- Confirm the action runs once per acceptance, not repeatedly while a control is held.
- How registration and prompt cleanup behave when the target changes, dies, leaves range, flees, enters combat, or becomes invalid.
- Whether the prompt behaves correctly with mouse/keyboard and controller.
- Whether repeated registration or multiple nearby targets cause priority or cleanup problems.
- Whether default ped focus behavior makes explicit ILO registration unnecessary for this use case.

## Verification plan

1. Confirm the ScriptHookRDR2DotNet-V2 API version referenced by the project and use matching runtime/API files.
2. Check the installed wrapper/XML for the ILO registration, unregister, prompt-group, UI-prompt, prompt-control, and prompt-completion functions.
3. Implement an isolated prototype for one eligible NPC. Keep target selection, ILO registration, UI prompt state, action completion, and cleanup separate.
4. Test while unarmed: focus the NPC with right mouse and confirm the intended approach/focus behavior.
5. Confirm the lock-on target accessor returns the focused NPC, not just the free-aim entity.
6. Confirm the action prompt is attached to the focused target and displays the intended G test binding.
7. Accept the prompt and confirm recruitment runs once.
8. Change targets, leave range, invalidate the target, and enter combat; confirm the prompt is hidden/removed and cannot act on a stale target.
9. Repeat the test and verify prompt registration does not accumulate or create duplicates.
10. Test controller behavior if it is in scope.
11. Record the observed result in the test record below. Do not mark behavior **Tested in game** until it has actually been tested.

## Current decision

**Planned:** Prototype the native interaction before building the full conversation system.

Use the game lock-on target accessor for the focused NPC. Keep the existing G recruitment logic available as a test/fallback path until the native UI prompt and its action completion have been tested in game. Preserve F9 as the current follow toggle. Do not remove or repurpose controls based only on native-research findings.

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