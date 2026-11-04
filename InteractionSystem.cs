using System;
using System.Collections.Generic;
using System.Linq;

public class InteractionSystem
{
    public bool CanPickLock(GangMember member)
    {
        if (member == null)
            return false;

        return (member.Goals != null && member.Goals.Contains("BreakerTool"))
            || (member.MemoryTags != null && member.MemoryTags.Contains("HasLockpick"));
    }

    public bool CanUseDynamite(GangMember member)
    {
        if (member == null)
            return false;

        return (member.Weapons != null && member.Weapons.Contains("Dynamite"))
            || (member.MemoryTags != null && member.MemoryTags.Contains("CanBlowOpen"));
    }

    public InteractionResult TryOpenDoor(GangMember member, bool locked)
    {
        if (!locked)
            return InteractionResult.Succeeded("Door already open.");

        if (CanPickLock(member))
            return InteractionResult.Succeeded("Door picked.");

        if (CanUseDynamite(member))
            return InteractionResult.Succeeded("Door blasted open.");

        return InteractionResult.Failed("Cannot open door.");
    }

    public InteractionResult TryOpenSafe(GangMember member, bool locked, bool reinforced)
    {
        if (!locked)
            return InteractionResult.Succeeded("Safe already open.");

        if (CanPickLock(member))
            return InteractionResult.Succeeded("Safe cracked open.");

        if (reinforced && CanUseDynamite(member))
            return InteractionResult.Succeeded("Safe blasted open.");

        return InteractionResult.Failed("Cannot open safe.");
    }
}

public class InteractionResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;

    public static InteractionResult Succeeded(string message)
    {
        return new InteractionResult
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static InteractionResult Failed(string message)
    {
        return new InteractionResult
        {
            IsSuccess = false,
            Message = message
        };
    }
}