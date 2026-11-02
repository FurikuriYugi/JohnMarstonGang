using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class RecruitMenuSystem
{
    private readonly string _savePath;
    private readonly List<RecruitOffer> _offers = new List<RecruitOffer>();

    public bool Enabled { get; set; } = true;
    public int MaxOffers { get; set; } = 12;
    public int OfferLifetimeMinutes { get; set; } = 60;
    public decimal BaseRecruitCost { get; set; } = 12m;

    public IReadOnlyList<RecruitOffer> Offers => _offers;

    public RecruitMenuSystem(string saveFileName = "RecruitMenuSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public void RefreshOffers(IEnumerable<NpcRecruitCandidate> candidates)
    {
        if (!Enabled)
            return;

        CleanupExpiredOffers();

        if (candidates == null)
            return;

        foreach (var candidate in candidates)
        {
            if (candidate == null || string.IsNullOrWhiteSpace(candidate.Id))
                continue;

            if (_offers.Any(x => x.CandidateId == candidate.Id))
                continue;

            if (_offers.Count >= MaxOffers)
                break;

            if (!candidate.IsWillingToTalk)
                continue;

            var offer = new RecruitOffer
            {
                OfferId = Guid.NewGuid().ToString("N"),
                CandidateId = candidate.Id,
                CandidateName = candidate.Name ?? "Unknown",
                Cost = CalculateCost(candidate),
                CreatedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(OfferLifetimeMinutes),
                RequiredHonor = candidate.RequiredHonor
            };

            _offers.Add(offer);
        }

        Save();
    }

    public bool TryAcceptOffer(string offerId, decimal availableMoney, out decimal cost, out NpcRecruitCandidate recruitedCandidate)
    {
        cost = 0m;
        recruitedCandidate = null;

        if (string.IsNullOrWhiteSpace(offerId))
            return false;

        CleanupExpiredOffers();

        var offer = _offers.FirstOrDefault(x => x.OfferId == offerId);
        if (offer == null)
            return false;

        cost = offer.Cost;
        if (availableMoney < cost)
            return false;

        recruitedCandidate = new NpcRecruitCandidate
        {
            Id = offer.CandidateId,
            Name = offer.CandidateName,
            IsWillingToTalk = true,
            RequiredHonor = offer.RequiredHonor
        };

        _offers.Remove(offer);
        Save();
        return true;
    }

    public bool RejectOffer(string offerId)
    {
        if (string.IsNullOrWhiteSpace(offerId))
            return false;

        bool removed = _offers.RemoveAll(x => x.OfferId == offerId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public void ClearAll()
    {
        _offers.Clear();
        Save();
    }

    public void CleanupExpiredOffers()
    {
        DateTime now = DateTime.UtcNow;
        bool changed = _offers.RemoveAll(x => x.ExpiresUtc <= now) > 0;
        if (changed)
            Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_offers, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<RecruitOffer>>(json);

            _offers.Clear();
            if (loaded != null)
                _offers.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.OfferId) && !string.IsNullOrWhiteSpace(x.CandidateId)));
        }
        catch
        {
            _offers.Clear();
        }
    }

    private decimal CalculateCost(NpcRecruitCandidate candidate)
    {
        decimal cost = BaseRecruitCost;

        if (candidate.IsWanted)
            cost += 25m;

        if (candidate.IsArmored)
            cost += 10m;

        if (candidate.RequiredHonor < 0)
            cost += 8m;

        if (candidate.RequiredHonor > 0)
            cost -= 2m;

        if (!string.IsNullOrWhiteSpace(candidate.Specialty))
        {
            switch (candidate.Specialty.ToLowerInvariant())
            {
                case "gunslinger":
                    cost += 18m;
                    break;
                case "scout":
                    cost += 10m;
                    break;
                case "medic":
                    cost += 15m;
                    break;
                case "driver":
                    cost += 12m;
                    break;
            }
        }

        return Math.Max(5m, cost);
    }
}

public class RecruitOffer
{
    public string OfferId { get; set; }
    public string CandidateId { get; set; }
    public string CandidateName { get; set; }
    public decimal Cost { get; set; }
    public int RequiredHonor { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime ExpiresUtc { get; set; }
}

public class NpcRecruitCandidate
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsWillingToTalk { get; set; }
    public bool IsWanted { get; set; }
    public bool IsArmored { get; set; }
    public int RequiredHonor { get; set; }
    public string Specialty { get; set; }
}