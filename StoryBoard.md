# JohnMarstonGang — StoryBoard and Architecture Plan

## Purpose

This is the living design source for the story, gameplay loops, system boundaries, decisions, and implementation order of the JohnMarstonGang mod for Red Dead Redemption 2.

This document is a design plan, not proof that a feature works. A feature may be marked implemented when its code exists, but mark it tested only after confirming its behavior in game under recorded conditions.

Add new ideas here before or while building them so their prerequisites, consequences, persistence, and integration points stay visible.

## Project constraints

- **Project:** JohnMorstonGang
- **Game:** Red Dead Redemption 2
- **Environment:** Visual Studio, C#
- **Script API:** `ScriptHookRDRNetAPI.dll` from ScriptHookRDR2DotNet-V2
- The GitHub repository is the source of truth for current files, members, and system contracts.
- Before changing code, read this document and the current owning files.
- Match callers to the actual contract in the owning file.
- Use Newtonsoft.Json for JSON; do not use System.Text.Json.
- Do not assume an unverified game native, event, scene, cutscene, or asset is available.
- Do not treat a data model or system class as a game-integrated feature until the running mod calls it and its behavior has been checked.
- Keep this document and `ResearchNotes.md` updated as decisions and research results change.

## Status key

- **Unverified:** The repository or behavior needs inspection.
- **Planned:** The design is agreed; implementation is not confirmed.
- **In progress:** Implementation is underway.
- **Implemented:** Code is present in the repository.
- **Tested:** Behavior has been confirmed in game under documented conditions.

Add a note when code exists but has not been tested in game, for example: **Implemented; runtime behavior unverified.**

## Testing scope and guardrails

The active test is only to display and complete a temporary **G Recruit** action in the focused human NPC's prompt group.

- Keep `Test.cs` small and limited to the active test question.
- Do not implement recruitment, alter gang data, or expand into unrelated systems in this test.
- Record target detection, wrapper resolution, and setup errors only when they inform this test.
- Treat prompt properties and native invocation as diagnostic evidence, not proof of visible rendering.
- Change one prompt-display hypothesis per test run.
- Do not add fallback interaction systems until the current experiment is reported and reviewed.
- Update `ResearchNotes.md` with test conditions, observed result, and what the result does—and does not—prove.
- Keep API signatures in `NativeReferenceGuide.md` marked as unverified until confirmed against the installed v2.2.0 API/native reference.

## Design pillars

1. **The gang is a persistent group.** Members have identity, skills, preferences, relationships, memories, equipment, needs, and goals.
2. **The world responds to actions.** Jobs, businesses, factions, banks, transport, and local people react to events and relationships.
3. **Choices have consequences.** Honor, suspicion, trust, alliances, resources, and morale can change.
4. **Character decisions are explainable.** Decisions follow personality, needs, goals, orders, situation, and available information.
5. **Gameplay resolves safely.** Save/load and repeated events must not duplicate loot, payouts, jobs, or contradictory outcomes.
6. **Conversations present system outcomes.** Dialogue and scenes are not the source of truth for jobs, inventory, relationships, or money.
7. **Build vertical slices.** Complete and test one connected loop before expanding to many disconnected systems.
8. **Respect the base game.** Prefer verified native interactions and familiar presentation; provide a fallback if a native feature cannot be made reliable.

## Core gameplay loop

1. The player meets people, recruits companions, receives information, or discovers an opportunity.
2. The gang evaluates the opportunity using risk, honor, relationships, alliances, abilities, resources, and member preferences.
3. Members discuss and may vote; the player chooses whether to follow the recommendation, override it, postpone, or decline.
4. The gang prepares people, weapons, supplies, transport, and tactics.
5. The job or activity plays out in the world. Companions react to orders and changing circumstances.
6. Members may collect eligible loot and resources. Carried gang property is recorded and transferred to the gang stores when returned to camp.
7. Outcomes update jobs, money, inventory, honor, suspicion, relationships, memories, and world state.
8. At a suitable time and place, the gang may discuss results, settle pending job shares, or decide how to use shared resources.
9. The player’s choices shape future opportunities, relationships, and gang progression.

## System responsibilities

### Core AI and NPC simulation

