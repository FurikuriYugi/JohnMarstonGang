using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionSystem
{
    private readonly List<CompanionRecord> _companions = new List<CompanionRecord>();

    public List<CompanionRecord> Companions => _companions;

    public CompanionRecord GetOrCreateCompanion(string companionId, string name)
    {
        var companion = _companions.FirstOrDefault(c => c.CompanionId == companionId);
        if (companion != null)
            return companion;

        companion = new CompanionRecord
        {
            CompanionId = companionId,
            Name = name,
            Loyalty = 50,
            Trust = 50,
            Experience = 0,
            Goals = new List<string>(),
            Memories = new List<string>()
        };

        _companions.Add(companion);
        return companion;
    }

    public void UpdateFromGangMember(GangMember member)
    {
        if (member == null)
            return;

        var companion = GetOrCreateCompanion(member.Id, member.Name);
        companion.Loyalty = member.Loyalty;
        companion.Trust = member.Loyalty;
        companion.Experience = member.Experience;
        companion.Goals = new List<string>(member.Goals ?? new List<string>());
        companion.Memories = new List<string>(member.MemoryTags ?? new List<string>());
    }

    public void ApplyPersonalGoal(GangMember member)
    {
        if (member == null)
            return;

        if (member.Goals == null)
            member.Goals = new List<string>();

        if (member.Goals.Count == 0)
            member.Goals.Add("Stay close to the gang");
    }

    public void AddMemory(string companionId, string memory)
    {
        var companion = _companions.FirstOrDefault(c => c.CompanionId == companionId);
        if (companion == null)
            return;

        if (!companion.Memories.Contains(memory))
            companion.Memories.Add(memory);
    }

    public List<CompanionSaveData> CreateSaveData()
    {
        return _companions.Select(c => new CompanionSaveData
        {
            CompanionId = c.CompanionId,
            Name = c.Name,
            Loyalty = c.Loyalty,
            Trust = c.Trust,
            Experience = c.Experience,
            Goals = new List<string>(c.Goals ?? new List<string>()),
            Memories = new List<string>(c.Memories ?? new List<string>())
        }).ToList();
    }

    public void LoadFromSave(List<CompanionSaveData> data)
    {
        _companions.Clear();

        if (data == null)
            return;

        foreach (var companionData in data)
        {
            _companions.Add(new CompanionRecord
            {
                CompanionId = companionData.CompanionId,
                Name = companionData.Name,
                Loyalty = companionData.Loyalty,
                Trust = companionData.Trust,
                Experience = companionData.Experience,
                Goals = companionData.Goals ?? new List<string>(),
                Memories = companionData.Memories ?? new List<string>()
            });
        }
    }
}

public class CompanionRecord
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Experience { get; set; }
    public List<string> Goals { get; set; }
    public List<string> Memories { get; set; }
}