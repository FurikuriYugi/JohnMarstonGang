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
}