using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RDR2;
using RDR2.Math;

public class InteractionSystem
{
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;

    private readonly Dictionary<int, InteractionLockState> _lockStates =
        new Dictionary<int, InteractionLockState>();

    private readonly Dictionary<int, InteractionDestructibleState> _destructibleStates =
        new Dictionary<int, InteractionDestructibleState>();

    public InteractionSystem(
        LockSystem lockSystem,
        DynamiteSystem dynamiteSystem)
    {
        _lockSystem = lockSystem;
        _dynamiteSystem = dynamiteSystem;
    }

    public void RegisterLock(LockRecord record)
    {
        if (record == null)
        {
            return;
        }

        _lockStates[record.EntityHandle] = new InteractionLockState
        {
            EntityHandle = record.EntityHandle,
            IsLocked = record.IsLocked,
            CanBePicked = record.CanBePicked,
            CanBeBlown = record.CanBeBlown,
            IsOpen = record.IsOpen
        };
    }

    public void RegisterDestructible(DestructibleRecord record)
    {
        if (record == null)
        {
            return;
        }

        _destructibleStates[record.EntityHandle] = new InteractionDestructibleState
        {
            EntityHandle = record.EntityHandle,
            IsDestroyed = record.IsDestroyed,
            Integrity = record.Integrity
        };
    }

    public bool CanPickLock(int entityHandle)
    {
        InteractionLockState state = GetLockState(entityHandle);
        return state != null && state.IsLocked && state.CanBePicked;
    }

    public bool CanBlowOpen(int entityHandle)
    {
        InteractionLockState state = GetLockState(entityHandle);
        return state != null && state.IsLocked && state.CanBeBlown;
    }

    public bool TryPickLock(int entityHandle)
    {
        InteractionLockState state = GetLockState(entityHandle);

        if (state == null || !state.IsLocked || !state.CanBePicked)
        {
            return false;
        }

        state.IsLocked = false;
        state.IsOpen = true;

        if (_lockSystem != null)
        {
            _lockSystem.SetLockState(entityHandle, false, true);
        }

        return true;
    }

    public bool TryBlowOpen(int entityHandle)
    {
        InteractionLockState state = GetLockState(entityHandle);

        if (state == null || !state.IsLocked || !state.CanBeBlown)
        {
            return false;
        }

        state.IsLocked = false;
        state.IsOpen = true;

        if (_dynamiteSystem != null)
        {
            _dynamiteSystem.MarkDestructibleDamaged(entityHandle, 100);
        }

        if (_lockSystem != null)
        {
            _lockSystem.SetLockState(entityHandle, false, true);
        }

        return true;
    }

    public bool DamageDestructible(int entityHandle, int damage)
    {
        InteractionDestructibleState state = GetDestructibleState(entityHandle);

        if (state == null || state.IsDestroyed)
        {
            return false;
        }

        state.Integrity -= Math.Max(0, damage);

        if (state.Integrity <= 0)
        {
            state.Integrity = 0;
            state.IsDestroyed = true;

            if (_dynamiteSystem != null)
            {
                _dynamiteSystem.SetDestroyed(entityHandle, true);
            }

            return true;
        }

        if (_dynamiteSystem != null)
        {
            _dynamiteSystem.SetIntegrity(entityHandle, state.Integrity);
        }

        return false;
    }

    public List<LockRecord> GetLocks()
    {
        List<LockRecord> records = new List<LockRecord>();

        foreach (InteractionLockState state in _lockStates.Values)
        {
            records.Add(new LockRecord
            {
                EntityHandle = state.EntityHandle,
                IsLocked = state.IsLocked,
                CanBePicked = state.CanBePicked,
                CanBeBlown = state.CanBeBlown,
                IsOpen = state.IsOpen
            });
        }

        return records;
    }

    public List<DestructibleRecord> GetDestructibles()
    {
        List<DestructibleRecord> records = new List<DestructibleRecord>();

        foreach (InteractionDestructibleState state in _destructibleStates.Values)
        {
            records.Add(new DestructibleRecord
            {
                EntityHandle = state.EntityHandle,
                IsDestroyed = state.IsDestroyed,
                Integrity = state.Integrity
            });
        }

        return records;
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        InteractionSystemSaveData data =
            JsonConvert.DeserializeObject<InteractionSystemSaveData>(json);

        _lockStates.Clear();
        _destructibleStates.Clear();

        if (data == null)
        {
            return;
        }

        if (data.Locks != null)
        {
            foreach (InteractionLockState state in data.Locks)
            {
                if (state == null)
                {
                    continue;
                }

                _lockStates[state.EntityHandle] = state;
            }
        }

        if (data.Destructibles != null)
        {
            foreach (InteractionDestructibleState state in data.Destructibles)
            {
                if (state == null)
                {
                    continue;
                }

                _destructibleStates[state.EntityHandle] = state;
            }
        }
    }

    public string SaveState()
    {
        InteractionSystemSaveData data = new InteractionSystemSaveData
        {
            Locks = new List<InteractionLockState>(_lockStates.Values),
            Destructibles = new List<InteractionDestructibleState>(_destructibleStates.Values)
        };

        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    private InteractionLockState GetLockState(int entityHandle)
    {
        InteractionLockState state;
        return _lockStates.TryGetValue(entityHandle, out state) ? state : null;
    }

    private InteractionDestructibleState GetDestructibleState(int entityHandle)
    {
        InteractionDestructibleState state;
        return _destructibleStates.TryGetValue(entityHandle, out state) ? state : null;
    }
}

public class InteractionSystemSaveData
{
    public List<InteractionLockState> Locks { get; set; }
    public List<InteractionDestructibleState> Destructibles { get; set; }
}

public class InteractionLockState
{
    public int EntityHandle { get; set; }
    public bool IsLocked { get; set; }
    public bool CanBePicked { get; set; }
    public bool CanBeBlown { get; set; }
    public bool IsOpen { get; set; }
}

public class InteractionDestructibleState
{
    public int EntityHandle { get; set; }
    public bool IsDestroyed { get; set; }
    public int Integrity { get; set; }
}