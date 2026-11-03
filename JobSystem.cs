// JobSystem.cs
using System;
using System.Collections.Generic;

public class JobSystem
{
    private readonly List<JobOffer> _jobs = new List<JobOffer>();

    public void AddJob(JobOffer job)
    {
        if (job == null)
            return;

        _jobs.Add(job);
    }

    public List<JobOffer> GetAvailableJobs()
    {
        List<JobOffer> availableJobs = new List<JobOffer>();

        for (int i = 0; i < _jobs.Count; i++)
        {
            JobOffer job = _jobs[i];
            if (job != null && !job.IsCompleted)
                availableJobs.Add(job);
        }

        return availableJobs;
    }

    public JobOffer GetBestJobForGang(GangManager gangManager)
    {
        if (gangManager == null)
            return null;

        List<GangMember> members = gangManager.GetAllMembers();
        if (members == null || members.Count == 0)
            return null;

        List<JobOffer> availableJobs = GetAvailableJobs();
        if (availableJobs.Count == 0)
            return null;

        JobOffer bestJob = null;
        int bestScore = int.MinValue;

        for (int i = 0; i < availableJobs.Count; i++)
        {
            JobOffer job = availableJobs[i];
            int score = ScoreJob(job, members);

            if (score > bestScore)
            {
                bestScore = score;
                bestJob = job;
            }
        }

        return bestJob;
    }

    public void VoteOnJob(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        JobOffer job = GetBestJobForGang(gangManager);
        if (job == null)
            return;

        List<GangMember> members = gangManager.GetAllMembers();
        if (members == null || members.Count == 0)
            return;

        int approval = 0;

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            approval += EvaluateMemberSupport(member, job);
        }

        job.IsApproved = approval >= 0;
    }

    public void CompleteJob(JobOffer job, GangManager gangManager)
    {
        if (job == null)
            return;

        job.IsCompleted = true;

        if (gangManager == null)
            return;

        List<GangMember> members = gangManager.GetAllMembers();
        if (members == null || members.Count == 0)
            return;

        int activeCount = 0;
        for (int i = 0; i < members.Count; i++)
        {
            if (members[i] != null)
                activeCount++;
        }

        if (activeCount == 0)
            return;

        decimal cashShare = job.RewardCash / activeCount;
        decimal goldShare = job.RewardGold / activeCount;

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            member.PersonalCash += cashShare;
            member.PersonalGold += goldShare;
            member.AddMemory("Received payment from job: " + job.Title);
        }
    }

    private int ScoreJob(JobOffer job, List<GangMember> members)
    {
        if (job == null || members == null)
            return int.MinValue;

        int score = 0;

        if (job.IsLegal)
            score += 10;
        else
            score -= 10;

        bool hasStealth = false;
        bool hasCombat = false;
        bool hasTrading = false;
        bool hasTransport = false;

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            if (member.SkillStealth > 60) hasStealth = true;
            if (member.SkillCombat > 60) hasCombat = true;
            if (member.SkillTrading > 60) hasTrading = true;
            if (member.SkillHandling > 60) hasTransport = true;
        }

        if (job.RequiresStealth)
            score += hasStealth ? 20 : -10;

        if (job.RequiresCombat)
            score += hasCombat ? 20 : -10;

        if (job.RequiresTrading)
            score += hasTrading ? 10 : -10;

        if (job.RequiresTransport)
            score += hasTransport ? 10 : -10;

        if (job.RewardHonor > 0)
            score += Math.Min(20, job.RewardHonor / 5);

        if (job.RewardCash > 0)
            score += (int)Math.Min(20m, job.RewardCash / 50m);

        return score;
    }

    private int EvaluateMemberSupport(GangMember member, JobOffer job)
    {
        int score = 0;

        if (job.IsLegal)
            score += member.Loyalty >= 40 ? 1 : -1;
        else
            score += member.Loyalty <= 60 ? 1 : 0;

        if (job.RequiresStealth && member.SkillStealth > 50)
            score += 1;

        if (job.RequiresCombat && member.SkillCombat > 50)
            score += 1;

        if (job.RequiresTrading && member.SkillTrading > 50)
            score += 1;

        if (member.Mood == GangMood.Loyal)
            score += 1;

        if (member.Mood == GangMood.Nervous)
            score -= 1;

        return score;
    }
}

public class JobOffer
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsLegal { get; set; }
    public bool RequiresStealth { get; set; }
    public bool RequiresCombat { get; set; }
    public bool RequiresTrading { get; set; }
    public bool RequiresTransport { get; set; }
    public bool IsApproved { get; set; }
    public bool IsCompleted { get; set; }
    public decimal RewardCash { get; set; }
    public decimal RewardGold { get; set; }
    public int RewardHonor { get; set; }

    public JobOffer()
    {
        JobId = Guid.NewGuid().ToString("N");
    }
}