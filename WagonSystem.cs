// WagonSystem.cs
using Newtonsoft.Json;
using RDR2;
using RDR2.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class SavedWagon
{
    public string ModelName { get; set; }
    public Vector3 Position { get; set; }
    public float Heading { get; set; }
    public int Handle { get; set; }
    public List<string> Inventory { get; set; } = new List<string>();
}

public class WagonSystem
{
    private readonly Dictionary<string, SavedWagon> _wagons = new Dictionary<string, SavedWagon>();

    public void RegisterOwnedWagon(string wagonId, Vehicle vehicle)
    {
        if (vehicle == null || !vehicle.Exists()) return;

        _wagons[wagonId] = new SavedWagon
        {
            Handle = vehicle.Handle,
            ModelName = vehicle.Model.Hash.ToString(),
            Position = vehicle.Position,
            Heading = vehicle.Heading,
            Inventory = new List<string>()
        };
        Save();
    }

    public void AddItem(string wagonId, string item)
    {
        if (!_wagons.TryGetValue(wagonId, out SavedWagon wagon))
            return;

        wagon.Inventory.Add(item);
        Save();
    }

    public bool RemoveItem(string wagonId, string item)
    {
        if (!_wagons.TryGetValue(wagonId, out SavedWagon wagon))
            return false;

        bool removed = wagon.Inventory.Remove(item);
        if (removed) Save();
        return removed;
    }

    public SavedWagon GetWagon(string wagonId)
    {
        _wagons.TryGetValue(wagonId, out SavedWagon wagon);
        return wagon;
    }

    public void RespawnNearPlayer(string wagonId, Model model)
    {
        if (!_wagons.TryGetValue(wagonId, out SavedWagon wagon))
            return;

        Ped player = Game.Player.Character;
        if (player == null || !player.Exists())
            return;

        if (!model.IsLoaded)
            model.Request(500);

        if (!model.IsLoaded)
            return;

        Vector3 spawnPos = player.Position + player.ForwardVector * 6f;
        Vehicle spawned = World.CreateVehicle(model, spawnPos, player.Heading);
        if (spawned != null && spawned.Exists())
        {
            wagon.Handle = spawned.Handle;
            wagon.Position = spawned.Position;
            wagon.Heading = spawned.Heading;
            Save();
        }
    }

    public void Save()
    {
        try
        {
            string path = "scripts\\JohnsGangWagons.json";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JsonConvert.SerializeObject(_wagons, Formatting.Indented));
        }
        catch { }
    }

    public void Load()
    {
        try
        {
            string path = "scripts\\JohnsGangWagons.json";
            if (!File.Exists(path)) return;
            var loaded = JsonConvert.DeserializeObject<Dictionary<string, SavedWagon>>(File.ReadAllText(path));
            if (loaded != null)
            {
                _wagons.Clear();
                foreach (var kv in loaded)
                    _wagons[kv.Key] = kv.Value;
            }
        }
        catch { }
    }
}