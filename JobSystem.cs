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
            RequiresHonorAbove = -100,
            RequiresAlliance = "Lawful"
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
            RequiresHonorAbove = -100,
            RequiresAlliance = "Outlaw"
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
            RequiresHonorAbove = -100,
            RequiresAlliance = "Any"
        });

        _availableJobs.Add(new JobRecord
        {
            Id = "bank_deposit",
            Title = "Deposit funds",
            Description = "Carry cash and gold safely to the bank.",
            Type = "BankDeposit",
            RewardMoney = 20m,
            HonorChange = 3,
            Risk = 5,
            RequiresHonorAbove = -100,
            RequiresAlliance = "Lawful"
        });

        _availableJobs.Add(new JobRecord
        {
            Id = "bank_robbery",
            Title = "Rob the bank",
            Description = "Break into the bank vault and take cash and gold.",
            Type = "BankRobbery",
            RewardMoney = 250m,
            HonorChange = -20,
            Risk = 90,
            RequiresHonorAbove = -100,
            RequiresAlliance = "Outlaw"
        });
    }

    public List<JobRecord> GetJobsForGang(GangManager gang)
    {
        int averageLoyalty = 0;

        if (gang != null && gang.Members != null && gang.Members.Count > 0)
        {
            averageLoyalty = (int)gang.Members.Average(x => x.Loyalty);
        }

        string alignment = DetermineGangAlignment(gang);

        return _availableJobs
            .Where(job => IsJobAllowed(job, alignment, averageLoyalty))
            .Select(CloneJob)
            .ToList();
    }

    public JobRecord GetBestJobForGang(GangManager gang)
    {
        List<JobRecord> jobs = GetJobsForGang(gang);
        if (jobs.Count == 0)
            return null;

        int averageLoyalty = 0;
        if (gang != null && gang.Members != null && gang.Members.Count > 0)
        {
            averageLoyalty = (int)gang.Members.Average(x => x.Loyalty);
        }

        string alignment = DetermineGangAlignment(gang);

        return jobs
            .OrderByDescending(job => ScoreJob(job, alignment, averageLoyalty))
            .FirstOrDefault();
    }

    public bool ShouldAcceptJob(JobRecord job, GangManager gang)
    {
        if (job == null)
            return false;

        int averageLoyalty = 0;
        if (gang != null && gang.Members != null && gang.Members.Count > 0)
        {
            averageLoyalty = (int)gang.Members.Average(x => x.Loyalty);
        }

        string alignment = DetermineGangAlignment(gang);
        return IsJobAllowed(job, alignment, averageLoyalty);
    }

    public bool VoteOnJob(GangManager gang, JobRecord job)
    {
        if (gang == null || job == null || gang.Members == null)
            return false;

        int totalVotes = 0;
        int yesVotes = 0;

        foreach (GangMember member in gang.Members)
        {
            if (member == null)
                continue;

            totalVotes++;

            if (ShouldMemberVoteYes(member, job))
                yesVotes++;
        }

        if (totalVotes == 0)
            return true;

        return yesVotes > (totalVotes / 2);
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

    public void ResolveJobReward(string jobId, GangManager gang)
    {
        JobRecord job = _availableJobs.FirstOrDefault(x => x.Id == jobId);
        if (job == null || gang == null || gang.Members == null)
            return;

        foreach (GangMember member in gang.Members)
        {
            if (member == null)
                continue;

            member.Experience += System.Math.Max(1, job.Risk / 10);
            member.Loyalty = Clamp(member.Loyalty + System.Math.Sign(job.HonorChange), 0, 100);
        }
    }

    private bool IsJobAllowed(JobRecord job, string alignment, int averageLoyalty)
    {
        if (job == null)
            return false;

        if (averageLoyalty < job.RequiresLoyaltyAbove)
            return false;

        if (job.RequiresHonorAbove > -100 && averageLoyalty < job.RequiresHonorAbove)
            return false;

        if (job.RequiresAlliance == null || job.RequiresAlliance == "Any")
            return true;

        return job.RequiresAlliance == alignment;
    }

    private int ScoreJob(JobRecord job, string alignment, int averageLoyalty)
    {
        int score = 0;

        if (job.RewardMoney >= 200m)
            score = 30;
        else if (job.RewardMoney >= 100m)
            score = 20;
        else if (job.RewardMoney >= 50m)
            score = 10;

        if (job.HonorChange >= 0)
            score += 5;

        if (job.Risk <= 20)
            score += 8;

        if (averageLoyalty >= 75)
            score += 5;

        if (job.RequiresAlliance == alignment)
            score += 10;

        return score;
    }

    private string DetermineGangAlignment(GangManager gang)
    {
        if (gang == null || gang.Members == null || gang.Members.Count == 0)
            return "Any";

        int loyalCount = 0;
        int outlawCount = 0;

        foreach (GangMember member in gang.Members)
        {
            if (member == null)
                continue;

            if (member.Alignment == "Loyal" || member.Alignment == "Friendly")
                loyalCount++;
            else if (member.Alignment == "Hostile" || member.Alignment == "Wary")
                outlawCount++;
        }

        if (loyalCount > outlawCount)
            return "Lawful";

        if (outlawCount > loyalCount)
            return "Outlaw";

        return "Any";
    }

    private bool ShouldMemberVoteYes(GangMember member, JobRecord job)
    {
        if (member == null || job == null)
            return false;

        if (job.Type == "Robbery" || job.Type == "BankRobbery")
            return member.Alignment == "Hostile" || member.Alignment == "Wary" || member.Loyalty >= 70;

        if (job.Type == "Escort" || job.Type == "Delivery" || job.Type == "BankDeposit")
            return member.Alignment == "Loyal" || member.Alignment == "Friendly" || member.Loyalty >= 50;

        return member.Loyalty >= 40;
    }

    private static int Clamp(int value, int min, int max)
    {
        if (value < min)
            return min;

        if (value > max)
            return max;

        return value;
    }

    private static JobRecord CloneJob(JobRecord job)
    {
        return new JobRecord
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Type = job.Type,
            RewardMoney = job.RewardMoney,
            HonorChange = job.HonorChange,
            Risk = job.Risk,
            RequiresHonorAbove = job.RequiresHonorAbove,
            RequiresAlliance = job.RequiresAlliance,
            RequiresLoyaltyAbove = job.RequiresLoyaltyAbove,
            IsAccepted = job.IsAccepted,
            IsCompleted = job.IsCompleted
        };
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
    public int RequiresLoyaltyAbove { get; set; }
    public string RequiresAlliance { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsCompleted { get; set; }
}