using Newtonsoft.Json;
using RDR2;
using System;
using System.IO;

public class Main : Script
{
    private const string SaveFileName = "JohnMorstonGang_Save.json";

    private SaveRoot _saveRoot;
    private GangManager _gangManager;
    private CompanionSystem _companionSystem;
    private BankSystem _bankSystem;
    private JobSystem _jobSystem;
    private LockSystem _lockSystem;
    private WeaponSystem _weaponSystem;

    public Main()
    {
        _saveRoot = new SaveRoot();
        _gangManager = new GangManager();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _lockSystem = new LockSystem();
        _weaponSystem = new WeaponSystem();
        LoadAll();
    }

    public override void Tick()
    {
    }

    private void LoadAll()
    {
        if (!File.Exists(SaveFileName))
            return;

        string json = File.ReadAllText(SaveFileName);
        if (string.IsNullOrWhiteSpace(json))
            return;

        _saveRoot = JsonConvert.DeserializeObject<SaveRoot>(json) ?? new SaveRoot();

        if (_saveRoot.GangMembers != null)
            _gangManager.LoadState(JsonConvert.SerializeObject(_saveRoot.GangMembers));

        if (_saveRoot.World != null && _saveRoot.World.Companions != null)
            _companionSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Companions));

        if (_saveRoot.BankAccounts != null)
            _bankSystem.LoadAccounts(_saveRoot.BankAccounts);

        if (_saveRoot.World != null && _saveRoot.World.ActiveJobs != null)
            _jobSystem.LoadJobs(_saveRoot.World.ActiveJobs);
    }

    private void SaveAll()
    {
        _saveRoot.GangMembers = _gangManager.GetMembers();
        _saveRoot.World.Companions = _companionSystem.GetCompanions();
        _saveRoot.BankAccounts = _bankSystem.GetAllAccounts();
        _saveRoot.World.ActiveJobs = _jobSystem.GetAllJobs();

        string json = JsonConvert.SerializeObject(_saveRoot, Formatting.Indented);
        File.WriteAllText(SaveFileName, json);
    }
}