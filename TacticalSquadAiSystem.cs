using System;
using System.Collections.Generic;

public class TacticalSquadAiSystem
{
    public void Update(GangManager gangManager)
    {
        if (gangManager == null || gangManager.Members == null)
            return;

        foreach (GangMember member in gangManager.Members)
        {
            if (member == null || !member.IsAlive)
                continue;

            ApplySituationalBehavior(member);
        }
    }

    private void ApplySituationalBehavior(GangMember member)
    {
        if (member.NeedsSupplies)
        {
            member.CurrentOrder = GangOrder.FindSupplies;
            member.PreferredCombatStyle = CombatStyle.ShortRange;
            return;
        }

        if (member.IsStealthMode)
        {
            member.CurrentOrder = GangOrder.Stealth;
            member.PreferredCombatStyle = CombatStyle.Stealth;
            return;
        }

        if (member.IsFollowingPlayer)
        {
            member.CurrentOrder = GangOrder.FollowPlayer;
            member.PreferredCombatStyle = GetBestCombatStyle(member);
            return;
        }

        member.CurrentOrder = GangOrder.HoldPosition;
        member.PreferredCombatStyle = GetBestCombatStyle(member);
    }

    private CombatStyle GetBestCombatStyle(GangMember member)
    {
        if (member == null)
            return CombatStyle.ShortRange;

        if (member.SkillStealth >= member.SkillCombat && member.SkillStealth >= member.SkillTrading)
            return CombatStyle.Stealth;

        if (member.SkillCombat > 75)
            return CombatStyle.LongRange;

        if (member.SkillTrading > 60)
            return CombatStyle.MediumRange;

        return CombatStyle.ShortRange;
    }
}