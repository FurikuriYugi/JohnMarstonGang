// CompanionSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionSystem
{
    private readonly Dictionary<string, CompanionRecord> _records = new Dictionary<string, CompanionRecord>();

    public CompanionRecord GetOrCreateRecord(string memberId, string name, CompanionType type)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        if (_records.ContainsKey(memberId))
            return _records[memberId];

        var record = new CompanionRecord
        {
            MemberId = memberId,
            Name = name,
            Type = type,
            Trust = 50,
            Experience = 0
        };

        _records[memberId] = record;
        return record;
    }

    public void UpdateFromGangMember(GangMember member)
    {
        if (member == null)
            return;

        var record = GetOrCreateRecord(member.MemberId, member.Name, member.Type);
        if (record == null)
            return;

        record.Name = member.Name;
        record.Type = member.Type;
        record.Loyalty = member.Loyalty;
        record.PreferredCombatStyle = member.PreferredCombatStyle;
        record.CurrentMood = member.Mood;
        record.LastKnownWeapon = member.GetSuggestedWeapon();
        record.Memories = new List<string>(member.Memories);
        record.Goals = new List<string>(member.Goals);
        record.PersonalCash = member.PersonalCash;
        record.PersonalGold = member.PersonalGold;
    }

    public void ApplyPersonalGoal(GangMember member)
    {
        if (member == null)
            return;

        if (member.SkillTrading > 65)
            member.AddGoal("Find profitable trade opportunities.");
        if (member.SkillCombat > 65)
            member.AddGoal("Improve combat readiness.");
        if (member.SkillStealth > 65)
            member.AddGoal("Stay quiet and avoid detection.");
        if (member.NeedsSupplies)
            member.AddGoal("Gather supplies.");
    }

    public void GainExperience(string memberId, int amount)
    {
        if (string.IsNullOrWhiteSpace(memberId) || !_records.ContainsKey(memberId))
            return;

        _records[memberId].Experience += Math.Max(0, amount);
    }
}

public class CompanionRecord
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Experience { get; set; }
    public GangMood CurrentMood { get; set; }
    public CombatStyle PreferredCombatStyle { get; set; }
    public string LastKnownWeapon { get; set; }
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
}