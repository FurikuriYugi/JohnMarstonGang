using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;
using System.Linq;

public class NPCAutonomousBehaviorSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly Dictionary<string, NPCBehaviorState> _npcBehaviors = new Dictionary<string, NPCBehaviorState>();
    private readonly Dictionary<string, string> _npcRoles = new Dictionary<string, string>();
    private readonly Random _random = new Random();
    private int _updateCounter = 0;

    private const int BEHAVIOR_UPDATE_INTERVAL = 120; // Update every 120 frames (~4 seconds)
    private const float MOVEMENT_SPEED = 1.5f;
    private const float LOCATION_ARRIVAL_DISTANCE = 5f;

    // Known bank and shop locations
    private readonly Dictionary<string, Vector3> _bankLocations = new Dictionary<string, Vector3>
    {
        { "valentine_bank", new Vector3(214.5f, -907.2f, 43.8f) },
        { "saint_denis_bank", new Vector3(2713.4f, -382.3f, 45.1f) },
        { "blackwater_bank", new Vector3(-799.5f, -1297.3f, 43.3f) }
    };

    private readonly Dictionary<string, Vector3> _shopLocations = new Dictionary<string, Vector3>
    {
        { "valentine_general_store", new Vector3(390.2f, -980.5f, 29.4f) },
        { "saint_denis_general_store", new Vector3(2827.8f, -1349.2f, 45.7f) },
        { "valentine_gunsmith", new Vector3(-302.5f, -922.3f, 31.3f) },
        { "saint_denis_gunsmith", new Vector3(2862.1f, -1440.8f, 45.3f) }
    };

    private readonly Dictionary<string, Vector3> _saloonLocations = new Dictionary<string, Vector3>
    {
        { "valentine_saloon", new Vector3(300.5f, -901.2f, 33.1f) },
        { "saint_denis_saloon", new Vector3(2718.3f, -470.5f, 46.2f) }
    };

    public NPCAutonomousBehaviorSystem(WorldEntityRegistry registry, BankSystem bankSystem, JobSystem jobSystem)
    {
        _registry = registry;
        _bankSystem = bankSystem;
        _jobSystem = jobSystem;
        InitializeNPCRoles();
    }

    private void InitializeNPCRoles()
    {
        _npcRoles["banker"] = "Banker";
        _npcRoles["shopkeeper"] = "Shop Owner";
        _npcRoles["gunsmith"] = "Gunsmith";
        _npcRoles["bartender"] = "Bartender";
        _npcRoles["traveler"] = "Traveler";
        _npcRoles["merchant"] = "Merchant";
        _npcRoles["worker"] = "Worker";
    }

    public void Update(Dictionary<string, Ped> worldNPCs)
    {
        _updateCounter++;
        if (_updateCounter % BEHAVIOR_UPDATE_INTERVAL != 0)
            return;

        _updateCounter = 0;

        if (worldNPCs == null || worldNPCs.Count == 0)
            return;

        foreach (KeyValuePair<string, Ped> npcPair in worldNPCs)
        {
            string npcId = npcPair.Key;
            Ped npcPed = npcPair.Value;

            if (npcPed == null || !npcPed.Exists())
            {
                _npcBehaviors.Remove(npcId);
                continue;
            }

            NPCBehaviorState behavior = EnsureBehaviorState(npcId, npcPed);
            UpdateNPCBehavior(npcId, npcPed, behavior);
        }

        // Generate world events and job opportunities
        GenerateWorldEvents();
    }

    private void UpdateNPCBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        if (behavior == null || npcPed == null || !npcPed.Exists())
            return;

        // Update behavior timer
        behavior.BehaviorTimer--;

        // If behavior is complete, assign new one
        if (behavior.BehaviorTimer <= 0)
        {
            AssignNewBehavior(npcId, npcPed, behavior);
        }

        // Execute current behavior
        ExecuteBehavior(npcId, npcPed, behavior);

        // Update faction relationships based on player actions
        UpdateFactionRelationships(npcId, npcPed, behavior);
    }

    private void AssignNewBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        if (string.IsNullOrWhiteSpace(behavior.Role))
        {
            behavior.Role = DetermineNPCRole(npcPed);
        }

        int roll = _random.Next(0, 100);

        switch (behavior.Role)
        {
            case "Banker":
                if (roll < 40)
                    behavior.CurrentBehavior = "VisitBank";
                else if (roll < 70)
                    behavior.CurrentBehavior = "ManageFunds";
                else
                    behavior.CurrentBehavior = "Idle";
                break;

            case "Shop Owner":
            case "Gunsmith":
                if (roll < 30)
                    behavior.CurrentBehavior = "TransferFundsToBank";
                else if (roll < 60)
                    behavior.CurrentBehavior = "WorkAtShop";
                else if (roll < 80)
                    behavior.CurrentBehavior = "InteractWithOthers";
                else
                    behavior.CurrentBehavior = "Idle";
                break;

            case "Bartender":
                if (roll < 50)
                    behavior.CurrentBehavior = "WorkAtSaloon";
                else if (roll < 75)
                    behavior.CurrentBehavior = "VisitBank";
                else
                    behavior.CurrentBehavior = "Idle";
                break;

            case "Traveler":
                if (roll < 40)
                    behavior.CurrentBehavior = "Travel";
                else if (roll < 60)
                    behavior.CurrentBehavior = "VisitShop";
                else if (roll < 80)
                    behavior.CurrentBehavior = "Rest";
                else
                    behavior.CurrentBehavior = "Idle";
                break;

            case "Merchant":
                if (roll < 35)
                    behavior.CurrentBehavior = "TransferGoodsToBank";
                else if (roll < 65)
                    behavior.CurrentBehavior = "InteractWithOthers";
                else if (roll < 85)
                    behavior.CurrentBehavior = "VisitBank";
                else
                    behavior.CurrentBehavior = "Idle";
                break;

            default:
                if (roll < 50)
                    behavior.CurrentBehavior = "Idle";
                else
                    behavior.CurrentBehavior = "Travel";
                break;
        }

        behavior.BehaviorTimer = _random.Next(180, 600); // 3-10 minutes
        behavior.TargetLocation = SelectTargetLocation(behavior.CurrentBehavior);
    }

    private void ExecuteBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        switch (behavior.CurrentBehavior)
        {
            case "VisitBank":
                ExecuteVisitBankBehavior(npcId, npcPed, behavior);
                break;
            case "ManageFunds":
                ExecuteManageFundsBehavior(npcId, npcPed, behavior);
                break;
            case "TransferFundsToBank":
                ExecuteTransferFundsBehavior(npcId, npcPed, behavior);
                break;
            case "WorkAtShop":
            case "WorkAtSaloon":
                ExecuteWorkBehavior(npcId, npcPed, behavior);
                break;
            case "VisitShop":
                ExecuteVisitShopBehavior(npcId, npcPed, behavior);
                break;
            case "Travel":
                ExecuteTravelBehavior(npcId, npcPed, behavior);
                break;
            case "Rest":
                ExecuteRestBehavior(npcId, npcPed, behavior);
                break;
            case "InteractWithOthers":
                ExecuteInteractionBehavior(npcId, npcPed, behavior);
                break;
            case "TransferGoodsToBank":
                ExecuteTransferGoodsBehavior(npcId, npcPed, behavior);
                break;
            case "Idle":
            default:
                ExecuteIdleBehavior(npcPed);
                break;
        }
    }

    private void ExecuteVisitBankBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomBankLocation();

        // Move toward bank
        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) > LOCATION_ARRIVAL_DISTANCE)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
        }
        else
        {
            // At bank - perform transaction
            if (!behavior.HasProcessedAction)
            {
                ProcessBankTransaction(npcId, npcPed, behavior);
                behavior.HasProcessedAction = true;
            }

            // Stay for a bit then leave
            if (behavior.BehaviorTimer < 100)
            {
                behavior.BehaviorTimer = 0;
            }
        }
    }

    private void ExecuteManageFundsBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // Bankers review accounts and audit transactions
        BankAccountData account = _bankSystem.GetAccount(npcId);

        if (account == null)
        {
            account = _bankSystem.EnsureAccount(npcId, npcPed.GetShorthandName());
        }

        // NPCs accumulate funds naturally over time
        if (_random.Next(0, 100) < 30)
        {
            decimal earnedCash = (decimal)_random.Next(10, 100);
            _bankSystem.DepositCash(npcId, earnedCash);
            behavior.RecordMemory($"Earned {earnedCash} cash from work");
        }

        ExecuteIdleBehavior(npcPed);
    }

    private void ExecuteTransferFundsBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // Shop owners and business NPCs transfer daily earnings to bank
        BankAccountData account = _bankSystem.GetAccount(npcId);

        if (account != null && account.Cash > 100m)
        {
            decimal transferAmount = account.Cash * 0.3m; // Transfer 30% to bank
            _bankSystem.DepositCash(npcId, transferAmount);
            behavior.RecordMemory($"Transferred {transferAmount} cash to bank account");

            // Move toward bank to complete transfer
            if (behavior.TargetLocation == Vector3.Zero)
                behavior.TargetLocation = SelectRandomBankLocation();

            if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) > LOCATION_ARRIVAL_DISTANCE)
            {
                MoveNPCToward(npcPed, behavior.TargetLocation);
            }
        }
    }

    private void ExecuteTransferGoodsBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // Merchants move goods and inventory between locations
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomShopLocation();

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) > LOCATION_ARRIVAL_DISTANCE)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
        }
        else
        {
            // Simulate goods transfer
            if (!behavior.HasProcessedAction)
            {
                behavior.RecordMemory("Transferred goods to/from location");
                behavior.HasProcessedAction = true;
            }
        }
    }

    private void ExecuteWorkBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // NPCs stay at their workplace and periodically gain funds
        if (behavior.TargetLocation == Vector3.Zero)
        {
            if (behavior.Role == "Bartender")
                behavior.TargetLocation = SelectRandomSaloonLocation();
            else
                behavior.TargetLocation = SelectRandomShopLocation();
        }

        // Stay at location
        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) > LOCATION_ARRIVAL_DISTANCE)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
        }
        else
        {
            // Earn money while working
            if (_random.Next(0, 100) < 25)
            {
                decimal earned = (decimal)_random.Next(5, 30);
                BankAccountData account = _bankSystem.EnsureAccount(npcId, npcPed.GetShorthandName());
                account.Cash += earned;
                behavior.RecordMemory($"Earned {earned} cash while working");
            }
        }
    }

    private void ExecuteVisitShopBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // NPCs visit shops to purchase items
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomShopLocation();

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) > LOCATION_ARRIVAL_DISTANCE)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
        }
        else
        {
            // Simulate shopping
            if (!behavior.HasProcessedAction)
            {
                BankAccountData account = _bankSystem.GetAccount(npcId);
                if (account != null && account.Cash > 0m)
                {
                    decimal spent = (decimal)_random.Next(5, 50);
                    if (account.Cash >= spent)
                    {
                        _bankSystem.WithdrawCash(npcId, spent);
                        behavior.RecordMemory($"Spent {spent} cash at shop");
                    }
                }
                behavior.HasProcessedAction = true;
            }
        }
    }

    private void ExecuteTravelBehavior(string npcId, Ped npcPed, NPCBehaviorState behavior)
    {
        // NPCs travel between locations
        if (behavior.TargetLocation == Vector3.Zero)
        {
            int roll = _random.Next(0, 3);
            behavior.TargetLocation = roll == 0
                ? SelectRandomBankLocation()
                : roll == 1
                    ? SelectRandomShopLocation()
                    : SelectRandomSaloonLocation();
        }

        float distance = Vector3.Distance(npcPed.Position, behavior.TargetLocation);
        if (distance > LOCATION_ARRIVAL_DISTANCE)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
        }
        else
        {
            behavior.BehaviorTimer = 0; // Change behavior when arrival reached
        }
    }

    private