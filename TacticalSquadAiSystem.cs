using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalSquadAiSystem
{
    public void UpdateSquadTactics(IEnumerable<GangMember> members, TacticalContext context)
    {
        if (members == null)
            return;

        foreach (var member in members)
        {
            if (member == null)
                continue;

            member.TacticalState = SelectTacticalState(member, context);
            member.CombatStyle = SelectCombatStyle(member, context);
            member.BehaviorPriority = SelectPriority(member, context);
        }
    }

    public string SelectTacticalState(GangMember member, TacticalContext context)
    {
        if (context == null)
            return "Follow";

        if (context.IsStealth)
            return "Stealth";

        if (context.IsInCombat)
            return "Engage";

        if (context.IsTraveling)
            return "Travel";

        if (member.MemoryTags != null && member.MemoryTags.Contains("NeedsFood"))
            return "SupplyRun";

        return "Follow";
    }

    public string SelectCombatStyle(GangMember member, TacticalContext context)
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

        return member.CombatStyle ?? "Ranged Medium";
    }

    public int SelectPriority(GangMember member, TacticalContext context)
    {
        var priority = 50;

        if (member != null && member.Loyalty >= 75)
            priority += 10;

        if (member != null && member.Hunger <= 20)
            priority -= 15;

        if (context != null && context.IsInCombat)
            priority += 20;

        return priority;
    }
}

public class TacticalContext
{
    public bool IsInCombat { get; set; }
    public bool IsStealth { get; set; }
    public bool IsTraveling { get; set; }
    public bool IsShopping { get; set; }
    public bool IsOnHorse { get; set; }
}