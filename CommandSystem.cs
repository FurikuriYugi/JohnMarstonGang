using System.Collections.Generic;
using Newtonsoft.Json;

public class CompanionSystem
{
    private readonly List<CompanionData> _companions = new List<CompanionData>();

    public List<CompanionData> GetCompanions()
    {
        return new List<CompanionData>(_companions);
    }

    public void AddCompanion(CompanionData companion)
    {
        if (companion == null || string.IsNullOrWhiteSpace(companion.CompanionId))
            return;

        _companions.RemoveAll(c => c != null && c.CompanionId == companion.CompanionId);
        _companions.Add(companion);
    }

    public CompanionData GetCompanion(string companionId)
    {
        if (string.IsNullOrWhiteSpace(companionId))
            return null;

        return _companions.Find(c => c != null && c.CompanionId == companionId);
    }

    public void RemoveCompanion(string companionId)
    {
        _companions.RemoveAll(c => c != null && c.CompanionId == companionId);
    }

    public void SetTask(string companionId, string task)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null)
            return;

        companion.CurrentTask = task;
    }

    public void SetCombatStyle(string companionId, string combatStyle)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null)
            return;

        companion.CurrentCombatStyle = combatStyle;
    }

    public void SetFollowing(string companionId, bool following)
    {
        CompanionData companion = GetCompanion(companionId);
        if (companion == null)
            return;

        companion.IsFollowingPlayer = following;
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