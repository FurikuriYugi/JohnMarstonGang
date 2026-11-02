using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampDispatchSystem
{
    private readonly string _savePath;
    private readonly List<CampDispatchRecord> _dispatches = new List<CampDispatchRecord>();

    public int MaxDispatches { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampDispatchRecord> Dispatches => _dispatches;

    public CampDispatchSystem(string saveFileName = "CampDispatchSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddDispatch(string title, string destination, string cargo, string assignedTo = null)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(destination))
            return false;

        if (_dispatches.Count >= MaxDispatches)
            _dispatches.RemoveAt(0);

        _dispatches.Add(new CampDispatchRecord
        {
            DispatchId = Guid.NewGuid().ToString("N"),
            Title = title,
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

    public bool CompleteDispatch(string dispatchId)
    {
        var dispatch = GetDispatch(dispatchId);
        if (dispatch == null)
            return false;

        dispatch.IsCompleted = true;
        dispatch.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveDispatch(string dispatchId)
    {
        if (string.IsNullOrWhiteSpace(dispatchId))
            return false;

        bool removed = _dispatches.RemoveAll(x => x.DispatchId == dispatchId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampDispatchRecord GetDispatch(string dispatchId)
    {
        if (string.IsNullOrWhiteSpace(dispatchId))
            return null;

        return _dispatches.FirstOrDefault(x => x.DispatchId == dispatchId);
    }

    public IEnumerable<CampDispatchRecord> GetOpenDispatches()
    {
        return _dispatches.Where(x => !x.IsCompleted).OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampDispatchRecord> GetDispatchesForDestination(string destination)
    {
        if (string.IsNullOrWhiteSpace(destination))
            return _dispatches.OrderByDescending(x => x.CreatedUtc);

        return _dispatches.Where(x => string.Equals(x.Destination, destination, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public int GetOpenCount()
    {
        return _dispatches.Count(x => !x.IsCompleted);
    }

    public string GetDispatchStatus()
    {
        int open = GetOpenCount();

        if (open >= 25)
            return "Backlogged";
        if (open >= 12)
            return "Busy";
        if (open >= 5)
            return "Flowing";
        if (open >= 1)
            return "Light";

        return "Idle";
    }

    public void Clear()
    {
        _dispatches.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_dispatches, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampDispatchRecord>>(json);

            _dispatches.Clear();
            if (loaded != null)
                _dispatches.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.DispatchId)));
        }
        catch
        {
            _dispatches.Clear();
        }
    }
}

public class CampDispatchRecord
{
    public string DispatchId { get; set; }
    public string Title { get; set; }
    public string Destination { get; set; }
    public string Cargo { get; set; }
    public string AssignedTo { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}