using Newtonsoft.Json;
using RDR2;
using System;
using System.IO;

public class Main : Script
{
    private GangManager _gangManager;
    private BankSystem _bankSystem;
    private TacticalSquadAiSystem _tacticalAi;
    private JobSystem _jobSystem;
    private InteractionSystem _interactionSystem;
    private WagonSystem _wagonSystem;
    private DynamiteSystem _dynamiteSystem;
    private LockSystem _lockSystem;
    private CompanionSystem _companionSystem;

    private readonly string _savePath = "JohnMorstonGangSave.json";
    private bool _initialized;

    public Main()
    {
        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void Initialize()
    {
        if (_initialized) return;

        _gangManager = new GangManager();
        _bankSystem = new BankSystem();
        _tacticalAi = new TacticalSquadAiSystem();
        _jobSystem = new JobSystem();
        _wagonSystem = new WagonSystem();
        _dynamiteSystem = new DynamiteSystem();
        _lockSystem = new LockSystem();
        _companionSystem = new CompanionSystem(_gangManager);

        _interactionSystem = new InteractionSystem(
            _lockSystem,
            _dynamiteSystem,
            _wagonSystem,
            _bankSystem,
            _jobSystem);

        LoadState();
        _initialized = true;
    }

    private void OnTick(object sender, EventArgs e)
    {
        Initialize();

        if (_tacticalAi != null && _gangManager != null)
            _tacticalAi.Update(_gangManager);

        _companionSystem?.Update();
    }

    private void OnAborted(object sender, EventArgs e)
    {
        SaveState();
    }

    private void SaveState()
    {
        if (_gangManager == null || _bankSystem == null || _wagonSystem == null || _jobSystem == null)
            return;

        var data = new GangSaveData
        {
            Members = _gangManager.CreateSaveData(),
            Bank = _bankSystem.CreateSaveData(),
            Wagons = _wagonSystem.CreateSaveData(),
            JobOffers = new System.Collections.Generic.List<JobOffer>(_jobSystem.Offers),
            JobAssignments = new System.Collections.Generic.List<JobAssignment>(_jobSystem.Assignments)
        };

        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(_savePath, json);
    }

    private void LoadState()
    {
        if (!File.Exists(_savePath))
            return;

        var json = File.ReadAllText(_savePath);
        var data = JsonConvert.DeserializeObject<GangSaveData>(json);

        if (data == null)
            return;

        _gangManager.LoadFromSave(data.Members);
        _bankSystem.LoadFromSave(data.Bank);
        _wagonSystem.LoadFromSave(data.Wagons);
        _jobSystem.LoadFromSave(data.JobOffers, data.JobAssignments);
    }
}