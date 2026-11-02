using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class WagonDeploymentSystem
{
    private readonly string _savePath;
    private readonly List<WagonDeploymentRecord> _records = new List<WagonDeploymentRecord>();

    public bool Enabled { get; set; } = true;
    public int MaxActiveDeployments { get; set; } = 3;
    public float DeployRadius { get; set; } = 8f;
    public float RecallRadius { get; set; } = 6f;

    public IReadOnlyList<WagonDeploymentRecord> Records => _records;

    public WagonDeploymentSystem(string saveFileName = "WagonDeploymentSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterWagon(string wagonId, string wagonModel, string ownerMemberId)
    {
        if (string.IsNullOrWhiteSpace(wagonId) || string.IsNullOrWhiteSpace(wagonModel))
            return false;

        var existing = _records.FirstOrDefault(x => x.WagonId == wagonId);
        if (existing != null)
        {
            existing.WagonModel = wagonModel;
            existing.OwnerMemberId = ownerMemberId;
            existing.Active = false;
            existing.LastKnownPosition = null;
            existing.LastUpdatedUtc = DateTime.UtcNow;
            Save();
            return true;
        }

        _records.Add(new WagonDeploymentRecord
        {
            WagonId = wagonId,
            WagonModel = wagonModel,
            OwnerMemberId = ownerMemberId,
            Active = false,
            SpawnedUtc = DateTime.UtcNow,
            LastUpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool DeployWagon(string wagonId, Vector3 deployPosition, float heading = 0f)
    {
        if (!Enabled || string.IsNullOrWhiteSpace(wagonId))
            return false;

        if (GetActiveCount() >= MaxActiveDeployments)
            return false;

        var record = _records.FirstOrDefault(x => x.WagonId == wagonId);
        if (record == null)
            return false;

        record.Active = true;
        record.LastKnownPosition = deployPosition;
        record.Heading = heading;
        record.LastUpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RecallWagon(string wagonId)
    {
        if (string.IsNullOrWhiteSpace(wagonId))
            return false;

        var record = _records.FirstOrDefault(x => x.WagonId == wagonId);
        if (record == null || !record.Active)
            return false;

        record.Active = false;
        record.LastKnownPosition = null;
        record.LastUpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool UpdateWagonPosition(string wagonId, Vector3 position, float heading = 0f)
    {
        var record = _records.FirstOrDefault(x => x.WagonId == wagonId);
        if (record == null)
            return false;

        record.LastKnownPosition = position;
        record.Heading = heading;
        record.LastUpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public WagonDeploymentRecord GetActiveWagonForMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        return _records.FirstOrDefault(x => x.Active && x.OwnerMemberId == memberId);
    }

    public IEnumerable<WagonDeploymentRecord> GetActiveWagons()
    {
        return _records.Where(x => x.Active);
    }

    public bool ShouldRecallForDistance(string wagonId, Vector3 playerPosition)
    {
        var record = _records.FirstOrDefault(x => x.WagonId == wagonId);
        if (record == null || !record.Active || !record.LastKnownPosition.HasValue)
            return false;

        return Vector3.Distance(playerPosition, record.LastKnownPosition.Value) <= RecallRadius;
    }

    public bool IsDeploymentInRange(Vector3 playerPosition, Vector3 targetPosition)
    {
        return Vector3.Distance(playerPosition, targetPosition) <= DeployRadius;
    }

    public void CleanupInactiveOlderThan(TimeSpan age)
    {
        DateTime cutoff = DateTime.UtcNow.Subtract(age);
        bool changed = _records.RemoveAll(x => !x.Active && x.LastUpdatedUtc < cutoff) > 0;
        if (changed)
            Save();
    }

    public void Clear()
    {
        _records.Clear();
        Save();
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
            var loaded = JsonSerializer.Deserialize<List<WagonDeploymentRecord>>(json);

            _records.Clear();
            if (loaded != null)
                _records.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.WagonId) && !string.IsNullOrWhiteSpace(x.WagonModel)));
        }
        catch
        {
            _records.Clear();
        }
    }

    private int GetActiveCount()
    {
        return _records.Count(x => x.Active);
    }
}

public class WagonDeploymentRecord
{
    public string WagonId { get; set; }
    public string WagonModel { get; set; }
    public string OwnerMemberId { get; set; }
    public bool Active { get; set; }
    public float Heading { get; set; }
    public DateTime SpawnedUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
    public Vector3? LastKnownPosition { get; set; }
}