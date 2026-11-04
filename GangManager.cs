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
        if (string.IsNullOrWhiteSpace(memberId) || !_members.ContainsKey(memberId))
            return null;

        return _members[memberId];
    }

    public bool SetRole(string memberId, string role)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return false;

        member.Role = role;
        return true;
    }

    public bool SetCombatStyle(string memberId, string combatStyle)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return false;

        member.CombatStyle = combatStyle;
        return true;
    }

    public bool SetTask(string memberId, string task)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return false;

        member.CurrentTask = task;
        return true;
    }

    public void ApplyTacticalContext(TacticalContext context)
    {
        foreach (GangMember member in _members.Values)
        {
            if (member == null)
                continue;

            member.TacticalState = TacticalStateResolver.Resolve(member, context);
            member.CombatStyle = TacticalStateResolver.ResolveCombatStyle(member, context);
            member.BehaviorPriority = TacticalStateResolver.ResolvePriority(member, context);
        }
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

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_members.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _members.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<GangMember> members = JsonConvert.DeserializeObject<List<GangMember>>(json);
        if (members == null)
            return;

        foreach (GangMember member in members)
        {
            if (member != null && !string.IsNullOrWhiteSpace(member.MemberId))
                _members[member.MemberId] = member;
        }
    }
}

public static class TacticalStateResolver
{
    public static string Resolve(GangMember member, TacticalContext context)
    {
        if (context == null)
            return "Follow";

        if (context.IsStealth)
            return "Stealth";

        if (context.IsInCombat)
            return "Engage";

        if (context.IsShopping)
            return "Support";

        if (context.IsTraveling)
            return "Travel";

        if (!string.IsNullOrWhiteSpace(member?.CurrentTask))
            return member.CurrentTask;

        return "Follow";
    }

    public static string ResolveCombatStyle(GangMember member, TacticalContext context)
    {
        if (context != null && context.IsStealth)
            return "Stealth";

        if (member == null)
            return "Ranged Medium";

        if (member.Role == "Scout")
            return "Ranged Long";

        if (member.Role == "Bruiser")
            return "Lethal Melee";

        if (member.Role == "Hunter")
            return "Ranged Short";

        return string.IsNullOrWhiteSpace(member.CombatStyle) ? "Ranged Medium" : member.CombatStyle;
    }

    public static int ResolvePriority(GangMember member, TacticalContext context)
    {
        int priority = 50;

        if (member != null && member.Loyalty >= 75)
            priority += 10;

        if (member != null && member.Hunger <= 20)
            priority -= 15;

        if (context != null && context.IsInCombat)
            priority += 20;

        if (context != null && context.IsStealth)
            priority += 5;

        if (context != null && context.IsTraveling)
            priority += 3;

        if (context != null && context.IsShopping)
            priority += 2;

        return priority;
    }
}

public class TacticalContext
{
    public bool IsInCombat { get; set; }
    public bool IsStealth { get; set; }
    public bool IsTraveling { get; set; }
    public bool IsShopping { get; set; }
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
}