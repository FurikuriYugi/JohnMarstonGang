# JohnMarstonGang — Storyboard and Architecture Plan

## Purpose

This document is the living design source for the story, gameplay loops, and system boundaries of the JohnMarstonGang mod for Red Dead Redemption 2. Add new ideas here before or while building them so their prerequisites, consequences, persistence, and integration points stay visible.

This is a design plan, not a claim that any listed feature is implemented. Update each feature’s status only after checking the repository and testing the behavior.

## Project constraints

- **Project:** JohnMorstonGang
- **Game:** Red Dead Redemption 2
- **Environment:** Visual Studio, C#
- **Script API:** ScriptHookRDRNetAPI.dll from ScriptHookRDR2DotNet-V2
- Use the repository as the source of truth for existing files, class members, and system contracts.
- Keep code aligned with the owning file’s actual contract.
- Use Newtonsoft.Json for JSON; do not use System.Text.Json.
- Do not assume an unverified game native, event, scene, or cutscene API exists.
- Keep this document synchronized with implemented behavior and known limitations.

## Status key

- **Unverified:** Check the repository and test before describing current behavior.
- **Planned:** Design agreed; implementation not yet confirmed.
- **In progress:** Code is being built.
- **Implemented:** Present in the repository.
- **Tested:** Confirmed in-game under the documented conditions.

## Design pillars

1. **The gang is a persistent group.** Members have identities, skills, preferences, relationships, memories, equipment, and goals.
2. **The world responds to actions.** Jobs, banks, businesses, coaches, trains, and local people should react to prior events and relationships.
3. **Choices have consequences.** Honor, suspicion, trust, alliances, resources, and gang morale can change.
4. **AI decisions must be understandable.** Each choice should follow the member’s traits, orders, situation, and available information.
5. **Gameplay resolves safely.** Saved state must not grant duplicate payouts, duplicate loot, or contradictory mission outcomes.
6. **Story moments serve systems.** Scenes and conversations should present meaningful outcomes, not replace the underlying gameplay rules.
7. **Build in vertical slices.** Complete and test one connected gameplay loop before adding a broad set of disconnected systems.

## Core gameplay loop

1. The player recruits companions, meets people, discovers opportunities, or receives a job.
2. The gang evaluates the opportunity based on honor, relationships, risk, resources, alliances, and member preferences.
3. The player and gang choose an approach and prepare people, weapons, supplies, wagons, and tactics.
4. The job plays out in the world, with AI decisions reacting to orders and changing circumstances.
5. The outcome updates money, goods, honor, suspicion, relationships, memories, and world state.
6. At an appropriate time and place, the gang discusses the result, receives shares, and considers investing in future needs.
7. The player’s choices affect future opportunities and the gang’s progression.

## System map

These are conceptual responsibilities. Before implementation, confirm how the current repository names and organizes each system.

### Core AI intelligence

Coordinates decisions for companions and relevant NPCs. Decisions should account for current orders, personal traits, memories, relationships, goals, danger, resources, and world context.

### Companion system

Tracks companion identity, appearance, clothing, weapons, abilities, skills, experience, needs, memories, relationships, and personal goals. The design includes human and animal companions.

### Tactical combat system

Chooses behavior using orders, combat style, individual ability, team role, threat, terrain, visibility, ammunition, and mission goals.

Combat styles to support:

- Nonlethal melee, including attempts to restrain or tie up a target.
- Lethal melee.
- Short-range combat, including melee and handguns.
- Medium-range combat, including bows and repeaters.
- Long-range combat, including rifles.
- Stealth, which can use nonlethal melee, lethal melee, or bows.

The AI should coordinate roles rather than have every companion independently choose the same action. Tactical choices may include following, holding position, taking cover, suppressing, flanking, retreating, picking a lock, breaching, or using a wagon or artillery piece when appropriate.

### Interaction, locks, and destruction

Supports doors, safes, and other valid targets through appropriate interaction choices: use a breaker tool, force entry, or use dynamite when the target and situation allow it. Destruction must be constrained to supported world objects and should produce consistent changes in the world.

### Banking and world economy

Tracks player, gang, and NPC accounts; deposits, withdrawals, debt, credit, business transfers, and bank vault contents. Businesses and people should move money and goods over time in ways that reflect their activity. Vault contents should arise from the recorded economic state rather than a fixed reward.

### Jobs and opportunities

Supports both gang-planned jobs and opportunities that arise from the world. Job outcomes should account for honor, relationships, alliances, resources, and approach. Gang members can recommend, debate, and vote on jobs.

### Trains and stagecoaches

Supports protection or robbery opportunities, with safes and strongboxes that can be picked, blasted, or otherwise opened through supported interactions. Potential rewards may include cash, gold, valuables, or information that leads to future jobs.

### Wagons, animals, and artillery

