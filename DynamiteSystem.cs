using System.Collections.Generic;
using System.Linq;

public class DynamiteSystem
{
    private readonly List<DynamiteTarget> _targets = new List<DynamiteTarget>();

    public IReadOnlyList<DynamiteTarget> Targets => _targets;

    public DynamiteTarget RegisterTarget(string targetId, DynamiteTargetType type, bool isDestructible, bool isExplodable)
    {
        DynamiteTarget existing = GetTarget(targetId);
        if (existing != null)
        {
            existing.Type = type;
            existing.IsDestructible = isDestructible;
            existing.IsExplodable = isExplodable;
            return existing;
        }

        DynamiteTarget target = new DynamiteTarget
        {
            TargetId = targetId,
            Type = type,
            IsDestructible = isDestructible,
            IsExplodable = isExplodable
        };

        _targets.Add(target);
        return target;
    }

    public DynamiteTarget GetTarget(string targetId)
    {
        return _targets.FirstOrDefault(x => x.TargetId == targetId);
    }

    public bool CanBlowOpen(string targetId, bool hasDynamite)
    {
        if (!hasDynamite)
            return false;

        DynamiteTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        return target.IsExplodable && !target.IsDestroyed;
    }

    public bool BlowOpen(string targetId, bool hasDynamite)
    {
        DynamiteTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        if (!CanBlowOpen(targetId, hasDynamite))
            return false;

        target.IsDestroyed = true;
        target.WasBlown = true;
        target.DamageLevel = 100;
        return true;
    }

    public bool DamageTarget(string targetId, int damage)
    {
        DynamiteTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        if (!target.IsDestructible || damage <= 0)
            return false;

        target.DamageLevel += damage;
        if (target.DamageLevel >= 100)
        {
            target.DamageLevel = 100;
            target.IsDestroyed = true;
        }

        return true;
    }

    public bool RepairTarget(string targetId, int amount)
    {
        DynamiteTarget target = GetTarget(targetId);
        if (target == null)
            return false;

        if (amount <= 0)
            return false;

        target.DamageLevel -= amount;
        if (target.DamageLevel < 0)
            target.DamageLevel = 0;

        if (target.DamageLevel < 100)
            target.IsDestroyed = false;

        return true;
    }

    public void RemoveDestroyedTargets()
    {
        _targets.RemoveAll(x => x.IsDestroyed && x.Type == DynamiteTargetType.Wall);
    }

    public int GetDamageLevel(string targetId)
    {
        DynamiteTarget target = GetTarget(targetId);
        return target == null ? 0 : target.DamageLevel;
    }
}

public class DynamiteTarget
{
    public string TargetId { get; set; }
    public DynamiteTargetType Type { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsExplodable { get; set; }
    public bool IsDestroyed { get; set; }
    public bool WasBlown { get; set; }
    public int DamageLevel { get; set; }
}

public enum DynamiteTargetType
{
    Door = 0,
    Safe = 1,
    Wall = 2,
    Chest = 3,
    Strongbox = 4,
    Wagon = 5,
    Barricade = 6
}