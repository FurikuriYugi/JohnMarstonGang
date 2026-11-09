# JohnMarstonGang — Project Bootstrap

## 1. Mandatory Working Procedure

Before every task or code/file update:

1. Re-read this bootstrap; do not rely only on remembered chat context.
2. Re-open the Source of Truth repository and inspect the files relevant to the task. For testing work, prioritize the current test code and logs.
3. Treat the current repository contents as authoritative over earlier chat messages. Test code and logs may be newer than documentation.
4. Do not claim to have read a file or accessed the repository unless you actually could. If a required source is unavailable, report what you could not access and ask me to provide it. Do not reconstruct it from memory.
5. Follow the project mode and file-specific rules below.
6. Treat conclusions from earlier assistant messages as hypotheses unless confirmed by the repository or test results.

If the repository cannot be accessed, wait a moment and try again, if not, try another available route or inspect the relevant project files if I provide them. Do not invent repository contents.

## 2. Project and Environment

- Project: JohnMorstonGang
- Game: Red Dead Redemption 2
- Source of Truth repository:
  - https://github.com/FurikuriYugi/JohnMarstonGang
- Environment: Visual Studio, C#
- API: `ScriptHookRDRNetAPI.dll`, version 2.2.0
  - Source: https://github.com/Halen84/ScriptHookRDR2DotNet-V2
  - API mapping is available in the repository’s `Context/` folder.
- Note about github repos. for folder view add tree/main to the address and then the folder and for files use blob/main (Example: I can view the base directory https://github.com/FurikuriYugi/JohnMarstonGang but when I try to look at Context it shows 404 or something so you use https://github.com/FurikuriYugi/JohnMarstonGang/tree/main/Context and then you can view the contents of that folder and then for a file in there called main you can search the folder now and see that there is Main-001.md etc so now you can use https://github.com/FurikuriYugi/JohnMarstonGang/blob/main/Main-001.md etc etc and it works.)

## 3. Modes and Documentation Rules

### Testing mode
- Focus on the current test files, test logs, and relevant API/native references.
- Do not change `StoryBoard.md` and don't look at any other files than the test files and documents.
- Record new findings in `ResearchNotes.md`.
- Logs and `Test.cs` will usually be ahead of the documentation; check them first.

### Development mode
- Consult and maintain `StoryBoard.md` while designing or building features.
- Also update `ResearchNotes.md` if we learn anything new.

## 4. Code and File Output Rules

- When I ask you to update, give, or fix code/files, provide the result in chat so I can update the repository.
- Before providing code, inspect the relevant current files in the repository.
- Provide complete updated files, not partial code or pseudo-code.
- Exclude namespace declarations when giving code because files are handled individually.
- Do not use `System.Text.Json`; use Newtonsoft.Json instead.
- Treat each owning file as the source of truth for its members and elements. Make requesting files match that contract exactly unless I explicitly say otherwise.
- `Script.Tick` is an event. Do not write `public override void Tick()`.

## 5. Project Vision

Create a Red Dead Redemption 2 mod that lets the player recruit NPCs and build a gang of useful, intelligent human and animal companions. Companions should act appropriately to the player’s situation, their own needs and goals, their skills, their relationship with the player, and the gang’s resources.

The mod’s major systems are:

- Core AI Intelligence System
- Companion System
- Tactical Combat System
- Lock Picking System
- Dynamite Destruction System
- Banking System
- Train System
- Stage Coach System
- Job System
- Wagon System
- Weapon System

### Core AI Intelligence System
Determines NPC and companion behavior so they can make context-sensitive decisions and act intelligently.

### Companion System
Tracks each companion’s characteristics, skills, appearance, clothing, weapons, memories, personal goals, experience, and abilities. Includes human and animal companions.

### Tactical Combat System
Chooses behavior based on commands, tactics, situations, individual traits, and goals. Tactical decisions can include fighting, stealth, picking a lock, kicking down a door, or using dynamite.

Combat styles to support:
- Non-lethal melee: fists or rope, including attempts to tie up a target.
- Lethal melee: fists, knives, and similar weapons.
- Short-range: melee and handguns.
- Medium-range: bows and repeaters.
- Long-range: rifles.
- Stealth: a combination of non-lethal melee, lethal melee, and bow use.

Companions should coordinate and adapt their weapon choices and behavior to the situation, including stealth.

### Lock Picking System
Allow doors, safes, and similar targets to be picked when the player or a companion has the breaker tool.

### Dynamite Destruction System
Allow dynamite to open safes and doors and damage or destroy walls and other objects.

### Banking System
Support player, companion, and NPC finances, including accounts, credit, deposits, withdrawals, and vault contents. Robbable bank reserves should reflect money deposited and withdrawn over time. NPCs such as bankers and shop owners should handle business and personal funds consistently. Banking with a local institution should affect how suspicious people are of the player.

### Train System
Allow train safes to be picked, cracked, or blasted. Train loot may include money, gold, jewels, and papers that lead to jobs or other opportunities.

### Stage Coach System
Allow stagecoach strongboxes to be picked, cracked, or blasted. Their loot may include money, gold, jewels, and papers that lead to jobs or other opportunities.

### Wagon System
Allow wagons to be purchased or stolen, hitched to horses, and used to store hunting or trading goods and equipment such as artillery. Wagon ownership and contents should persist safely through death, crashes, and reloads. Owned wagons should return near the player, similar to the player’s horse.

### Weapon System and Artillery
Make artillery usable and portable, whether purchased or taken. Artillery should be movable solo, slowly—especially when rotating—or moved faster with a gunner. A gunner should reduce reload cooldown. Support hitching artillery to horses or wagons, and include a gunner and gunner’s assistant.

### Job System
Provide gang jobs that the player and companions can vote on based on relationships and alliances. Companions should receive a share of earnings and make sensible choices about spending or investing in gang supplies and weapons.

NPCs should also offer jobs based on their knowledge of the player and gang, the player’s reputation, and the NPCs’ interests. Jobs may be lawful or criminal—for example, protecting a bank coach carrying excess money and gold to Saint Denis or robbing it. The world should have ongoing opportunities to help people or commit crimes, with appropriate income and honor consequences.

## 6. Current Testing Focus

We are testing how to add **G Recruit** to the unarmed lock-on menu. This is a prompt-display test only; G must not recruit the NPC or change gang membership.

### Current research direction

Ghidra analysis of Dog Companion found that its B Bond prompt uses a standard prompt built with `VAR_STRING`, assigned to the target entity’s prompt group, then shown and enabled. The new test should mirror that path and record its visual result. See `ResearchNotes.md`.

### Latest user-verified result

G Recruit didn't show in the unarmed lock on menu.

### Interpretation and next-step caution
The observed **Recruit** outer-radius label is not proof that the G action prompt appears in the focused lock-on menu. Inspect the latest `Test.cs` and logs before changing anything; they may already contain additional diagnostics or newer results. Keep test changes focused on isolating the prompt-display problem, and do not implement actual recruitment as part of this prompt test unless explicitly requested.

A proposed test of passing the registered G prompt handle as the ILO registration prompt argument, instead of `0`, is only a hypothesis until verified against the current source and tested in-game. Do not record it as a confirmed native behavior before testing.

# Mode: Testing mode
## 7. Current Task
Keep using our database and research to find a way to add custom string to unarmed lock on menu and then building new test files updating the documents and testing. 