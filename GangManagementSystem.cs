using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class GangManagementSystem
{
    private readonly Dictionary<string, GangMember> gangMembers =
        new Dictionary<string, GangMember>(StringComparer.Ordinal);
    private readonly Dictionary<string, Hideout> hideouts =
        new Dictionary<string, Hideout>(StringComparer.Ordinal);
    private readonly Dictionary<string, PendingJobReward> pendingRewards =
        new Dictionary<string, PendingJobReward>(StringComparer.Ordinal);
    private readonly Dictionary<string, GangMilestone> milestones =
        new Dictionary<string, GangMilestone>(StringComparer.Ordinal);
    private readonly GangJobPayoutSystem payoutSystem;
    private readonly BankSystem bankSystem;

    public string ActiveHideoutId { get; set; }

    public GangManagementSystem()
        : this(new GangJobPayoutSystem(), new BankSystem())
    {
    }

    public GangManagementSystem(
        GangJobPayoutSystem payoutSystem,
        BankSystem bankSystem)
    {
        this.payoutSystem = payoutSystem ??
            throw new ArgumentNullException(nameof(payoutSystem));
        this.bankSystem = bankSystem ??
            throw new ArgumentNullException(nameof(bankSystem));
    }

    #region Gang Member Management

    public void AddMember(GangMember member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        if (string.IsNullOrWhiteSpace(member.MemberId))
        {
            throw new ArgumentException(
                "A gang member must have a non-empty member ID.",
                nameof(member));
        }

        if (gangMembers.ContainsKey(member.MemberId))
        {
            throw new InvalidOperationException(
                $"Gang member {member.MemberId} already exists.");
        }

        gangMembers.Add(member.MemberId, member);
    }

    // Retains GangManager's upsert behavior for existing callers.
    public void RegisterMember(GangMember member)
    {
        if (member == null || string.IsNullOrWhiteSpace(member.MemberId))
        {
            return;
        }

        gangMembers[member.MemberId] = member;
    }

    public void RemoveMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId) ||
            !gangMembers.Remove(memberId))
        {
            throw new KeyNotFoundException(
                $"Gang member {memberId} not found.");
        }
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
        {
            return null;
        }

        GangMember member;
        return gangMembers.TryGetValue(memberId, out member)
            ? member
            : null;
    }

    public List<GangMember> GetMembers()
    {
        return gangMembers.Values.ToList();
    }

    public List<GangMember> GetAllMembers()
    {
        return GetMembers();
    }

    public List<GangMember> GetMembersByRole(string role)
    {
        return gangMembers.Values
            .Where(member => member != null && member.Role == role)
            .ToList();
    }

    public int GetMemberCount()
    {
        return gangMembers.Count;
    }

    public bool MemberExists(string memberId)
    {
        return !string.IsNullOrWhiteSpace(memberId) &&
            gangMembers.ContainsKey(memberId);
    }

    public void UpdateMemberRole(string memberId, string newRole)
    {
        GangMember member = GetRequiredMember(memberId);
        member.Role = newRole;
    }

    public void UpdateMemberLoyalty(string memberId, int loyaltyChange)
    {
        GangMember member = GetRequiredMember(memberId);
        member.Loyalty = Clamp(member.Loyalty + loyaltyChange, 0, 100);
    }

    public void UpdateMemberTask(string memberId, string task)
    {
        GangMember member = GetRequiredMember(memberId);
        member.CurrentTask = task;
    }

    // Absorbed from GangManager.
    public void ApplyTacticalContext(
        string memberId,
        string combatStyle,
        string currentTask,
        bool followingPlayer)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
        {
            return;
        }

        member.CombatStyle = combatStyle;
        member.CurrentTask = currentTask;
        member.IsFollowingPlayer = followingPlayer;
    }

    private GangMember GetRequiredMember(string memberId)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
        {
            throw new KeyNotFoundException(
                $"Gang member {memberId} not found.");
        }

        return member;
    }

    #endregion

    #region Hideout Management

    public void CreateHideout(
        string hideoutId,
        string locationName,
        float positionX,
        float positionY,
        float positionZ,
        string stage)
    {
        if (string.IsNullOrWhiteSpace(hideoutId))
        {
            throw new ArgumentException(
                "A hideout must have a non-empty ID.",
                nameof(hideoutId));
        }

        if (hideouts.ContainsKey(hideoutId))
        {
            throw new InvalidOperationException(
                $"Hideout {hideoutId} already exists.");
        }

        hideouts.Add(hideoutId, new Hideout
        {
            HideoutId = hideoutId,
            LocationName = locationName,
            PositionX = positionX,
            PositionY = positionY,
            PositionZ = positionZ,
            Stage = stage,
            ChestInventory = CreateEmptyChest(),
            CreatedAtUtc = DateTime.UtcNow
        });
    }

    public Hideout GetHideout(string hideoutId)
    {
        if (string.IsNullOrWhiteSpace(hideoutId))
        {
            return null;
        }

        Hideout hideout;
        return hideouts.TryGetValue(hideoutId, out hideout)
            ? hideout
            : null;
    }

    public List<Hideout> GetAllHideouts()
    {
        return hideouts.Values.ToList();
    }

    public Hideout GetActiveHideout()
    {
        return GetHideout(ActiveHideoutId);
    }

    public void SetActiveHideout(string hideoutId)
    {
        if (!hideouts.ContainsKey(hideoutId))
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }

        ActiveHideoutId = hideoutId;
    }

    public void UpgradeHideoutStage(string hideoutId, string newStage)
    {
        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }

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

    public GangManagementData ExportManagementData()
    {
        return new GangManagementData
        {
            Hideouts = hideouts.Values.ToList(),
            PendingRewards = pendingRewards.Values.ToList(),
            Milestones = milestones.Values.ToList(),
            ActiveHideoutId = ActiveHideoutId
        };
    }

    public void LoadFromData(GangManagementData data)
    {
        if (data == null)
        {
            return;
        }

        hideouts.Clear();
        pendingRewards.Clear();
        milestones.Clear();

        if (data.Hideouts != null)
        {
            foreach (Hideout hideout in data.Hideouts)
            {
                if (hideout == null ||
                    string.IsNullOrWhiteSpace(hideout.HideoutId))
                {
                    continue;
                }

                EnsureChest(hideout);
                hideouts[hideout.HideoutId] = hideout;
            }
        }

        if (data.PendingRewards != null)
        {
            foreach (PendingJobReward reward in data.PendingRewards)
            {
                if (reward == null ||
                    string.IsNullOrWhiteSpace(reward.RewardId))
                {
                    continue;
                }

                NormalizePendingReward(reward);
                pendingRewards[reward.RewardId] = reward;
            }
        }

        if (data.Milestones != null)
        {
            foreach (GangMilestone milestone in data.Milestones)
            {
                if (milestone == null ||
                    string.IsNullOrWhiteSpace(milestone.MilestoneId))
                {
                    continue;
                }

                if (milestone.UnlockedCapabilities == null)
                {
                    milestone.UnlockedCapabilities =
                        new Dictionary<string, string>();
                }

                milestones[milestone.MilestoneId] = milestone;
            }
        }

        ActiveHideoutId = hideouts.ContainsKey(data.ActiveHideoutId)
            ? data.ActiveHideoutId
            : null;
    }

    // Absorbed from GangManager; SaveRoot remains the repository's
    // persistence contract for the roster and gang-management data.
    public SaveRoot ExportSaveData()
    {
        return new SaveRoot
        {
            GangMembers = GetMembers(),
            GangManagement = ExportManagementData()
        };
    }

    public void ImportSaveData(SaveRoot data)
    {
        gangMembers.Clear();

        if (data == null)
        {
            LoadFromData(null);
            return;
        }

        if (data.GangMembers != null)
        {
            foreach (GangMember member in data.GangMembers)
            {
                if (member == null ||
                    string.IsNullOrWhiteSpace(member.MemberId))
                {
                    continue;
                }

                gangMembers[member.MemberId] = member;
            }
        }

        LoadFromData(data.GangManagement);
    }

    public void WriteToSaveRoot(SaveRoot data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        data.SaveVersion = Math.Max(data.SaveVersion, 6);
        data.GangMembers = GetMembers();
        data.GangManagement = ExportManagementData();
    }

    public void LoadFromSaveRoot(SaveRoot data)
    {
        ImportSaveData(data);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            ExportSaveData(),
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            ImportSaveData(new SaveRoot());
            return;
        }

        try
        {
            SaveRoot saveRoot =
                JsonConvert.DeserializeObject<SaveRoot>(json);
            if (saveRoot != null)
            {
                ImportSaveData(saveRoot);
                return;
            }
        }
        catch (JsonException)
        {
            // Try the legacy GangManager format below.
        }

        try
        {
            List<GangMember> legacyMembers =
                JsonConvert.DeserializeObject<List<GangMember>>(json);
            SaveRoot legacySave = new SaveRoot
            {
                GangMembers = legacyMembers ?? new List<GangMember>()
            };
            ImportSaveData(legacySave);
        }
        catch (JsonException)
        {
            // Leave the current in-memory state unchanged for invalid data.
        }
    }

    #endregion

    private static int Clamp(int value, int minimum, int maximum)
    {
        return Math.Max(minimum, Math.Min(maximum, value));
    }
}