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

    public void RegisterLockTarget(string targetId, LockTargetType targetType, bool requiresBreakerTool)
    {
        _lockSystem?.RegisterTarget(targetId, targetType, requiresBreakerTool);
    }

    public void RegisterDestructibleTarget(string targetId, DestructibleTargetType targetType, int health, bool lootable)
    {
        _dynamiteSystem?.RegisterTarget(targetId, targetType, health, lootable);
    }

    public void TryPickLock(string targetId, bool hasBreakerTool)
    {
        _lockSystem?.PickLock(targetId, hasBreakerTool);
    }

    public void TryBlowOpen(string targetId, int damage)
    {
        _dynamiteSystem?.ApplyExplosion(targetId, damage);
    }

    public void CreateOrClaimWagon(string wagonId, string ownerId)
    {
        _wagonSystem?.CreateWagon(wagonId, ownerId, true);
    }

    public void AddWagonCargo(string wagonId, string itemName)
    {
        _wagonSystem?.AddCargo(wagonId, itemName);
    }

    public void HitchHorseToWagon(string wagonId, string horseId)
    {
        _wagonSystem?.HitchHorse(wagonId, horseId);
        _wagonSystem?.SetHitched(wagonId, true);
    }

    public void UnhitchHorseFromWagon(string wagonId, string horseId)
    {
        _wagonSystem?.UnhitchHorse(wagonId, horseId);
    }

    public void DepositMoney(string ownerId, int cash, int gold)
    {
        _bankSystem?.DepositToAccount(ownerId, cash, gold);
    }

    public bool WithdrawMoney(string ownerId, int cash, int gold)
    {
        if (_bankSystem == null)
            return false;

        return _bankSystem.WithdrawFromAccount(ownerId, cash, gold);
    }

    public void BankDepositToVault(string bankName, int cash, int gold)
    {
        _bankSystem?.DepositToVault(bankName, cash, gold);
    }

    public void BankWithdrawFromVault(string bankName, int cash, int gold)
    {
        _bankSystem?.WithdrawFromVault(bankName, cash, gold);
    }

    public JobOffer CreateJob(string title, string description, string location, int rewardCash, int rewardHonor, int riskLevel, JobType jobType)
    {
        if (_jobSystem == null)
            return null;

        return _jobSystem.CreateJob(title, description, location, rewardCash, rewardHonor, riskLevel, jobType);
    }

    public void CompleteJob(string jobId)
    {
        _jobSystem?.CompleteJob(jobId);
    }

    public bool VoteOnJob(GangManager gangManager, string jobId)
    {
        if (_jobSystem == null || gangManager == null)
            return false;

        return _jobSystem.GangVoteOnJob(gangManager, jobId);
    }
}