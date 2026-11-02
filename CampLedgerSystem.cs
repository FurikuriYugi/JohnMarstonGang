using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampLedgerSystem
{
    private readonly string _savePath;
    private readonly List<CampLedgerEntry> _entries = new List<CampLedgerEntry>();

    public int MaxEntries { get; set; } = 500;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampLedgerEntry> Entries => _entries;

    public CampLedgerSystem(string saveFileName = "CampLedgerSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddEntry(string title, string category, string message, string source = null, decimal amount = 0m)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(message))
            return false;

        if (_entries.Count >= MaxEntries)
            _entries.RemoveAt(0);

        _entries.Add(new CampLedgerEntry
        {
            EntryId = Guid.NewGuid().ToString("N"),
            Title = title,
            Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
            Message = message,
            Source = source,
            Amount = amount,
            CreatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public IEnumerable<CampLedgerEntry> GetEntries()
    {
        return _entries.OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampLedgerEntry> GetEntriesByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return GetEntries();

        return GetEntries().Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CampLedgerEntry> GetEntriesBySource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            return GetEntries();

        return GetEntries().Where(x => string.Equals(x.Source, source, StringComparison.OrdinalIgnoreCase));
    }

    public decimal GetNetTotal()
    {
        return _entries.Sum(x => x.Amount);
    }

    public decimal GetTotalByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return GetNetTotal();

        return _entries.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase)).Sum(x => x.Amount);
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
            var loaded = JsonSerializer.Deserialize<List<CampLedgerEntry>>(json);

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

public class CampLedgerEntry
{
    public string EntryId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedUtc { get; set; }
}