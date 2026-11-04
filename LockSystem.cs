using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LockSystem
{
    private readonly Dictionary<int, LockableTarget> _targets = new Dictionary<int, LockableTarget>();

    public void RegisterTarget(LockableTarget target)
    {
        if (target == null)
            return;

        _targets[target.Id] = target;
    }

    public bool CanPick(int targetId, bool hasBreakerTool)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        LockableTarget target = _targets[targetId];
        return hasBreakerTool && target.IsLocked && target.SupportsPicking;
    }

    public bool PickLock(int targetId)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        LockableTarget target = _targets[targetId];
        if (!target.IsLocked || !target.SupportsPicking)
            return false;

        target.IsLocked = false;
        target.WasPicked = true;
        return true;
    }

    public bool CanBlast(int targetId)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        return _targets[targetId].SupportsExplosion;
    }

    public bool BlastOpen(int targetId)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        LockableTarget target = _targets[targetId];
        if (!target.SupportsExplosion)
            return false;

        target.IsLocked = false;
        target.IsBrokenOpen = true;
        return true;
    }

    public bool Open(int targetId)
    {
        if (!_targets.ContainsKey(targetId))
            return false;

        _targets[targetId].IsLocked = false;
        return true;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_targets.Values, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _targets.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<LockableTarget> targets = JsonConvert.DeserializeObject<List<LockableTarget>>(json);
        if (targets == null)
            return;

        foreach (LockableTarget target in targets)
        {
            if (target != null)
                _targets[target.Id] = target;
        }
    }
}

public class LockableTarget
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TargetType { get; set; }
    public bool IsLocked { get; set; } = true;
    public bool SupportsPicking { get; set; } = true;
    public bool SupportsExplosion { get; set; } = true;
    public bool WasPicked { get; set; }
    public bool IsBrokenOpen { get; set; }
}