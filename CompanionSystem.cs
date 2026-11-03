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

        CompanionRecord record = new CompanionRecord
        {
            MemberId = memberId,
            Name = name,
            Type = type,
            Trust = 50,
            Experience = 0,
            Loyalty = 50,
            CurrentMood = GangMood.Neutral,
            PreferredCombatStyle = CombatStyle.ShortRange
        };

        _records[memberId] = record;
        return record;
    }

    public void UpdateFromGangMember(GangMember member)
    {
        if (member == null)
            return;

        CompanionRecord record = GetOrCreateRecord(member.MemberId, member.Name, member.Type);
        if (record == null)
            return;

        record.Name = member.Name;
        record.Type = member.Type;
        record.Loyalty = member.Loyalty;
        record.PreferredCombatStyle = member.PreferredCombatStyle;
        record.CurrentMood = member.Mood;
        record.LastKnownWeapon = member.GetSuggestedWeapon();
        record.Memories = new List<string>(member.Memories ?? new List<string>());
        record.Goals = new List<string>(member.Goals ?? new List<string>());
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

    public CompanionRecord GetRecord(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        _records.TryGetValue(memberId, out CompanionRecord record);
        return record;
    }

    public IReadOnlyCollection<CompanionRecord> GetAllRecords()
    {
        return _records.Values.ToList().AsReadOnly();
    }

    public List<CompanionRecordData> CreateSaveData()
    {
        List<CompanionRecordData> data = new List<CompanionRecordData>();

        foreach (CompanionRecord record in _records.Values)
        {
            data.Add(new CompanionRecordData
            {
                MemberId = record.MemberId,
                Name = record.Name,
                Type = record.Type.ToString(),
                Loyalty = record.Loyalty,
                Trust = record.Trust,
                Experience = record.Experience,
                CurrentMood = record.CurrentMood.ToString(),
                PreferredCombatStyle = record.PreferredCombatStyle.ToString(),
                LastKnownWeapon = record.LastKnownWeapon,
                Memories = new List<string>(record.Memories ?? new List<string>()),
                Goals = new List<string>(record.Goals ?? new List<string>()),
                PersonalCash = record.PersonalCash,
                PersonalGold = record.PersonalGold
            });
        }

        return data;
    }

    public void LoadFromSave(List<CompanionRecordData> data)
    {
        _records.Clear();

        if (data == null)
            return;

        foreach (CompanionRecordData item in data)
        {
            CompanionType type = CompanionType.Human;
            GangMood mood = GangMood.Neutral;
            CombatStyle style = CombatStyle.ShortRange;

            Enum.TryParse(item.Type, true, out type);
            Enum.TryParse(item.CurrentMood, true, out mood);
            Enum.TryParse(item.PreferredCombatStyle, true, out style);

            CompanionRecord record = new CompanionRecord
            {
                MemberId = item.MemberId,
                Name = item.Name,
                Type = type,
                Loyalty = item.Loyalty,
                Trust = item.Trust,
                Experience = item.Experience,
                CurrentMood = mood,
                PreferredCombatStyle = style,
                LastKnownWeapon = item.LastKnownWeapon,
                Memories = item.Memories ?? new List<string>(),
                Goals = item.Goals ?? new List<string>(),
                PersonalCash = item.PersonalCash,
                PersonalGold = item.PersonalGold
            };

            if (!string.IsNullOrWhiteSpace(record.MemberId))
                _records[record.MemberId] = record;
        }
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