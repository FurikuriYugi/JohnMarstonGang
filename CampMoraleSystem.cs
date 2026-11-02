using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampMoraleSystem
{
    private readonly string _savePath;
    private readonly List<CampMoraleRecord> _records = new List<CampMoraleRecord>();

    public int BaseMorale { get; set; } = 50;
    public int MinMorale { get; set; } = 0;
    public int MaxMorale { get; set; } = 100;

    public IReadOnlyList<CampMoraleRecord> Records => _records;

    public CampMoraleSystem(string saveFileName = "CampMoraleSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public void AddRecord(string reason, int moraleDelta, string source = null)
    {
        if (string.IsNullOrWhiteSpace(reason))
            reason = "Unknown";

        _records.Add(new CampMoraleRecord
        {
            RecordId = Guid.NewGuid().ToString("N"),
            Reason = reason,
            MoraleDelta = moraleDelta,
            Source = source,
            TimestampUtc = DateTime.UtcNow
        });

        Save();
    }

    public bool RemoveRecord(string recordId)
    {
        if (string.IsNullOrWhiteSpace(recordId))
            return false;

        bool removed = _records.RemoveAll(x => x.RecordId == recordId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public void ClearRecords()
    {
        _records.Clear();
        Save();
    }

    public int GetCurrentMorale()
    {
        int morale = BaseMorale + _records.Sum(x => x.MoraleDelta);
        if (morale < MinMorale)
            morale = MinMorale;
        if (morale > MaxMorale)
            morale = MaxMorale;

        return morale;
    }

    public string GetMoraleState()
    {
        int morale = GetCurrentMorale();

        if (morale >= 80)
            return "Excellent";
        if (morale >= 60)
            return "Good";
        if (morale >= 40)
            return "Neutral";
        if (morale >= 20)
            return "Low";

        return "Critical";
    }

    public IEnumerable<CampMoraleRecord> GetRecentRecords(int count)
    {
        if (count <= 0)
            return Enumerable.Empty<CampMoraleRecord>();

        return _records.OrderByDescending(x => x.TimestampUtc).Take(count);
    }

    public IEnumerable<CampMoraleRecord> GetRecordsBySource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            return _records;

        return _records.Where(x => string.Equals(x.Source, source, StringComparison.OrdinalIgnoreCase));
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_records, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampMoraleRecord>>(json);

            _records.Clear();
            if (loaded != null)
                _records.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.RecordId)));
        }
        catch
        {
            _records.Clear();
        }
    }
}

public class CampMoraleRecord
{
    public string RecordId { get; set; }
    public string Reason { get; set; }
    public int MoraleDelta { get; set; }
    public string Source { get; set; }
    public DateTime TimestampUtc { get; set; }
}