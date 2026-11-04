using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public List<GangMember> GetMembers()
    {
        return new List<GangMember>(_members);
    }

    public void AddMember(GangMember member)
    {
        if (member == null)
            return;

        _members.Add(member);
    }

    public void RemoveMember(string memberId)
    {
        _members.RemoveAll(member => member != null && member.MemberId == memberId);
    }

    public GangMember GetMember(string memberId)
    {
        return _members.Find(member => member != null && member.MemberId == memberId);
    }

    public void SetFollowing(string memberId, bool following)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.IsFollowingPlayer = following;
    }

    public void SetCurrentTask(string memberId, string task)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.CurrentTask = task;
    }

    public void UpdateLoyalty(string memberId, int delta)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.Loyalty += delta;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_members, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _members.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<GangMember> members = JsonConvert.DeserializeObject<List<GangMember>>(json);
        if (members != null)
            _members.AddRange(members);
    }
}