Supports acquiring, using, storing, and recovering wagons and their contents. Wagons and animals should persist appropriately through death, crashes, and reloads. Artillery should have crew roles, movement and reload considerations, and supported towing or transport behavior.

## Gang base progression

A gang base is both a practical resource hub and a place for relationships and story events to develop.

### Base stages

These are progression concepts, not assumed map locations:

- **Mobile camp:** Basic rest, regrouping, simple supplies, and small gang discussions.
- **Established hideout:** More storage and planning capacity; regular job briefings and earnings discussions.
- **Fortified base:** Expanded defenses and facilities; larger planning gatherings and advanced gang logistics.

Define specific locations, upgrades, and gameplay gates only after confirming what is feasible in the repository and game API.

### Base progression should track

- Current base stage and location.
- Unlocked facilities and their effects.
- Stored supplies, equipment, and gang funds.
- Relevant upgrades and their costs.
- Members assigned to duties, if that system is supported.
- Whether the base is safe, threatened, discovered, or unavailable.
- Pending story events that should wait until the player can attend.

Progression should offer meaningful choices. For example, resources spent on fortifications may not also be available for weapons, food, medical supplies, or a wagon.

## Story event: distributing job shares at the base

A successful job creates a pending reward for a safe, in-world gang gathering. The split is fixed and cannot be changed by the player.

### Fixed payout rules

1. Calculate net proceeds after job costs.
2. Put exactly half of the net proceeds in the participants’ pool and half in the gang’s shared funds.
3. Divide the participants’ pool equally among everyone who went on the job, including the player if they participated.
4. Put the gang’s half in the camp chest by default. A bank deposit is available only when story progression and the relevant bank state support it.
5. If there are no participants, do not create a successful payout plan that silently loses or reallocates the participants’ half; define and validate the eligible-participant rule before accepting the job outcome.

The player cannot change the 50/50 split, participant eligibility, or equal-share rule during the gathering.

### Pending reward and gathering flow

1. On successful job completion, create and persist a pending reward record containing the job ID, net proceeds, participant IDs and equal shares, the gang-fund share, and its intended destination.
2. Keep that reward pending until a safe gathering is available. Do not require a gathering immediately after the job.
3. Present the pending reward at a safe post-job gathering. Do not trigger it during combat, active mission play, or another important scene.
4. Settle the ledger once. If the gathering is skipped, unavailable, or interrupted, preserve the pending reward for a later gathering.
5. Do not pay a second time if the gathering, save/load, or settlement call is repeated.
6. A bank destination may replace the camp chest only when the relevant story progression permits it; the gathering does not itself unlock that option.

### Scene requirements

- Prefer a verified, supported cutscene only if its availability and use in this mod context are confirmed.
- Otherwise, present the result through an in-game conversation or interaction supported by the project’s existing APIs.
- Keep reward creation, persistence, gathering presentation, and settlement as distinct responsibilities.
- An interrupted or skipped gathering must not erase a pending reward or cause duplicate settlement.


## Job System: gang votes and bank opportunity arc

The Job System owns proposals, voting, mission outcomes, and their consequences. The bank system owns account, vault, suspicion, and access state. The story event presents important decisions but should not be the source of truth for those systems.

### Gang job proposals

A proposal should describe:

- Job identity, source, and objective.
- Potential reward and costs.
- Expected risk and likely consequences.
- Honor impact and relevant alliances.
- Required people, skills, tools, and supplies.
- Possible approaches and known constraints.
- Which members support, oppose, or abstain, and why.
- The result of the vote and any player override.

Members should vote according to their traits and experiences, not random selection. Relevant factors can include loyalty, greed, courage, honor alignment, skills, relationships, personal goals, and memories. Make the reason for a recommendation available for dialogue or debugging.

### Bank influence and vault access

A bank-related opportunity can introduce a progression arc in which the gang seeks influence or access to a bank vault. Possible fictional approaches may involve building trust, bribery, recruiting an insider, or other risky plans. Each approach must have distinct costs and consequences.

The result should be represented as explicit world state, such as:

- Manager relationship or allegiance.
- Whether the gang has legitimate or illicit access, and its scope.
- Current suspicion and security attention.
- Known security conditions or a job-specific opportunity.
- A history of deposits, withdrawals, losses, or transfers that informs vault contents.
- Future risks if the plan is exposed or the arrangement changes.

Do not treat vault access as a permanent, consequence-free unlock. Jobs, suspicion, the bank’s records, and changes in personnel should be able to affect it.

## Payout and investment rules

