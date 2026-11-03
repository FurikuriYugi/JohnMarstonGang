// BankSystem.cs
using System;
using System.Collections.Generic;

public class BankSystem
{
    private readonly Dictionary<string, decimal> _cashAccounts = new Dictionary<string, decimal>();
    private readonly Dictionary<string, decimal> _goldAccounts = new Dictionary<string, decimal>();

    public decimal VaultCash { get; private set; }
    public decimal VaultGold { get; private set; }

    public void DepositCash(string ownerId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerId) || amount <= 0m) return;

        if (!_cashAccounts.ContainsKey(ownerId))
            _cashAccounts[ownerId] = 0m;

        _cashAccounts[ownerId] += amount;
        VaultCash += amount;
    }

    public bool WithdrawCash(string ownerId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerId) || amount <= 0m) return false;

        if (!_cashAccounts.ContainsKey(ownerId))
            _cashAccounts[ownerId] = 0m;

        if (_cashAccounts[ownerId] < amount)
            return false;

        _cashAccounts[ownerId] -= amount;
        VaultCash -= amount;
        if (VaultCash < 0m) VaultCash = 0m;
        return true;
    }

    public void DepositGold(string ownerId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerId) || amount <= 0m) return;

        if (!_goldAccounts.ContainsKey(ownerId))
            _goldAccounts[ownerId] = 0m;

        _goldAccounts[ownerId] += amount;
        VaultGold += amount;
    }

    public bool WithdrawGold(string ownerId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(ownerId) || amount <= 0m) return false;

        if (!_goldAccounts.ContainsKey(ownerId))
            _goldAccounts[ownerId] = 0m;

        if (_goldAccounts[ownerId] < amount)
            return false;

        _goldAccounts[ownerId] -= amount;
        VaultGold -= amount;
        if (VaultGold < 0m) VaultGold = 0m;
        return true;
    }

    public decimal GetCashBalance(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId)) return 0m;
        return _cashAccounts.ContainsKey(ownerId) ? _cashAccounts[ownerId] : 0m;
    }

    public decimal GetGoldBalance(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId)) return 0m;
        return _goldAccounts.ContainsKey(ownerId) ? _goldAccounts[ownerId] : 0m;
    }

    public bool TransferCash(string fromOwnerId, string toOwnerId, decimal amount)
    {
        if (!WithdrawCash(fromOwnerId, amount)) return false;
        DepositCash(toOwnerId, amount);
        return true;
    }

    public bool TransferGold(string fromOwnerId, string toOwnerId, decimal amount)
    {
        if (!WithdrawGold(fromOwnerId, amount)) return false;
        DepositGold(toOwnerId, amount);
        return true;
    }

    public BankSaveData CreateSaveData()
    {
        return new BankSaveData
        {
            VaultCash = VaultCash,
            VaultGold = VaultGold,
            CashAccounts = new Dictionary<string, decimal>(_cashAccounts),
            GoldAccounts = new Dictionary<string, decimal>(_goldAccounts)
        };
    }

    public void LoadFromSave(BankSaveData data)
    {
        _cashAccounts.Clear();
        _goldAccounts.Clear();

        if (data == null)
            return;

        VaultCash = data.VaultCash;
        VaultGold = data.VaultGold;

        if (data.CashAccounts != null)
        {
            foreach (var pair in data.CashAccounts)
                _cashAccounts[pair.Key] = pair.Value;
        }

        if (data.GoldAccounts != null)
        {
            foreach (var pair in data.GoldAccounts)
                _goldAccounts[pair.Key] = pair.Value;
        }
    }
}