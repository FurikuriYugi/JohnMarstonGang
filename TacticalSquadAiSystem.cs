using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalSquadAiSystem
{
    public void Update(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        List<GangMember> members = gangManager.GetActiveMembers().ToList();

        foreach (GangMember member in members)
        {
            UpdateMemberState(member, gangManager);
            AssignBehavior(member, gangManager, members);
        }
    }

    private void UpdateMemberState(GangMember member, GangManager gangManager)
    {
        if (member == null)
            return;

        member.UpdateState();

        if (member.IsStealthing || member.CurrentGangMode == GangMode.Stealth)
        {
            member.IsInCombatMode = false;
            member.IsShopping = false;
            member.CurrentWeaponPreference = member.GetSuggestedWeapon();
            return;
        }

        if (member.IsInCombatMode || member.CurrentGangMode == GangMode.Combat)
        {
            member.IsShopping = false;
            member.CurrentWeaponPreference = member.GetSuggestedWeapon();
            return;
        }

        if (member.IsShopping || member.CurrentGangMode == GangMode.Shopping)
        {
            member.IsStealthing = false;
            member.IsInCombatMode = false;
            member.CurrentWeaponPreference = member.GetSuggestedWeapon();
            return;
        }

        member.CurrentWeaponPreference = member.GetSuggestedWeapon();
    }

    private void AssignBehavior(GangMember member, GangManager gangManager, List<GangMember> allMembers)
    {
        if (member == null)
            return;

        if (member.CurrentTask != GangTaskType.None)
            return;

        if (member.IsStealthing || member.CurrentGangMode == GangMode.Stealth)
        {
            member.CurrentTask = GangTaskType.StealthMove;
            member.CurrentTaskDescription = "Move quietly and support stealth actions.";
            return;
        }

        if (member.IsShopping || member.CurrentGangMode == GangMode.Shopping)
        {
            member.CurrentTask = GangTaskType.Shop;
            member.CurrentTaskDescription = "Handle purchases, supplies, or errands.";
            return;
        }

        if (member.IsInCombatMode || member.CurrentGangMode == GangMode.Combat)
        {
            member.CurrentTask = GangTaskType.Attack;
            member.CurrentTaskDescription = "Support combat and tactical positioning.";
            return;
        }

        if (member.Role == GangMemberRole.Support)
        {
            member.CurrentTask = GangTaskType.GatherSupplies;
            member.CurrentTaskDescription = "Gather supplies for the gang.";
            return;
        }

        if (member.Role == GangMemberRole.Scout)
        {
            member.CurrentTask = GangTaskType.Escort;
            member.CurrentTaskDescription = "Scout ahead and stay near the player.";
            return;
        }

        member.CurrentTask = GangTaskType.FollowPlayer;
        member.CurrentTaskDescription = "Follow the player and remain available.";
    }

    public void SetStealthMode(GangManager gangManager, bool enabled)
    {
        if (gangManager == null)
            return;

        gangManager.SetStealthMode(enabled);
        if (enabled)
            gangManager.SetGangMode(GangMode.Stealth);
    }

    public void SetCombatMode(GangManager gangManager, bool enabled)
    {
        if (gangManager == null)
            return;

        gangManager.SetCombatMode(enabled);
        if (enabled)
            gangManager.SetGangMode(GangMode.Combat);
    }

    public void SetShoppingMode(GangManager gangManager, bool enabled)
    {
        if (gangManager == null)
            return;

        gangManager.SetShoppingMode(enabled);
        if (enabled)
            gangManager.SetGangMode(GangMode.Shopping);
    }

    public void ClearTemporaryTask(GangMember member)
    {
        if (member == null)
            return;

        member.CurrentTask = GangTaskType.None;
        member.CurrentTargetId = null;
        member.CurrentTaskDescription = null;
    }

    public void SetTask(GangMember member, GangTaskType taskType, string targetId = null, string description = null)
    {
        if (member == null)
            return;

        member.CurrentTask = taskType;
        member.CurrentTargetId = targetId;
        member.CurrentTaskDescription = description;
    }
}