using Newtonsoft.Json;
using RDR2;
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
    private DynamiteSystem _dynamiteSystem;
    private WeaponSystem _weaponSystem;
    private WagonSystem _wagonSystem;
    private InteractionSystem _interactionSystem;
    private CommandSystem _commandSystem;
    private TacticalSquadAiSystem _tacticalSquadAiSystem;

    public Main()
    {
        _saveRoot = new SaveRoot();
        _gangManager = new GangManager();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _weaponSystem = new WeaponSystem();
        _wagonSystem = new WagonSystem();
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);
        _commandSystem = new CommandSystem();
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();

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

        if (_saveRoot.World != null && _saveRoot.World.Commands != null)
            _commandSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Commands));

        if (_saveRoot.World != null && _saveRoot.World.TacticalOrders != null)
            _tacticalSquadAiSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.TacticalOrders));
    }

    private void SaveAll()
    {
        if (_saveRoot == null)
            _saveRoot = new SaveRoot();

        if (_saveRoot.World == null)
            _saveRoot.World = new WorldState();

        _saveRoot.GangMembers = _gangManager.GetMembers();
        _saveRoot.World.Companions = _companionSystem.GetCompanions();
        _saveRoot.BankAccounts = _bankSystem.GetAllAccounts();
        _saveRoot.World.ActiveJobs = _jobSystem.GetAllJobs();
        _saveRoot.World.Wagons = _wagonSystem.GetWagons();
        _saveRoot.World.WeaponPlatforms = _weaponSystem.GetPlatforms();
        _saveRoot.World.Commands = _commandSystem.GetCommands();
        _saveRoot.World.TacticalOrders = _tacticalSquadAiSystem.GetOrders();

        string json = JsonConvert.SerializeObject(_saveRoot, Formatting.Indented);
        File.WriteAllText(SaveFileName, json);
    }
}