using GTA;
using GTA.Math;
using RDR2;
using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionAutonomousTaskSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly Dictionary<string, CompanionTask> _activeTasks = new Dictionary<string, CompanionTask>();
    private readonly Random _random = new Random();

    private const float SHOP_SEARCH_RADIUS = 150f;
    private const float LOOT_SEARCH_RADIUS = 50f;
    private const int TASK_UPDATE_INTERVAL = 10;

    public CompanionAutonomousTaskSystem(WorldEntityRegistry registry, BankSystem bankSystem)
    {
        _registry = registry;
        _bankSystem = bankSystem;
    }

    public void AssignTask(string companionId, string taskType, Dictionary<string, object> parameters)
    {
        _activeTasks[companionId] = new CompanionTask
        {
            CompanionId = companionId,
            TaskType = taskType,
            Parameters = parameters,
            StartTime = Environment.TickCount,
            IsActive = true,
            Progress = 0f
        };
    }

    public bool UpdateCompanionTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionContext context)
    {
        if (!_activeTasks.ContainsKey(companionId))
            return false;

        CompanionTask task = _activeTasks[companionId];

        if (!task.IsActive || companionPed == null || !companionPed.Exists())
        {
            _activeTasks.Remove(companionId);
            return false;
        }

        int elapsedTime = Environment.TickCount - task.StartTime;
        if (elapsedTime > 60000) // 60 second timeout
        {
            _activeTasks.Remove(companionId);
            aiState.CurrentGoal = null;
            return false;
        }

        switch (task.TaskType)
        {
            case "Heal":
                return ExecuteHealTask(companionId, companionPed, aiState, task);
            case "FindFood":
                return ExecuteFindFoodTask(companionId, companionPed, aiState, task, context);
            case "FindAmmo":
                return ExecuteFindAmmoTask(companionId, companionPed, aiState, task, context);
            case "LootNearby":
                return ExecuteLootNearbyTask(companionId, companionPed, aiState, task, context);
            case "Rest":
                return ExecuteRestTask(companionId, companionPed, aiState, task);
            case "Invest":
                return ExecuteInvestTask(companionId, companionPed, aiState, task);
            default:
                return false;
        }
    }

    private bool ExecuteHealTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task)
    {
        if (!task.Parameters.ContainsKey("targetHealth"))
            return false;

        int targetHealth = (int)task.Parameters["targetHealth"];

        // Find healing items in inventory or search for them
        if (companionPed.Health >= targetHealth * 0.95f)
        {
            task.IsActive = false;
            aiState.CurrentGoal = null;
            _activeTasks.Remove(companionId);
            aiState.Hunger += 10;
            aiState.Thirst += 10;
            return false;
        }

        // Use healing items if available (simplified - assumes companion carries items)
        companionPed.Health = Math.Min(targetHealth, companionPed.Health + 5);
        task.Progress += 0.05f;

        return true;
    }

    private bool ExecuteFindFoodTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task, CompanionContext context)
    {
        float targetRadius = task.Parameters.ContainsKey("targetNearbyRadius") ? (float)task.Parameters["targetNearbyRadius"] : 100f;
        decimal maxPrice = task.Parameters.ContainsKey("maxPrice") ? (decimal)task.Parameters["maxPrice"] : 50m;

        if (aiState.Hunger < 20)
        {
            task.IsActive = false;
            aiState.CurrentGoal = null;
            aiState.Hunger = 0;
            aiState.Thirst = 0;
            _activeTasks.Remove(companionId);
            aiState.Morale = Math.Min(100, aiState.Morale + 5);
            return false;
        }

        // Simulate searching for food vendors/restaurants
        // In a real implementation, this would pathfind to nearby shops
        Vector3 companionPos = companionPed.Position;

        // Find nearby shops (simplified - would need actual shop data from RDR2)
        // For now, simulate the action
        task.Progress += 0.02f;

        // Companion eats if it finds food
        if (task.Progress > 0.5f)
        {
            decimal companionFunds = _bankSystem.GetBalance(companionId).Cash;
            if (companionFunds >= maxPrice)
            {
                // Deduct cost and reduce hunger
                _bankSystem.Withdraw(companionId, maxPrice, 0m);
                aiState.Hunger = Math.Max(0, aiState.Hunger - 40);
                aiState.Thirst = Math.Max(0, aiState.Thirst - 20);
                aiState.Morale = Math.Min(100, aiState.Morale + 3);

                task.IsActive = false;
                aiState.CurrentGoal = null;
                _activeTasks.Remove(companionId);
                return false;
            }
        }

        return true;
    }

    private bool ExecuteFindAmmoTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task, CompanionContext context)
    {
        int targetAmmoCount = task.Parameters.ContainsKey("targetAmmoCount") ? (int)task.Parameters["targetAmmoCount"] : 30;
        decimal maxPrice = task.Parameters.ContainsKey("maxPrice") ? (decimal)task.Parameters["maxPrice"] : 50m;

        int currentAmmo = GetCompanionAmmoCount(companionPed);
        if (currentAmmo >= targetAmmoCount)
        {
            task.IsActive = false;
            aiState.CurrentGoal = null;
            _activeTasks.Remove(companionId);
            return false;
        }

        // Simulate searching for ammo vendors
        task.Progress += 0.03f;

        if (task.Progress > 0.6f)
        {
            decimal companionFunds = _bankSystem.GetBalance(companionId).Cash;
            if (companionFunds >= maxPrice)
            {
                // Buy ammo and update weapon
                _bankSystem.Withdraw(companionId, maxPrice, 0m);

                // Add ammo to companion's current weapon
                Weapon companionWeapon = companionPed.Weapons.Current;
                if (companionWeapon != null)
                {
                    companionWeapon.Ammo = Math.Min(companionWeapon.MaxAmmo, companionWeapon.Ammo + 30);
                }

                aiState.ExperiencePoints += 10;
                task.IsActive = false;
                aiState.CurrentGoal = null;
                _activeTasks.Remove(companionId);
                return false;
            }
        }

        return true;
    }

    private bool ExecuteLootNearbyTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task, CompanionContext context)
    {
        float searchRadius = task.Parameters.ContainsKey("searchRadius") ? (float)task.Parameters["searchRadius"] : 50f;
        bool prioritizeHigh = task.Parameters.ContainsKey("prioritizeHigh") && (bool)task.Parameters["prioritizeHigh"];

        if (task.Progress > 0.7f)
        {
            task.IsActive = false;
            aiState.CurrentGoal = null;
            _activeTasks.Remove(companionId);
            aiState.ExperiencePoints += 5;
            return false;
        }

        // Simulate looting nearby items
        // In real implementation, would search for actual item pickups in world
        Vector3 companionPos = companionPed.Position;

        // Simulate finding and picking up items
        task.Progress += 0.05f;

        // Random chance to find something valuable
        if (task.Progress > 0.4f && _random.Next(0, 100) < 40)
        {
            decimal lootValue = _random.Next(10, 50);
            _bankSystem.Deposit(companionId, lootValue, 0m);
            aiState.ExperiencePoints += 5;
        }

        return true;
    }

    private bool ExecuteRestTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task)
    {
        if (aiState.Fatigue < 10)
        {
            task.IsActive = false;
            aiState.CurrentGoal = "Rested and ready";
            _activeTasks.Remove(companionId);
            return false;
        }

        // Reduce fatigue while resting
        aiState.Fatigue = Math.Max(0, aiState.Fatigue - 5);
        task.Progress += 0.1f;

        return true;
    }

    private bool ExecuteInvestTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionTask task)
    {
        decimal investAmount = task.Parameters.ContainsKey("investAmount") ? (decimal)task.Parameters["investAmount"] : 100m;
        string investType = task.Parameters.ContainsKey("investType") ? (string)task.Parameters["investType"] : "Gang";

        AccountBalance balance = _bankSystem.GetBalance(companionId);
        if (balance.Cash < investAmount)
        {
            task.IsActive = false;
            aiState.CurrentGoal = "Lacks funds to invest";
            _activeTasks.Remove(companionId);
            return false;
        }

        // Withdraw investment amount
        _bankSystem.Withdraw(companionId, investAmount, 0m);

        // Add investment record
        if (!aiState.SkillProgression.ContainsKey("Investments"))
            aiState.SkillProgression["Investments"] = 0;

        aiState.SkillProgression["Investments"] += 1;
        aiState.Morale = Math.Min(100, aiState.Morale + 10);
        aiState.ExperiencePoints += 30;

        task.IsActive = false;
        aiState.CurrentGoal = $"Invested {investAmount} in {investType}";
        _activeTasks.Remove(companionId);

        return false;
    }

    public int GetCompanionAmmoCount(Ped companionPed)
    {
        if (companionPed == null || !companionPed.Exists())
            return 0;

        try
        {
            Weapon currentWeapon = companionPed.Weapons.Current;
            if (currentWeapon != null)
            {
                return currentWeapon.Ammo;
            }

            int totalAmmo = 0;
            foreach (Weapon weapon in companionPed.Weapons)
            {
                totalAmmo += weapon.Ammo;
            }
            return totalAmmo;
        }
        catch
        {
            return 0;
        }
    }

    public void CancelTask(string companionId)
    {
        if (_activeTasks.ContainsKey(companionId))
        {
            _activeTasks.Remove(companionId);
        }
    }

    public bool HasActiveTask(string companionId)
    {
        return _activeTasks.ContainsKey(companionId) && _activeTasks[companionId].IsActive;
    }

    public CompanionTask GetActiveTask(string companionId)
    {
        if (_activeTasks.ContainsKey(companionId))
            return _activeTasks[companionId];
        return null;
    }

    public Dictionary<string, CompanionTask> GetAllActiveTasks()
    {
        return new Dictionary<string, CompanionTask>(_activeTasks);
    }

    public void ClearCompletedTasks()
    {
        List<string> completedIds = _activeTasks
            .Where(x => !x.Value.IsActive)
            .Select(x => x.Key)
            .ToList();

        foreach (string id in completedIds)
        {
            _activeTasks.Remove(id);
        }
    }
}

