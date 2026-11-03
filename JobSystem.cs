using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private readonly List<JobOffer> _jobs = new List<JobOffer>();

    public IReadOnlyList<JobOffer> Jobs => _jobs;

    public JobOffer CreateJob(JobType type, string title, string description, decimal cash, decimal gold, int honorChange)
    {
        var job = new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            JobType = type,
            Title = title,
            Description = description,
            PayoutCash = cash,
            PayoutGold = gold,
            HonorChange = honorChange
        };

        _jobs.Add(job);
        return job;
    }

    public IEnumerable<JobOffer> GetAvailableJobs()
    {
        return _jobs.Where(x => !x.Completed);
    }

    public void CompleteJob(string jobId)
    {
        var job = _jobs.FirstOrDefault(x => x.JobId == jobId);
        if (job != null)
            job.Completed = true;
    }

    public void GenerateWorldJobs()
    {
        if (_jobs.Any())
            return;

        CreateJob(JobType.Escort, "Protect the coach", "Escort a money coach safely to town.", 40m, 0m, 5);
        CreateJob(JobType.Robbery, "Hit the bank run", "Intercept a bank transport and take the loot.", 150m, 2m, -10);
        CreateJob(JobType.Hunting, "Supply meat", "Bring in meat and pelts for the gang.", 20m, 0m, 3);
    }
}