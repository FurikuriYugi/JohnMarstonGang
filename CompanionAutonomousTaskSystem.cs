using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionAutonomousTaskSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly Dictionary<string, CompanionTaskData> _activeTasks = new Dictionary<string, CompanionTaskData>();
    private readonly Random _random = new Random();

    private const float TASK_UPDATE_INTERVAL = 1f;
    private const float SHOP_DISTANCE_THRESHOLD = 100f;
    private const float BANK_DISTANCE_THRESHOLD = 150f;

    public CompanionAutonomousTaskSystem(WorldEntityRegistry registry, BankSystem bankSystem)
    {
        _registry = registry;
        _bankSystem = bankSystem;
    }

    public void AssignTask(string companionId, string taskType, Dictionary<string, object> parameters)
    {
        if (string.IsNullOrWhiteSpace(companionId) || string.IsNullOrWhiteSpace(taskType))
            return;

        _activeTasks[companionId] = new CompanionTaskData
        {
            CompanionId = companionId,
            TaskType = taskType,
            Parameters = parameters ?? new Dictionary<string, object>(),
            StartTime = DateTime.UtcNow,
            IsActive = true
        };
    }

    public bool UpdateCompanionTask(string companionId, Ped companionPed, CompanionAIState aiState, CompanionContext context)
    {
        if (string.IsNullOrWhiteSpace(companionId) || companionPed == null || !companionPed.Exists())
            return false;

        if (!_activeTasks.ContainsKey(companionId))
            return false;

        CompanionTaskData task = _activeTasks[companionId];

        if (!task.IsActive)
        {
            _activeTasks.Remove(companionId);
            return false;
        }

        // Check if task has timed out (default 5 minutes)
        TimeSpan elapsed = DateTime.UtcNow - task.StartTime;
        if (elapsed.TotalSeconds > 300)
        {
            _activeTasks.Remove(companionId);
            return false;
        }

        switch (task.TaskType)
        {
            case "Heal":
                return ExecuteHealTask(companionPed, task, aiState);
            case "FindFood":
                return ExecuteFindFoodTask(companionPed, task, aiState, context);
            case "FindAmmo":
                return ExecuteFindAmmoTask(companionPed, task, aiState, context);
            case "LootNearby":
                return ExecuteLootTask(companionPed, task, aiState, context);
            case "RestAtCamp":
                return ExecuteRestTask(companionPed, task, aiState);
            case "Visit Bank":
                return ExecuteVisitBankTask(companionPed, task, aiState, companionId);
            default:
                return false;
        }
    }

    private bool ExecuteHealTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState)
    {
        if (companionPed == null || !companionPed.Exists())
            return false;

        int targetHealth = (int)task.GetParameter("targetHealth", companionPed.MaxHealth);

        // If health is sufficient, mark task complete
        if (companionPed.Health >= targetHealth)
        {
            task.IsActive = false;
            aiState.Hunger += 10; // Eating makes you hungry after
            return false;
        }

        // Healing logic - apply first aid or consumables
        // This would integrate with RDR2 healing items
        // For now, gradually recover
        companionPed.Health = Math.Min(targetHealth, companionPed.Health + 5);
        aiState.CurrentGoal = $"Healing... {companionPed.Health}/{targetHealth}";

        return true;
    }

    private bool ExecuteFindFoodTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState, CompanionContext context)
    {
        if (companionPed == null || !companionPed.Exists())
            return false;

        if (!task.Parameters.ContainsKey("targetNearbyRadius"))
            task.Parameters["targetNearbyRadius"] = 100f;

        if (!task.Parameters.ContainsKey("maxPrice"))
            task.Parameters["maxPrice"] = 50m;

        // Companion moves toward nearby shops or camp
        // For now, simulate food consumption
        if (_random.Next(0, 100) < 30)
        {
            aiState.Hunger = Math.Max(0, aiState.Hunger - 30);
            task.IsActive = false;
            aiState.CurrentGoal = "Ate food";
            return false;
        }

        aiState.CurrentGoal = "Looking for food...";
        return true;
    }

    private bool ExecuteFindAmmoTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState, CompanionContext context)
    {
        if (companionPed == null || !companionPed.Exists())
            return false;

        int targetAmmoCount = (int)task.GetParameter("targetAmmoCount", 30);
        int currentAmmo = GetCompanionAmmoCount(companionPed);

        if (currentAmmo >= targetAmmoCount)
        {
            task.IsActive = false;
            return false;
        }

        // Simulate ammo restocking
        if (_random.Next(0, 100) < 25)
        {
            int ammoGained = _random.Next(5, 15);
            aiState.CurrentGoal = $"Restocked {ammoGained} ammo";
            task.IsActive = false;
            return false;
        }

        aiState.CurrentGoal = "Searching for ammunition...";
        return true;
    }

    private bool ExecuteLootTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState, CompanionContext context)
    {
        if (companionPed == null || !companionPed.Exists())
            return false;

        float searchRadius = (float)task.GetParameter("searchRadius", 50f);

        // Simulate looting nearby items
        if (_random.Next(0, 100) < 35)
        {
            string item = GenerateRandomLoot();
            _registry.GiveItem(companionPed.Handle.ToString(), item);
            aiState.CurrentGoal = $"Found {item}";
            task.IsActive = false;
            return false;
        }

        aiState.CurrentGoal = "Scavenging nearby...";
        return true;
    }

    private bool ExecuteRestTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState)
    {
        if (companionPed == null || !companionPed.Exists())
            return false;

        // Reduce fatigue while resting
        aiState.Fatigue = Math.Max(0, aiState.Fatigue - 5);
        aiState.Hunger = Math.Min(100, aiState.Hunger + 2); // Get hungry while resting

        if (aiState.Fatigue <= 20)
        {
            task.IsActive = false;
            aiState.CurrentGoal = "Rested";
            return false;
        }

        aiState.CurrentGoal = "Resting...";
        return true;
    }

    private bool ExecuteVisitBankTask(Ped companionPed, CompanionTaskData task, CompanionAIState aiState, string companionId)
    {
        if (companionPed == null || !companionPed.Exists() || string.IsNullOrWhiteSpace(companionId))
            return false;

        bool isDepositing = (bool)task.GetParameter("isDepositing", true);
        decimal amount = (decimal)task.GetParameter("amount", 0m);
        string assetType = (string)task.GetParameter("assetType", "Cash");

        // Ensure bank account exists
        BankAccountData account = _bankSystem.EnsureAccount(companionId, companionPed.GetShorthandName());

        if (isDepositing)
        {
            if (assetType == "Cash")
                _bankSystem.DepositCash(companionId, amount);
            else if (assetType == "Gold")
                _bankSystem.DepositGold(companionId, amount);

            aiState.BanksVisited++;
            aiState.RecordMemory($"Deposited {amount} {assetType} to bank");
        }
        else
        {
            if (assetType == "Cash")
            {
                if (_bankSystem.WithdrawCash(companionId, amount))
                {
                    aiState.DepositsWithdrawn++;
                    aiState.RecordMemory($"Withdrew {amount} {assetType} from bank");
                }
            }
            else if (assetType == "Gold")
            {
                if (_bankSystem.WithdrawGold(companionId, amount))
                {
                    aiState.DepositsWithdrawn++;
                    aiState.RecordMemory($"Withdrew {amount} {assetType} from bank");
                }
            }
        }

        task.IsActive = false;
        aiState.CurrentGoal = $"{(isDepositing ? "Deposited" : "Withdrew")} funds at bank";

        return false;
    }

    public int GetCompanionAmmoCount(Ped companionPed)
    {
        if (companionPed == null || !companionPed.Exists())
            return 0;

        // This would query the actual ammo from the Ped's weapons
        // For now, return a simulated value
        int totalAmmo = 0;

        // Get current weapon
        uint currentWeapon = companionPed.Weapons.Current;
        if (currentWeapon != 0)
        {
            // Count ammo in current weapon (would use RDR2 API)
            totalAmmo += _random.Next(10, 50);
        }

        return totalAmmo;
    }

    private string GenerateRandomLoot()
    {
        int roll = _random.Next(0, 100);

        if (roll < 30)
            return "Bullet";
        else if (roll < 50)
            return "Consumable";
        else if (roll < 75)
            return "Food";
        else if (roll < 90)
            return "Medicine";
        else
            return "Valuables";
    }

    private class CompanionTaskData
    {
        public string CompanionId { get; set; }
        public string TaskType { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; }

        public object GetParameter(string key, object defaultValue)
        {
            return Parameters.ContainsKey(key) ? Parameters[key] : defaultValue;
        }
    }
}