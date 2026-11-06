<!-- Main lines 501-750 of 1034 -->
```csharp
            3f,
            true);

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            targetPed.IsHuman
                ? "Gang member recruited. They will follow you."
                : "Animal companion recruited. They will follow you.");
    }

    private bool IsRuntimeCompanion(Ped ped)
    {
        foreach (Ped companionPed in _runtimeCompanionPeds.Values)
        {
            if (companionPed != null &&
                companionPed.Exists() &&
                companionPed.Handle == ped.Handle)
            {
                return true;
            }
        }

        return false;
    }

    private void ToggleCompanionFollowState()
    {
        bool hasFollowingCompanion = false;
        bool hasRuntimeCompanion = false;

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            if (pair.Value == null || !pair.Value.Exists())
            {
                continue;
            }

            hasRuntimeCompanion = true;

            WorldEntityData entity =
                _entityRegistry.GetEntity(pair.Key);

            if (entity != null && entity.IsFollowingPlayer)
            {
                hasFollowingCompanion = true;
            }
        }

        if (!hasRuntimeCompanion)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "No recruited companions are available in this session.");
            return;
        }

        bool shouldFollow = !hasFollowingCompanion;
        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow &&
            (playerPed == null || !playerPed.Exists()))
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            Ped companionPed = pair.Value;

            if (companionPed == null || !companionPed.Exists())
            {
                continue;
            }

            string task = shouldFollow ? "FollowPlayer" : "Stay";

            WorldEntityData entity =
                _entityRegistry.GetEntity(pair.Key);

            if (entity != null)
            {
                entity.IsFollowingPlayer = shouldFollow;
                entity.CurrentTask = task;
            }

            CompanionData companion =
                _companionSystem.GetCompanion(pair.Key);

            if (companion != null)
            {
                companion.IsFollowingPlayer = shouldFollow;
                companion.CurrentTask = task;
            }

            _gangManagementSystem.ApplyTacticalContext(
                pair.Key,
                companion != null ? companion.CurrentCombatStyle : null,
                task,
                shouldFollow);

            companionPed.AlwaysKeepTask = true;

            if (shouldFollow)
            {
                companionPed.Task.FollowToOffsetOfEntity(
                    playerPed,
                    new Vector3(0f, -2f, 0f),
                    2f,
                    -1,
                    3f,
                    true);
            }
            else
            {
                companionPed.Task.StandStill(-1);
            }
        }

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            shouldFollow
                ? "Gang members are following you."
                : "Gang members will stay here.");
    }

    private void OnTick(object sender, EventArgs e)
    {
        UpdateInteractionMenu();

        _tickCounter++;

        if (_tickCounter % 30 == 0)
        {
            _cachedNearbyEnemies = GetNearbyCombatPeds();
        }

        Ped playerPed = Game.Player.Character;

        if (playerPed != null && playerPed.Exists())
        {
            _coreAISystem.Update(
                playerPed,
                _runtimeCompanionPeds,
                playerPed.IsInCombat,
                _cachedNearbyEnemies);
        }

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
```
