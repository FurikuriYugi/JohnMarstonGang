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
            JobId = Guid.NewGuid().ToString(),
            Title = "Protect a shipment",
            Description = "Escort valuable cargo safely through hostile territory.",
            LocationName = townName,
            PaymentCash = 35m,
            PaymentGold = 0m,
            HonorChange = 5,
            IsCriminal = false
        });

        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Title = "Rob the shipment",
            Description = "Ambush a valuable coach or wagon carrying cash and gold.",
            LocationName = townName,
            PaymentCash = 120m,
            PaymentGold = 1m,
            HonorChange = -10,
            IsCriminal = true
        });
    }

    public JobAssignment AssignJob(string jobId, string gangMemberName)
    {
        JobOffer offer = Offers.FirstOrDefault(o => o != null && o.JobId == jobId && o.IsAvailable);
        if (offer == null)
            return null;

        offer.IsAvailable = false;

        JobAssignment assignment = new JobAssignment
        {
            AssignmentId = Guid.NewGuid().ToString(),
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

    public void CompleteJob(string assignmentId)
    {
        JobAssignment assignment = Assignments.FirstOrDefault(a => a != null && a.AssignmentId == assignmentId);
        if (assignment == null)
            return;

        assignment.Status = JobStatus.Completed;
    }

    public void FailJob(string assignmentId)
    {
        JobAssignment assignment = Assignments.FirstOrDefault(a => a != null && a.AssignmentId == assignmentId);
        if (assignment == null)
            return;

        assignment.Status = JobStatus.Failed;
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        Offers = offers ?? new List<JobOffer>();
        Assignments = assignments ?? new List<JobAssignment>();
    }
}