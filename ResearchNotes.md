# Research Notes

## Current mode

The project is in test mode. Do not edit `StoryBoard.md` during this phase. Keep test findings in this file and native/API findings in `NativeReferenceGuide.md`.

## Recruitment prompt test

The current test has one goal: add a G-controlled “Recruit” prompt to the focused human NPC’s existing interaction menu, alongside the game’s existing options such as Greet and Rob.

The test must not recruit the NPC or change gang membership. Pressing G is logged only.

## Relevant prompt path

The focused target is obtained with:

```text
RDR2.Native.PLAYER.GET\_PLAYER\_INTERACTION\_TARGET\_ENTITY
```