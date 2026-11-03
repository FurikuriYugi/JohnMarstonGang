using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();

    public decimal VaultCash { get; private set; }
    public decimal VaultGold { get; private set; }

    public IReadOnlyList<BankAccount> Accounts => _accounts;

    public BankAccount GetOrCreateAccount(string ownerId, string ownerName)
    {
        var account = _accounts.FirstOrDefault(x => x.OwnerId == ownerId);
        if (account != null)
            return account;

        account = new BankAccount
        {
            OwnerId = ownerId,
            OwnerName = ownerName
        };

        _accounts.Add(account);
        return account;
    }

    public void Deposit(string ownerId, string ownerName, decimal cash, decimal gold)
    {
        var account = GetOrCreateAccount(ownerId, ownerName);
        if (cash > 0)
        {
            account.Cash += cash;
            VaultCash += cash;
        }

        if (gold > 0)
        {
            account.Gold += gold;
            VaultGold += gold;
        }

        account.Reputation += 1;
        account.Suspicion = Math.Max(0, account.Suspicion - 1);
    }

    public bool Withdraw(string ownerId, decimal cash, decimal gold)
    {
        var account = _accounts.FirstOrDefault(x => x.OwnerId == ownerId);
        if (account == null)
            return false;

        if (account.Cash < cash || account.Gold < gold)
            return false;

        account.Cash -= cash;
        account.Gold -= gold;

        VaultCash = Math.Max(0, VaultCash - cash);
        VaultGold = Math.Max(0, VaultGold - gold);

        return true;
    }

    public void RegisterRobbery(decimal cashTaken, decimal goldTaken)
    {
        VaultCash = Math.Max(0, VaultCash - cashTaken);
        VaultGold = Math.Max(0, VaultGold - goldTaken);

        foreach (var account in _accounts)
        {
            account.Suspicion += 3;
        }
    }

    public decimal GetTotalStoredCash()
    {
        return _accounts.Sum(x => x.Cash);
    }

    public decimal GetTotalStoredGold()
    {
        return _accounts.Sum(x => x.Gold);
    }

    public BankSaveData ToSaveData()
    {
        return new BankSaveData
        {
            VaultCash = VaultCash,
            VaultGold = VaultGold,
            Accounts = _accounts.Select(x => x.ToSaveData()).ToList()
        };
    }

    public void LoadFromSaveData(BankSaveData data)
    {
        _accounts.Clear();

        if (data == null)
            return;

        VaultCash = data.VaultCash;
        VaultGold = data.VaultGold;

        foreach (var saved in data.Accounts)
        {
            _accounts.Add(BankAccount.FromSaveData(saved));
        }
    }
}