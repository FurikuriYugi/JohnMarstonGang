using System.Collections.Generic;
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