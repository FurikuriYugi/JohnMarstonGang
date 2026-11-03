// BankSystem.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>();
    private readonly List<BankTransaction> _transactions = new List<BankTransaction>();
    private const string SaveFileName = "JohnMarstonGang_Save.xml";

    public static BankSystem Load()
    {
        var bank = new BankSystem();

        if (!File.Exists(SaveFileName))
            return bank;

        try
        {
            using (var fs = File.OpenRead(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(GangSaveData));
                var data = (GangSaveData)serializer.ReadObject(fs);

                if (data != null && data.Bank != null)
                {
                    if (data.Bank.Accounts != null)
                    {
                        foreach (var account in data.Bank.Accounts)
                        {
                            bank._accounts[account.OwnerId] = new BankAccount
                            {
                                OwnerId = account.OwnerId,
                                OwnerName = account.OwnerName,
                                Cash = account.Cash,
                                Gold = account.Gold,
                                IsBusinessAccount = account.IsBusinessAccount
                            };
                        }
                    }

                    if (data.Bank.Transactions != null)
                    {
                        foreach (var tx in data.Bank.Transactions)
                        {
                            bank._transactions.Add(new BankTransaction
                            {
                                TransactionId = tx.TransactionId,
                                FromId = tx.FromId,
                                ToId = tx.ToId,
                                CashAmount = tx.CashAmount,
                                GoldAmount = tx.GoldAmount,
                                Reason = tx.Reason
                            });
                        }
                    }
                }
            }
        }
        catch
        {
        }

        return bank;
    }

    public void Save()
    {
        try
        {
            GangSaveData data = LoadOrCreateSaveData();

            data.Bank = new BankSaveData
            {
                PlayerCash = GetCash("player"),
                PlayerGold = GetGold("player"),
                Accounts = _accounts.Values.Select(a => new BankAccountData
                {
                    OwnerId = a.OwnerId,
                    OwnerName = a.OwnerName,
                    Cash = a.Cash,
                    Gold = a.Gold,
                    IsBusinessAccount = a.IsBusinessAccount
                }).ToList(),
                Transactions = _transactions.Select(t => new BankTransactionData
                {
                    TransactionId = t.TransactionId,
                    FromId = t.FromId,
                    ToId = t.ToId,
                    CashAmount = t.CashAmount,
                    GoldAmount = t.GoldAmount,
                    Reason = t.Reason
                }).ToList()
            };

            using (var fs = File.Create(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(GangSaveData));
                serializer.WriteObject(fs, data);
            }
        }
        catch
        {
        }
    }

    private GangSaveData LoadOrCreateSaveData()
    {
        if (!File.Exists(SaveFileName))
            return new GangSaveData();

        try
        {
            using (var fs = File.OpenRead(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(GangSaveData));
                var data = (GangSaveData)serializer.ReadObject(fs);
                return data ?? new GangSaveData();
            }
        }
        catch
        {
            return new GangSaveData();
        }
    }

    public BankAccount GetOrCreateAccount(string ownerId, string ownerName = null, bool isBusinessAccount = false)
    {
        if (!_accounts.TryGetValue(ownerId, out var account))
        {
            account = new BankAccount
            {
                OwnerId = ownerId,
                OwnerName = ownerName ?? ownerId,
                IsBusinessAccount = isBusinessAccount
            };
            _accounts[ownerId] = account;
        }

        if (!string.IsNullOrEmpty(ownerName))
            account.OwnerName = ownerName;

        if (isBusinessAccount)
            account.IsBusinessAccount = true;

        return account;
    }

    public decimal GetCash(string ownerId)
    {
        return GetOrCreateAccount(ownerId).Cash;
    }

    public decimal GetGold(string ownerId)
    {
        return GetOrCreateAccount(ownerId).Gold;
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        if (amount <= 0) return;
        var account = GetOrCreateAccount(ownerId);
        account.Cash += amount;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        if (amount <= 0) return false;
        var account = GetOrCreateAccount(ownerId);
        if (account.Cash < amount) return false;
        account.Cash -= amount;
        return true;
    }

    public void DepositGold(string ownerId, decimal amount)
    {
        if (amount <= 0) return;
        var account = GetOrCreateAccount(ownerId);
        account.Gold += amount;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        if (amount <= 0) return false;
        var account = GetOrCreateAccount(ownerId);
        if (account.Gold < amount) return false;
        account.Gold -= amount;
        return true;
    }

    public bool TransferCash(string fromId, string toId, decimal amount, string reason)
    {
        if (string.IsNullOrEmpty(fromId) || string.IsNullOrEmpty(toId)) return false;
        if (!WithdrawCash(fromId, amount)) return false;

        DepositCash(toId, amount);
        AddTransaction(fromId, toId, amount, 0m, reason);
        return true;
    }

    public bool TransferGold(string fromId, string toId, decimal amount, string reason)
    {
        if (string.IsNullOrEmpty(fromId) || string.IsNullOrEmpty(toId)) return false;
        if (!WithdrawGold(fromId, amount)) return false;

        DepositGold(toId, amount);
        AddTransaction(fromId, toId, 0m, amount, reason);
        return true;
    }

    public void AddTransaction(string fromId, string toId, decimal cashAmount, decimal goldAmount, string reason)
    {
        _transactions.Add(new BankTransaction
        {
            TransactionId = Guid.NewGuid().ToString("N"),
            FromId = fromId,
            ToId = toId,
            CashAmount = cashAmount,
            GoldAmount = goldAmount,
            Reason = reason
        });
    }

    public IReadOnlyList<BankTransaction> GetTransactions()
    {
        return _transactions;
    }
}

public class BankAccount
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public bool IsBusinessAccount { get; set; }
}

public class BankTransaction
{
    public string TransactionId { get; set; }
    public string FromId { get; set; }
    public string ToId { get; set; }
    public decimal CashAmount { get; set; }
    public decimal GoldAmount { get; set; }
    public string Reason { get; set; }
}