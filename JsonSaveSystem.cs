using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class JsonSaveSystem
{
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore
    };

    public static void Save<T>(string path, T data)
    {
        string json = JsonConvert.SerializeObject(data, Settings);
        File.WriteAllText(path, json);
    }

    public static T Load<T>(string path) where T : new()
    {
        if (!File.Exists(path))
            return new T();

        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
            return new T();

        return JsonConvert.DeserializeObject<T>(json, Settings) ?? new T();
    }
}