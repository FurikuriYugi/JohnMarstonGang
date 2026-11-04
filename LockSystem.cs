using System.Collections.Generic;
using Newtonsoft.Json;

public class LockSystem
{
    private readonly Dictionary<string, LockRecord> _locks = new Dictionary<string, LockRecord>();

    public void RegisterLock(LockRecord lockRecord)
    {
        if (lockRecord == null || string.IsNullOrWhiteSpace(lockRecord.TargetId))
            return;

        _locks[lockRecord.TargetId] = lockRecord;
    }

    public bool HasLock(string targetId)
    {
        return !string.IsNullOrWhiteSpace(targetId) && _locks.ContainsKey(targetId);
    }

    public LockRecord GetLock(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        _locks.TryGetValue(targetId, out LockRecord lockRecord);
        return lockRecord;
    }

    public List<LockRecord> GetAllLocks()
    {
        return new List<LockRecord>(_locks.Values);
    }

    public bool CanPick(string targetId)
    {
        LockRecord lockRecord = GetLock(targetId);
        return lockRecord != null && !lockRecord.IsBroken;
    }

    public void Pick(string targetId)
    {
        LockRecord lockRecord = GetLock(targetId);
        if (lockRecord == null)
            return;

        lockRecord.IsLocked = false;
        lockRecord.IsBroken = false;
    }

    public void Break(string targetId)
    {
        LockRecord lockRecord = GetLock(targetId);
        if (lockRecord == null)
            return;

        lockRecord.IsLocked = false;
        lockRecord.IsBroken = true;
    }

    public void Relock(string targetId)
    {
        LockRecord lockRecord = GetLock(targetId);
        if (lockRecord == null)
            return;

        lockRecord.IsLocked = true;
        lockRecord.IsBroken = false;
    }

    public void LoadLocks(List<LockRecord> locks)
    {
        _locks.Clear();

        if (locks == null)
            return;

        foreach (LockRecord lockRecord in locks)
        {
            if (lockRecord != null && !string.IsNullOrWhiteSpace(lockRecord.TargetId))
                _locks[lockRecord.TargetId] = lockRecord;
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(GetAllLocks(), Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _locks.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<LockRecord> locks = JsonConvert.DeserializeObject<List<LockRecord>>(json);
        LoadLocks(locks);
    }
}

public class LockRecord
{
    public string TargetId { get; set; }
    public string TargetType { get; set; }
    public bool IsLocked { get; set; }
    public bool IsBroken { get; set; }
    public bool CanBePicked { get; set; }
    public bool CanBeBlasted { get; set; }
}