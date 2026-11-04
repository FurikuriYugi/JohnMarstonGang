using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class JobSystem
{
    private readonly List<JobData> _jobs = new List<JobData>();

    public List<JobData> GetAllJobs()
    {
        return new List<JobData>(_jobs);
    }

    public JobData GetJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return null;

        return _jobs.Find(x => x != null && x.JobId == jobId);
    }

    public void RegisterJob(JobData job)
    {
        if (job == null || string.IsNullOrWhiteSpace(job.JobId))
            return;

        _jobs.RemoveAll(x => x != null && x.JobId == job.JobId);
        _jobs.Add(job);
    }

    public void RemoveJob(string jobId)
    {
        _jobs.RemoveAll(x => x != null && x.JobId == jobId);
    }

    public JobData CreateJob(string jobType, string title, string description, string sponsorId, string targetId, decimal rewardCash, decimal rewardGold, int honorChange, bool isIllegal)
    {
        JobData job = new JobData
        {
            JobId = Guid.NewGuid().ToString("N"),
            JobType = jobType,
            Title = title,
            Description = description,
            SponsorId = sponsorId,
            TargetId = targetId,
            RewardCash = rewardCash,
            RewardGold = rewardGold,
            HonorChange = honorChange,
            IsIllegal = isIllegal,
            IsAvailable = true,
            IsCompleted = false,
            IsFailed = false
        };

        RegisterJob(job);
        return job;
    }

    public void AssignParticipant(string jobId, string participantId)
    {
        JobData job = GetJob(jobId);
        if (job == null || string.IsNullOrWhiteSpace(participantId))
            return;

        if (job.Participants == null)
            job.Participants = new List<string>();

        if (!job.Participants.Contains(participantId))
            job.Participants.Add(participantId);
    }

    public void AddObjective(string jobId, string objective)
    {
        JobData job = GetJob(jobId);
        if (job == null || string.IsNullOrWhiteSpace(objective))
            return;

        if (job.Objectives == null)
            job.Objectives = new List<string>();

        job.Objectives.Add(objective);
    }

    public void CompleteJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsCompleted = true;
        job.IsAvailable = false;
        job.IsFailed = false;
    }

    public void FailJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsFailed = true;
        job.IsAvailable = false;
        job.IsCompleted = false;
    }

    public List<JobData> GetAvailableJobs()
    {
        return _jobs.Where(x => x != null && x.IsAvailable && !x.IsCompleted && !x.IsFailed).ToList();
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_jobs, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _jobs.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<JobData> jobs = JsonConvert.DeserializeObject<List<JobData>>(json);
        if (jobs != null)
            _jobs.AddRange(jobs);
    }
}