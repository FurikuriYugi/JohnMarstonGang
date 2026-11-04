using System;
using System.Collections.Generic;

public static class TacticalSquadAiSystem
{
    public static string ResolveTacticalState(GangMember member, TacticalContext context)
    {
        if (member == null)
            return "Idle";

        if (context == null)
            return "Idle";

        if (!member.IsAlive)
            return "Down";

        if (context.IsInCombat)
        {
            if (context.IsStealth)
                return "StealthCombat";

            if (context.IsUsingArtillery)
                return "ArtillerySupport";

            return "Combat";
        }

        if (context.IsStealth)
            return "Stealth";

        if (context.IsShopping)
            return "SupportShopping";

        if (context.IsRobbing)
            return "HeistSupport";

        if (context.IsTraveling)
            return "Traveling";

        if (context.IsInCamp)
            return "CampDuty";

        return "Idle";
    }

    public static string ResolveCombatStyle(GangMember member, TacticalContext context)
    {
        if (member == null)
            return "Ranged Medium";

        if (!member.IsAlive)
            return "None";

        if (context != null && context.IsStealth)
            return ChooseStealthStyle(member);

        if (context != null && context.IsInCombat)
            return ChooseCombatStyle(member, context);

        return ChooseBalancedStyle(member);
    }

    public static int ResolvePriority(GangMember member, TacticalContext context)
    {
        if (member == null)
            return 0;

        int priority = 50;

        priority += Math.Max(0, member.Loyalty / 2);
        priority += Math.Max(0, member.CombatSkill / 5);
        priority += Math.Max(0, member.SupportSkill / 5);

        if (member.Hunger > 70)
            priority += 10;

        if (context != null)
        {
            if (context.IsInCombat)
                priority += 25;

            if (context.IsStealth)
                priority += 15;

            if (context.IsShopping)
                priority += 10;

            if (context.IsRobbing)
                priority += 20;

            if (context.IsUsingArtillery)
                priority += 18;
        }

        if (!member.IsFollowingPlayer)
            priority -= 10;

        if (priority < 0)
            priority = 0;

        return priority;
    }

    private static string ChooseStealthStyle(GangMember member)
    {
        if (member.StealthSkill >= member.CombatSkill)
            return "Stealth";

        if (member.SupportSkill > member.CombatSkill)
            return "Stealth Support";

        return "Stealth";
    }

    private static string ChooseCombatStyle(GangMember member, TacticalContext context)
    {
        if (context != null && context.IsUsingArtillery)
            return "Artillery Support";

        if (member.CombatSkill >= 80)
            return "Ranged Long";

        if (member.CombatSkill >= 60)
            return "Ranged Medium";

        if (member.CombatSkill >= 40)
            return "Ranged Short";

        return "Lethal Melee";
    }

    private static string ChooseBalancedStyle(GangMember member)
    {
        if (member.StealthSkill >= 70)
            return "Stealth";

        if (member.CombatSkill >= 70)
            return "Ranged Long";

        if (member.CombatSkill >= 50)
            return "Ranged Medium";

        return "Ranged Short";
    }

    public static List<string> BuildRecommendedActions(GangMember member, TacticalContext context)
    {
        List<string> actions = new List<string>();

        if (member == null)
            return actions;

        string state = ResolveTacticalState(member, context);

        if (state == "Stealth" || state == "StealthCombat")
        {
            actions.Add("Hold position");
            actions.Add("Use quiet weapon");
            actions.Add("Avoid detection");
        }
        else if (state == "Combat")
        {
            actions.Add("Take cover");
            actions.Add("Engage threat");
            actions.Add("Flank target");
        }
        else if (state == "ArtillerySupport")
        {
            actions.Add("Load artillery");
            actions.Add("Assist gunner");
            actions.Add("Maintain firing lane");
        }
        else if (state == "SupportShopping")
        {
            actions.Add("Follow at distance");
            actions.Add("Guard perimeter");
            actions.Add("Carry supplies");
        }
        else if (state == "HeistSupport")
        {
            actions.Add("Watch entrance");
            actions.Add("Prepare tools");
            actions.Add("Carry loot");
        }
        else if (state == "Traveling")
        {
            actions.Add("Stay close");
            actions.Add("Patrol road");
        }
        else if (state == "CampDuty")
        {
            actions.Add("Rest");
            actions.Add("Maintain camp");
        }
        else
        {
            actions.Add("Idle");
        }

        return actions;
    }
}