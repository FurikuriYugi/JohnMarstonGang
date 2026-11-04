// JsonSaveSystem.cs
using System.IO;
using Newtonsoft.Json;

public class JsonSaveSystem
{
    public void Save<T>(string path, T data)
    {
        if (string.IsNullOrWhiteSpace(path) || data == null)
            return;

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public T Load<T>(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            return default(T);

        string json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
            return default(T);

        return JsonConvert.DeserializeObject<T>(json);
    }
}