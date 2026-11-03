// JobSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

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
        return _jobs.Where(x => x != null && !x.IsCompleted).ToList();
    }

    public JobOffer GetBestJobForGang(GangManager gangManager)
    {
        if (gangManager == null)
            return null;

        var members = gangManager.GetAllMembers().Where(x => x != null && x.IsActive).ToList();
        if (members.Count == 0)
            return GetAvailableJobs().FirstOrDefault();

        JobOffer best = null;
        int bestScore = int.MinValue;

        foreach (var job in GetAvailableJobs())
        {
            int score = ScoreJob(job, members);
            if (score > bestScore)
            {
                bestScore = score;
                best = job;
            }
        }

        return best;
    }

    public void VoteOnJob(GangManager gangManager)
    {
        if (gangManager == null)
            return;

        var job = GetBestJobForGang(gangManager);
        if (job == null)
            return;

        var approval = 0;

        foreach (var member in gangManager.GetAllMembers())
        {
            if (member == null || !member.IsActive)
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

        var split = job.RewardCash;
        var members = gangManager.GetAllMembers().Where(x => x != null && x.IsActive).ToList();

        if (members.Count > 0)
        {
            var share = split / members.Count;
            foreach (var member in members)
            {
                member.PersonalCash += share;
                member.AddMemory("Received payment from job: " + job.Title);
            }
        }
    }

    private int ScoreJob(JobOffer job, List<GangMember> members)
    {
        int score = 0;

        if (job.IsLegal)
            score += 10;
        else
            score -= 10;

        if (job.RequiresStealth)
            score += members.Any(x => x.SkillStealth > 60) ? 20 : -10;

        if (job.RequiresCombat)
            score += members.Any(x => x.SkillCombat > 60) ? 20 : -10;

        if (job.RequiresTrading)
            score += members.Any(x => x.SkillTrading > 60) ? 10 : -10;

        if (job.RequiresTransport)
            score += members.Any(x => x.SkillHandling > 60) ? 10 : -10;

        score += (int)Math.Min(20, job.RewardHonor / 5m);
        score += (int)Math.Min(20, job.RewardCash / 50m);

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