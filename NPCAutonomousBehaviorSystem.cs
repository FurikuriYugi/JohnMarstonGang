using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;
using System.Linq;

public class NPCAutonomousBehaviorSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly Dictionary<string, NPCBehaviorState> _npcBehaviors = new Dictionary<string, NPCBehaviorState>();
    private readonly Random _random = new Random();

    private const int BEHAVIOR_UPDATE_INTERVAL = 60; // Update every 60 frames (~2 seconds)
    private int _updateCounter = 0;

    // Location identifiers for known shops and banks
    private readonly HashSet<string> _bankLocations = new HashSet<string>
    {
        "valentine_bank", "saint_denis_bank", "blackwater_bank", "armadillo_bank", "strawberry_bank"
    };

    private readonly HashSet<string> _shopLocations = new HashSet<string>
    {
        "valentine_general_store", "saint_denis_general_store", "blackwater_general_store",
        "valentine_gunsmith", "saint_denis_gunsmith", "blackwater_gunsmith",
        "valentine_saloon", "saint_denis_saloon", "blackwater_saloon"
    };

    private readonly Dictionary<string, string> _npcRoles = new Dictionary<string, string>
    {
        { "banker", "Works at bank" },
        { "shopkeeper", "Runs general store