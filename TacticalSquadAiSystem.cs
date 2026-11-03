using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalSquadAiSystem
{
    public void Update(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        List<GangMember> activeMembers = gangManager.GetActiveMembers();
        if (activeMembers.Count == 0)
            return;

        foreach (GangMember member in activeMembers)
            UpdateMember(member, activeMembers);
    }

    private void UpdateMember(GangMember member, List<GangMember> squad)
    {
        if (member == null || !member.IsAlive)
            return;

        if (member.IsInCombat)
        {
            ApplyCombatBehavior(member, squad);
            return;
        }

        if (member.IsStealthing)
        {
            ApplyStealthBehavior(member, squad);
            return;
        }

        if (member.IsShopping || member.NeedsSupplies)
        {
            ApplySupplyBehavior(member);
            return;
        }

        if (member.FollowingPlayer)
        {
            ApplyFollowBehavior(member, squad);
            return;
        }

        ApplyIdleBehavior(member, squad);
    }

    private void ApplyCombatBehavior(GangMember member, List<GangMember> squad)
    {
        member.IsOnJob = true;
        member.IsShopping = false;

        CombatStyle style = SelectCombatStyle(member, squad);
        member.SetCombatStyle(style, member.GetSuggestedWeapon());

        if (member.Loyalty < 25)
            member.Mood = GangMood.Nervous;
        else if (member.Loyalty > 75)
            member.Mood = GangMood.Loyal;
        else
            member.Mood = GangMood.Aggressive;
    }

    private CombatStyle SelectCombatStyle(GangMember member, List<GangMember> squad)
    {
        if (member.ShouldUseStealth())
            return CombatStyle.Stealth;

        int stealthCount = squad.Count(x => x != null && x.ShouldUseStealth());
        if (stealthCount > squad.Count / 2)
            return CombatStyle.Stealth;

        if (member.SkillCombat >= 75)
            return CombatStyle.LongRange;

        if (member.SkillStealth >= 60)
            return CombatStyle.MediumRange;

        if (member.Role == GangMemberRole.Gunner)
            return CombatStyle.LongRange;

        if (member.Role == GangMemberRole.Guard)
            return CombatStyle.ShortRange;

        return CombatStyle.ShortRange;
    }

    private void ApplyStealthBehavior(GangMember member, List<GangMember> squad)
    {
        member.SetCombatStyle(CombatStyle.Stealth, "Bow");
        member.Mood = member.Loyalty >= 50 ? GangMood.Loyal : GangMood.Nervous;
        member.CurrentTask = "Stealth";
    }

    private void ApplySupplyBehavior(GangMember member)
    {
        member.IsShopping = true;
        member.IsOnJob = false;
        member.CurrentTask = "Supply Run";

        if (member.SkillTrading > 50)
            member.Role = GangMemberRole.Trader;
        else if (member.SkillHandling > 50)
            member.Role = GangMemberRole.Driver;

        member.AddGoal("Acquire ammo, food, medicine, and gang supplies.");
        member.AddMemory("Working on supplies for the gang.");
    }

    private void ApplyFollowBehavior(GangMember member, List<GangMember> squad)
    {
        member.CurrentTask = "Follow";
        member.IsShopping = false;

        if (member.NeedsSupplies)
            member.AddGoal("Get supplies: ammo, food, medicine.");

        if (member.SkillTrading > 50)
            member.Role = GangMemberRole.Trader;
        else if (member.SkillHandling > 50)
            member.Role = GangMemberRole.Driver;

        if (member.PreferredCombatStyle == CombatStyle.Stealth)
            member.Mood = GangMood.Nervous;
        else if (member.Loyalty > 75)
            member.Mood = GangMood.Loyal;
        else
            member.Mood = GangMood.Neutral;
    }

    private void ApplyIdleBehavior(GangMember member, List<GangMember> squad)
    {
        member.IsOnJob = false;
        member.CurrentTask = "Idle";

        if (member.NeedsSupplies)
        {
            member.AddMemory("Needs to restock supplies.");
            member.AddGoal("Buy ammo, food, and medicine.");
        }

        if (member.Loyalty > 80)
            member.Mood = GangMood.Loyal;
        else if (member.Loyalty < 30)
            member.Mood = GangMood.Nervous;
        else
            member.Mood = GangMood.Neutral;
    }
}