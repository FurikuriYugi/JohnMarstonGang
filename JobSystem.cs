using System.Collections.Generic;
using Newtonsoft.Json;

public class JobSystem
{
    private readonly Dictionary<string, JobData> _jobs = new Dictionary<string, JobData>();

    public void RegisterJob(JobData job)
    {
        if (job == null || string.IsNullOrWhiteSpace(job.JobId))
            return;

        _jobs[job.JobId] = job;
    }

    public bool HasJob(string jobId)
    {
        return !string.IsNullOrWhiteSpace(jobId) && _jobs.ContainsKey(jobId);
    }

    public JobData GetJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return null;

        _jobs.TryGetValue(jobId, out JobData job);
        return job;
    }

    public List<JobData> GetAllJobs()
    {
        return new List<JobData>(_jobs.Values);
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

            _jobs[job.JobId] = job;
        }
    }

    public void MarkCompleted(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsCompleted = true;
        job.IsFailed = false;
        job.IsAvailable = false;
    }

    public void MarkFailed(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsFailed = true;
        job.IsCompleted = false;
        job.IsAvailable = false;
    }

    public void SetAvailability(string jobId, bool available)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsAvailable = available;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllJobs(), Formatting.Indented);
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