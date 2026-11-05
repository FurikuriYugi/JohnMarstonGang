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

        NormalizeAccount(account);

        _accounts.RemoveAll(
            x => x != null && x.OwnerId == account.OwnerId);
        _accounts.Add(account);
    }

    public BankAccountData EnsureAccount(
        string ownerId,
        string ownerName = null)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

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

    public void DepositGold(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null || amount <= 0m)
            return;

        account.Gold += amount;
        account.LastTransactionUtc = DateTime.UtcNow;
    }

    public void DepositMoney(
        string ownerId,
        decimal cashAmount,
        decimal goldAmount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null ||
            cashAmount < 0m ||
            goldAmount < 0m ||
            (cashAmount == 0m && goldAmount == 0m))
        {
            return;
        }

        account.Cash += cashAmount;
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

    public decimal GetAvailableCredit(string ownerId)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null)
            return 0m;

        decimal availableCredit =
            account.CreditLimit - account.Debt;

        return availableCredit > 0m
            ? availableCredit
            : 0m;
    }

    public bool CanBorrow(string ownerId, decimal amount)
    {
        if (amount <= 0m)
            return false;

        return amount <= GetAvailableCredit(ownerId);
    }

    public bool TryBorrowCash(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null ||
            amount <= 0m ||
            amount > GetAvailableCredit(ownerId))
        {
            return false;
        }

        account.Cash += amount;
        account.Debt += amount;
        account.LastTransactionUtc = DateTime.UtcNow;
        return true;
    }

    public bool TryRepayDebt(string ownerId, decimal amount)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null ||
            amount <= 0m ||
            account.Debt <= 0m)
        {
            return false;
        }

        decimal repayment = Math.Min(amount, account.Debt);
        if (account.Cash < repayment)
            return false;

        account.Cash -= repayment;
        account.Debt -= repayment;
        account.LastTransactionUtc = DateTime.UtcNow;
        return true;
    }

    public bool TrySetCreditLimit(
        string ownerId,
        decimal creditLimit)
    {
        BankAccountData account = GetAccount(ownerId);
        if (account == null ||
            creditLimit < 0m ||
            creditLimit < account.Debt)
        {
            return false;
        }

        account.CreditLimit = creditLimit;
        account.LastTransactionUtc = DateTime.UtcNow;
        return true;
    }

    public void TransferCash(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        TryTransferCash(fromOwnerId, toOwnerId, amount);
    }

    public bool TryTransferCash(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        if (!CanTransfer(fromOwnerId, toOwnerId, amount))
            return false;

        BankAccountData fromAccount = GetAccount(fromOwnerId);
        BankAccountData toAccount = GetAccount(toOwnerId);

        if (fromAccount == null ||
            toAccount == null ||
            fromAccount.Cash < amount)
        {
            return false;
        }

        fromAccount.Cash -= amount;
        toAccount.Cash += amount;

        DateTime transactionTime = DateTime.UtcNow;
        fromAccount.LastTransactionUtc = transactionTime;
        toAccount.LastTransactionUtc = transactionTime;
        return true;
    }

    public void TransferGold(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        TryTransferGold(fromOwnerId, toOwnerId, amount);
    }

    public bool TryTransferGold(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        if (!CanTransfer(fromOwnerId, toOwnerId, amount))
            return false;

        BankAccountData fromAccount = GetAccount(fromOwnerId);
        BankAccountData toAccount = GetAccount(toOwnerId);

        if (fromAccount == null ||
            toAccount == null ||
            fromAccount.Gold < amount)
        {
            return false;
        }

        fromAccount.Gold -= amount;
        toAccount.Gold += amount;

        DateTime transactionTime = DateTime.UtcNow;
        fromAccount.LastTransactionUtc = transactionTime;
        toAccount.LastTransactionUtc = transactionTime;
        return true;
    }

    private bool CanTransfer(
        string fromOwnerId,
        string toOwnerId,
        decimal amount)
    {
        return !string.IsNullOrWhiteSpace(fromOwnerId) &&
            !string.IsNullOrWhiteSpace(toOwnerId) &&
            !string.Equals(
                fromOwnerId,
                toOwnerId,
                StringComparison.Ordinal) &&
            amount > 0m;
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
        if (string.IsNullOrWhiteSpace(json))
        {
            _accounts.Clear();
            return;
        }

        List<BankAccountData> loadedAccounts;
        try
        {
            loadedAccounts =
                JsonConvert.DeserializeObject<List<BankAccountData>>(json);
        }
        catch (JsonException)
        {
            return;
        }

        List<BankAccountData> validAccounts =
            new List<BankAccountData>();
        HashSet<string> seenOwnerIds =
            new HashSet<string>(StringComparer.Ordinal);

        if (loadedAccounts != null)
        {
            foreach (BankAccountData account in loadedAccounts)
            {
                if (account == null ||
                    string.IsNullOrWhiteSpace(account.OwnerId) ||
                    !seenOwnerIds.Add(account.OwnerId))
                {
                    continue;
                }

                NormalizeAccount(account);
                validAccounts.Add(account);
            }
        }

        _accounts.Clear();
        _accounts.AddRange(validAccounts);
    }

    private void NormalizeAccount(BankAccountData account)
    {
        if (account.Cash < 0m)
            account.Cash = 0m;

        if (account.Gold < 0m)
            account.Gold = 0m;

        if (account.CreditLimit < 0m)
            account.CreditLimit = 0m;

        if (account.Debt < 0m)
            account.Debt = 0m;
    }
}