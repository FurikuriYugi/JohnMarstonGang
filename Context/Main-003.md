<!-- Main lines 501-750 of 1135 -->
```csharp
        if (ped == null ||
            !ped.Exists() ||
            ped.IsPlayer ||
            (!ped.IsHuman && !ped.IsAnimal) ||
            ped.IsDeadOrDying ||
            ped.IsInVehicle ||
            ped.IsInCombat ||
            ped.IsFleeing ||
            IsRuntimeCompanion(ped))
        {
            return false;
        }

        Ped playerPed = Game.Player.Character;

        return playerPed != null &&
               playerPed.Exists() &&
               ped.Position.DistanceTo(playerPed.Position) <= 5f;
    }

    private void RecruitPed(Ped targetPed)
    {
        if (!IsRecruitmentCandidate(targetPed))
        {
            RDR2.UI.Screen.PrintSubtitle(
                "That person cannot be recruited right now.");
            return;
        }

        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        string companionId = Guid.NewGuid().ToString("N");
        string name =
            targetPed.IsHuman ? "Gang Member" : "Animal Companion";
        string role =
            targetPed.IsHuman ? "Companion" : "Animal Companion";
        string combatStyle =
            targetPed.IsHuman ? "Ranged Medium" : "Animal Support";

        CompanionData companion = new CompanionData
        {
            CompanionId = companionId,
            Name = name,
            Species = targetPed.IsHuman ? "Human" : "Animal",
            IsHuman = targetPed.IsHuman,
            Role = role,
            Personality = "Undetermined",
            Loyalty = 50,
            CurrentCombatStyle = combatStyle,
            CurrentTask = "FollowPlayer",
            IsRecruitable = false,
            IsFollowingPlayer = true
        };

        WorldEntityData entity = new WorldEntityData
        {
            EntityId = companionId,
            EntityType = targetPed.IsHuman ? "Human" : "Animal",
            DisplayName = name,
            FactionId = "PlayerGang",
            IsAlive = true,
            IsRecruitable = false,
            IsFollowingPlayer = true,
            CurrentTask = companion.CurrentTask,
            CombatStyle = companion.CurrentCombatStyle,
            Loyalty = companion.Loyalty
        };

        GangMember gangMember = new GangMember
        {
            MemberId = companionId,
            Name = name,
            Role = role,
            CombatStyle = combatStyle,
            CurrentTask = "FollowPlayer",
            Loyalty = companion.Loyalty,
            IsFollowingPlayer = true
        };

        _companionSystem.RegisterCompanion(companion);
        _entityRegistry.RegisterEntity(entity);
        _gangManagementSystem.RegisterMember(gangMember);
        _runtimeCompanionPeds[companionId] = targetPed;

        targetPed.AlwaysKeepTask = true;
        targetPed.Task.FollowToOffsetOfEntity(
            playerPed,
            new Vector3(0f, -2f, 0f),
            2f,
            -1,
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
        if (ped == null)
        {
            return false;
        }

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
        UpdateRecruitmentPrompt();

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

```
