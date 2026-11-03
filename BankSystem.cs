using System;
using System.Collections.Generic;

public class BankSystem
{
    private decimal _vaultCash;
    private decimal _vaultGold;

    private readonly Dictionary<string, decimal> _cashAccounts = new Dictionary<string, decimal>();
    private readonly Dictionary<string, decimal> _goldAccounts = new Dictionary<string, decimal>();

    public decimal VaultCash => _vaultCash;
    public decimal VaultGold => _vaultGold;

    public decimal GetCashBalance(string accountId)
    {
        if (string.IsNullOrWhiteSpace(accountId))
            return 0m;

        return _cashAccounts.TryGetValue(accountId, out var balance) ? balance : 0m;
    }

    public decimal GetGoldBalance(string accountId)
    {
        if (string.IsNullOrWhiteSpace(accountId))
            return 0m;

        return _goldAccounts.TryGetValue(accountId, out var balance) ? balance : 0m;
    }

    public void DepositCash(string accountId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(accountId) || amount <= 0m)
            return;

        if (!_cashAccounts.ContainsKey(accountId))
            _cashAccounts[accountId] = 0m;

        _cashAccounts[accountId] += amount;
        _vaultCash += amount;
    }

    public void DepositGold(string accountId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(accountId) || amount <= 0m)
            return;

        if (!_goldAccounts.ContainsKey(accountId))
            _goldAccounts[accountId] = 0m;

        _goldAccounts[accountId] += amount;
        _vaultGold += amount;
    }

    public bool WithdrawCash(string accountId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(accountId) || amount <= 0m)
            return false;

        var current = GetCashBalance(accountId);
        if (current < amount)
            return false;

        _cashAccounts[accountId] = current - amount;
        _vaultCash -= amount;
        if (_vaultCash < 0m) _vaultCash = 0m;
        return true;
    }

    public bool WithdrawGold(string accountId, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(accountId) || amount <= 0m)
            return false;

        var current = GetGoldBalance(accountId);
        if (current < amount)
            return false;

        _goldAccounts[accountId] = current - amount;
        _vaultGold -= amount;
        if (_vaultGold < 0m) _vaultGold = 0m;
        return true;
    }

    public void AddVaultCash(decimal amount)
    {
        if (amount <= 0m)
            return;

        _vaultCash += amount;
    }

    public void AddVaultGold(decimal amount)
    {
        if (amount <= 0m)
            return;

        _vaultGold += amount;
    }

    public BankSaveData CreateSaveData()
    {
        return new BankSaveData
        {
            VaultCash = _vaultCash,
            VaultGold = _vaultGold,
            CashAccounts = new Dictionary<string, decimal>(_cashAccounts),
            GoldAccounts = new Dictionary<string, decimal>(_goldAccounts)
        };
    }

    public void LoadFromSave(BankSaveData data)
    {
        _cashAccounts.Clear();
        _goldAccounts.Clear();
        _vaultCash = 0m;
        _vaultGold = 0m;

        if (data == null)
            return;

        _vaultCash = data.VaultCash;
        _vaultGold = data.VaultGold;

        if (data.CashAccounts != null)
        {
            foreach (var kvp in data.CashAccounts)
                _cashAccounts[kvp.Key] = kvp.Value;
        }

        if (data.GoldAccounts != null)
        {
            foreach (var kvp in data.GoldAccounts)
                _goldAccounts[kvp.Key] = kvp.Value;
        }
    }
}