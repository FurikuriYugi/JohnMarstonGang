using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyAuditSystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyAuditRecord> _audits = new List<CampSupplyAuditRecord>();

    public int MaxAudits { get; set; } = 256;
    public IReadOnlyList<CampSupplyAuditRecord> Audits => _audits;

    public CampSupplyAuditSystem(string saveFileName = "CampSupplyAuditSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddAudit(string title, string detail, string source, bool passed)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (_audits.Count >= MaxAudits)
            _audits.RemoveAt(0);

        _audits.Add(new CampSupplyAuditRecord
        {
            AuditId = Guid.NewGuid().ToString("N"),
            Title = title,
            Detail = string.IsNullOrWhiteSpace(detail) ? title : detail,
            Source = source,
            Passed = passed,
            CreatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool RemoveAudit(string auditId)
    {
        if (string.IsNullOrWhiteSpace(auditId))
            return false;

        bool removed = _audits.RemoveAll(x => x.AuditId == auditId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyAuditRecord GetAudit(string auditId)
    {
        if (string.IsNullOrWhiteSpace(auditId))
            return null;

        return _audits.FirstOrDefault(x => x.AuditId == auditId);
    }

    public IEnumerable<CampSupplyAuditRecord> GetRecentAudits(int count)
    {
        if (count <= 0)
            count = 1;

        return _audits.OrderByDescending(x => x.CreatedUtc).Take(count);
    }

    public IEnumerable<CampSupplyAuditRecord> GetFailedAudits()
    {
        return _audits.Where(x => !x.Passed).OrderByDescending(x => x.CreatedUtc);
    }

    public int GetFailedCount()
    {
        return _audits.Count(x => !x.Passed);
    }

    public string GetAuditStatus()
    {
        int failed = GetFailedCount();

        if (failed >= 20)
            return "Critical";
        if (failed >= 10)
            return "Severe";
        if (failed >= 4)
            return "Elevated";
        if (failed >= 1)
            return "Notice";

        return "Clear";
    }

    public void Clear()
    {
        _audits.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_audits, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSupplyAuditRecord>>(json);

            _audits.Clear();
            if (loaded != null)
                _audits.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.AuditId)));
        }
        catch
        {
            _audits.Clear();
        }
    }
}

public class CampSupplyAuditRecord
{
    public string AuditId { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string Source { get; set; }
    public bool Passed { get; set; }
    public DateTime CreatedUtc { get; set; }
}