Core AI coordinates decisions for companions and relevant NPCs. Decisions consider current orders, traits, needs, goals, memories, relationships, danger, resources, and world context.

Use local, bounded simulation for NPCs and factions. When relevant characters are present, use supported in-world behavior. For distant characters, update compact persistent state rather than requiring every NPC to run full AI continuously.

### Companions and social relationships

Track a companion’s identity, appearance, clothing, weapons, abilities, skills, experience, needs, memories, relationships, and personal goals. The design includes human and animal companions.

Distinguish:

- **Traits/personality:** relatively stable tendencies such as cautious, generous, ambitious, loyal, or quick-tempered.
- **Needs:** changing conditions such as food, rest, safety, medicine, or money.
- **Goals:** desired outcomes such as revenge, advancement, security, or helping a particular person.
- **Relationships and memories:** specific history between people that changes trust, fear, resentment, affection, and willingness to help.

Use those factors to explain reactions, not just produce an invisible score. Recruitment requires a conversation and a willing NPC; it is not an automatic result of selecting a recruit action. Refusal may lead to a later opportunity, lasting rejection, or hostility depending on the person and what happened.

### Interaction, conversation, and recruitment

The intended player-facing interaction is the base game’s right-mouse lock-on interaction while unarmed, if the project’s installed API and in-game testing confirm it can support the required prompt and action selection.

Use contextual actions appropriate to the target and state, such as:

- Talk or greet.
- Discuss an available job lead.
- Invite a person to join the gang.
- Follow, stay, or receive another command from an existing companion.
- Request an item or assistance where supported.

A conversation should account for who the NPC is, what they know, their relationship with the player and gang, and their current needs and goals. An interaction prompt is a presentation affordance; the conversation and social systems own the outcome.

The present custom F7/F9 controls are test controls, not the intended finished interaction design. According to the inspected `Main.cs`, F7 opens the current custom interaction overlay, while F9 toggles companion follow. Replace these as normal player controls only after the native interaction path is verified in game. Keep a debug fallback temporarily if required for testing.

### Job leads, discussions, and votes

A lead begins as information, not an accepted job. It may come from a gang member’s conversation, a local incident, a faction conflict, or another supported world event.

Persist an opportunity with a stable ID and enough state to explain it: source, location, involved people or factions, known facts, uncertainty, possible approaches, risks, rewards, required resources, consequences, and expiration or change conditions.

Suggested opportunity states:

- Rumored
- Reported
- Awaiting discussion
- Discussing
- Awaiting player decision
- Accepted
- Declined
- Expired
- Resolved

A member with a lead should try to approach the player and, when appropriate, the gang. If the player is busy or absent, keep the lead available rather than losing or silently accepting it. Use a marker or notification only after confirming the game API supports it. A clear interaction or subtitle fallback is acceptable.

Members may support, oppose, or abstain from a proposal. Their reasons come from relevant traits, needs, skills, memories, relationships, goals, alliances, and knowledge of the situation. Record reasons so dialogue and debugging can explain their views.

The player may:

- Hear the discussion and accept the gang’s recommendation.
- Override the gang and choose another available approach.
- Postpone the decision while the lead remains available.
- Decline the opportunity.

A vote informs the player; it does not remove player agency. Ignoring or postponing a lead must not silently accept or decline it.

A scene or conversation presents the decision but must not be the only record of whether a job was accepted. Persist the opportunity and job state independently of scene completion.

### Jobs and world events

The Job System owns job proposals, voting state, accepted jobs, objectives, outcomes, costs, and job consequences. Other systems own their own state: the bank owns accounts and vault state; the inventory owns goods; the relationship system owns relationships.

World events may create opportunities, but not every incident must become a job. For example, if a person is wronged by a gang or faction, they may ask for help, seek revenge, consider joining, reject the player, or do nothing. Their response depends on their own motives and the event’s context.

### Field salvage and gang stores

Companions may collect eligible loot and resources while travelling, fighting, or working. They may scavenge when circumstances are safe or follow an explicit player order to collect a requested item.

Potential sources include bodies, horses, containers, dropped weapons, plants, and other supported world objects. Do not assume that every source can be searched or looted by a script. Verify each interaction against the installed API and in-game behavior.

