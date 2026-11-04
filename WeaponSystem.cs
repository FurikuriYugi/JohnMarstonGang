using System.Collections.Generic;
using Newtonsoft.Json;

public class WeaponSystem
{
    private readonly Dictionary<string, WeaponLoadout> _loadouts = new Dictionary<string, WeaponLoadout>();

    public void RegisterLoadout(WeaponLoadout loadout)
    {
        if (loadout == null || string.IsNullOrWhiteSpace(loadout.OwnerId))
            return;

        _loadouts[loadout.OwnerId] = loadout;
    }

    public WeaponLoadout GetLoadout(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return null;

        _loadouts.TryGetValue(ownerId, out WeaponLoadout loadout);
        return loadout;
    }

    public List<WeaponLoadout> GetAllLoadouts()
    {
        return new List<WeaponLoadout>(_loadouts.Values);
    }

    public void LoadLoadouts(List<WeaponLoadout> loadouts)
    {
        _loadouts.Clear();

        if (loadouts == null)
            return;

        foreach (WeaponLoadout loadout in loadouts)
        {
            if (loadout != null && !string.IsNullOrWhiteSpace(loadout.OwnerId))
                _loadouts[loadout.OwnerId] = loadout;
        }
    }

    public void SetCombatStyle(string ownerId, string style)
    {
        WeaponLoadout loadout = GetLoadout(ownerId);
        if (loadout == null || string.IsNullOrWhiteSpace(style))
            return;

        loadout.CombatStyle = style;
    }

    public void EquipPrimary(string ownerId, string weapon)
    {
        WeaponLoadout loadout = GetLoadout(ownerId);
        if (loadout == null || string.IsNullOrWhiteSpace(weapon))
            return;

        loadout.PrimaryWeapon = weapon;
    }

    public void EquipSecondary(string ownerId, string weapon)
    {
        WeaponLoadout loadout = GetLoadout(ownerId);
        if (loadout == null || string.IsNullOrWhiteSpace(weapon))
            return;

        loadout.SecondaryWeapon = weapon;
    }

    public void EquipThrowable(string ownerId, string weapon)
    {
        WeaponLoadout loadout = GetLoadout(ownerId);
        if (loadout == null || string.IsNullOrWhiteSpace(weapon))
            return;

        loadout.ThrowableWeapon = weapon;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllLoadouts(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _loadouts.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<WeaponLoadout> loadouts = JsonConvert.DeserializeObject<List<WeaponLoadout>>(json);
        LoadLoadouts(loadouts);
    }
}

public class WeaponLoadout
{
    public string OwnerId { get; set; }
    public string CombatStyle { get; set; }
    public string PrimaryWeapon { get; set; }
    public string SecondaryWeapon { get; set; }
    public string ThrowableWeapon { get; set; }
    public bool UseSuppressorLogic { get; set; }
    public bool PreferNonLethal { get; set; }
    public bool PreferMelee { get; set; }
}