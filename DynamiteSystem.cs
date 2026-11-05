using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DynamiteSystem
{
    private readonly List<DestructibleRecord> _destructibles =
        new List<DestructibleRecord>();

    public List<DestructibleRecord> GetDestructibles()
    {
        return new List<DestructibleRecord>(_destructibles);
    }

    public DestructibleRecord GetDestructible(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        return _destructibles.Find(
            x => x != null && x.DestructibleId == targetId);
    }

    public void RegisterDestructible(DestructibleRecord record)
    {
        if (record == null ||
            string.IsNullOrWhiteSpace(record.DestructibleId))
        {
            return;
        }

        if (record.MaxHealthPoints < 0)
            record.MaxHealthPoints = 0;

        if (record.HealthPoints < 0)
            record.HealthPoints = 0;

        if (record.MaxHealthPoints > 0 &&
            record.HealthPoints > record.MaxHealthPoints)
        {
            record.HealthPoints = record.MaxHealthPoints;
        }

        if (record.IsDestroyed)
            record.HealthPoints = 0;

        _destructibles.RemoveAll(
            x => x != null &&
                 x.DestructibleId == record.DestructibleId);

        _destructibles.Add(record);
    }

    public bool CanDestroy(string targetId)
    {
        DestructibleRecord record = GetDestructible(targetId);

        return record != null &&
               record.CanBeBlasted &&
               !record.IsDestroyed;
    }

    public void ApplyExplosion(string targetId, int damage)
    {
        DestructibleRecord record = GetDestructible(targetId);

        if (record == null ||
            !record.CanBeBlasted ||
            record.IsDestroyed ||
            damage <= 0)
        {
            return;
        }

        int effectiveDamage =
            damage - Math.Max(0, record.BlastResistance);

        if (effectiveDamage <= 0)
            return;

        record.HealthPoints -= effectiveDamage;
        record.LastDamagedUtc = DateTime.UtcNow;

        if (record.HealthPoints <= 0)
        {
            record.HealthPoints = 0;
            record.IsDestroyed = true;
        }
    }

    public void Damage(string targetId, int amount)
    {
        ApplyExplosion(targetId, amount);
    }

    public void SetDestroyed(string targetId, bool destroyed)
    {
        DestructibleRecord record = GetDestructible(targetId);

        if (record == null)
            return;

        record.IsDestroyed = destroyed;

        if (destroyed)
        {
            record.HealthPoints = 0;
        }
        else if (record.MaxHealthPoints > 0)
        {
            record.HealthPoints = record.MaxHealthPoints;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            _destructibles,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _destructibles.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<DestructibleRecord> destructibles =
            JsonConvert.DeserializeObject<List<DestructibleRecord>>(json);

        if (destructibles == null)
            return;

        foreach (DestructibleRecord record in destructibles)
            RegisterDestructible(record);
    }
}