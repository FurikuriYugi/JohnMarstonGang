<!-- Main lines 251-500 of 1041 -->
```csharp
        bool hasTarget =
            RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
                Game.Player.Handle,
                &entityHandle,
                false,
                false);

        if (!hasTarget || entityHandle == 0)
        {
            return null;
        }

        return Entity.FromHandle(entityHandle) as Ped;
    }

    private bool IsInteractionMenuTarget(Ped ped)
    {
        return GetRuntimeCompanionId(ped) != null ||
               IsRecruitmentCandidate(ped);
    }

    private void ExecuteInteractionMenuOption()
    {
        Ped currentTarget = GetInteractionTargetPed();

        if (!_interactionMenuVisible ||
            _interactionTargetPed == null ||
            currentTarget == null ||
            !currentTarget.Exists() ||
            currentTarget.Handle != _interactionTargetHandle)
        {
            return;
        }

        if (string.IsNullOrEmpty(_interactionTargetCompanionId))
        {
            if (_selectedInteractionOption == 0)
            {
                RecruitPed(currentTarget);
            }

            return;
        }

        if (_selectedInteractionOption == 0)
        {
            ToggleCompanionFollowState(
                _interactionTargetCompanionId,
                currentTarget);
        }
        else if (_selectedInteractionOption == 1)
        {
            DismissCompanion(
                _interactionTargetCompanionId,
                currentTarget);
        }
    }

    private void ToggleCompanionFollowState(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        WorldEntityData entity =
            _entityRegistry.GetEntity(companionId);

        CompanionData companion =
            _companionSystem.GetCompanion(companionId);

        bool shouldFollow = entity != null
            ? !entity.IsFollowingPlayer
            : companion != null && !companion.IsFollowingPlayer;

        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow &&
            (playerPed == null || !playerPed.Exists()))
        {
            return;
        }

        string task = shouldFollow ? "FollowPlayer" : "Stay";

        if (entity != null)
        {
            entity.IsFollowingPlayer = shouldFollow;
            entity.CurrentTask = task;
        }

        if (companion != null)
        {
            companion.IsFollowingPlayer = shouldFollow;
            companion.CurrentTask = task;
        }

        _gangManagementSystem.ApplyTacticalContext(
            companionId,
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

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            shouldFollow
                ? "Gang member is following you."
                : "Gang member will stay here.");
    }

    private void DismissCompanion(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        companionPed.Task.ClearTasks(true);
        companionPed.AlwaysKeepTask = false;

        _runtimeCompanionPeds.Remove(companionId);
        _companionSystem.RemoveCompanion(companionId);
        _entityRegistry.RemoveEntity(companionId);

        if (_gangManagementSystem.MemberExists(companionId))
        {
            _gangManagementSystem.RemoveMember(companionId);
        }

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle("Gang member dismissed.");
    }

    private bool IsRecruitmentCandidate(Ped ped)
    {
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
```
