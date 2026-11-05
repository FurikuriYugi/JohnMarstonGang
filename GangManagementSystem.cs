using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class GangManagementSystem
{
    private Dictionary<string, GangMember> gangMembers = new Dictionary<string, GangMember>();
    private Dictionary<string, Hideout> hideouts = new Dictionary<string, Hideout>();
    private Dictionary<string, PendingJobReward> pendingRewards = new Dictionary<string, PendingJobReward>();
    private Dictionary<string, GangMilestone> milestones = new Dictionary<string, GangMilestone>();
    private GangJobPayoutSystem payoutSystem;
    private BankSystem bankSystem;

    public string ActiveHideoutId { get; set; }

    public GangManagementSystem(GangJobPayoutSystem payoutSystem, BankSystem bankSystem)
    {
        this.payoutSystem = payoutSystem ?? throw new ArgumentNullException(nameof(payoutSystem));
        this.bankSystem = bankSystem ?? throw new ArgumentNullException(nameof(bankSystem));
    }

    #region Gang Member Management

    public void AddMember(GangMember member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        if (gangMembers.ContainsKey(member.MemberId))
        {
            throw new InvalidOperationException($"Gang member {member.MemberId} already exists.");
        }

        gangMembers[member.MemberId] = member;
    }

    public void RemoveMember(string memberId)
    {
        if (!gangMembers.ContainsKey(memberId))
        {
            throw new KeyNotFoundException($"Gang member {memberId} not found.");
        }

        gangMembers.Remove(memberId);
    }

    public GangMember GetMember(string memberId)
    {
        return gangMembers.ContainsKey(memberId) ? gangMembers[memberId] : null;
    }

    public List<GangMember> GetAllMembers()
    {
        return new List<GangMember>(gangMembers.Values);
    }

    public List<GangMember> GetMembersByRole(string role)
    {
        return gangMembers.Values.Where(m => m.Role == role).ToList();
    }

    public int GetMemberCount()
    {
        return gangMembers.Count;
    }

    public bool MemberExists(string memberId)
    {
        return gangMembers.ContainsKey(memberId);
    }

    public void UpdateMemberRole(string memberId, string newRole)
    {
        if (!gangMembers.ContainsKey(memberId))
        {
            throw new KeyNotFoundException($"Gang member {memberId} not found.");
        }

        gangMembers[memberId].Role = newRole;
    }

    public void UpdateMemberLoyalty(string memberId, int loyaltyChange)
    {
        if (!gangMembers.ContainsKey(memberId))
        {
            throw new KeyNotFoundException($"Gang member {memberId} not found.");
        }

        var member = gangMembers[memberId];
        member.Loyalty = Math.Max(0, Math.Min(100, member.Loyalty + loyaltyChange));
    }

    public void UpdateMemberTask(string memberId, string task)
    {
        if (!gangMembers.ContainsKey(memberId))
        {
            throw new KeyNotFoundException($"Gang member {memberId} not found.");
        }

        gangMembers[memberId].CurrentTask = task;
    }

    #endregion

    #region Hideout Management

    public void CreateHideout(string hideoutId, string locationName, float positionX, float positionY, float positionZ, string stage)
    {
        if (hideouts.ContainsKey(hideoutId))
        {
            throw new InvalidOperationException($"Hideout {hideoutId} already exists.");
        }

        var hideout = new Hideout
        {
            HideoutId = hideoutId,
            LocationName = locationName,
            PositionX = positionX,
            PositionY = positionY,
            PositionZ = positionZ,
            Stage = stage,
            ChestInventory = new ChestInventory
            {
                Cash = 0,
                Gold = 0,
                StolenItems = new List<string>(),
                Valuables = new List<string>(),
                LastUpdatedUtc = DateTime.UtcNow
            },
            CreatedAtUtc = DateTime.UtcNow
        };

        hideouts[hideoutId] = hideout;
    }

    public Hideout GetHideout(string hideoutId)
    {
        return hideouts.ContainsKey(hideoutId) ? hideouts[hideoutId] : null;
    }

    public List<Hideout> GetAllHideouts()
    {
        return new List<Hideout>(hideouts.Values);
    }

    public Hideout GetActiveHideout()
    {
        return string.IsNullOrEmpty(ActiveHideoutId) ? null : GetHideout(ActiveHideoutId);
    }

    public void SetActiveHideout(string hideoutId)
    {
        if (!hideouts.ContainsKey(hideoutId))
        {
            throw new KeyNotFoundException($"Hideout {hideoutId} not found.");
        }

        ActiveHideoutId = hideoutId;
    }

    public void UpgradeHideoutStage(string hideoutId, string newStage)
    {
        if (!hideouts.ContainsKey(hideoutId))
        {
            throw new KeyNotFoundException($"Hideout {hideoutId} not found.");
        }

        hideouts[hideoutId].Stage = newStage;
        CompleteMilestone("HideoutUpgraded");
    }

    #endregion

    #region Hideout Chest Management

    public void DepositToCommunalChest(decimal cash, decimal gold, List<string> stolenItems = null)
    {
        var activeHideout = GetActiveHideout();
        if (activeHideout == null)
        {
            throw new InvalidOperationException("No active hideout set.");
        }

        var chest = activeHideout.ChestInventory;
        chest.Cash += cash;
        chest.Gold += gold;

        if (stolenItems != null && stolenItems.Count > 0)
        {
            chest.StolenItems.AddRange(stolenItems);
        }

        chest.LastUpdatedUtc = DateTime.UtcNow;
    }

    public bool WithdrawFromCommunalChest(decimal cash, decimal gold)
    {
        var activeHideout = GetActiveHideout();
        if (activeHideout == null)
        {
            throw new InvalidOperationException("No active hideout set.");
        }

        var chest = activeHideout.ChestInventory;

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
        var hideout = GetHideout(hideoutId);
        return hideout?.ChestInventory;
    }

    public void AddStolenItemsToChest(string hideoutId, List<string> stolenItems)
    {
        var hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException($"Hideout {hideoutId} not found.");
        }

        hideout.ChestInventory.StolenItems.AddRange(stolenItems);
        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
    }

    public void RemoveStolenItemsFromChest(string hideoutId, List<string> stolenItems)
    {
        var hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException($"Hideout {hideoutId} not found.");
        }

        foreach (var item in stolenItems)
        {
            hideout.ChestInventory.StolenItems.Remove(item);
        }

        hideout.ChestInventory.LastUpdatedUtc = DateTime.UtcNow;
    }

    #endregion

    #region Job Completion and Reward Ledger

    public void OnJobCompleted(object sender, JobCompletedEventArgs args)
    {
        if (args == null || args.JobData == null)
        {
            return;
        }

        var jobData = args.JobData;

        if (jobData.Participants == null || jobData.Participants.Count == 0)
        {
            return;
        }

        var aliveParticipants = jobData.Participants.Where(p => gangMembers.ContainsKey(p)).ToList();

        if (aliveParticipants.Count == 0)
        {
            return;
        }

        var totalAmount = jobData.RewardCash + jobData.RewardGold;

        var shares = payoutSystem.CalculatePayouts(
            totalAmount,
            aliveParticipants,
            new Dictionary<string, int>()
        );

        var reward = new PendingJobReward
        {
            RewardId = Guid.NewGuid().ToString(),
            JobId = jobData.JobId,
            TotalAmount = totalAmount,
            GangShareAmount = shares.ContainsKey("gang_fund") ? shares["gang_fund"] : 0,
            MemberShares = new Dictionary<string, decimal>(),
            ParticipantIds = new List<string>(aliveParticipants),
            DestinationType = "hideout_chest",
            DestinationId = ActiveHideoutId,
            IsSettled = false,
            CreatedAtUtc = DateTime.UtcNow,
            SettledAtUtc = DateTime.MinValue
        };

        foreach (var participantId in aliveParticipants)
        {
            if (shares.ContainsKey(participantId))
            {
                reward.MemberShares[participantId] = shares[participantId];
            }
        }

        pendingRewards[reward.RewardId] = reward;
    }

    public PendingJobReward GetPendingReward(string rewardId)
    {
        return pendingRewards.ContainsKey(rewardId) ? pendingRewards[rewardId] : null;
    }

    public List<PendingJobReward> GetAllPendingRewards()
    {
        return pendingRewards.Values.Where(r => !r.IsSettled).ToList();
    }

    public List<PendingJobReward> GetUnsettledRewardsForHideout(string hideoutId)
    {
        return pendingRewards.Values
            .Where(r => !r.IsSettled && r.DestinationId == hideoutId)
            .ToList();
    }

    public void SettleReward(string rewardId)
    {
        if (!pendingRewards.ContainsKey(rewardId))
        {
            throw new KeyNotFoundException($"Pending reward {rewardId} not found.");
        }

        var reward = pendingRewards[rewardId];

        if (reward.IsSettled)
        {
            return;
        }

        if (reward.DestinationType == "hideout_chest")
        {
            SettleToHideoutChest(reward);
        }
        else if (reward.DestinationType == "bank_account")
        {
            SettleToBank(reward);
        }

        reward.IsSettled = true;
        reward.SettledAtUtc = DateTime.UtcNow;
    }

    private void SettleToHideoutChest(PendingJobReward reward)
    {
        var hideout = GetHideout(reward.DestinationId);
        if (hideout == null)
        {
            throw new InvalidOperationException($"Hideout {reward.DestinationId} not found for reward settlement.");
        }

        var chest = hideout.ChestInventory;
        chest.Cash += reward.GangShareAmount;

        foreach (var memberShare in reward.MemberShares)
        {
            var member = GetMember(memberShare.Key);
            if (member != null)
            {
                member.Loyalty = Math.Min(100, member.Loyalty + 5);
            }
        }

        chest.LastUpdatedUtc = DateTime.UtcNow;
    }

    private void SettleToBank(PendingJobReward reward)
    {
        var bankAccountId = $"gang_reserve_{reward.DestinationId}";

        var account = new BankAccountData
        {
            OwnerId = bankAccountId,
            OwnerName = "Gang Reserve",
            Cash = reward.GangShareAmount,
            Gold = 0,
            CreditLimit = 5000,
            Debt = 0,
            IsSuspicious = false,
            LastTransactionUtc = DateTime.UtcNow
        };

        try
        {
            bankSystem.CreateAccount(account.OwnerId, account.OwnerName, account.CreditLimit);
            bankSystem.Deposit(account.OwnerId, reward.GangShareAmount, 0);
        }
        catch
        {
            bankSystem.Deposit(bankAccountId, reward.GangShareAmount, 0);
        }

        foreach (var memberShare in reward.MemberShares)
        {
            var member = GetMember(memberShare.Key);
            if (member != null)
            {
                member.Loyalty = Math.Min(100, member.Loyalty + 5);
            }
        }
    }

    #endregion

    #region Gang Milestones

    public void CompleteMilestone(string milestoneName)
    {
        var milestoneId = Guid.NewGuid().ToString();

        var milestone = new GangMilestone
        {
            MilestoneId = milestoneId,
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

        milestones[milestoneId] = milestone;
    }

    public GangMilestone GetMilestone(string milestoneId)
    {
        return milestones.ContainsKey(milestoneId) ? milestones[milestoneId] : null;
    }

    public List<GangMilestone> GetAllMilestones()
    {
        return new List<GangMilestone>(milestones.Values);
    }

    public bool IsMilestoneCompleted(string milestoneName)
    {
        return milestones.Values.Any(m => m.MilestoneName == milestoneName);
    }

    public bool CanUseBankDestination()
    {
        var milestone = milestones.Values.FirstOrDefault(m => m.MilestoneName == "BankerInteractionInitiated");
        return milestone != null;
    }

    public bool CanSellStolenGoods()
    {
        var milestone = milestones.Values.FirstOrDefault(m => m.MilestoneName == "FenceMeetsGang");
        return milestone != null;
    }

    #endregion

    #region Data Persistence

    public void LoadFromData(GangManagementData data)
    {
        if (data == null)
        {
            return;
        }

        hideouts.Clear();
        if (data.Hideouts != null)
        {
            foreach (var hideout in data.Hideouts)
            {
                hideouts[hideout.HideoutId] = hideout;
            }
        }

        pendingRewards.Clear();
        if (data.PendingRewards != null)
        {
            foreach (var reward in data.PendingRewards)

