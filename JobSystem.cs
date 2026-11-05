using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private Dictionary<string, JobData> jobs = new Dictionary<string, JobData>();

    public event EventHandler<JobCompletedEventArgs> JobCompleted;
    public event EventHandler<JobFailedEventArgs> JobFailed;

    public void CreateJob(string jobId, string jobType, string title, string description, string sponsorId, string targetId, string locationId, decimal rewardCash, decimal rewardGold, int honorChange, bool isIllegal)
    {
        if (jobs.ContainsKey(jobId))
        {
            throw new InvalidOperationException($"Job {jobId} already exists.");
        }

        var job = new JobData
        {
            JobId = jobId,
            JobType = jobType,
            Title = title,
            Description = description,
            SponsorId = sponsorId,
            TargetId = targetId,
            LocationId = locationId,
            RewardCash = rewardCash,
            RewardGold = rewardGold,
            HonorChange = honorChange,
            IsIllegal = isIllegal,
            IsCompleted = false,
            IsFailed = false,
            IsAvailable = true,
            Objectives = new List<string>(),
            Participants = new List<string>()
        };

        jobs[jobId] = job;
    }

    public void AddObjective(string jobId, string objective)
    {
        if (!jobs.ContainsKey(jobId))
        {
            throw new KeyNotFoundException($"Job {jobId} not found.");
        }

        jobs[jobId].Objectives.Add(objective);
    }

    public void AssignParticipant(string jobId, string participantId)
    {
        if (!jobs.ContainsKey(jobId))
        {
            throw new KeyNotFoundException($"Job {jobId} not found.");
        }

        if (!jobs[jobId].Participants.Contains(participantId))
        {
            jobs[jobId].Participants.Add(participantId);
        }
    }

    public void RemoveParticipant(string jobId, string participantId)
    {
        if (!jobs.ContainsKey(jobId))
        {
            throw new KeyNotFoundException($"Job {jobId} not found.");
        }

        jobs[jobId].Participants.Remove(participantId);
    }

    public void CompleteJob(string jobId)
    {
        if (!jobs.ContainsKey(jobId))
        {
            throw new KeyNotFoundException($"Job {jobId} not found.");
        }

        var job = jobs[jobId];
        job.IsCompleted = true;
        job.IsFailed = false;
        job.IsAvailable = false;

        JobCompleted?.Invoke(this, new JobCompletedEventArgs
        {
            JobId = jobId,
            JobData = job,
            CompletedAtUtc = DateTime.UtcNow
        });
    }

    public void FailJob(string jobId, string reason = "")
    {
        if (!jobs.ContainsKey(jobId))
        {
            throw new KeyNotFoundException($"Job {jobId} not found.");
        }

        var job = jobs[jobId];
        job.IsCompleted = false;
        job.IsFailed = true;
        job.IsAvailable = false;

        JobFailed?.Invoke(this, new JobFailedEventArgs
        {
            JobId = jobId,
            JobData = job,
            Reason = reason,
            FailedAtUtc = DateTime.UtcNow
        });
    }

    public JobData GetJob(string jobId)
    {
        return jobs.ContainsKey(jobId) ? jobs[jobId] : null;
    }

    public List<JobData> GetAvailableJobs()
    {
        return jobs.Values.Where(j => j.IsAvailable).ToList();
    }

    public List<JobData> GetActiveJobs()
    {
        return jobs.Values.Where(j => !j.IsCompleted && !j.IsFailed).ToList();
    }

    public List<JobData> GetCompletedJobs()
    {
        return jobs.Values.Where(j => j.IsCompleted).ToList();
    }

    public List<JobData> GetFailedJobs()
    {
        return jobs.Values.Where(j => j.IsFailed).ToList();
    }

    public List<JobData> GetJobsByParticipant(string participantId)
    {
        return jobs.Values.Where(j => j.Participants.Contains(participantId)).ToList();
    }

    public List<JobData> GetJobsByJobType(string jobType)
    {
        return jobs.Values.Where(j => j.JobType == jobType).ToList();
    }

    public List<JobData> GetJobsBySponsor(string sponsorId)
    {
        return jobs.Values.Where(j => j.SponsorId == sponsorId).ToList();
    }

    public List<JobData> GetJobsByLocation(string locationId)
    {
        return jobs.Values.Where(j => j.LocationId == locationId).ToList();
    }

    public int GetParticipantCount(string jobId)
    {
        return jobs.ContainsKey(jobId) ? jobs[jobId].Participants.Count : 0;
    }

    public decimal GetTotalRewardAmount(string jobId)
    {
        if (!jobs.ContainsKey(jobId))
        {
            return 0;
        }

        var job = jobs[jobId];
        return job.RewardCash + job.RewardGold;
    }

    public void UpdateJobAvailability(string jobId, bool isAvailable)
    {
        if (jobs.ContainsKey(jobId))
        {
            jobs[jobId].IsAvailable = isAvailable;
        }
    }

    public void LoadJobs(List<JobData> jobDataList)
    {
        jobs.Clear();
        foreach (var jobData in jobDataList)
        {
            jobs[jobData.JobId] = jobData;
        }
    }

    public List<JobData> GetAllJobs()
    {
        return new List<JobData>(jobs.Values);
    }
}

public class JobCompletedEventArgs : EventArgs
{
    public string JobId { get; set; }
    public JobData JobData { get; set; }
    public DateTime CompletedAtUtc { get; set; }
}

public class JobFailedEventArgs : EventArgs
{
    public string JobId { get; set; }
    public JobData JobData { get; set; }
    public string Reason { get; set; }
    public DateTime FailedAtUtc { get; set; }
}