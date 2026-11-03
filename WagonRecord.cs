using System;
using System.Collections.Generic;

public class WagonRecord
{
    public string WagonId { get; set; } = Guid.NewGuid().ToString("N");
    public string ModelName { get; set; } = string.Empty;
    public bool Owned { get; set; }
    public bool Hitchable { get; set; } = true;
    public float Health { get; set; } = 100f;
    public List<string> InventoryItems { get; set; } = new List<string>();

    public WagonSaveData ToSaveData()
    {
        return new WagonSaveData
        {
            Id = WagonId,
            ModelName = ModelName,
            Owned = Owned,
            Hitchable = Hitchable,
            Health = Health,
            InventoryItems = new List<string>(InventoryItems)
        };
    }

    public static WagonRecord FromSaveData(WagonSaveData data)
    {
        if (data == null)
            return new WagonRecord();

        return new WagonRecord
        {
            WagonId = string.IsNullOrWhiteSpace(data.Id) ? Guid.NewGuid().ToString("N") : data.Id,
            ModelName = data.ModelName,
            Owned = data.Owned,
            Hitchable = data.Hitchable,
            Health = data.Health,
            InventoryItems = data.InventoryItems ?? new List<string>()
        };
    }
}