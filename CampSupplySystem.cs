using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplySystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyRecord> _supplies = new List<CampSupplyRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxSupplyTypes { get; set; } = 64;

    public IReadOnlyList<CampSupplyRecord> Supplies => _supplies;

    public CampSupplySystem(string saveFileName = "CampSupplySystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddOrUpdateSupply(string supplyId, string supplyName, int quantity, decimal unitCost, string source = null)
    {
        if (string.IsNullOrWhiteSpace(supplyId) || string.IsNullOrWhiteSpace(supplyName))
            return false;

        var existing = _supplies.FirstOrDefault(x => x.SupplyId == supplyId);
        if (existing == null)
        {
            if (_supplies.Count >= MaxSupplyTypes)
                return false;

            _supplies.Add(new CampSupplyRecord
            {
                SupplyId = supplyId,
                SupplyName = supplyName,
                Quantity = Math.Max(0, quantity),
                UnitCost = Math.Max(0m, unitCost),
                Source = source,
                Enabled = true,
                AddedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            });
        }
        else
        {
            existing.SupplyName = supplyName;
            existing.Quantity = Math.Max(0, quantity);
            existing.UnitCost = Math.Max(0m, unitCost);
            existing.Source = source;
            existing.Enabled = true;
            existing.UpdatedUtc = DateTime.UtcNow;
        }

        Save();
        return true;
    }

    public bool AdjustQuantity(string supplyId, int delta)
    {
        var supply = _supplies.FirstOrDefault(x => x.SupplyId == supplyId);
        if (supply == null)
            return false;

        supply.Quantity = Math.Max(0, supply.Quantity + delta);
        supply.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveSupply(string supplyId)
    {
        if (string.IsNullOrWhiteSpace(supplyId))
            return false;

        bool removed = _supplies.RemoveAll(x => x.SupplyId == supplyId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyRecord GetSupply(string supplyId)
    {
        if (string.IsNullOrWhiteSpace(supplyId))
            return null;

        return _supplies.FirstOrDefault(x => x.SupplyId == supplyId);
    }

    public IEnumerable<CampSupplyRecord> GetEnabledSupplies()
    {
        return _supplies.Where(x => x.Enabled);
    }

    public int GetTotalQuantity()
    {
        return _supplies.Sum(x => x.Quantity);
    }

    public decimal GetInventoryValue()
    {
        return _supplies.Sum(x => x.Quantity * x.UnitCost);
    }

    public bool HasSupply(string supplyId)
    {
        return !string.IsNullOrWhiteSpace(supplyId) && _supplies.Any(x => x.SupplyId == supplyId);
    }

    public void Clear()
    {
        _supplies.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_supplies, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSupplyRecord>>(json);

            _supplies.Clear();
            if (loaded != null)
                _supplies.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.SupplyId) && !string.IsNullOrWhiteSpace(x.SupplyName)));
        }
        catch
        {
            _supplies.Clear();
        }
    }
}

public class CampSupplyRecord
{
    public string SupplyId { get; set; }
    public string SupplyName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string Source { get; set; }
    public bool Enabled { get; set; }
    public DateTime AddedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}