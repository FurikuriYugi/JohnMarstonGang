// InteractionSystem.cs
using System;

public class InteractionSystem
{
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;
    private readonly WagonSystem _wagonSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;

    public InteractionSystem(
        LockSystem lockSystem,
        DynamiteSystem dynamiteSystem,
        WagonSystem wagonSystem,
        BankSystem bankSystem,
        JobSystem jobSystem)
    {
        _lockSystem = lockSystem;
        _dynamiteSystem = dynamiteSystem;
        _wagonSystem = wagonSystem;
        _bankSystem = bankSystem;
        _jobSystem = jobSystem;
    }

    public bool TryPickDoor(string targetId, bool hasBreakerTool)
    {
        return _lockSystem != null && _lockSystem.PickLock(targetId, hasBreakerTool);
    }

    public bool TryBlastDoor(string targetId, bool hasDynamite)
    {
        return _lockSystem != null && _lockSystem.BlastOpen(targetId, hasDynamite);
    }

    public bool TryBlowTarget(string targetId, bool hasDynamite)
    {
        return _dynamiteSystem != null && _dynamiteSystem.BlowOpen(targetId, hasDynamite);
    }

    public bool TryForceOpen(string targetId)
    {
        return _lockSystem != null && _lockSystem.ForceOpen(targetId);
    }

    public bool RegisterLockTarget(string targetId, LockTargetType type, bool isLocked, bool canBeBlasted)
    {
        if (_lockSystem == null) return false;
        _lockSystem.RegisterTarget(targetId, type, isLocked, canBeBlasted);
        return true;
    }

    public bool RegisterDynamiteTarget(string targetId, DynamiteTargetType type, bool isDestructible, bool isExplodable)
    {
        if (_dynamiteSystem == null) return false;
        _dynamiteSystem.RegisterTarget(targetId, type, isDestructible, isExplodable);
        return true;
    }

    public Wagon CreateOrClaimWagon(string wagonName, string ownerId, WagonType type)
    {
        if (_wagonSystem == null) return null;
        return _wagonSystem.CreateWagon(wagonName, ownerId, type);
    }

    public bool StoreItemInWagon(string wagonId, string itemName, int amount)
    {
        if (_wagonSystem == null) return false;
        return _wagonSystem.StoreItem(wagonId, itemName, amount);
    }

    public bool RemoveItemFromWagon(string wagonId, string itemName, int amount)
    {
        if (_wagonSystem == null) return false;
        return _wagonSystem.RemoveItem(wagonId, itemName, amount);
    }

    public void DepositBankCash(string ownerId, decimal amount)
    {
        if (_bankSystem == null) return;
        _bankSystem.DepositCash(ownerId, amount);
    }

    public void DepositBankGold(string ownerId, decimal amount)
    {
        if (_bankSystem == null) return;
        _bankSystem.DepositGold(ownerId, amount);
    }

    public bool WithdrawBankCash(string ownerId, decimal amount)
    {
        if (_bankSystem == null) return false;
        return _bankSystem.WithdrawCash(ownerId, amount);
    }

    public bool WithdrawBankGold(string ownerId, decimal amount)
    {
        if (_bankSystem == null) return false;
        return _bankSystem.WithdrawGold(ownerId, amount);
    }

    public void AddTownJobs(string townName)
    {
        _jobSystem?.GenerateTownJobs(townName);
    }

    public bool AcceptJob(string jobId, string gangMemberName = null)
    {
        return _jobSystem != null && _jobSystem.AcceptJob(jobId, gangMemberName);
    }

    public bool CompleteJob(string assignmentId)
    {
        return _jobSystem != null && _jobSystem.CompleteJob(assignmentId);
    }

    public bool FailJob(string assignmentId)
    {
        return _jobSystem != null && _jobSystem.FailJob(assignmentId);
    }

    public void PayOutCompletedJobs(string playerAccountId)
    {
        _jobSystem?.PayOutCompletedJobs(_bankSystem, playerAccountId);
    }
}