using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    public List<JobOffer> Offers { get; private set; } = new List<JobOffer>();
    public List<JobAssignment> Assignments { get; private set; } = new List<JobAssignment>();

    public void GenerateTownJobs(string townName)
    {
        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Title = townName + " Escort Contract",
            Description = "Protect a shipment and deliver it safely.",
            Location = townName,
            RewardCash = 25,
            RewardHonor = 1,
            RiskLevel = 2,
            JobType = JobType.Escort,
            IsCompleted = false
        });

        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Title = townName + " Delivery Work",
            Description = "Carry supplies for a local business.",
            Location = townName,
            RewardCash = 15,
            RewardHonor = 1,
            RiskLevel = 1,
            JobType = JobType.Delivery,
            IsCompleted = false
        });

        Offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Title = townName + " Robbery Tip",
            Description = "A lead on valuable cargo moving through the area.",
            Location = townName,
            RewardCash = 50,
            RewardHonor = -2,
            RiskLevel = 4,
            JobType = JobType.Robbery,
            IsCompleted = false
        });
    }

    public JobOffer CreateJob(string title, string description, string location, int rewardCash, int rewardHonor, int riskLevel, JobType jobType)
    {
        JobOffer offer = new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Title = title,
            Description = description,
            Location = location,
            RewardCash = rewardCash,
            RewardHonor = rewardHonor,
            RiskLevel = riskLevel,
            JobType = jobType,
            IsCompleted = false
        };

        Offers.Add(offer);
        return offer;
    }

    public void AssignJob(string jobId, string memberId)
    {
        JobOffer offer = Offers.FirstOrDefault(j => j.JobId == jobId);
        if (offer == null || offer.IsCompleted)
            return;

        JobAssignment assignment = Assignments.FirstOrDefault(a => a.JobId == jobId);
        if (assignment == null)
        {
            assignment = new JobAssignment
            {
                JobId = jobId,
                AssignedMemberIds = new List<string>()
            };
            Assignments.Add(assignment);
        }

        if (!assignment.AssignedMemberIds.Contains(memberId))
            assignment.AssignedMemberIds.Add(memberId);
    }

    public bool GangVoteOnJob(GangManager gangManager, string jobId)
    {
        JobOffer offer = Offers.FirstOrDefault(j => j.JobId == jobId);
        if (offer == null)
            return false;

        int yesVotes = 0;
        int noVotes = 0;

        foreach (GangMember member in gangManager.GetActiveMembers())
        {
            bool voteYes = offer.RiskLevel <= 2;
            if (member.Honor < 0 && offer.JobType == JobType.Robbery)
                voteYes = true;

            if (member.Loyalty > 40)
                yesVotes++;
            else
                noVotes++;
        }

        return yesVotes >= noVotes;
    }

    public void CompleteJob(string jobId)
    {
        JobOffer offer = Offers.FirstOrDefault(j => j.JobId == jobId);
        if (offer == null)
            return;

        offer.IsCompleted = true;
    }

    public void PayOutCompletedJobs(BankSystem bankSystem, string playerId, GangManager gangManager)
    {
        List<JobOffer> completed = Offers.Where(o => o.IsCompleted).ToList();

        foreach (JobOffer offer in completed)
        {
            bankSystem.DepositToAccount(playerId, offer.RewardCash, 0);
            gangManager?.AdjustHonorForGang(offer.RewardHonor);
            gangManager?.DistributeReward(offer.RewardCash);

            Offers.Remove(offer);
            Assignments.RemoveAll(a => a.JobId == offer.JobId);
        }
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        Offers = offers ?? new List<JobOffer>();
        Assignments = assignments ?? new List<JobAssignment>();
    }
}

public class JobOffer
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int RewardCash { get; set; }
    public int RewardHonor { get; set; }
    public int RiskLevel { get; set; }
    public JobType JobType { get; set; }
    public bool IsCompleted { get; set; }
}

public class JobAssignment
{
    public string JobId { get; set; }
    public List<string> AssignedMemberIds { get; set; }
}

public enum JobType
{
    Escort,
    Delivery,
    Robbery,
    Protection,
    Investigation
}