- Calculate the payout from net proceeds, after recorded job costs.
- The split is fixed: 50% to the participating group and 50% to gang shared funds.
- Divide the participants’ pool equally among the people who went on the job, including the player if they participated.
- Do not offer a player-configurable split, contribution-weighted shares, or ad hoc bonuses as part of job settlement.
- The camp chest is the default destination for gang funds. Use a bank only when story progression supports it.
- Create and persist a pending reward record before presenting the gathering.
- Settle each job’s reward once only. Repeated calls, save/load, or an interrupted gathering must not duplicate or erase it.
- Keep the gathering separate from the ledger’s source-of-truth data.
- Investment features are a later system: any investment must debit available gang funds and record its result without changing the fixed job split.

## Persistence and integration notes

- Before changing code, inspect the current owning files and use their actual types and members.
- Store durable identifiers and outcomes, not transient game handles or object references.
- Keep a clear distinction between pending, presented, committed, interrupted, and completed events.
- Define defaults and load behavior for older or incomplete save data.
- Use Newtonsoft.Json in line with the project’s requirements.
- Check save/load and repeated-call behavior for every system that moves money, loot, supplies, or progression.
- Keep systems separated by responsibility, but integrate them through explicit data and calls. Avoid adding modules that are never invoked by the running mod.
- Confirm project-file inclusion rules before adding a new `.cs` file.

## Suggested implementation order

### Milestone 0: Repository and runtime audit

- Verify the current project structure, system contracts, save flow, and available game API.
- Record which features are already implemented, incomplete, or placeholders.
- Identify the actual in-game interaction and command entry points.
- Confirm how new source files are included in the Visual Studio project.

### Milestone 1: Job reward ledger

- Define the job outcome and reward record using current project conventions.
- Calculate costs, member shares, and gang reserve.
- Add persistence and duplicate-settlement protection.
- Test invalid, interrupted, and repeated settlement paths.

### Milestone 2: Base gathering and share presentation

- Define event eligibility and pending-event behavior.
- Present the reward and player choices at a safe base state.
- Apply the payout once, whether the scene completes normally or uses a fallback.
- Update member reactions and memories.

### Milestone 3: Gang job voting

- Connect proposals to the actual job model.
- Add member preference scoring and recorded votes.
- Allow the player to participate or override, with consequences.
- Test ties, unavailable members, changing votes, and save/load.

### Milestone 4: Base progression and investment

- Add persisted base stage and supported upgrades.
- Connect gang reserve spending to supplies or facilities.
- Make upgrades affect gameplay rather than only display text.

### Milestone 5: Bank opportunity arc

- Connect job proposals to bank state and NPC relationships.
- Implement scoped access, vault accounting, and suspicion consequences.
- Test branch outcomes and persistence after manager or security changes.

### Milestone 6: Expand world jobs and logistics

- Add train, stagecoach, protection, wagon, artillery, and supply opportunities in integrated slices.
- Connect rewards to the ledger and equipment or transport systems.
- Continue with companion needs, tactical coordination, locks, and destruction as their dependencies are ready.

## Acceptance checklist for each feature

Before marking a feature implemented or tested, confirm:

- [ ] It is connected to the running mod’s actual entry points.
- [ ] The code matches the current repository contracts.
- [ ] Success, failure, cancellation, and interruption are defined.
- [ ] Save/load preserves the intended state.
- [ ] Repeating the action does not duplicate money, loot, jobs, or progression.
- [ ] Related systems receive the correct outcome.
- [ ] The player receives understandable feedback.
- [ ] The behavior has been tested in-game or its test limitation is recorded.
- [ ] This document’s status and known limitations are updated.

### Job reward acceptance criteria

- Net proceeds equal the job reward minus its recorded costs.
- The participants’ pool and gang-fund pool are each exactly half of net proceeds.
- All eligible participants, including the player when applicable, receive equal shares; rounding reconciles to the pool total.
- The reward remains pending if a gathering is skipped, unavailable, or interrupted.
- Completing or retrying settlement cannot pay the same job twice, including after save/load.
- Camp-chest funds persist. Bank routing is unavailable until story progression explicitly allows it.
- Gathering presentation does not change the fixed payout rule or serve as the reward ledger.

## New idea template

Copy this section for each proposed feature.

### Idea: [Short name]

- **Player-facing goal:**
- **Story purpose:**
- **Systems involved:**
- **Prerequisites:**
- **Player choices:**
- **Companion or NPC reactions:**
- **Costs and rewards:**
- **Honor, trust, suspicion, or alliance effects:**
- **World-state changes:**
- **Persistence requirements:**
- **Failure, interruption, and fallback behavior:**
- **Acceptance tests:**
- **Status:** Planned

## Decision log

Record decisions that change the architecture or story direction.

- **2026-11-05:** Job payouts are designed as pending, in-world gang events at an appropriate base, rather than automatic account deposits. The event must settle the ledger exactly once.
- **2026-11-05:** Bank-manager influence and vault access belong to a Job System progression arc; the bank system records access, accounts, security, and vault state.
- **2026-11-05:** Any reuse of base-game story events or cutscenes must be verified against the repository and supported API before implementation.