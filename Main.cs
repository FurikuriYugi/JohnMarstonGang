using System.IO;
using Newtonsoft.Json;
using RDR2;

public class Main : Script
{
    private const string SaveFileName = "JohnMorstonGang_Save.json";

    private SaveRoot _saveRoot;
    private GangManager _gangManager;
    private CompanionSystem _companionSystem;
    private BankSystem _bankSystem;
    private JobSystem _jobSystem;
    private LockSystem _lockSystem;
    private DynamiteSystem _dynamiteSystem;
    private TacticalSquadAiSystem _tacticalSquadAiSystem;
    private WeaponSystem _weaponSystem;
    private WagonSystem _wagonSystem;
    private CommandSystem _commandSystem;
    private InteractionSystem _interactionSystem;

    public Main()
    {
        _saveRoot = new SaveRoot();
        _gangManager = new GangManager();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();
        _weaponSystem = new WeaponSystem();
        _wagonSystem = new WagonSystem();
        _commandSystem = new CommandSystem();
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);

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
            _bankSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.BankAccounts));

        if (_saveRoot.World != null && _saveRoot.World.ActiveJobs != null)
            _jobSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.ActiveJobs));

        if (_saveRoot.World != null && _saveRoot.World.Wagons != null)
            _wagonSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Wagons));

        if (_saveRoot.World != null && _saveRoot.World.WeaponPlatforms != null)
            _weaponSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.WeaponPlatforms));
    }

    private void SaveAll()
    {
        if (_saveRoot == null)
            _saveRoot = new SaveRoot();

        _saveRoot.GangMembers = _gangManager.GetMembers();

        if (_saveRoot.World == null)
            _saveRoot.World = new WorldState();

        _saveRoot.World.Companions = _companionSystem.GetCompanions();
        _saveRoot.BankAccounts = _bankSystem.GetAllAccounts();
        _saveRoot.World.ActiveJobs = _jobSystem.GetAllJobs();
        _saveRoot.World.Wagons = _wagonSystem.GetWagons();
        _saveRoot.World.WeaponPlatforms = _weaponSystem.GetWeaponPlatforms();

        string json = JsonConvert.SerializeObject(_saveRoot, Formatting.Indented);
        File.WriteAllText(SaveFileName, json);
    }
}