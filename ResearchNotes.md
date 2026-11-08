# Research Notes

## Current mode

The project is in test mode. Do not edit `StoryBoard.md` during this phase. Record test findings in this file and native/API findings in `NativeReferenceGuide.md`.

## Current test objective

Test only these steps:

1. Get the focused interaction target.
2. Determine whether it is a human NPC.
3. Add a G-controlled “Recruit” prompt to that NPC’s existing prompt group.
4. Log whether the prompt becomes active and whether it is pressed.

The test must not recruit the NPC or change gang membership.

## Evidence collected

### Test log

The recorded `Test_Log.txt` shows:

- The UI prompt was created with handle `207362`.
- Each recorded focused-target lookup returned `nativeFoundTarget=False` and `focusedTarget=none`.
- No target group was retrieved or assigned.
- No target-specific prompt display was verified.

These log entries establish that prompt creation ran, but they do not establish that target detection or adding the prompt to an NPC’s menu works.

### In-game visual report

The user reported that right-clicking an NPC caused the game to crash with a Script Hook RDR2 CORE exception, id 33. This is a reported observation. The available log does not establish which native or operation caused the crash.

### API signature

The user checked the Object Browser and provided this declaration:

```csharp
public unsafe static bool GET_PLAYER_INTERACTION_TARGET_ENTITY(
    int player,
    int* outEntity,
    bool p2,
    bool p3)
```