using System;

public class InteractionSystem
{
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;

    public InteractionSystem(
        LockSystem lockSystem,
        DynamiteSystem dynamiteSystem)
    {
        _lockSystem = lockSystem;
        _dynamiteSystem = dynamiteSystem;
    }

    public bool TryPickLock(string targetId)
    {
        if (_lockSystem == null ||
            string.IsNullOrWhiteSpace(targetId) ||
            !_lockSystem.CanPick(targetId))
        {
            return false;
        }

        _lockSystem.Pick(targetId);
        return true;
    }

    public bool TryBreakLock(string targetId)
    {
        if (_lockSystem == null ||
            string.IsNullOrWhiteSpace(targetId))
        {
            return false;
        }

        LockRecord record = _lockSystem.GetLock(targetId);

        if (record == null ||
            !record.IsLocked ||
            record.IsPickedOpen ||
            record.IsBlownOpen)
        {
            return false;
        }

        _lockSystem.Break(targetId);
        return true;
    }

    public bool TryBlowOpen(string targetId, int damage)
    {
        if (_dynamiteSystem == null ||
            string.IsNullOrWhiteSpace(targetId) ||
            damage <= 0 ||
            !_dynamiteSystem.CanDestroy(targetId))
        {
            return false;
        }

        _dynamiteSystem.ApplyExplosion(targetId, damage);
        return true;
    }

    public bool IsLocked(string targetId)
    {
        if (_lockSystem == null ||
            string.IsNullOrWhiteSpace(targetId))
        {
            return false;
        }

        LockRecord record = _lockSystem.GetLock(targetId);

        return record != null &&
               record.IsLocked &&
               !record.IsPickedOpen &&
               !record.IsBlownOpen;
    }

    public bool IsBroken(string targetId)
    {
        if (_lockSystem == null ||
            string.IsNullOrWhiteSpace(targetId))
        {
            return false;
        }

        LockRecord record = _lockSystem.GetLock(targetId);
        return record != null && record.IsBlownOpen;
    }

    public bool IsDestroyed(string targetId)
    {
        if (_dynamiteSystem == null ||
            string.IsNullOrWhiteSpace(targetId))
        {
            return false;
        }

        DestructibleRecord record =
            _dynamiteSystem.GetDestructible(targetId);

        return record != null && record.IsDestroyed;
    }

    public bool CanPickLock(string targetId)
    {
        return _lockSystem != null &&
               _lockSystem.CanPick(targetId);
    }

    public bool CanBlowOpen(string targetId)
    {
        return _dynamiteSystem != null &&
               _dynamiteSystem.CanDestroy(targetId);
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

    public void SyncTarget(
        string targetId,
        bool isLocked,
        bool isBroken,
        bool isDestructible,
        bool isDestroyed,
        int health,
        int maxHealth)
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
                    LockId = targetId,
                    IsLocked = isLocked && !isBroken,
                    IsPickedOpen = !isLocked && !isBroken,
                    IsBlownOpen = isBroken
                };

                _lockSystem.RegisterLock(lockRecord);
            }
            else
            {
                lockRecord.IsLocked = isLocked && !isBroken;
                lockRecord.IsPickedOpen = !isLocked && !isBroken;
                lockRecord.IsBlownOpen = isBroken;
            }
        }

        if (_dynamiteSystem == null || !isDestructible)
            return;

        DestructibleRecord destructibleRecord =
            _dynamiteSystem.GetDestructible(targetId);

        if (destructibleRecord == null)
        {
            destructibleRecord = new DestructibleRecord
            {
                DestructibleId = targetId,
                IsDestroyed = isDestroyed,
                HealthPoints = health,
                MaxHealthPoints = maxHealth,
                CanBeBlasted = true
            };

            _dynamiteSystem.RegisterDestructible(destructibleRecord);
        }
        else
        {
            destructibleRecord.IsDestroyed = isDestroyed;
            destructibleRecord.HealthPoints = health;
            destructibleRecord.MaxHealthPoints = maxHealth;
            destructibleRecord.CanBeBlasted = true;
        }
    }
}