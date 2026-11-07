<!-- Main lines 1001-1135 of 1135 -->
```csharp
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

            if (entity.CombatSkill < 0)
            {
                entity.CombatSkill = 0;
            }

            if (entity.StealthSkill < 0)
            {
                entity.StealthSkill = 0;
            }

            _gangManagementSystem.ApplyTacticalContext(
                entity.EntityId,
                entity.CombatStyle,
                entity.CurrentTask,
                entity.IsFollowingPlayer);
        }

        foreach (TacticalOrderData order
                 in _tacticalSquadAiSystem.GetOrders())
        {
            if (order == null || order.IsCompleted)
            {
                continue;
            }

            _tacticalDecisionSystem.ApplyOrderToSquad(order);
        }
    }

    private void SaveAll()
    {
        SyncWorldStateFromSystems();
        JsonSaveSystem.Save(_saveRoot);
    }

    private static int Clamp(int value, int minimum, int maximum)
    {
        return Math.Max(minimum, Math.Min(maximum, value));
    }
}
```
