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

    public bool AcceptJob(string jobId)
    {
        if (!_jobs.ContainsKey(jobId))
            return false;

        JobRecord job = _jobs[jobId];
        if (!job.IsAvailable || job.IsCompleted)
            return false;

        job.IsAvailable = false;
        return true;
    }

    public bool CompleteJob(string jobId)
    {
        if (!_jobs.ContainsKey(jobId))
            return false;

        JobRecord job = _jobs[jobId];
        if (job.IsCompleted)
            return false;

        job.IsCompleted = true;
        job.IsAvailable = false;
        return true;
    }

    public int GetHonorChange(string jobId)
    {
        if (!_jobs.ContainsKey(jobId))
            return 0;

        return _jobs[jobId].HonorChange;
    }

    public decimal GetCashPayout(string jobId)
    {
        if (!_jobs.ContainsKey(jobId))
            return 0m;

        return _jobs[jobId].PayoutCash;
    }

    public decimal GetGoldPayout(string jobId)
    {
        if (!_jobs.ContainsKey(jobId))
            return 0m;

        return _jobs[jobId].PayoutGold;
    }

    public List<JobRecord> GetAllJobs()
    {
        return new List<JobRecord>(_jobs.Values);
    }

    public void LoadJobs(List<JobRecord> jobs)
    {
        _jobs.Clear();

        if (jobs == null)
            return;

        foreach (JobRecord job in jobs)
        {
            if (job != null && !string.IsNullOrWhiteSpace(job.JobId))
                _jobs[job.JobId] = job;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_jobs.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _jobs.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<JobRecord> jobs = JsonConvert.DeserializeObject<List<JobRecord>>(json);
        if (jobs == null)
            return;

        foreach (JobRecord job in jobs)
        {
            if (job != null && !string.IsNullOrWhiteSpace(job.JobId))
                _jobs[job.JobId] = job;
        }
    }
}