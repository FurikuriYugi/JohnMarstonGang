using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, BankVaultRecordData> _vaultRecords = new Dictionary<string, BankVaultRecordData>(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyCollection<BankAccount> Accounts => _accounts.Values.ToList().AsReadOnly();

    public BankAccount GetOrCreateAccount(string ownerName)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            ownerName = "Unknown";

        if (_accounts.TryGetValue(ownerName, out BankAccount account))
            return account;

        account = new BankAccount(ownerName);
        _accounts[ownerName] = account;
        return account;
    }

    public void DepositCash(string ownerName, decimal amount)
    {
        if (amount <= 0m) return;
        GetOrCreateAccount(ownerName).Cash += amount;
    }

    public bool WithdrawCash(string ownerName, decimal amount)
    {
        if (amount <= 0m) return false;
        BankAccount account = GetOrCreateAccount(ownerName);
        if (account.Cash < amount) return false;
        account.Cash -= amount;
        return true;
    }

    public void DepositGold(string ownerName, decimal amount)
    {
        if (amount <= 0m) return;
        GetOrCreateAccount(ownerName).Gold += amount;
    }

    public bool WithdrawGold(string ownerName, decimal amount)
    {
        if (amount <= 0m) return false;
        BankAccount account = GetOrCreateAccount(ownerName);
        if (account.Gold < amount) return false;
        account.Gold -= amount;
        return true;
    }

    public void AddCreditLimit(string ownerName, decimal amount)
    {
        if (amount <= 0m) return;
        GetOrCreateAccount(ownerName).CreditLimit += amount;
    }

    public bool BorrowAgainstCredit(string ownerName, decimal amount)
    {
        if (amount <= 0m) return false;

        BankAccount account = GetOrCreateAccount(ownerName);
        decimal availableCredit = account.CreditLimit - account.Debt;

        if (availableCredit < amount)
            return false;

        account.Debt += amount;
        account.Cash += amount;
        return true;
    }

    public bool RepayDebt(string ownerName, decimal amount)
    {
        if (amount <= 0m) return false;

        BankAccount account = GetOrCreateAccount(ownerName);
        if (account.Cash < amount || account.Debt <= 0m)
            return false;

        decimal payment = Math.Min(amount, account.Debt);
        account.Cash -= payment;
        account.Debt -= payment;
        return true;
    }

    public void MarkSuspicious(string ownerName, bool suspicious)
    {
        GetOrCreateAccount(ownerName).IsSuspicious = suspicious;
    }

    public void ApplyBusinessFlow(string businessName, decimal cashIn, decimal cashOut, decimal goldIn, decimal goldOut)
    {
        BankAccount account = GetOrCreateAccount(businessName);
        account.Cash += cashIn;
        account.Cash -= cashOut;
        account.Gold += goldIn;
        account.Gold -= goldOut;

        if (account.Cash < 0m) account.Cash = 0m;
        if (account.Gold < 0m) account.Gold = 0m;
    }

    public void StoreVault(string bankName, decimal cash, decimal gold)
    {
        if (string.IsNullOrWhiteSpace(bankName))
            bankName = "Unknown Bank";

        if (!_vaultRecords.TryGetValue(bankName, out BankVaultRecordData record))
        {
            record = new BankVaultRecordData
            {
                BankName = bankName,
                CashStored = 0m,
                GoldStored = 0m,
                LastUpdated = DateTime.UtcNow
            };
            _vaultRecords[bankName] = record;
        }

        record.CashStored += Math.Max(0m, cash);
        record.GoldStored += Math.Max(0m, gold);
        record.LastUpdated = DateTime.UtcNow;
    }

    public bool RemoveFromVault(string bankName, decimal cash, decimal gold)
    {
        if (string.IsNullOrWhiteSpace(bankName))
            bankName = "Unknown Bank";

        if (!_vaultRecords.TryGetValue(bankName, out BankVaultRecordData record))
            return false;

        if (record.CashStored < cash || record.GoldStored < gold)
            return false;

        record.CashStored -= Math.Max(0m, cash);
        record.GoldStored -= Math.Max(0m, gold);
        record.LastUpdated = DateTime.UtcNow;
        return true;
    }

    public decimal GetVaultCash(string bankName)
    {
        if (string.IsNullOrWhiteSpace(bankName))
            bankName = "Unknown Bank";

        return _vaultRecords.TryGetValue(bankName, out BankVaultRecordData record) ? record.CashStored : 0m;
    }

    public decimal GetVaultGold(string bankName)
    {
        if (string.IsNullOrWhiteSpace(bankName))
            bankName = "Unknown Bank";

        return _vaultRecords.TryGetValue(bankName, out BankVaultRecordData record) ? record.GoldStored : 0m;
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

        foreach (BankVaultRecordData record in _vaultRecords.Values)
        {
            data.VaultRecords.Add(new BankVaultRecordData
            {
                BankName = record.BankName,
                CashStored = record.CashStored,
                GoldStored = record.GoldStored,
                LastUpdated = record.LastUpdated
            });
        }

        return data;
    }

    public void LoadFromSave(BankSaveData data)
    {
        _accounts.Clear();
        _vaultRecords.Clear();

        if (data == null)
            return;

        if (data.Accounts != null)
        {
            foreach (BankAccountData accountData in data.Accounts)
            {
                if (accountData == null)
                    continue;

                BankAccount account = new BankAccount(accountData.OwnerName)
                {
                    Cash = accountData.Cash,
                    Gold = accountData.Gold,
                    CreditLimit = accountData.CreditLimit,
                    Debt = accountData.Debt,
                    IsSuspicious = accountData.IsSuspicious
                };

                _accounts[account.OwnerName] = account;
            }
        }

        if (data.VaultRecords != null)
        {
            foreach (BankVaultRecordData record in data.VaultRecords)
            {
                if (record == null)
                    continue;

                _vaultRecords[record.BankName] = new BankVaultRecordData
                {
                    BankName = record.BankName,
                    CashStored = record.CashStored,
                    GoldStored = record.GoldStored,
                    LastUpdated = record.LastUpdated
                };
            }
        }
    }
}

public class BankAccount
{
    public string OwnerName { get; private set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Debt { get; set; }
    public bool IsSuspicious { get; set; }

    public BankAccount(string ownerName)
    {
        OwnerName = string.IsNullOrWhiteSpace(ownerName) ? "Unknown" : ownerName.Trim();
    }
}