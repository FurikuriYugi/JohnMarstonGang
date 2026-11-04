using Newtonsoft.Json;
using RDR2;

public class Main : Script
{
    private readonly GangManager _gangManager;
    private readonly CompanionSystem _companionSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;
    private readonly WeaponSystem _weaponSystem;
    private readonly WagonSystem _wagonSystem;
    private readonly TacticalSquadAiSystem _tacticalSquadAiSystem;
    private readonly WorldEntityRegistry _entityRegistry;
    private readonly TacticalDecisionSystem _tacticalDecisionSystem;
    private readonly InteractionSystem _interactionSystem;

    private SaveRoot _saveRoot;

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
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();
        _entityRegistry = new WorldEntityRegistry();
        _tacticalDecisionSystem = new TacticalDecisionSystem(_entityRegistry, _tacticalSquadAiSystem);
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);

        LoadAll();
    }

    public override void Tick()
    {
    }

    private void LoadAll()
    {
        _saveRoot = JsonSaveSystem.Load();

        if (_saveRoot == null)
            _saveRoot = new SaveRoot();

        if (_saveRoot.GangMembers != null)
            _gangManager.ImportSaveData(_saveRoot);

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

        if (_saveRoot.World != null && _saveRoot.World.Locks != null)
            _lockSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Locks));

        if (_saveRoot.World != null && _saveRoot.World.Destructibles != null)
            _dynamiteSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Destructibles));

        if (_saveRoot.World != null && _saveRoot.World.TacticalOrders != null)
            _tacticalSquadAiSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.TacticalOrders));

        if (_saveRoot.WorldEntities != null)
            _entityRegistry.LoadState(JsonConvert.SerializeObject(_saveRoot.WorldEntities));
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
        _saveRoot.World.Locks = _lockSystem.GetLocks();
        _saveRoot.World.Destructibles = _dynamiteSystem.GetDestructibles();
        _saveRoot.World.TacticalOrders = _tacticalSquadAiSystem.GetOrders();
        _saveRoot.WorldEntities = _entityRegistry.GetEntities();

        JsonSaveSystem.Save(_saveRoot);
    }
}