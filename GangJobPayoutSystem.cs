using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public sealed class GangJobPayoutMember
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal ContributionWeight { get; set; }
}

public sealed class GangJobPayoutShare
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal ContributionWeight { get; set; }
    public decimal Amount { get; set; }
}

public sealed class GangJobPayoutPlan
{
    public string JobId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal GangReserveAmount { get; set; }
    public List<GangJobPayoutShare> MemberShares { get; set; }

    public GangJobPayoutPlan()
    {
        MemberShares = new List<GangJobPayoutShare>();
    }
}

public sealed class GangJobPayoutState
{
    public List<string> SettledJobIds { get; set; }

    public GangJobPayoutState()
    {
        SettledJobIds = new List<string>();
    }
}

public sealed class GangJobPayoutSystem
{
    private readonly HashSet<string> _settledJobIds =
        new HashSet<string>(StringComparer.Ordinal);

    public GangJobPayoutPlan CreatePlan(
        string jobId,
        decimal totalAmount,
        decimal reservePercent,
        IEnumerable<GangJobPayoutMember> members)
    {
        if (string.IsNullOrWhiteSpace(jobId) ||
            totalAmount <= 0m ||
            reservePercent < 0m ||
            reservePercent > 100m)
        {
            return null;
        }

        decimal roundedTotal = RoundCash(totalAmount);
        if (roundedTotal <= 0m)
            return null;

        List<GangJobPayoutMember> validMembers =
            NormalizeMembers(members);

        decimal reserveAmount = RoundCash(
            roundedTotal * reservePercent / 100m);

        if (reserveAmount > roundedTotal)
            reserveAmount = roundedTotal;

        decimal memberPool = roundedTotal - reserveAmount;

        if (memberPool > 0m && validMembers.Count == 0)
            return null;

        List<GangJobPayoutShare> shares =
            SplitMemberPool(memberPool, validMembers);

        return new GangJobPayoutPlan
        {
            JobId = jobId,
            TotalAmount = roundedTotal,
            GangReserveAmount = reserveAmount,
            MemberShares = shares
        };
    }

    public bool IsJobSettled(string jobId)
    {
        return !string.IsNullOrWhiteSpace(jobId) &&
               _settledJobIds.Contains(jobId);
    }

