// JobSystem.cs
using Newtonsoft.Json;
using RDR2;
using RDR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

public enum JobType
{
    Escort,
    Guard,
    Robbery,
    Delivery,
    Hunt,
    Rescue
}

public class JobOffer
{
    public string JobId { get; set; }
    public JobType Type { get; set; }
    public string Title { get; set; }
    public string Employer { get; set; }
    public float RewardCash { get; set; }
    public float RewardGold { get; set; }
    public float HonorDelta { get; set; }
    public bool IsGangJob { get; set; }
}

public class ActiveJob
{
    public string JobId { get; set; }
    public JobOffer Offer { get; set; }
    public DateTime AcceptedAt { get; set; }
    public bool Completed { get; set; }
    public bool Failed { get; set; }
}

public class JobSystem
{
    private readonly List<JobOffer> _offers = new List<JobOffer>();
    private readonly List<ActiveJob> _active = new List<ActiveJob>();

    public void GenerateWorldJobs()
    {
        _offers.Clear();
        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Type = JobType.Escort,
            Title = "Escort the money coach",
            Employer = "Saint Denis Bank",
            RewardCash = 75,
            RewardGold = 0,
            HonorDelta = 5,
            IsGangJob = false
        });
        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Type = JobType.Robbery,
            Title = "Rob the supply coach",
            Employer = "Unknown Contact",
            RewardCash = 250,
            RewardGold = 1,
            HonorDelta = -15,
            IsGangJob = false
        });
        _offers.Add(new JobOffer
        {
            JobId = Guid.NewGuid().ToString(),
            Type = JobType.Hunt,
            Title = "Hunt and deliver pelts",
            Employer = "Trader",
            RewardCash = 40,
            RewardGold = 0,
            HonorDelta = 3,
            IsGangJob = false
        });
    }

    public IEnumerable<JobOffer> GetOffers() => _offers;

    public bool AcceptJob(string jobId)
    {
        JobOffer offer = _offers.FirstOrDefault(x => x.JobId == jobId);
        if (offer == null) return false;

        if (_active.Any(x => x.JobId == jobId))
            return false;

        _active.Add(new ActiveJob
        {
            JobId = jobId,
            Offer = offer,
            AcceptedAt = DateTime.UtcNow,
            Completed = false,
            Failed = false
        });
        return true;
    }

    public bool CompleteJob(string jobId)
    {
        ActiveJob job = _active.FirstOrDefault(x => x.JobId == jobId);
        if (job == null || job.Completed || job.Failed)
            return false;

        job.Completed = true;
        return true;
    }

    public bool FailJob(string jobId)
    {
        ActiveJob job = _active.FirstOrDefault(x => x.JobId == jobId);
        if (job == null || job.Completed || job.Failed)
            return false;

        job.Failed = true;
        return true;
    }

    public void PayOut(string jobId, BankSystem bank, string ownerId)
    {
        ActiveJob job = _active.FirstOrDefault(x => x.JobId == jobId);
        if (job == null || !job.Completed) return;

        bank.Deposit(ownerId, job.Offer.RewardCash, job.Offer.RewardGold);
        UI.Notify($"Job paid: ${job.Offer.RewardCash}");
        _active.Remove(job);
    }

    public void Save()
    {
        try
        {
            string path = "scripts\\JohnsGangJobs.json";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JsonConvert.SerializeObject(new { Offers = _offers, Active = _active }, Formatting.Indented));
        }
        catch { }
    }
}