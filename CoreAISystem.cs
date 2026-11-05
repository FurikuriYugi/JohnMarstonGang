using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoreAISystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly TacticalDecisionSystem _tacticalSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly CompanionAutonomousTaskSystem _autonomousTaskSystem;
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
        _autonomousTaskSystem = new CompanionAutonomousTaskSystem(_registry, _bankSystem);
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

            CompanionAIState aiState = EnsureAIState(entity.EntityId);
            CompanionContext context = new CompanionContext
            {
                PlayerPosition = playerPed.Position,
                CompanionPosition = companionPed.Position,
                DistanceToPlayer = Vector3.Distance(playerPed.Position, companionPed.Position),
                PlayerHealth = playerPed.Health,
                CompanionHealth = companionPed.Health,
                IsPlayerInCombat = playerInCombat,
                NearbyEnemyCount = nearbyEnemies?.Count ?? 0
            };

            // Update needs and morale
            UpdateNeeds(entity, aiState, context);

            // Handle autonomous tasks when not following
            if (!entity.IsFollowingPlayer)
            {
                HandleAutonomousBehavior(entity, aiState, companionPed, playerPed, context);
            }
            else
            {
                // Combat and tactical behavior when following
                HandleCombatBehavior(entity, aiState, companionPed, playerPed, context, nearbyEnemies);
            }

            // Experience and skill progression
            if (aiState.ExperiencePoints >= 100)
            {
                LevelUp(entity, aiState);
            }

            // Update registry
            _registry.UpdateEntity(entity);
        }
    }

    private void HandleAutonomousBehavior(WorldEntityData entity, CompanionAIState aiState, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        // Manage autonomous tasks from the task system
        bool hasActiveTask = _autonomousTaskSystem.UpdateCompanionTask(entity.EntityId, companionPed, aiState, context);

        if (hasActiveTask)
            return;

        // Assign new task based on needs
        AssignAutonomousTask(entity, aiState, companionPed, playerPed, context);
    }

    private void AssignAutonomousTask(WorldEntityData entity, CompanionAIState aiState, Ped companionPed, Ped playerPed, CompanionContext context)
    {
        // Priority: Health > Hunger > Thirst > Ammo > Tasks

        // Check if companion needs healing
        if (companionPed.Health < companionPed.MaxHealth * 0.7f)
        {
            _autonomousTaskSystem.AssignTask(entity.EntityId, "Heal", new Dictionary<string, object>
            {
                { "targetHealth", companionPed.MaxHealth }
            });
            aiState.CurrentGoal = "Finding healing items";
            return;
        }

        // Check if companion is hungry
        if (aiState.Hunger > 70)
        {
            _autonomousTaskSystem.AssignTask(entity.EntityId, "FindFood", new Dictionary<string, object>
            {
                { "targetNearbyRadius", 100f },
                { "maxPrice", (decimal)(aiState.Morale > 50 ? 50 : 30) }
            });
            aiState.CurrentGoal = "Finding food and water";
            aiState.GoalTimer = _random.Next(60, 180);
            return;
        }

        // Check if companion needs ammo
        int ammoCount = _autonomousTaskSystem.GetCompanionAmmoCount(companionPed);
        if (ammoCount < 20)
        {
            _autonomousTaskSystem.AssignTask(entity.EntityId, "FindAmmo", new Dictionary<string, object>
            {
                { "targetAmmoCount", 30 },
                { "maxPrice", (decimal)(aiState.Morale > 50 ? 100 : 50) }
            });
            aiState.CurrentGoal = "Restocking ammunition";
            aiState.GoalTimer = _random.Next(120, 300);
            return;
        }

        // Idle or loot nearby items
        if (_random.Next(0, 100) < 30 && aiState.Hunger < 30)
        {
            _autonomousTaskSystem.AssignTask(entity.EntityId, "LootNearby", new Dictionary<string, object>
            {
                { "searchRadius", 50f },
                { "prioritizeHigh", true }
            });
            aiState.CurrentGoal = "Scavenging for supplies";
            aiState.GoalTimer = _random.Next(30, 90);
        }
    }

    private void HandleCombatBehavior(WorldEntityData entity, CompanionAIState aiState, Ped companionPed, Ped playerPed, CompanionContext context, List<Ped> nearbyEnemies)
    {
        if (!context.IsPlayerInCombat || nearbyEnemies == null || nearbyEnemies.Count == 0)
            return;

        string threatType = _tacticalSystem.EvaluateThreat(playerPed, nearbyEnemies);
        string combatStyle = ResolveCombatStyleWithPersonality(entity, aiState, threatType, context);

        // Apply combat style to companion
        aiState.LastCombatStyle = combatStyle;
        entity.CombatStyle = combatStyle;

        // Track combat experience
        aiState.ExperiencePoints += _random.Next(5, 15);
        aiState.Fatigue += _random.Next(1, 3);

        // Morale effects from combat
        if (context.CompanionHealth < companionPed.MaxHealth * 0.5f)
        {
            aiState.Morale = Math.Max(0, aiState.Morale - 5);
        }
        if (nearbyEnemies.Count > 3)
        {
            aiState.Morale = Math.Max(0, aiState.Morale - 2);
        }
    }

    private void UpdateNeeds(WorldEntityData entity, CompanionAIState aiState, CompanionContext context)
    {
        // Increment needs over time
        aiState.Hunger = Math.Min(100, aiState.Hunger + _random.Next(0, 2));
        aiState.Thirst = Math.Min(100, aiState.Thirst + _random.Next(0, 2));
        aiState.Fatigue = Math.Min(100, aiState.Fatigue + _random.Next(0, 1));

        // Hunger/Thirst affect morale
        if (aiState.Hunger > 50)
            aiState.Morale = Math.Max(0, aiState.Morale - 1);
        if (aiState.Thirst > 50)
            aiState.Morale = Math.Max(0, aiState.Morale - 1);

        // Fatigue affects combat effectiveness
        if (aiState.Fatigue > 70)
            entity.CombatSkill = Math.Max(0, entity.CombatSkill - 10);

        // Rest reduces fatigue
        if (!context.IsPlayerInCombat && aiState.Fatigue > 0)
        {
            aiState.Fatigue = Math.Max(0, aiState.Fatigue - 2);
        }
    }

    private string ResolveCombatStyleWithPersonality(WorldEntityData entity, CompanionAIState aiState, string threatType, CompanionContext context)
    {
        string baseStyle = _tacticalSystem.ResolveCombatStyle(
            threatType,
            aiState.PrefersStealth,
            context.DistanceToPlayer < 10f,
            false,
            false
        );

        // Personality modifies behavior
        if (aiState.Personality == "Bold")
        {
            if (baseStyle == "Ranged Medium")
                return "Ranged Short"; // Get closer and more aggressive
            if (baseStyle == "Stealth")
                return "Lethal Melee"; // Go loud
        }
        else if (aiState.Personality == "Cautious")
        {
            if (baseStyle == "Ranged Short")
                return "Ranged Medium"; // Stay back
            if (baseStyle == "Lethal Melee")
                return "Ranged Medium"; // Avoid close combat
        }
        else if (aiState.Personality == "Loyal")
        {
            // Stick closer to player
            if (context.DistanceToPlayer > 15f)
                return "Ranged Short"; // Move closer
        }

        return baseStyle;
    }

    private void LevelUp(WorldEntityData entity, CompanionAIState aiState)
    {
        aiState.ExperiencePoints -= 100;
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
        aiState.Morale = Math.Min(100, aiState.Morale + 10);
    }

    public void RewardCompanionForJob(string companionId, JobData job, bool jobCompleted)
    {
        if (!_aiStates.ContainsKey(companionId))
            return;

        CompanionAIState aiState = _aiStates[companionId];
        WorldEntityData entity = _registry.GetEntity(companionId);

        if (entity == null)
            return;

        if (jobCompleted)
        {
            // Calculate companion's share
            decimal companionShare = job.RewardCash / 2m; // 50% split with player for now
            decimal goldShare = job.RewardGold / 2m;

            // Add to companion's bank account
            _bankSystem.Deposit(companionId, companionShare, goldShare);

            // Update AI state
            aiState.TotalEarnings += companionShare;
            aiState.CompletedJobs.Add(job.JobId);
            aiState.ExperiencePoints += 50;

            // Morale boost
            aiState.Morale = Math.Min(100, aiState.Morale + 15);

            // Honor affects loyalty
            if (job.IsIllegal)
            {
                entity.Loyalty = Math.Max(0, entity.Loyalty - 5);
            }
            else
            {
                entity.Loyalty = Math.Min(100, entity.Loyalty + 10);
            }

            // Update skill based on job type
            UpdateSkillsFromJobCompletion(entity, aiState, job);
        }
        else
        {
            // Job failed - morale penalty
            aiState.Morale = Math.Max(0, aiState.Morale - 20);
            entity.Loyalty = Math.Max(0, entity.Loyalty - 15);
        }
    }

    private void UpdateSkillsFromJobCompletion(WorldEntityData entity, CompanionAIState aiState, JobData job)
    {
        switch (job.JobType)
        {
            case "Combat":
                entity.CombatSkill = Math.Min(100, entity.CombatSkill + 5);
                break;
            case "Stealth":
                entity.StealthSkill = Math.Min(100, entity.StealthSkill + 5);
                break;
            case "Support":
                entity.SupportSkill = Math.Min(100, entity.SupportSkill + 5);
                break;
            case "Mixed":
                entity.CombatSkill = Math.Min(100, entity.CombatSkill + 2);
                entity.StealthSkill = Math.Min(100, entity.StealthSkill + 2);
                entity.SupportSkill = Math.Min(100, entity.SupportSkill + 1);
                break;
        }
    }

    public CompanionAIState GetAIState(string entityId)
    {
        return EnsureAIState(entityId);
    }

    public void SetAIState(string entityId, CompanionAIState state)
    {
        _aiStates[entityId] = state;
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
}

public class CompanionContext
{
    public Vector3 PlayerPosition { get; set; }
    public Vector3 CompanionPosition { get; set; }
    public float DistanceToPlayer { get; set; }
    public int PlayerHealth { get; set; }
    public int CompanionHealth { get; set; }
    public bool IsPlayerInCombat { get; set; }
    public int NearbyEnemyCount { get; set; }
    public float TimeElapsed { get; set; }
}