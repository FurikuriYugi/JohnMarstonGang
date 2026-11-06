<!-- Main lines 751-1000 of 1041 -->
```csharp
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

        _saveRoot.World.TacticalOrders =
            _tacticalSquadAiSystem.GetOrders();

        _saveRoot.World.Commands =
            _commandSystem.GetCommands();

        _saveRoot.WorldEntities =
            _entityRegistry.GetEntities();

        List<CompanionAIStateData> savedAIStates =
            new List<CompanionAIStateData>();

        foreach (WorldEntityData entity
                 in _entityRegistry.GetByFaction("PlayerGang"))
        {
            if (entity == null ||
                string.IsNullOrWhiteSpace(entity.EntityId))
            {
                continue;
            }

            CompanionAIState state =
                _coreAISystem.GetAIState(entity.EntityId);

            if (state == null)
            {
                continue;
            }

            savedAIStates.Add(new CompanionAIStateData
            {
                CompanionId = entity.EntityId,
                Personality = state.Personality,
                Morale = state.Morale,
                Fatigue = state.Fatigue,
                Hunger = state.Hunger,
                Thirst = state.Thirst,
                PrefersStealth = state.PrefersStealth,
                CurrentGoal = state.CurrentGoal,
                LastActivityTimestamp = state.LastActivityTimestamp,
                ExperiencePoints = state.ExperiencePoints,
                SkillProgression =
                    state.SkillProgression ??
                    new Dictionary<string, int>(),
                CompletedJobs =
                    state.CompletedJobs ??
                    new List<string>(),
                TotalEarnings = state.TotalEarnings,
                BanksVisited = state.BanksVisited,
                DepositsWithdrawn = state.DepositsWithdrawn
            });
        }

        _saveRoot.World.CompanionAIStates = savedAIStates;
    }

    private void UpdateEntityAI()
    {
        _commandSystem.ProcessActiveCommands(
            _tacticalDecisionSystem);

        foreach (WorldEntityData entity in _entityRegistry.GetEntities())
        {
            if (entity == null || !entity.IsAlive)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(entity.CombatStyle))
            {
                entity.CombatStyle =
                    _tacticalDecisionSystem.ResolveCombatStyle(
                        "Normal",
                        false,
                        false,
                        false,
                        false);
            }

            if (entity.IsFollowingPlayer &&
                string.IsNullOrWhiteSpace(entity.CurrentTask))
            {
                entity.CurrentTask = "FollowPlayer";
            }

            if (entity.IsRecruitable && entity.Loyalty < 0)
            {
                entity.Loyalty = 0;
            }

            if (entity.SupportSkill < 0)
            {
                entity.SupportSkill = 0;
            }
```