Scavenging decisions account for danger, distance, orders, carrying capacity, available time, and individual traits. Companions should not instantly collect everything nearby or repeatedly attempt an unavailable pickup.

Track carried items per member separately from gang storage. On returning to a valid camp, members deposit eligible gang property. Interrupted trips, death, capture, dismissal, or a missing companion need defined outcomes. Do not silently duplicate or erase a transfer.

Store inventory by meaningful category, such as money, valuables, weapons, food, ingredients, and supplies. Record deposits, withdrawals, consumption, sales, and other transfers. Use durable identities and outcomes rather than transient game entity handles.

The player may request an item or assistance from a member. Resolve the request against that member’s carried items, the chest’s stock, access rules, availability, and the member’s disposition. Tell the player whether the request was fulfilled, refused, unavailable, or delayed.

### Chest access, camp duties, and theft

The gang chest is shared property used for gang needs; it is not a universal, unrestricted personal inventory.

Potential roles include:

- **Quartermaster:** manages routine withdrawals and records their purpose and recipient.
- **Cook:** plans meals and draws suitable ingredients.
- **Hunter/gatherer:** collects requested food, plants, or supplies.
- **Fence-runner:** transports authorized valuables to a fence and records the goods sold, proceeds, and relevant risks.

Members may have multiple skills, rotate duties, or assist one another. Cooks may request that the player or another member hunt or gather. Role assignment and supply use should have understandable consequences.

The player and explicitly authorized senior members may access shared resources. Routine member behavior should respect gang property when loyalty and circumstances support it.

A member’s loyalty, needs, personality, goals, relationships, and grievances can lead to an attempt to steal from the chest. Record theft attempts as persistent events: person, item or amount, time, location, and possible witnesses. Detection depends on who is present and what they can perceive.

Possible responses include restitution, a hearing, confinement where supported, exile, or violence. Gang rules and the people involved should shape the outcome. A violent reaction is possible but must not be the automatic response to every theft. Avoid promising a holding area until the relevant location and game behavior are verified.

### Payout marker and fixed job shares

Separate **shared chest inventory** from **pending job payouts**.

Ordinary field loot and supplies go into shared stores by default and do not automatically trigger a payout scene. A payout marker means a pending job reward needs presentation or settlement; it does not mean only that the chest contains items.

A successful job creates a pending reward for an appropriate, safe gang gathering. The split is fixed and cannot be changed by the player:

1. Calculate net proceeds after recorded job costs.
2. Put exactly half of net proceeds in the participants’ pool and half in the gang’s shared funds.
3. Divide the participants’ pool equally among everyone who went on the job, including the player if they participated.
4. Put the gang’s half in the camp chest by default.
5. Use a bank destination only when story progression and bank state explicitly support it.
6. Define and validate eligible-participant behavior before accepting an outcome with no participants. Do not silently lose or reallocate the participants’ pool.

The player cannot change the 50/50 split, participant eligibility, or equal-share rule during the gathering. Valuable goods must be valued or sold through an explicitly defined system before they count as job proceeds; do not invent a value during the payout scene.

On job completion, persist a pending reward with job ID, net proceeds, participants and shares, gang-fund share, and intended destination. Present it only when a safe gathering is available—not during combat, active mission play, or another important scene. If skipped, unavailable, or interrupted, keep it pending. Settle it once only, including across save/load and repeated calls.

Keep reward creation, ledger persistence, gathering presentation, and settlement as separate responsibilities.

### Camp and base progression

A gang base is both a practical resource hub and a place for relationships and story events.

Conceptual stages:

- **Mobile camp:** rest, regrouping, basic supplies, and small discussions.
- **Established hideout:** more storage, job briefings, and earnings discussions.
- **Fortified base:** expanded defenses and advanced logistics.

These are design concepts, not confirmed map locations or available assets. Confirm feasible locations and facilities before implementing them.

Track base stage and location, facilities, upgrades, supplies, equipment, gang funds, assigned duties if supported, safety/threat state, and pending story events. Resources spent on one upgrade are not available for weapons, food, medicine, or transport unless separately earned.

### Factions and local simulation

Factions have relationships, territory or areas of influence, goals, rivals, and relevant event history. Simulate activity locally and persist bounded state for distant factions.

