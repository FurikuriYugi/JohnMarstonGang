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
        {
            _companionSystem.UpdateFromGangMember(member);
        }

        TacticalContext context = BuildTacticalContext();
        _tacticalSystem.UpdateSquadTactics(_gangManager.Members, context);

        SyncSaveState();
    }

    private TacticalContext BuildTacticalContext()
    {
        return new TacticalContext
        {
            IsTraveling = true,
            IsShopping = false,
            IsInCombat = false,
            IsStealth = false
        };
    }

    private void OnAborted(object sender, EventArgs e)
    {
        SaveState();
    }

    private void LoadState()
    {
        string path = GetSavePath();
        _saveData = JsonSaveSystem.Load<PlayerGangSaveData>(path);

        if (_saveData.Bank != null)
        {
            _bankSystem.VaultCash = _saveData.Bank.VaultCash;
            _bankSystem.VaultGold = _saveData.Bank.VaultGold;
            _bankSystem.LoadFromSave(_saveData.Bank.Accounts);
        }

        if (_saveData.Wagons != null)
        {
            _wagonSystem.LoadFromSaveData(_saveData.Wagons);
        }

        if (_saveData.Companions != null)
        {
            _companionSystem.LoadFromSave(_saveData.Companions);
        }

        if (_saveData.GangMembers == null)
        {
            return;
        }

        foreach (GangMemberSaveData member in _saveData.GangMembers)
        {
            GangMember gangMember = new GangMember
            {
                Id = string.IsNullOrWhiteSpace(member.Id) ? Guid.NewGuid().ToString("N") : member.Id,
                Name = member.Name,
                ModelName = member.ModelName,
                Role = member.Role.ToString(),
                Alignment = member.Alignment.ToString(),
                CombatStyle = member.CombatStyle.ToString(),
                TacticalState = member.TacticalState.ToString(),
                Loyalty = member.Loyalty,
                Experience = member.Experience,
                Weapons = member.Weapons ?? new System.Collections.Generic.List<string>(),
                MemoryTags = member.MemoryTags ?? new System.Collections.Generic.List<string>(),
                Goals = member.Goals ?? new System.Collections.Generic.List<string>()
            };

            _gangManager.AddMember(gangMember);
        }
    }

    private void SyncSaveState()
    {
        _saveData.Bank = new BankSaveData
        {
            VaultCash = _bankSystem.VaultCash,
            VaultGold = _bankSystem.VaultGold,
            Accounts = _bankSystem.CreateSaveData()
        };

        _saveData.Wagons = _wagonSystem.Wagons.Select(x => x.ToSaveData()).ToList();
        _saveData.Companions = _companionSystem.CreateSaveData();
        _saveData.GangMembers = _gangManager.CreateSaveData();
    }

    private void SaveState()
    {
        SyncSaveState();
        JsonSaveSystem.Save(GetSavePath(), _saveData);
    }

    private string GetSavePath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFileName);
    }
}