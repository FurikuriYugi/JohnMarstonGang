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

    public bool HasWagon(string wagonId)
    {
        return !string.IsNullOrWhiteSpace(wagonId) && _wagons.ContainsKey(wagonId);
    }

    public WagonRecord GetWagon(string wagonId)
    {
        if (string.IsNullOrWhiteSpace(wagonId))
            return null;

        _wagons.TryGetValue(wagonId, out WagonRecord wagon);
        return wagon;
    }

    public List<WagonRecord> GetAllWagons()
    {
        return new List<WagonRecord>(_wagons.Values);
    }

    public void LoadWagons(List<WagonRecord> wagons)
    {
        _wagons.Clear();

        if (wagons == null)
            return;

        foreach (WagonRecord wagon in wagons)
        {
            if (wagon != null && !string.IsNullOrWhiteSpace(wagon.WagonId))
                _wagons[wagon.WagonId] = wagon;
        }
    }

    public void AddStoredItem(string wagonId, string item)
    {
        WagonRecord wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(item))
            return;

        if (wagon.StoredItems == null)
            wagon.StoredItems = new List<string>();

        wagon.StoredItems.Add(item);
    }

    public void AttachHorse(string wagonId, string horseId)
    {
        WagonRecord wagon = GetWagon(wagonId);
        if (wagon == null || string.IsNullOrWhiteSpace(horseId))
            return;

        if (wagon.AttachedHorses == null)
            wagon.AttachedHorses = new List<string>();

        if (!wagon.AttachedHorses.Contains(horseId))
            wagon.AttachedHorses.Add(horseId);
    }

    public void DetachHorse(string wagonId, string horseId)
    {
        WagonRecord wagon = GetWagon(wagonId);
        if (wagon == null || wagon.AttachedHorses == null || string.IsNullOrWhiteSpace(horseId))
            return;

        wagon.AttachedHorses.Remove(horseId);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllWagons(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _wagons.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WagonRecord> wagons = JsonConvert.DeserializeObject<List<WagonRecord>>(json);
        LoadWagons(wagons);
    }
}

public class WagonRecord
{
    public string WagonId { get; set; }
    public string OwnerId { get; set; }
    public bool IsOwned { get; set; }
    public bool IsHitched { get; set; }
    public List<string> StoredItems { get; set; } = new List<string>();
    public List<string> AttachedHorses { get; set; } = new List<string>();
}