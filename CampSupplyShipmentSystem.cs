using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyShipmentSystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyShipmentRecord> _shipments = new List<CampSupplyShipmentRecord>();

    public int MaxShipments { get; set; } = 256;
    public IReadOnlyList<CampSupplyShipmentRecord> Shipments => _shipments;

    public CampSupplyShipmentSystem(string saveFileName = "CampSupplyShipmentSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddShipment(string origin, string destination, string cargo, int quantity, string assignedTo = null)
    {
        if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            return false;

        if (quantity <= 0)
            quantity = 1;

        if (_shipments.Count >= MaxShipments)
            _shipments.RemoveAt(0);

        _shipments.Add(new CampSupplyShipmentRecord
        {
            ShipmentId = Guid.NewGuid().ToString("N"),
            Origin = origin,
            Destination = destination,
            Cargo = string.IsNullOrWhiteSpace(cargo) ? "General" : cargo,
            Quantity = quantity,
            AssignedTo = assignedTo,
            IsDelivered = false,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool MarkDelivered(string shipmentId)
    {
        var shipment = GetShipment(shipmentId);
        if (shipment == null)
            return false;

        shipment.IsDelivered = true;
        shipment.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveShipment(string shipmentId)
    {
        if (string.IsNullOrWhiteSpace(shipmentId))
            return false;

        bool removed = _shipments.RemoveAll(x => x.ShipmentId == shipmentId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyShipmentRecord GetShipment(string shipmentId)
    {
        if (string.IsNullOrWhiteSpace(shipmentId))
            return null;

        return _shipments.FirstOrDefault(x => x.ShipmentId == shipmentId);
    }

    public IEnumerable<CampSupplyShipmentRecord> GetPendingShipments()
    {
        return _shipments.Where(x => !x.IsDelivered).OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampSupplyShipmentRecord> GetDeliveredShipments()
    {
        return _shipments.Where(x => x.IsDelivered).OrderByDescending(x => x.CreatedUtc);
    }

    public int GetPendingCount()
    {
        return _shipments.Count(x => !x.IsDelivered);
    }

    public string GetShipmentStatus()
    {
        int pending = GetPendingCount();

        if (pending >= 25)
            return "Backlogged";
        if (pending >= 12)
            return "Busy";
        if (pending >= 5)
            return "Flowing";
        if (pending >= 1)
            return "Light";

        return "Idle";
    }

    public void Clear()
    {
        _shipments.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_shipments, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<CampSupplyShipmentRecord>>(json);

            _shipments.Clear();
            if (loaded != null)
                _shipments.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.ShipmentId)));
        }
        catch
        {
            _shipments.Clear();
        }
    }
}

public class CampSupplyShipmentRecord
{
    public string ShipmentId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Cargo { get; set; }
    public int Quantity { get; set; }
    public string AssignedTo { get; set; }
    public bool IsDelivered { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}