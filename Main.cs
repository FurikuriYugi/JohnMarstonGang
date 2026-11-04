using System.Collections.Generic;
using Newtonsoft.Json;

public class Main
{
    private readonly GangManager _gangManager;
    private readonly TacticalSquadAiSystem _tacticalAi;
    private readonly CompanionSystem _companionSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly WagonSystem _wagonSystem;
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;
    private readonly InteractionSystem _interactionSystem;
    private readonly JsonSaveSystem _saveSystem;

    public Main()
    {
        _gangManager = new GangManager();
        _tacticalAi = new TacticalSquadAiSystem();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _wagonSystem = new WagonSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);
        _saveSystem = new JsonSaveSystem();
    }

    public void Initialize()
    {
        LoadDefaults();
    }

    public void Tick()
    {
        TacticalContext context = BuildTacticalContext();
        _gangManager.ApplyTacticalContext(context);
        _tacticalAi.UpdateSquadTactics(_gangManager.GetAllMembers(), context);
    }

    private TacticalContext BuildTacticalContext()
    {
        return new TacticalContext
        {
            IsInCombat = false,
            IsStealth = false,
            IsTraveling = true,
            IsShopping = false
        };
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
                Hunger = 0,
                MemoryTags = new List<string>()
            });
        }

        if (_bankSystem.GetAllAccounts().Count == 0)
        {
            _bankSystem.RegisterAccount(new BankAccountRecord
            {
                OwnerId = "player",
                OwnerName = "Player",
                Cash = 0m,
                Gold = 0m,
                CreditLimit = 100m,
                Debt = 0m,
                IsSuspicious = false
            });
        }

        if (_jobSystem.GetAllJobs().Count == 0)
        {
            _jobSystem.RegisterJob(new JobRecord
            {
                JobId = "starter_bank_run",
                Title = "Bank Run",
                Faction = "Civilian",
                PayoutCash = 10m,
                PayoutGold = 0m,
                HonorChange = 1,
                IsAvailable = true,
                IsCompleted = false
            });
        }

        if (_wagonSystem.GetAllWagons().Count == 0)
        {
            _wagonSystem.RegisterWagon(new WagonData
            {
                WagonId = "starter_wagon",
                OwnerId = "player",
                IsOwned = true,
                IsHitched = false,
                StoredItems = new List<string>()
            });
        }
    }

    public void SaveAll(string path)
    {
        SaveRoot root = new SaveRoot
        {
            GangMembers = _gangManager.GetAllMembers(),
            Companions = _companionSystem.GetAllCompanions(),
            BankAccounts = _bankSystem.GetAllAccounts(),
            Jobs = _jobSystem.GetAllJobs(),
            Wagons = _wagonSystem.GetAllWagons()
        };

        _saveSystem.Save(path, root);
    }

    public void LoadAll(string path)
    {
        SaveRoot root = _saveSystem.Load<SaveRoot>(path);
        if (root == null)
            return;

        _gangManager.LoadMembers(root.GangMembers);
        _companionSystem.LoadCompanions(root.Companions);
        _bankSystem.LoadAccounts(root.BankAccounts);
        _jobSystem.LoadJobs(root.Jobs);
        _wagonSystem.LoadWagons(root.Wagons);
    }
}