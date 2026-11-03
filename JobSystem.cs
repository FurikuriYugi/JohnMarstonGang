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
        if (string.IsNullOrWhiteSpace(townName))
            return;

        AddOffer(CreateCustomJob("Escort Cargo", "Protect a wagon carrying supplies into town.", townName, 15m, 0m, 5, false));
        AddOffer(CreateCustomJob("Rob Bank Coach", "Ambush a coach hauling money and gold.", townName, 40m, 2m, -10, true));
        AddOffer(CreateCustomJob("Deliver Supplies", "Move food and ammunition to an outpost.", townName, 20m, 0m, 3, false));
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
        if (bank == null || string.IsNullOrWhiteSpace(playerAccountId))
            return;

        foreach (var assignment in _assignments.Where(x => x.Status == JobStatus.Completed).ToList())
        {
            if (assignment.PaymentCash > 0m)
                bank.DepositCash(playerAccountId, assignment.PaymentCash);

            if (assignment.PaymentGold > 0m)
                bank.DepositGold(playerAccountId, assignment.PaymentGold);

            _assignments.Remove(assignment);
        }
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        _offers.Clear();
        _assignments.Clear();

        if (offers != null)
            _offers.AddRange(offers);

        if (assignments != null)
            _assignments.AddRange(assignments);
    }

    public JobOffer GetOffer(string jobId)
    {
        return _offers.FirstOrDefault(x => x.JobId == jobId);
    }

    public JobAssignment GetAssignment(string assignmentId)
    {
        return _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
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