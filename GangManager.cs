// GangManager.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public IReadOnlyList<GangMember> Members => _members;

    public GangMember CreateMember(string name, CompanionType type = CompanionType.Human)
    {
        var member = new GangMember
        {
            Name = name,
            Type = type,
            IsActive = true
        };

        _members.Add(member);
        return member;
    }

    public GangMember GetMember(string memberId)
    {
        return _members.FirstOrDefault(x => x.MemberId == memberId);
    }

    public GangMember GetMemberByName(string name)
    {
        return _members.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public bool RemoveMember(string memberId)
    {
        var member = GetMember(memberId);
        if (member == null) return false;

        _members.Remove(member);
        return true;
    }

    public void AssignRole(string memberId, GangMemberRole role)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.Role = role;
    }

    public void SetCombatStyle(string memberId, CombatStyle style, string weaponChoice = null)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.SetCombatStyle(style, weaponChoice);
    }

    public void SetFollowingPlayer(string memberId, bool following)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.FollowingPlayer = following;
    }

    public void AddMemory(string memberId, string memory)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.AddMemory(memory);
    }

    public void AddGoal(string memberId, string goal)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.AddGoal(goal);
    }

    public void AdjustLoyalty(string memberId, int amount)
    {
        var member = GetMember(memberId);
        if (member == null) return;

        member.AdjustLoyalty(amount);
    }

    public IEnumerable<GangMember> GetActiveMembers()
    {
        return _members.Where(x => x.IsActive);
    }

    public IEnumerable<GangMember> GetFollowers()
    {
        return _members.Where(x => x.IsActive && x.FollowingPlayer);
    }

    public void LoadFromSave(List<GangMemberSaveData> savedMembers)
    {
        _members.Clear();
        if (savedMembers == null) return;

        foreach (var saved in savedMembers)
        {
            _members.Add(new GangMember
            {
                MemberId = saved.MemberId,
                Name = saved.Name,
                Type = saved.Type,
                Role = saved.Role,
                PreferredCombatStyle = saved.PreferredCombatStyle,
                Mood = saved.Mood,
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
                Goals = saved.Goals ?? new List<string>(),
                IsActive = true
            });
        }
    }

    public List<GangMemberSaveData> CreateSaveData()
    {
        var result = new List<GangMemberSaveData>();

        foreach (var member in _members)
        {
            result.Add(new GangMemberSaveData
            {
                MemberId = member.MemberId,
                Name = member.Name,
                Type = member.Type,
                Role = member.Role,
                PreferredCombatStyle = member.PreferredCombatStyle,
                Mood = member.Mood,
                Loyalty = member.Loyalty,
                SkillCombat = member.SkillCombat,
                SkillStealth = member.SkillStealth,
                SkillHandling = member.SkillHandling,
                SkillTrading = member.SkillTrading,
                WeaponChoice = member.WeaponChoice,
                Outfit = member.Outfit,
                PersonalCash = member.PersonalCash,
                PersonalGold = member.PersonalGold,
                Memories = new List<string>(member.Memories),
                Goals = new List<string>(member.Goals)
            });
        }

        return result;
    }
}