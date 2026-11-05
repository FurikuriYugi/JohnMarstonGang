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
    private readonly Dictionary<string, CompanionAIState> _aiStates =
        new Dictionary<string, CompanionAIState>();
    private readonly Random _random = new Random();

    private const int UPDATE_INTERVAL = 30;
    private int _updateCounter = 0;

    public CoreAISystem(
        WorldEntityRegistry registry,
        TacticalDecisionSystem tacticalSystem,
        BankSystem bankSystem,
        JobSystem jobSystem)
    {
        _registry = registry;
        _tacticalSystem = tacticalSystem;
        _bankSystem = bankSystem;
        _jobSystem = jobSystem;
        _autonomousTaskSystem =
            new CompanionAutonomousTaskSystem(_registry, _bankSystem);
    }

    public void Update(
        Ped playerPed,
        Dictionary<string, Ped> companionPeds,
        bool playerInCombat,
        List<Ped> nearbyEnemies)
    {
        _updateCounter++;
        if (_updateCounter % UPDATE_INTERVAL != 0)
            return;

        if (playerPed == null || !playerPed.Exists() || companionPeds == null)
            return;

        int livingEnemyCount = CountLivingEnemies(nearbyEnemies);
        List<WorldEntityData> entities = _registry.GetFollowers();

        foreach (WorldEntityData entity in entities)
        {
            if (entity == null || !entity.IsAlive)
                continue;

            if (string.IsNullOrWhiteSpace(entity.EntityId) ||
                !companionPeds.ContainsKey(entity.EntityId))
            {
                continue;
            }

            Ped companionPed = companionPeds[entity.EntityId];
            if (companionPed == null || !companionPed.Exists())
                continue;

            CompanionAIState aiState = EnsureAIState(entity.EntityId);
            if (aiState == null)
                continue;

            CompanionContext context = new CompanionContext
            {
                PlayerPosition = playerPed.Position,
                CompanionPosition = companionPed.Position,
                DistanceToPlayer =
                    Vector3.Distance(playerPed.Position, companionPed.Position),
                PlayerHealth = playerPed.Health,
                CompanionHealth = companionPed.Health,
                IsPlayerInCombat = playerInCombat,
                NearbyEnemyCount = livingEnemyCount
            };

            UpdateNeeds(entity, aiState, context);

            if (!entity.IsFollowingPlayer)
            {
                HandleAutonomousBehavior(
                    entity,
                    aiState,
                    companionPed,
                    playerPed,
                    context);
            }
            else
            {
                _autonomousTaskSystem.CancelTask(entity.EntityId);
                aiState.ResetCurrentTask();

                HandleCombatBehavior(
                    entity,
                    aiState,
                    companionPed,
                    playerPed,
                    context,
                    nearbyEnemies);
            }

            while (aiState.ExperiencePoints >= 100)
                LevelUp(entity, aiState);

            _registry.UpdateEntity(entity);
        }
    }

    private void HandleAutonomousBehavior(
        WorldEntityData entity,
        CompanionAIState aiState,
        Ped companionPed,
        Ped playerPed,
        CompanionContext context)
    {
        bool hasActiveTask =
            _autonomousTaskSystem.UpdateCompanionTask(
                entity.EntityId,
                companionPed,
                aiState,
                context);

        if (hasActiveTask)
            return;

        AssignAutonomousTask(
            entity,
            aiState,
            companionPed,
            playerPed,
            context);
    }

    private void AssignAutonomousTask(
        WorldEntityData entity,
        CompanionAIState aiState,
        Ped companionPed,
        Ped playerPed,
        CompanionContext context)
    {
        if (companionPed == null || !companionPed.Exists())
            return;

        if (context != null && context.IsPlayerInCombat)
        {
            aiState.CurrentGoal = "Waiting for combat to end.";
            return;
        }

        if (aiState.Fatigue >= 70)
        {
            _autonomousTaskSystem.AssignTask(
                entity.EntityId,
                "RestAtCamp",
                new Dictionary<string, object>());

            if (_autonomousTaskSystem.HasActiveTask(
                entity.EntityId,
                "RestAtCamp"))
            {
                aiState.CurrentGoal = "Resting at camp.";
            }

            return;
        }

        if (string.IsNullOrWhiteSpace(aiState.CurrentGoal) ||
            aiState.CurrentGoal == "Resting at camp.")
        {
            aiState.CurrentGoal = "Waiting for orders.";
        }
    }

    private void HandleCombatBehavior(
        WorldEntityData entity,
        CompanionAIState aiState,
        Ped companionPed,
        Ped playerPed,
        CompanionContext context,
        List<Ped> nearbyEnemies)
    {
        if (!context.IsPlayerInCombat ||
            context.NearbyEnemyCount == 0 ||
            nearbyEnemies == null)
        {
            return;
        }

        string threatType =
            _tacticalSystem.EvaluateThreat(playerPed, nearbyEnemies);

        string combatStyle = ResolveCombatStyleWithPersonality(
            entity,
            aiState,
            threatType,
            context);

        aiState.LastCombatStyle = combatStyle;
        entity.CombatStyle = combatStyle;
        aiState.ExperiencePoints += _random.Next(5, 15);

        // Combat builds fatigue gradually instead of adding it on every AI update.
        if (_random.Next(0, 30) == 0)
            aiState.Fatigue = Math.Min(100, aiState.Fatigue + 1);

        if (context.CompanionHealth < companionPed.MaxHealth * 0.5f)
            aiState.Morale = Math.Max(0, aiState.Morale - 5);

        if (context.NearbyEnemyCount > 3)
            aiState.Morale = Math.Max(0, aiState.Morale - 2);
    }

    private void UpdateNeeds(
        WorldEntityData entity,
        CompanionAIState aiState,
        CompanionContext context)
    {
        // Need values change occasionally rather than on every AI update.
        if (_random.Next(0, 60) == 0)
            aiState.Hunger = Math.Min(100, aiState.Hunger + 1);

        if (_random.Next(0, 60) == 0)
            aiState.Thirst = Math.Min(100, aiState.Thirst + 1);

        // Apply occasional morale penalties while needs remain unmet.
        if (aiState.Hunger > 50 && _random.Next(0, 60) == 0)
            aiState.Morale = Math.Max(0, aiState.Morale - 1);

        if (aiState.Thirst > 50 && _random.Next(0, 60) == 0)
            aiState.Morale = Math.Max(0, aiState.Morale - 1);

        // Fatigue is accumulated during combat and reduced by the supported
        // RestAtCamp task. Do not permanently lower the companion's skill.
    }

    private string ResolveCombatStyleWithPersonality(
        WorldEntityData entity,
        CompanionAIState aiState,
        string threatType,
        CompanionContext context)
    {
        string baseStyle = _tacticalSystem.ResolveCombatStyle(
            threatType,
            aiState.PrefersStealth,
            context.DistanceToPlayer < 10f,
            false,
            false);

        if (aiState.Personality == "Bold")
        {
            if (baseStyle == "Ranged Medium")
                return "Ranged Short";

            if (baseStyle == "Stealth")
                return "Lethal Melee";
        }
        else if (aiState.Personality == "Cautious")
        {
            if (baseStyle == "Ranged Short")
                return "Ranged Medium";

            if (baseStyle == "Lethal Melee")
                return "Ranged Medium";
        }
        else if (aiState.Personality == "Loyal")
        {
            if (context.DistanceToPlayer > 15f)
                return "Ranged Short";
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
                entity.CombatSkill =
                    Math.Min(100, entity.CombatSkill + levelGains);
                break;

            case 1:
                entity.StealthSkill =
                    Math.Min(100, entity.StealthSkill + levelGains);
                break;

            case 2:
                entity.SupportSkill =
                    Math.Min(100, entity.SupportSkill + levelGains);
                break;
        }

        entity.Loyalty = Math.Min(100, entity.Loyalty + 5);
        aiState.Morale = Math.Min(100, aiState.Morale + 10);
    }

    public void RewardCompanionForJob(
        string companionId,
        JobData job,
        bool jobCompleted)
    {
        if (string.IsNullOrWhiteSpace(companionId) || job == null)
            return;

        CompanionAIState aiState;
        if (!_aiStates.TryGetValue(companionId, out aiState) ||
            aiState == null)
        {
            return;
        }

        WorldEntityData entity = _registry.GetEntity(companionId);
        if (entity == null)
            return;

        if (jobCompleted)
        {
            decimal companionShare = job.RewardCash / 2m;
            decimal goldShare = job.RewardGold / 2m;

            _bankSystem.DepositMoney(
                companionId,
                companionShare,
                goldShare);

            aiState.TotalEarnings += companionShare;
            aiState.CompletedJobs.Add(job.JobId);
            aiState.ExperiencePoints += 50;
            aiState.Morale = Math.Min(100, aiState.Morale + 15);

            if (job.IsIllegal)
                entity.Loyalty = Math.Max(0, entity.Loyalty - 5);
            else
                entity.Loyalty = Math.Min(100, entity.Loyalty + 10);

            UpdateSkillsFromJobCompletion(entity, aiState, job);
        }
        else
        {
            aiState.Morale = Math.Max(0, aiState.Morale - 20);
            entity.Loyalty = Math.Max(0, entity.Loyalty - 15);
        }
    }

    private void UpdateSkillsFromJobCompletion(
        WorldEntityData entity,
        CompanionAIState aiState,
        JobData job)
    {
        switch (job.JobType)
        {
            case "Combat":
                entity.CombatSkill =
                    Math.Min(100, entity.CombatSkill + 5);
                break;

            case "Stealth":
                entity.StealthSkill =
                    Math.Min(100, entity.StealthSkill + 5);
                break;

            case "Support":
                entity.SupportSkill =
                    Math.Min(100, entity.SupportSkill + 5);
                break;

            case "Mixed":
                entity.CombatSkill =
                    Math.Min(100, entity.CombatSkill + 2);
                entity.StealthSkill =
                    Math.Min(100, entity.StealthSkill + 2);
                entity.SupportSkill =
                    Math.Min(100, entity.SupportSkill + 1);
                break;
        }
    }

    public CompanionAIState GetAIState(string entityId)
    {
        if (string.IsNullOrWhiteSpace(entityId))
            return null;

        return EnsureAIState(entityId);
    }

    public void SetAIState(string entityId, CompanionAIState state)
    {
        if (string.IsNullOrWhiteSpace(entityId) || state == null)
            return;

        state.EntityId = entityId;
        _aiStates[entityId] = state;
    }

    private CompanionAIState EnsureAIState(string entityId)
    {
        if (string.IsNullOrWhiteSpace(entityId))
            return null;

        CompanionAIState aiState;
        if (!_aiStates.TryGetValue(entityId, out aiState) ||
            aiState == null)
        {
            aiState = new CompanionAIState
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

            _aiStates[entityId] = aiState;
        }

        return aiState;
    }

    private int CountLivingEnemies(List<Ped> nearbyEnemies)
    {
        if (nearbyEnemies == null)
            return 0;

        int count = 0;

        foreach (Ped enemy in nearbyEnemies)
        {
            if (enemy == null || !enemy.Exists() || enemy.IsDeadOrDying)
                continue;

            count++;
        }

        return count;
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