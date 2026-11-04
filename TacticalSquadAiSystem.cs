using System.Collections.Generic;

public class TacticalSquadAiSystem
{
    public void UpdateSquadTactics(IEnumerable<GangMember> members, TacticalContext context)
    {
        if (members == null)
            return;

        foreach (GangMember member in members)
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

        if (context.IsShopping)
            return "Support";

        if (member != null && member.MemoryTags != null && member.MemoryTags.Contains("NeedsFood"))
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

        if (!string.IsNullOrEmpty(member.CombatStyle))
            return member.CombatStyle;

        return "Ranged Medium";
    }

    public int SelectPriority(GangMember member, TacticalContext context)
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