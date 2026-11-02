using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampTradeSystem
{
    private readonly string _savePath;
    private readonly List<CampTradeRecord> _trades = new List<CampTradeRecord>();

    public int MaxTrades { get; set; } = 128;
    public bool Enabled { get; set; } = true;

    public IReadOnlyList<CampTradeRecord> Trades => _trades;

    public CampTradeSystem(string saveFileName = "CampTradeSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool RegisterTrade(string partner, string itemName, int quantity, int unitPrice, string notes = null)
    {
        if (string.IsNullOrWhiteSpace(partner) || string.IsNullOrWhiteSpace(itemName))
            return false;

        if (quantity <= 0 || unitPrice < 0)
            return false;

        if (_trades.Count >= MaxTrades)
            return false;

        _trades.Add(new CampTradeRecord
        {
            TradeId = Guid.NewGuid().ToString("N"),
            Partner = partner,
            ItemName = itemName,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalPrice = quantity * unitPrice,
            Notes = notes,
            TimestampUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool RemoveTrade(string tradeId)
    {
        if (string.IsNullOrWhiteSpace(tradeId))
            return false;

        bool removed = _trades.RemoveAll(x => x.TradeId == tradeId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampTradeRecord GetTrade(string tradeId)
    {
        if (string.IsNullOrWhiteSpace(tradeId))
            return null;

        return _trades.FirstOrDefault(x => x.TradeId == tradeId);
    }

    public IEnumerable<CampTradeRecord> GetTradesByPartner(string partner)
    {
        if (string.IsNullOrWhiteSpace(partner))
            return _trades;

        return _trades.Where(x => string.Equals(x.Partner, partner, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CampTradeRecord> GetTradesByItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            return _trades;

        return _trades.Where(x => string.Equals(x.ItemName, itemName, StringComparison.OrdinalIgnoreCase));
    }

    public int GetTotalTradeValue()
    {
        return _trades.Sum(x => x.TotalPrice);
    }

    public int GetTradeCount()
    {
        return _trades.Count;
    }

    public void Clear()
    {
        _trades.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_trades, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<CampTradeRecord>>(json);

            _trades.Clear();
            if (loaded != null)
                _trades.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.TradeId)));
        }
        catch
        {
            _trades.Clear();
        }
    }
}

public class CampTradeRecord
{
    public string TradeId { get; set; }
    public string Partner { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
    public int TotalPrice { get; set; }
    public string Notes { get; set; }
    public DateTime TimestampUtc { get; set; }
}