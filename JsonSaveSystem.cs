using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonSaveSystem
{
    private static readonly string SaveFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JohnMarstonGang");
    private static readonly string SaveFilePath = Path.Combine(SaveFolder, "save.json");

    public static SaveRoot Load()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
                return new SaveRoot();

            string json = File.ReadAllText(SaveFilePath);
            if (string.IsNullOrWhiteSpace(json))
                return new SaveRoot();

            JObject root = JObject.Parse(json);
            int saveVersion = root["SaveVersion"] != null ? root["SaveVersion"].Value<int>() : 1;

            SaveRoot data = root.ToObject<SaveRoot>();
            if (data == null)
                return new SaveRoot();

            Normalize(data, saveVersion);
            return data;
        }
        catch
        {
            return new SaveRoot();
        }
    }

    public static void Save(SaveRoot data)
    {
        if (data == null)
            data = new SaveRoot();

        try
        {
            if (!Directory.Exists(SaveFolder))
                Directory.CreateDirectory(SaveFolder);

            Normalize(data, data.SaveVersion);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(SaveFilePath, json);
        }
        catch
        {
        }
    }

    public static SaveRoot LoadToGangManager(GangManager manager)
    {
        SaveRoot data = Load();

        if (manager != null)
            manager.ImportSaveData(data);

        return data;
    }

    public static void SaveFromGangManager(GangManager manager)
    {
        if (manager == null)
            return;

        Save(manager.ExportSaveData());
    }

    private static void Normalize(SaveRoot data, int saveVersion)
    {
        if (data == null)
            return;

        if (data.SaveVersion <= 0)
            data.SaveVersion = 4;

        if (data.World == null)
            data.World = new WorldState();

        if (data.GangMembers == null)
            data.GangMembers = new System.Collections.Generic.List<GangMember>();

        if (data.BankAccounts == null)
            data.BankAccounts = new System.Collections.Generic.List<BankAccountData>();

        if (data.WorldEntities == null)
            data.WorldEntities = new System.Collections.Generic.List<WorldEntityData>();

        if (data.World.Companions == null)
            data.World.Companions = new System.Collections.Generic.List<CompanionData>();

        if (data.World.ActiveJobs == null)
            data.World.ActiveJobs = new System.Collections.Generic.List<JobData>();

        if (data.World.Wagons == null)
            data.World.Wagons = new System.Collections.Generic.List<WagonData>();

        if (data.World.WeaponPlatforms == null)
            data.World.WeaponPlatforms = new System.Collections.Generic.List<WeaponPlatformData>();

        if (data.World.FactionRelations == null)
            data.World.FactionRelations = new System.Collections.Generic.List<FactionRelationData>();

        if (data.World.Locations == null)
            data.World.Locations = new System.Collections.Generic.List<LocationStateData>();

        if (data.World.Entities == null)
            data.World.Entities = new System.Collections.Generic.List<EntityStateData>();

        if (data.World.Locks == null)
            data.World.Locks = new System.Collections.Generic.List<LockRecord>();

        if (data.World.Destructibles == null)
            data.World.Destructibles = new System.Collections.Generic.List<DestructibleRecord>();

        if (data.World.TacticalOrders == null)
            data.World.TacticalOrders = new System.Collections.Generic.List<TacticalOrderData>();

        NormalizeJobs(data);
        NormalizeWorldEntities(data);
        NormalizeCompanions(data);
        NormalizeWagons(data);
        NormalizeWeaponPlatforms(data);
        NormalizeLocks(data);
        NormalizeDestructibles(data);
        NormalizeTacticalOrders(data);
        NormalizeBankAccounts(data);
        NormalizeLocations(data);
        NormalizeFactionRelations(data);

        if (saveVersion < 4)
            MigrateFromV3(data);

        data.SaveVersion = 4;
    }

    private static void NormalizeJobs(SaveRoot data)
    {
        for (int i = 0; i < data.World.ActiveJobs.Count; i++)
        {
            JobData job = data.World.ActiveJobs[i];
            if (job == null)
                continue;

            if (job.Objectives == null)
                job.Objectives = new System.Collections.Generic.List<string>();

            if (job.Participants == null)
                job.Participants = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeWorldEntities(SaveRoot data)
    {
        for (int i = 0; i < data.WorldEntities.Count; i++)
        {
            WorldEntityData entity = data.WorldEntities[i];
            if (entity == null)
                continue;

            if (entity.InventoryItems == null)
                entity.InventoryItems = new System.Collections.Generic.List<string>();

            if (entity.KnownGoals == null)
                entity.KnownGoals = new System.Collections.Generic.List<string>();
        }

        for (int i = 0; i < data.World.Entities.Count; i++)
        {
            EntityStateData entity = data.World.Entities[i];
            if (entity == null)
                continue;

            if (entity.InventoryItems == null)
                entity.InventoryItems = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeCompanions(SaveRoot data)
    {
        for (int i = 0; i < data.World.Companions.Count; i++)
        {
            CompanionData companion = data.World.Companions[i];
            if (companion == null)
                continue;

            if (companion.InventoryItems == null)
                companion.InventoryItems = new System.Collections.Generic.List<string>();

            if (companion.Memories == null)
                companion.Memories = new System.Collections.Generic.List<string>();

            if (companion.Goals == null)
                companion.Goals = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeWagons(SaveRoot data)
    {
        for (int i = 0; i < data.World.Wagons.Count; i++)
        {
            WagonData wagon = data.World.Wagons[i];
            if (wagon == null)
                continue;

            if (wagon.StoredItems == null)
                wagon.StoredItems = new System.Collections.Generic.List<string>();

            if (wagon.AttachedHorses == null)
                wagon.AttachedHorses = new System.Collections.Generic.List<string>();

            if (wagon.AttachedWeapons == null)
                wagon.AttachedWeapons = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeWeaponPlatforms(SaveRoot data)
    {
        for (int i = 0; i < data.World.WeaponPlatforms.Count; i++)
        {
            WeaponPlatformData platform = data.World.WeaponPlatforms[i];
            if (platform == null)
                continue;

            if (platform.ReloadCooldownSeconds < 0)
                platform.ReloadCooldownSeconds = 0;
        }
    }

    private static void NormalizeLocks(SaveRoot data)
    {
        for (int i = 0; i < data.World.Locks.Count; i++)
        {
            LockRecord record = data.World.Locks[i];
            if (record == null)
                continue;
        }
    }

    private static void NormalizeDestructibles(SaveRoot data)
    {
        for (int i = 0; i < data.World.Destructibles.Count; i++)
        {
            DestructibleRecord record = data.World.Destructibles[i];
            if (record == null)
                continue;

            if (record.MaxHealth < 0)
                record.MaxHealth = 0;

            if (record.Health < 0)
                record.Health = 0;

            if (record.MaxHealth > 0 && record.Health > record.MaxHealth)
                record.Health = record.MaxHealth;
        }
    }

    private static void NormalizeTacticalOrders(SaveRoot data)
    {
        for (int i = 0; i < data.World.TacticalOrders.Count; i++)
        {
            TacticalOrderData order = data.World.TacticalOrders[i];
            if (order == null)
                continue;

            if (order.AssignedMemberIds == null)
                order.AssignedMemberIds = new System.Collections.Generic.List<string>();

            if (order.Notes == null)
                order.Notes = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeBankAccounts(SaveRoot data)
    {
        for (int i = 0; i < data.BankAccounts.Count; i++)
        {
            BankAccountData account = data.BankAccounts[i];
            if (account == null)
                continue;

            if (account.Cash < 0)
                account.Cash = 0;

            if (account.Gold < 0)
                account.Gold = 0;

            if (account.CreditLimit < 0)
                account.CreditLimit = 0;

            if (account.Debt < 0)
                account.Debt = 0;
        }
    }

    private static void NormalizeLocations(SaveRoot data)
    {
        for (int i = 0; i < data.World.Locations.Count; i++)
        {
            LocationStateData location = data.World.Locations[i];
            if (location == null)
                continue;

            if (location.UnlockedDoors == null)
                location.UnlockedDoors = new System.Collections.Generic.List<string>();

            if (location.DestroyedObjects == null)
                location.DestroyedObjects = new System.Collections.Generic.List<string>();
        }
    }

    private static void NormalizeFactionRelations(SaveRoot data)
    {
        for (int i = 0; i < data.World.FactionRelations.Count; i++)
        {
            FactionRelationData relation = data.World.FactionRelations[i];
            if (relation == null)
                continue;
        }
    }

    private static void MigrateFromV3(SaveRoot data)
    {
        if (data == null)
            return;

        if (data.WorldEntities == null)
            data.WorldEntities = new System.Collections.Generic.List<WorldEntityData>();
    }
}