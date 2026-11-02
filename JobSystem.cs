// JobSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private readonly List<JobOffer> _offers = new List<JobOffer>();
    private readonly List<JobAssignment> _assignments = new List<JobAssignment>();

    public IReadOnlyList<JobOffer> Offers => _offers;
    public IReadOnlyList<JobAssignment> Assignments => _assignments;

    public void GenerateTownJobs(string townName)
    {
        if (string.IsNullOrEmpty(townName))
            return;

        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = "Escort Cargo",
            Description = "Protect a wagon carrying supplies into town.",
            LocationName = townName,
            PaymentCash = 15m,
            PaymentGold = 0m,
            HonorChange = 5,
            IsCriminal = false
        });

        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = "Rob Bank Coach",
            Description = "Ambush a coach hauling money and gold.",
            LocationName = townName,
            PaymentCash = 40m,
            PaymentGold = 2m,
            HonorChange = -10,
            IsCriminal = true
        });

        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = "Deliver Supplies",
            Description = "Move food and ammunition to an outpost.",
            LocationName = townName,
            PaymentCash = 20m,
            PaymentGold = 0m,
            HonorChange = 3,
            IsCriminal = false
        });
    }

    public JobOffer CreateCustomJob(string title, string description, string locationName, decimal cash, decimal gold, int honorChange, bool criminal)
    {
        return new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = title,
            Description = description,
            LocationName = locationName,
            PaymentCash = cash,
            PaymentGold = gold,
            HonorChange = honorChange,
            IsCriminal = criminal
        };
    }

    public void AddOffer(JobOffer offer)
    {
        if (offer == null) return;
        _offers.Add(offer);
    }

    public bool AcceptJob(string jobId, string gangMemberName = null)
    {
        var offer = _offers.FirstOrDefault(x => x.JobId == jobId);
        if (offer == null)
            return false;

        _offers.Remove(offer);

        _assignments.Add(new JobAssignment
        {
            AssignmentId = Guid.NewGuid().ToString("N"),
            JobId = offer.JobId,
            Title = offer.Title,
            GangMemberName = gangMemberName,
            Status = JobStatus.Active,
            PaymentCash = offer.PaymentCash,
            PaymentGold = offer.PaymentGold,
            HonorChange = offer.HonorChange,
            IsCriminal = offer.IsCriminal
        });

        return true;
    }

    public bool CompleteJob(string assignmentId)
    {
        var assignment = _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Completed;
        return true;
    }

    public bool FailJob(string assignmentId)
    {
        var assignment = _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Failed;
        return true;
    }

    public void PayOutCompletedJobs(BankSystem bank, string playerAccountId)
    {
        if (bank == null || string.IsNullOrEmpty(playerAccountId))
            return;

        foreach (var assignment in _assignments.Where(x => x.Status == JobStatus.Completed).ToList())
        {
            if (assignment.PaymentCash > 0)
                bank.DepositCash(playerAccountId, assignment.PaymentCash);

            if (assignment.PaymentGold > 0)
                bank.DepositGold(playerAccountId, assignment.PaymentGold);

            _assignments.Remove(assignment);
        }
    }

    public int GetHonorDeltaForCompletedJobs()
    {
        return _assignments
            .Where(x => x.Status == JobStatus.Completed)
            .Sum(x => x.HonorChange);
    }

    public void ClearCompletedJobs()
    {
        _assignments.RemoveAll(x => x.Status == JobStatus.Completed);
    }
}

public class JobOffer
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LocationName { get; set; }
    public decimal PaymentCash { get; set; }
    public decimal PaymentGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsCriminal { get; set; }
}

public class JobAssignment
{
    public string AssignmentId { get; set; }
    public string JobId { get; set; }
    public string Title { get; set; }
    public string GangMemberName { get; set; }
    public JobStatus Status { get; set; }
    public decimal PaymentCash { get; set; }
    public decimal PaymentGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsCriminal { get; set; }
}

public enum JobStatus
{
    Active = 0,
    Completed = 1,
    Failed = 2
}