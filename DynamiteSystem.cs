using System;
using System.Collections.Generic;
using System.Linq;

public class DynamiteSystem
{
    private readonly List<DestructibleTarget> _targets = new List<DestructibleTarget>();

    public List<DestructibleTarget> Targets => _targets;

    public void RegisterTarget(string targetId, DestructibleTargetType targetType, int health, bool lootable = false)
    {
        DestructibleTarget target = _targets.FirstOrDefault(t => t.TargetId == targetId);
        if (target != null)
        {
            target.TargetType = targetType;
            target.Health = health;
            target.IsLootable = lootable;
            return;
        }

        _targets.Add(new DestructibleTarget
        {
            TargetId = targetId,
            TargetType = targetType,
            Health = health,
            IsDestroyed = false,
            IsLootable = lootable
        });
    }

    public bool CanBlowOpen(string targetId)
    {
        DestructibleTarget target = GetTarget(targetId);
        return target != null && !target.IsDestroyed;
    }

    public void ApplyExplosion(string targetId, int damage)
    {
        DestructibleTarget target = GetTarget(targetId);
        if (target == null || target.IsDestroyed)
            return;

        target.Health -= damage;

        if (target.Health <= 0)
        {
            target.Health = 0;
            target.IsDestroyed = true;
        }
    }

    public bool IsDestroyed(string targetId)
    {
        DestructibleTarget target = GetTarget(targetId);
        return target != null && target.IsDestroyed;
    }

    public bool IsLootable(string targetId)
    {
        DestructibleTarget target = GetTarget(targetId);
        return target != null && target.IsLootable && target.IsDestroyed;
    }

    public void Repair(string targetId, int health)
    {
        DestructibleTarget target = GetTarget(targetId);
        if (target == null)
            return;

        target.Health += health;
        if (target.Health > 0)
            target.IsDestroyed = false;
    }

    public DestructibleTarget GetTarget(string targetId)
    {
        return _targets.FirstOrDefault(t => t.TargetId == targetId);
    }

    public List<DestructibleTarget> CreateSaveData()
    {
        return _targets.Select(t => new DestructibleTarget
        {
            TargetId = t.TargetId,
            TargetType = t.TargetType,
            Health = t.Health,
            IsDestroyed = t.IsDestroyed,
            IsLootable = t.IsLootable
        }).ToList();
    }

    public void LoadFromSave(List<DestructibleTarget> targets)
    {
        _targets.Clear();

        if (targets == null)
            return;

        foreach (DestructibleTarget target in targets)
        {
            _targets.Add(new DestructibleTarget
            {
                TargetId = target.TargetId,
                TargetType = target.TargetType,
                Health = target.Health,
                IsDestroyed = target.IsDestroyed,
                IsLootable = target.IsLootable
            });
        }
    }
}

public class DestructibleTarget
{
    public string TargetId { get; set; }
    public DestructibleTargetType TargetType { get; set; }
    public int Health { get; set; }
    public bool IsDestroyed { get; set; }
    public bool IsLootable { get; set; }
}

public enum DestructibleTargetType
{
    Door,
    Safe,
    Wall,
    Chest,
    Strongbox,
    Object
}