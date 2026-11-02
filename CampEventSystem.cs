using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampEventSystem
{
    private readonly string _savePath;
    private readonly List<CampEventRecord> _events = new List<CampEventRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxEvents { get; set; } = 64;

    public IReadOnlyList<CampEventRecord> Events => _events;

    public CampEventSystem(string saveFileName = "CampEventSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddOrUpdateEvent(string eventId, string eventName, DateTime scheduledUtc, string description = null, bool recurring = false, string recurrenceRule = null)
    {
        if (string.IsNullOrWhiteSpace(eventId) || string.IsNullOrWhiteSpace(eventName))
            return false;

        var existing = _events.FirstOrDefault(x => x.EventId == eventId);
        if (existing == null)
        {
            if (_events.Count >= MaxEvents)
                return false;

            _events.Add(new CampEventRecord
            {
                EventId = eventId,
                EventName = eventName,
                ScheduledUtc = scheduledUtc,
                Description = description,
                Recurring = recurring,
                RecurrenceRule = recurrenceRule,
                Enabled = true,
                CreatedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            });
        }
        else
        {
            existing.EventName = eventName;
            existing.ScheduledUtc = scheduledUtc;
            existing.Description = description;
            existing.Recurring = recurring;
            existing.RecurrenceRule = recurrenceRule;
            existing.Enabled = true;
            existing.UpdatedUtc = DateTime.UtcNow;
        }

        Save();
        return true;
    }

    public bool RemoveEvent(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
            return false;

        bool removed = _events.RemoveAll(x => x.EventId == eventId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public bool SetEventTime(string eventId, DateTime scheduledUtc)
    {
        var ev = _events.FirstOrDefault(x => x.EventId == eventId);
        if (ev == null)
            return false;

        ev.ScheduledUtc = scheduledUtc;
        ev.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool ToggleEvent(string eventId, bool enabled)
    {
        var ev = _events.FirstOrDefault(x => x.EventId == eventId);
        if (ev == null)
            return false;

        ev.Enabled = enabled;
        ev.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public CampEventRecord GetEvent(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
            return null;

        return _events.FirstOrDefault(x => x.EventId == eventId);
    }

    public IEnumerable<CampEventRecord> GetUpcomingEvents()
    {
        return _events.Where(x => x.Enabled && x.ScheduledUtc >= DateTime.UtcNow).OrderBy(x => x.ScheduledUtc);
    }

    public IEnumerable<CampEventRecord> GetPastEvents()
    {
        return _events.Where(x => x.ScheduledUtc < DateTime.UtcNow).OrderByDescending(x => x.ScheduledUtc);
    }

    public bool HasEvent(string eventId)
    {
        return !string.IsNullOrWhiteSpace(eventId) && _events.Any(x => x.EventId == eventId);
    }

    public void Clear()
    {
        _events.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_events, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampEventRecord>>(json);

            _events.Clear();
            if (loaded != null)
                _events.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.EventId) && !string.IsNullOrWhiteSpace(x.EventName)));
        }
        catch
        {
            _events.Clear();
        }
    }
}

public class CampEventRecord
{
    public string EventId { get; set; }
    public string EventName { get; set; }
    public DateTime ScheduledUtc { get; set; }
    public string Description { get; set; }
    public bool Recurring { get; set; }
    public string RecurrenceRule { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}