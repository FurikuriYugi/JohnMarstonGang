using System;
using System.Collections.Generic;
using System.Linq;

public class LockSystem
{
    private readonly HashSet<string> _pickableTargets = new HashSet<string>();

    public void RegisterPickableTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        _pickableTargets.Add(targetId);
    }

    public void UnregisterPickableTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        _pickableTargets.Remove(targetId);
    }

    public bool IsPickable(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return false;

        return _pickableTargets.Contains(targetId);
    }

    public InteractionResult TryPickLock(GangMember member, string targetId, bool isLocked)
    {
        if (!isLocked)
            return InteractionResult.Succeeded("Already unlocked.");

        if (member == null)
            return InteractionResult.Failed("No actor.");

        if (CanPick(member))
            return InteractionResult.Succeeded("Lock picked.");

        return InteractionResult.Failed("Cannot pick lock.");
    }

    public InteractionResult TryCrackSafe(GangMember member, string safeId, bool isLocked)
    {
        if (!isLocked)
            return InteractionResult.Succeeded("Safe already open.");

        if (member == null)
            return InteractionResult.Failed("No actor.");

        if (CanPick(member))
            return InteractionResult.Succeeded("Safe cracked.");

        if (CanBlowOpen(member))
            return InteractionResult.Succeeded("Safe blasted open.");

        return InteractionResult.Failed("Cannot open safe.");
    }

    public bool CanPick(GangMember member)
    {
        if (member == null)
            return false;

        if (member.MemoryTags.Contains("HasLockpick"))
            return true;

        if (member.Goals.Contains("BreakerTool"))
            return true;

        return member.Role == GangMemberRole.Engineer || member.Role == GangMemberRole.Scout;
    }

    public bool CanBlowOpen(GangMember member)
    {
        if (member == null)
            return false;

        if (member.Weapons.Contains("Dynamite"))
            return true;

        if (member.MemoryTags.Contains("CanBlowOpen"))
            return true;

        return member.Role == GangMemberRole.Engineer;
    }
}