using System;
using System.Collections.Generic;
using System.Linq;

public class TacticalDecisionSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly TacticalSquadAiSystem _squadAiSystem;

    public TacticalDecisionSystem(WorldEntityRegistry registry, TacticalSquadAiSystem squadAiSystem)
    {
        _registry = registry;
        _squadAiSystem = squadAiSystem;
    }

    public string ResolveCombatStyle(string threatType, bool isStealthRequested, bool isCloseRange, bool isMounted, bool isIndoor)
    {
        if (isStealthRequested)
            return "Stealth";
        if (isMounted)
            return isIndoor ? "Lethal Melee" : "Ranged Short";
        if (isIndoor)
            return isCloseRange ? "Lethal Melee" : "Ranged Short";
        if (threatType == "HeavyArmor" || threatType == "Fortified")
            return "Ranged Long";
        if (isCloseRange)
            return "Ranged Short";
        return "Ranged Medium";
    }

    public TacticalOrderData CreateOrder(string squadId, string issuerId, string targetId, string commandType, string combatStyle, string task, bool stealth, bool follow)
    {
        TacticalOrderData order = new TacticalOrderData
        {
            OrderId = Guid.NewGuid().ToString("N"),
            SquadId = squadId,
            IssuerId = issuerId,
            TargetId = targetId,
            CommandType = commandType,
            CombatStyle = combatStyle,
            CurrentTask = task,
            IsStealthMode = stealth,
            IsFollowMode = follow,
            IsCompleted = false
        };

        List<WorldEntityData> squad = string.IsNullOrWhiteSpace(squadId)
            ? new List<WorldEntityData>()
            : _registry.GetFollowers().Where(x => x.FactionId == squadId).ToList();

        foreach (WorldEntityData member in squad)
            order.AssignedMemberIds.Add(member.EntityId);

        _squadAiSystem.RegisterOrder(order);
        ApplyOrderToSquad(order);
        return order;
    }

    public void ApplyOrderToSquad(TacticalOrderData order)
    {
        if (order == null)
            return;

        foreach (string memberId in order.AssignedMemberIds)
        {
            WorldEntityData entity = _registry.GetEntity(memberId);
            if (entity == null)
                continue;

            entity.CurrentTask = order.CurrentTask;
            entity.CombatStyle = order.CombatStyle;
            entity.IsFollowingPlayer = order.IsFollowMode;
        }
    }

    public void MarkCompleted(string orderId)
    {
        TacticalOrderData order = _squadAiSystem.GetOrder(orderId);
        if (order == null)
            return;

        order.IsCompleted = true;
    }

    public void EvaluateEntity(WorldEntityData entity, string threatType, bool isStealthRequested, bool isCloseRange, bool isMounted, bool isIndoor)
    {
        if (entity == null || !entity.IsAlive)
            return;

        entity.CombatStyle = ResolveCombatStyle(threatType, isStealthRequested, isCloseRange, isMounted, isIndoor);

        if (isStealthRequested)
            entity.CurrentTask = "StealthApproach";
        else if (isCloseRange)
            entity.CurrentTask = "CloseQuarters";
        else if (isMounted)
            entity.CurrentTask = "MountedSupport";
        else
            entity.CurrentTask = "AdvanceAndEngage";
    }
}