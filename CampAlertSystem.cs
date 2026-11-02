using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampAlertSystem
{
    private readonly string _savePath;
    private readonly List<CampAlertRecord> _alerts = new List<CampAlertRecord>();

    public int MaxAlerts { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampAlertRecord> Alerts => _alerts;

    public CampAlertSystem(string saveFileName = "CampAlertSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RaiseAlert(string title, string severity, string message, string source = null)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(message))
            return false;

        if (_alerts.Count >= MaxAlerts)
            _alerts.RemoveAt(0);

        _alerts.Add(new CampAlertRecord
        {
            AlertId = Guid.NewGuid().ToString("N"),
            Title = title,
            Severity = string.IsNullOrWhiteSpace(severity) ? "Normal" : severity,
            Message = message,
            Source = source,
            IsResolved = false,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool ResolveAlert(string alertId)
    {
        var alert = GetAlert(alertId);
        if (alert == null)
            return false;

        alert.IsResolved = true;
        alert.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateAlertMessage(string alertId, string message)
    {
        var alert = GetAlert(alertId);
        if (alert == null || string.IsNullOrWhiteSpace(message))
            return false;

        alert.Message = message;
        alert.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveAlert(string alertId)
    {
        if (string.IsNullOrWhiteSpace(alertId))
            return false;

        bool removed = _alerts.RemoveAll(x => x.AlertId == alertId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampAlertRecord GetAlert(string alertId)
    {
        if (string.IsNullOrWhiteSpace(alertId))
            return null;

        return _alerts.FirstOrDefault(x => x.AlertId == alertId);
    }

    public IEnumerable<CampAlertRecord> GetOpenAlerts()
    {
        return _alerts.Where(x => !x.IsResolved).OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampAlertRecord> GetAlertsBySeverity(string severity)
    {
        if (string.IsNullOrWhiteSpace(severity))
            return _alerts.OrderByDescending(x => x.CreatedUtc);

        return _alerts.Where(x => string.Equals(x.Severity, severity, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public int GetOpenCount()
    {
        return _alerts.Count(x => !x.IsResolved);
    }

    public string GetAlertStatus()
    {
        int open = GetOpenCount();

        if (open >= 20)
            return "Critical";
        if (open >= 10)
            return "Severe";
        if (open >= 4)
            return "Elevated";
        if (open >= 1)
            return "Notice";

        return "Clear";
    }

    public void Clear()
    {
        _alerts.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_alerts, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampAlertRecord>>(json);

            _alerts.Clear();
            if (loaded != null)
                _alerts.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.AlertId)));
        }
        catch
        {
            _alerts.Clear();
        }
    }
}

public class CampAlertRecord
{
    public string AlertId { get; set; }
    public string Title { get; set; }
    public string Severity { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public bool IsResolved { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}