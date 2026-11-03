using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();

    public IReadOnlyList<BankAccount> Accounts => _accounts;

    public BankAccount GetAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        return _accounts.FirstOrDefault(x => x != null && x.OwnerId == ownerId);
    }

    public BankAccount GetOrCreateAccount(string ownerId, string ownerName = null)
    {
        var account = GetAccount(ownerId);
        if (account != null)
            return account;

        account = new BankAccount
        {
            OwnerId = ownerId,
            OwnerName = string.IsNullOrWhiteSpace(ownerName) ? ownerId : ownerName,
            Cash = 0,
            Gold = 0
        };

        _accounts.Add(account);
        return account;
    }

    public void DepositCash(string ownerId, int amount, string reason = null)
    {
        if (amount <= 0)
            return;

        var account = GetOrCreateAccount(ownerId);
        account.Cash += amount;
        AddTransaction(account, amount, 0, reason ?? "Deposit Cash");
    }

    public bool WithdrawCash(string ownerId, int amount, string reason = null)
    {
        if (amount <= 0)
            return false;

        var account = GetOrCreateAccount(ownerId);
        if (account.Cash < amount)
            return false;

        account.Cash -= amount;
        AddTransaction(account, -amount, 0, reason ?? "Withdraw Cash");
        return true;
    }

    public void DepositGold(string ownerId, int amount, string reason = null)
    {
        if (amount <= 0)
            return;

        var account = GetOrCreateAccount(ownerId);
        account.Gold += amount;
        AddTransaction(account, 0, amount, reason ?? "Deposit Gold");
    }

    public bool WithdrawGold(string ownerId, int amount, string reason = null)
    {
        if (amount <= 0)
            return false;

        var account = GetOrCreateAccount(ownerId);
        if (account.Gold < amount)
            return false;

        account.Gold -= amount;
        AddTransaction(account, 0, -amount, reason ?? "Withdraw Gold");
        return true;
    }

    public void RestoreFromPersistence(IEnumerable<BankAccountData> accounts)
    {
        _accounts.Clear();

        if (accounts == null)
            return;

        foreach (var data in accounts)
        {
            if (data == null)
                continue;

            _accounts.Add(new BankAccount
            {
                OwnerId = data.OwnerId,
                OwnerName = data.OwnerName,
                Cash = data.Cash,
                Gold = data.Gold,
                Transactions = data.Transactions != null
                    ? data.Transactions.Select(x => new BankTransaction
                    {
                        TransactionId = x.TransactionId,
                        OwnerId = x.OwnerId,
                        CashDelta = x.CashDelta,
                        GoldDelta = x.GoldDelta,
                        Reason = x.Reason
                    }).ToList()
                    : new List<BankTransaction>()
            });
        }
    }

    public List<BankAccountData> BuildPersistence()
    {
        return _accounts.Select(a => new BankAccountData
        {
            OwnerId = a.OwnerId,
            OwnerName = a.OwnerName,
            Cash = a.Cash,
            Gold = a.Gold,
            Transactions = a.Transactions.Select(t => new BankTransactionData
            {
                TransactionId = t.TransactionId,
                OwnerId = t.OwnerId,
                CashDelta = t.CashDelta,
                GoldDelta = t.GoldDelta,
                Reason = t.Reason
            }).ToList()
        }).ToList();
    }

    private void AddTransaction(BankAccount account, int cashDelta, int goldDelta, string reason)
    {
        account.Transactions.Add(new BankTransaction
        {
            TransactionId = Guid.NewGuid().ToString("N"),
            OwnerId = account.OwnerId,
            CashDelta = cashDelta,
            GoldDelta = goldDelta,
            Reason = reason
        });
    }
}

public class BankAccount
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public int Cash { get; set; }
    public int Gold { get; set; }
    public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();
}

public class BankTransaction
{
    public string TransactionId { get; set; }
    public string OwnerId { get; set; }
    public int CashDelta { get; set; }
    public int GoldDelta { get; set; }
    public string Reason { get; set; }
}