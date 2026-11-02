using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyInventorySystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyInventoryRecord> _items = new List<CampSupplyInventoryRecord>();

    public int MaxItems { get; set; } = 128;
    public IReadOnlyList<CampSupplyInventoryRecord> Items => _items;

    public CampSupplyInventorySystem(string saveFileName = "CampSupplyInventorySystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddItem(string name, int quantity, string source = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (quantity <= 0)
            quantity = 1;

        var existing = _items.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        if (existing != null)
        {
            existing.Quantity += quantity;
            existing.Source = string.IsNullOrWhiteSpace(source) ? existing.Source : source;
            existing.Notes = string.IsNullOrWhiteSpace(notes) ? existing.Notes : notes;
            existing.UpdatedUtc = DateTime.UtcNow;
            Save();
            return true;
        }

        if (_items.Count >= MaxItems)
            _items.RemoveAt(0);

        _items.Add(new CampSupplyInventoryRecord
        {
            ItemId = Guid.NewGuid().ToString("N"),
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

    public bool RemoveItem(string itemId, int quantity)
    {
        if (string.IsNullOrWhiteSpace(itemId) || quantity <= 0)
            return false;

        var item = GetItem(itemId);
        if (item == null)
            return false;

        item.Quantity -= quantity;
        if (item.Quantity <= 0)
            _items.Remove(item);
        else
            item.UpdatedUtc = DateTime.UtcNow;

        Save();
        return true;
    }

    public CampSupplyInventoryRecord GetItem(string itemId)
    {
        if (string.IsNullOrWhiteSpace(itemId))
            return null;

        return _items.FirstOrDefault(x => x.ItemId == itemId);
    }

    public IEnumerable<CampSupplyInventoryRecord> GetAllItems()
    {
        return _items.OrderByDescending(x => x.UpdatedUtc);
    }

    public int GetTotalQuantity()
    {
        return _items.Sum(x => x.Quantity);
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public string GetInventoryStatus()
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
        _items.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_items, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSupplyInventoryRecord>>(json);

            _items.Clear();
            if (loaded != null)
                _items.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.ItemId)));
        }
        catch
        {
            _items.Clear();
        }
    }
}

public class CampSupplyInventoryRecord
{
    public string ItemId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Source { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}