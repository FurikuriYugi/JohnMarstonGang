using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();

    public decimal VaultCash { get; set; }
    public decimal VaultGold { get; set; }

    public List<BankAccount> Accounts
    {
        get { return _accounts; }
    }

    public BankAccount GetOrCreateAccount(string ownerId, string ownerName)
    {
        BankAccount account = _accounts.FirstOrDefault(x => x.OwnerId == ownerId);
        if (account != null)
            return account;

        account = new BankAccount
        {
            OwnerId = ownerId,
            OwnerName = ownerName,
            Cash = 0m,
            Gold = 0m,
            Suspicion = 0,
            Reputation = 0
        };

        _accounts.Add(account);
        return account;
    }

    public void Deposit(string ownerId, string ownerName, decimal cash, decimal gold)
    {
        BankAccount account = GetOrCreateAccount(ownerId, ownerName);
        account.Cash += cash;
        account.Gold += gold;
    }

    public bool Withdraw(string ownerId, decimal cash, decimal gold)
    {
        BankAccount account = _accounts.FirstOrDefault(x => x.OwnerId == ownerId);
        if (account == null)
            return false;

        if (account.Cash < cash || account.Gold < gold)
            return false;

        account.Cash -= cash;
        account.Gold -= gold;
        return true;
    }

    public void TransferToVault(decimal cash, decimal gold)
    {
        VaultCash += cash;
        VaultGold += gold;
    }

    public bool TakeFromVault(decimal cash, decimal gold)
    {
        if (VaultCash < cash || VaultGold < gold)
            return false;

        VaultCash -= cash;
        VaultGold -= gold;
        return true;
    }

    public void IncreaseSuspicion(string ownerId, int amount)
    {
        BankAccount account = _accounts.FirstOrDefault(x => x.OwnerId == ownerId);
        if (account == null)
            return;

        account.Suspicion += amount;
        if (account.Suspicion < 0)
            account.Suspicion = 0;
    }

    public List<BankAccountSaveData> CreateSaveData()
    {
        List<BankAccountSaveData> saveData = new List<BankAccountSaveData>();

        foreach (BankAccount account in _accounts)
        {
            saveData.Add(new BankAccountSaveData
            {
                OwnerId = account.OwnerId,
                OwnerName = account.OwnerName,
                Cash = account.Cash,
                Gold = account.Gold,
                Suspicion = account.Suspicion,
                Reputation = account.Reputation
            });
        }

        return saveData;
    }

    public void LoadFromSave(List<BankAccountSaveData> saveData)
    {
        _accounts.Clear();

        if (saveData == null)
            return;

        foreach (BankAccountSaveData accountData in saveData)
        {
            _accounts.Add(new BankAccount
            {
                OwnerId = accountData.OwnerId,
                OwnerName = accountData.OwnerName,
                Cash = accountData.Cash,
                Gold = accountData.Gold,
                Suspicion = accountData.Suspicion,
                Reputation = accountData.Reputation
            });
        }
    }
}

public class BankAccount
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public int Suspicion { get; set; }
    public int Reputation { get; set; }
}