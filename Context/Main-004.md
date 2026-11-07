<!-- Main lines 751-1000 of 1135 -->
```csharp
        if (_tickCounter % 30 != 0)
        {
            return;
        }

        UpdateEntityAI();
        SaveAll();
    }

    private List<Ped> GetNearbyCombatPeds()
    {
        List<Ped> nearbyEnemies = new List<Ped>();
        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            return nearbyEnemies;
        }

        Ped[] worldPeds = World.GetAllPeds();

        if (worldPeds == null)
        {
            return nearbyEnemies;
        }

        foreach (Ped ped in worldPeds)
        {
            if (ped == null ||
                !ped.Exists() ||
                ped.Handle == playerPed.Handle ||
                ped.IsDeadOrDying ||
                !ped.IsInCombat ||
                IsRuntimeCompanion(ped))
            {
                continue;
            }

            if (ped.Position.DistanceTo(playerPed.Position) > 50f)
            {
                continue;
            }

            nearbyEnemies.Add(ped);
        }

        return nearbyEnemies;
    }

    private void LoadAll()
    {
        _saveRoot = JsonSaveSystem.Load();

        if (_saveRoot == null)
        {
            _saveRoot = new SaveRoot();
        }

        _gangManagementSystem.LoadFromSaveRoot(_saveRoot);
        _jobSystem.LoadFromSaveRoot(_saveRoot);

        if (_saveRoot.World != null &&
            _saveRoot.World.Companions != null)
        {
            _companionSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Companions));
        }

        if (_saveRoot.BankAccounts != null)
        {
            _bankSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.BankAccounts));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Wagons != null)
        {
            _wagonSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Wagons));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.WeaponPlatforms != null)
        {
            _weaponSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.WeaponPlatforms));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Locks != null)
        {
            _lockSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Locks));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Destructibles != null)
        {
            _dynamiteSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.Destructibles));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.TacticalOrders != null)
        {
            _tacticalSquadAiSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.TacticalOrders));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Commands != null)
        {
            _commandSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Commands));
        }

        if (_saveRoot.WorldEntities != null)
        {
            _entityRegistry.LoadState(
                JsonConvert.SerializeObject(_saveRoot.WorldEntities));
        }

        LoadCompanionAIStates();
        SyncInteractionRecordsFromSave();
    }

    private void LoadCompanionAIStates()
    {
        if (_saveRoot == null ||
            _saveRoot.World == null ||
            _saveRoot.World.CompanionAIStates == null)
        {
            return;
        }

        foreach (CompanionAIStateData savedState
                 in _saveRoot.World.CompanionAIStates)
        {
            if (savedState == null ||
                string.IsNullOrWhiteSpace(savedState.CompanionId))
            {
                continue;
            }

            CompanionAIState state = new CompanionAIState
            {
                EntityId = savedState.CompanionId,
                Personality =
                    string.IsNullOrWhiteSpace(savedState.Personality)
                        ? "Neutral"
                        : savedState.Personality,
                Morale = savedState.Morale,
                Fatigue = savedState.Fatigue,
                Hunger = savedState.Hunger,
                Thirst = savedState.Thirst,
                PrefersStealth = savedState.PrefersStealth,
                CurrentGoal = savedState.CurrentGoal,
                LastActivityTimestamp =
                    savedState.LastActivityTimestamp,
                ExperiencePoints = savedState.ExperiencePoints,
                SkillProgression =
                    savedState.SkillProgression ??
                    new Dictionary<string, int>(),
                CompletedJobs =
                    savedState.CompletedJobs ??
                    new List<string>(),
                TotalEarnings = savedState.TotalEarnings,
                BanksVisited = savedState.BanksVisited,
                DepositsWithdrawn = savedState.DepositsWithdrawn
            };

            state.Morale = Clamp(state.Morale, 0, 100);
            state.Fatigue = Clamp(state.Fatigue, 0, 100);
            state.Hunger = Clamp(state.Hunger, 0, 100);
            state.Thirst = Clamp(state.Thirst, 0, 100);

            _coreAISystem.SetAIState(
                savedState.CompanionId,
                state);
        }
    }

    private void SyncInteractionRecordsFromSave()
    {
        if (_saveRoot == null || _saveRoot.World == null)
        {
            return;
        }

        if (_saveRoot.World.Locks != null)
        {
            foreach (LockRecord record in _saveRoot.World.Locks)
            {
                if (record != null)
                {
                    _interactionSystem.RegisterLock(record);
                }
            }
        }

        if (_saveRoot.World.Destructibles != null)
        {
            foreach (DestructibleRecord record
                     in _saveRoot.World.Destructibles)
            {
                if (record != null)
                {
                    _interactionSystem.RegisterDestructible(record);
                }
            }
        }
    }

    private void SyncWorldStateFromSystems()
    {
        if (_saveRoot == null)
        {
            _saveRoot = new SaveRoot();
        }

        if (_saveRoot.World == null)
        {
            _saveRoot.World = new WorldState();
        }

        _gangManagementSystem.WriteToSaveRoot(_saveRoot);

        _saveRoot.World.Companions =
            _companionSystem.GetCompanions();

        _saveRoot.BankAccounts =
            _bankSystem.GetAllAccounts();

        _jobSystem.WriteToSaveRoot(_saveRoot);

        _saveRoot.World.Wagons =
            _wagonSystem.GetWagons();

        _saveRoot.World.WeaponPlatforms =
            _weaponSystem.GetPlatforms();

        _saveRoot.World.Locks =
            _lockSystem.GetLocks();

        _saveRoot.World.Destructibles =
            _dynamiteSystem.GetDestructibles();

```
