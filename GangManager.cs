using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public IReadOnlyList<GangMember> Members => _members;

    public void AddMember(GangMember member)
    {
        if (member == null)
            return;

        if (_members.Any(x => x.Id == member.Id))
            return;

        _members.Add(member);
    }

    public bool RemoveMember(string id)
    {
        var member = _members.FirstOrDefault(x => x.Id == id);
        if (member == null)
            return false;

        return _members.Remove(member);
    }

    public GangMember GetMember(string id)
    {
        return _members.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<GangMember> GetMembersByRole(GangMemberRole role)
    {
        return _members.Where(x => x.Role == role);
    }

    public void SetFollowingMode(bool enabled)
    {
        foreach (var member in _members)
        {
            member.TacticalState = enabled ? TacticalState.Following : TacticalState.Idle;
        }
    }

    public void SetStealthMode(bool enabled)
    {
        foreach (var member in _members)
        {
            member.TacticalState = enabled ? TacticalState.Stealth : TacticalState.Idle;
            member.CombatStyle = enabled ? CombatStyle.Stealth : member.CombatStyle;
        }
    }

    public void SetCombatAlert()
    {
        foreach (var member in _members)
        {
            member.TacticalState = TacticalState.Combat;
        }
    }

    public void UpdateMemberNeeds()
    {
        foreach (var member in _members)
        {
            member.NeedsFood = member.NeedsFood || member.Hunger >= 70;
            member.NeedsAmmo = member.NeedsAmmo || member.AmmoReserve <= 10;
            member.NeedsMedicine = member.NeedsMedicine || member.Health <= 35;
        }
    }

    public void AssignRole(string id, GangMemberRole role)
    {
        var member = GetMember(id);
        if (member == null)
            return;

        member.Role = role;
        member.UpdateCombatStyleFromRole();
    }

    public void RewardMember(string id, int loyaltyDelta, int experienceDelta)
    {
        var member = GetMember(id);
        if (member == null)
            return;

        member.Loyalty = Math.Max(0, member.Loyalty + loyaltyDelta);
        member.Experience = Math.Max(0, member.Experience + experienceDelta);
    }
}