using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WeaponSystem
{
    private readonly List<WeaponPlatformData> _platforms =
        new List<WeaponPlatformData>();

    private readonly Dictionary<string, int> _baseReloadCooldowns =
        new Dictionary<string, int>(StringComparer.Ordinal);

    public List<WeaponPlatformData> GetPlatforms()
    {
        return new List<WeaponPlatformData>(_platforms);
    }

    public WeaponPlatformData GetPlatform(string platformId)
    {
        if (string.IsNullOrWhiteSpace(platformId))
            return null;

        return _platforms.Find(
            x => x != null && x.PlatformId == platformId);
    }

    public void RegisterPlatform(WeaponPlatformData platform)
    {
        if (platform == null ||
            string.IsNullOrWhiteSpace(platform.PlatformId))
        {
            return;
        }

        int baseCooldown;

        if (!_baseReloadCooldowns.TryGetValue(
                platform.PlatformId,
                out baseCooldown))
        {
            baseCooldown = GetBaseCooldownFromSavedValue(platform);
        }

        _platforms.RemoveAll(
            x => x != null && x.PlatformId == platform.PlatformId);

        _baseReloadCooldowns[platform.PlatformId] =
            Math.Max(1, baseCooldown);

        _platforms.Add(platform);
        RecalculateReloadCooldown(platform);
    }

    public WeaponPlatformData CreatePlatform(
        string ownerId,
        string platformType,
        bool portable,
        bool requiresGunner,
        bool requiresAssistant)
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
        RecalculateReloadCooldown(platform);
    }

    public void AssignAssistant(string platformId, string assistantId)
    {
        WeaponPlatformData platform = GetPlatform(platformId);
        if (platform == null)
            return;

        platform.CurrentAssistantId = assistantId;
        RecalculateReloadCooldown(platform);
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
        _baseReloadCooldowns.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WeaponPlatformData> platforms =
            JsonConvert.DeserializeObject<List<WeaponPlatformData>>(json);

        if (platforms == null)
            return;

        HashSet<string> loadedPlatformIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (WeaponPlatformData platform in platforms)
        {
            if (platform == null ||
                string.IsNullOrWhiteSpace(platform.PlatformId) ||
                !loadedPlatformIds.Add(platform.PlatformId))
            {
                continue;
            }

            RegisterPlatform(platform);
        }
    }

    private int GetBaseCooldownFromSavedValue(
        WeaponPlatformData platform)
    {
        int savedCooldown = Math.Max(
            1,
            platform.ReloadCooldownSeconds);

        int crewReduction = 0;

        if (!string.IsNullOrWhiteSpace(platform.CurrentGunnerId))
            crewReduction += 2;

        if (!string.IsNullOrWhiteSpace(platform.CurrentAssistantId))
            crewReduction += 1;

        return savedCooldown + crewReduction;
    }

    private void RecalculateReloadCooldown(
        WeaponPlatformData platform)
    {
        if (platform == null ||
            string.IsNullOrWhiteSpace(platform.PlatformId))
        {
            return;
        }

        int baseCooldown;

        if (!_baseReloadCooldowns.TryGetValue(
                platform.PlatformId,
                out baseCooldown))
        {
            baseCooldown = GetBaseCooldownFromSavedValue(platform);
            _baseReloadCooldowns[platform.PlatformId] =
                Math.Max(1, baseCooldown);
        }

        int crewReduction = 0;

        if (!string.IsNullOrWhiteSpace(platform.CurrentGunnerId))
            crewReduction += 2;

        if (!string.IsNullOrWhiteSpace(platform.CurrentAssistantId))
            crewReduction += 1;

        platform.ReloadCooldownSeconds =
            Math.Max(1, baseCooldown - crewReduction);
    }
}