<!-- Main lines 1001-1034 of 1034 -->
```csharp
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
