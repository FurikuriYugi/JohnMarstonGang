using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionSystem
{
    private readonly List<CompanionRecord> _companions = new List<CompanionRecord>();

    public IReadOnlyList<CompanionRecord> Companions => _companions;

    public CompanionRecord CreateCompanion(string name)
    {
        var existing = GetCompanion(name);
        if (existing != null)
            return existing;

        var companion = new CompanionRecord
        {
            CompanionId = Guid.NewGuid().ToString("N"),
            Name = name,
            Type = CompanionType.Human,
            Personality = "Neutral",
            Loyalty = 0,
            SkillCombat = 0,
            SkillStealth = 0,
            SkillHandling = 0,
            SkillTrading = 0,
            WeaponChoice = null,
            Outfit = null,
            PersonalCash = 0m,
            PersonalGold = 0m,
            Memories = new List<string>(),
            Goals = new List<string>()
        };

        _companions.Add(companion);
        return companion;
    }

    public CompanionRecord GetCompanion(string name)
    {
        return _companions.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public void Update(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        foreach (var member in gangManager.Members)
        {
            var companion = GetCompanion(member.Name);
            if (companion == null)
                continue;

            companion.Loyalty = member.Loyalty;
            companion.SkillCombat = member.SkillCombat;
            companion.SkillStealth = member.SkillStealth;
            companion.SkillHandling = member.SkillHandling;
            companion.SkillTrading = member.SkillTrading;
            companion.WeaponChoice = member.WeaponChoice;
            companion.Outfit = member.Outfit;
            companion.PersonalCash = member.PersonalCash;
            companion.PersonalGold = member.PersonalGold;
            companion.Memories = new List<string>(member.Memories);
            companion.Goals = new List<string>(member.Goals);
        }
    }

    public List<CompanionSaveData> CreateSaveData()
    {
        var result = new List<CompanionSaveData>();

        foreach (var companion in _companions)
        {
            result.Add(new CompanionSaveData
            {
                CompanionId = companion.CompanionId,
                Name = companion.Name,
                Type = companion.Type,
                Personality = companion.Personality,
                Loyalty = companion.Loyalty,
                SkillCombat = companion.SkillCombat,
                SkillStealth = companion.SkillStealth,
                SkillHandling = companion.SkillHandling,
                SkillTrading = companion.SkillTrading,
                WeaponChoice = companion.WeaponChoice,
                Outfit = companion.Outfit,
                PersonalCash = companion.PersonalCash,
                PersonalGold = companion.PersonalGold,
                Memories = companion.Memories != null ? new List<string>(companion.Memories) : new List<string>(),
                Goals = companion.Goals != null ? new List<string>(companion.Goals) : new List<string>()
            });
        }

        return result;
    }

    public void LoadFromSave(List<CompanionSaveData> savedCompanions)
    {
        _companions.Clear();

        if (savedCompanions == null)
            return;

        foreach (var saved in savedCompanions)
        {
            _companions.Add(new CompanionRecord
            {
                CompanionId = saved.CompanionId,
                Name = saved.Name,
                Type = saved.Type,
                Personality = saved.Personality,
                Loyalty = saved.Loyalty,
                SkillCombat = saved.SkillCombat,
                SkillStealth = saved.SkillStealth,
                SkillHandling = saved.SkillHandling,
                SkillTrading = saved.SkillTrading,
                WeaponChoice = saved.WeaponChoice,
                Outfit = saved.Outfit,
                PersonalCash = saved.PersonalCash,
                PersonalGold = saved.PersonalGold,
                Memories = saved.Memories ?? new List<string>(),
                Goals = saved.Goals ?? new List<string>()
            });
        }
    }
}

public class CompanionRecord
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public string Personality { get; set; }
    public int Loyalty { get; set; }
    public int SkillCombat { get; set; }
    public int SkillStealth { get; set; }
    public int SkillHandling { get; set; }
    public int SkillTrading { get; set; }
    public string WeaponChoice { get; set; }
    public string Outfit { get; set; }
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
    public List<string> Memories { get; set; }
    public List<string> Goals { get; set; }
}