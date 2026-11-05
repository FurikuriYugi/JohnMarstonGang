using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;

public class CoreAISystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly TacticalDecisionSystem _tacticalSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly CompanionAutonomousTaskSystem _autonomousTaskSystem;
    private readonly Dictionary<string, CompanionAIState> _aiStates =
        new Dictionary<string, CompanionAIState>();
    private readonly Dictionary<string, NeedsClock> _needsClocks =
        new Dictionary<string, NeedsClock>();
    private readonly Dictionary<string, DateTime> _lastCombatEffects =
        new Dictionary<string, DateTime>();

    private readonly Random _random = new Random();

    private const int UPDATE_INTERVAL = 30;
    private int _updateCounter;

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

        if (playerPed == null || !playerPed.Exists())
            return;

        if (companionPeds == null)
            return;

        List<WorldEntityData> entities =
            _registry.GetByFaction("PlayerGang");

        foreach (WorldEntityData entity in entities)
        {
            if (entity == null ||
                !entity.IsAlive ||
                string.IsNullOrWhiteSpace(entity.EntityId))
            {
                continue;
            }

            Ped companionPed;
            if (!companionPeds.TryGetValue(entity.EntityId, out companionPed) ||
                companionPed == null ||
                !companionPed.Exists() ||
                companionPed.IsDeadOrDying)
            {
                continue;
            }

            CompanionAIState aiState = EnsureAIState(entity.EntityId);

            CompanionContext context = new CompanionContext
            {
                PlayerPosition = playerPed.Position,
                CompanionPosition = companionPed.Position,
                DistanceToPlayer =
                    Vector3.Distance(playerPed.Position, companionPed.Position),
                PlayerHealth = playerPed.Health,
                CompanionHealth = companionPed.Health,
                IsPlayerInCombat = playerInCombat,
                NearbyEnemyCount = nearbyEnemies == null
                    ? 0
                    : nearbyEnemies.Count
            };

            bool isResting =
                _autonomousTaskSystem.HasActiveTask(
                    entity.EntityId,
                    "RestAtCamp");

            UpdateNeeds(entity, aiState, context, isResting);

            if (entity.IsFollowingPlayer)
            {
                _autonomousTaskSystem.CancelTask(entity.EntityId);
                HandleCombatBehavior(
                    entity,
                    aiState,
                    companionPed,
                    playerPed,
                    context,
                    nearbyEnemies);
            }
            else
            {
                HandleAutonomousBehavior(
                    entity,
                    aiState,
                    companionPed,
                    playerPed,
                    context);
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
        bool taskStillActive =
            _autonomousTaskSystem.UpdateCompanionTask(
                entity.EntityId,
                companionPed,
                aiState,
                context);

        if (taskStillActive)
            return;

        AssignAutonomousTask(entity, aiState, companionPed, context);
    }

    private void AssignAutonomousTask(
        WorldEntityData entity,
        CompanionAIState aiState,
        Ped companionPed,
        CompanionContext context)
    {
        if (companionPed.Health < companionPed.MaxHealth * 0.7f)
        {
            aiState.CurrentGoal =
                "Needs first aid; no supported companion healing action is available.";
            return;
        }

        if (aiState.Hunger > 70)
        {
            aiState.CurrentGoal =
                "Needs food; no supported companion purchase or consumption action is available.";
            return;
        }

        if (aiState.Thirst > 70)
        {
            aiState.CurrentGoal =
                "Needs water; no supported companion purchase or consumption action is available.";
            return;
        }

        if (aiState.Fatigue > 70 && !context.IsPlayerInCombat)
        {
            _autonomousTaskSystem.AssignTask(
                entity.EntityId,
                "RestAtCamp",
                new Dictionary<string, object>());

            aiState.CurrentGoal = "Resting";
            return;
        }

        if (aiState.CurrentGoal == "Rested" ||
            string.IsNullOrWhiteSpace(aiState.CurrentGoal) ||
            aiState.CurrentGoal.StartsWith(
                "Needs food;",
                StringComparison.Ordinal) ||
            aiState.CurrentGoal.StartsWith(
                "Needs water;",
                StringComparison.Ordinal) ||
            aiState.CurrentGoal.StartsWith(
                "Needs first aid;",
                StringComparison.Ordinal))
        {
            aiState.CurrentGoal = "Staying with the gang.";
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
            nearbyEnemies == null ||
            nearbyEnemies.Count == 0)
        {
            return;
        }

        string threatType =
            _tacticalSystem.EvaluateThreat(playerPed, nearbyEnemies);

        string combatStyle =
            ResolveCombatStyleWithPersonality(
                aiState,
                threatType,
                context);

        aiState.LastCombatStyle = combatStyle;
        entity.CombatStyle = combatStyle;

        DateTime now = DateTime.UtcNow;
        DateTime lastCombatEffect;

        if (!_lastCombatEffects.TryGetValue(
                entity.EntityId,
                out lastCombatEffect) ||
            (now - lastCombatEffect).TotalSeconds >= 10)
        {
            _lastCombatEffects[entity.EntityId] = now;
            aiState.ExperiencePoints += _random.Next(5, 15);
            aiState.Fatigue = Math.Min(100, aiState.Fatigue + 1);

            if (context.CompanionHealth < companionPed.MaxHealth * 0.5f)
                aiState.Morale = Math.Max(0, aiState.Morale - 1);

            if (nearbyEnemies.Count > 3)
                aiState.Morale = Math.Max(0, aiState.Morale - 1);
        }
    }

    private void UpdateNeeds(
        WorldEntityData entity,
        CompanionAIState aiState,
        CompanionContext context,
        bool isResting)
    {
        NeedsClock clock;

        if (!_needsClocks.TryGetValue(entity.EntityId, out clock))
        {
            clock = new NeedsClock();
            _needsClocks[entity.EntityId] = clock;
        }

        DateTime now = DateTime.UtcNow;
        double elapsedSeconds = (now - clock.LastUpdateUtc).TotalSeconds;

        // Avoid applying a large needs increase after a pause or long frame stall.
        if (elapsedSeconds < 0)
            elapsedSeconds = 0;
        if (elapsedSeconds > 5)
            elapsedSeconds = 5;

        clock.LastUpdateUtc = now;
        clock.HungerSeconds += elapsedSeconds;
        clock.ThirstSeconds += elapsedSeconds;

        if (!context.IsPlayerInCombat && !isResting)
            clock.FatigueSeconds += elapsedSeconds;

        while (clock.HungerSeconds >= 600)
        {
            clock.HungerSeconds -= 600;
            aiState.Hunger = Math.Min(100, aiState.Hunger + 1);
        }

        while (clock.ThirstSeconds >= 300)
        {
            clock.ThirstSeconds -= 300;
            aiState.Thirst = Math.Min(100, aiState.Thirst + 1);
        }

        while (clock.FatigueSeconds >= 600)
        {
            clock.FatigueSeconds -= 600;
            aiState.Fatigue = Math.Min(100, aiState.Fatigue + 1);
        }

        if (aiState.Hunger > 70 || aiState.Thirst > 70)
        {
            clock.MoraleSeconds += elapsedSeconds;

            if (clock.MoraleSeconds >= 60)
            {
                clock.MoraleSeconds %= 60;
                aiState.Morale = Math.Max(0, aiState.Morale - 1);
            }
        }
        else
        {
            clock.MoraleSeconds = 0;
        }

        aiState.Morale = Clamp(aiState.Morale, 0, 100);
        aiState.Hunger = Clamp(aiState.Hunger, 0, 100);
        aiState.Thirst = Clamp(aiState.Thirst, 0, 100);
        aiState.Fatigue = Clamp(aiState.Fatigue, 0, 100);
    }

    private string ResolveCombatStyleWithPersonality(
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

        if (string.Equals(
                aiState.Personality,
                "Bold",
                StringComparison.OrdinalIgnoreCase))
        {
            if (baseStyle == "Ranged Medium")
                return "Ranged Short";

            if (baseStyle == "Stealth")
                return "Lethal Melee";
        }
        else if (string.Equals(
                     aiState.Personality,
                     "Cautious",
                     StringComparison.OrdinalIgnoreCase))
        {
            if (baseStyle == "Ranged Short" ||
                baseStyle == "Lethal Melee")
            {
                return "Ranged Medium";
            }
        }
        else if (string.Equals(
                     aiState.Personality,
                     "Loyal",
                     StringComparison.OrdinalIgnoreCase))
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

            default:
                entity.SupportSkill =
                    Math.Min(100, entity.SupportSkill + levelGains);
                break;
        }

        entity.Loyalty = Math.Min(100, entity.Loyalty + 5);
        aiState.Morale = Math.Min(100, aiState.Morale + 5);
    }

    public void RewardCompanionForJob(
        string companionId,
        JobData job,
        bool jobCompleted)
    {
        if (string.IsNullOrWhiteSpace(companionId) || job == null)
            return;

        CompanionAIState aiState;
        if (!_aiStates.TryGetValue(companionId, out aiState))
            return;

        WorldEntityData entity = _registry.GetEntity(companionId);
        if (entity == null)
            return;

        if (jobCompleted)
        {
            decimal companionShare = job.RewardCash / 2m;
            decimal goldShare = job.RewardGold / 2m;

            _bankSystem.Deposit(companionId, companionShare, goldShare);

            aiState.TotalEarnings += companionShare;
            if (aiState.CompletedJobs == null)
                aiState.CompletedJobs = new List<string>();

            aiState.CompletedJobs.Add(job.JobId);
            aiState.ExperiencePoints += 50;
            aiState.Morale = Math.Min(100, aiState.Morale + 15);

            if (job.IsIllegal)
                entity.Loyalty = Math.Max(0, entity.Loyalty - 5);
            else
                entity.Loyalty = Math.Min(100, entity.Loyalty + 10);

            UpdateSkillsFromJobCompletion(entity, job);
        }
        else
        {
            aiState.Morale = Math.Max(0, aiState.Morale - 20);
            entity.Loyalty = Math.Max(0, entity.Loyalty - 15);
        }
    }

    private void UpdateSkillsFromJobCompletion(
        WorldEntityData entity,
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

        if (state.SkillProgression == null)
            state.SkillProgression = new Dictionary<string, int>();

        if (state.CompletedJobs == null)
            state.CompletedJobs = new List<string>();

        if (state.CurrentTaskParameters == null)
            state.CurrentTaskParameters = new Dictionary<string, object>();

        if (state.FactionOpinions == null)
            state.FactionOpinions = new Dictionary<string, int>();

        if (state.InteractionMemories == null)
            state.InteractionMemories = new List<string>();

        state.Morale = Clamp(state.Morale, 0, 100);
        state.Fatigue = Clamp(state.Fatigue, 0, 100);
        state.Hunger = Clamp(state.Hunger, 0, 100);
        state.Thirst = Clamp(state.Thirst, 0, 100);

        _aiStates[entityId] = state;
    }

    private CompanionAIState EnsureAIState(string entityId)
    {
        CompanionAIState state;

        if (!_aiStates.TryGetValue(entityId, out state) || state == null)
        {
            state = new CompanionAIState
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

            _aiStates[entityId] = state;
        }

        if (state.SkillProgression == null)
            state.SkillProgression = new Dictionary<string, int>();

        if (state.CompletedJobs == null)
            state.CompletedJobs = new List<string>();

        if (state.CurrentTaskParameters == null)
            state.CurrentTaskParameters = new Dictionary<string, object>();

        if (state.FactionOpinions == null)
            state.FactionOpinions = new Dictionary<string, int>();

        if (state.InteractionMemories == null)
            state.InteractionMemories = new List<string>();

        return state;
    }

    private static int Clamp(int value, int min, int max)
    {
        return value < min ? min : value > max ? max : value;
    }

    private class NeedsClock
    {
        public DateTime LastUpdateUtc { get; set; }
        public double HungerSeconds { get; set; }
        public double ThirstSeconds { get; set; }
        public double FatigueSeconds { get; set; }
        public double MoraleSeconds { get; set; }

        public NeedsClock()
        {
            LastUpdateUtc = DateTime.UtcNow;
        }
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