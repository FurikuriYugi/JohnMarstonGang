using System.Collections.Generic;
using System.Linq;

public class LockSystem
{
    private readonly List<LockTarget> _targets = new List<LockTarget>();

    public IReadOnlyList<LockTarget> Targets => _targets;

    public LockTarget RegisterTarget(string targetId, LockTargetType type, bool isLocked, bool canBeBlasted)
    {
        LockTarget existing = GetTarget(targetId);
        if (existing != null)
        {
            existing.Type = type;
            existing.IsLocked = isLocked;
            existing.CanBeBlasted = canBeBlasted;
            return existing;
        }

        LockTarget target = new LockTarget
        {
            TargetId = targetId,
            Type = type,
            IsLocked = isLocked,
            CanBeBlasted = canBeBlasted
        };

        _targets.Add(target);
        return target;
    }

    public bool CanPickLock(string targetId, bool hasBreakerTool)
    {
        if (!hasBreakerTool)
            return false;

        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        return target.IsLocked && target.Type != LockTargetType.Wall;
    }

    public bool PickLock(string targetId, bool hasBreakerTool)
    {
        if (!CanPickLock(targetId, hasBreakerTool))
            return false;

        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        target.IsLocked = false;
        target.WasPicked = true;
        return true;
    }

    public bool CanBlastOpen(string targetId, bool hasDynamite)
    {
        if (!hasDynamite)
            return false;

        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        return target.IsLocked && target.CanBeBlasted;
    }

    public bool BlastOpen(string targetId, bool hasDynamite)
    {
        if (!CanBlastOpen(targetId, hasDynamite))
            return false;

        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        target.IsLocked = false;
        target.WasBlasted = true;
        target.IsDestroyed = target.Type == LockTargetType.Wall;
        return true;
    }

    public bool ForceOpen(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        target.IsLocked = false;
        target.WasForced = true;
        return true;
    }

    public LockTarget GetTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        return _targets.FirstOrDefault(x => x.TargetId == targetId);
    }

    public bool IsUnlocked(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        return !target.IsLocked;
    }

    public void RemoveDestroyedTargets()
    {
        _targets.RemoveAll(x => x.IsDestroyed);
    }

    public void MarkAsLocked(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return;

        target.IsLocked = true;
    }

    public void MarkAsUnlocked(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return;

        target.IsLocked = false;
    }
}

public class LockTarget
{
    public string TargetId { get; set; }
    public LockTargetType Type { get; set; }
    public bool IsLocked { get; set; }
    public bool CanBeBlasted { get; set; }
    public bool WasPicked { get; set; }
    public bool WasBlasted { get; set; }
    public bool WasForced { get; set; }
    public bool IsDestroyed { get; set; }
}

public enum LockTargetType
{
    Door = 0,
    Safe = 1,
    Wall = 2,
    Chest = 3,
    Strongbox = 4
}