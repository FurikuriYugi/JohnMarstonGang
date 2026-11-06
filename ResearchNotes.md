# Research Notes

## Purpose

Record what has been checked about the repository, ScriptHookRDR2DotNet-V2,
RDR2 natives, and in-game behavior. This file distinguishes verified facts
from hypotheses and test plans so uncertain API details do not become
assumed contracts.

Update this file when a native signature is checked, an in-game test is
performed, or a design decision changes. Keep the larger gameplay design
in `StoryBoard.md`.

## Status labels

- **Verified in repository:** Confirmed in current project files.
- **Documented in local reference:** Listed in a project note or installed
  API documentation, but not necessarily confirmed in game.
- **Hypothesis:** Plausible usage or interpretation; do not depend on it
  in production code.
- **Tested in game:** Confirmed in game; record the environment and steps.
- **Open:** Needs further investigation.

## Focus: native conversation and contextual interaction

### Goal

Use the base game's right-mouse lock-on interaction while unarmed for
conversation and recruitment, if the installed API can register an
appropriate prompt and reliably detect its acceptance. The prompt should
use the game's own label and input glyph rather than a separate custom
F7 overlay.

The native prompt is not the source of truth for conversation outcomes.
It should identify an eligible target and dispatch an action into the
mod's conversation/recruitment logic.

### Findings from the current repository

- **Verified in repository:** `Main.cs` subscribes to the Script `Tick`
  event and a `KeyDown` handler. Do not implement `public override void
  Tick()`.
- **Verified in repository:** The current target-selection path calls
  `PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY` and falls back to
  `Game.Player.GetFreeAimEntity()`.
- **Verified in repository:** F7 opens or executes the custom interaction
  overlay. That overlay currently offers recruitment for an eligible
  nearby person or animal, and follow/stay/dismiss actions for an existing
  companion.
- **Verified in repository:** F9 toggles companion follow; it is not the
  recruit action.
- **Verified in repository:** The overlay uses a `TextElement` and custom
  keyboard selection. This is useful as a temporary test/fallback path,
  not the intended final presentation.
- **Documented in local reference:** `NativeReferenceGuide.md` identifies
  `RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT` as a candidate
  prompt native.
- **Documented in local reference:** The guide reports that the installed
  XML describes p8 as determining whether the ILO prompt is a lock-on
  prompt with RMB, and that p3 is usually the same as the radius value.
- **Open:** The installed API XML and DLL cannot be verified from this
  note alone. The guide refers to a local XML path; inspect the actual
  game installation used for testing before treating that path or
  signature as current.

### Unverified details in the existing native guide

The existing guide explicitly marks these details as uncertain:

- The meaning and correct values of p4, p5, p6, p7, and p9.
- Whether the native must be called every frame or registered on a
  different lifecycle.
- How to detect acceptance of the prompt.
- Whether prompt rendering and glyph selection are automatic in this
  ScriptHook/API version.
- How the prompt should be removed or invalidated when the target changes,
  dies, leaves range, or enters combat.
- Whether multiple prompts can be registered safely and how they are
  prioritized.
- Whether the prompt behaves as intended for both mouse/keyboard and
  controller input.

The usage pattern in `NativeReferenceGuide.md`, including proposed
parameter values, is a hypothesis until checked against the installed
wrapper and tested in game. Do not copy it into production code as a
verified signature.

### Verification plan

1. Inspect the exact `ScriptHookRDRNetAPI.dll` and XML documentation
   installed for the game version used in testing.
2. Confirm the native's generated C# signature and parameter types in the
   installed API documentation or assembly.
3. Check the project's current references and version before adding or
   changing any native call.
4. Build an isolated prototype with one valid target and one harmless
   action label.
5. Test whether the game displays the prompt and correct input glyph while
   locked onto the target and unarmed.
6. Test how acceptance is detected. Confirm the action runs once—not
   repeatedly while the input remains held.
7. Test target changes, leaving range, target death, combat, fleeing,
   vehicles, invalid handles, and repeated registration.
8. Test mouse/keyboard and controller behavior if both are in scope.
9. Test prompt cleanup and confirm the prompt does not remain attached to
   a stale target.
10. If the native path is unreliable or unavailable, keep a clearly
    documented fallback interaction while preserving the intended
    base-game-style presentation goal.

### Current decision

**Planned:** Prototype the native interaction before building the full
conversation system.

Do not remove the temporary controls until the native path and at least
one contextual action are tested in game. Once the replacement is
confirmed, remove F7/F9 from ordinary player-facing use. Retain a
debug-only path only if it remains useful for development.

## Follow-up research topics

These are open research items, not confirmed capabilities:

- Native interaction prompt registration, input acceptance, and cleanup.
- Conversation, dialogue, subtitle, and menu presentation supported by
  this ScriptHook version.
- Whether any base-game scene or cutscene assets can be safely reused.
- Companion task and movement APIs for realistic travel to and return from
  camp.
- Body, horse, container, weapon pickup, and plant collection interactions.
- Persistent identity for lootable world entities and duplicate-loot
  prevention.
- Inventory transfer and item ownership limitations imposed by the game.
- Reliable marker or notification presentation for job discussions and
  pending payouts.
- Camp gathering and safe-scene detection.
- Faction and distant-NPC simulation constraints.

For each topic, record the exact file, native, API version, or in-game
test that supports a claim. Leave unsupported items marked **Open** or
**Hypothesis**.

## Test record template

Copy this block for each experiment:

### Test: [short name]

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
- **Conclusion:** Verified in repository / Documented in local reference /
  Hypothesis / Tested in game / Open
- **Follow-up:**

## Change log

- **2026-11-06:** Created this file to track API and in-game verification
  separately from design decisions. Recorded current `Main.cs` interaction
  behavior and the uncertainty already documented for the lock-on prompt.