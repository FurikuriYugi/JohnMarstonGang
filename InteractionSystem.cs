// InteractionSystem.cs
using System;
using System.Collections.Generic;

public class InteractionSystem
{
    public void HandlePlayerInteraction(GangManager gangManager, string interactionType)
    {
        if (gangManager == null || string.IsNullOrWhiteSpace(interactionType))
            return;

        List<GangMember> members = gangManager.GetAllMembers();
        if (members == null)
            return;

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            switch (interactionType.ToLowerInvariant())
            {
                case "follow":
                    member.FollowingPlayer = true;
                    member.IsShopping = false;
                    member.IsStealthing = false;
                    member.IsInCombat = false;
                    member.AddMemory("Agreed to follow the player.");
                    break;

                case "stay":
                    member.FollowingPlayer = false;
                    member.AddMemory("Ordered to stay nearby.");
                    break;

                case "shop":
                    member.IsShopping = true;
                    member.FollowingPlayer = false;
                    member.AddMemory("Sent to handle shopping.");
                    break;

                case "stealth":
                    member.IsStealthing = true;
                    member.IsInCombat = false;
                    member.SetCombatStyle(CombatStyle.Stealth, "Bow");
                    member.AddMemory("Switched to stealth behavior.");
                    break;

                case "combat":
                    member.IsInCombat = true;
                    member.IsStealthing = false;
                    member.IsShopping = false;
                    member.AddMemory("Prepared for combat.");
                    break;

                case "restock":
                    member.NeedsSupplies = false;
                    member.AddMemory("Supplies restocked.");
                    break;
            }
        }
    }

    public void HandleMemberInteraction(GangMember member, string topic, bool positive)
    {
        if (member == null || string.IsNullOrWhiteSpace(topic))
            return;

        if (positive)
        {
            member.AdjustLoyalty(5);
            member.AddMemory("Positive interaction about " + topic);
        }
        else
        {
            member.AdjustLoyalty(-5);
            member.AddMemory("Negative interaction about " + topic);
        }
    }

    public void ApplySituation(GangManager gangManager, bool isStealth, bool isCombat, bool isShopping)
    {
        if (gangManager == null)
            return;

        List<GangMember> members = gangManager.GetAllMembers();
        if (members == null)
            return;

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            member.IsStealthing = isStealth;
            member.IsInCombat = isCombat;
            member.IsShopping = isShopping;

            if (isStealth)
            {
                member.SetCombatStyle(CombatStyle.Stealth, "Bow");
            }
            else if (isCombat)
            {
                if (member.SkillCombat > 70)
                    member.SetCombatStyle(CombatStyle.LongRange, "Rifle");
                else
                    member.SetCombatStyle(CombatStyle.ShortRange, "Revolver");
            }
        }
    }
}