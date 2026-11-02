using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampWatchSystem
{
    private readonly string _savePath;
    private readonly List<CampWatchRecord> _watchList = new List<CampWatchRecord>();

    public int MaxWatchEntries { get; set; } = 64;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampWatchRecord> WatchList => _watchList;

    public CampWatchSystem(string saveFileName = "CampWatchSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterWatch(string name, string role, string zone = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (_watchList.Count >= MaxWatchEntries)
            return false;

        _watchList.Add(new CampWatchRecord
        {
            WatchId = Guid.NewGuid().ToString("N"),
            Name = name,
            Role = string.IsNullOrWhiteSpace(role) ? "Unknown" : role,
            Zone = zone,
            Notes = notes,
            IsActive = true,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool SetWatchActive(string watchId, bool active)
    {
        var watch = GetWatch(watchId);
        if (watch == null)
            return false;

        watch.IsActive = active;
        watch.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateWatchZone(string watchId, string zone)
    {
        var watch = GetWatch(watchId);
        if (watch == null)
            return false;

        watch.Zone = zone;
        watch.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateWatchNotes(string watchId, string notes)
    {
        var watch = GetWatch(watchId);
        if (watch == null)
            return false;

        watch.Notes = notes;
        watch.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveWatch(string watchId)
    {
        if (string.IsNullOrWhiteSpace(watchId))
            return false;

        bool removed = _watchList.RemoveAll(x => x.WatchId == watchId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampWatchRecord GetWatch(string watchId)
    {
        if (string.IsNullOrWhiteSpace(watchId))
            return null;

        return _watchList.FirstOrDefault(x => x.WatchId == watchId);
    }

    public IEnumerable<CampWatchRecord> GetActiveWatchers()
    {
        return _watchList.Where(x => x.IsActive);
    }

    public IEnumerable<CampWatchRecord> GetWatchersByRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return _watchList;

        return _watchList.Where(x => string.Equals(x.Role, role, StringComparison.OrdinalIgnoreCase));
    }

    public int GetActiveCount()
    {
        return _watchList.Count(x => x.IsActive);
    }

    public string GetWatchStatus()
    {
        int active = GetActiveCount();

        if (active >= 20)
            return "Heavy Watch";
        if (active >= 10)
            return "Moderate Watch";
        if (active >= 4)
            return "Thin Watch";
        if (active >= 1)
            return "Bare Watch";

        return "No Watch";
    }

    public void Clear()
    {
        _watchList.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_watchList, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampWatchRecord>>(json);

            _watchList.Clear();
            if (loaded != null)
                _watchList.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.WatchId)));
        }
        catch
        {
            _watchList.Clear();
        }
    }
}

public class CampWatchRecord
{
    public string WatchId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Zone { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}