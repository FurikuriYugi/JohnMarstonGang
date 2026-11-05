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

        return _jobs.Find(
            x => x != null && x.JobId == jobId);
    }

    public void RegisterJob(JobData job)
    {
        if (job == null ||
            string.IsNullOrWhiteSpace(job.JobId))
        {
            return;
        }

        NormalizeJob(job);

        _jobs.RemoveAll(
            x => x != null && x.JobId == job.JobId);

        _jobs.Add(job);
    }

    public void RemoveJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return;

        _jobs.RemoveAll(
            x => x != null && x.JobId == jobId);
    }

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
        JobData job = new JobData
        {
            JobId = Guid.NewGuid().ToString("N"),
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
            IsAvailable = true,
            IsCompleted = false,
            IsFailed = false,
            Objectives = new List<string>(),
            Participants = new List<string>()
        };

        RegisterJob(job);
        return job;
    }

    public void AssignParticipant(
        string jobId,
        string participantId)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            string.IsNullOrWhiteSpace(participantId) ||
            job.IsCompleted ||
            job.IsFailed)
        {
            return;
        }

        if (job.Participants == null)
            job.Participants = new List<string>();

        if (!job.Participants.Contains(participantId))
            job.Participants.Add(participantId);
    }

    public void RemoveParticipant(
        string jobId,
        string participantId)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            string.IsNullOrWhiteSpace(participantId) ||
            job.Participants == null)
        {
            return;
        }

        job.Participants.RemoveAll(
            x => x == participantId);
    }

    public void AddObjective(
        string jobId,
        string objective)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            string.IsNullOrWhiteSpace(objective) ||
            job.IsCompleted ||
            job.IsFailed)
        {
            return;
        }

        if (job.Objectives == null)
            job.Objectives = new List<string>();

        job.Objectives.Add(objective);
    }

    public void CompleteJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            job.IsCompleted ||
            job.IsFailed)
        {
            return;
        }

        job.IsCompleted = true;
        job.IsAvailable = false;
        job.IsFailed = false;
    }

    public void FailJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            job.IsCompleted ||
            job.IsFailed)
        {
            return;
        }

        job.IsFailed = true;
        job.IsAvailable = false;
        job.IsCompleted = false;
    }

    public void SetAvailability(
        string jobId,
        bool isAvailable)
    {
        JobData job = GetJob(jobId);
        if (job == null ||
            job.IsCompleted ||
            job.IsFailed)
        {
            return;
        }

        job.IsAvailable = isAvailable;
    }

    public List<JobData> GetAvailableJobs()
    {
        return _jobs
            .Where(x =>
                x != null &&
                x.IsAvailable &&
                !x.IsCompleted &&
                !x.IsFailed)
            .ToList();
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            _jobs,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            _jobs.Clear();
            return;
        }

        List<JobData> loadedJobs;

        try
        {
            loadedJobs =
                JsonConvert.DeserializeObject<List<JobData>>(json);
        }
        catch (JsonException)
        {
            return;
        }

        List<JobData> validJobs = new List<JobData>();
        HashSet<string> seenJobIds =
            new HashSet<string>(StringComparer.Ordinal);

        if (loadedJobs != null)
        {
            foreach (JobData job in loadedJobs)
            {
                if (job == null ||
                    string.IsNullOrWhiteSpace(job.JobId) ||
                    !seenJobIds.Add(job.JobId))
                {
                    continue;
                }

                NormalizeJob(job);
                validJobs.Add(job);
            }
        }

        _jobs.Clear();
        _jobs.AddRange(validJobs);
    }

    public void LoadFromSaveRoot(SaveRoot data)
    {
        _jobs.Clear();

        if (data == null ||
            data.World == null ||
            data.World.ActiveJobs == null)
        {
            return;
        }

        HashSet<string> seenJobIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (JobData job in data.World.ActiveJobs)
        {
            if (job == null ||
                string.IsNullOrWhiteSpace(job.JobId) ||
                !seenJobIds.Add(job.JobId))
            {
                continue;
            }

            NormalizeJob(job);
            _jobs.Add(job);
        }
    }

    public void WriteToSaveRoot(SaveRoot data)
    {
        if (data == null)
            return;

        if (data.World == null)
            data.World = new WorldState();

        data.World.ActiveJobs = GetAllJobs();
    }

    private void NormalizeJob(JobData job)
    {
        if (job.Objectives == null)
            job.Objectives = new List<string>();

        if (job.Participants == null)
            job.Participants = new List<string>();

        if (job.IsCompleted)
        {
            job.IsFailed = false;
            job.IsAvailable = false;
        }
        else if (job.IsFailed)
        {
            job.IsAvailable = false;
        }
    }
}