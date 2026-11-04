using System;
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

    public WeaponPlatformData CreatePlatform(string ownerId, string platformType, bool portable, bool requiresGunner, bool requiresAssistant)
    {
        WeaponPlatformData platform = new WeaponPlatformData
        {
            PlatformId = Guid.NewGuid().ToString("N"),
            OwnerId = ownerId,
            PlatformType = platformType,
            IsPortable = portable,
            RequiresGunner = requiresGunner,
            RequiresAssistant = requiresAssistant,
            ReloadCooldownSeconds = portable ? 6 : 12
        };

        RegisterPlatform(platform);
        return platform;
    }

    public void AssignGunner(string platformId, string gunnerId)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentGunnerId = gunnerId;
        if (!string.IsNullOrWhiteSpace(gunnerId))
            platform.ReloadCooldownSeconds = Math.Max(1, platform.ReloadCooldownSeconds - 2);
    }

    public void AssignAssistant(string platformId, string assistantId)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentAssistantId = assistantId;
        if (!string.IsNullOrWhiteSpace(assistantId))
            platform.ReloadCooldownSeconds = Math.Max(1, platform.ReloadCooldownSeconds - 1);
    }

    public void SetAmmoType(string platformId, string ammoType)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentAmmoType = ammoType;
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