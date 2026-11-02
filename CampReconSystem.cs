using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampReconSystem
{
    private readonly string _savePath;
    private readonly List<CampReconReport> _reports = new List<CampReconReport>();

    public int MaxReports { get; set; } = 128;
    public IReadOnlyList<CampReconReport> Reports => _reports;

    public CampReconSystem(string saveFileName = "CampReconSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddReport(string area, int threatLevel, string notes, string scoutName = null, bool isRead = false)
    {
        if (string.IsNullOrWhiteSpace(area))
            return false;

        if (threatLevel < 0)
            threatLevel = 0;
        if (threatLevel > 100)
            threatLevel = 100;

        if (_reports.Count >= MaxReports)
            _reports.RemoveAt(0);

        _reports.Add(new CampReconReport
        {
            ReportId = Guid.NewGuid().ToString("N"),
            Area = area,
            ThreatLevel = threatLevel,
            Notes = notes,
            ScoutName = scoutName,
            IsRead = isRead,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool MarkRead(string reportId, bool isRead = true)
    {
        var report = GetReport(reportId);
        if (report == null)
            return false;

        report.IsRead = isRead;
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

    public CampReconReport GetReport(string reportId)
    {
        if (string.IsNullOrWhiteSpace(reportId))
            return null;

        return _reports.FirstOrDefault(x => x.ReportId == reportId);
    }

    public IEnumerable<CampReconReport> GetUnreadReports()
    {
        return _reports.Where(x => !x.IsRead)
            .OrderByDescending(x => x.ThreatLevel)
            .ThenByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampReconReport> GetReportsByArea(string area)
    {
        if (string.IsNullOrWhiteSpace(area))
            return _reports.OrderByDescending(x => x.CreatedUtc);

        return _reports.Where(x => string.Equals(x.Area, area, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.ThreatLevel)
            .ThenByDescending(x => x.CreatedUtc);
    }

    public int GetAverageThreatLevel()
    {
        if (_reports.Count == 0)
            return 0;

        return (int)Math.Round(_reports.Average(x => x.ThreatLevel));
    }

    public string GetReconStatus()
    {
        int count = _reports.Count;

        if (count >= 30)
            return "Overloaded";
        if (count >= 15)
            return "Active";
        if (count >= 5)
            return "Watchful";
        if (count >= 1)
            return "Minor Concern";

        return "Quiet";
    }

    public string GetAreaRiskLabel(int threatLevel)
    {
        if (threatLevel >= 25)
            return "Immediate Danger";
        if (threatLevel >= 12)
            return "High Risk";
        if (threatLevel >= 5)
            return "Watchful";
        if (threatLevel >= 1)
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
            var loaded = JsonSerializer.Deserialize<List<CampReconReport>>(json);

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

public class CampReconReport
{
    public string ReportId { get; set; }
    public string Area { get; set; }
    public int ThreatLevel { get; set; }
    public string Notes { get; set; }
    public string ScoutName { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}