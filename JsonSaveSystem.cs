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
            {
                return new SaveRoot();
            }

            string json = File.ReadAllText(SaveFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new SaveRoot();
            }

            JObject root = JObject.Parse(json);
            int saveVersion = root["SaveVersion"] != null ? root["SaveVersion"].Value<int>() : 1;

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

        if (data.SaveVersion <= 0)
        {
            data.SaveVersion = 2;
        }

        if (data.World == null)
        {
            data.World = new WorldState();
        }

        if (data.GangMembers == null)
        {
            data.GangMembers = new System.Collections.Generic.List<GangMember>();
        }

        if (data.BankAccounts == null)
        {
            data.BankAccounts = new System.Collections.Generic.List<BankAccountData>();
        }

        if (data.World.Companions == null)
        {
            data.World.Companions = new System.Collections.Generic.List<CompanionData>();
        }

        if (data.World.ActiveJobs == null)
        {
            data.World.ActiveJobs = new System.Collections.Generic.List<JobData>();
        }

        if (data.World.Wagons == null)
        {
            data.World.Wagons = new System.Collections.Generic.List<WagonData>();
        }

        if (data.World.WeaponPlatforms == null)
        {
            data.World.WeaponPlatforms = new System.Collections.Generic.List<WeaponPlatformData>();
        }

        if (data.World.FactionRelations == null)
        {
            data.World.FactionRelations = new System.Collections.Generic.List<FactionRelationData>();
        }

        if (data.World.Locations == null)
        {
            data.World.Locations = new System.Collections.Generic.List<LocationStateData>();
        }

        if (data.World.Entities == null)
        {
            data.World.Entities = new System.Collections.Generic.List<EntityStateData>();
        }

        if (saveVersion < 2)
        {
            MigrateFromV1(data);
        }

        data.SaveVersion = 2;
    }

    private static void MigrateFromV1(SaveRoot data)
    {
        if (data == null)
        {
            return;
        }

        if (data.World == null)
        {
            data.World = new WorldState();
        }

        if (data.World.Companions == null)
        {
            data.World.Companions = new System.Collections.Generic.List<CompanionData>();
        }

        if (data.World.ActiveJobs == null)
        {
            data.World.ActiveJobs = new System.Collections.Generic.List<JobData>();
        }

        if (data.World.Wagons == null)
        {
            data.World.Wagons = new System.Collections.Generic.List<WagonData>();
        }

        if (data.World.WeaponPlatforms == null)
        {
            data.World.WeaponPlatforms = new System.Collections.Generic.List<WeaponPlatformData>();
        }

        if (data.World.FactionRelations == null)
        {
            data.World.FactionRelations = new System.Collections.Generic.List<FactionRelationData>();
        }

        if (data.World.Locations == null)
        {
            data.World.Locations = new System.Collections.Generic.List<LocationStateData>();
        }

        if (data.World.Entities == null)
        {
            data.World.Entities = new System.Collections.Generic.List<EntityStateData>();
        }
    }
}