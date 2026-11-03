using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    private readonly List<BankVault> _vaults = new List<BankVault>();
    private readonly List<BusinessLedger> _businessLedgers = new List<BusinessLedger>();

    public List<BankAccount> Accounts => _accounts;
    public List<BankVault> Vaults => _vaults;
    public List<BusinessLedger> BusinessLedgers => _businessLedgers;

    public BankAccount GetOrCreateAccount(string ownerId)
    {
        BankAccount account = _accounts.FirstOrDefault(a => a.OwnerId == ownerId);
        if (account != null)
            return account;

        account = new BankAccount
        {
            OwnerId = ownerId,
            Cash = 0,
            Gold = 0,
            Trust = 50,
            Suspicion = 0
        };

        _accounts.Add(account);
        return account;
    }

    public BankVault GetOrCreateVault(string bankName)
    {
        BankVault vault = _vaults.FirstOrDefault(v => v.BankName == bankName);
        if (vault != null)
            return vault;

        vault = new BankVault
        {
            BankName = bankName,
            StoredCash = 0,
            StoredGold = 0,
            RecentWithdrawals = 0
        };

        _vaults.Add(vault);
        return vault;
    }

    public BusinessLedger GetOrCreateBusinessLedger(string ownerId, string businessName)
    {
        BusinessLedger ledger = _businessLedgers.FirstOrDefault(b => b.OwnerId == ownerId && b.BusinessName == businessName);
        if (ledger != null)
            return ledger;

        ledger = new BusinessLedger
        {
            OwnerId = ownerId,
            BusinessName = businessName,
            CashOnHand = 0,
            GoldOnHand = 0
        };

        _businessLedgers.Add(ledger);
        return ledger;
    }

    public void DepositToAccount(string ownerId, int cash, int gold)
    {
        BankAccount account = GetOrCreateAccount(ownerId);
        account.Cash += cash;
        account.Gold += gold;
        account.Trust += 1;
    }

    public bool WithdrawFromAccount(string ownerId, int cash, int gold)
    {
        BankAccount account = GetOrCreateAccount(ownerId);
        if (account.Cash < cash || account.Gold < gold)
            return false;

        account.Cash -= cash;
        account.Gold -= gold;
        account.Suspicion += 1;
        return true;
    }

    public void DepositToVault(string bankName, int cash, int gold)
    {
        BankVault vault = GetOrCreateVault(bankName);
        vault.StoredCash += cash;
        vault.StoredGold += gold;
    }

    public bool WithdrawFromVault(string bankName, int cash, int gold)
    {
        BankVault vault = GetOrCreateVault(bankName);
        if (vault.StoredCash < cash || vault.StoredGold < gold)
            return false;

        vault.StoredCash -= cash;
        vault.StoredGold -= gold;
        vault.RecentWithdrawals += 1;
        return true;
    }

    public void AddBusinessIncome(string ownerId, string businessName, int cash, int gold)
    {
        BusinessLedger ledger = GetOrCreateBusinessLedger(ownerId, businessName);
        ledger.CashOnHand += cash;
        ledger.GoldOnHand += gold;
    }

    public void TransferBusinessToBank(string ownerId, string businessName, string bankName, int cash, int gold)
    {
        BusinessLedger ledger = GetOrCreateBusinessLedger(ownerId, businessName);
        BankVault vault = GetOrCreateVault(bankName);

        if (ledger.CashOnHand < cash || ledger.GoldOnHand < gold)
            return;

        ledger.CashOnHand -= cash;
        ledger.GoldOnHand -= gold;
        vault.StoredCash += cash;
        vault.StoredGold += gold;
    }

    public int GetSuspicionLevel(string ownerId)
    {
        BankAccount account = GetOrCreateAccount(ownerId);
        return account.Suspicion;
    }

    public int GetTrustLevel(string ownerId)
    {
        BankAccount account = GetOrCreateAccount(ownerId);
        return account.Trust;
    }

    public BankSaveData CreateSaveData()
    {
        return new BankSaveData
        {
            Accounts = _accounts.Select(a => new BankAccountSaveData
            {
                OwnerId = a.OwnerId,
                Cash = a.Cash,
                Gold = a.Gold,
                Trust = a.Trust,
                Suspicion = a.Suspicion
            }).ToList(),
            Vaults = _vaults.Select(v => new BankVaultSaveData
            {
                BankName = v.BankName,
                StoredCash = v.StoredCash,
                StoredGold = v.StoredGold,
                RecentWithdrawals = v.RecentWithdrawals
            }).ToList(),
            BusinessLedgers = _businessLedgers.Select(b => new BusinessLedgerSaveData
            {
                OwnerId = b.OwnerId,
                BusinessName = b.BusinessName,
                CashOnHand = b.CashOnHand,
                GoldOnHand = b.GoldOnHand
            }).ToList()
        };
    }

    public void LoadFromSave(BankSaveData data)
    {
        _accounts.Clear();
        _vaults.Clear();
        _businessLedgers.Clear();

        if (data == null)
            return;

        if (data.Accounts != null)
        {
            foreach (BankAccountSaveData accountData in data.Accounts)
            {
                _accounts.Add(new BankAccount
                {
                    OwnerId = accountData.OwnerId,
                    Cash = accountData.Cash,
                    Gold = accountData.Gold,
                    Trust = accountData.Trust,
                    Suspicion = accountData.Suspicion
                });
            }
        }

        if (data.Vaults != null)
        {
            foreach (BankVaultSaveData vaultData in data.Vaults)
            {
                _vaults.Add(new BankVault
                {
                    BankName = vaultData.BankName,
                    StoredCash = vaultData.StoredCash,
                    StoredGold = vaultData.StoredGold,
                    RecentWithdrawals = vaultData.RecentWithdrawals
                });
            }
        }

        if (data.BusinessLedgers != null)
        {
            foreach (BusinessLedgerSaveData ledgerData in data.BusinessLedgers)
            {
                _businessLedgers.Add(new BusinessLedger
                {
                    OwnerId = ledgerData.OwnerId,
                    BusinessName = ledgerData.BusinessName,
                    CashOnHand = ledgerData.CashOnHand,
                    GoldOnHand = ledgerData.GoldOnHand
                });
            }
        }
    }
}

public class BankAccount
{
    public string OwnerId { get; set; }
    public int Cash { get; set; }
    public int Gold { get; set; }
    public int Trust { get; set; }
    public int Suspicion { get; set; }
}

public class BankVault
{
    public string BankName { get; set; }
    public int StoredCash { get; set; }
    public int StoredGold { get; set; }
    public int RecentWithdrawals { get; set; }
}

public class BusinessLedger
{
    public string OwnerId { get; set; }
    public string BusinessName { get; set; }
    public int CashOnHand { get; set; }
    public int GoldOnHand { get; set; }
}