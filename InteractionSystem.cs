using System;

public class InteractionSystem
{
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;

    public InteractionSystem(LockSystem lockSystem, DynamiteSystem dynamiteSystem)
    {
        _lockSystem = lockSystem;
        _dynamiteSystem = dynamiteSystem;
    }

    public bool TryPickLock(string targetId)
    {
        if (_lockSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        if (!_lockSystem.CanPick(targetId))
            return false;

        _lockSystem.Pick(targetId);
        return true;
    }

    public bool TryBreakLock(string targetId)
    {
        if (_lockSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        if (!_lockSystem.HasLock(targetId))
            return false;

        _lockSystem.Break(targetId);
        return true;
    }

    public bool TryBlowOpen(string targetId, int damage)
    {
        if (_dynamiteSystem == null || string.IsNullOrWhiteSpace(targetId) || damage <= 0)
            return false;

        if (!_dynamiteSystem.CanDestroy(targetId))
            return false;

        _dynamiteSystem.ApplyExplosion(targetId, damage);
        return true;
    }

    public bool IsLocked(string targetId)
    {
        if (_lockSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        LockRecord record = _lockSystem.GetLock(targetId);
        return record != null && record.IsLocked && !record.IsBroken;
    }

    public bool IsBroken(string targetId)
    {
        if (_lockSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        LockRecord record = _lockSystem.GetLock(targetId);
        return record != null && record.IsBroken;
    }

    public bool IsDestroyed(string targetId)
    {
        if (_dynamiteSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        DestructibleRecord record = _dynamiteSystem.GetDestructible(targetId);
        return record != null && record.IsDestroyed;
    }

    public bool CanPickLock(string targetId)
    {
        return _lockSystem != null && _lockSystem.CanPick(targetId);
    }

    public bool CanBlowOpen(string targetId)
    {
        if (_dynamiteSystem == null || string.IsNullOrWhiteSpace(targetId))
            return false;

        DestructibleRecord record = _dynamiteSystem.GetDestructible(targetId);
        return record != null && record.IsDestructible && !record.IsDestroyed && record.CanBeBlasted;
    }

    public void RegisterLock(LockRecord record)
    {
        if (_lockSystem == null || record == null)
            return;

        _lockSystem.RegisterLock(record);
    }

    public void RegisterDestructible(DestructibleRecord record)
    {
        if (_dynamiteSystem == null || record == null)
            return;

        _dynamiteSystem.RegisterDestructible(record);
    }

    public void SyncTarget(string targetId, bool isLocked, bool isBroken, bool isDestructible, bool isDestroyed, int health, int maxHealth)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return;

        if (_lockSystem != null)
        {
            LockRecord lockRecord = _lockSystem.GetLock(targetId);
            if (lockRecord == null)
            {
                lockRecord = new LockRecord
                {
                    TargetId = targetId,
                    IsLocked = isLocked,
                    IsBroken = isBroken
                };
                _lockSystem.RegisterLock(lockRecord);
            }
            else
            {
                lockRecord.IsLocked = isLocked;
                lockRecord.IsBroken = isBroken;
            }
        }

        if (_dynamiteSystem != null)
        {
            DestructibleRecord destructibleRecord = _dynamiteSystem.GetDestructible(targetId);
            if (destructibleRecord == null)
            {
                destructibleRecord = new DestructibleRecord
                {
                    TargetId = targetId,
                    IsDestructible = isDestructible,
                    IsDestroyed = isDestroyed,
                    Health = health,
                    MaxHealth = maxHealth,
                    CanBeBlasted = true,
                    CanBePicked = true
                };
                _dynamiteSystem.RegisterDestructible(destructibleRecord);
            }
            else
            {
                destructibleRecord.IsDestructible = isDestructible;
                destructibleRecord.IsDestroyed = isDestroyed;
                destructibleRecord.Health = health;
                destructibleRecord.MaxHealth = maxHealth;
            }
        }
    }
}