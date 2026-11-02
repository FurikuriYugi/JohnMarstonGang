using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyLedgerSystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyLedgerRecord> _entries = new List<CampSupplyLedgerRecord>();

    public int MaxEntries { get; set; } = 500;
    public IReadOnlyList<CampSupplyLedgerRecord> Entries => _entries;

    public CampSupplyLedgerSystem(string saveFileName = "CampSupplyLedgerSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddEntry(string title, string category, string message, string source, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (_entries.Count >= MaxEntries)
            _entries.RemoveAt(0);

        _entries.Add(new CampSupplyLedgerRecord
        {
            EntryId = Guid.NewGuid().ToString("N"),
            Title = title,
            Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
            Message = string.IsNullOrWhiteSpace(message) ? title : message,
            Source = source,
            Amount = amount,
            CreatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool RemoveEntry(string entryId)
    {
        if (string.IsNullOrWhiteSpace(entryId))
            return false;

        bool removed = _entries.RemoveAll(x => x.EntryId == entryId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyLedgerRecord GetEntry(string entryId)
    {
        if (string.IsNullOrWhiteSpace(entryId))
            return null;

        return _entries.FirstOrDefault(x => x.EntryId == entryId);
    }

    public IEnumerable<CampSupplyLedgerRecord> GetEntriesByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _entries.OrderByDescending(x => x.CreatedUtc);

        return _entries.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampSupplyLedgerRecord> GetEntriesBySource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            return _entries.OrderByDescending(x => x.CreatedUtc);

        return _entries.Where(x => string.Equals(x.Source, source, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public decimal GetTotalAmount()
    {
        return _entries.Sum(x => x.Amount);
    }

    public decimal GetCategoryTotal(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return 0m;

        return _entries.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase))
            .Sum(x => x.Amount);
    }

    public string GetLedgerStatus()
    {
        int count = _entries.Count;

        if (count >= 400)
            return "Full";
        if (count >= 250)
            return "Busy";
        if (count >= 100)
            return "Steady";
        if (count >= 25)
            return "Light";

        return "Sparse";
    }

    public void Clear()
    {
        _entries.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_entries, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampSupplyLedgerRecord>>(json);

            _entries.Clear();
            if (loaded != null)
                _entries.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.EntryId)));
        }
        catch
        {
            _entries.Clear();
        }
    }
}

public class CampSupplyLedgerRecord
{
    public string EntryId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedUtc { get; set; }
}