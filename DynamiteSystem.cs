using System.Collections.Generic;
using Newtonsoft.Json;

public class DynamiteSystem
{
    private readonly Dictionary<int, DestructibleTarget> _targets = new Dictionary<int, DestructibleTarget>();

    public void RegisterTarget(DestructibleTarget target)
    {
        if (target == null)
            return;

        _targets[target.Id] = target;
    }

    public bool CanBeDestroyed(int targetId)
    {
        return _targets.ContainsKey(targetId) && _targets[targetId].IsDestructible;
    }

    public bool DamageTarget(int targetId, float damage)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        DestructibleTarget target = _targets[targetId];
        if (!target.IsDestructible)
            return false;

        target.CurrentHealth -= damage;
        if (target.CurrentHealth <= 0f)
        {
            target.CurrentHealth = 0f;
            target.IsDestroyed = true;
        }

        return true;
    }

    public bool BlastOpen(int targetId)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        DestructibleTarget target = _targets[targetId];
        if (!target.AllowsExplosion)
            return false;

        target.CurrentHealth = 0f;
        target.IsDestroyed = true;
        return true;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_targets.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _targets.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<DestructibleTarget> targets = JsonConvert.DeserializeObject<List<DestructibleTarget>>(json);
        if (targets == null)
            return;

        foreach (DestructibleTarget target in targets)
        {
            if (target != null)
                _targets[target.Id] = target;
        }
    }
}

public class DestructibleTarget
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDestructible { get; set; } = true;
    public bool AllowsExplosion { get; set; } = true;
    public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; } = 100f;
    public bool IsDestroyed { get; set; }
    public string TargetType { get; set; }
}