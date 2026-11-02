using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSignalTowerSystem
{
    private readonly string _savePath;
    private readonly List<CampSignalTowerRecord> _signals = new List<CampSignalTowerRecord>();

    public int MaxSignals { get; set; } = 128;
    public IReadOnlyList<CampSignalTowerRecord> Signals => _signals;

    public CampSignalTowerSystem(string saveFileName = "CampSignalTowerSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddSignal(string title, string category, string details, string source = null, bool isAcknowledged = false)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (_signals.Count >= MaxSignals)
            _signals.RemoveAt(0);

        _signals.Add(new CampSignalTowerRecord
        {
            SignalId = Guid.NewGuid().ToString("N"),
            Title = title,
            Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
            Details = string.IsNullOrWhiteSpace(details) ? title : details,
            Source = source,
            IsAcknowledged = isAcknowledged,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool AcknowledgeSignal(string signalId, bool acknowledged = true)
    {
        var signal = GetSignal(signalId);
        if (signal == null)
            return false;

        signal.IsAcknowledged = acknowledged;
        signal.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveSignal(string signalId)
    {
        if (string.IsNullOrWhiteSpace(signalId))
            return false;

        bool removed = _signals.RemoveAll(x => x.SignalId == signalId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSignalTowerRecord GetSignal(string signalId)
    {
        if (string.IsNullOrWhiteSpace(signalId))
            return null;

        return _signals.FirstOrDefault(x => x.SignalId == signalId);
    }

    public IEnumerable<CampSignalTowerRecord> GetUnreadSignals()
    {
        return _signals.Where(x => !x.IsAcknowledged)
            .OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampSignalTowerRecord> GetSignalsByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _signals.OrderByDescending(x => x.CreatedUtc);

        return _signals.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public int GetUnacknowledgedCount()
    {
        return _signals.Count(x => !x.IsAcknowledged);
    }

    public string GetSignalStatus()
    {
        int count = GetUnacknowledgedCount();

        if (count >= 30)
            return "Overloaded";
        if (count >= 15)
            return "Active";
        if (count >= 5)
            return "Monitoring";
        if (count >= 1)
            return "Quietly Active";

        return "Idle";
    }

    public void Clear()
    {
        _signals.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_signals, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSignalTowerRecord>>(json);

            _signals.Clear();
            if (loaded != null)
                _signals.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.SignalId)));
        }
        catch
        {
            _signals.Clear();
        }
    }
}

public class CampSignalTowerRecord
{
    public string SignalId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Details { get; set; }
    public string Source { get; set; }
    public bool IsAcknowledged { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}