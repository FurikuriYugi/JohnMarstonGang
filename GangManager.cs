using System.Collections.Generic;
using Newtonsoft.Json;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public List<GangMember> GetMembers()
    {
        return new List<GangMember>(_members);
    }

    public void RegisterMember(GangMember member)
    {
        if (member == null || string.IsNullOrWhiteSpace(member.MemberId))
            return;

        _members.RemoveAll(x => x != null && x.MemberId == member.MemberId);
        _members.Add(member);
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        return _members.Find(x => x != null && x.MemberId == memberId);
    }

    public void RemoveMember(string memberId)
    {
        _members.RemoveAll(x => x != null && x.MemberId == memberId);
    }

    public void ApplyTacticalContext(string memberId, string combatStyle, string currentTask, bool followingPlayer)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.CombatStyle = combatStyle;
        member.CurrentTask = currentTask;
        member.IsFollowingPlayer = followingPlayer;
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