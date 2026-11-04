using System.Collections.Generic;
using Newtonsoft.Json;

public class WeaponSystem
{
    private readonly List<WeaponPlatformData> _platforms = new List<WeaponPlatformData>();

    public List<WeaponPlatformData> GetPlatforms()
    {
        return new List<WeaponPlatformData>(_platforms);
    }

    public void AddPlatform(WeaponPlatformData platform)
    {
        if (platform == null || string.IsNullOrWhiteSpace(platform.PlatformId))
            return;

        _platforms.RemoveAll(p => p != null && p.PlatformId == platform.PlatformId);
        _platforms.Add(platform);
    }

    public WeaponPlatformData GetPlatform(string platformId)
    {
        if (string.IsNullOrWhiteSpace(platformId))
            return null;

        return _platforms.Find(p => p != null && p.PlatformId == platformId);
    }

    public void SetGunCrew(string platformId, string gunnerId, string assistantId)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentGunnerId = gunnerId;
        platform.CurrentAssistantId = assistantId;
    }

    public void SetAmmoType(string platformId, string ammoType)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentAmmoType = ammoType;
    }

    public void SetReloadCooldown(string platformId, int seconds)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.ReloadCooldownSeconds = seconds;
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