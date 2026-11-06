using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonSaveSystem
{
    private static readonly string SaveFolder =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "JohnMarstonGang");

    private static readonly string SaveFilePath =
        Path.Combine(SaveFolder, "save.json");

    private const int CurrentSaveVersion = 5;

    public static SaveRoot Load()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                return new SaveRoot();
            }

            string json = File.ReadAllText(SaveFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SaveRoot();
            }

            JObject root = JObject.Parse(json);

            int saveVersion = root["SaveVersion"] != null
                ? root["SaveVersion"].Value<int>()
                : 1;

            SaveRoot data = root.ToObject<SaveRoot>();

            if (data == null)
            {
                return new SaveRoot();
            }

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
        {
            data = new SaveRoot();
        }

        try
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }

            Normalize(data, data.SaveVersion);

            string json = JsonConvert.SerializeObject(
                data,
                Formatting.Indented);

            File.WriteAllText(SaveFilePath, json);
        }
        catch
        {
            // A save failure should not crash the game script.
        }
    }

    public static SaveRoot LoadToGangManager(GangManager manager)
    {
        SaveRoot data = Load();

        if (manager != null)
        {
            manager.ImportSaveData(data);
        }

        return data;
    }

    public static void SaveFromGangManager(GangManager manager)
    {
        if (manager == null)
        {
            return;
        }

        Save(manager.ExportSaveData());
    }

    private static void Normalize(SaveRoot data, int saveVersion)
    {
        if (data == null)
        {
            return;
        }

        if (saveVersion <= 0)
        {
            saveVersion = 1;
        }

        if (data.World == null)
        {
            data.World = new WorldState();
        }

        if (data.GangMembers == null)
        {
            data.GangMembers = new List<GangMember>();
        }

        if (data.BankAccounts == null)
        {
            data.BankAccounts = new List<BankAccountData>();
        }

        if (data.WorldEntities == null)
        {
            data.WorldEntities = new List<WorldEntityData>();
        }

        NormalizeGangManagement(data);
        NormalizeWorldCollections(data);
        NormalizeJobs(data);
        NormalizeWorldEntities(data);
        NormalizeCompanions(data);
        NormalizeCompanionAIStates(data);
        NormalizeWagons(data);
        NormalizeWeaponPlatforms(data);
        NormalizeTacticalOrders(data);
        NormalizeCommands(data);
        NormalizeBankAccounts(data);
        NormalizeLocations(data);
        NormalizeTrainLoot(data);
        NormalizeStageCoachLoot(data);

        if (saveVersion < 4)
        {
            MigrateFromV3(data);
        }

        data.SaveVersion = CurrentSaveVersion;
    }

    private static void NormalizeGangManagement(SaveRoot data)
    {
        if (data.GangManagement == null)
        {
            data.GangManagement = new GangManagementData();
        }

        if (data.GangManagement.Hideouts == null)
        {
            data.GangManagement.Hideouts = new List<Hideout>();
        }

        if (data.GangManagement.PendingRewards == null)
        {
            data.GangManagement.PendingRewards =
                new List<PendingJobReward>();
        }

        if (data.GangManagement.Milestones == null)
        {
            data.GangManagement.Milestones = new List<GangMilestone>();
        }

        for (int i = 0;
             i < data.GangManagement.Hideouts.Count;
             i++)
        {
            Hideout hideout = data.GangManagement.Hideouts[i];

            if (hideout == null)
            {
                continue;
            }

            if (hideout.ChestInventory == null)
            {
                hideout.ChestInventory = new ChestInventory();
            }

            if (hideout.ChestInventory.StolenItems == null)
            {
                hideout.ChestInventory.StolenItems =
                    new List<string>();
            }

            if (hideout.ChestInventory.Valuables == null)
            {
                hideout.ChestInventory.Valuables =
                    new List<string>();
            }
        }

        for (int i = 0;
             i < data.GangManagement.PendingRewards.Count;
             i++)
        {
            PendingJobReward reward =
                data.GangManagement.PendingRewards[i];

            if (reward == null)
            {
                continue;
            }

            if (reward.MemberShares == null)
            {
                reward.MemberShares =
                    new Dictionary<string, decimal>();
            }

            if (reward.ParticipantIds == null)
            {
                reward.ParticipantIds = new List<string>();
            }
        }

        for (int i = 0;
             i < data.GangManagement.Milestones.Count;
             i++)
        {
            GangMilestone milestone =
                data.GangManagement.Milestones[i];

            if (milestone != null &&
                milestone.UnlockedCapabilities == null)
            {
                milestone.UnlockedCapabilities =
                    new Dictionary<string, string>();
            }
        }
    }

    private static void NormalizeWorldCollections(SaveRoot data)
    {
        if (data.World.Companions == null)
        {
            data.World.Companions = new List<CompanionData>();
        }

        if (data.World.ActiveJobs == null)
        {
            data.World.ActiveJobs = new List<JobData>();
        }

        if (data.World.Wagons == null)
        {
            data.World.Wagons = new List<WagonData>();
        }

        if (data.World.WeaponPlatforms == null)
        {
            data.World.WeaponPlatforms =
                new List<WeaponPlatformData>();
        }

        if (data.World.FactionRelations == null)
        {
            data.World.FactionRelations =
                new List<FactionRelationData>();
        }

        if (data.World.Locations == null)
        {
            data.World.Locations = new List<LocationStateData>();
        }

        if (data.World.Entities == null)
        {
            data.World.Entities = new List<EntityStateData>();
        }

        if (data.World.Commands == null)
        {
            data.World.Commands = new List<CommandData>();
        }

        if (data.World.Locks == null)
        {
            data.World.Locks = new List<LockRecord>();
        }

        if (data.World.Destructibles == null)
        {
            data.World.Destructibles =
                new List<DestructibleRecord>();
        }

        if (data.World.TacticalOrders == null)
        {
            data.World.TacticalOrders =
                new List<TacticalOrderData>();
        }

        if (data.World.BankVaults == null)
        {
            data.World.BankVaults = new List<BankVaultData>();
        }

        if (data.World.TrainLoot == null)
        {
            data.World.TrainLoot = new List<TrainLootData>();
        }

        if (data.World.StageCoachLoot == null)
        {
            data.World.StageCoachLoot =
                new List<StageCoachLootData>();
        }

        if (data.World.ObjectiveStates == null)
        {
            data.World.ObjectiveStates =
                new List<ObjectiveStateData>();
        }

        if (data.World.CompanionAIStates == null)
        {
            data.World.CompanionAIStates =
                new List<CompanionAIStateData>();
        }
    }

    private static void NormalizeJobs(SaveRoot data)
    {
        for (int i = 0; i < data.World.ActiveJobs.Count; i++)
        {
            JobData job = data.World.ActiveJobs[i];

            if (job == null)
            {
                continue;
            }

            if (job.Objectives == null)
            {
                job.Objectives = new List<string>();
            }

            if (job.Participants == null)
            {
                job.Participants = new List<string>();
            }
        }
    }

    private static void NormalizeWorldEntities(SaveRoot data)
    {
        for (int i = 0; i < data.WorldEntities.Count; i++)
        {
            WorldEntityData entity = data.WorldEntities[i];

            if (entity == null)
            {
                continue;
            }

            if (entity.InventoryItems == null)
            {
                entity.InventoryItems = new List<string>();
            }

            if (entity.KnownGoals == null)
            {
                entity.KnownGoals = new List<string>();
            }
        }

        for (int i = 0; i < data.World.Entities.Count; i++)
        {
            EntityStateData entity = data.World.Entities[i];

            if (entity == null)
            {
                continue;
            }

            if (entity.InventoryItems == null)
            {
                entity.InventoryItems = new List<string>();
            }
        }
    }

    private static void NormalizeCompanions(SaveRoot data)
    {
        for (int i = 0; i < data.World.Companions.Count; i++)
        {
            CompanionData companion = data.World.Companions[i];

            if (companion == null)
            {
                continue;
            }

            if (companion.InventoryItems == null)
            {
                companion.InventoryItems = new List<string>();
            }

            if (companion.Memories == null)
            {
                companion.Memories = new List<string>();
            }

            if (companion.Goals == null)
            {
                companion.Goals = new List<string>();
            }
        }
    }

    private static void NormalizeCompanionAIStates(SaveRoot data)
    {
        for (int i = 0;
             i < data.World.CompanionAIStates.Count;
             i++)
        {
            CompanionAIStateData state =
                data.World.CompanionAIStates[i];

            if (state == null)
            {
                continue;
            }

            if (state.SkillProgression == null)
            {
                state.SkillProgression =
                    new Dictionary<string, int>();
            }

            if (state.CompletedJobs == null)
            {
                state.CompletedJobs = new List<string>();
            }
        }
    }

    private static void NormalizeWagons(SaveRoot data)
    {
        for (int i = 0; i < data.World.Wagons.Count; i++)
        {
            WagonData wagon = data.World.Wagons[i];

            if (wagon == null)
            {
                continue;
            }

            if (wagon.StoredItems == null)
            {
                wagon.StoredItems = new List<string>();
            }

            if (wagon.AttachedHorses == null)
            {
                wagon.AttachedHorses = new List<string>();
            }

            if (wagon.AttachedWeapons == null)
            {
                wagon.AttachedWeapons = new List<string>();
            }
        }
    }

    private static void NormalizeWeaponPlatforms(SaveRoot data)
    {
        for (int i = 0;
             i < data.World.WeaponPlatforms.Count;
             i++)
        {
            WeaponPlatformData platform =
                data.World.WeaponPlatforms[i];

            if (platform != null &&
                platform.ReloadCooldownSeconds < 0)
            {
                platform.ReloadCooldownSeconds = 0;
            }
        }
    }

    private static void NormalizeTacticalOrders(SaveRoot data)
    {
        for (int i = 0;
             i < data.World.TacticalOrders.Count;
             i++)
        {
            TacticalOrderData order =
                data.World.TacticalOrders[i];

            if (order == null)
            {
                continue;
            }

            if (order.AssignedMemberIds == null)
            {
                order.AssignedMemberIds = new List<string>();
            }

            if (order.Notes == null)
            {
                order.Notes = new List<string>();
            }
        }
    }

    private static void NormalizeCommands(SaveRoot data)
    {
        for (int i = 0; i < data.World.Commands.Count; i++)
        {
            CommandData command = data.World.Commands[i];

            if (command != null && command.TargetIds == null)
            {
                command.TargetIds = new List<string>();
            }
        }
    }

    private static void NormalizeBankAccounts(SaveRoot data)
    {
        for (int i = 0; i < data.BankAccounts.Count; i++)
        {
            BankAccountData account = data.BankAccounts[i];

            if (account == null)
            {
                continue;
            }

            if (account.Cash < 0m)
            {
                account.Cash = 0m;
            }

            if (account.Gold < 0m)
            {
                account.Gold = 0m;
            }

            if (account.CreditLimit < 0m)
            {
                account.CreditLimit = 0m;
            }

            if (account.Debt < 0m)
            {
                account.Debt = 0m;
            }
        }
    }

    private static void NormalizeLocations(SaveRoot data)
    {
        for (int i = 0; i < data.World.Locations.Count; i++)
        {
            LocationStateData location =
                data.World.Locations[i];

            if (location == null)
            {
                continue;
            }

            if (location.UnlockedDoors == null)
            {
                location.UnlockedDoors = new List<string>();
            }

            if (location.DestroyedObjects == null)
            {
                location.DestroyedObjects =
                    new List<string>();
            }
        }
    }

    private static void NormalizeTrainLoot(SaveRoot data)
    {
        for (int i = 0; i < data.World.TrainLoot.Count; i++)
        {
            TrainLootData loot = data.World.TrainLoot[i];

            if (loot != null && loot.Papers == null)
            {
                loot.Papers = new List<string>();
            }
        }
    }

    private static void NormalizeStageCoachLoot(SaveRoot data)
    {
        for (int i = 0;
             i < data.World.StageCoachLoot.Count;
             i++)
        {
            StageCoachLootData loot =
                data.World.StageCoachLoot[i];

            if (loot != null && loot.Papers == null)
            {
                loot.Papers = new List<string>();
            }
        }
    }

    private static void MigrateFromV3(SaveRoot data)
    {
        if (data.WorldEntities == null)
        {
            data.WorldEntities = new List<WorldEntityData>();
        }

        if (data.World == null)
        {
            data.World = new WorldState();
        }

        NormalizeWorldCollections(data);
    }
}