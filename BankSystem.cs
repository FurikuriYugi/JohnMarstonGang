using System;
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

    public bool DepositCash(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        _accounts[ownerId].Cash += amount;
        return true;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        BankAccountRecord account = _accounts[ownerId];
        if (account.Cash < amount)
            return false;

        account.Cash -= amount;
        return true;
    }

    public bool DepositGold(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        _accounts[ownerId].Gold += amount;
        return true;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        BankAccountRecord account = _accounts[ownerId];
        if (account.Gold < amount)
            return false;

        account.Gold -= amount;
        return true;
    }

    public bool TakeLoan(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        BankAccountRecord account = _accounts[ownerId];
        if (account.Debt + amount > account.CreditLimit)
            return false;

        account.Cash += amount;
        account.Debt += amount;
        account.IsSuspicious = true;
        return true;
    }

    public bool RepayLoan(string ownerId, decimal amount)
    {
        if (!_accounts.ContainsKey(ownerId) || amount <= 0m)
            return false;

        BankAccountRecord account = _accounts[ownerId];
        if (account.Cash < amount || account.Debt <= 0m)
            return false;

        decimal payment = Math.Min(amount, account.Debt);
        account.Cash -= payment;
        account.Debt -= payment;
        return true;
    }

    public decimal GetVaultValue()
    {
        decimal total = 0m;
        foreach (BankAccountRecord account in _accounts.Values)
            total += account.Cash + account.Gold;
        return total;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_accounts.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _accounts.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<BankAccountRecord> accounts = JsonConvert.DeserializeObject<List<BankAccountRecord>>(json);
        if (accounts == null)
            return;

        foreach (BankAccountRecord account in accounts)
        {
            if (account != null && !string.IsNullOrWhiteSpace(account.OwnerId))
                _accounts[account.OwnerId] = account;
        }
    }
}