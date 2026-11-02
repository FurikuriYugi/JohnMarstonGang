using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampDemandSystem
{
    private readonly string _savePath;
    private readonly List<CampDemandRecord> _demands = new List<CampDemandRecord>();

    public int MaxDemands { get; set; } = 128;
    public IReadOnlyList<CampDemandRecord> Demands => _demands;

    public CampDemandSystem(string saveFileName = "CampDemandSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddDemand(string title, string category, string request, int priority = 5, string assignedTo = null, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (priority < 0)
            priority = 0;
        if (priority > 10)
            priority = 10;

        if (_demands.Count >= MaxDemands)
            _demands.RemoveAt(0);

        _demands.Add(new CampDemandRecord
        {
            DemandId = Guid.NewGuid().ToString("N"),
            Title = title,
            Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
            Request = string.IsNullOrWhiteSpace(request) ? title : request,
            Priority = priority,
            AssignedTo = assignedTo,
            Notes = notes,
            IsFulfilled = false,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool FulfillDemand(string demandId, string notes = null)
    {
        var demand = GetDemand(demandId);
        if (demand == null)
            return false;

        demand.IsFulfilled = true;
        demand.Notes = string.IsNullOrWhiteSpace(notes) ? demand.Notes : notes;
        demand.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool AssignDemand(string demandId, string assignedTo)
    {
        var demand = GetDemand(demandId);
        if (demand == null)
            return false;

        demand.AssignedTo = assignedTo;
        demand.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveDemand(string demandId)
    {
        if (string.IsNullOrWhiteSpace(demandId))
            return false;

        bool removed = _demands.RemoveAll(x => x.DemandId == demandId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampDemandRecord GetDemand(string demandId)
    {
        if (string.IsNullOrWhiteSpace(demandId))
            return null;

        return _demands.FirstOrDefault(x => x.DemandId == demandId);
    }

    public IEnumerable<CampDemandRecord> GetOpenDemands()
    {
        return _demands.Where(x => !x.IsFulfilled)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.CreatedUtc);
    }

    public IEnumerable<CampDemandRecord> GetFulfilledDemands()
    {
        return _demands.Where(x => x.IsFulfilled)
            .OrderByDescending(x => x.UpdatedUtc);
    }

    public IEnumerable<CampDemandRecord> GetDemandsByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _demands.OrderByDescending(x => x.CreatedUtc);

        return _demands.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.Priority)
            .ThenByDescending(x => x.CreatedUtc);
    }

    public int GetDemandPressure()
    {
        if (_demands.Count == 0)
            return 0;

        int pressure = 0;
        foreach (var demand in _demands)
        {
            pressure += demand.IsFulfilled ? 0 : (demand.Priority + 1);
        }

        return pressure;
    }

    public string GetDemandStatus()
    {
        int pressure = GetDemandPressure();

        if (pressure >= 60)
            return "Overloaded";
        if (pressure >= 30)
            return "Busy";
        if (pressure >= 15)
            return "Steady";
        if (pressure >= 5)
            return "Light";

        return "Idle";
    }

    public void Clear()
    {
        _demands.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_demands, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampDemandRecord>>(json);

            _demands.Clear();
            if (loaded != null)
                _demands.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.DemandId)));
        }
        catch
        {
            _demands.Clear();
        }
    }
}

public class CampDemandRecord
{
    public string DemandId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Request { get; set; }
    public int Priority { get; set; }
    public string AssignedTo { get; set; }
    public string Notes { get; set; }
    public bool IsFulfilled { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}