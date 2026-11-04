using System.Collections.Generic;
using Newtonsoft.Json;

public class GangManager
{
    private readonly Dictionary<string, GangMember> _members = new Dictionary<string, GangMember>();

    public void RegisterMember(GangMember member)
    {
        if (member == null || string.IsNullOrWhiteSpace(member.MemberId))
            return;

        _members[member.MemberId] = member;
    }

    public bool HasMember(string memberId)
    {
        return !string.IsNullOrWhiteSpace(memberId) && _members.ContainsKey(memberId);
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        _members.TryGetValue(memberId, out GangMember member);
        return member;
    }

    public List<GangMember> GetAllMembers()
    {
        return new List<GangMember>(_members.Values);
    }

    public void LoadMembers(List<GangMember> members)
    {
        _members.Clear();

        if (members == null)
            return;

        foreach (GangMember member in members)
        {
            if (member != null && !string.IsNullOrWhiteSpace(member.MemberId))
                _members[member.MemberId] = member;
        }
    }

    public void ApplyTacticalContext(TacticalContext context)
    {
        foreach (GangMember member in _members.Values)
        {
            if (member == null)
                continue;

            member.TacticalState = TacticalSquadAiSystem.ResolveTacticalState(member, context);
            member.CombatStyle = TacticalSquadAiSystem.ResolveCombatStyle(member, context);
            member.BehaviorPriority = TacticalSquadAiSystem.ResolvePriority(member, context);
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllMembers(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _members.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<GangMember> members = JsonConvert.DeserializeObject<List<GangMember>>(json);
        LoadMembers(members);
    }
}

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string CombatStyle { get; set; }
    public string TacticalState { get; set; }
    public string CurrentTask { get; set; }
    public int Loyalty { get; set; }
    public int Hunger { get; set; }
    public int BehaviorPriority { get; set; } = 50;
    public List<string> MemoryTags { get; set; } = new List<string>();

    public List<string> Goals { get; set; } = new List<string>();
    public List<string> InventoryItems { get; set; } = new List<string>();
    public bool IsFollowingPlayer { get; set; } = true;
    public bool IsAlive { get; set; } = true;
    public int CombatSkill { get; set; }
    public int StealthSkill { get; set; }
    public int SupportSkill { get; set; }
}

public class TacticalContext
{
    public bool IsInCombat { get; set; }
    public bool IsStealth { get; set; }
    public bool IsTraveling { get; set; }
    public bool IsShopping { get; set; }
    public bool IsRobbing { get; set; }
    public bool IsDefending { get; set; }
    public bool IsUsingArtillery { get; set; }
    public bool IsInCamp { get; set; }
    public bool IsMounted { get; set; }
}