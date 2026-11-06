<!-- GangManagementSystem.cs lines 751-1000 of 1186 -->
```csharp

        if (reward.MemberCashShares == null)
        {
            reward.MemberCashShares =
                new Dictionary<string, decimal>(StringComparer.Ordinal);
        }

        if (reward.MemberGoldShares == null)
        {
            reward.MemberGoldShares =
                new Dictionary<string, decimal>(StringComparer.Ordinal);
        }

        if (reward.ParticipantIds == null)
        {
            reward.ParticipantIds = new List<string>();
        }

        if (reward.LedgerVersion < 1)
        {
            // Previous records collapsed cash and gold into one cash amount.
            // Treat that recorded amount as cash so pending legacy rewards
            // settle the same way they did before this ledger change.
            reward.GrossCash = RoundCash(Math.Max(0m, reward.TotalAmount));
            reward.GrossGold = 0m;
            reward.JobCostCash = 0m;
            reward.JobCostGold = 0m;
            reward.NetCash = reward.GrossCash;
            reward.NetGold = 0m;
            reward.GangShareCash =
                RoundCash(Math.Max(0m, reward.GangShareAmount));
            reward.GangShareGold = 0m;

            reward.MemberCashShares =
                new Dictionary<string, decimal>(StringComparer.Ordinal);
            foreach (KeyValuePair<string, decimal> share
                in reward.MemberShares)
            {
                if (!string.IsNullOrWhiteSpace(share.Key) &&
                    share.Value >= 0m)
                {
                    reward.MemberCashShares[share.Key] =
                        RoundCash(share.Value);
                }
            }

            reward.MemberGoldShares =
                new Dictionary<string, decimal>(StringComparer.Ordinal);
            reward.LedgerVersion = 1;
        }

        reward.GrossCash = RoundCash(Math.Max(0m, reward.GrossCash));
        reward.GrossGold = RoundCash(Math.Max(0m, reward.GrossGold));
        reward.JobCostCash = RoundCash(Math.Max(0m, reward.JobCostCash));
        reward.JobCostGold = RoundCash(Math.Max(0m, reward.JobCostGold));
        reward.NetCash = RoundCash(Math.Max(0m, reward.NetCash));
        reward.NetGold = RoundCash(Math.Max(0m, reward.NetGold));
        reward.GangShareCash =
            RoundCash(Math.Max(0m, reward.GangShareCash));
        reward.GangShareGold =
            RoundCash(Math.Max(0m, reward.GangShareGold));

        reward.ParticipantIds = NormalizeParticipantIds(
            reward.ParticipantIds);

        reward.TotalAmount = reward.NetCash + reward.NetGold;
        reward.GangShareAmount =
            reward.GangShareCash + reward.GangShareGold;

        Dictionary<string, decimal> combinedShares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);

        foreach (string participantId in reward.ParticipantIds)
        {
            decimal cash;
            decimal gold;

            if (!reward.MemberCashShares.TryGetValue(
                participantId,
                out cash))
            {
                cash = 0m;
                reward.MemberCashShares[participantId] = 0m;
            }

            if (!reward.MemberGoldShares.TryGetValue(
                participantId,
                out gold))
            {
                gold = 0m;
                reward.MemberGoldShares[participantId] = 0m;
            }

            combinedShares[participantId] =
                RoundCash(Math.Max(0m, cash) + Math.Max(0m, gold));
        }

        reward.MemberShares = combinedShares;
    }

    private static List<string> NormalizeParticipantIds(
        IEnumerable<string> participantIds)
    {
        if (participantIds == null)
        {
            return new List<string>();
        }

        return participantIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();
    }

    private static Dictionary<string, decimal> SplitEqually(
        decimal amount,
        List<string> participantIds)
    {
        Dictionary<string, decimal> shares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);

        if (participantIds == null || participantIds.Count == 0)
        {
            return shares;
        }

        decimal totalCents = RoundCash(amount) * 100m;
        decimal centsPerParticipant =
            decimal.Floor(totalCents / participantIds.Count);
        int remainingCents =
            (int)(totalCents -
                centsPerParticipant * participantIds.Count);

        for (int i = 0; i < participantIds.Count; i++)
        {
            decimal cents = centsPerParticipant;
            if (i < remainingCents)
            {
                cents += 1m;
            }

            shares[participantIds[i]] = cents / 100m;
        }

        return shares;
    }

    private static decimal GetNetAmount(decimal gross, decimal cost)
    {
        decimal net = RoundCash(gross) - RoundCash(cost);
        return net > 0m ? net : 0m;
    }

    private static decimal GetGangHalf(decimal amount)
    {
        decimal totalCents = RoundCash(amount) * 100m;
        return decimal.Floor(totalCents / 2m) / 100m;
    }

    private static decimal RoundCash(decimal amount)
    {
        return decimal.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero);
    }

    #endregion

    #region Gang Milestones

    public void CompleteMilestone(string milestoneName)
    {
        if (string.IsNullOrWhiteSpace(milestoneName) ||
            IsMilestoneCompleted(milestoneName))
        {
            return;
        }

        GangMilestone milestone = new GangMilestone
        {
            MilestoneId = Guid.NewGuid().ToString("N"),
            MilestoneName = milestoneName,
            CompletedAtUtc = DateTime.UtcNow,
            Description = $"Gang milestone: {milestoneName}",
            UnlockedCapabilities = new Dictionary<string, string>()
        };

        switch (milestoneName)
        {
            case "FirstMemberRecruited":
                milestone.UnlockedCapabilities["CanDoJobs"] = "true";
                break;
            case "CompletedFiveJobs":
                milestone.UnlockedCapabilities["CanUpgradeHideout"] = "true";
                break;
            case "BankerInteractionInitiated":
                milestone.UnlockedCapabilities["CanUseBankDestination"] = "true";
                break;
            case "FenceMeetsGang":
                milestone.UnlockedCapabilities["CanSellStolenGoods"] = "true";
                break;
            case "HideoutUpgraded":
                milestone.UnlockedCapabilities["UpgradedHideout"] = "true";
                break;
        }

        milestones.Add(milestone.MilestoneId, milestone);
    }

    public GangMilestone GetMilestone(string milestoneId)
    {
        if (string.IsNullOrWhiteSpace(milestoneId))
        {
            return null;
        }

        GangMilestone milestone;
        return milestones.TryGetValue(milestoneId, out milestone)
            ? milestone
            : null;
    }

    public List<GangMilestone> GetAllMilestones()
    {
        return milestones.Values.ToList();
    }

    public bool IsMilestoneCompleted(string milestoneName)
    {
        return !string.IsNullOrWhiteSpace(milestoneName) &&
            milestones.Values.Any(
                milestone => milestone != null &&
                    milestone.MilestoneName == milestoneName);
    }

    public bool CanUseBankDestination()
    {
        return IsMilestoneCompleted("BankerInteractionInitiated");
    }

    public bool CanSellStolenGoods()
    {
        return IsMilestoneCompleted("FenceMeetsGang");
    }

    #endregion

    #region Persistence
```
