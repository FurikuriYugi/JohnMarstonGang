using System.Collections.Generic;
using Newtonsoft.Json;

public class DynamiteSystem
{
    private readonly List<DestructibleRecord> _destructibles = new List<DestructibleRecord>();

    public List<DestructibleRecord> GetDestructibles()
    {
        return new List<DestructibleRecord>(_destructibles);
    }

    public DestructibleRecord GetDestructible(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        return _destructibles.Find(x => x != null && x.TargetId == targetId);
    }

    public void RegisterDestructible(DestructibleRecord record)
    {
        if (record == null || string.IsNullOrWhiteSpace(record.TargetId))
            return;

        _destructibles.RemoveAll(x => x != null && x.TargetId == record.TargetId);
        _destructibles.Add(record);
    }

    public void Damage(string targetId, int amount)
    {
        DestructibleRecord record = GetDestructible(targetId);
        if (record == null || amount <= 0)
            return;

        record.Health -= amount;
        if (record.Health <= 0)
        {
            record.Health = 0;
            record.IsDestroyed = true;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_destructibles, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _destructibles.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<DestructibleRecord> destructibles = JsonConvert.DeserializeObject<List<DestructibleRecord>>(json);
        if (destructibles != null)
            _destructibles.AddRange(destructibles);
    }
}