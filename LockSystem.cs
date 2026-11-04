using System.Collections.Generic;
using Newtonsoft.Json;

public class LockSystem
{
    private readonly List<LockRecord> _locks = new List<LockRecord>();

    public List<LockRecord> GetLocks()
    {
        return new List<LockRecord>(_locks);
    }

    public LockRecord GetLock(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        return _locks.Find(x => x != null && x.TargetId == targetId);
    }

    public void RegisterLock(LockRecord lockRecord)
    {
        if (lockRecord == null || string.IsNullOrWhiteSpace(lockRecord.TargetId))
            return;

        _locks.RemoveAll(x => x != null && x.TargetId == lockRecord.TargetId);
        _locks.Add(lockRecord);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_locks, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _locks.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<LockRecord> locks = JsonConvert.DeserializeObject<List<LockRecord>>(json);
        if (locks != null)
            _locks.AddRange(locks);
    }
}