using System.Collections.Generic;
using Newtonsoft.Json;

public class BankSystem
{
    private readonly Dictionary<string, BankAccountRecord> _accounts = new Dictionary<string, BankAccountRecord>();

    public void RegisterAccount(BankAccountRecord account)
    {
        if (account == null || string.IsNullOrWhiteSpace(account.OwnerId))
            return;

        _accounts[account.OwnerId] = account;
    }

    public bool HasAccount(string ownerId)
    {
        return !string.IsNullOrWhiteSpace(ownerId) && _accounts.ContainsKey(ownerId);
    }

    public BankAccountRecord GetAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        _accounts.TryGetValue(ownerId, out BankAccountRecord account);
        return account;
    }

    public List<BankAccountRecord> GetAllAccounts()
    {
        return new List<BankAccountRecord>(_accounts.Values);
    }

    public void LoadAccounts(List<BankAccountData> accounts)
    {
        _accounts.Clear();

        if (accounts == null)
            return;

        foreach (BankAccountData account in accounts)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.OwnerId))
                continue;

            _accounts[account.OwnerId] = new BankAccountRecord
            {
                OwnerId = account.OwnerId,
                OwnerName = account.OwnerName,
                Cash = account.Cash,
                Gold = account.Gold,
                CreditLimit = account.CreditLimit,
                Debt = account.Debt,
                IsSuspicious = account.IsSuspicious
            };
        }
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Cash += amount;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return false;

        if (account.Cash < amount)
            return false;

        account.Cash -= amount;
        return true;
    }

    public void DepositGold(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Gold += amount;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return false;

        if (account.Gold < amount)
            return false;

        account.Gold -= amount;
        return true;
    }

    public void AddDebt(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Debt += amount;
    }

    public void ReduceDebt(string ownerId, decimal amount)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Debt -= amount;
        if (account.Debt < 0m)
            account.Debt = 0m;
    }

    public void SetSuspicious(string ownerId, bool suspicious)
    {
        BankAccountRecord account = GetAccount(ownerId);
        if (account == null)
            return;

        account.IsSuspicious = suspicious;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllAccounts(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _accounts.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<BankAccountData> accounts = JsonConvert.DeserializeObject<List<BankAccountData>>(json);
        LoadAccounts(accounts);
    }
}