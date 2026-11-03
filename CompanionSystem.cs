using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class CompanionSystem
{
    private readonly string _savePath;
    private readonly List<CompanionProfile> _profiles = new List<CompanionProfile>();

    public IReadOnlyList<CompanionProfile> Profiles => _profiles;

    public CompanionSystem(string saveFileName = "CompanionSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public CompanionProfile GetOrCreateProfile(string memberName)
    {
        if (string.IsNullOrWhiteSpace(memberName))
            memberName = "Member";

        var profile = _profiles.FirstOrDefault(x => x.MemberName == memberName);
        if (profile != null)
            return profile;

        profile = new CompanionProfile
        {
            MemberName = memberName,
            Morale = 50,
            Loyalty = 50,
            Health = 100,
            Hunger = 0,
            Fatigue = 0,
            Experience = 0,
            SkillGuns = 10,
            SkillMelee = 10,
            SkillStealth = 10,
            SkillScavenging = 10,
            CurrentGoal = "Stay with the gang"
        };

        _profiles.Add(profile);
        Save();
        return profile;
    }

    public void RegisterMember(GangMember member)
    {
        if (member == null) return;
        GetOrCreateProfile(member.Name);
    }

    public void UpdateMorale(string memberName, int delta)
    {
        var profile = GetOrCreateProfile(memberName);
        profile.Morale = Clamp(profile.Morale + delta, 0, 100);
        Save();
    }

    public void UpdateLoyalty(string memberName, int delta)
    {
        var profile = GetOrCreateProfile(memberName);
        profile.Loyalty = Clamp(profile.Loyalty + delta, 0, 100);
        Save();
    }

    public void AddExperience(string memberName, int amount)
    {
        if (amount <= 0) return;

        var profile = GetOrCreateProfile(memberName);
        profile.Experience += amount;
        Save();
    }

    public void SetGoal(string memberName, string goal)
    {
        var profile = GetOrCreateProfile(memberName);
        profile.CurrentGoal = string.IsNullOrWhiteSpace(goal) ? "Stay with the gang" : goal;
        Save();
    }

    public void AddMemory(string memberName, string memory)
    {
        if (string.IsNullOrWhiteSpace(memory)) return;

        var profile = GetOrCreateProfile(memberName);
        profile.Memories.Add(new CompanionMemory
        {
            Text = memory,
            CreatedUtc = DateTime.UtcNow
        });

        if (profile.Memories.Count > 25)
            profile.Memories.RemoveAt(0);

        Save();
    }

    public void SetNeeds(string memberName, int hunger, int fatigue)
    {
        var profile = GetOrCreateProfile(memberName);
        profile.Hunger = Clamp(hunger, 0, 100);
        profile.Fatigue = Clamp(fatigue, 0, 100);
        Save();
    }

    public bool IsReadyForCombat(string memberName)
    {
        var profile = GetOrCreateProfile(memberName);
        return profile.Health > 25 && profile.Fatigue < 85 && profile.Hunger < 85;
    }

    public CombatStyle RecommendStyle(string memberName, TacticalContext context)
    {
        var profile = GetOrCreateProfile(memberName);

        if (context == null)
            return CombatStyle.RangedMedium;

        if (context.IsStealthMode)
        {
            if (profile.SkillStealth >= 40)
                return CombatStyle.Stealth;

            return CombatStyle.NonLethalMelee;
        }

        if (context.IsCloseQuarters)
            return profile.SkillMelee >= profile.SkillGuns ? CombatStyle.LethalMelee : CombatStyle.RangedShort;

        if (context.IsLongRangeCombat)
            return CombatStyle.RangedLong;

        if (context.IsMediumRangeCombat)
            return CombatStyle.RangedMedium;

        return CombatStyle.RangedShort;
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            File.WriteAllText(_savePath, JsonConvert.SerializeObject(_profiles, Formatting.Indented));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonConvert.DeserializeObject<List<CompanionProfile>>(json);

            _profiles.Clear();
            if (loaded != null)
                _profiles.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.MemberName)));
        }
        catch
        {
            _profiles.Clear();
        }
    }

    private int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}

public class CompanionProfile
{
    public string MemberName { get; set; }
    public int Morale { get; set; }
    public int Loyalty { get; set; }
    public int Health { get; set; }
    public int Hunger { get; set; }
    public int Fatigue { get; set; }
    public int Experience { get; set; }
    public int SkillGuns { get; set; }
    public int SkillMelee { get; set; }
    public int SkillStealth { get; set; }
    public int SkillScavenging { get; set; }
    public string CurrentGoal { get; set; }
    public List<CompanionMemory> Memories { get; set; } = new List<CompanionMemory>();
}

public class CompanionMemory
{
    public string Text { get; set; }
    public DateTime CreatedUtc { get; set; }
}