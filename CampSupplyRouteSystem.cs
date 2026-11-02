using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyRouteSystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyRouteRecord> _routes = new List<CampSupplyRouteRecord>();

    public int MaxRoutes { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampSupplyRouteRecord> Routes => _routes;

    public CampSupplyRouteSystem(string saveFileName = "CampSupplyRouteSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddRoute(string origin, string destination, string cargo, string assignedTo = null)
    {
        if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            return false;

        if (_routes.Count >= MaxRoutes)
            _routes.RemoveAt(0);

        _routes.Add(new CampSupplyRouteRecord
        {
            RouteId = Guid.NewGuid().ToString("N"),
            Origin = origin,
            Destination = destination,
            Cargo = string.IsNullOrWhiteSpace(cargo) ? "General" : cargo,
            AssignedTo = assignedTo,
            IsCompleted = false,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool CompleteRoute(string routeId)
    {
        var route = GetRoute(routeId);
        if (route == null)
            return false;

        route.IsCompleted = true;
        route.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveRoute(string routeId)
    {
        if (string.IsNullOrWhiteSpace(routeId))
            return false;

        bool removed = _routes.RemoveAll(x => x.RouteId == routeId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyRouteRecord GetRoute(string routeId)
    {
        if (string.IsNullOrWhiteSpace(routeId))
            return null;

        return _routes.FirstOrDefault(x => x.RouteId == routeId);
    }

    public IEnumerable<CampSupplyRouteRecord> GetOpenRoutes()
    {
        return _routes.Where(x => !x.IsCompleted).OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampSupplyRouteRecord> GetRoutesForDestination(string destination)
    {
        if (string.IsNullOrWhiteSpace(destination))
            return _routes.OrderByDescending(x => x.CreatedUtc);

        return _routes.Where(x => string.Equals(x.Destination, destination, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public int GetOpenCount()
    {
        return _routes.Count(x => !x.IsCompleted);
    }

    public string GetRouteStatus()
    {
        int open = GetOpenCount();

        if (open >= 30)
            return "Overloaded";
        if (open >= 15)
            return "Busy";
        if (open >= 5)
            return "Steady";
        if (open >= 1)
            return "Light";

        return "Idle";
    }

    public void Clear()
    {
        _routes.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_routes, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSupplyRouteRecord>>(json);

            _routes.Clear();
            if (loaded != null)
                _routes.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.RouteId)));
        }
        catch
        {
            _routes.Clear();
        }
    }
}

public class CampSupplyRouteRecord
{
    public string RouteId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Cargo { get; set; }
    public string AssignedTo { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}