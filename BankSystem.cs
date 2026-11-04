using System.Collections.Generic;
using Newtonsoft.Json;

public class BankSystem
{
    private readonly Dictionary<string, BankAccountData> _accounts = new Dictionary<string, BankAccountData>();

    public void RegisterAccount(BankAccountData account)
    {
        if (account == null || string.IsNullOrWhiteSpace(account.OwnerId))
            return;

        _accounts[account.OwnerId] = account;
    }

    public bool HasAccount(string ownerId)
    {
        return !string.IsNullOrWhiteSpace(ownerId) && _accounts.ContainsKey(ownerId);
    }

    public BankAccountData GetAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        _accounts.TryGetValue(ownerId, out BankAccountData account);
        return account;
    }

    public List<BankAccountData> GetAllAccounts()
    {
        return new List<BankAccountData>(_accounts.Values);
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

            _accounts[account.OwnerId] = account;
        }
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Cash += amount;
        account.LastTransactionUtc = System.DateTime.UtcNow;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m || account.Cash < amount)
            return false;

        account.Cash -= amount;
        account.LastTransactionUtc = System.DateTime.UtcNow;
        return true;
    }

    public void DepositGold(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Gold += amount;
        account.LastTransactionUtc = System.DateTime.UtcNow;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m || account.Gold < amount)
            return false;

        account.Gold -= amount;
        account.LastTransactionUtc = System.DateTime.UtcNow;
        return true;
    }

    public void AddDebt(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Debt += amount;
        account.LastTransactionUtc = System.DateTime.UtcNow;
    }

    public void ReduceDebt(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Debt -= amount;
        if (account.Debt < 0m)
            account.Debt = 0m;

        account.LastTransactionUtc = System.DateTime.UtcNow;
    }

    public void SetSuspicious(string ownerId, bool suspicious)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null)
            return;

        account.IsSuspicious = suspicious;
        account.LastTransactionUtc = System.DateTime.UtcNow;
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