using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly Dictionary<string, WagonState> _wagons = new Dictionary<string, WagonState>(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyCollection<WagonState> Wagons => _wagons.Values.ToList().AsReadOnly();

    public WagonState CreateOrGetWagon(string wagonId, string modelName, string ownerName)
    {
        if (string.IsNullOrWhiteSpace(wagonId))
            wagonId = Guid.NewGuid().ToString("N");

        if (_wagons.TryGetValue(wagonId, out WagonState existing))
            return existing;

        WagonState wagon = new WagonState
        {
            WagonId = wagonId,
            ModelName = string.IsNullOrWhiteSpace(modelName) ? "UnknownWagon" : modelName,
            OwnerName = string.IsNullOrWhiteSpace(ownerName) ? "Unknown" : ownerName,
            IsOwned = true,
            IsPersistent = true,
            IsHookedToHorse = false
        };

        _wagons[wagonId] = wagon;
        return wagon;
    }

    public Wagon CreateWagon(string wagonName, string ownerId, WagonType type)
    {
        WagonState state = CreateOrGetWagon(wagonName, type.ToString(), ownerId);
        return new Wagon(state);
    }

    public bool StoreItem(string wagonId, string itemName, int amount)
    {
        if (string.IsNullOrWhiteSpace(itemName) || amount <= 0)
            return false;

        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", "Unknown");
        for (int i = 0; i < amount; i++)
            wagon.Contents.Add(itemName);

        return true;
    }

    public bool RemoveItem(string wagonId, string itemName, int amount)
    {
        if (string.IsNullOrWhiteSpace(itemName) || amount <= 0)
            return false;

        if (!_wagons.TryGetValue(wagonId, out WagonState wagon))
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

    public void SetOwned(string wagonId, bool isOwned, string ownerName = null)
    {
        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", ownerName ?? "Unknown");
        wagon.IsOwned = isOwned;
        if (!string.IsNullOrWhiteSpace(ownerName))
            wagon.OwnerName = ownerName;
    }

    public void SetPersistent(string wagonId, bool persistent)
    {
        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", "Unknown");
        wagon.IsPersistent = persistent;
    }

    public void SetHooked(string wagonId, bool hooked)
    {
        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", "Unknown");
        wagon.IsHookedToHorse = hooked;
    }

    public void MoveWagon(string wagonId, double x, double y, double z)
    {
        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", "Unknown");
        wagon.X = x;
        wagon.Y = y;
        wagon.Z = z;
    }

    public void AddContent(string wagonId, string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            return;

        WagonState wagon = CreateOrGetWagon(wagonId, "UnknownWagon", "Unknown");
        wagon.Contents.Add(itemName);
    }

    public bool RemoveContent(string wagonId, string itemName)
    {
        if (!_wagons.TryGetValue(wagonId, out WagonState wagon))
            return false;

        return wagon.Contents.Remove(itemName);
    }

    public void ClearContents(string wagonId)
    {
        if (!_wagons.TryGetValue(wagonId, out WagonState wagon))
            return;

        wagon.Contents.Clear();
    }

    public WagonSaveData CreateSaveData()
    {
        WagonSaveData data = new WagonSaveData();

        foreach (WagonState wagon in _wagons.Values)
        {
            data.Wagons.Add(new WagonData
            {
                WagonId = wagon.WagonId,
                ModelName = wagon.ModelName,
                OwnerName = wagon.OwnerName,
                IsOwned = wagon.IsOwned,
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

    public void LoadFromSave(WagonSaveData data)
    {
        _wagons.Clear();

        if (data == null || data.Wagons == null)
            return;

        foreach (WagonData wagonData in data.Wagons)
        {
            if (wagonData == null)
                continue;

            WagonState wagon = new WagonState
            {
                WagonId = string.IsNullOrWhiteSpace(wagonData.WagonId) ? Guid.NewGuid().ToString("N") : wagonData.WagonId,
                ModelName = string.IsNullOrWhiteSpace(wagonData.ModelName) ? "UnknownWagon" : wagonData.ModelName,
                OwnerName = string.IsNullOrWhiteSpace(wagonData.OwnerName) ? "Unknown" : wagonData.OwnerName,
                IsOwned = wagonData.IsOwned,
                IsPersistent = wagonData.IsPersistent,
                IsHookedToHorse = wagonData.IsHookedToHorse,
                X = wagonData.X,
                Y = wagonData.Y,
                Z = wagonData.Z,
                Contents = wagonData.Contents != null ? new List<string>(wagonData.Contents) : new List<string>()
            };

            _wagons[wagon.WagonId] = wagon;
        }
    }
}

public class WagonState
{
    public string WagonId { get; set; }
    public string ModelName { get; set; }
    public string OwnerName { get; set; }
    public bool IsOwned { get; set; }
    public bool IsPersistent { get; set; }
    public bool IsHookedToHorse { get; set; }
    public List<string> Contents { get; set; } = new List<string>();
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public enum WagonType
{
    Standard,
    Cargo,
    Supply,
    Cannon,
    Maxim
}

public class Wagon
{
    public WagonState State { get; private set; }

    public Wagon(WagonState state)
    {
        State = state;
    }
}