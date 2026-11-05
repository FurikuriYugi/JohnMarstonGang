using GTA;
using GTA.Math;
using Newtonsoft.Json;
using RDR2;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoreAISystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly TacticalDecisionSystem _tacticalSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly Dictionary<string, CompanionAIState> _aiStates = new Dictionary<string, CompanionAIState>();
    private readonly Random _random = new Random();
    private const int UPDATE_INTERVAL = 30;
    private int _updateCounter = 0;

    public CoreAISystem(WorldEntityRegistry registry, TacticalDecisionSystem tacticalSystem, BankSystem bankSystem, JobSystem jobSystem)
    {
        _registry = registry;
        _tacticalSystem = tacticalSystem;
        _bankSystem = bankSystem;
        _jobSystem = jobSystem;
    }

    public void Update(Ped playerPed, Dictionary<string, Ped> companionPeds, bool playerInCombat, List<Ped> nearbyEnemies)
    {
        _updateCounter++;
        if (_updateCounter % UPDATE_INTERVAL != 0)
            return;

        if (playerPed == null || !playerPed.Exists())
            return;

        List<WorldEntityData> entities = _registry.GetFollowers();

        foreach (WorldEntityData entity in entities)
        {
            if (entity == null || !entity.IsAlive)
                continue;

            if (!companionPeds.ContainsKey(entity.EntityId))
                continue;

            Ped companionPed = companionPeds[entity.EntityId];
            if (companionPed == null || !companionPed.Exists())
                continue;

            UpdateCompanionAI(entity, companionPed, playerPed, playerInCombat, nearbyEnemies);
        }
    }

    private void UpdateCompanionAI(WorldEntityData entity, Ped companionPed, Ped playerPed, bool playerInCombat, List<Ped> nearbyEnemies)
    {
        CompanionAIState state = EnsureAIState(entity.EntityId);

        // Update emotional state
        UpdateEmotionalState(entity, state, playerPed, companionPed, playerInCombat, nearbyEnemies);

        // Determine context
        CompanionContext context = DetermineContext(entity, companionPed, playerPed, playerInCombat, nearbyEnemies);

        // Make behavioral decisions
        if (entity.IsFollowingPlayer)
        {
            HandleFollowingBehavior(entity, state, companionPed, playerPed, context);
        }
        else
        {
            HandleIdleBehavior(entity, state, companionPed, playerPed, context);
        }

        // Gain experience
        GainExperience(entity, state, context);

        // Update needs
        UpdateNeeds(entity, state, context);
    }

    private void UpdateEmotionalState(WorldEntityData entity, CompanionAIState state, Ped playerPed, Ped companionPed, bool playerInCombat, List<Ped> nearbyEnemies)
    {
        // Morale changes based on situation
        if (playerInCombat)
        {
            if (nearbyEnemies != null && nearbyEnemies.Count > 3)
            {
                state.Morale -= 2; // Outnumbered
            }
            else if (entity.CombatSkill >= 75)
            {
                state.Morale += 1; // Confident in combat
            }
        }
        else if (playerPed != null && companionPed != null && companionPed.Position.DistanceTo(playerPed.Position) < 5f)
        {
            state.Morale += 1; // Happy to be near player
        }

        // Fatigue increases during combat/activity
        if (playerInCombat || companionPed.IsRunning)
        {
            state.Fatigue = Math.Min(100, state.Fatigue + 2);
        }
        else
        {
            state.Fatigue = Math.Max(0, state.Fatigue - 1); // Rest reduces fatigue
        }

        // Needs increase over time
        state.Hunger = Math.Min(100, state.Hunger + 1);
        state.Thirst = Math.Min(100, state.Thirst + 1);

        // Clamp values
        state.Morale = Math.Max(0, Math.Min(100, state.Morale));
        entity.Loyalty = Math.Max(0, Math.Min(100, entity.Loyalty));
    }

    private CompanionContext DetermineContext(WorldEntityData entity, Ped companionPed, Ped playerPed, bool playerInCombat, List<Ped> nearbyEnemies)
    {
        float distanceToPlayer = playerPed != null ? companionPed.Position.DistanceTo(playerPed.Position) : 999f;

        CompanionContext context = new CompanionContext
        {
            IsPlayerInCombat = playerInCombat,
            NearbyEnemyCount = nearbyEnemies?.Count ?? 0,
            PlayerLocation = playerPed?.Position ?? Vector3.Zero,
            EntityLocation = companionPed?.Position ?? Vector3.Zero,
            DistanceToPlayer = distanceToPlayer,
            IsPlayerShopping = false, // TODO: detect shopping
            IsPlayerIndoor = false // TODO: detect indoor
        };

        // Determine activity type
        if (playerInCombat)
            context.CurrentActivity = "InCombat";
        else if (distanceToPlayer < 3f)
            context.CurrentActivity = "WithPlayer";
        else if (distanceToPlayer < 20f)
            context.CurrentActivity = "NearPlayer";
        else
            context.CurrentActivity = "Distant";

        return context;
    }

    private void HandleFollowingBehavior(WorldEntityData entity, CompanionAIState state, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        if (context.IsPlayerInCombat)
        {
            HandleCombatFollowing(entity, state, companionPed, playerPed, context);
        }
        else
        {
            HandleCasualFollowing(entity, state, companionPed, playerPed, context);
        }
    }

    private void HandleCombatFollowing(WorldEntityData entity, CompanionAIState state, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        // Resolve threat type
        string threatType = _tacticalSystem.ResolveThreatType(
            context.NearbyEnemyCount > 1,
            false,
            false,
            state.PrefersStealth
        );

        // Resolve combat style based on personality and situation
        string combatStyle = ResolveCombatStyleWithPersonality(entity, state, threatType, context);
        entity.CombatStyle = combatStyle;
        state.LastCombatStyle = combatStyle;

        // Position tactically
        if (context.NearbyEnemyCount <= 1)
        {
            // Support player's flank
            Vector3 offset = playerPed.ForwardVector * 2f + playerPed.RightVector * (state.Personality == "Bold" ? -4f : 3f);
            companionPed.Task.FollowToOffsetOfEntity(playerPed, offset, 3f, -1, 2f, true);
        }
        else
        {
            // Spread out formation
            Vector3 offset = playerPed.ForwardVector * 1f + playerPed.RightVector * -4f;
            companionPed.Task.FollowToOffsetOfEntity(playerPed, offset, 3f, -1, 2f, true);
        }
    }

    private void HandleCasualFollowing(WorldEntityData entity, CompanionAIState state, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        // Relaxed following with personality variation
        Vector3 baseOffset = new Vector3(0f, -2f, 0f);

        if (state.Personality == "Bold")
            baseOffset = new Vector3(1f, -1.5f, 0f);
        else if (state.Personality == "Cautious")
            baseOffset = new Vector3(-1f, -3f, 0f);

        companionPed.Task.FollowToOffsetOfEntity(playerPed, baseOffset, 2f, -1, 2.5f, true);
    }

    private void HandleIdleBehavior(WorldEntityData entity, CompanionAIState state, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        // When not following, companions do autonomous activities

        // Critical needs
        if (state.Hunger > 75)
        {
            state.CurrentGoal = "Eat";
            state.GoalTimer = 0;
        }
        else if (state.Thirst > 75)
        {
            state.CurrentGoal = "Drink";
            state.GoalTimer = 0;
        }
        else if (state.Fatigue > 80)
        {
            state.CurrentGoal = "Rest";
            state.GoalTimer = 0;
        }
        // Personal goals
        else if (string.IsNullOrEmpty(state.CurrentGoal) || state.GoalTimer > 600)
        {
            PickRandomIdleGoal(state);
        }
        else
        {
            state.GoalTimer++;
        }

        // Execute current goal (actual behavior would be implemented with game tasks)
        ExecuteGoal(entity, state, companionPed, context);
    }

    private void PickRandomIdleGoal(CompanionAIState state)
    {
        int random = _random.Next(0, 5);
        switch (random)
        {
            case 0:
                state.CurrentGoal = "Patrol";
                break;
            case 1:
                state.CurrentGoal = "Rest";
                break;
            case 2:
                state.CurrentGoal = "Socialize";
                break;
            case 3:
                state.CurrentGoal = "Maintain Weapon";
                break;
            default:
                state.CurrentGoal = "Wait";
                break;
        }
        state.GoalTimer = 0;
    }

    private void ExecuteGoal(WorldEntityData entity, CompanionAIState state, Ped companionPed, CompanionContext context)
    {
        // TODO: Implement actual game behaviors based on current goal
        // This is where you'd set companion animations, movements, etc.
        // For now, just track the goal
    }

    private void GainExperience(WorldEntityData entity, CompanionAIState state, CompanionContext context)
    {
        if (context.IsPlayerInCombat)
        {
            state.ExperiencePoints += 2;
            entity.CombatSkill = Math.Min(100, entity.CombatSkill + 1);

            if (context.DistanceToPlayer > 15f)
                entity.StealthSkill = Math.Min(100, entity.StealthSkill + 1);
        }

        if (context.CurrentActivity == "WithPlayer")
        {
            state.ExperiencePoints += 1;
            entity.SupportSkill = Math.Min(100, entity.SupportSkill + 1);
        }

        // Level up after reaching threshold
        if (state.ExperiencePoints >= 100)
        {
            LevelUp(entity, state);
        }
    }

    private void LevelUp(WorldEntityData entity, CompanionAIState state)
    {
        state.ExperiencePoints -= 100;
        int levelGains = _random.Next(2, 5);

        switch (_random.Next(0, 3))
        {
            case 0:
                entity.CombatSkill = Math.Min(100, entity.CombatSkill + levelGains);
                break;
            case 1:
                entity.StealthSkill = Math.Min(100, entity.StealthSkill + levelGains);
                break;
            case 2:
                entity.SupportSkill = Math.Min(100, entity.SupportSkill + levelGains);
                break;
        }

        entity.Loyalty = Math.Min(100, entity.Loyalty + 5);
    }

    private void UpdateNeeds(WorldEntityData entity, CompanionAIState state, CompanionContext context)
    {
        // Hunger/Thirst affect morale
        if (state.Hunger > 50)
            state.Morale -= 1;
        if (state.Thirst > 50)
            state.Morale -= 1;

        // Fatigue affects combat effectiveness
        if (state.Fatigue > 70)
            entity.CombatSkill = Math.Max(0, entity.CombatSkill - 10);
    }

    private string ResolveCombatStyleWithPersonality(WorldEntityData entity, CompanionAIState state, string threatType, CompanionContext context)
    {
        // Use tactical system as base
        string baseStyle = _tacticalSystem.ResolveCombatStyle(
            threatType,
            state.PrefersStealth,
            context.DistanceToPlayer < 10f,
            false,
            false
        );

        // Personality modifies behavior
        if (state.Personality == "Bold")
        {
            // Prefer aggressive combat
            if (baseStyle == "Ranged Medium")
                return "Ranged Short"; // Get closer
        }
        else if (state.Personality == "Cautious")
        {
            // Prefer ranged combat
            if (baseStyle == "Ranged Short")
                return "Ranged Medium"; // Stay back
        }

        return baseStyle;
    }

    private CompanionAIState EnsureAIState(string entityId)
    {
        if (!_aiStates.ContainsKey(entityId))
        {
            _aiStates[entityId] = new CompanionAIState
            {
                EntityId = entityId,
                Personality = "Neutral",
                Morale = 50,
                Fatigue = 0,
                Hunger = 0,
                Thirst = 0,
                PrefersStealth = false,
                CurrentGoal = null,
                GoalTimer = 0,
                ExperiencePoints = 0,
                LastCombatStyle = "Ranged Medium",
                SkillProgression = new Dictionary<string, int>(),
                CompletedJobs = new List<string>(),
                TotalEarnings = 0m,
                BanksVisited = 0,
                DepositsWithdrawn = 0
            };
        }
        return _aiStates[entityId];
    }

    public void RewardCompanionForJob(string companionId