using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampTerritorySystem
{
    private readonly string _savePath;
    private readonly List<CampTerritoryRecord> _territories = new List<CampTerritoryRecord>();

    public int MaxTerritories { get; set; } = 64;
    public bool EnableTerritoryControl { get; set; } = true;

    public IReadOnlyList<CampTerritoryRecord> Territories => _territories;

    public CampTerritorySystem(string saveFileName = "CampTerritorySystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterTerritory(string name, string region, string owner = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (_territories.Count >= MaxTerritories)
            return false;

        _territories.Add(new CampTerritoryRecord
        {
            TerritoryId = Guid.NewGuid().ToString("N"),
            Name = name,
            Region = string.IsNullOrWhiteSpace(region) ? "Unknown" : region,
            Owner = owner,
            Notes = notes,
            IsControlled = true,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool SetTerritoryControlled(string territoryId, bool controlled)
    {
        var territory = GetTerritory(territoryId);
        if (territory == null)
            return false;

        territory.IsControlled = controlled;
        territory.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool ChangeTerritoryOwner(string territoryId, string owner)
    {
        var territory = GetTerritory(territoryId);
        if (territory == null)
            return false;

        territory.Owner = owner;
        territory.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateTerritoryNotes(string territoryId, string notes)
    {
        var territory = GetTerritory(territoryId);
        if (territory == null)
            return false;

        territory.Notes = notes;
        territory.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveTerritory(string territoryId)
    {
        if (string.IsNullOrWhiteSpace(territoryId))
            return false;

        bool removed = _territories.RemoveAll(x => x.TerritoryId == territoryId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampTerritoryRecord GetTerritory(string territoryId)
    {
        if (string.IsNullOrWhiteSpace(territoryId))
            return null;

        return _territories.FirstOrDefault(x => x.TerritoryId == territoryId);
    }

    public IEnumerable<CampTerritoryRecord> GetTerritoriesByRegion(string region)
    {
        if (string.IsNullOrWhiteSpace(region))
            return _territories;

        return _territories.Where(x => string.Equals(x.Region, region, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CampTerritoryRecord> GetControlledTerritories()
    {
        return _territories.Where(x => x.IsControlled);
    }

    public IEnumerable<CampTerritoryRecord> GetUncontrolledTerritories()
    {
        return _territories.Where(x => !x.IsControlled);
    }

    public int GetControlledCount()
    {
        return _territories.Count(x => x.IsControlled);
    }

    public string GetTerritoryStatus()
    {
        int controlled = GetControlledCount();

        if (controlled >= 48)
            return "Dominant";
        if (controlled >= 24)
            return "Strong";
        if (controlled >= 12)
            return "Stable";
        if (controlled >= 1)
            return "Fragile";

        return "Lost";
    }

    public void Clear()
    {
        _territories.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_territories, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampTerritoryRecord>>(json);

            _territories.Clear();
            if (loaded != null)
                _territories.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.TerritoryId)));
        }
        catch
        {
            _territories.Clear();
        }
    }
}

public class CampTerritoryRecord
{
    public string TerritoryId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Owner { get; set; }
    public string Notes { get; set; }
    public bool IsControlled { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}