public class InteractionSystem
{
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;

    public InteractionSystem(LockSystem lockSystem, DynamiteSystem dynamiteSystem)
    {
        _lockSystem = lockSystem;
        _dynamiteSystem = dynamiteSystem;
    }

    public bool TryPickTarget(int targetId, bool hasBreakerTool)
    {
        if (_lockSystem == null)
            return false;

        if (!_lockSystem.CanPick(targetId, hasBreakerTool))
            return false;

        return _lockSystem.PickLock(targetId);
    }

    public bool TryBlastTarget(int targetId)
    {
        if (_lockSystem != null && _lockSystem.CanBlast(targetId))
            return _lockSystem.BlastOpen(targetId);

        if (_dynamiteSystem != null && _dynamiteSystem.CanBeDestroyed(targetId))
            return _dynamiteSystem.BlastOpen(targetId);

        return false;
    }

    public bool TryOpenTarget(int targetId)
    {
        if (_lockSystem == null)
            return false;

        return _lockSystem.Open(targetId);
    }
}