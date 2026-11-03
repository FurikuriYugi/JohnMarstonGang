using System;
using System.Collections.Generic;

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

    public bool TryPickLock(string targetId, bool hasBreakerTool)
    {
        if (_lockSystem == null)
            return false;

        return _lockSystem.PickLock(targetId, hasBreakerTool);
    }

    public bool TryBlastOpen(string targetId, bool hasDynamite)
    {
        if (_dynamiteSystem == null)
            return false;

        return _dynamiteSystem.BlowOpen(targetId, hasDynamite);
    }

    public bool TryForceOpen(string targetId)
    {
        if (_lockSystem == null)
            return false;

        return _lockSystem.ForceOpen(targetId);
    }

    public Wagon CreateWagon(string name, string ownerName, bool persistent)
    {
        if (_wagonSystem == null)
            return null;

        return _wagonSystem.CreateWagon(name, ownerName, persistent);
    }

    public bool StoreInWagon(string wagonId, string itemName, int amount)
    {
        if (_wagonSystem == null)
            return false;

        return _wagonSystem.StoreItem(wagonId, itemName, amount);
    }

    public bool RemoveFromWagon(string wagonId, string itemName, int amount)
    {
        if (_wagonSystem == null)
            return false;

        return _wagonSystem.RemoveItem(wagonId, itemName, amount);
    }

    public void DepositCash(string ownerName, decimal amount)
    {
        if (_bankSystem == null)
            return;

        _bankSystem.DepositCash(ownerName, amount);
    }

    public void DepositGold(string ownerName, decimal amount)
    {
        if (_bankSystem == null)
            return;

        _bankSystem.DepositGold(ownerName, amount);
    }

    public bool WithdrawCash(string ownerName, decimal amount)
    {
        if (_bankSystem == null)
            return false;

        return _bankSystem.WithdrawCash(ownerName, amount);
    }

    public bool WithdrawGold(string ownerName, decimal amount)
    {
        if (_bankSystem == null)
            return false;

        return _bankSystem.WithdrawGold(ownerName, amount);
    }

    public void GenerateTownJobs(string townName)
    {
        if (_jobSystem == null)
            return;

        _jobSystem.GenerateTownJobs(townName);
    }

    public JobOffer GetJobOffer(string jobId)
    {
        if (_jobSystem == null)
            return null;

        return _jobSystem.GetOffer(jobId);
    }

    public JobAssignment AcceptJob(string jobId, string gangMemberName = null)
    {
        if (_jobSystem == null)
            return null;

        return _jobSystem.AcceptJob(jobId, gangMemberName);
    }

    public bool CompleteJob(string assignmentId)
    {
        if (_jobSystem == null)
            return false;

        return _jobSystem.CompleteJob(assignmentId);
    }

    public bool FailJob(string assignmentId)
    {
        if (_jobSystem == null)
            return false;

        return _jobSystem.FailJob(assignmentId);
    }

    public List<JobOffer> GetAvailableJobs()
    {
        if (_jobSystem == null)
            return new List<JobOffer>();

        return _jobSystem.GetAvailableJobs();
    }
}