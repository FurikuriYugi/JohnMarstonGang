using System.Collections.Generic;
using Newtonsoft.Json;

public class JobSystem
{
    private readonly List<JobData> _jobs = new List<JobData>();

    public List<JobData> GetAllJobs()
    {
        return new List<JobData>(_jobs);
    }

    public void RegisterJob(JobData job)
    {
        if (job == null || string.IsNullOrWhiteSpace(job.JobId))
            return;

        _jobs.RemoveAll(x => x != null && x.JobId == job.JobId);
        _jobs.Add(job);
    }

    public JobData GetJob(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            return null;

        return _jobs.Find(x => x != null && x.JobId == jobId);
    }

    public void CompleteJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsCompleted = true;
        job.IsFailed = false;
        job.IsAvailable = false;
    }

    public void FailJob(string jobId)
    {
        JobData job = GetJob(jobId);
        if (job == null)
            return;

        job.IsFailed = true;
        job.IsCompleted = false;
        job.IsAvailable = false;
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