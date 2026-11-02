using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampUpgradeSystem
{
    private readonly string _savePath;
    private readonly List<CampUpgradeRecord> _upgrades = new List<CampUpgradeRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxUpgrades { get; set; } = 24;

    public IReadOnlyList<CampUpgradeRecord> Upgrades => _upgrades;

    public CampUpgradeSystem(string saveFileName = "CampUpgradeSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddOrUpdateUpgrade(string upgradeId, string upgradeName, int level, decimal cost, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(upgradeId) || string.IsNullOrWhiteSpace(upgradeName))
            return false;

        var existing = _upgrades.FirstOrDefault(x => x.UpgradeId == upgradeId);
        if (existing == null)
        {
            if (_upgrades.Count >= MaxUpgrades)
                return false;

            _upgrades.Add(new CampUpgradeRecord
            {
                UpgradeId = upgradeId,
                UpgradeName = upgradeName,
                Level = Math.Max(0, level),
                Cost = Math.Max(0m, cost),
                Notes = notes,
                PurchasedUtc = DateTime.UtcNow,
                LastUpdatedUtc = DateTime.UtcNow,
                Enabled = true
            });
        }
        else
        {
            existing.UpgradeName = upgradeName;
            existing.Level = Math.Max(0, level);
            existing.Cost = Math.Max(0m, cost);
            existing.Notes = notes;
            existing.LastUpdatedUtc = DateTime.UtcNow;
            existing.Enabled = true;
        }

        Save();
        return true;
    }

    public bool RemoveUpgrade(string upgradeId)
    {
        if (string.IsNullOrWhiteSpace(upgradeId))
            return false;

        bool removed = _upgrades.RemoveAll(x => x.UpgradeId == upgradeId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public bool SetUpgradeLevel(string upgradeId, int level)
    {
        var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeId == upgradeId);
        if (upgrade == null)
            return false;

        upgrade.Level = Math.Max(0, level);
        upgrade.LastUpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool ToggleUpgrade(string upgradeId, bool enabled)
    {
        var upgrade = _upgrades.FirstOrDefault(x => x.UpgradeId == upgradeId);
        if (upgrade == null)
            return false;

        upgrade.Enabled = enabled;
        upgrade.LastUpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public CampUpgradeRecord GetUpgrade(string upgradeId)
    {
        if (string.IsNullOrWhiteSpace(upgradeId))
            return null;

        return _upgrades.FirstOrDefault(x => x.UpgradeId == upgradeId);
    }

    public IEnumerable<CampUpgradeRecord> GetEnabledUpgrades()
    {
        return _upgrades.Where(x => x.Enabled);
    }

    public decimal GetTotalUpgradeCost()
    {
        return _upgrades.Sum(x => x.Cost);
    }

    public int GetTotalUpgradeLevel()
    {
        return _upgrades.Sum(x => x.Level);
    }

    public bool HasUpgrade(string upgradeId)
    {
        return !string.IsNullOrWhiteSpace(upgradeId) && _upgrades.Any(x => x.UpgradeId == upgradeId);
    }

    public void Clear()
    {
        _upgrades.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_upgrades, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampUpgradeRecord>>(json);

            _upgrades.Clear();
            if (loaded != null)
                _upgrades.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.UpgradeId) && !string.IsNullOrWhiteSpace(x.UpgradeName)));
        }
        catch
        {
            _upgrades.Clear();
        }
    }
}

public class CampUpgradeRecord
{
    public string UpgradeId { get; set; }
    public string UpgradeName { get; set; }
    public int Level { get; set; }
    public decimal Cost { get; set; }
    public string Notes { get; set; }
    public bool Enabled { get; set; }
    public DateTime PurchasedUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
}