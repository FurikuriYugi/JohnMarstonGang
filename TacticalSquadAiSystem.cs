using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalSquadAiSystem
{
    public void UpdateSquadTactics(IEnumerable<GangMember> members, TacticalContext context)
    {
        if (members == null || context == null)
            return;

        foreach (var member in members)
        {
            if (member == null)
                continue;

            ApplyContextToMember(member, context);
        }
    }

    private void ApplyContextToMember(GangMember member, TacticalContext context)
    {
        if (context.IsUnderFire)
        {
            member.TacticalState = TacticalState.Combat;
            member.CombatStyle = ChooseCombatStyleForThreat(member, context);
            return;
        }

        if (context.IsStealthMission)
        {
            member.TacticalState = TacticalState.Stealth;
            member.CombatStyle = CombatStyle.Stealth;
            return;
        }

        if (context.IsShoppingOrTown)
        {
            member.TacticalState = TacticalState.Guarding;
            member.CombatStyle = CombatStyle.RangedShort;
            return;
        }

        if (context.IsTraveling)
        {
            member.TacticalState = TacticalState.Following;
            member.CombatStyle = ChooseTravelCombatStyle(member);
            return;
        }

        if (context.RequiresWork)
        {
            member.TacticalState = TacticalState.Working;
            member.CombatStyle = ChooseWorkCombatStyle(member);
            return;
        }

        member.TacticalState = TacticalState.Idle;
    }

    private CombatStyle ChooseCombatStyleForThreat(GangMember member, TacticalContext context)
    {
        if (context.IsCloseQuarters)
        {
            if (member.Role == GangMemberRole.Scout || member.Role == GangMemberRole.Hunter)
                return CombatStyle.LethalMelee;

            return CombatStyle.RangedShort;
        }

        if (context.IsLongRange)
            return CombatStyle.RangedLong;

        if (context.IsSilentApproach)
            return CombatStyle.Stealth;

        return member.CombatStyle;
    }

    private CombatStyle ChooseTravelCombatStyle(GangMember member)
    {
        switch (member.Role)
        {
            case GangMemberRole.Scout:
                return CombatStyle.Stealth;
            case GangMemberRole.Hunter:
                return CombatStyle.RangedMedium;
            case GangMemberRole.Gunner:
                return CombatStyle.RangedLong;
            case GangMemberRole.Medic:
                return CombatStyle.RangedShort;
            default:
                return CombatStyle.RangedShort;
        }
    }

    private CombatStyle ChooseWorkCombatStyle(GangMember member)
    {
        if (member.Role == GangMemberRole.Engineer)
            return CombatStyle.RangedShort;

        if (member.Role == GangMemberRole.Driver)
            return CombatStyle.RangedShort;

        return CombatStyle.RangedMedium;
    }
}