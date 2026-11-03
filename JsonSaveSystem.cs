using Newtonsoft.Json;
using System.IO;

public static class JsonSaveSystem
{
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.None,
        NullValueHandling = NullValueHandling.Ignore
    };

    public static void Save<T>(string path, T data)
    {
        var json = JsonConvert.SerializeObject(data, Settings);
        File.WriteAllText(path, json);
    }

    public static T Load<T>(string path) where T : new()
    {
        if (!File.Exists(path))
            return new T();

        var json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
            return new T();

        var data = JsonConvert.DeserializeObject<T>(json, Settings);
        if (data == null)
            return new T();

        return data;
    }
}