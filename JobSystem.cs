using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private readonly List<JobRecord> _availableJobs = new List<JobRecord>();

    public IReadOnlyList<JobRecord> AvailableJobs
    {
        get { return _availableJobs; }
    }

    public void GenerateWorldJobs()
    {
        _availableJobs.Clear();

        _availableJobs.Add(new JobRecord
        {
            Id = "escort_bank_coach",
            Title = "Protect the bank coach",
            Description = "Escort the coach carrying cash and gold to Saint Denis.",
            Type = "Escort",
            RewardMoney = 45m,
            HonorChange = 5,
            Risk = 20,
            RequiresHonorAbove = -100
        });

        _availableJobs.Add(new JobRecord
        {
            Id = "rob_stage_coach",
            Title = "Rob the stage coach",
            Description = "Ambush a stage coach and take its strongbox.",
            Type = "Robbery",
            RewardMoney = 120m,
            HonorChange = -10,
            Risk = 70,
            RequiresHonorAbove = -100
        });

        _availableJobs.Add(new JobRecord
        {
            Id = "deliver_supplies",
            Title = "Deliver supplies",
            Description = "Move food and ammo to a frontier camp.",
            Type = "Delivery",
            RewardMoney = 30m,
            HonorChange = 2,
            Risk = 10,
            RequiresHonorAbove = -100
        });
    }

    public List<JobRecord> GetJobsForGang(GangManager gang)
    {
        if (gang == null)
            return new List<JobRecord>(_availableJobs);

        return new List<JobRecord>(_availableJobs);
    }

    public void AcceptJob(string jobId)
    {
        JobRecord job = _availableJobs.FirstOrDefault(x => x.Id == jobId);
        if (job == null)
            return;

        job.IsAccepted = true;
    }

    public void CompleteJob(string jobId)
    {
        JobRecord job = _availableJobs.FirstOrDefault(x => x.Id == jobId);
        if (job == null)
            return;

        job.IsCompleted = true;
    }
}

public class JobRecord
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public decimal RewardMoney { get; set; }
    public int HonorChange { get; set; }
    public int Risk { get; set; }
    public int RequiresHonorAbove { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsCompleted { get; set; }
}