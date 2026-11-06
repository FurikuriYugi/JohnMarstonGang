using System;
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
        {
            return null;
        }

        return _locks.Find(record =>
            record != null &&
            string.Equals(
                record.LockId,
                targetId,
                StringComparison.Ordinal));
    }

    public void RegisterLock(LockRecord lockRecord)
    {
        if (lockRecord == null ||
            string.IsNullOrWhiteSpace(lockRecord.LockId))
        {
            return;
        }

        _locks.RemoveAll(record =>
            record != null &&
            string.Equals(
                record.LockId,
                lockRecord.LockId,
                StringComparison.Ordinal));

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
               record.IsPickable;
    }

    public bool CanBreak(string targetId)
    {
        LockRecord record = GetLock(targetId);

        return record != null &&
               record.IsLocked &&
               record.IsBypassable;
    }

    public void Pick(string targetId)
    {
        LockRecord record = GetLock(targetId);

        if (record == null ||
            !record.IsLocked ||
            !record.IsPickable)
        {
            return;
        }

        record.IsLocked = false;
    }

    public void Break(string targetId)
    {
        LockRecord record = GetLock(targetId);

        if (record == null ||
            !record.IsLocked ||
            !record.IsBypassable)
        {
            return;
        }

        record.IsLocked = false;
    }

    public void SetLocked(string targetId, bool locked)
    {
        LockRecord record = GetLock(targetId);

        if (record == null)
        {
            return;
        }

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
        {
            return;
        }

        try
        {
            List<LockRecord> locks =
                JsonConvert.DeserializeObject<List<LockRecord>>(json);

            if (locks == null)
            {
                return;
            }

            foreach (LockRecord record in locks)
            {
                RegisterLock(record);
            }
        }
        catch (JsonException)
        {
            _locks.Clear();
        }
    }
}