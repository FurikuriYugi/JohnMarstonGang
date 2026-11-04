using System;
using Newtonsoft.Json;

public class Main
{
    private readonly GangManager _gangManager;
    private readonly CompanionSystem _companionSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly WagonSystem _wagonSystem;
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;
    private readonly InteractionSystem _interactionSystem;
    private readonly TacticalSquadAiSystem _tacticalSquadAiSystem;

    public Main()
    {
        _gangManager = new GangManager();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _wagonSystem = new WagonSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();
    }

    public void Initialize()
    {
        LoadDefaults();
    }

    public void Tick()
    {
        TacticalContext context = BuildTacticalContext();

        _gangManager.ApplyTacticalContext(context);
        _tacticalSquadAiSystem.UpdateSquadTactics(_gangManager.GetAllMembers(), context);

        UpdateCompanions(context);
        UpdateWorldSystems(context);
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

    private void UpdateCompanions(TacticalContext context)
    {
        if (context == null)
            return;
    }

    private void UpdateWorldSystems(TacticalContext context)
    {
    }

    private void LoadDefaults()
    {
        if (_gangManager.GetAllMembers().Count == 0)
        {
            _gangManager.RegisterMember(new GangMember
            {
                MemberId = "player",
                Name = "Player",
                Role = "Leader",
                CombatStyle = "Ranged Medium",
                TacticalState = "Follow",
                Loyalty = 100,
                Hunger = 0
            });
        }
    }

    public string SaveAll()
    {
        SaveRoot root = new SaveRoot
        {
            GangMembers = _gangManager.GetAllMembers(),
            Companions = _companionSystem.GetAllCompanions(),
            BankAccounts = _bankSystem.GetAllAccounts(),
            Jobs = _jobSystem.GetAllJobs(),
            Wagons = _wagonSystem.GetAllWagons()
        };

        return JsonConvert.SerializeObject(root, Formatting.Indented);
    }

    public void LoadAll(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return;

        SaveRoot root = JsonConvert.DeserializeObject<SaveRoot>(json);
        if (root == null)
            return;

        _gangManager.LoadMembers(root.GangMembers);
        _companionSystem.LoadCompanions(root.Companions);
        _bankSystem.LoadAccounts(root.BankAccounts);
        _jobSystem.LoadJobs(root.Jobs);
        _wagonSystem.LoadWagons(root.Wagons);
    }
}