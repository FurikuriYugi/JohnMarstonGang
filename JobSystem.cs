using System.Collections.Generic;
using Newtonsoft.Json;

public class JobSystem
{
    private readonly Dictionary<string, JobRecord> _jobs = new Dictionary<string, JobRecord>();

    public void RegisterJob(JobRecord job)
    {
        if (job == null || string.IsNullOrWhiteSpace(job.JobId))
            return;

        _jobs[job.JobId] = job;
    }

    public bool HasJob(string jobId)
    {
        return !string.IsNullOrWhiteSpace(jobId) && _jobs.ContainsKey(jobId);
    }

    public JobRecord GetJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return null;

        _jobs.TryGetValue(jobId, out JobRecord job);
        return job;
    }

    public List<JobRecord> GetAllJobs()
    {
        return new List<JobRecord>(_jobs.Values);
    }

    public void LoadJobs(List<JobData> jobs)
    {
        _jobs.Clear();

        if (jobs == null)
            return;

        foreach (JobData job in jobs)
        {
            if (job == null || string.IsNullOrWhiteSpace(job.JobId))
                continue;

            _jobs[job.JobId] = new JobRecord
            {
                JobId = job.JobId,
                Title = job.Title,
                Faction = job.Faction,
                PayoutCash = job.PayoutCash,
                PayoutGold = job.PayoutGold,
                HonorChange = job.HonorChange,
                IsAvailable = job.IsAvailable,
                IsCompleted = job.IsCompleted
            };
        }
    }

    public List<JobData> GetJobsAsData()
    {
        List<JobData> result = new List<JobData>();

        foreach (JobRecord job in _jobs.Values)
        {
            result.Add(new JobData
            {
                JobId = job.JobId,
                Title = job.Title,
                Faction = job.Faction,
                PayoutCash = job.PayoutCash,
                PayoutGold = job.PayoutGold,
                HonorChange = job.HonorChange,
                IsAvailable = job.IsAvailable,
                IsCompleted = job.IsCompleted
            });
        }

        return result;
    }

    public void MarkCompleted(string jobId)
    {
        JobRecord job = GetJob(jobId);
        if (job == null)
            return;

        job.IsCompleted = true;
        job.IsAvailable = false;
    }

    public void SetAvailability(string jobId, bool available)
    {
        JobRecord job = GetJob(jobId);
        if (job == null)
            return;

        job.IsAvailable = available;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetJobsAsData(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _jobs.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<JobData> jobs = JsonConvert.DeserializeObject<List<JobData>>(json);
        LoadJobs(jobs);
    }
}