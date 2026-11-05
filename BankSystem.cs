using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BankSystem
{
    private readonly List<BankAccountData> _accounts =
        new List<BankAccountData>();

    public List<BankAccountData> GetAllAccounts()
    {
        return new List<BankAccountData>(_accounts);
    }

    public BankAccountData GetAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        return _accounts.Find(
            x => x != null && x.OwnerId == ownerId);
    }

    public void RegisterAccount(BankAccountData account)
    {
        if (account == null ||
            string.IsNullOrWhiteSpace(account.OwnerId))
        {
            return;
        }

        _accounts.RemoveAll(
            x => x != null && x.OwnerId == account.OwnerId);

        _accounts.Add(account);
    }

    public BankAccountData EnsureAccount(
        string ownerId,
        string ownerName = null)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account != null)
            return account;

        account = new BankAccountData
        {
            OwnerId = ownerId,
            OwnerName = ownerName,
            LastTransactionUtc = DateTime.UtcNow
        };

        RegisterAccount(account);
        return account;
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null || amount <= 0m)
            return;

        account.Cash += amount;
        account.LastTransactionUtc = DateTime.UtcNow;
    }

    // Gold-only deposit. Used by TransferGold.
    public void DepositGold(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null || amount <= 0m)
            return;

        account.Gold += amount;
        account.LastTransactionUtc = DateTime.UtcNow;
    }

    // Retains the existing CoreAISystem call: cash share first, gold share second.
    public void DepositGold(
        string ownerId,
        decimal cashAmount,
        decimal goldAmount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null ||
            (cashAmount <= 0m && goldAmount <= 0m))
        {
            return;
        }

        if (cashAmount > 0m)
            account.Cash += cashAmount;

        if (goldAmount > 0m)
            account.Gold += goldAmount;

        account.LastTransactionUtc = DateTime.UtcNow;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null ||
            amount <= 0m ||
            account.Cash < amount)
        {
            return false;
        }

        account.Cash -= amount;
        account.LastTransactionUtc = DateTime.UtcNow;
        return true;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null ||
            amount <= 0m ||
            account.Gold < amount)
        {
            return false;
        }

        account.Gold -= amount;
        account.LastTransactionUtc = DateTime.UtcNow;
        return true;
    }

    public void TransferCash(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        if (WithdrawCash(fromOwnerId, amount))
            DepositCash(toOwnerId, amount);
    }

    public void TransferGold(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        if (WithdrawGold(fromOwnerId, amount))
            DepositGold(toOwnerId, amount);
    }

    public void AddDebt(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null || amount <= 0m)
            return;

        account.Debt += amount;
        account.LastTransactionUtc = DateTime.UtcNow;
    }

    public void ApplySuspicion(string ownerId, bool suspicious)
    {
        BankAccountData account = GetAccount(ownerId);

        if (account == null)
            return;

        account.IsSuspicious = suspicious;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            _accounts,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _accounts.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<BankAccountData> accounts =
            JsonConvert.DeserializeObject<List<BankAccountData>>(json);

        if (accounts != null)
            _accounts.AddRange(accounts);
    }
}