using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GangRankSystem
{
    private readonly string _savePath;
    private readonly List<GangRankRecord> _ranks = new List<GangRankRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxRanks { get; set; } = 32;

    public IReadOnlyList<GangRankRecord> Ranks => _ranks;

    public GangRankSystem(string saveFileName = "GangRankSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddOrUpdateRank(string rankId, string rankName, int order, decimal wage, string permissions = null)
    {
        if (string.IsNullOrWhiteSpace(rankId) || string.IsNullOrWhiteSpace(rankName))
            return false;

        var existing = _ranks.FirstOrDefault(x => x.RankId == rankId);
        if (existing == null)
        {
            if (_ranks.Count >= MaxRanks)
                return false;

            _ranks.Add(new GangRankRecord
            {
                RankId = rankId,
                RankName = rankName,
                Order = order,
                Wage = Math.Max(0m, wage),
                Permissions = permissions,
                Enabled = true,
                CreatedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            });
        }
        else
        {
            existing.RankName = rankName;
            existing.Order = order;
            existing.Wage = Math.Max(0m, wage);
            existing.Permissions = permissions;
            existing.Enabled = true;
            existing.UpdatedUtc = DateTime.UtcNow;
        }

        Save();
        return true;
    }

    public bool RemoveRank(string rankId)
    {
        if (string.IsNullOrWhiteSpace(rankId))
            return false;

        bool removed = _ranks.RemoveAll(x => x.RankId == rankId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public bool SetRankOrder(string rankId, int order)
    {
        var rank = _ranks.FirstOrDefault(x => x.RankId == rankId);
        if (rank == null)
            return false;

        rank.Order = order;
        rank.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool SetRankWage(string rankId, decimal wage)
    {
        var rank = _ranks.FirstOrDefault(x => x.RankId == rankId);
        if (rank == null)
            return false;

        rank.Wage = Math.Max(0m, wage);
        rank.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public GangRankRecord GetRank(string rankId)
    {
        if (string.IsNullOrWhiteSpace(rankId))
            return null;

        return _ranks.FirstOrDefault(x => x.RankId == rankId);
    }

    public GangRankRecord GetHighestRank()
    {
        return _ranks.OrderByDescending(x => x.Order).FirstOrDefault();
    }

    public GangRankRecord GetLowestRank()
    {
        return _ranks.OrderBy(x => x.Order).FirstOrDefault();
    }

    public IEnumerable<GangRankRecord> GetEnabledRanks()
    {
        return _ranks.Where(x => x.Enabled).OrderBy(x => x.Order);
    }

    public bool HasRank(string rankId)
    {
        return !string.IsNullOrWhiteSpace(rankId) && _ranks.Any(x => x.RankId == rankId);
    }

    public void Clear()
    {
        _ranks.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_ranks, options));
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
            var loaded = JsonSerializer.Deserialize<List<GangRankRecord>>(json);

            _ranks.Clear();
            if (loaded != null)
                _ranks.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.RankId) && !string.IsNullOrWhiteSpace(x.RankName)));
        }
        catch
        {
            _ranks.Clear();
        }
    }
}

public class GangRankRecord
{
    public string RankId { get; set; }
    public string RankName { get; set; }
    public int Order { get; set; }
    public decimal Wage { get; set; }
    public string Permissions { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}