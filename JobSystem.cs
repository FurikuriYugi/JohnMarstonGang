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

        Offers.Add(CreateJob(
            townName + " Escort Contract",
            "Protect a shipment and deliver it safely.",
            townName,
            25,
            1,
            2,
            JobType.Escort));

        Offers.Add(CreateJob(
            townName + " Delivery Work",
            "Carry supplies for a local business.",
            townName,
            15,
            1,
            1,
            JobType.Delivery));

        Offers.Add(CreateJob(
            townName + " Protection Detail",
            "Keep trouble away from a local business.",
            townName,
            20,
            1,
            2,
            JobType.Protection));

        Offers.Add(CreateJob(
            townName + " Robbery Tip",
            "A lead on valuable cargo moving through the area.",
            townName,
            50,
            -2,
            4,
            JobType.Robbery));
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

    public bool AssignJob(string jobId, string memberId)
    {
        if (string.IsNullOrWhiteSpace(jobId) || string.IsNullOrWhiteSpace(memberId))
            return false;

        JobOffer offer = Offers.FirstOrDefault(j => j.JobId == jobId);
        if (offer == null || offer.IsCompleted)
            return false;

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

        return true;
    }

    public bool GangVoteOnJob(GangManager gangManager, string jobId)
    {
        if (gangManager == null || string.IsNullOrWhiteSpace(jobId))
            return false;

        JobOffer offer = Offers.FirstOrDefault(j => j.JobId == jobId);
        if (offer == null)
            return false;

        int yesVotes = 0;
        int noVotes = 0;

        foreach (GangMember member in gangManager.GetActiveMembers())
        {
            bool voteYes = offer.RiskLevel <= 2;

            if (offer.JobType == JobType.Robbery)
                voteYes = member.Honor <= 0 || member.Loyalty >= 40;

            if (offer.JobType == JobType.Protection || offer.JobType == JobType.Escort)
                voteYes = member.Loyalty >= 30 || member.Honor >= 0;

            if (member.Loyalty >= 50)
                yesVotes++;
            else
                noVotes++;

            if (voteYes)
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
        if (bankSystem == null)
            return;

        List<JobOffer> completed = Offers.Where(o => o.IsCompleted).ToList();

        foreach (JobOffer offer in completed)
        {
            bankSystem.DepositToAccount(playerId, offer.RewardCash, 0);

            if (gangManager != null)
            {
                gangManager.AdjustHonorForGang(offer.RewardHonor);
                gangManager.DistributeReward(offer.RewardCash);
            }

            Offers.Remove(offer);
            Assignments.RemoveAll(a => a.JobId == offer.JobId);
        }
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        Offers = offers ?? new List<JobOffer>();
        Assignments = assignments ?? new List<JobAssignment>();

        foreach (JobAssignment assignment in Assignments)
        {
            if (assignment.AssignedMemberIds == null)
                assignment.AssignedMemberIds = new List<string>();
        }
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
    public List<string> AssignedMemberIds { get; set; } = new List<string>();
}

public enum JobType
{
    Escort,
    Delivery,
    Robbery,
    Protection,
    Investigation
}