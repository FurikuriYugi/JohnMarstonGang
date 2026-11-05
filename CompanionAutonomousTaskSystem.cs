using RDR2;
using System;
using System.Collections.Generic;

public class CompanionAutonomousTaskSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly Dictionary<string, CompanionTaskData> _activeTasks =
        new Dictionary<string, CompanionTaskData>();

    public CompanionAutonomousTaskSystem(
        WorldEntityRegistry registry,
        BankSystem bankSystem)
    {
        _registry = registry;
    }

    public void AssignTask(
        string companionId,
        string taskType,
        Dictionary<string, object> parameters)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            !string.Equals(
                taskType,
                "RestAtCamp",
                StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        WorldEntityData entity = _registry.GetEntity(companionId);
        if (entity == null ||
            !entity.IsAlive ||
            entity.IsFollowingPlayer)
        {
            return;
        }

        _activeTasks[companionId] = new CompanionTaskData
        {
            CompanionId = companionId,
            TaskType = "RestAtCamp",
            Parameters = parameters ??
                new Dictionary<string, object>(),
            StartTimeUtc = DateTime.UtcNow,
            LastUpdateUtc = DateTime.UtcNow,
            IsActive = true
        };
    }

    public bool HasActiveTask(
        string companionId,
        string taskType)
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

        WorldEntityData entity = _registry.GetEntity(companionId);
        if (entity == null ||
            !entity.IsAlive ||
            entity.IsFollowingPlayer)
        {
            _activeTasks.Remove(companionId);
            aiState.ResetCurrentTask();
            return false;
        }

        if ((DateTime.UtcNow - task.StartTimeUtc).TotalMinutes >= 30)
        {
            _activeTasks.Remove(companionId);
            task.IsActive = false;
            aiState.ResetCurrentTask();
            aiState.CurrentGoal = "Rest interrupted.";
            return false;
        }

        if (!string.Equals(
                task.TaskType,
                "RestAtCamp",
                StringComparison.OrdinalIgnoreCase))
        {
            _activeTasks.Remove(companionId);
            task.IsActive = false;
            aiState.ResetCurrentTask();
            aiState.CurrentGoal =
                "This task has no supported in-game action.";
            return false;
        }

        if (context != null && context.IsPlayerInCombat)
        {
            _activeTasks.Remove(companionId);
            task.IsActive = false;
            aiState.ResetCurrentTask();
            aiState.CurrentGoal = "Rest interrupted by combat.";
            return false;
        }

        DateTime now = DateTime.UtcNow;
        double elapsedSeconds =
            (now - task.LastUpdateUtc).TotalSeconds;

        if (elapsedSeconds < 0)
            elapsedSeconds = 0;
        if (elapsedSeconds > 5)
            elapsedSeconds = 5;

        task.LastUpdateUtc = now;
        task.RestProgressSeconds += elapsedSeconds;

        // This changes only the companion AI fatigue value.
        // It does not heal the Ped or grant inventory items.
        while (task.RestProgressSeconds >= 30)
        {
            task.RestProgressSeconds -= 30;
            aiState.Fatigue = Math.Max(0, aiState.Fatigue - 1);
        }

        if (aiState.Fatigue <= 20)
        {
            _activeTasks.Remove(companionId);
            task.IsActive = false;
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
    /// Returns -1 because this project currently has no verified API call
    /// for reading a companion Ped's actual ammunition reserve.
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
        public DateTime LastUpdateUtc { get; set; }
        public double RestProgressSeconds { get; set; }
        public bool IsActive { get; set; }
    }
}