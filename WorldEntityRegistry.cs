using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class WorldEntityRegistry
{
    private readonly List<WorldEntityData> _entities = new List<WorldEntityData>();

    public List<WorldEntityData> GetEntities()
    {
        return new List<WorldEntityData>(_entities);
    }

    public WorldEntityData GetEntity(string entityId)
    {
        if (string.IsNullOrWhiteSpace(entityId))
            return null;

        return _entities.Find(x => x != null && x.EntityId == entityId);
    }

    public void RegisterEntity(WorldEntityData entity)
    {
        if (entity == null || string.IsNullOrWhiteSpace(entity.EntityId))
            return;

        _entities.RemoveAll(x => x != null && x.EntityId == entity.EntityId);
        _entities.Add(entity);
    }

    public void RemoveEntity(string entityId)
    {
        _entities.RemoveAll(x => x != null && x.EntityId == entityId);
    }

    public List<WorldEntityData> GetByFaction(string factionId)
    {
        if (string.IsNullOrWhiteSpace(factionId))
            return new List<WorldEntityData>();

        return _entities.Where(x => x != null && x.FactionId == factionId).ToList();
    }

    public List<WorldEntityData> GetRecruitable()
    {
        return _entities.Where(x => x != null && x.IsRecruitable && x.IsAlive).ToList();
    }

    public List<WorldEntityData> GetFollowers()
    {
        return _entities.Where(x => x != null && x.IsFollowingPlayer && x.IsAlive).ToList();
    }

    public WorldEntityData EnsureEntity(string entityId, string displayName, string entityType)
    {
        WorldEntityData entity = GetEntity(entityId);
        if (entity != null)
            return entity;

        entity = new WorldEntityData
        {
            EntityId = string.IsNullOrWhiteSpace(entityId) ? Guid.NewGuid().ToString("N") : entityId,
            DisplayName = displayName,
            EntityType = entityType,
            IsAlive = true
        };

        RegisterEntity(entity);
        return entity;
    }

    public void SetFollowState(string entityId, bool isFollowingPlayer, string currentTask)
    {
        WorldEntityData entity = GetEntity(entityId);
        if (entity == null)
            return;

        entity.IsFollowingPlayer = isFollowingPlayer;
        entity.CurrentTask = currentTask;
    }

    public void SetCombatProfile(string entityId, string combatStyle, int combatSkill, int stealthSkill, int supportSkill)
    {
        WorldEntityData entity = GetEntity(entityId);
        if (entity == null)
            return;

        entity.CombatStyle = combatStyle;
        entity.CombatSkill = combatSkill;
        entity.StealthSkill = stealthSkill;
        entity.SupportSkill = supportSkill;
    }

    public void GiveItem(string entityId, string item)
    {
        WorldEntityData entity = GetEntity(entityId);
        if (entity == null || string.IsNullOrWhiteSpace(item))
            return;

        if (entity.InventoryItems == null)
            entity.InventoryItems = new List<string>();

        if (!entity.InventoryItems.Contains(item))
            entity.InventoryItems.Add(item);
    }

    public bool HasItem(string entityId, string item)
    {
        WorldEntityData entity = GetEntity(entityId);
        if (entity == null || string.IsNullOrWhiteSpace(item) || entity.InventoryItems == null)
            return false;

        return entity.InventoryItems.Contains(item);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_entities, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _entities.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WorldEntityData> entities = JsonConvert.DeserializeObject<List<WorldEntityData>>(json);
        if (entities != null)
            _entities.AddRange(entities);
    }
}