using System;
using System.Collections.Generic;
using System.Linq;

public class BankSystem
{
    private readonly Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();
    private readonly List<BankTransaction> ledger = new List<BankTransaction>();

    public event Action<BankTransaction> OnTransactionRecorded;
    public event Action<BankAccount, BankTransaction> OnVaultRobbed;
    public event Action<BankAccount> OnSuspicionChanged;

    public BankAccount GetOrCreatePlayerAccount(string playerId)
    {
        return GetOrCreateAccount("player:" + playerId, playerId, BankAccountType.Player);
    }

    public BankAccount GetOrCreateGangAccount(string gangId)
    {
        return GetOrCreateAccount("gang:" + gangId, gangId, BankAccountType.Gang);
    }

    public BankAccount GetOrCreateNPCAccount(string npcId)
    {
        return GetOrCreateAccount("npc:" + npcId, npcId, BankAccountType.NPC);
    }

    public BankAccount GetOrCreateBusinessAccount(string businessId)
    {
        return GetOrCreateAccount("business:" + businessId, businessId, BankAccountType.Business);
    }

    public BankAccount GetOrCreateVaultAccount(string bankId)
    {
        return GetOrCreateAccount("vault:" + bankId, bankId, BankAccountType.Vault);
    }

    public BankAccount GetAccount(string accountId)
    {
        BankAccount account;
        accounts.TryGetValue(accountId, out account);
        return account;
    }

