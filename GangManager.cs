using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public IReadOnlyList<GangMember> Members => _members.AsReadOnly();

    public GangMember AddMember(string name, CompanionType type = CompanionType.Human, GangMemberRole role = GangMemberRole.Recruit)
    {
        GangMember member = new GangMember
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unknown" : name,
            Type = type,
            Role = role,
            IsActive = true
        };

        _members.Add(member);
        return member;
    }

    public bool RemoveMember(string memberId)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return false;

        return _members.Remove(member);
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        return _members.FirstOrDefault(m => string.Equals(m.MemberId, memberId, StringComparison.OrdinalIgnoreCase));
    }

    public GangMember GetMemberByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _members.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public List<GangMember> GetActiveMembers()
    {
        return _members.Where(m => m != null && m.IsActive).ToList();
    }

    public void UpdateAll()
    {
        foreach (GangMember member in _members)
        {
            if (member == null || !member.IsActive)
                continue;

            if (member.NeedsSupplies)
                member.AddGoal("Acquire supplies");

            if (member.IsShopping)
                member.AddMemory("Went shopping with the player");

            if (member.IsStealthing)
                member.SetCombatStyle(CombatStyle.Stealth);

            if (member.IsInCombat && member.Loyalty < 25)
                member.AdjustLoyalty(-1);
        }
    }

    public List<GangMemberData> CreateSaveData()
    {
        List<GangMemberData> data = new List<GangMemberData>();

        foreach (GangMember member in _members)
        {
            if (member == null)
                continue;

            data.Add(new GangMemberData
            {
                Name = member.Name,
                ModelName = member.Type.ToString(),
                Role = member.Role.ToString(),
                Health = 100f,
                Stamina = 100f,
                DeadEye = 100f,
                Honor = member.Loyalty,
                IsAlive = true,
                IsFollowing = member.FollowingPlayer,
                CombatStyle = member.PreferredCombatStyle.ToString(),
                Goals = new List<string>(member.Goals),
                Memories = new List<string>(member.Memories),
                InventoryItems = new List<string>()
            });
        }

        return data;
    }

    public void LoadFromSave(List<GangMemberData> data)
    {
        _members.Clear();

        if (data == null)
            return;

        foreach (GangMemberData saved in data)
        {
            if (saved == null)
                continue;

            GangMember member = new GangMember
            {
                Name = saved.Name,
                Type = Enum.TryParse(saved.ModelName, true, out CompanionType type) ? type : CompanionType.Human,
                Role = Enum.TryParse(saved.Role, true, out GangMemberRole role) ? role : GangMemberRole.Recruit,
                PreferredCombatStyle = GangTypeHelpers.ParseCombatStyle(saved.CombatStyle),
                Loyalty = saved.Honor,
                FollowingPlayer = saved.IsFollowing,
                IsActive = saved.IsAlive
            };

            if (saved.Goals != null)
                member.Goals.AddRange(saved.Goals);

            if (saved.Memories != null)
                member.Memories.AddRange(saved.Memories);

            _members.Add(member);
        }
    }
}