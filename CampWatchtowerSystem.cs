using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampWatchtowerSystem
{
    private readonly string _savePath;
    private readonly List<CampWatchtowerRecord> _watchtowers = new List<CampWatchtowerRecord>();

    public int MaxWatchtowers { get; set; } = 64;
    public IReadOnlyList<CampWatchtowerRecord> Watchtowers => _watchtowers;

    public CampWatchtowerSystem(string saveFileName = "CampWatchtowerSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddWatchtower(string name, string location, string duty, string notes = null, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (_watchtowers.Count >= MaxWatchtowers)
            _watchtowers.RemoveAt(0);

        _watchtowers.Add(new CampWatchtowerRecord
        {
            WatchtowerId = Guid.NewGuid().ToString("N"),
            Name = name,
            Location = string.IsNullOrWhiteSpace(location) ? "Unknown" : location,
            Duty = string.IsNullOrWhiteSpace(duty) ? "Overwatch" : duty,
            Notes = notes,
            IsActive = isActive,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool SetActive(string watchtowerId, bool isActive)
    {
        var watchtower = GetWatchtower(watchtowerId);
        if (watchtower == null)
            return false;

        watchtower.IsActive = isActive;
        watchtower.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateDuty(string watchtowerId, string duty, string notes = null)
    {
        var watchtower = GetWatchtower(watchtowerId);
        if (watchtower == null)
            return false;

        if (!string.IsNullOrWhiteSpace(duty))
            watchtower.Duty = duty;

        if (!string.IsNullOrWhiteSpace(notes))
            watchtower.Notes = notes;

        watchtower.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveWatchtower(string watchtowerId)
    {
        if (string.IsNullOrWhiteSpace(watchtowerId))
            return false;

        bool removed = _watchtowers.RemoveAll(x => x.WatchtowerId == watchtowerId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampWatchtowerRecord GetWatchtower(string watchtowerId)
    {
        if (string.IsNullOrWhiteSpace(watchtowerId))
            return null;

        return _watchtowers.FirstOrDefault(x => x.WatchtowerId == watchtowerId);
    }

    public IEnumerable<CampWatchtowerRecord> GetActiveWatchtowers()
    {
        return _watchtowers.Where(x => x.IsActive)
            .OrderBy(x => x.Name);
    }

    public IEnumerable<CampWatchtowerRecord> GetWatchtowersByLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return _watchtowers.OrderByDescending(x => x.CreatedUtc);

        return _watchtowers.Where(x => string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Name);
    }

    public int GetActiveCount()
    {
        return _watchtowers.Count(x => x.IsActive);
    }

    public string GetWatchtowerStatus()
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
        _watchtowers.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_watchtowers, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampWatchtowerRecord>>(json);

            _watchtowers.Clear();
            if (loaded != null)
                _watchtowers.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.WatchtowerId)));
        }
        catch
        {
            _watchtowers.Clear();
        }
    }
}

public class CampWatchtowerRecord
{
    public string WatchtowerId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Duty { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}