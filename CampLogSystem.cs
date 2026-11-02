using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampLogSystem
{
    private readonly string _savePath;
    private readonly List<CampLogRecord> _logs = new List<CampLogRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxEntries { get; set; } = 500;

    public IReadOnlyList<CampLogRecord> Logs => _logs;

    public CampLogSystem(string saveFileName = "CampLogSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public void AddEntry(string title, string message, string category = null, string source = null)
    {
        if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(message))
            return;

        _logs.Add(new CampLogRecord
        {
            LogId = Guid.NewGuid().ToString("N"),
            Title = title ?? string.Empty,
            Message = message ?? string.Empty,
            Category = category,
            Source = source,
            TimestampUtc = DateTime.UtcNow
        });

        while (_logs.Count > MaxEntries)
            _logs.RemoveAt(0);

        Save();
    }

    public bool RemoveEntry(string logId)
    {
        if (string.IsNullOrWhiteSpace(logId))
            return false;

        bool removed = _logs.RemoveAll(x => x.LogId == logId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampLogRecord GetEntry(string logId)
    {
        if (string.IsNullOrWhiteSpace(logId))
            return null;

        return _logs.FirstOrDefault(x => x.LogId == logId);
    }

    public IEnumerable<CampLogRecord> GetRecentEntries(int count)
    {
        if (count <= 0)
            return Enumerable.Empty<CampLogRecord>();

        return _logs.OrderByDescending(x => x.TimestampUtc).Take(count);
    }

    public IEnumerable<CampLogRecord> GetEntriesByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _logs;

        return _logs.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public void Clear()
    {
        _logs.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_logs, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampLogRecord>>(json);

            _logs.Clear();
            if (loaded != null)
                _logs.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.LogId)));
        }
        catch
        {
            _logs.Clear();
        }
    }
}

public class CampLogRecord
{
    public string LogId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Category { get; set; }
    public string Source { get; set; }
    public DateTime TimestampUtc { get; set; }
}