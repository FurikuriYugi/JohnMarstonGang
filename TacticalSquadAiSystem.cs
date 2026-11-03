using System.Collections.Generic;

public class TacticalSquadAiSystem
{
    public void UpdateGangTactics(List<GangMember> members, TacticalContext context)
    {
        if (members == null || context == null)
            return;

        foreach (var member in members)
        {
            if (member == null)
                continue;

            if (context.IsStealthMode)
            {
                member.CombatStyle = CombatStyle.Stealth;
                continue;
            }

            if (context.IsCloseQuarters)
            {
                member.CombatStyle = CombatStyle.LethalMelee;
                continue;
            }

            if (context.IsLongRangeCombat)
            {
                member.CombatStyle = CombatStyle.RangedLong;
                continue;
            }

            if (context.IsMediumRangeCombat)
            {
                member.CombatStyle = CombatStyle.RangedMedium;
                continue;
            }

            if (context.IsCombatActive)
            {
                member.CombatStyle = CombatStyle.RangedShort;
                continue;
            }

            if (context.IsShoppingOrTown)
            {
                member.CombatStyle = CombatStyle.RangedShort;
                continue;
            }

            if (context.IsTraveling && member.IsFollowing)
            {
                member.CombatStyle = CombatStyle.RangedMedium;
                continue;
            }

            if (member.WagonRole == WagonRole.Gunner || member.WagonRole == WagonRole.Driver || member.WagonRole == WagonRole.Loader)
            {
                member.CombatStyle = CombatStyle.RangedShort;
            }
        }
    }

    public void ApplyFormationSpacing(List<GangMember> members, TacticalContext context)
    {
        if (members == null || context == null)
            return;

        for (int i = 0; i < members.Count; i++)
        {
            var member = members[i];
            if (member == null)
                continue;

            if (!member.IsFollowing)
                continue;
        }
    }

    public void AssignWagonSupport(List<GangMember> members, string wagonId)
    {
        if (members == null)
            return;

        for (int i = 0; i < members.Count; i++)
        {
            var member = members[i];
            if (member == null)
                continue;

            if (i == 0)
                member.AssignToWagon(wagonId, WagonRole.Driver);
            else if (i == 1)
                member.AssignToWagon(wagonId, WagonRole.Gunner);
            else if (i == 2)
                member.AssignToWagon(wagonId, WagonRole.Loader);
            else
                member.AssignToWagon(wagonId, WagonRole.Guard);
        }
    }

    public void ClearWagonSupport(List<GangMember> members)
    {
        if (members == null)
            return;

        foreach (var member in members)
        {
            if (member == null)
                continue;

            member.ClearWagonAssignment();
        }
    }
}