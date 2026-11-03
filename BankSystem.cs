using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>(StringComparer.OrdinalIgnoreCase);

    public void DepositCash(string ownerName, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerName) || amount <= 0m)
            return;

        BankAccount account = GetOrCreateAccount(ownerName);
        account.Cash += amount;
    }

    public void DepositGold(string ownerName, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerName) || amount <= 0m)
            return;

        BankAccount account = GetOrCreateAccount(ownerName);
        account.Gold += amount;
    }

    public bool WithdrawCash(string ownerName, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerName) || amount <= 0m)
            return false;

        BankAccount account = GetOrCreateAccount(ownerName);
        if (account.Cash < amount)
            return false;

        account.Cash -= amount;
        return true;
    }

    public bool WithdrawGold(string ownerName, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerName) || amount <= 0m)
            return false;

        BankAccount account = GetOrCreateAccount(ownerName);
        if (account.Gold < amount)
            return false;

        account.Gold -= amount;
        return true;
    }

    public BankSaveData CreateSaveData()
    {
        BankSaveData data = new BankSaveData();

        foreach (BankAccount account in _accounts.Values)
        {
            data.Accounts.Add(new BankAccountData
            {
                OwnerName = account.OwnerName,
                Cash = account.Cash,
                Gold = account.Gold,
                CreditLimit = account.CreditLimit,
                Debt = account.Debt,
                IsSuspicious = account.IsSuspicious
            });
        }

        return data;
    }

    public void LoadFromSave(BankSaveData saveData)
    {
        _accounts.Clear();

        if (saveData == null || saveData.Accounts == null)
            return;

        foreach (BankAccountData item in saveData.Accounts)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.OwnerName))
                continue;

            _accounts[item.OwnerName] = new BankAccount
            {
                OwnerName = item.OwnerName,
                Cash = item.Cash,
                Gold = item.Gold,
                CreditLimit = item.CreditLimit,
                Debt = item.Debt,
                IsSuspicious = item.IsSuspicious
            };
        }
    }

    private BankAccount GetOrCreateAccount(string ownerName)
    {
        if (!_accounts.TryGetValue(ownerName, out BankAccount account))
        {
            account = new BankAccount { OwnerName = ownerName };
            _accounts[ownerName] = account;
        }

        return account;
    }
}

public class BankAccount
{
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Debt { get; set; }
    public bool IsSuspicious { get; set; }
}