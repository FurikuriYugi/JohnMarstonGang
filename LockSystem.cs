// LockSystem.cs
using Newtonsoft.Json;
using RDR2;
using RDR2.Native;
using RDR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Xml;

public class LockedTarget
{
    public int EntityHandle { get; set; }
    public string TargetType { get; set; } // Door, Safe, Chest
    public bool IsLocked { get; set; }
    public bool IsBroken { get; set; }
}

public class LockSystem
{
    private readonly Dictionary<int, LockedTarget> _targets = new Dictionary<int, LockedTarget>();

    public void RegisterTarget(int entityHandle, string type, bool locked = true)
    {
        if (entityHandle == 0) return;

        _targets[entityHandle] = new LockedTarget
        {
            EntityHandle = entityHandle,
            TargetType = type,
            IsLocked = locked,
            IsBroken = false
        };
    }

    public bool TryPickLock(Ped player, int entityHandle)
    {
        if (player == null || !player.Exists()) return false;
        if (!_targets.TryGetValue(entityHandle, out LockedTarget target)) return false;
        if (!target.IsLocked || target.IsBroken) return false;

        int breakerTool = Game.GenerateHash("p_breaker_tool01x");
        if (!player.Inventory.HasItem(breakerTool))
        {
            UI.Notify("You need a breaker tool.");
            return false;
        }

        player.Task.PlayAnimation("script_re@crouch@", "lockpick_idle", 2.0f, -1, AnimationFlags.None);
        target.IsLocked = false;
        UI.Notify($"{target.TargetType} unlocked.");
        Save();
        return true;
    }

    public bool ForceOpenWithDynamite(Ped player, int entityHandle)
    {
        if (player == null || !player.Exists()) return false;
        if (!_targets.TryGetValue(entityHandle, out LockedTarget target)) return false;
        if (target.IsBroken) return false;

        int dynamiteHash = Game.GenerateHash("weapon_thrown_dynamite");
        if (!player.Inventory.HasItem(dynamiteHash))
        {
            UI.Notify("You need dynamite.");
            return false;
        }

        target.IsLocked = false;
        target.IsBroken = true;

        Vector3 pos = Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, entityHandle, true, true);
        Function.Call(Hash.ADD_EXPLOSION, pos.X, pos.Y, pos.Z, 23, 1.0f, true, false, 1.0f);

        UI.Notify($"{target.TargetType} destroyed.");
        Save();
        return true;
    }

    public bool IsLocked(int entityHandle)
    {
        if (_targets.TryGetValue(entityHandle, out LockedTarget target))
            return target.IsLocked && !target.IsBroken;
        return false;
    }

    public void Save()
    {
        try
        {
            string path = "scripts\\JohnsGangLocks.json";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(path, JsonConvert.SerializeObject(_targets, Formatting.Indented));
        }
        catch { }
    }

    public void Load()
    {
        try
        {
            string path = "scripts\\JohnsGangLocks.json";
            if (!File.Exists(path)) return;

            var loaded = JsonConvert.DeserializeObject<Dictionary<int, LockedTarget>>(File.ReadAllText(path));
            if (loaded == null) return;

            _targets.Clear();
            foreach (var kv in loaded)
                _targets[kv.Key] = kv.Value;
        }
        catch { }
    }
}