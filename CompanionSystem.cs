using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CompanionSystem
{
    private readonly string _saveFileName;
    private List<CompanionRecord> _companions = new List<CompanionRecord>();

    public CompanionSystem(string saveFileName = "CompanionSystem.json")
    {
        _saveFileName = saveFileName;
        Load();
    }

    public void Update(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        foreach (var member in gangManager.GetActiveMembers())
        {
            if (member == null || string.IsNullOrWhiteSpace(member.Name))
                continue;

            var companion = GetOrCreate(member.Name);
            companion.Loyalty = member.Loyalty;
            companion.Experience += member.IsInCombat ? 1 : 0;
            companion.PreferredStyle = member.PreferredCombatStyle;
            companion.LastKnownMood = member.Mood.ToString();
        }

        Save();
    }

    public CompanionRecord GetOrCreate(string name)
    {
        var companion = _companions.FirstOrDefault(x => x.Name == name);
        if (companion != null)
            return companion;

        companion = new CompanionRecord
        {
            Name = name,
            Loyalty = 50,
            Experience = 0,
            PreferredStyle = CombatStyle.ShortRange,
            LastKnownMood = "Neutral"
        };

        _companions.Add(companion);
        return companion;
    }

    public void Save()
    {
        File.WriteAllText(_saveFileName, JsonConvert.SerializeObject(_companions, Formatting.Indented));
    }

    public void Load()
    {
        if (!File.Exists(_saveFileName))
        {
            _companions = new List<CompanionRecord>();
            return;
        }

        var json = File.ReadAllText(_saveFileName);
        _companions = JsonConvert.DeserializeObject<List<CompanionRecord>>(json) ?? new List<CompanionRecord>();
    }
}