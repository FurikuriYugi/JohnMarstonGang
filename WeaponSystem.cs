using System.Collections.Generic;
using Newtonsoft.Json;

public class WeaponSystem
{
    private readonly List<WeaponPlatformData> _platforms = new List<WeaponPlatformData>();

    public List<WeaponPlatformData> GetPlatforms()
    {
        return new List<WeaponPlatformData>(_platforms);
    }

    public WeaponPlatformData GetPlatform(string platformId)
    {
        if (string.IsNullOrWhiteSpace(platformId))
            return null;

        return _platforms.Find(x => x != null && x.PlatformId == platformId);
    }

    public void RegisterPlatform(WeaponPlatformData platform)
    {
        if (platform == null || string.IsNullOrWhiteSpace(platform.PlatformId))
            return;

        _platforms.RemoveAll(x => x != null && x.PlatformId == platform.PlatformId);
        _platforms.Add(platform);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_platforms, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _platforms.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WeaponPlatformData> platforms = JsonConvert.DeserializeObject<List<WeaponPlatformData>>(json);
        if (platforms != null)
            _platforms.AddRange(platforms);
    }
}