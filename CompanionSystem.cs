using System.Collections.Generic;
using Newtonsoft.Json;

public class CompanionSystem
{
    private readonly List<CompanionData> _companions = new List<CompanionData>();

    public List<CompanionData> GetCompanions()
    {
        return new List<CompanionData>(_companions);
    }

    public CompanionData GetCompanion(string companionId)
    {
        if (string.IsNullOrWhiteSpace(companionId))
            return null;

        return _companions.Find(x => x != null && x.CompanionId == companionId);
    }

    public void RegisterCompanion(CompanionData companion)
    {
        if (companion == null || string.IsNullOrWhiteSpace(companion.CompanionId))
            return;

        _companions.RemoveAll(x => x != null && x.CompanionId == companion.CompanionId);
        _companions.Add(companion);
    }

    public void RemoveCompanion(string companionId)
    {
        _companions.RemoveAll(x => x != null && x.CompanionId == companionId);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_companions, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _companions.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<CompanionData> companions = JsonConvert.DeserializeObject<List<CompanionData>>(json);
        if (companions != null)
            _companions.AddRange(companions);
    }
}