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
        {
            return null;
        }

        return _destructibles.Find(record =>
            record != null &&
            string.Equals(
                record.DestructibleId,
                targetId,
                StringComparison.Ordinal));
    }

    public void RegisterDestructible(DestructibleRecord record)
    {
        if (record == null ||
            string.IsNullOrWhiteSpace(record.DestructibleId))
        {
            return;
        }

        _destructibles.RemoveAll(existing =>
            existing != null &&
            string.Equals(
                existing.DestructibleId,
                record.DestructibleId,
                StringComparison.Ordinal));

        _destructibles.Add(record);
    }

    public bool CanDestroy(string targetId)
    {
        DestructibleRecord record = GetDestructible(targetId);

        return record != null &&
               record.IsDestroyableByExplosives &&
               !record.IsDestroyed;
    }

    public void ApplyExplosion(string targetId, int damage)
    {
        if (damage <= 0 || !CanDestroy(targetId))
        {
            return;
        }

        SetDestroyed(targetId, true);
    }

    public void Damage(string targetId, int amount)
    {
        ApplyExplosion(targetId, amount);
    }

    public void SetDestroyed(string targetId, bool destroyed)
    {
        DestructibleRecord record = GetDestructible(targetId);

        if (record == null)
        {
            return;
        }

        record.IsDestroyed = destroyed;
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
        {
            return;
        }

        try
        {
            List<DestructibleRecord> destructibles =
                JsonConvert.DeserializeObject<List<DestructibleRecord>>(
                    json);

            if (destructibles == null)
            {
                return;
            }

            foreach (DestructibleRecord record in destructibles)
            {
                RegisterDestructible(record);
            }
        }
        catch (JsonException)
        {
            _destructibles.Clear();
        }
    }
}