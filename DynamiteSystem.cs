using System.Collections.Generic;
using Newtonsoft.Json;

public class DynamiteSystem
{
    private readonly Dictionary<string, DestructibleRecord> _destructibles = new Dictionary<string, DestructibleRecord>();

    public void RegisterDestructible(DestructibleRecord destructible)
    {
        if (destructible == null || string.IsNullOrWhiteSpace(destructible.TargetId))
            return;

        _destructibles[destructible.TargetId] = destructible;
    }

    public DestructibleRecord GetDestructible(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        _destructibles.TryGetValue(targetId, out DestructibleRecord destructible);
        return destructible;
    }

    public List<DestructibleRecord> GetAllDestructibles()
    {
        return new List<DestructibleRecord>(_destructibles.Values);
    }

    public bool CanDestroy(string targetId)
    {
        DestructibleRecord destructible = GetDestructible(targetId);
        return destructible != null && destructible.IsDestructible && destructible.Health > 0;
    }

    public void ApplyExplosion(string targetId, int damage)
    {
        DestructibleRecord destructible = GetDestructible(targetId);
        if (destructible == null || damage <= 0)
            return;

        destructible.Health -= damage;
        if (destructible.Health <= 0)
        {
            destructible.Health = 0;
            destructible.IsDestroyed = true;
        }
    }

    public void Destroy(string targetId)
    {
        DestructibleRecord destructible = GetDestructible(targetId);
        if (destructible == null)
            return;

        destructible.Health = 0;
        destructible.IsDestroyed = true;
    }

    public void Repair(string targetId, int amount)
    {
        DestructibleRecord destructible = GetDestructible(targetId);
        if (destructible == null || amount <= 0)
            return;

        destructible.Health += amount;
        if (destructible.Health > destructible.MaxHealth)
            destructible.Health = destructible.MaxHealth;

        if (destructible.Health > 0)
            destructible.IsDestroyed = false;
    }

    public void LoadDestructibles(List<DestructibleRecord> destructibles)
    {
        _destructibles.Clear();

        if (destructibles == null)
            return;

        foreach (DestructibleRecord destructible in destructibles)
        {
            if (destructible != null && !string.IsNullOrWhiteSpace(destructible.TargetId))
                _destructibles[destructible.TargetId] = destructible;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllDestructibles(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _destructibles.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<DestructibleRecord> destructibles = JsonConvert.DeserializeObject<List<DestructibleRecord>>(json);
        LoadDestructibles(destructibles);
    }
}

public class DestructibleRecord
{
    public string TargetId { get; set; }
    public string TargetType { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsDestroyed { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public bool CanBeBlasted { get; set; }
    public bool CanBePicked { get; set; }
}