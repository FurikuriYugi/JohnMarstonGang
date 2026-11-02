using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampRosterSystem
{
    private readonly string _savePath;
    private readonly List<CampRosterRecord> _roster = new List<CampRosterRecord>();

    public int MaxMembers { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampRosterRecord> Members => _roster;

    public CampRosterSystem(string saveFileName = "CampRosterSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddMember(string name, string role, string duty, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role))
            return false;

        if (_roster.Count >= MaxMembers)
            _roster.RemoveAt(0);

        _roster.Add(new CampRosterRecord
        {
            MemberId = Guid.NewGuid().ToString("N"),
            Name = name,
            Role = role,
            Duty = string.IsNullOrWhiteSpace(duty) ? "General" : duty,
            Notes = notes,
            IsActive = true,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool UpdateMember(string memberId, string role, string duty, string notes)
    {
        var member = GetMember(memberId);
        if (member == null)
            return false;

        if (!string.IsNullOrWhiteSpace(role))
            member.Role = role;
        if (!string.IsNullOrWhiteSpace(duty))
            member.Duty = duty;

        member.Notes = notes;
        member.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool SetActive(string memberId, bool active)
    {
        var member = GetMember(memberId);
        if (member == null)
            return false;

        member.IsActive = active;
        member.UpdatedUtc = DateTime.UtcNow;
        Save();
        return true;
    }

    public bool RemoveMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return false;

        bool removed = _roster.RemoveAll(x => x.MemberId == memberId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampRosterRecord GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        return _roster.FirstOrDefault(x => x.MemberId == memberId);
    }

    public IEnumerable<CampRosterRecord> GetActiveMembers()
    {
        return _roster.Where(x => x.IsActive).OrderBy(x => x.Name);
    }

    public IEnumerable<CampRosterRecord> GetMembersByRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return _roster.OrderBy(x => x.Name);

        return _roster.Where(x => string.Equals(x.Role, role, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Name);
    }

    public int GetActiveCount()
    {
        return _roster.Count(x => x.IsActive);
    }

    public string GetRosterStatus()
    {
        int active = GetActiveCount();

        if (active >= 75)
            return "Full";
        if (active >= 40)
            return "Large";
        if (active >= 15)
            return "Steady";
        if (active >= 1)
            return "Small";

        return "Empty";
    }

    public void Clear()
    {
        _roster.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_roster, options));
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
            var loaded = JsonSerializer.Deserialize<List<CampRosterRecord>>(json);

            _roster.Clear();
            if (loaded != null)
                _roster.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.MemberId)));
        }
        catch
        {
            _roster.Clear();
        }
    }
}

public class CampRosterRecord
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Duty { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}