A conflict can create a local event that an NPC witnesses or reports. Possible consequences include a job, a request for help, a revenge opportunity, recruitment interest, hostility, or no player-facing event. Do not make every NPC recruitable or every conflict into a mission.

### Tactical combat

Choose behavior using orders, combat style, individual ability, team role, threat, terrain, visibility, ammunition, and mission goals.

Combat styles to support:

- Nonlethal melee, including attempts to restrain or tie up a target.
- Lethal melee.
- Short-range combat, including melee and handguns.
- Medium-range combat, including bows and repeaters.
- Long-range combat, including rifles.
- Stealth, which may use nonlethal melee, lethal melee, or bows.

Coordinate roles instead of having every companion independently choose the same action. Choices may include following, holding, taking cover, suppressing, flanking, retreating, picking a lock, breaching, or using supported transport or artillery.

### Locks and destruction

Support doors, safes, and other valid targets through appropriate interaction choices: use a breaker tool, force entry, or use dynamite when the target and situation allow it. Destruction is limited to supported world objects and must produce consistent world changes. Verify target support and consequences before implementation.

### Banking and economy

Track player, gang, and NPC accounts; deposits, withdrawals, debt, credit, business transfers, and bank vault contents. Businesses and people should move money and goods over time in ways that reflect their activity.

Vault contents arise from recorded economic state rather than a fixed reward. Bank access, suspicion, security, manager relationships, and deposits or withdrawals affect later opportunities. Do not treat vault access as a permanent consequence-free unlock.

### Trains and stagecoaches

Support protection or robbery opportunities with safes or strongboxes that can be opened through verified interactions. Potential rewards include cash, gold, valuables, or information leading to future jobs. Connect outcomes to the job ledger, suspicion, and world state.

### Wagons, animals, and artillery

Support acquiring, using, storing, and recovering wagons and their contents. Wagons and animals should persist appropriately through death, crashes, and reloads.

Artillery should have crew roles, movement and reload considerations, and supported towing or transport behavior. Do not claim a weapon, hitching, or persistence behavior until tested against the game API and in-game.

## Current repository observations

These observations are not a claim of in-game functionality:

- `Main.cs` contains a custom F7 interaction overlay. It uses the interaction target native and a free-aim target fallback.
- `Main.cs` currently maps F9 to toggling companion follow.
- The current overlay supports immediate recruitment for eligible nearby people or animals, and follow/stay/dismiss actions for existing companions.
- `NativeReferenceGuide.md` documents a possible lock-on prompt native, but labels several parameters and input-detection behavior as unknown or hypotheses.
- Job, companion, banking, and payout code exists in the repository. In-world conversations, opportunity presentation, automated looting, camp chest interactions, and the full job-to-gathering experience must be separately verified.
- Repository presence is not the same as in-game integration or testing.

## Suggested implementation order

### Milestone 0: Repository and runtime baseline

- Inspect current source, project-file inclusion, save flow, and system contracts before each implementation.
- Record what exists, what is a placeholder, and what is connected to the running script.
- Confirm the game/API version and collect in-game test conditions.
- **Status:** Repository review underway; runtime/API verification remains.

### Milestone 1: Native interaction research and prototype

- **Status:** In progress. `PLAYER.GET_PLAYER_TARGET_ENTITY` found focused NPCs in the tested setup. The ambient-group and explicit active-group prompt tests did not make **G Recruit** visible. The next test removes both activation calls and tests target-entity prompt-group assignment alone. No recruitment or gang-data changes are part of the test. See `ResearchNotes.md` for evidence and `NativeReferenceGuide.md` for API status.

### Milestone 2: One conversation and recruitment flow

- Replace immediate recruitment with a conversation and invitation.
- Use one NPC archetype and a small number of personality/relationship outcomes.
- Support acceptance, refusal, interruption, and a clear fallback if a native interaction cannot run.
- Persist relationship changes and recruitment outcome once only.
- **Status:** Planned.

### Milestone 3: Reported lead, discussion, and player decision

- Persist one gang member’s job lead.
- Present it to the player without losing it when the player is busy.
- Add a short discussion with explainable member recommendations.
- Support accept, override, postpone, and decline.
- Connect exactly one accepted choice to the current job model.
- **Status:** Planned.