    public bool TrySettlePlan(
        GangJobPayoutPlan plan,
        string gangReserveOwnerId,
        string gangReserveOwnerName,
        BankSystem bankSystem)
    {
        if (plan == null ||
            bankSystem == null ||
            string.IsNullOrWhiteSpace(plan.JobId) ||
            _settledJobIds.Contains(plan.JobId) ||
            !IsValidPlan(plan, gangReserveOwnerId))
        {
            return false;
        }

        if (plan.GangReserveAmount > 0m)
        {
            EnsureAccount(
                bankSystem,
                gangReserveOwnerId,
                gangReserveOwnerName);
        }

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share.Amount > 0m)
            {
                EnsureAccount(
                    bankSystem,
                    share.OwnerId,
                    share.OwnerName);
            }
        }

        if (plan.GangReserveAmount > 0m)
        {
            bankSystem.DepositCash(
                gangReserveOwnerId,
                plan.GangReserveAmount);
        }

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share.Amount > 0m)
                bankSystem.DepositCash(share.OwnerId, share.Amount);
        }

        _settledJobIds.Add(plan.JobId);
        return true;
    }

    public string SaveState()
    {
        GangJobPayoutState state = new GangJobPayoutState
        {
            SettledJobIds = _settledJobIds
                .OrderBy(x => x, StringComparer.Ordinal)
                .ToList()
        };

        return JsonConvert.SerializeObject(
            state,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            _settledJobIds.Clear();
            return;
        }

        GangJobPayoutState state;
        try
        {
            state =
                JsonConvert.DeserializeObject<GangJobPayoutState>(json);
        }
        catch (JsonException)
        {
            return;
        }

        if (state == null || state.SettledJobIds == null)
        {
            _settledJobIds.Clear();
            return;
        }

        HashSet<string> validIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (string jobId in state.SettledJobIds)
        {
            if (!string.IsNullOrWhiteSpace(jobId))
                validIds.Add(jobId);
        }

        _settledJobIds.Clear();
        foreach (string jobId in validIds)
            _settledJobIds.Add(jobId);
    }

    private List<GangJobPayoutMember> NormalizeMembers(
        IEnumerable<GangJobPayoutMember> members)
    {
        if (members == null)
            return new List<GangJobPayoutMember>();

        List<GangJobPayoutMember> validMembers =
            new List<GangJobPayoutMember>();

        HashSet<string> seenOwnerIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (GangJobPayoutMember member in members)
        {
            if (member == null ||
                string.IsNullOrWhiteSpace(member.OwnerId) ||
                member.ContributionWeight <= 0m ||
                !seenOwnerIds.Add(member.OwnerId))
            {
                continue;
            }

            validMembers.Add(new GangJobPayoutMember
            {
                OwnerId = member.OwnerId,
                OwnerName = member.OwnerName,
                ContributionWeight = member.ContributionWeight
            });
        }

        return validMembers;
    }

    private List<GangJobPayoutShare> SplitMemberPool(
        decimal memberPool,
        List<GangJobPayoutMember> members)
    {
        List<GangJobPayoutShare> shares =
            new List<GangJobPayoutShare>();

        if (memberPool <= 0m || members == null || members.Count == 0)
            return shares;

        decimal totalWeight = members.Sum(
            x => x.ContributionWeight);

        if (totalWeight <= 0m)
            return shares;

        decimal poolCents = memberPool * 100m;
        decimal allocatedCents = 0m;
        List<PayoutAllocation> allocations =
            new List<PayoutAllocation>();

        foreach (GangJobPayoutMember member in members)
        {
            decimal exactCents =
                poolCents * member.ContributionWeight / totalWeight;
            decimal wholeCents = decimal.Floor(exactCents);

            allocations.Add(new PayoutAllocation
            {
                Member = member,
                WholeCents = wholeCents,
                FractionalCents = exactCents - wholeCents
            });

            allocatedCents += wholeCents;
        }

        int remainingCents =
            (int)(poolCents - allocatedCents);

        List<PayoutAllocation> remainderOrder = allocations
            .OrderByDescending(x => x.FractionalCents)
            .ThenBy(x => x.Member.OwnerId, StringComparer.Ordinal)
            .ToList();

        for (int i = 0; i < remainingCents; i++)
            remainderOrder[i].WholeCents += 1m;

        foreach (PayoutAllocation allocation in allocations)
        {
            shares.Add(new GangJobPayoutShare
            {
                OwnerId = allocation.Member.OwnerId,
                OwnerName = allocation.Member.OwnerName,
                ContributionWeight =
                    allocation.Member.ContributionWeight,
                Amount = allocation.WholeCents / 100m
            });
        }

        return shares;
    }

    private bool IsValidPlan(
        GangJobPayoutPlan plan,
        string gangReserveOwnerId)
    {
        if (plan.TotalAmount <= 0m ||
            plan.GangReserveAmount < 0m ||
            plan.GangReserveAmount > plan.TotalAmount ||
            plan.MemberShares == null)
        {
            return false;
        }

        if (plan.GangReserveAmount > 0m &&
            string.IsNullOrWhiteSpace(gangReserveOwnerId))
        {
            return false;
        }

        decimal shareTotal = 0m;
        HashSet<string> seenOwnerIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share == null ||
                string.IsNullOrWhiteSpace(share.OwnerId) ||
                share.Amount < 0m ||
                share.ContributionWeight < 0m ||
                !seenOwnerIds.Add(share.OwnerId))
            {
                return false;
            }

            shareTotal += share.Amount;
        }

        return RoundCash(
            plan.GangReserveAmount + shareTotal) ==
            RoundCash(plan.TotalAmount);
    }

    private void EnsureAccount(
        BankSystem bankSystem,
        string ownerId,
        string ownerName)
    {
        if (bankSystem.GetAccount(ownerId) != null)
            return;

        bankSystem.RegisterAccount(new BankAccountData
        {
            OwnerId = ownerId,
            OwnerName = ownerName
        });
    }

    private decimal RoundCash(decimal amount)
    {
        return decimal.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero);
    }

    private sealed class PayoutAllocation
    {
        public GangJobPayoutMember Member { get; set; }
        public decimal WholeCents { get; set; }
        public decimal FractionalCents { get; set; }
    }
}