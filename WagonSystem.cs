using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WagonSystem
{
    private readonly Dictionary<string, WagonRecord> _wagons = new Dictionary<string, WagonRecord>();

    public void RegisterWagon(WagonRecord wagon)
    {
        if (wagon == null || string.IsNullOrWhiteSpace(wagon.WagonId))
            return;

        _wagons[wagon.WagonId] = wagon;
    }

    public bool OwnWagon(string wagonId)
    {
        if (string.IsNullOrWhiteSpace(wagonId) || !_wagons.ContainsKey(wagonId))
            return false;

        _wagons[wagonId].IsOwned = true;
        return true;
    }

    public bool StoreItem(string wagonId, string itemName, int amount)
    {
        if (!_wagons.ContainsKey(wagonId))
            return false;

        WagonRecord wagon = _wagons[wagonId];
        if (wagon.StoredItems == null)
            wagon.StoredItems = new Dictionary<string, int>();

        if (string.IsNullOrWhiteSpace(itemName) || amount <= 0)
            return false;

        if (!wagon.StoredItems.ContainsKey(itemName))
            wagon.StoredItems[itemName] = 0;

        wagon.StoredItems[itemName] += amount;
        return true;
    }

    public bool RemoveItem(string wagonId, string itemName, int amount)
    {
        if (!_wagons.ContainsKey(wagonId))
            return false;

        WagonRecord wagon = _wagons[wagonId];
        if (wagon.StoredItems == null || !wagon.StoredItems.ContainsKey(itemName))
            return false;

        if (amount <= 0 || wagon.StoredItems[itemName] < amount)
            return false;

        wagon.StoredItems[itemName] -= amount;
        if (wagon.StoredItems[itemName] <= 0)
            wagon.StoredItems.Remove(itemName);

        return true;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_wagons.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _wagons.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WagonRecord> wagons = JsonConvert.DeserializeObject<List<WagonRecord>>(json);
        if (wagons == null)
            return;

        foreach (WagonRecord wagon in wagons)
        {
            if (wagon != null && !string.IsNullOrWhiteSpace(wagon.WagonId))
                _wagons[wagon.WagonId] = wagon;
        }
    }
}

public class WagonRecord
{
    public string WagonId { get; set; }
    public string DisplayName { get; set; }
    public bool IsOwned { get; set; }
    public string OwnerName { get; set; }
    public Dictionary<string, int> StoredItems { get; set; } = new Dictionary<string, int>();
    public bool HasHitchSupport { get; set; } = true;
    public bool PersistentAcrossReloads { get; set; } = true;
}