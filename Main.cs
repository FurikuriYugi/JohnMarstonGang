using System.Collections.Generic;
using Newtonsoft.Json;

public class Main
{
    private readonly GangManager _gangManager;
    private readonly TacticalSquadAiSystem _tacticalAi;
    private readonly BankSystem _bankSystem;
    private readonly JsonSaveSystem _saveSystem;

    public Main()
    {
        _gangManager = new GangManager();
        _tacticalAi = new TacticalSquadAiSystem();
        _bankSystem = new BankSystem();
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
                CurrentTask = string.Empty,
                Loyalty = 100,
                Hunger = 0,
                BehaviorPriority = 50,
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
    }

    public void SaveAll(string path)
    {
        SaveRoot root = new SaveRoot
        {
            GangMembers = _gangManager.GetAllMembers(),
            BankAccounts = ConvertBankAccounts(_bankSystem.GetAllAccounts())
        };

        _saveSystem.Save(path, root);
    }

    public void LoadAll(string path)
    {
        SaveRoot root = _saveSystem.Load<SaveRoot>(path);
        if (root == null)
            return;

        _gangManager.LoadMembers(root.GangMembers);
        _bankSystem.LoadAccounts(ConvertBankAccounts(root.BankAccounts));
    }

    private List<BankAccountRecord> ConvertBankAccounts(List<BankAccountData> accounts)
    {
        List<BankAccountRecord> result = new List<BankAccountRecord>();

        if (accounts == null)
            return result;

        foreach (BankAccountData account in accounts)
        {
            if (account == null)
                continue;

            result.Add(new BankAccountRecord
            {
                OwnerId = account.OwnerId,
                OwnerName = account.OwnerName,
                Cash = account.Cash,
                Gold = account.Gold,
                CreditLimit = account.CreditLimit,
                Debt = account.Debt,
                IsSuspicious = account.IsSuspicious
            });
        }

        return result;
    }

    private List<BankAccountData> ConvertBankAccounts(List<BankAccountRecord> accounts)
    {
        List<BankAccountData> result = new List<BankAccountData>();

        if (accounts == null)
            return result;

        foreach (BankAccountRecord account in accounts)
        {
            if (account == null)
                continue;

            result.Add(new BankAccountData
            {
                OwnerId = account.OwnerId,
                OwnerName = account.OwnerName,
                Cash = account.Cash,
                Gold = account.Gold,
                CreditLimit = account.CreditLimit,
                Debt = account.Debt,
                IsSuspicious = account.IsSuspicious
            });
        }

        return result;
    }
}