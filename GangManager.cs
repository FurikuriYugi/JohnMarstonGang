using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public IReadOnlyList<GangMember> Members
    {
        get { return _members; }
    }

    public void AddMember(GangMember member)
    {
        if (member == null)
            return;

        if (string.IsNullOrWhiteSpace(member.Id))
            member.Id = Guid.NewGuid().ToString("N");

        if (_members.Any(x => x.Id == member.Id))
            return;

        if (member.Weapons == null)
            member.Weapons = new List<string>();

        if (member.MemoryTags == null)
            member.MemoryTags = new List<string>();

        if (member.Goals == null)
            member.Goals = new List<string>();

        if (string.IsNullOrWhiteSpace(member.CombatStyle))
            member.CombatStyle = "Ranged Medium";

        if (string.IsNullOrWhiteSpace(member.TacticalState))
            member.TacticalState = "Follow";

        if (string.IsNullOrWhiteSpace(member.Alignment))
            member.Alignment = "Neutral";

        if (string.IsNullOrWhiteSpace(member.Role))
            member.Role = "Member";

        _members.Add(member);
    }

    public void RemoveMember(string memberId)
    {
        GangMember member = _members.FirstOrDefault(x => x.Id == memberId);
        if (member != null)
            _members.Remove(member);
    }

    public GangMember GetMember(string memberId)
    {
        return _members.FirstOrDefault(x => x.Id == memberId);
    }

    public void UpdateMemberNeeds()
    {
        foreach (GangMember member in _members)
        {
            if (member == null)
                continue;

            member.Hunger = Math.Max(0, member.Hunger - 1);
            member.Health = Math.Max(0, member.Health);

            if (member.Hunger <= 20 && !member.MemoryTags.Contains("NeedsFood"))
                member.MemoryTags.Add("NeedsFood");

            if (member.Health <= 25 && !member.MemoryTags.Contains("NeedsMedicine"))
                member.MemoryTags.Add("NeedsMedicine");
        }
    }

    public void AssignRole(string memberId, string role)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.Role = role;
    }

    public void SetCombatStyle(string memberId, string style)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.CombatStyle = style;
    }

    public void SetAlignment(string memberId, string alignment)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.Alignment = alignment;
    }

    public List<GangMemberSaveData> CreateSaveData()
    {
        List<GangMemberSaveData> saveData = new List<GangMemberSaveData>();

        foreach (GangMember member in _members)
        {
            if (member == null)
                continue;

            saveData.Add(new GangMemberSaveData
            {
                Id = member.Id,
                Name = member.Name,
                CompanionType = CompanionType.Human,
                Role = member.Role,
                Alignment = member.Alignment,
                CombatStyle = member.CombatStyle,
                TacticalState = member.TacticalState,
                Loyalty = member.Loyalty,
                Experience = member.Experience,
                ModelName = member.ModelName,
                Weapons = member.Weapons == null ? new List<string>() : new List<string>(member.Weapons),
                MemoryTags = member.MemoryTags == null ? new List<string>() : new List<string>(member.MemoryTags),
                Goals = member.Goals == null ? new List<string>() : new List<string>(member.Goals)
            });
        }

        return saveData;
    }

    public void LoadFromSave(List<GangMemberSaveData> saveData)
    {
        _members.Clear();

        if (saveData == null)
            return;

        foreach (GangMemberSaveData data in saveData)
        {
            _members.Add(new GangMember
            {
                Id = data.Id,
                Name = data.Name,
                Role = data.Role,
                Alignment = data.Alignment,
                CombatStyle = data.CombatStyle,
                TacticalState = data.TacticalState,
                Loyalty = data.Loyalty,
                Experience = data.Experience,
                ModelName = data.ModelName,
                Weapons = data.Weapons == null ? new List<string>() : new List<string>(data.Weapons),
                MemoryTags = data.MemoryTags == null ? new List<string>() : new List<string>(data.MemoryTags),
                Goals = data.Goals == null ? new List<string>() : new List<string>(data.Goals),
                Health = 100,
                Hunger = 100
            });
        }
    }
}

public class GangMember
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ModelName { get; set; }
    public string Role { get; set; }
    public string Alignment { get; set; }
    public string CombatStyle { get; set; }
    public string TacticalState { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; } = 100;
    public int Hunger { get; set; } = 100;
    public List<string> Weapons { get; set; } = new List<string>();
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
}

public enum CompanionType
{
    Human,
    Animal
}

public enum GangMemberRole
{
    Member,
    Scout,
    Bruiser,
    Hunter,
    Leader
}

public enum RelationshipAlignment
{
    Neutral,
    Friendly,
    Loyal,
    Wary,
    Hostile
}

public enum CombatStyle
{
    NonLethalMelee,
    LethalMelee,
    RangedShort,
    RangedMedium,
    RangedLong,
    Stealth
}

public enum TacticalState
{
    Follow,
    Engage,
    Stealth,
    Travel,
    Support,
    SupplyRun
}