// BankSystem.cs
using Newtonsoft.Json;
using RDR2;
using RDR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

public class BankAccount
{
    public string OwnerId { get; set; }
    public double Cash { get; set; }
    public double Gold { get; set; }
    public double CreditLimit { get; set; }
    public double Suspicion { get; set; }
}

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>();
    private const string DefaultBankId = "SaintDenisBank";

    public void Deposit(string ownerId, double cash, double gold)
    {
        BankAccount acc = GetOrCreate(ownerId);
        acc.Cash += Math.Max(0, cash);
        acc.Gold += Math.Max(0, gold);
        Save();
    }

    public bool Withdraw(string ownerId, double cash, double gold)
    {
        BankAccount acc = GetOrCreate(ownerId);
        if (acc.Cash < cash || acc.Gold < gold)
            return false;

        acc.Cash -= cash;
        acc.Gold -= gold;
        Save();
        return true;
    }

    public bool TakeLoan(string ownerId, double amount)
    {
        BankAccount acc = GetOrCreate(ownerId);
        if (amount <= 0) return false;
        if (amount > acc.CreditLimit) return false;

        acc.Cash += amount;
        acc.Suspicion += amount * 0.05;
        Save();
        return true;
    }

    public bool RobBank(string ownerId, out double cashTaken, out double goldTaken)
    {
        BankAccount acc = GetOrCreate(DefaultBankId);
        cashTaken = Math.Min(acc.Cash, 5000.0);
        goldTaken = Math.Min(acc.Gold, 50.0);

        acc.Cash -= cashTaken;
        acc.Gold -= goldTaken;
        acc.Suspicion += 25.0;
        Save();

        if (!string.IsNullOrEmpty(ownerId))
        {
            BankAccount thief = GetOrCreate(ownerId);
            thief.Cash += cashTaken;
            thief.Gold += goldTaken;
            Save();
        }

        return cashTaken > 0 || goldTaken > 0;
    }

    public void TransferBusinessRevenue(string businessId, string bankId, double cash, double gold)
    {
        Withdraw(businessId, cash, gold);
        Deposit(bankId, cash, gold);
    }

    public BankAccount GetOrCreate(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            ownerId = "Unknown";

        if (!_accounts.TryGetValue(ownerId, out BankAccount acc))
        {
            acc = new BankAccount
            {
                OwnerId = ownerId,
                Cash = 0,
                Gold = 0,
                CreditLimit = 1000,
                Suspicion = 0
            };
            _accounts[ownerId] = acc;
        }
        return acc;
    }

    public double GetBankLootAmount()
    {
        BankAccount acc = GetOrCreate(DefaultBankId);
        return acc.Cash + (acc.Gold * 150.0);
    }

    public void Save()
    {
        try
        {
            string path = "scripts\\JohnsGangBank.json";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JsonConvert.SerializeObject(_accounts, Formatting.Indented));
        }
        catch { }
    }

    public void Load()
    {
        try
        {
            string path = "scripts\\JohnsGangBank.json";
            if (!File.Exists(path)) return;
            var loaded = JsonConvert.DeserializeObject<Dictionary<string, BankAccount>>(File.ReadAllText(path));
            if (loaded != null)
            {
                _accounts.Clear();
                foreach (var kv in loaded)
                    _accounts[kv.Key] = kv.Value;
            }
        }
        catch { }
    }
}