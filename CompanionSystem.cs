using System.Collections.Generic;
using Newtonsoft.Json;

public class CompanionSystem
{
    private readonly Dictionary<string, CompanionData> _companions = new Dictionary<string, CompanionData>();

    public void RegisterCompanion(CompanionData companion)
    {
        if (companion == null || string.IsNullOrWhiteSpace(companion.CompanionId))
            return;

        _companions[companion.CompanionId] = companion;
    }

    public bool HasCompanion(string companionId)
    {
        return !string.IsNullOrWhiteSpace(companionId) && _companions.ContainsKey(companionId);
    }

    public CompanionData GetCompanion(string companionId)
    {
        if (string.IsNullOrWhiteSpace(companionId))
            return null;

        _companions.TryGetValue(companionId, out CompanionData companion);
        return companion;
    }

    public List<CompanionData> GetAllCompanions()
    {
        return new List<CompanionData>(_companions.Values);
    }

    public void LoadCompanions(List<CompanionData> companions)
    {
        _companions.Clear();

        if (companions == null)
            return;

        foreach (CompanionData companion in companions)
        {
            if (companion != null && !string.IsNullOrWhiteSpace(companion.CompanionId))
                _companions[companion.CompanionId] = companion;
        }
    }

    public void AddMemory(string companionId, string memoryTag)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null || string.IsNullOrWhiteSpace(memoryTag))
            return;

        if (companion.MemoryTags == null)
            companion.MemoryTags = new List<string>();

        if (!companion.MemoryTags.Contains(memoryTag))
            companion.MemoryTags.Add(memoryTag);
    }

    public void AddGoal(string companionId, string goal)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null || string.IsNullOrWhiteSpace(goal))
            return;

        if (companion.Goals == null)
            companion.Goals = new List<string>();

        if (!companion.Goals.Contains(goal))
            companion.Goals.Add(goal);
    }

    public void AddItem(string companionId, string item)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null || string.IsNullOrWhiteSpace(item))
            return;

        if (companion.Inventory == null)
            companion.Inventory = new List<string>();

        companion.Inventory.Add(item);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllCompanions(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _companions.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<CompanionData> companions = JsonConvert.DeserializeObject<List<CompanionData>>(json);
        LoadCompanions(companions);
    }
}

public class CompanionData
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Role { get; set; }
    public string CombatStyle { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Skills { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public List<string> Inventory { get; set; } = new List<string>();
}