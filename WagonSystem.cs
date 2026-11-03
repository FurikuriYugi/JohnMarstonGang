using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly List<WagonRecord> _wagons = new List<WagonRecord>();

    public IReadOnlyList<WagonRecord> Wagons => _wagons;

    public WagonRecord RegisterWagon(string modelName, bool owned)
    {
        var wagon = new WagonRecord
        {
            WagonId = Guid.NewGuid().ToString("N"),
            ModelName = modelName,
            Owned = owned,
            Hitchable = true,
            Health = 100f
        };

        _wagons.Add(wagon);
        return wagon;
    }

    public WagonRecord GetWagon(string wagonId)
    {
        return _wagons.FirstOrDefault(x => x.WagonId == wagonId);
    }

    public bool RemoveWagon(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return false;

        return _wagons.Remove(wagon);
    }

    public void AddItemToWagon(string wagonId, string itemName)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return;

        if (string.IsNullOrWhiteSpace(itemName))
            return;

        wagon.InventoryItems.Add(itemName);
    }

    public bool RemoveItemFromWagon(string wagonId, string itemName)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return false;

        return wagon.InventoryItems.Remove(itemName);
    }

    public void DamageWagon(string wagonId, float damage)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return;

        wagon.Health = Math.Max(0f, wagon.Health - Math.Max(0f, damage));
    }

    public void RepairWagon(string wagonId, float amount)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return;

        wagon.Health = Math.Min(100f, wagon.Health + Math.Max(0f, amount));
    }

    public void SetOwned(string wagonId, bool owned)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null)
            return;

        wagon.Owned = owned;
    }

    public void LoadFromSaveData(IEnumerable<WagonSaveData> savedWagons)
    {
        _wagons.Clear();

        if (savedWagons == null)
            return;

        foreach (var saved in savedWagons)
        {
            _wagons.Add(WagonRecord.FromSaveData(saved));
        }
    }
}