using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    public List<JobOffer> Offers { get; private set; } = new List<JobOffer>();
    public List<JobAssignment> Assignments { get; private set; } = new List<JobAssignment>();

    public void GenerateTownJobs(string townName)
    {
        if (string.IsNullOrWhiteSpace(townName))
            return;

        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = "Escort Shipment",
            Description = "Protect a shipment through dangerous territory.",
            LocationName = townName,
            PaymentCash = 35m,
            PaymentGold = 0m,
            HonorChange = 5,
            IsCriminal = false,
            IsAvailable = true
        });

        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = "Rob Shipment",
            Description = "Ambush and rob a valuable coach or wagon.",
            LocationName = townName,
            PaymentCash = 120m,
            PaymentGold = 1m,
            HonorChange = -10,
            IsCriminal = true,
            IsAvailable = true
        });
    }

    public JobOffer GetOffer(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return null;

        return Offers.FirstOrDefault(x => x != null && string.Equals(x.JobId, jobId, StringComparison.OrdinalIgnoreCase));
    }

    public List<JobOffer> GetAvailableJobs()
    {
        return Offers.Where(x => x != null && x.IsAvailable).ToList();
    }

    public JobAssignment AcceptJob(string jobId, string gangMemberName = null)
    {
        JobOffer offer = GetOffer(jobId);
        if (offer == null || !offer.IsAvailable)
            return null;

        offer.IsAvailable = false;

        JobAssignment assignment = new JobAssignment
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
        };

        Assignments.Add(assignment);
        return assignment;
    }

    public bool CompleteJob(string assignmentId)
    {
        JobAssignment assignment = GetAssignment(assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Completed;
        return true;
    }

    public bool FailJob(string assignmentId)
    {
        JobAssignment assignment = GetAssignment(assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Failed;
        return true;
    }

    public bool AcceptJobWithGangVote(string jobId, GangManager gangManager, string gangMemberName = null)
    {
        if (gangManager == null)
            return false;

        List<GangMember> members = gangManager.GetActiveMembers();
        if (members.Count == 0)
            return false;

        int yesVotes = 0;
        foreach (GangMember member in members)
        {
            if (member.Loyalty >= 50)
                yesVotes++;
        }

        if (yesVotes <= members.Count / 2)
            return false;

        return AcceptJob(jobId, gangMemberName) != null;
    }

    public void PayOutCompletedJobs(BankSystem bankSystem, string playerAccountId, GangManager gangManager = null)
    {
        if (bankSystem == null || string.IsNullOrWhiteSpace(playerAccountId))
            return;

        foreach (JobAssignment assignment in Assignments.Where(x => x != null && x.Status == JobStatus.Completed).ToList())
        {
            bankSystem.DepositCash(playerAccountId, assignment.PaymentCash);
            bankSystem.DepositGold(playerAccountId, assignment.PaymentGold);

            if (gangManager != null)
                gangManager.RewardGang(assignment.PaymentCash, assignment.PaymentGold);

            assignment.Status = JobStatus.Completed;
        }
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        Offers = offers ?? new List<JobOffer>();
        Assignments = assignments ?? new List<JobAssignment>();
    }

    private JobAssignment GetAssignment(string assignmentId)
    {
        if (string.IsNullOrWhiteSpace(assignmentId))
            return null;

        return Assignments.FirstOrDefault(x => x != null && string.Equals(x.AssignmentId, assignmentId, StringComparison.OrdinalIgnoreCase));
    }
}