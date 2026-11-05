using System.Collections.Generic;
using Newtonsoft.Json;

public class LockSystem
{
    private readonly List<LockRecord> _locks =
        new List<LockRecord>();

    public List<LockRecord> GetLocks()
    {
        return new List<LockRecord>(_locks);
    }

    public LockRecord GetLock(string targetId)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            return null;

        return _locks.Find(
            x => x != null && x.LockId == targetId);
    }

    public void RegisterLock(LockRecord lockRecord)
    {
        if (lockRecord == null ||
            string.IsNullOrWhiteSpace(lockRecord.LockId))
        {
            return;
        }

        _locks.RemoveAll(
            x => x != null && x.LockId == lockRecord.LockId);

        _locks.Add(lockRecord);
    }

    public bool HasLock(string targetId)
    {
        return GetLock(targetId) != null;
    }

    public bool CanPick(string targetId)
    {
        LockRecord record = GetLock(targetId);

        return record != null &&
               record.IsLocked &&
               !record.IsPickedOpen &&
               !record.IsBlownOpen;
    }

    public void Pick(string targetId)
    {
        LockRecord record = GetLock(targetId);

        if (record == null ||
            !record.IsLocked ||
            record.IsBlownOpen)
        {
            return;
        }

        record.IsLocked = false;
        record.IsPickedOpen = true;
    }

    public void Break(string targetId)
    {
        LockRecord record = GetLock(targetId);

        if (record == null)
            return;

        record.IsLocked = false;
        record.IsBlownOpen = true;
    }

    public void SetLocked(string targetId, bool locked)
    {
        LockRecord record = GetLock(targetId);

        if (record == null)
            return;

        if (!record.IsPickedOpen && !record.IsBlownOpen)
            record.IsLocked = locked;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            _locks,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _locks.Clear();

        if (string.IsNullOrWhiteSpace(json))
            return;

        List<LockRecord> locks =
            JsonConvert.DeserializeObject<List<LockRecord>>(json);

        if (locks != null)
            _locks.AddRange(locks);
    }
}