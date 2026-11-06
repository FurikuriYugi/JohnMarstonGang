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
    public decimal CashAmount { get; set; }
    public decimal GoldAmount { get; set; }
}

public sealed class GangJobPayoutPlan
{
    public string JobId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal GangReserveAmount { get; set; }
    public List<GangJobPayoutShare> MemberShares { get; set; }

    public decimal TotalCash { get; set; }
    public decimal TotalGold { get; set; }
    public decimal GangReserveCash { get; set; }
    public decimal GangReserveGold { get; set; }

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

    public GangJobPayoutPlan CreatePlan(JobData job)
    {
        if (job == null ||
            string.IsNullOrWhiteSpace(job.JobId) ||
            job.RewardCash < 0m ||
            job.RewardGold < 0m ||
            job.CostCash < 0m ||
            job.CostGold < 0m)
        {
            return null;
        }

        List<string> participantIds = NormalizeParticipantIds(job.Participants);
        if (participantIds.Count == 0)
        {
            return null;
        }

        decimal netCash = GetNetAmount(job.RewardCash, job.CostCash);
        decimal netGold = GetNetAmount(job.RewardGold, job.CostGold);

        if (netCash <= 0m && netGold <= 0m)
        {
            return null;
        }

        List<GangJobPayoutMember> members = participantIds
            .Select(id => new GangJobPayoutMember
            {
                OwnerId = id,
                OwnerName = id,
                ContributionWeight = 1m
            })
            .ToList();

        return BuildPlan(job.JobId, netCash, netGold, members);
    }

