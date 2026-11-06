<!-- GangManagementSystem.cs lines 501-750 of 1186 -->
```csharp
        PendingJobReward reward;
        return pendingRewards.TryGetValue(rewardId, out reward)
            ? reward
            : null;
    }

    public List<PendingJobReward> GetAllPendingRewards()
    {
        return pendingRewards.Values
            .Where(reward => reward != null && !reward.IsSettled)
            .ToList();
    }

    public List<PendingJobReward> GetUnsettledRewardsForHideout(
        string hideoutId)
    {
        return pendingRewards.Values
            .Where(reward =>
                reward != null &&
                !reward.IsSettled &&
                reward.DestinationId == hideoutId)
            .ToList();
    }

    public void SettleReward(string rewardId)
    {
        PendingJobReward reward = GetPendingReward(rewardId);
        if (reward == null)
        {
            throw new KeyNotFoundException(
                $"Pending reward {rewardId} not found.");
        }

        if (reward.IsSettled)
        {
            return;
        }

        NormalizePendingReward(reward);
        if (!IsValidPendingReward(reward))
        {
            throw new InvalidOperationException(
                "The pending reward ledger is invalid; no payout was made.");
        }

        PrepareParticipantAccounts(reward);

        string destinationType =
            reward.DestinationType ?? "hideout_chest";

        if (destinationType == "hideout_chest")
        {
            SettleToHideoutChest(reward);
        }
        else if (destinationType == "bank_account")
        {
            if (!CanUseBankDestination())
            {
                throw new InvalidOperationException(
                    "The bank reward destination has not been unlocked.");
            }

            SettleToBank(reward);
        }
        else
        {
            throw new InvalidOperationException(
                $"Unsupported reward destination: {destinationType}.");
        }

        reward.IsSettled = true;
        reward.SettledAtUtc = DateTime.UtcNow;
    }

    private void SettleToHideoutChest(PendingJobReward reward)
    {
        string hideoutId = string.IsNullOrWhiteSpace(reward.DestinationId)
            ? ActiveHideoutId
            : reward.DestinationId;

        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new InvalidOperationException(
                "A valid hideout is required before this reward can be settled.");
        }

        EnsureChest(hideout);
        hideout.ChestInventory.Cash += reward.GangShareCash;
        hideout.ChestInventory.Gold += reward.GangShareGold;
        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
        DepositParticipantShares(reward);
    }

    private void SettleToBank(PendingJobReward reward)
    {
        string accountId = string.IsNullOrWhiteSpace(reward.DestinationId)
            ? "gang_reserve"
            : reward.DestinationId;

        bankSystem.EnsureAccount(accountId, "Gang Reserve");
        bankSystem.DepositMoney(
            accountId,
            reward.GangShareCash,
            reward.GangShareGold);

        DepositParticipantShares(reward);
    }

    private void PrepareParticipantAccounts(PendingJobReward reward)
    {
        foreach (string participantId in reward.ParticipantIds)
        {
            GangMember member = GetMember(participantId);
            string ownerName = member == null ||
                string.IsNullOrWhiteSpace(member.Name)
                ? participantId
                : member.Name;

            bankSystem.EnsureAccount(participantId, ownerName);
        }

        if (reward.DestinationType == "bank_account")
        {
            string accountId = string.IsNullOrWhiteSpace(reward.DestinationId)
                ? "gang_reserve"
                : reward.DestinationId;

            bankSystem.EnsureAccount(accountId, "Gang Reserve");
        }
    }

    private void DepositParticipantShares(PendingJobReward reward)
    {
        foreach (string participantId in reward.ParticipantIds)
        {
            decimal cash = reward.MemberCashShares[participantId];
            decimal gold = reward.MemberGoldShares[participantId];

            if (cash > 0m || gold > 0m)
            {
                bankSystem.DepositMoney(participantId, cash, gold);

                GangMember member = GetMember(participantId);
                if (member != null)
                {
                    member.Loyalty = Clamp(member.Loyalty + 5, 0, 100);
                }
            }
        }
    }

    private static bool IsValidPendingReward(PendingJobReward reward)
    {
        if (reward == null ||
            string.IsNullOrWhiteSpace(reward.JobId) ||
            reward.NetCash < 0m ||
            reward.NetGold < 0m ||
            reward.GangShareCash < 0m ||
            reward.GangShareGold < 0m ||
            reward.GrossCash < 0m ||
            reward.GrossGold < 0m ||
            reward.JobCostCash < 0m ||
            reward.JobCostGold < 0m ||
            reward.ParticipantIds == null ||
            reward.ParticipantIds.Count == 0 ||
            reward.MemberCashShares == null ||
            reward.MemberGoldShares == null)
        {
            return false;
        }

        decimal expectedNetCash = GetNetAmount(
            reward.GrossCash,
            reward.JobCostCash);
        decimal expectedNetGold = GetNetAmount(
            reward.GrossGold,
            reward.JobCostGold);

        if (reward.NetCash != expectedNetCash ||
            reward.NetGold != expectedNetGold ||
            reward.GangShareCash != GetGangHalf(reward.NetCash) ||
            reward.GangShareGold != GetGangHalf(reward.NetGold))
        {
            return false;
        }

        List<string> participants = NormalizeParticipantIds(
            reward.ParticipantIds);

        if (participants.Count == 0 ||
            participants.Count != reward.ParticipantIds.Count ||
            !participants.SequenceEqual(
                reward.ParticipantIds,
                StringComparer.Ordinal))
        {
            return false;
        }

        Dictionary<string, decimal> expectedCash = SplitEqually(
            reward.NetCash - reward.GangShareCash,
            participants);
        Dictionary<string, decimal> expectedGold = SplitEqually(
            reward.NetGold - reward.GangShareGold,
            participants);

        return DictionariesMatch(
                   expectedCash,
                   reward.MemberCashShares) &&
               DictionariesMatch(
                   expectedGold,
                   reward.MemberGoldShares) &&
               RoundCash(reward.GangShareCash +
                   reward.MemberCashShares.Values.Sum()) ==
                   RoundCash(reward.NetCash) &&
               RoundCash(reward.GangShareGold +
                   reward.MemberGoldShares.Values.Sum()) ==
                   RoundCash(reward.NetGold);
    }

    private static bool DictionariesMatch(
        Dictionary<string, decimal> expected,
        Dictionary<string, decimal> actual)
    {
        if (expected == null || actual == null ||
            expected.Count != actual.Count)
        {
            return false;
        }

        foreach (KeyValuePair<string, decimal> pair in expected)
        {
            decimal actualAmount;
            if (!actual.TryGetValue(pair.Key, out actualAmount) ||
                actualAmount != pair.Value)
            {
                return false;
            }
        }

        return true;
    }

    private static void NormalizePendingReward(PendingJobReward reward)
    {
        if (reward.MemberShares == null)
        {
            reward.MemberShares =
                new Dictionary<string, decimal>(StringComparer.Ordinal);
        }
```
