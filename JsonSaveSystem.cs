using System;
using System.IO;
using Newtonsoft.Json;

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

            SaveRoot data = JsonConvert.DeserializeObject<SaveRoot>(json);
            return data ?? new SaveRoot();
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
}