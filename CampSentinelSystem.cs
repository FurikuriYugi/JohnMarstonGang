using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSentinelSystem
{
    private readonly string _savePath;
    private readonly List<CampSentinelRecord> _sentinels = new List<CampSentinelRecord>();

    public int MaxSentinels { get; set; } = 64;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampSentinelRecord> Sentinels => _sentinels;

    public CampSentinelSystem(string saveFileName = "CampSentinelSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterSentinel(string name, string post, string duty = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (_sentinels.Count >= MaxSentinels)
            return false;

        _sentinels.Add(new CampSentinelRecord
        {
            SentinelId = Guid.NewGuid().ToString("N"),
            Name = name,
            Post = string.IsNullOrWhiteSpace(post) ? "Unknown" : post,
            Duty = duty,
            Notes = notes,
            IsOnDuty = true,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool SetSentinelOnDuty(string sentinelId, bool onDuty)
    {
        var sentinel = GetSentinel(sentinelId);
        if (sentinel == null)
            return false;

        sentinel.IsOnDuty = onDuty;
        sentinel.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateSentinelDuty(string sentinelId, string duty)
    {
        var sentinel = GetSentinel(sentinelId);
        if (sentinel == null)
            return false;

        sentinel.Duty = duty;
        sentinel.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateSentinelNotes(string sentinelId, string notes)
    {
        var sentinel = GetSentinel(sentinelId);
        if (sentinel == null)
            return false;

        sentinel.Notes = notes;
        sentinel.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveSentinel(string sentinelId)
    {
        if (string.IsNullOrWhiteSpace(sentinelId))
            return false;

        bool removed = _sentinels.RemoveAll(x => x.SentinelId == sentinelId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSentinelRecord GetSentinel(string sentinelId)
    {
        if (string.IsNullOrWhiteSpace(sentinelId))
            return null;

        return _sentinels.FirstOrDefault(x => x.SentinelId == sentinelId);
    }

    public IEnumerable<CampSentinelRecord> GetOnDutySentinels()
    {
        return _sentinels.Where(x => x.IsOnDuty);
    }

    public IEnumerable<CampSentinelRecord> GetSentinelsByPost(string post)
    {
        if (string.IsNullOrWhiteSpace(post))
            return _sentinels;

        return _sentinels.Where(x => string.Equals(x.Post, post, StringComparison.OrdinalIgnoreCase));
    }

    public int GetOnDutyCount()
    {
        return _sentinels.Count(x => x.IsOnDuty);
    }

    public string GetSentinelStatus()
    {
        int active = GetOnDutyCount();

        if (active >= 24)
            return "Fully Guarded";
        if (active >= 12)
            return "Well Guarded";
        if (active >= 6)
            return "Lightly Guarded";
        if (active >= 1)
            return "Sparse";

        return "Ungarded";
    }

    public void Clear()
    {
        _sentinels.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_sentinels, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSentinelRecord>>(json);

            _sentinels.Clear();
            if (loaded != null)
                _sentinels.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.SentinelId)));
        }
        catch
        {
            _sentinels.Clear();
        }
    }
}

public class CampSentinelRecord
{
    public string SentinelId { get; set; }
    public string Name { get; set; }
    public string Post { get; set; }
    public string Duty { get; set; }
    public string Notes { get; set; }
    public bool IsOnDuty { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}