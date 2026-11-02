using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampRationSystem
{
    private readonly string _savePath;
    private readonly List<CampRationRecord> _rations = new List<CampRationRecord>();

    public int MaxRationTypes { get; set; } = 64;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampRationRecord> Rations => _rations;

    public CampRationSystem(string saveFileName = "CampRationSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterRation(string name, int quantity, string source = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name) || quantity < 0)
            return false;

        if (_rations.Count >= MaxRationTypes)
            return false;

        _rations.Add(new CampRationRecord
        {
            RationId = Guid.NewGuid().ToString("N"),
            Name = name,
            Quantity = quantity,
            Source = source,
            Notes = notes,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool AddQuantity(string rationId, int amount)
    {
        var ration = GetRation(rationId);
        if (ration == null || amount == 0)
            return false;

        ration.Quantity += amount;
        if (ration.Quantity < 0)
            ration.Quantity = 0;

        ration.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool SetQuantity(string rationId, int quantity)
    {
        var ration = GetRation(rationId);
        if (ration == null || quantity < 0)
            return false;

        ration.Quantity = quantity;
        ration.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool SetRationNotes(string rationId, string notes)
    {
        var ration = GetRation(rationId);
        if (ration == null)
            return false;

        ration.Notes = notes;
        ration.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveRation(string rationId)
    {
        if (string.IsNullOrWhiteSpace(rationId))
            return false;

        bool removed = _rations.RemoveAll(x => x.RationId == rationId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampRationRecord GetRation(string rationId)
    {
        if (string.IsNullOrWhiteSpace(rationId))
            return null;

        return _rations.FirstOrDefault(x => x.RationId == rationId);
    }

    public CampRationRecord GetRationByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _rations.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CampRationRecord> GetLowRations(int threshold = 10)
    {
        return _rations.Where(x => x.Quantity <= threshold);
    }

    public int GetTotalQuantity()
    {
        return _rations.Sum(x => x.Quantity);
    }

    public string GetSupplyStatus()
    {
        int total = GetTotalQuantity();

        if (total >= 500)
            return "Abundant";
        if (total >= 250)
            return "Stable";
        if (total >= 100)
            return "Low";
        if (total >= 25)
            return "Critical";

        return "Bare";
    }

    public void Clear()
    {
        _rations.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_rations, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampRationRecord>>(json);

            _rations.Clear();
            if (loaded != null)
                _rations.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.RationId)));
        }
        catch
        {
            _rations.Clear();
        }
    }
}

public class CampRationRecord
{
    public string RationId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Source { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}