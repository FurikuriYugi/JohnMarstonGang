using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private readonly Dictionary<string, JobData> jobs =
        new Dictionary<string, JobData>(StringComparer.Ordinal);

    public event EventHandler<JobCompletedEventArgs> JobCompleted;
    public event EventHandler<JobFailedEventArgs> JobFailed;

    public JobData CreateJob(
        string jobId,
        string jobType,
        string title,
        string description,
        string sponsorId,
        string targetId,
        string locationId,
        decimal rewardCash,
        decimal rewardGold,
        int honorChange,
        bool isIllegal)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentException(
                "A job must have a non-empty job ID.",
                nameof(jobId));
        }

        if (string.IsNullOrWhiteSpace(jobType))
        {
            throw new ArgumentException(
                "A job must have a non-empty job type.",
                nameof(jobType));
        }

        if (rewardCash < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rewardCash),
                "A job reward cannot be negative.");
        }

        if (rewardGold < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rewardGold),
                "A job reward cannot be negative.");
        }

        if (jobs.ContainsKey(jobId))
        {
            throw new InvalidOperationException(
                $"Job {jobId} already exists.");
        }

        JobData job = new JobData
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
            CostCash = 0m,
            CostGold = 0m,
            HonorChange = honorChange,
            IsIllegal = isIllegal,
            IsCompleted = false,
            IsFailed = false,
            IsAvailable = true,
            Objectives = new List<string>(),
            Participants = new List<string>()
        };

        jobs.Add(jobId, job);
        return job;
    }

    // Convenience overload for world-generated jobs.
    public JobData CreateJob(
        string jobType,
        string title,
        string description,
        string sponsorId,
        string targetId,
        string locationId,
        decimal rewardCash,
        decimal rewardGold,
        int honorChange,
        bool isIllegal)
    {
        return CreateJob(
            Guid.NewGuid().ToString("N"),
            jobType,
            title,
            description,
            sponsorId,
            targetId,
            locationId,
            rewardCash,
            rewardGold,
            honorChange,
            isIllegal);
    }

    public void AddObjective(string jobId, string objective)
    {
        JobData job = GetRequiredJob(jobId);

        if (string.IsNullOrWhiteSpace(objective))
        {
            return;
        }

        if (job.Objectives == null)
        {
            job.Objectives = new List<string>();
        }

        job.Objectives.Add(objective);
    }

    public void AssignParticipant(string jobId, string participantId)
    {
        JobData job = GetRequiredJob(jobId);

        if (job.IsCompleted || job.IsFailed)
        {
            throw new InvalidOperationException(
                "Participants cannot be changed after a job has ended.");
        }

        if (string.IsNullOrWhiteSpace(participantId))
        {
            return;
        }

        if (job.Participants == null)
        {
            job.Participants = new List<string>();
        }

        if (!job.Participants.Contains(participantId))
        {
            job.Participants.Add(participantId);
        }
    }

    public void RemoveParticipant(string jobId, string participantId)
    {
        JobData job = GetRequiredJob(jobId);

        if (job.IsCompleted || job.IsFailed)
        {
            throw new InvalidOperationException(
                "Participants cannot be changed after a job has ended.");
        }

        if (job.Participants == null ||
            string.IsNullOrWhiteSpace(participantId))
        {
            return;
        }

        job.Participants.Remove(participantId);
    }

    public void SetJobCosts(
        string jobId,
        decimal cashCost,
        decimal goldCost)
    {
        JobData job = GetRequiredJob(jobId);

        if (job.IsCompleted || job.IsFailed)
        {
            throw new InvalidOperationException(
                "Job costs cannot be changed after a job has ended.");
        }

        if (cashCost < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cashCost),
                "Job costs cannot be negative.");
        }

        if (goldCost < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(goldCost),
                "Job costs cannot be negative.");
        }

        job.CostCash = RoundMoney(cashCost);
        job.CostGold = RoundMoney(goldCost);
    }

    public void CompleteJob(string jobId)
    {
        JobData job = GetRequiredJob(jobId);

        if (job.IsCompleted || job.IsFailed)
        {
            return;
        }

        List<string> participants = NormalizeParticipantIds(job.Participants);
        if (participants.Count == 0)
        {
            throw new InvalidOperationException(
                "A job cannot be completed without at least one assigned participant.");
        }

        job.Participants = participants;
        job.IsCompleted = true;
        job.IsFailed = false;
        job.IsAvailable = false;

        EventHandler<JobCompletedEventArgs> handler = JobCompleted;
        if (handler != null)
        {
            handler(this, new JobCompletedEventArgs
            {
                JobId = job.JobId,
                JobData = job,
                CompletedAtUtc = DateTime.UtcNow
            });
        }
    }

    public void FailJob(string jobId, string reason = "")
    {
        JobData job = GetRequiredJob(jobId);

        if (job.IsCompleted || job.IsFailed)
        {
            return;
        }

        job.IsCompleted = false;
        job.IsFailed = true;
        job.IsAvailable = false;

        EventHandler<JobFailedEventArgs> handler = JobFailed;
        if (handler != null)
        {
            handler(this, new JobFailedEventArgs
            {
                JobId = job.JobId,
                JobData = job,
                Reason = reason ?? string.Empty,
                FailedAtUtc = DateTime.UtcNow
            });
        }
    }

    public JobData GetJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            return null;
        }

        JobData job;
        return jobs.TryGetValue(jobId, out job) ? job : null;
    }

    public List<JobData> GetAvailableJobs()
    {
        return jobs.Values
            .Where(job =>
                job != null &&
                job.IsAvailable &&
                !job.IsCompleted &&
                !job.IsFailed)
            .ToList();
    }

    public List<JobData> GetActiveJobs()
    {
        return jobs.Values
            .Where(job =>
                job != null &&
                !job.IsCompleted &&
                !job.IsFailed)
            .ToList();
    }

    public List<JobData> GetCompletedJobs()
    {
        return jobs.Values
            .Where(job => job != null && job.IsCompleted)
            .ToList();
    }

    public List<JobData> GetFailedJobs()
    {
        return jobs.Values
            .Where(job => job != null && job.IsFailed)
            .ToList();
    }

    public List<JobData> GetJobsByParticipant(string participantId)
    {
        if (string.IsNullOrWhiteSpace(participantId))
        {
            return new List<JobData>();
        }

        return jobs.Values
            .Where(job =>
                job != null &&
                job.Participants != null &&
                job.Participants.Contains(participantId))
            .ToList();
    }

    public List<JobData> GetJobsByJobType(string jobType)
    {
        if (string.IsNullOrWhiteSpace(jobType))
        {
            return new List<JobData>();
        }

        return jobs.Values
            .Where(job =>
                job != null &&
                string.Equals(
                    job.JobType,
                    jobType,
                    StringComparison.Ordinal))
            .ToList();
    }

    public List<JobData> GetJobsBySponsor(string sponsorId)
    {
        if (string.IsNullOrWhiteSpace(sponsorId))
        {
            return new List<JobData>();
        }

        return jobs.Values
            .Where(job =>
                job != null &&
                string.Equals(
                    job.SponsorId,
                    sponsorId,
                    StringComparison.Ordinal))
            .ToList();
    }

    public List<JobData> GetJobsByLocation(string locationId)
    {
        if (string.IsNullOrWhiteSpace(locationId))
        {
            return new List<JobData>();
        }

        return jobs.Values
            .Where(job =>
                job != null &&
                string.Equals(
                    job.LocationId,
                    locationId,
                    StringComparison.Ordinal))
            .ToList();
    }

    public int GetParticipantCount(string jobId)
    {
        JobData job = GetJob(jobId);
        return job == null || job.Participants == null
            ? 0
            : job.Participants.Count;
    }

    public decimal GetTotalRewardAmount(string jobId)
    {
        JobData job = GetJob(jobId);
        return job == null
            ? 0m
            : job.RewardCash + job.RewardGold;
    }

    public decimal GetTotalCostAmount(string jobId)
    {
        JobData job = GetJob(jobId);
        return job == null
            ? 0m
            : job.CostCash + job.CostGold;
    }

    public decimal GetNetRewardCash(string jobId)
    {
        JobData job = GetJob(jobId);
        return job == null
            ? 0m
            : GetNetAmount(job.RewardCash, job.CostCash);
    }

    public decimal GetNetRewardGold(string jobId)
    {
        JobData job = GetJob(jobId);
        return job == null
            ? 0m
            : GetNetAmount(job.RewardGold, job.CostGold);
    }

    public void UpdateJobAvailability(string jobId, bool isAvailable)
    {
        JobData job = GetJob(jobId);

        if (job == null || job.IsCompleted || job.IsFailed)
        {
            return;
        }

        job.IsAvailable = isAvailable;
    }

    public void LoadJobs(List<JobData> jobDataList)
    {
        jobs.Clear();

        if (jobDataList == null)
        {
            return;
        }

        foreach (JobData job in jobDataList)
        {
            if (job == null || string.IsNullOrWhiteSpace(job.JobId))
            {
                continue;
            }

            NormalizeJob(job);
            jobs[job.JobId] = job;
        }
    }

    public List<JobData> GetAllJobs()
    {
        return jobs.Values
            .Where(job => job != null)
            .ToList();
    }

    public void LoadFromSaveRoot(SaveRoot saveRoot)
    {
        if (saveRoot == null || saveRoot.World == null)
        {
            LoadJobs(null);
            return;
        }

        LoadJobs(saveRoot.World.ActiveJobs);
    }

    public void WriteToSaveRoot(SaveRoot saveRoot)
    {
        if (saveRoot == null)
        {
            throw new ArgumentNullException(nameof(saveRoot));
        }

        if (saveRoot.World == null)
        {
            saveRoot.World = new WorldState();
        }

        saveRoot.World.ActiveJobs = GetAllJobs();
    }

    private JobData GetRequiredJob(string jobId)
    {
        JobData job = GetJob(jobId);

        if (job == null)
        {
            throw new KeyNotFoundException(
                $"Job {jobId} not found.");
        }

        return job;
    }

    private static void NormalizeJob(JobData job)
    {
        if (job.Objectives == null)
        {
            job.Objectives = new List<string>();
        }

        if (job.Participants == null)
        {
            job.Participants = new List<string>();
        }

        if (job.CostCash < 0m)
        {
            job.CostCash = 0m;
        }

        if (job.CostGold < 0m)
        {
            job.CostGold = 0m;
        }

        job.CostCash = RoundMoney(job.CostCash);
        job.CostGold = RoundMoney(job.CostGold);
    }

    private static List<string> NormalizeParticipantIds(
        IEnumerable<string> participantIds)
    {
        if (participantIds == null)
        {
            return new List<string>();
        }

        return participantIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();
    }

    private static decimal GetNetAmount(decimal reward, decimal cost)
    {
        decimal net = RoundMoney(reward) - RoundMoney(cost);
        return net > 0m ? net : 0m;
    }

    private static decimal RoundMoney(decimal amount)
    {
        return decimal.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero);
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