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
}