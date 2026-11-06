<!-- GangManagementSystem.cs lines 251-500 of 1186 -->
```csharp

        hideout.Stage = newStage;
        CompleteMilestone("HideoutUpgraded");
    }

    #endregion

    #region Hideout Chest Management

    public void DepositToCommunalChest(
        decimal cash,
        decimal gold,
        List<string> stolenItems = null)
    {
        if (cash < 0m || gold < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cash),
                "Chest deposits cannot be negative.");
        }

        Hideout hideout = GetActiveHideout();
        if (hideout == null)
        {
            throw new InvalidOperationException("No active hideout set.");
        }

        EnsureChest(hideout);
        hideout.ChestInventory.Cash += cash;
        hideout.ChestInventory.Gold += gold;

        if (stolenItems != null)
        {
            hideout.ChestInventory.StolenItems.AddRange(
                stolenItems.Where(item => item != null));
        }

        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
    }

    public bool WithdrawFromCommunalChest(decimal cash, decimal gold)
    {
        if (cash < 0m || gold < 0m)
        {
            return false;
        }

        Hideout hideout = GetActiveHideout();
        if (hideout == null)
        {
            throw new InvalidOperationException("No active hideout set.");
        }

        EnsureChest(hideout);
        ChestInventory chest = hideout.ChestInventory;
        if (chest.Cash < cash || chest.Gold < gold)
        {
            return false;
        }

        chest.Cash -= cash;
        chest.Gold -= gold;
        chest.LastUpdatedUtc = DateTime.UtcNow;
        return true;
    }

    public ChestInventory GetChestInventory(string hideoutId)
    {
        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            return null;
        }

        EnsureChest(hideout);
        return hideout.ChestInventory;
    }

    public void AddStolenItemsToChest(
        string hideoutId,
        List<string> stolenItems)
    {
        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }

        if (stolenItems == null || stolenItems.Count == 0)
        {
            return;
        }

        EnsureChest(hideout);
        hideout.ChestInventory.StolenItems.AddRange(
            stolenItems.Where(item => item != null));
        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
    }

    public void RemoveStolenItemsFromChest(
        string hideoutId,
        List<string> stolenItems)
    {
        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }

        if (stolenItems == null || stolenItems.Count == 0)
        {
            return;
        }

        EnsureChest(hideout);
        foreach (string item in stolenItems)
        {
            hideout.ChestInventory.StolenItems.Remove(item);
        }

        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
    }

    private static ChestInventory CreateEmptyChest()
    {
        return new ChestInventory
        {
            Cash = 0m,
            Gold = 0m,
            StolenItems = new List<string>(),
            Valuables = new List<string>(),
            LastUpdatedUtc = DateTime.UtcNow
        };
    }

    private static void EnsureChest(Hideout hideout)
    {
        if (hideout.ChestInventory == null)
        {
            hideout.ChestInventory = CreateEmptyChest();
        }

        if (hideout.ChestInventory.StolenItems == null)
        {
            hideout.ChestInventory.StolenItems = new List<string>();
        }

        if (hideout.ChestInventory.Valuables == null)
        {
            hideout.ChestInventory.Valuables = new List<string>();
        }
    }

    #endregion

    #region Job Reward Ledger

    public void OnJobCompleted(object sender, JobCompletedEventArgs args)
    {
        if (args == null ||
            args.JobData == null ||
            string.IsNullOrWhiteSpace(args.JobData.JobId) ||
            !string.Equals(
                args.JobId,
                args.JobData.JobId,
                StringComparison.Ordinal) ||
            !args.JobData.IsCompleted ||
            args.JobData.IsFailed)
        {
            return;
        }

        JobData job = args.JobData;

        // Keep settled records so a repeated completion event cannot recreate
        // a reward after settlement or after loading a save.
        if (pendingRewards.Values.Any(
            reward => reward != null &&
                string.Equals(
                    reward.JobId,
                    job.JobId,
                    StringComparison.Ordinal)))
        {
            return;
        }

        GangJobPayoutPlan plan = payoutSystem.CreatePlan(job);
        if (plan == null)
        {
            return;
        }

        List<string> participantIds = plan.MemberShares
            .Select(share => share.OwnerId)
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();

        Dictionary<string, decimal> cashShares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);
        Dictionary<string, decimal> goldShares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);
        Dictionary<string, decimal> combinedShares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            cashShares[share.OwnerId] = share.CashAmount;
            goldShares[share.OwnerId] = share.GoldAmount;
            combinedShares[share.OwnerId] =
                share.CashAmount + share.GoldAmount;
        }

        PendingJobReward rewardRecord = new PendingJobReward
        {
            LedgerVersion = 1,
            RewardId = Guid.NewGuid().ToString("N"),
            JobId = job.JobId,
            GrossCash = RoundCash(job.RewardCash),
            GrossGold = RoundCash(job.RewardGold),
            JobCostCash = RoundCash(job.CostCash),
            JobCostGold = RoundCash(job.CostGold),
            NetCash = plan.TotalCash,
            NetGold = plan.TotalGold,
            GangShareCash = plan.GangReserveCash,
            GangShareGold = plan.GangReserveGold,
            MemberCashShares = cashShares,
            MemberGoldShares = goldShares,
            TotalAmount = plan.TotalAmount,
            GangShareAmount = plan.GangReserveAmount,
            MemberShares = combinedShares,
            ParticipantIds = participantIds,
            DestinationType = "hideout_chest",
            DestinationId = ActiveHideoutId,
            IsSettled = false,
            CreatedAtUtc = DateTime.UtcNow,
            SettledAtUtc = DateTime.MinValue
        };

        pendingRewards.Add(rewardRecord.RewardId, rewardRecord);
    }

    public PendingJobReward GetPendingReward(string rewardId)
    {
        if (string.IsNullOrWhiteSpace(rewardId))
        {
            return null;
        }

```
