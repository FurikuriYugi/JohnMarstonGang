using System.IO;
using Newtonsoft.Json;

public class JsonSaveSystem
{
    public void Save(string path, SaveRoot root)
    {
        if (string.IsNullOrWhiteSpace(path) || root == null)
            return;

        string json = JsonConvert.SerializeObject(root, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public T Load<T>(string path) where T : class
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }
}