using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampDefenseSystem
{
    private readonly string _savePath;
    private readonly List<CampDefenseRecord> _defenses = new List<CampDefenseRecord>();

    public int MaxDefenses { get; set; } = 32;
    public bool AutoAlertEnabled { get; set; } = true;
    public int AlertRadius { get; set; } = 80;

    public IReadOnlyList<CampDefenseRecord> Defenses => _defenses;

    public CampDefenseSystem(string saveFileName = "CampDefenseSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterDefense(string name, string defenseType, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (_defenses.Count >= MaxDefenses)
            return false;

        _defenses.Add(new CampDefenseRecord
        {
            DefenseId = Guid.NewGuid().ToString("N"),
            Name = name,
            DefenseType = string.IsNullOrWhiteSpace(defenseType) ? "Unknown" : defenseType,
            Notes = notes,
            IsActive = true,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool SetDefenseActive(string defenseId, bool active)
    {
        var defense = GetDefense(defenseId);
        if (defense == null)
            return false;

        defense.IsActive = active;
        defense.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateDefenseNotes(string defenseId, string notes)
    {
        var defense = GetDefense(defenseId);
        if (defense == null)
            return false;

        defense.Notes = notes;
        defense.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveDefense(string defenseId)
    {
        if (string.IsNullOrWhiteSpace(defenseId))
            return false;

        bool removed = _defenses.RemoveAll(x => x.DefenseId == defenseId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampDefenseRecord GetDefense(string defenseId)
    {
        if (string.IsNullOrWhiteSpace(defenseId))
            return null;

        return _defenses.FirstOrDefault(x => x.DefenseId == defenseId);
    }

    public IEnumerable<CampDefenseRecord> GetActiveDefenses()
    {
        return _defenses.Where(x => x.IsActive);
    }

    public IEnumerable<CampDefenseRecord> GetDefensesByType(string defenseType)
    {
        if (string.IsNullOrWhiteSpace(defenseType))
            return _defenses;

        return _defenses.Where(x => string.Equals(x.DefenseType, defenseType, StringComparison.OrdinalIgnoreCase));
    }

    public int GetActiveDefenseCount()
    {
        return _defenses.Count(x => x.IsActive);
    }

    public string GetDefenseStatus()
    {
        int active = GetActiveDefenseCount();

        if (active >= 16)
            return "Fortified";
        if (active >= 8)
            return "Secure";
        if (active >= 4)
            return "Guarded";
        if (active >= 1)
            return "Exposed";

        return "Unprotected";
    }

    public void Clear()
    {
        _defenses.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_defenses, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampDefenseRecord>>(json);

            _defenses.Clear();
            if (loaded != null)
                _defenses.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.DefenseId)));
        }
        catch
        {
            _defenses.Clear();
        }
    }
}

public class CampDefenseRecord
{
    public string DefenseId { get; set; }
    public string Name { get; set; }
    public string DefenseType { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}