    // Compatibility overload. Reserve percentage and contribution weights no
    // longer control payouts: the reserve is fixed at 50% and shares are equal.
    public GangJobPayoutPlan CreatePlan(
        string jobId,
        decimal totalAmount,
        decimal reservePercent,
        IEnumerable<GangJobPayoutMember> members)
    {
        if (string.IsNullOrWhiteSpace(jobId) ||
            totalAmount <= 0m ||
            reservePercent != 50m)
        {
            return null;
        }

        List<GangJobPayoutMember> validMembers = NormalizeMembers(members);
        if (validMembers.Count == 0)
        {
            return null;
        }

        return BuildPlan(
            jobId,
            RoundCash(totalAmount),
            0m,
            validMembers);
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

        if (plan.GangReserveCash > 0m ||
            plan.GangReserveGold > 0m)
        {
            bankSystem.EnsureAccount(
                gangReserveOwnerId,
                gangReserveOwnerName);
        }

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share.CashAmount > 0m || share.GoldAmount > 0m)
            {
                bankSystem.EnsureAccount(share.OwnerId, share.OwnerName);
            }
        }

        if (plan.GangReserveCash > 0m ||
            plan.GangReserveGold > 0m)
        {
            bankSystem.DepositMoney(
                gangReserveOwnerId,
                plan.GangReserveCash,
                plan.GangReserveGold);
        }

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share.CashAmount > 0m || share.GoldAmount > 0m)
            {
                bankSystem.DepositMoney(
                    share.OwnerId,
                    share.CashAmount,
                    share.GoldAmount);
            }
        }

        _settledJobIds.Add(plan.JobId);
        return true;
    }

    public string SaveState()
    {
        GangJobPayoutState state = new GangJobPayoutState
        {
            SettledJobIds = _settledJobIds
                .OrderBy(id => id, StringComparer.Ordinal)
                .ToList()
        };

        return JsonConvert.SerializeObject(state, Formatting.Indented);
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
            state = JsonConvert.DeserializeObject<GangJobPayoutState>(json);
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
            {
                validIds.Add(jobId);
            }
        }

        _settledJobIds.Clear();
        foreach (string jobId in validIds)
        {
            _settledJobIds.Add(jobId);
        }
    }

    private static GangJobPayoutPlan BuildPlan(
        string jobId,
        decimal netCash,
        decimal netGold,
        List<GangJobPayoutMember> members)
    {
        netCash = RoundCash(netCash);
        netGold = RoundCash(netGold);

        decimal gangCash = GetGangHalf(netCash);
        decimal gangGold = GetGangHalf(netGold);
        decimal participantCash = netCash - gangCash;
        decimal participantGold = netGold - gangGold;

        Dictionary<string, decimal> cashShares =
            SplitEqually(participantCash, members);
        Dictionary<string, decimal> goldShares =
            SplitEqually(participantGold, members);

        List<GangJobPayoutShare> shares = members
            .OrderBy(member => member.OwnerId, StringComparer.Ordinal)
            .Select(member =>
            {
                decimal cash = cashShares[member.OwnerId];
                decimal gold = goldShares[member.OwnerId];

                return new GangJobPayoutShare
                {
                    OwnerId = member.OwnerId,
                    OwnerName = member.OwnerName,
                    ContributionWeight = 1m,
                    CashAmount = cash,
                    GoldAmount = gold,
                    Amount = cash + gold
                };
            })
            .ToList();

        return new GangJobPayoutPlan
        {
            JobId = jobId,
            TotalCash = netCash,
            TotalGold = netGold,
            TotalAmount = netCash + netGold,
            GangReserveCash = gangCash,
            GangReserveGold = gangGold,
            GangReserveAmount = gangCash + gangGold,
            MemberShares = shares
        };
    }

    private static List<GangJobPayoutMember> NormalizeMembers(
        IEnumerable<GangJobPayoutMember> members)
    {
        if (members == null)
        {
            return new List<GangJobPayoutMember>();
        }

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
                ContributionWeight = 1m
            });
        }

        return validMembers
            .OrderBy(member => member.OwnerId, StringComparer.Ordinal)
            .ToList();
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
        List<GangJobPayoutMember> members)
    {
        Dictionary<string, decimal> shares =
            new Dictionary<string, decimal>(StringComparer.Ordinal);

        if (members == null || members.Count == 0)
        {
            return shares;
        }

        decimal totalCents = RoundCash(amount) * 100m;
        decimal centsPerMember = decimal.Floor(totalCents / members.Count);
        int remainingCents =
            (int)(totalCents - centsPerMember * members.Count);

        List<GangJobPayoutMember> orderedMembers = members
            .OrderBy(member => member.OwnerId, StringComparer.Ordinal)
            .ToList();

        for (int i = 0; i < orderedMembers.Count; i++)
        {
            decimal cents = centsPerMember;
            if (i < remainingCents)
            {
                cents += 1m;
            }

            shares[orderedMembers[i].OwnerId] = cents / 100m;
        }

        return shares;
    }

    private static bool IsValidPlan(
        GangJobPayoutPlan plan,
        string gangReserveOwnerId)
    {
        if (plan.TotalCash < 0m ||
            plan.TotalGold < 0m ||
            plan.GangReserveCash < 0m ||
            plan.GangReserveGold < 0m ||
            plan.MemberShares == null)
        {
            return false;
        }

        if ((plan.GangReserveCash > 0m ||
             plan.GangReserveGold > 0m) &&
            string.IsNullOrWhiteSpace(gangReserveOwnerId))
        {
            return false;
        }

        if (RoundCash(plan.TotalAmount) !=
                RoundCash(plan.TotalCash + plan.TotalGold) ||
            RoundCash(plan.GangReserveAmount) !=
                RoundCash(plan.GangReserveCash + plan.GangReserveGold) ||
            plan.GangReserveCash != GetGangHalf(plan.TotalCash) ||
            plan.GangReserveGold != GetGangHalf(plan.TotalGold))
        {
            return false;
        }

        HashSet<string> seenIds =
            new HashSet<string>(StringComparer.Ordinal);
        decimal memberCashTotal = 0m;
        decimal memberGoldTotal = 0m;

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share == null ||
                string.IsNullOrWhiteSpace(share.OwnerId) ||
                share.CashAmount < 0m ||
                share.GoldAmount < 0m ||
                !seenIds.Add(share.OwnerId))
            {
                return false;
            }

            if (RoundCash(share.Amount) !=
                RoundCash(share.CashAmount + share.GoldAmount))
            {
                return false;
            }

            memberCashTotal += share.CashAmount;
            memberGoldTotal += share.GoldAmount;
        }

        if (seenIds.Count == 0)
        {
            return false;
        }

        List<GangJobPayoutMember> members = plan.MemberShares
            .Select(share => new GangJobPayoutMember
            {
                OwnerId = share.OwnerId,
                OwnerName = share.OwnerName,
                ContributionWeight = 1m
            })
            .ToList();

        Dictionary<string, decimal> expectedCash = SplitEqually(
            plan.TotalCash - plan.GangReserveCash,
            members);
        Dictionary<string, decimal> expectedGold = SplitEqually(
            plan.TotalGold - plan.GangReserveGold,
            members);

        foreach (GangJobPayoutShare share in plan.MemberShares)
        {
            if (share.CashAmount != expectedCash[share.OwnerId] ||
                share.GoldAmount != expectedGold[share.OwnerId])
            {
                return false;
            }
        }

        return RoundCash(plan.GangReserveCash + memberCashTotal) ==
                   RoundCash(plan.TotalCash) &&
               RoundCash(plan.GangReserveGold + memberGoldTotal) ==
                   RoundCash(plan.TotalGold);
    }

    private static decimal GetNetAmount(decimal reward, decimal cost)
    {
        decimal net = RoundCash(reward) - RoundCash(cost);
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
}