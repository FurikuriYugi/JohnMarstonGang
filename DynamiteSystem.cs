using System;
using System.Collections.Generic;

public class DynamiteSystem
{
    private readonly Dictionary<string, DynamiteTarget> _targets = new Dictionary<string, DynamiteTarget>();

    public void RegisterTarget(string targetId, bool isDestructible, bool isExplodable)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        _targets[targetId] = new DynamiteTarget
        {
            TargetId = targetId,
            IsDestructible = isDestructible,
            IsExplodable = isExplodable,
            IsDestroyed = false
        };
    }

    public bool BlowOpen(string targetId, bool hasDynamite)
    {
        if (string.IsNullOrWhiteSpace(targetId) || !hasDynamite)
            return false;

        if (!_targets.TryGetValue(targetId, out DynamiteTarget target))
            return false;

        if (!target.IsDestructible || !target.IsExplodable)
            return false;

        target.IsDestroyed = true;
        return true;
    }

    public DynamiteTarget GetTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        _targets.TryGetValue(targetId, out DynamiteTarget target);
        return target;
    }
}

public class DynamiteTarget
{
    public string TargetId { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsExplodable { get; set; }
    public bool IsDestroyed { get; set; }
}