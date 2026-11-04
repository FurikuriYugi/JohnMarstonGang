using System.Collections.Generic;
using Newtonsoft.Json;

public class CompanionSystem
{
    private readonly Dictionary<string, CompanionRecord> _companions = new Dictionary<string, CompanionRecord>();

    public void RegisterCompanion(CompanionRecord companion)
    {
        if (companion == null || string.IsNullOrWhiteSpace(companion.CompanionId))
            return;

        _companions[companion.CompanionId] = companion;
    }

    public bool SetCompanionGoal(string companionId, string goal)
    {
        if (string.IsNullOrWhiteSpace(companionId) || !_companions.ContainsKey(companionId))
            return false;

        _companions[companionId].CurrentGoal = goal;
        return true;
    }

    public List<CompanionRecord> GetAllCompanions()
    {
        return new List<CompanionRecord>(_companions.Values);
    }

    public void LoadCompanions(List<CompanionRecord> companions)
    {
        _companions.Clear();

        if (companions == null)
            return;

        foreach (CompanionRecord companion in companions)
        {
            if (companion != null && !string.IsNullOrWhiteSpace(companion.CompanionId))
                _companions[companion.CompanionId] = companion;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_companions.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _companions.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<CompanionRecord> companions = JsonConvert.DeserializeObject<List<CompanionRecord>>(json);
        if (companions == null)
            return;

        foreach (CompanionRecord companion in companions)
        {
            if (companion != null && !string.IsNullOrWhiteSpace(companion.CompanionId))
                _companions[companion.CompanionId] = companion;
        }
    }
}

public class CompanionRecord
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string CurrentGoal { get; set; }
}