### Milestone 4: One field-loot-to-camp transfer

- Verify one supported loot source and one item type.
- Let one companion collect it and track it as carried property.
- Return to a valid camp and deposit it into persisted gang storage once.
- Test interruptions, invalid targets, save/load, and repeated ticks.
- **Status:** Planned.

### Milestone 5: One playable job and recorded outcome

- Connect one accepted job to an in-world objective.
- Record costs, participants, outcome, honor, suspicion, and eligible proceeds.
- Keep mission completion distinct from reward presentation.
- **Status:** Planned.

### Milestone 6: Safe gathering and payout presentation

- Create a pending reward on successful job completion.
- Present it at a safe gathering and show the fixed shares.
- Settle exactly once; preserve pending state if skipped or interrupted.
- Update relevant member reactions and memories.
- **Status:** Reward-related code exists in the repository; in-game gathering and settlement behavior remain to be verified.

### Milestone 7: Chest permissions, camp duties, and expanded salvage

- Add categorized chest inventory and withdrawal permissions.
- Add quartermaster, cooking, hunting/gathering, and fence-running as supported vertical slices.
- Expand tested field salvage sources without duplicating world loot.
- Add theft attempts and deliberate, persistent responses.
- **Status:** Planned.

### Milestone 8: Expand world opportunities and other systems

- Add faction incidents, local jobs, bank arcs, trains, stagecoaches, wagons, artillery, and broader tactical or destruction behavior in integrated slices.
- Verify APIs and persistence for each before marking them implemented or tested.
- **Status:** Planned.

## Acceptance checklist

Before marking any feature implemented or tested, confirm:

- It is connected to the running mod’s actual entry points.
- The code matches current repository contracts.
- Success, failure, cancellation, invalid targets, and interruption are defined.
- Save/load preserves intended state.
- Repeating the action does not duplicate money, loot, jobs, or progression.
- Related systems receive the correct outcome.
- The player receives understandable feedback.
- In-game testing conditions and limitations are recorded.
- This document and `ResearchNotes.md` reflect the current status.

## Decision log

- **2026-11-05:** Job payouts are pending in-world gang events at an appropriate base, rather than automatic account deposits. Settlement must occur once only.
- **2026-11-05:** Bank-manager influence and vault access belong to a Job System progression arc; the bank system owns accounts, access, security, and vault state.
- **2026-11-05:** Reuse of base-game story events or cutscenes must be verified against the repository and supported API before implementation.
- **2026-11-06:** Ordinary scavenged loot belongs in shared gang stores by default; it does not itself trigger job-share payout. Job proceeds retain the fixed 50/50 split and equal participant shares.
- **2026-11-06:** The intended conversation/recruitment interaction is the base game's right-mouse lock-on prompt if verified in the installed API and in game. The custom F7/F9 controls are transitional/debug controls, not the intended finished interaction design.
- **2026-11-07:** `PLAYER.GET_PLAYER_TARGET_ENTITY` resolved at runtime as `bool(int, int*)`. In two test cycles, while `IsFreeFocusing=True`, it returned the same handle as the NPC captured from free aim (`178178` and `197890`). After focus ended, the native reported no target. This verifies the target-getter path for the tested setup; it does not establish that the ILO prompt was visible.
- **2026-11-07:** The ILO wrapper call returned without an exception for both captured NPCs. `GetInteractionLockonTargetEntity()` remained `none` in the G snapshots. G was diagnostic only; no recruitment occurred. Prompt visibility and unarmed behavior remain unverified.
- **2026-11-07:** The user confirmed the G Recruit option did not appear. The harness logged successful target matching, ILO registration, target-group attachment, and `Visible=True`, but those states do not prove that the prompt rendered. G was not pressed; no recruitment or gang-data change occurred.
- **2026-11-07:** The ambient-group activation trial did not display G Recruit.
- **2026-11-07:** The explicit active-group activation trial also did not display G Recruit. This rejects that display hypothesis for the tested setup; it does not establish why the prompt failed to render.
- **Next test:** Create the same test-only prompt, attach it to the focused NPC’s target prompt group, and do not call ambient- or active-group activation natives. Record whether the option appears and whether pressing G produces the test-only completion log. No recruitment or gang-data changes are in scope.
