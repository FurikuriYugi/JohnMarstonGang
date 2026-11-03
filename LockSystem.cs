using System;
using System.Collections.Generic;
using System.Linq;

public class LockSystem
{
    private readonly List<LockTarget> _targets = new List<LockTarget>();

    public List<LockTarget> Targets => _targets;

    public void RegisterTarget(string targetId, LockTargetType targetType, bool requiresBreakerTool)
    {
        LockTarget target = _targets.FirstOrDefault(t => t.TargetId == targetId);
        if (target != null)
        {
            target.TargetType = targetType;
            target.RequiresBreakerTool = requiresBreakerTool;
            return;
        }

        _targets.Add(new LockTarget
        {
            TargetId = targetId,
            TargetType = targetType,
            IsLocked = true,
            IsPicked = false,
            RequiresBreakerTool = requiresBreakerTool
        });
    }

    public bool CanPick(string targetId, bool hasBreakerTool)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        if (target.RequiresBreakerTool && !hasBreakerTool)
            return false;

        return target.IsLocked && !target.IsPicked;
    }

    public void PickLock(string targetId, bool hasBreakerTool)
    {
        if (!CanPick(targetId, hasBreakerTool))
            return;

        LockTarget target = GetTarget(targetId);
        target.IsLocked = false;
        target.IsPicked = true;
    }

    public void ForceOpen(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        if (target == null)
            return;

        target.IsLocked = false;
        target.IsPicked = false;
    }

    public bool IsUnlocked(string targetId)
    {
        LockTarget target = GetTarget(targetId);
        return target != null && !target.IsLocked;
    }

    public LockTarget GetTarget(string targetId)
    {
        return _targets.FirstOrDefault(t => t.TargetId == targetId);
    }

    public List<LockTarget> CreateSaveData()
    {
        return _targets.Select(t => new LockTarget
        {
            TargetId = t.TargetId,
            TargetType = t.TargetType,
            IsLocked = t.IsLocked,
            IsPicked = t.IsPicked,
            RequiresBreakerTool = t.RequiresBreakerTool
        }).ToList();
    }

    public void LoadFromSave(List<LockTarget> targets)
    {
        _targets.Clear();

        if (targets == null)
            return;

        foreach (LockTarget target in targets)
        {
            _targets.Add(new LockTarget
            {
                TargetId = target.TargetId,
                TargetType = target.TargetType,
                IsLocked = target.IsLocked,
                IsPicked = target.IsPicked,
                RequiresBreakerTool = target.RequiresBreakerTool
            });
        }
    }
}

public class LockTarget
{
    public string TargetId { get; set; }
    public LockTargetType TargetType { get; set; }
    public bool IsLocked { get; set; }
    public bool IsPicked { get; set; }
    public bool RequiresBreakerTool { get; set; }
}

public enum LockTargetType
{
    Door,
    Safe,
    Chest,
    Strongbox,
    Wagon,
    Object
}