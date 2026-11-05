using RDR2;
using System;
using System.Collections.Generic;

public class CompanionAutonomousTaskSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly Dictionary<string, CompanionTaskData> _activeTasks =
        new Dictionary<string, CompanionTaskData>();

    private static readonly HashSet<string> SupportedTaskTypes =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "RestAtCamp"
        };

    public CompanionAutonomousTaskSystem(
        WorldEntityRegistry registry,
        BankSystem bankSystem)
    {
        _registry = registry;
        _bankSystem = bankSystem;
    }

    public void AssignTask(
        string companionId,
        string taskType,
        Dictionary<string, object> parameters)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            string.IsNullOrWhiteSpace(taskType))
        {
            return;
        }

        // Do not create task records that would invent items, money,
        // healing, or purchases the game did not actually provide.
        if (!SupportedTaskTypes.Contains(taskType))
            return;

        _activeTasks[companionId] = new CompanionTaskData
        {
            CompanionId = companionId,
            TaskType = taskType,
            Parameters = parameters ??
                new Dictionary<string, object>(),
            StartTimeUtc = DateTime.UtcNow,
            LastProgressUtc = DateTime.UtcNow,
            IsActive = true
        };
    }

    public bool HasActiveTask(string companionId, string taskType)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            string.IsNullOrWhiteSpace(taskType))
        {
            return false;
        }

        CompanionTaskData task;
        return _activeTasks.TryGetValue(companionId, out task) &&
            task != null &&
            task.IsActive &&
            string.Equals(
                task.TaskType,
                taskType,
                StringComparison.OrdinalIgnoreCase);
    }

    public void CancelTask(string companionId)
    {
        if (string.IsNullOrWhiteSpace(companionId))
            return;

        _activeTasks.Remove(companionId);
    }

    public bool UpdateCompanionTask(
        string companionId,
        Ped companionPed,
        CompanionAIState aiState,
        CompanionContext context)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists() ||
            aiState == null)
        {
            return false;
        }

        CompanionTaskData task;
        if (!_activeTasks.TryGetValue(companionId, out task) ||
            task == null ||
            !task.IsActive)
        {
            _activeTasks.Remove(companionId);
            return false;
        }

        if ((DateTime.UtcNow - task.StartTimeUtc).TotalMinutes >= 30)
        {
            task.IsActive = false;
            _activeTasks.Remove(companionId);
            aiState.ResetCurrentTask();
            aiState.CurrentGoal = "Rest interrupted.";
            return false;
        }

        if (string.Equals(
                task.TaskType,
                "RestAtCamp",
                StringComparison.OrdinalIgnoreCase))
        {
            return ExecuteRestTask(task, aiState, context);
        }

        // Defensive fallback: unknown or unsupported tasks never produce
        // items, change health, or alter bank balances.
        task.IsActive = false;
        _activeTasks.Remove(companionId);
        aiState.ResetCurrentTask();
        aiState.CurrentGoal =
            "This companion task has no supported in-game action.";
        return false;
    }

    private bool ExecuteRestTask(
        CompanionTaskData task,
        CompanionAIState aiState,
        CompanionContext context)
    {
        if (context != null && context.IsPlayerInCombat)
        {
            task.LastProgressUtc = DateTime.UtcNow;
            aiState.CurrentGoal = "Rest interrupted by combat.";
            return true;
        }

        DateTime now = DateTime.UtcNow;
        double elapsedSeconds =
            (now - task.LastProgressUtc).TotalSeconds;

        if (elapsedSeconds < 0)
            elapsedSeconds = 0;
        if (elapsedSeconds > 10)
            elapsedSeconds = 10;

        task.LastProgressUtc = now;
        task.RestProgressSeconds += elapsedSeconds;

        // Reduce fatigue slowly over real elapsed time, not once per AI tick.
        while (task.RestProgressSeconds >= 30)
        {
            task.RestProgressSeconds -= 30;
            aiState.Fatigue = Math.Max(0, aiState.Fatigue - 1);
        }

        if (aiState.Fatigue <= 20)
        {
            task.IsActive = false;
            _activeTasks.Remove(task.CompanionId);
            aiState.ResetCurrentTask();
            aiState.CurrentGoal = "Rested.";
            return false;
        }

        aiState.IsExecutingTask = true;
        aiState.CurrentTaskType = task.TaskType;
        aiState.CurrentGoal = "Resting.";
        return true;
    }

    /// <summary>
    /// The current project has no verified API call for reading a Ped's
    /// actual ammunition reserve. Returns -1 to mean unavailable.
    /// </summary>
    public int GetCompanionAmmoCount(Ped companionPed)
    {
        if (companionPed == null || !companionPed.Exists())
            return -1;

        return -1;
    }

    private class CompanionTaskData
    {
        public string CompanionId { get; set; }
        public string TaskType { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime LastProgressUtc { get; set; }
        public double RestProgressSeconds { get; set; }
        public bool IsActive { get; set; }
    }
}