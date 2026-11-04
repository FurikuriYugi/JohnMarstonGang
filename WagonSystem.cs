using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class WagonSystem
{
    private readonly List<WagonData> _wagons = new List<WagonData>();

    public List<WagonData> GetWagons()
    {
        return new List<WagonData>(_wagons);
    }

    public WagonData GetWagon(string wagonId)
    {
        if (string.IsNullOrWhiteSpace(wagonId))
            return null;

        return _wagons.Find(x => x != null && x.WagonId == wagonId);
    }

    public void RegisterWagon(WagonData wagon)
    {
        if (wagon == null || string.IsNullOrWhiteSpace(wagon.WagonId))
            return;

        _wagons.RemoveAll(x => x != null && x.WagonId == wagon.WagonId);
        _wagons.Add(wagon);
    }

    public WagonData CreateWagon(string ownerId, string displayName, bool owned)
    {
        WagonData wagon = new WagonData
        {
            WagonId = Guid.NewGuid().ToString("N"),
            OwnerId = ownerId,
            DisplayName = displayName,
            IsOwned = owned,
            IsStoredNearPlayer = true,
            IsHitchable = true
        };

        RegisterWagon(wagon);
        return wagon;
    }

    public void RemoveWagon(string wagonId)
    {
        _wagons.RemoveAll(x => x != null && x.WagonId == wagonId);
    }

    public void StoreItem(string wagonId, string item)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(item))
            return;

        if (wagon.StoredItems == null)
            wagon.StoredItems = new List<string>();

        wagon.StoredItems.Add(item);
    }

    public bool RemoveItem(string wagonId, string item)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(item) || wagon.StoredItems == null)
            return false;

        return wagon.StoredItems.Remove(item);
    }

    public void AttachHorse(string wagonId, string horseId)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(horseId))
            return;

        if (wagon.AttachedHorses == null)
            wagon.AttachedHorses = new List<string>();

        if (!wagon.AttachedHorses.Contains(horseId))
            wagon.AttachedHorses.Add(horseId);
    }

    public void DetachHorse(string wagonId, string horseId)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(horseId) || wagon.AttachedHorses == null)
            return;

        wagon.AttachedHorses.Remove(horseId);
    }

    public void AttachWeapon(string wagonId, string weaponId)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(weaponId))
            return;

        if (wagon.AttachedWeapons == null)
            wagon.AttachedWeapons = new List<string>();

        if (!wagon.AttachedWeapons.Contains(weaponId))
            wagon.AttachedWeapons.Add(weaponId);
    }

    public void SetNearPlayer(string wagonId, bool isNearPlayer)
    {
        WagonData wagon = GetWagon(wagonId);
        if (wagon == null)
            return;

        wagon.IsStoredNearPlayer = isNearPlayer;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_wagons, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _wagons.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WagonData> wagons = JsonConvert.DeserializeObject<List<WagonData>>(json);
        if (wagons != null)
            _wagons.AddRange(wagons);
    }
}