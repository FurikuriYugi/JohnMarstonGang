using System;
using System.Collections.Generic;

public class DynamiteSystem
{
    private readonly Dictionary<string, DestructibleTarget> _targets = new Dictionary<string, DestructibleTarget>();

    public void RegisterTarget(string targetId, float health, bool canBeBlasted)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        _targets[targetId] = new DestructibleTarget
        {
            TargetId = targetId,
            Health = health,
            CanBeBlasted = canBeBlasted
        };
    }

    public void UnregisterTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        _targets.Remove(targetId);
    }

    public DestructibleTarget GetTarget(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        DestructibleTarget target;
        if (_targets.TryGetValue(targetId, out target))
            return target;

        return null;
    }

    public InteractionResult ApplyExplosion(string targetId, float damage)
    {
        var target = GetTarget(targetId);
        if (target == null)
            return InteractionResult.Failed("No target.");

        if (!target.CanBeBlasted)
            return InteractionResult.Failed("Target cannot be blasted.");

        target.Health = Math.Max(0f, target.Health - Math.Max(0f, damage));

        if (target.Health <= 0f)
            return InteractionResult.Succeeded("Target destroyed.");

        return InteractionResult.Succeeded("Target damaged.");
    }

    public InteractionResult BlowDoorOpen(string doorId)
    {
        var target = GetTarget(doorId);
        if (target == null)
            return InteractionResult.Failed("No door.");

        if (!target.CanBeBlasted)
            return InteractionResult.Failed("Door is blast-resistant.");

        target.Health = 0f;
        return InteractionResult.Succeeded("Door blown open.");
    }

    public InteractionResult BlowSafeOpen(string safeId)
    {
        var target = GetTarget(safeId);
        if (target == null)
            return InteractionResult.Failed("No safe.");

        if (!target.CanBeBlasted)
            return InteractionResult.Failed("Safe is blast-resistant.");

        target.Health = 0f;
        return InteractionResult.Succeeded("Safe blown open.");
    }
}