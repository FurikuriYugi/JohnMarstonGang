using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly List<Wagon> _wagons = new List<Wagon>();

    public Wagon CreateWagon(string name, string ownerName, bool persistent)
    {
        Wagon wagon = new Wagon
        {
            WagonId = Guid.NewGuid().ToString("N"),
            Name = string.IsNullOrWhiteSpace(name) ? "Wagon" : name,
            OwnerName = ownerName,
            IsPersistent = persistent,
            Contents = new List<string>()
        };

        _wagons.Add(wagon);
        return wagon;
    }

    public Wagon GetWagon(string wagonId)
    {
        if (string.IsNullOrWhiteSpace(wagonId))
            return null;

        return _wagons.FirstOrDefault(w => string.Equals(w.WagonId, wagonId, StringComparison.OrdinalIgnoreCase));
    }

    public bool StoreItem(string wagonId, string itemName, int amount)
    {
        Wagon wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(itemName) || amount <= 0)
            return false;

        for (int i = 0; i < amount; i++)
            wagon.Contents.Add(itemName);

        return true;
    }

    public bool RemoveItem(string wagonId, string itemName, int amount)
    {
        Wagon wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(itemName) || amount <= 0)
            return false;

        int removed = 0;
        for (int i = wagon.Contents.Count - 1; i >= 0 && removed < amount; i--)
        {
            if (string.Equals(wagon.Contents[i], itemName, StringComparison.OrdinalIgnoreCase))
            {
                wagon.Contents.RemoveAt(i);
                removed++;
            }
        }

        return removed > 0;
    }

    public List<WagonData> CreateSaveData()
    {
        List<WagonData> data = new List<WagonData>();

        foreach (Wagon wagon in _wagons)
        {
            data.Add(new WagonData
            {
                WagonId = wagon.WagonId,
                ModelName = wagon.Name,
                OwnerName = wagon.OwnerName,
                IsOwned = !string.IsNullOrWhiteSpace(wagon.OwnerName),
                IsPersistent = wagon.IsPersistent,
                IsHookedToHorse = wagon.IsHookedToHorse,
                Contents = new List<string>(wagon.Contents),
                X = wagon.X,
                Y = wagon.Y,
                Z = wagon.Z
            });
        }

        return data;
    }

    public void LoadFromSave(WagonSaveData saveData)
    {
        _wagons.Clear();

        if (saveData == null || saveData.Wagons == null)
            return;

        foreach (WagonData item in saveData.Wagons)
        {
            if (item == null)
                continue;

            Wagon wagon = new Wagon
            {
                WagonId = item.WagonId,
                Name = item.ModelName,
                OwnerName = item.OwnerName,
                IsPersistent = item.IsPersistent,
                IsHookedToHorse = item.IsHookedToHorse,
                Contents = item.Contents ?? new List<string>(),
                X = item.X,
                Y = item.Y,
                Z = item.Z
            };

            _wagons.Add(wagon);
        }
    }
}

public class Wagon
{
    public string WagonId { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public bool IsPersistent { get; set; }
    public bool IsHookedToHorse { get; set; }
    public List<string> Contents { get; set; } = new List<string>();
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}