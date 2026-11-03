// WagonSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly List<Wagon> _wagons = new List<Wagon>();

    public IReadOnlyList<Wagon> Wagons => _wagons;

    public Wagon CreateWagon(string wagonName, string ownerId, WagonType type)
    {
        var wagon = new Wagon
        {
            WagonId = Guid.NewGuid().ToString("N"),
            WagonName = wagonName,
            OwnerId = ownerId,
            Type = type
        };

        _wagons.Add(wagon);
        return wagon;
    }

    public Wagon GetWagon(string wagonId)
    {
        return _wagons.FirstOrDefault(x => x.WagonId == wagonId);
    }

    public bool StoreItem(string wagonId, string itemName, int amount)
    {
        if (string.IsNullOrWhiteSpace(itemName) || amount <= 0) return false;

        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;

        if (!wagon.StoredItems.ContainsKey(itemName))
            wagon.StoredItems[itemName] = 0;

        wagon.StoredItems[itemName] += amount;
        return true;
    }

    public bool RemoveItem(string wagonId, string itemName, int amount)
    {
        if (string.IsNullOrWhiteSpace(itemName) || amount <= 0) return false;

        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;

        if (!wagon.StoredItems.ContainsKey(itemName))
            return false;

        if (wagon.StoredItems[itemName] < amount)
            return false;

        wagon.StoredItems[itemName] -= amount;

        if (wagon.StoredItems[itemName] <= 0)
            wagon.StoredItems.Remove(itemName);

        return true;
    }

    public bool HasItem(string wagonId, string itemName, int amount)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;
        if (!wagon.StoredItems.ContainsKey(itemName)) return false;
        return wagon.StoredItems[itemName] >= amount;
    }

    public int GetItemCount(string wagonId, string itemName)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return 0;
        return wagon.StoredItems.ContainsKey(itemName) ? wagon.StoredItems[itemName] : 0;
    }

    public bool SetOwner(string wagonId, string ownerId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;

        wagon.OwnerId = ownerId;
        return true;
    }

    public WagonSaveData CreateSaveData()
    {
        var data = new WagonSaveData();

        foreach (var wagon in _wagons)
        {
            data.Wagons.Add(new WagonSaveEntry
            {
                WagonId = wagon.WagonId,
                WagonName = wagon.WagonName,
                OwnerId = wagon.OwnerId,
                Type = wagon.Type,
                StoredItems = new Dictionary<string, int>(wagon.StoredItems)
            });
        }

        return data;
    }

    public void LoadFromSave(WagonSaveData data)
    {
        _wagons.Clear();

        if (data == null || data.Wagons == null)
            return;

        foreach (var entry in data.Wagons)
        {
            _wagons.Add(new Wagon
            {
                WagonId = entry.WagonId,
                WagonName = entry.WagonName,
                OwnerId = entry.OwnerId,
                Type = entry.Type,
                StoredItems = entry.StoredItems ?? new Dictionary<string, int>()
            });
        }
    }
}

public class Wagon
{
    public string WagonId { get; set; }
    public string WagonName { get; set; }
    public string OwnerId { get; set; }
    public WagonType Type { get; set; }
    public Dictionary<string, int> StoredItems { get; set; } = new Dictionary<string, int>();
}

public enum WagonType
{
    Cargo = 0,
    Hunting = 1,
    Trading = 2,
    Artillery = 3
}

public class WagonSaveData
{
    public List<WagonSaveEntry> Wagons { get; set; } = new List<WagonSaveEntry>();
}

public class WagonSaveEntry
{
    public string WagonId { get; set; }
    public string WagonName { get; set; }
    public string OwnerId { get; set; }
    public WagonType Type { get; set; }
    public Dictionary<string, int> StoredItems { get; set; } = new Dictionary<string, int>();
}