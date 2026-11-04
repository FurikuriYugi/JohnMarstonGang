using System.Collections.Generic;
using Newtonsoft.Json;

public class BankSystem
{
    private readonly List<BankAccountData> _accounts = new List<BankAccountData>();

    public List<BankAccountData> GetAllAccounts()
    {
        return new List<BankAccountData>(_accounts);
    }

    public BankAccountData GetAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        return _accounts.Find(x => x != null && x.OwnerId == ownerId);
    }

    public void RegisterAccount(BankAccountData account)
    {
        if (account == null || string.IsNullOrWhiteSpace(account.OwnerId))
            return;

        _accounts.RemoveAll(x => x != null && x.OwnerId == account.OwnerId);
        _accounts.Add(account);
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Cash += amount;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m || account.Cash < amount)
            return false;

        account.Cash -= amount;
        return true;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_accounts, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _accounts.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<BankAccountData> accounts = JsonConvert.DeserializeObject<List<BankAccountData>>(json);
        if (accounts != null)
            _accounts.AddRange(accounts);
    }
}