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

        if (string.IsNullOrWhiteSpace(member.Id))
            member.Id = Guid.NewGuid().ToString("N");

        if (_members.Any(x => x.Id == member.Id))
            return;

        member.Weapons ??= new List<string>();
        member.MemoryTags ??= new List<string>();
        member.Goals ??= new List<string>();

        if (string.IsNullOrWhiteSpace(member.CombatStyle))
            member.CombatStyle = "Ranged Medium";

        if (string.IsNullOrWhiteSpace(member.TacticalState))
            member.TacticalState = "Follow";

        _members.Add(member);
    }

    public void RemoveMember(string memberId)
    {
        var member = _members.FirstOrDefault(x => x.Id == memberId);
        if (member != null)
            _members.Remove(member);
    }

    public GangMember GetMember(string memberId)
    {
        return _members.FirstOrDefault(x => x.Id == memberId);
    }

    public void UpdateMemberNeeds()
    {
        foreach (var member in _members)
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
        var member = GetMember(memberId);
        if (member == null)
            return;

        member.Role = role;
    }

    public void SetCombatStyle(string memberId, string style)
    {
        var member = GetMember(memberId);
        if (member == null)
            return;

        member.CombatStyle = style;
    }
}