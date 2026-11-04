using System;
using System.IO;
using System.Linq;
using RDR2;

public class Main : Script
{
    private const string SaveFileName = "JohnMarstonGang_Save.json";

    private readonly GangManager _gangManager = new GangManager();
    private readonly BankSystem _bankSystem = new BankSystem();
    private readonly TacticalSquadAiSystem _tacticalSystem = new TacticalSquadAiSystem();
    private readonly JobSystem _jobSystem = new JobSystem();
    private readonly WagonSystem _wagonSystem = new WagonSystem();
    private readonly CompanionSystem _companionSystem = new CompanionSystem();

    private PlayerGangSaveData _saveData = new PlayerGangSaveData();

    public Main()
    {
        Tick += OnTick;
        Aborted += OnAborted;
        Interval = 250;

        LoadState();
        _jobSystem.GenerateWorldJobs();
    }

    private void OnTick(object sender, EventArgs e)
    {
        _gangManager.UpdateMemberNeeds();

        foreach (var member in _gangManager.Members)
            _companionSystem.UpdateFromGangMember(member);

        var context = new TacticalContext
        {
            IsTraveling = true
        };

        _tacticalSystem.UpdateSquadTactics(_gangManager.Members, context);
    }

    private void OnAborted(object sender, EventArgs e)
    {
        SaveState();
    }

    private void LoadState()
    {
        var path = GetSavePath();
        _saveData = JsonSaveSystem.Load<PlayerGangSaveData>(path);

        if (_saveData.Bank != null)
            _bankSystem.LoadFromSaveData(_saveData.Bank);

        _wagonSystem.LoadFromSaveData(_saveData.Wagons);
        _companionSystem.LoadFromSave(_saveData.Companions);

        if (_saveData.GangMembers != null)
        {
            foreach (var member in _saveData.GangMembers)
            {
                _gangManager.AddMember(new GangMember
                {
                    Id = string.IsNullOrWhiteSpace(member.Id) ? Guid.NewGuid().ToString("N") : member.Id,
                    Name = member.Name,
                    CompanionType = member.CompanionType,
                    Role = member.Role,
                    Alignment = member.Alignment,
                    CombatStyle = member.CombatStyle,
                    TacticalState = member.TacticalState,
                    Loyalty = member.Loyalty,
                    Experience = member.Experience,
                    ModelName = member.ModelName,
                    Weapons = member.Weapons ?? new System.Collections.Generic.List<string>(),
                    MemoryTags = member.MemoryTags ?? new System.Collections.Generic.List<string>(),
                    Goals = member.Goals ?? new System.Collections.Generic.List<string>()
                });
            }
        }
    }

    private void SaveState()
    {
        _saveData.Bank = _bankSystem.ToSaveData();
        _saveData.Wagons = _wagonSystem.Wagons.Select(x => x.ToSaveData()).ToList();
        _saveData.Companions = _companionSystem.CreateSaveData();

        _saveData.GangMembers = _gangManager.Members.Select(x => new GangMemberSaveData
        {
            Id = x.Id,
            Name = x.Name,
            CompanionType = x.CompanionType,
            Role = x.Role,
            Alignment = x.Alignment,
            CombatStyle = x.CombatStyle,
            TacticalState = x.TacticalState,
            Loyalty = x.Loyalty,
            Experience = x.Experience,
            ModelName = x.ModelName,
            Weapons = new System.Collections.Generic.List<string>(x.Weapons),
            MemoryTags = new System.Collections.Generic.List<string>(x.MemoryTags),
            Goals = new System.Collections.Generic.List<string>(x.Goals)
        }).ToList();

        JsonSaveSystem.Save(GetSavePath(), _saveData);
    }

    private string GetSavePath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFileName);
    }
}