public class CompanionTask
{
    public string CompanionId { get; set; }
    public string TaskType { get; set; }
    public Dictionary<string, object> Parameters { get; set; }
    public int StartTime { get; set; }
    public bool IsActive { get; set; }
    public float Progress { get; set; }
}

public class CompanionAIState
{
    public string EntityId { get; set; }
    public string Personality { get; set; }
    public int Morale { get; set; }
    public int Fatigue { get; set; }
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    public bool PrefersStealth { get; set; }
    public string CurrentGoal { get; set; }
    public int GoalTimer { get; set; }
    public int ExperiencePoints { get; set; }
    public string LastCombatStyle { get; set; }
    public Dictionary<string, int> SkillProgression { get; set; }
    public List<string> CompletedJobs { get; set; }
    public decimal TotalEarnings { get; set; }
    public int BanksVisited { get; set; }
    public int DepositsWithdrawn { get; set; }

    [JsonConstructor]
    public CompanionAIState() { }

    public CompanionAIState(string entityId)
    {
        EntityId = entityId;
        Personality = "Neutral";
        Morale = 50;
        Fatigue = 0;
        Hunger = 0;
        Thirst = 0;
        PrefersStealth = false;
        CurrentGoal = null;
        GoalTimer = 0;
        ExperiencePoints = 0;
        LastCombatStyle = "Ranged Medium";
        SkillProgression = new Dictionary<string, int>();
        CompletedJobs = new List<string>();
        TotalEarnings = 0m;
        BanksVisited = 0;
        DepositsWithdrawn = 0;
    }
}