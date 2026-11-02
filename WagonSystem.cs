// WagonSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly List<Wagon> _wagons = new List<Wagon>();

    public IReadOnlyList<Wagon> Wagons => _wagons;

    public Wagon CreateWagon(string name, string ownerId, WagonType type)
    {
        var wagon = new Wagon
        {
            WagonId = Guid.NewGuid().ToString("N"),
            Name = name,
            OwnerId = ownerId,
            Type = type,
            IsOwned = true,
            IsHitched = false,
            IsPersistent = true
        };

        _wagons.Add(wagon);
        return wagon;
    }

    public Wagon GetWagon(string wagonId)
    {
        return _wagons.FirstOrDefault(x => x.WagonId == wagonId);
    }

    public bool HitchWagon(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;

        wagon.IsHitched = true;
        return true;
    }

    public bool UnhitchWagon(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;

        wagon.IsHitched = false;
        return true;
    }

    public bool StoreItem(string wagonId, string itemName, int amount)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;
        if (string.IsNullOrEmpty(itemName)) return false;
        if (amount <= 0) return false;

        var stack = wagon.Cargo.FirstOrDefault(x => x.ItemName == itemName);
        if (stack == null)
        {
            stack = new WagonCargoItem
            {
                ItemName = itemName,
                Amount = 0
            };
            wagon.Cargo.Add(stack);
        }

        stack.Amount += amount;
        return true;
    }

    public bool RemoveItem(string wagonId, string itemName, int amount)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;
        if (string.IsNullOrEmpty(itemName)) return false;
        if (amount <= 0) return false;

        var stack = wagon.Cargo.FirstOrDefault(x => x.ItemName == itemName);
        if (stack == null) return false;
        if (stack.Amount < amount) return false;

        stack.Amount -= amount;
        if (stack.Amount <= 0)
            wagon.Cargo.Remove(stack);

        return true;
    }

    public bool CanCarryWagonType(WagonType type)
    {
        return type == WagonType.Cargo || type == WagonType.Combat || type == WagonType.Artillery;
    }

    public bool IsOwnedBy(string wagonId, string ownerId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return false;
        return wagon.OwnerId == ownerId;
    }

    public void AttachHorse(string wagonId, string horseId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return;

        wagon.HorseId = horseId;
        wagon.IsHitched = true;
    }

    public void DetachHorse(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return;

        wagon.HorseId = null;
        wagon.IsHitched = false;
    }

    public void MakePersistent(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return;

        wagon.IsPersistent = true;
    }

    public void ClearDestroyedWagons()
    {
        _wagons.RemoveAll(x => x.IsDestroyed);
    }

    public void MarkDestroyed(string wagonId)
    {
        var wagon = GetWagon(wagonId);
        if (wagon == null) return;

        wagon.IsDestroyed = true;
    }

    public void RestoreFromPersistence(IEnumerable<WagonData> wagons)
    {
        _wagons.Clear();

        if (wagons == null)
            return;

        foreach (var data in wagons)
        {
            _wagons.Add(new Wagon
            {
                WagonId = data.WagonId,
                Name = data.Name,
                OwnerId = data.OwnerId,
                HorseId = data.HorseId,
                Type = (WagonType)data.Type,
                IsOwned = data.IsOwned,
                IsHitched = data.IsHitched,
                IsPersistent = data.IsPersistent,
                IsDestroyed = data.IsDestroyed,
                Cargo = data.Cargo != null
                    ? data.Cargo.Select(x => new WagonCargoItem
                    {
                        ItemName = x.ItemName,
                        Amount = x.Amount
                    }).ToList()
                    : new List<WagonCargoItem>()
            });
        }
    }

    public List<WagonData> BuildPersistence()
    {
        return _wagons.Select(w => new WagonData
        {
            WagonId = w.WagonId,
            Name = w.Name,
            OwnerId = w.OwnerId,
            HorseId = w.HorseId,
            Type = (int)w.Type,
            IsOwned = w.IsOwned,
            IsHitched = w.IsHitched,
            IsPersistent = w.IsPersistent,
            IsDestroyed = w.IsDestroyed,
            Cargo = w.Cargo.Select(c => new WagonCargoData
            {
                ItemName = c.ItemName,
                Amount = c.Amount
            }).ToList()
        }).ToList();
    }
}

public class Wagon
{
    public string WagonId { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public string HorseId { get; set; }
    public WagonType Type { get; set; }
    public bool IsOwned { get; set; }
    public bool IsHitched { get; set; }
    public bool IsPersistent { get; set; }
    public bool IsDestroyed { get; set; }
    public List<WagonCargoItem> Cargo { get; set; } = new List<WagonCargoItem>();
}

public class WagonCargoItem
{
    public string ItemName { get; set; }
    public int Amount { get; set; }
}

public enum WagonType
{
    Cargo = 0,
    Combat = 1,
    Artillery = 2
}