using System;
using System.Collections.Generic;
using System.Linq;

public class WagonSystem
{
    private readonly List<WagonRecord> _wagons = new List<WagonRecord>();

    public List<WagonRecord> Wagons => _wagons;

    public WagonRecord GetOrCreateWagon(string wagonId)
    {
        WagonRecord wagon = _wagons.FirstOrDefault(w => w.WagonId == wagonId);
        if (wagon != null)
            return wagon;

        wagon = new WagonRecord
        {
            WagonId = wagonId,
            OwnerId = null,
            IsOwned = false,
            IsHitched = false,
            IsPersistent = true,
            Cargo = new List<string>(),
            HitchHorseIds = new List<string>()
        };

        _wagons.Add(wagon);
        return wagon;
    }

    public WagonRecord CreateWagon(string wagonId, string ownerId, bool isPersistent = true)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.OwnerId = ownerId;
        wagon.IsOwned = true;
        wagon.IsPersistent = isPersistent;
        return wagon;
    }

    public void TransferOwnership(string wagonId, string newOwnerId)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.OwnerId = newOwnerId;
        wagon.IsOwned = true;
    }

    public void SetHitched(string wagonId, bool hitched)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.IsHitched = hitched;
    }

    public void AddCargo(string wagonId, string itemName)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        if (!wagon.Cargo.Contains(itemName))
            wagon.Cargo.Add(itemName);
    }

    public void RemoveCargo(string wagonId, string itemName)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.Cargo.Remove(itemName);
    }

    public void ClearCargo(string wagonId)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.Cargo.Clear();
    }

    public void HitchHorse(string wagonId, string horseId)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        if (!wagon.HitchHorseIds.Contains(horseId))
            wagon.HitchHorseIds.Add(horseId);
    }

    public void UnhitchHorse(string wagonId, string horseId)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        wagon.HitchHorseIds.Remove(horseId);
    }

    public bool HasCargo(string wagonId, string itemName)
    {
        WagonRecord wagon = GetOrCreateWagon(wagonId);
        return wagon.Cargo.Contains(itemName);
    }

    public List<WagonRecord> CreateSaveData()
    {
        return _wagons.Select(w => new WagonRecord
        {
            WagonId = w.WagonId,
            OwnerId = w.OwnerId,
            IsOwned = w.IsOwned,
            IsHitched = w.IsHitched,
            IsPersistent = w.IsPersistent,
            Cargo = new List<string>(w.Cargo ?? new List<string>()),
            HitchHorseIds = new List<string>(w.HitchHorseIds ?? new List<string>())
        }).ToList();
    }

    public void LoadFromSave(WagonSaveData data)
    {
        _wagons.Clear();

        if (data == null || data.Wagons == null)
            return;

        foreach (WagonRecord wagon in data.Wagons)
        {
            _wagons.Add(new WagonRecord
            {
                WagonId = wagon.WagonId,
                OwnerId = wagon.OwnerId,
                IsOwned = wagon.IsOwned,
                IsHitched = wagon.IsHitched,
                IsPersistent = wagon.IsPersistent,
                Cargo = wagon.Cargo ?? new List<string>(),
                HitchHorseIds = wagon.HitchHorseIds ?? new List<string>()
            });
        }
    }
}

public class WagonRecord
{
    public string WagonId { get; set; }
    public string OwnerId { get; set; }
    public bool IsOwned { get; set; }
    public bool IsHitched { get; set; }
    public bool IsPersistent { get; set; }
    public List<string> Cargo { get; set; }
    public List<string> HitchHorseIds { get; set; }
}