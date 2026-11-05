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
        if (payoutSystem == null)
        {
            throw new ArgumentNullException(nameof(payoutSystem));
        }

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
            throw new InvalidOperationException(
                "No active hideout set.");
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
            throw new InvalidOperationException(
                "No active hideout set.");
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
            args.JobData.Participants == null ||
            args.JobData.Participants.Count == 0)
        {
            return;
        }

        JobData job = args.JobData;

        // A repeated completion event must not create another pending reward.
        if (pendingRewards.Values.Any(
            reward => reward != null && reward.JobId == job.JobId))
        {
            return;
        }

        List<string> participants = job.Participants
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        if (participants.Count == 0)
        {
            return;
        }

        decimal totalAmount = RoundCash(job.RewardCash + job.RewardGold);
        if (totalAmount <= 0m)
        {
            return;
        }

        decimal gangShare = RoundCash(totalAmount / 2m);
        decimal participantPool = totalAmount - gangShare;
        Dictionary<string, decimal> shares =
            SplitEqually(participantPool, participants);

        PendingJobReward rewardRecord = new PendingJobReward
        {
            RewardId = Guid.NewGuid().ToString("N"),
            JobId = job.JobId,
            TotalAmount = totalAmount,
            GangShareAmount = gangShare,
            MemberShares = shares,
            ParticipantIds = participants,
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

        string destinationType = reward.DestinationType ?? "hideout_chest";

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
        hideout.ChestInventory.Cash += reward.GangShareAmount;
        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;

        DepositParticipantShares(reward);
    }

    private void SettleToBank(PendingJobReward reward)
    {
        string accountId = string.IsNullOrWhiteSpace(reward.DestinationId)
            ? "gang_reserve"
            : reward.DestinationId;

        bankSystem.EnsureAccount(accountId, "Gang Reserve");
        bankSystem.DepositCash(accountId, reward.GangShareAmount);

        DepositParticipantShares(reward);
    }

    private void DepositParticipantShares(PendingJobReward reward)
    {
        if (reward.MemberShares == null)
        {
            return;
        }

        foreach (KeyValuePair<string, decimal> share in reward.MemberShares)
        {
            if (string.IsNullOrWhiteSpace(share.Key) || share.Value <= 0m)
            {
                continue;
            }

            GangMember member = GetMember(share.Key);
            string ownerName = member == null ||
                string.IsNullOrWhiteSpace(member.Name)
                    ? share.Key
                    : member.Name;

            bankSystem.EnsureAccount(share.Key, ownerName);
            bankSystem.DepositCash(share.Key, share.Value);

            if (member != null)
            {
                member.Loyalty = Clamp(member.Loyalty + 5, 0, 100);
            }
        }
    }

    private static Dictionary<string, decimal> SplitEqually(
        decimal amount,
        List<string> participantIds)
    {
        Dictionary<string, decimal> shares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);

        if (amount <= 0m ||
            participantIds == null ||
            participantIds.Count == 0)
        {
            return shares;
        }

        decimal totalCents = amount * 100m;
        decimal centsPerParticipant =
            decimal.Floor(totalCents / participantIds.Count);
        int remainingCents =
            (int)(totalCents - centsPerParticipant * participantIds.Count);

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

                if (reward.MemberShares == null)
                {
                    reward.MemberShares =
                        new Dictionary<string, decimal>();
                }

                if (reward.ParticipantIds == null)
                {
                    reward.ParticipantIds = new List<string>();
                }

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
        SaveRoot data = new SaveRoot
        {
            GangMembers = GetMembers(),
            GangManagement = ExportManagementData()
        };

        return data;
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
                GangMembers = legacyMembers ??
                    new List<GangMember>()
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