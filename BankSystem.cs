// BankSystem.cs
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>();
    private const string SaveFileName = "JohnMarstonGang_Bank_Save.xml";

    public static BankSystem Load()
    {
        var bank = new BankSystem();

        if (!File.Exists(SaveFileName))
            return bank;

        try
        {
            using (var fs = File.OpenRead(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(BankSaveData));
                var data = (BankSaveData)serializer.ReadObject(fs);

                if (data != null && data.Accounts != null)
                {
                    foreach (var account in data.Accounts)
                    {
                        bank._accounts[account.OwnerId] = new BankAccount
                        {
                            OwnerId = account.OwnerId,
                            Cash = account.Cash,
                            Gold = account.Gold
                        };
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
            var data = new BankSaveData
            {
                Accounts = _accounts.Values.Select(a => new BankAccountData
                {
                    OwnerId = a.OwnerId,
                    Cash = a.Cash,
                    Gold = a.Gold
                }).ToList()
            };

            using (var fs = File.Create(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(BankSaveData));
                serializer.WriteObject(fs, data);
            }
        }
        catch
        {
        }
    }

    public BankAccount GetOrCreateAccount(string ownerId)
    {
        if (!_accounts.TryGetValue(ownerId, out var account))
        {
            account = new BankAccount { OwnerId = ownerId };
            _accounts[ownerId] = account;
        }

        return account;
    }

    public void DepositCash(string ownerId, decimal amount)
    {
        var account = GetOrCreateAccount(ownerId);
        account.Cash += amount;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        var account = GetOrCreateAccount(ownerId);
        if (account.Cash < amount)
            return false;

        account.Cash -= amount;
        return true;
    }

    public void DepositGold(string ownerId, decimal amount)
    {
        var account = GetOrCreateAccount(ownerId);
        account.Gold += amount;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        var account = GetOrCreateAccount(ownerId);
        if (account.Gold < amount)
            return false;

        account.Gold -= amount;
        return true;
    }
}

public class BankAccount
{
    public string OwnerId { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
}