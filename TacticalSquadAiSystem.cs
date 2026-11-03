// TacticalSquadAiSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalSquadAiSystem
{
    public void Update(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        foreach (var member in gangManager.GetActiveMembers())
        {
            UpdateMember(member);
        }
    }

    private void UpdateMember(GangMember member)
    {
        if (member == null)
            return;

        if (member.IsInCombat)
        {
            ApplyCombatBehavior(member);
            return;
        }

        if (member.FollowingPlayer)
        {
            ApplyFollowBehavior(member);
            return;
        }

        ApplyIdleBehavior(member);
    }

    private void ApplyCombatBehavior(GangMember member)
    {
        switch (member.PreferredCombatStyle)
        {
            case CombatStyle.NonLethalMelee:
                member.WeaponChoice = "Fists";
                break;
            case CombatStyle.LethalMelee:
                member.WeaponChoice = "Knife";
                break;
            case CombatStyle.ShortRange:
                if (string.IsNullOrWhiteSpace(member.WeaponChoice))
                    member.WeaponChoice = "Revolver";
                break;
            case CombatStyle.MediumRange:
                member.WeaponChoice = "Repeater";
                break;
            case CombatStyle.LongRange:
                member.WeaponChoice = "Rifle";
                break;
            case CombatStyle.Stealth:
                member.WeaponChoice = "Bow";
                break;
        }

        if (member.Loyalty < 25)
            member.Mood = GangMood.Nervous;
        else
            member.Mood = GangMood.Aggressive;
    }

    private void ApplyFollowBehavior(GangMember member)
    {
        if (member.PreferredCombatStyle == CombatStyle.Stealth)
            member.Mood = GangMood.Nervous;
        else if (member.Loyalty > 75)
            member.Mood = GangMood.Loyal;
        else
            member.Mood = GangMood.Neutral;
    }

    private void ApplyIdleBehavior(GangMember member)
    {
        if (member.Loyalty > 80)
            member.Mood = GangMood.Loyal;
        else if (member.Loyalty < 30)
            member.Mood = GangMood.Nervous;
        else
            member.Mood = GangMood.Neutral;
    }
}