    public bool Deposit(string accountId, int cash, int gold = 0, string reason = null, string sourceAccountId = null)
    {
        if (cash < 0 || gold < 0) return false;

        BankAccount account = GetAccount(accountId);
        if (account == null || account.Frozen) return false;

        account.Cash += cash;
        account.Gold += gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = sourceAccountId,
            ToAccountId = accountId,
            Type = BankTransactionType.Deposit,
            Cash = cash,
            Gold = gold,
            Reason = reason ?? "Deposit"
        });

        return true;
    }

    public bool Withdraw(string accountId, int cash, int gold = 0, string reason = null, string targetAccountId = null)
    {
        if (cash < 0 || gold < 0) return false;

        BankAccount account = GetAccount(accountId);
        if (account == null || account.Frozen) return false;
        if (!CanAfford(account, cash, gold)) return false;

        account.Cash -= cash;
        account.Gold -= gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = accountId,
            ToAccountId = targetAccountId,
            Type = BankTransactionType.Withdrawal,
            Cash = cash,
            Gold = gold,
            Reason = reason ?? "Withdrawal"
        });

        return true;
    }

    public bool Transfer(string fromAccountId, string toAccountId, int cash, int gold = 0, string reason = null)
    {
        if (cash < 0 || gold < 0) return false;
        if (fromAccountId == toAccountId) return false;

        BankAccount from = GetAccount(fromAccountId);
        BankAccount to = GetAccount(toAccountId);

        if (from == null || to == null) return false;
        if (from.Frozen || to.Frozen) return false;
        if (!CanAfford(from, cash, gold)) return false;

        from.Cash -= cash;
        from.Gold -= gold;

        to.Cash += cash;
        to.Gold += gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Type = BankTransactionType.Transfer,
            Cash = cash,
            Gold = gold,
            Reason = reason ?? "Transfer"
        });

        return true;
    }

    public bool PayGangMember(string gangId, string memberId, int cash, int gold = 0)
    {
        BankAccount gang = GetOrCreateGangAccount(gangId);
        BankAccount member = GetOrCreateNPCAccount(memberId);

        if (!CanAfford(gang, cash, gold)) return false;

        gang.Cash -= cash;
        gang.Gold -= gold;

        member.Cash += cash;
        member.Gold += gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = gang.AccountId,
            ToAccountId = member.AccountId,
            Type = BankTransactionType.Payroll,
            Cash = cash,
            Gold = gold,
            Reason = "Gang payroll",
            RelatedEntityId = memberId
        });

        return true;
    }

    public void AddBusinessIncome(string businessId, int cash, int gold = 0)
    {
        BankAccount business = GetOrCreateBusinessAccount(businessId);
        business.Cash += cash;
        business.Gold += gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = null,
            ToAccountId = business.AccountId,
            Type = BankTransactionType.Income,
            Cash = cash,
            Gold = gold,
            Reason = "Business income",
            RelatedEntityId = businessId
        });
    }

    public void StoreBusinessMoneyInVault(string businessId, string bankId, float ratio)
    {
        if (ratio < 0f) ratio = 0f;
        if (ratio > 1f) ratio = 1f;

        BankAccount business = GetOrCreateBusinessAccount(businessId);
        BankAccount vault = GetOrCreateVaultAccount(bankId);

        int cash = (int)(business.Cash * ratio);
        int gold = (int)(business.Gold * ratio);

        if (cash <= 0 && gold <= 0) return;

        business.Cash -= cash;
        business.Gold -= gold;
        vault.Cash += cash;
        vault.Gold += gold;

        RecordTransaction(new BankTransaction
        {
            FromAccountId = business.AccountId,
            ToAccountId = vault.AccountId,
            Type = BankTransactionType.Transfer,
            Cash = cash,
            Gold = gold,
            Reason = "Business vault deposit",
            RelatedEntityId = businessId
        });
    }

    public bool RobVault(string bankId, int cash, int gold = 0, string robberId = null)
    {
        BankAccount vault = GetOrCreateVaultAccount(bankId);
        if (vault.Frozen) return false;
        if (!CanAfford(vault, cash, gold)) return false;

        vault.Cash -= cash;
        vault.Gold -= gold;

        IncreaseSuspicion(vault.AccountId, 0.35f);

        BankTransaction tx = new BankTransaction
        {
            FromAccountId = vault.AccountId,
            ToAccountId = robberId,
            Type = BankTransactionType.Robbery,
            Cash = cash,
            Gold = gold,
            Reason = "Vault robbery",
            RelatedEntityId = robberId
        };

        RecordTransaction(tx);
        OnVaultRobbed?.Invoke(vault, tx);

        return true;
    }

    public void IncreaseSuspicion(string accountId, float amount)
    {
        BankAccount account = GetAccount(accountId);
        if (account == null) return;

        account.Suspicion += amount;
        if (account.Suspicion > 1f) account.Suspicion = 1f;

        OnSuspicionChanged?.Invoke(account);
    }

    public void DecreaseSuspicion(string accountId, float amount)
    {
        BankAccount account = GetAccount(accountId);
        if (account == null) return;

        account.Suspicion -= amount;
        if (account.Suspicion < 0f) account.Suspicion = 0f;

        OnSuspicionChanged?.Invoke(account);
    }

    public void FreezeAccount(string accountId, bool frozen = true)
    {
        BankAccount account = GetAccount(accountId);
        if (account != null)
        {
            account.Frozen = frozen;
        }
    }

    public int GetVaultCash(string bankId)
    {
        return GetOrCreateVaultAccount(bankId).Cash;
    }

    public int GetVaultGold(string bankId)
    {
        return GetOrCreateVaultAccount(bankId).Gold;
    }

    public IReadOnlyList<BankTransaction> GetLedger()
    {
        return ledger;
    }

    public IReadOnlyList<BankTransaction> GetAccountTransactions(string accountId)
    {
        BankAccount account = GetAccount(accountId);
        if (account != null)
        {
            return account.Transactions;
        }
        else
        {
            return Array.Empty<BankTransaction>();
        }
    }

    public BankSystemData SaveData()
    {
        return new BankSystemData
        {
            Accounts = accounts.Values.ToList(),
            Ledger = ledger.ToList()
        };
    }

    public void LoadData(BankSystemData data)
    {
        accounts.Clear();
        ledger.Clear();

        if (data == null) return;

        if (data.Accounts != null)
        {
            foreach (BankAccount account in data.Accounts)
            {
                accounts[account.AccountId] = account;
            }
        }

        if (data.Ledger != null)
        {
            ledger.AddRange(data.Ledger);
        }
    }

    private BankAccount GetOrCreateAccount(string accountId, string ownerId, BankAccountType accountType)
    {
        BankAccount existing;
        if (accounts.TryGetValue(accountId, out existing))
        {
            return existing;
        }

        BankAccount created = new BankAccount(accountId, ownerId, accountType);
        accounts[accountId] = created;
        return created;
    }

    private bool CanAfford(BankAccount account, int cash, int gold)
    {
        return account.Cash >= cash && account.Gold >= gold;
    }

    private void RecordTransaction(BankTransaction transaction)
    {
        ledger.Add(transaction);

        if (!string.IsNullOrEmpty(transaction.FromAccountId))
        {
            BankAccount from;
            if (accounts.TryGetValue(transaction.FromAccountId, out from))
            {
                from.Transactions.Add(transaction);
            }
        }

        if (!string.IsNullOrEmpty(transaction.ToAccountId))
        {
            BankAccount to;
            if (accounts.TryGetValue(transaction.ToAccountId, out to))
            {
                to.Transactions.Add(transaction);
            }
        }

        OnTransactionRecorded?.Invoke(transaction);
    }
}

public enum BankAccountType
{
    Player,
    Gang,
    NPC,
    Business,
    Vault
}

public enum BankTransactionType
{
    Deposit,
    Withdrawal,
    Transfer,
    Robbery,
    Income,
    Payroll,
    Interest,
    Fee
}

public class BankTransaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TimeUtc { get; set; } = DateTime.UtcNow;

    public string FromAccountId { get; set; }
    public string ToAccountId { get; set; }

    public BankTransactionType Type { get; set; }
    public int Cash { get; set; }
    public int Gold { get; set; }

    public string Reason { get; set; }
    public string RelatedEntityId { get; set; }
}

public class BankAccount
{
    public string AccountId { get; set; }
    public string OwnerId { get; set; }
    public BankAccountType AccountType { get; set; }

    public int Cash { get; set; }
    public int Gold { get; set; }

    public int CreditLimit { get; set; }
    public int Debt { get; set; }

    public float Suspicion { get; set; }
    public bool Frozen { get; set; }

    public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();

    public BankAccount() { }

    public BankAccount(string accountId, string ownerId, BankAccountType accountType)
    {
        AccountId = accountId;
        OwnerId = ownerId;
        AccountType = accountType;
    }
}