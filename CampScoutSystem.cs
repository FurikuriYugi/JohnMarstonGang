using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampScoutSystem
{
    private readonly string _savePath;
    private readonly List<ScoutReportRecord> _reports = new List<ScoutReportRecord>();

    public int MaxReports { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<ScoutReportRecord> Reports => _reports;

    public CampScoutSystem(string saveFileName = "CampScoutSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddReport(string area, string threatLevel, string notes, string scoutName = null)
    {
        if (string.IsNullOrWhiteSpace(area) || string.IsNullOrWhiteSpace(notes))
            return false;

        if (_reports.Count >= MaxReports)
            _reports.RemoveAt(0);

        _reports.Add(new ScoutReportRecord
        {
            ReportId = Guid.NewGuid().ToString("N"),
            Area = area,
            ThreatLevel = string.IsNullOrWhiteSpace(threatLevel) ? "Unknown" : threatLevel,
            Notes = notes,
            ScoutName = scoutName,
            IsRead = false,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool MarkRead(string reportId)
    {
        var report = GetReport(reportId);
        if (report == null)
            return false;

        report.IsRead = true;
        report.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveReport(string reportId)
    {
        if (string.IsNullOrWhiteSpace(reportId))
            return false;

        bool removed = _reports.RemoveAll(x => x.ReportId == reportId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public ScoutReportRecord GetReport(string reportId)
    {
        if (string.IsNullOrWhiteSpace(reportId))
            return null;

        return _reports.FirstOrDefault(x => x.ReportId == reportId);
    }

    public IEnumerable<ScoutReportRecord> GetUnreadReports()
    {
        return _reports.Where(x => !x.IsRead).OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<ScoutReportRecord> GetReportsForArea(string area)
    {
        if (string.IsNullOrWhiteSpace(area))
            return _reports.OrderByDescending(x => x.CreatedUtc);

        return _reports.Where(x => string.Equals(x.Area, area, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public int GetUnreadCount()
    {
        return _reports.Count(x => !x.IsRead);
    }

    public string GetAreaRiskStatus()
    {
        int unread = GetUnreadCount();

        if (unread >= 25)
            return "Immediate Danger";
        if (unread >= 12)
            return "High Risk";
        if (unread >= 5)
            return "Watchful";
        if (unread >= 1)
            return "Minor Concern";

        return "Quiet";
    }

    public void Clear()
    {
        _reports.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_reports, options));
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
            var loaded = JsonSerializer.Deserialize<List<ScoutReportRecord>>(json);

            _reports.Clear();
            if (loaded != null)
                _reports.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.ReportId)));
        }
        catch
        {
            _reports.Clear();
        }
    }
}

public class ScoutReportRecord
{
    public string ReportId { get; set; }
    public string Area { get; set; }
    public string ThreatLevel { get; set; }
    public string Notes { get; set; }
    public string ScoutName { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}