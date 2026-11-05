using RDR2;
using System;
using System.Collections.Generic;

public class CoreAISystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly TacticalDecisionSystem _tacticalDecisionSystem;
    private readonly BankSystem _bankSystem;
    private readonly Dictionary<string, CompanionAIState> _aiStates = new Dictionary<string, CompanionAIState>();
    private readonly Random _random = new Random();
    private const int UPDATE_INTERVAL = 30; // Frames between AI updates

    public CoreAISystem(WorldEntityRegistry registry, TacticalDecisionSystem tacticalDecisionSystem, BankSystem bankSystem)
    {
        _registry = registry;
        _tacticalDecisionSystem = tacticalDecisionSystem;
        _bankSystem = bankSystem;
    }

    public void Update(Ped playerPed, List<Ped> companionPeds, bool playerInCombat, List<Ped> nearbyEnemies)