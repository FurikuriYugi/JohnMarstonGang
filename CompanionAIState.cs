using System;
using System.Collections.Generic;

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
    public long LastActivityTimestamp { get; set; }
    public Dictionary<string, object> CurrentTaskParameters { get; set; }
    public bool IsExecutingTask { get; set; }
    public string CurrentTaskType { get; set; }
    public Dictionary<string, int> FactionOpinions { get; set; }
    public List<string> InteractionMemories { get; set; }

    public CompanionAIState()
    {
        SkillProgression = new Dictionary<string, int>();
        CompletedJobs = new List<string>();
        CurrentTaskParameters = new Dictionary<string, object>();
        FactionOpinions = new Dictionary<string, int>();
        InteractionMemories = new List<string>();
        LastActivityTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public void RecordMemory(string memory)
    {
        if (string.IsNullOrWhiteSpace(memory))
            return;

        InteractionMemories.Add($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {memory}");

        // Keep only last 50 memories
        if (InteractionMemories.Count > 50)
            InteractionMemories.RemoveAt(0);
    }

    public void UpdateFactionOpinion(string factionId, int opinionChange)
    {
        if (string.IsNullOrWhiteSpace(factionId))
            return;

        if (!FactionOpinions.ContainsKey(factionId))
            FactionOpinions[factionId] = 0;

        FactionOpinions[factionId] = Math.Max(-100, Math.Min(100, FactionOpinions[factionId] + opinionChange));
    }

    public int GetFactionOpinion(string factionId)
    {
        if (string.IsNullOrWhiteSpace(factionId) || !FactionOpinions.ContainsKey(factionId))
            return 0;

        return FactionOpinions[factionId];
    }

    public void ResetCurrentTask()
    {
        CurrentTaskType = null;
        CurrentTaskParameters?.Clear();
        IsExecutingTask = false;
        GoalTimer = 0;
    }

    public bool HasCompletedJob(string jobId)
    {
        return !string.IsNullOrWhiteSpace(jobId) && CompletedJobs.Contains(jobId);
    }
}