using System;
using System.Collections.Generic;
using System.Linq;

public class CompanionSystem
{
    private readonly List<CompanionRecord> _companions = new List<CompanionRecord>();

    public List<CompanionRecord> Companions
    {
        get { return _companions; }
    }

    public CompanionRecord GetOrCreateCompanion(string companionId, string name)
    {
        CompanionRecord companion = _companions.FirstOrDefault(c => c.Id == companionId);
        if (companion != null)
            return companion;

        companion = new CompanionRecord
        {
            Id = companionId,
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

        CompanionRecord companion = GetOrCreateCompanion(member.Id, member.Name);
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
        CompanionRecord companion = _companions.FirstOrDefault(c => c.Id == companionId);
        if (companion == null)
            return;

        if (!companion.Memories.Contains(memory))
            companion.Memories.Add(memory);
    }

    public List<CompanionSaveData> CreateSaveData()
    {
        return _companions.Select(c => new CompanionSaveData
        {
            Id = c.Id,
            Name = c.Name,
            ModelName = c.ModelName,
            Alignment = c.Alignment,
            Loyalty = c.Loyalty,
            Experience = c.Experience,
            IsHuman = c.IsHuman,
            IsAnimal = c.IsAnimal,
            CurrentWeapon = c.CurrentWeapon,
            CurrentTask = c.CurrentTask,
            Outfit = c.Outfit,
            PersonalGoal = c.PersonalGoal
        }).ToList();
    }

    public void LoadFromSave(List<CompanionSaveData> data)
    {
        _companions.Clear();

        if (data == null)
            return;

        foreach (CompanionSaveData companionData in data)
        {
            _companions.Add(new CompanionRecord
            {
                Id = companionData.Id,
                Name = companionData.Name,
                ModelName = companionData.ModelName,
                Alignment = companionData.Alignment,
                Loyalty = companionData.Loyalty,
                Experience = companionData.Experience,
                IsHuman = companionData.IsHuman,
                IsAnimal = companionData.IsAnimal,
                CurrentWeapon = companionData.CurrentWeapon,
                CurrentTask = companionData.CurrentTask,
                Outfit = companionData.Outfit,
                PersonalGoal = companionData.PersonalGoal,
                Goals = new List<string>(),
                Memories = new List<string>(),
                Trust = companionData.Loyalty
            });
        }
    }
}

public class CompanionRecord
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ModelName { get; set; }
    public string Alignment { get; set; }
    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Experience { get; set; }
    public bool IsHuman { get; set; }
    public bool IsAnimal { get; set; }
    public string CurrentWeapon { get; set; }
    public string CurrentTask { get; set; }
    public string Outfit { get; set; }
    public string PersonalGoal { get; set; }
    public List<string> Goals { get; set; }
    public List<string> Memories { get; set; }
}