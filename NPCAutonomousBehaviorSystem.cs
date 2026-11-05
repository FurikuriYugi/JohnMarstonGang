using RDR2;
using RDR2.Math;
using System;
using System.Collections.Generic;

public class NPCAutonomousBehaviorSystem
{
    private readonly WorldEntityRegistry _registry;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly Dictionary<string, NPCBehaviorState> _npcBehaviors =
        new Dictionary<string, NPCBehaviorState>();
    private readonly Dictionary<string, string> _npcRoles =
        new Dictionary<string, string>();
    private readonly Random _random = new Random();
    private Dictionary<string, Ped> _currentWorldNPCs =
        new Dictionary<string, Ped>();
    private int _updateCounter;
    private int _worldEventCooldown;

    private const int BehaviorUpdateInterval = 120;
    private const int WorldEventCooldownUpdates = 150;
    private const float LocationArrivalDistance = 5f;
    private const float InteractionDistance = 3f;

    private readonly Dictionary<string, Vector3> _bankLocations =
        new Dictionary<string, Vector3>
        {
            { "valentine_bank", new Vector3(214.5f, -907.2f, 43.8f) },
            { "saint_denis_bank", new Vector3(2713.4f, -382.3f, 45.1f) },
            { "blackwater_bank", new Vector3(-799.5f, -1297.3f, 43.3f) }
        };

    private readonly Dictionary<string, Vector3> _shopLocations =
        new Dictionary<string, Vector3>
        {
            { "valentine_general_store", new Vector3(390.2f, -980.5f, 29.4f) },
            { "saint_denis_general_store", new Vector3(2827.8f, -1349.2f, 45.3f) },
            { "valentine_gunsmith", new Vector3(-302.5f, -922.3f, 31.3f) },
            { "saint_denis_gunsmith", new Vector3(2862.1f, -1440.8f, 45.3f) }
        };

    private readonly Dictionary<string, Vector3> _saloonLocations =
        new Dictionary<string, Vector3>
        {
            { "valentine_saloon", new Vector3(300.5f, -901.2f, 33.1f) },
            { "saint_denis_saloon", new Vector3(2718.3f, -470.5f, 46.2f) }
        };

    public NPCAutonomousBehaviorSystem(
        WorldEntityRegistry registry,
        BankSystem bankSystem,
        JobSystem jobSystem)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _bankSystem = bankSystem ?? throw new ArgumentNullException(nameof(bankSystem));
        _jobSystem = jobSystem ?? throw new ArgumentNullException(nameof(jobSystem));
        InitializeNPCRoles();
    }

    private void InitializeNPCRoles()
    {
        _npcRoles["banker"] = "Banker";
        _npcRoles["shopkeeper"] = "Shop Owner";
        _npcRoles["shop owner"] = "Shop Owner";
        _npcRoles["gunsmith"] = "Gunsmith";
        _npcRoles["bartender"] = "Bartender";
        _npcRoles["traveler"] = "Traveler";
        _npcRoles["merchant"] = "Merchant";
        _npcRoles["worker"] = "Worker";
    }

    public void Update(Dictionary<string, Ped> worldNPCs)
    {
        _updateCounter++;
        if (_updateCounter < BehaviorUpdateInterval)
            return;

        _updateCounter = 0;

        if (worldNPCs == null)
        {
            _currentWorldNPCs = new Dictionary<string, Ped>();
            return;
        }

        _currentWorldNPCs = worldNPCs;
        RemoveMissingOrInvalidNPCStates(worldNPCs);

        foreach (KeyValuePair<string, Ped> npcPair in worldNPCs)
        {
            string npcId = npcPair.Key;
            Ped npcPed = npcPair.Value;

            if (string.IsNullOrWhiteSpace(npcId) ||
                npcPed == null ||
                !npcPed.Exists() ||
                npcPed.IsDeadOrDying)
            {
                continue;
            }

            NPCBehaviorState behavior = EnsureBehaviorState(npcId, npcPed);
            UpdateNPCBehavior(npcId, npcPed, behavior);
        }

        GenerateWorldEvents();
    }

    public NPCBehaviorState GetBehaviorState(string npcId)
    {
        if (string.IsNullOrWhiteSpace(npcId))
            return null;

        NPCBehaviorState state;
        return _npcBehaviors.TryGetValue(npcId, out state) ? state : null;
    }

    public void RecordPlayerInteraction(
        string npcId,
        bool positiveInteraction,
        string memory)
    {
        if (string.IsNullOrWhiteSpace(npcId))
            return;

        NPCBehaviorState behavior;
        if (!_npcBehaviors.TryGetValue(npcId, out behavior))
            return;

        behavior.RecordMemory(
            string.IsNullOrWhiteSpace(memory)
                ? (positiveInteraction
                    ? "Had a positive interaction with the player."
                    : "Had a negative interaction with the player.")
                : memory);

        WorldEntityData entity = _registry.GetEntity(npcId);
        if (entity == null)
            return;

        int change = positiveInteraction ? 5 : -10;
        entity.Loyalty = Clamp(entity.Loyalty + change, 0, 100);
        _registry.UpdateEntity(entity);
    }

    private void RemoveMissingOrInvalidNPCStates(Dictionary<string, Ped> worldNPCs)
    {
        List<string> idsToRemove = new List<string>();

        foreach (KeyValuePair<string, NPCBehaviorState> statePair in _npcBehaviors)
        {
            Ped ped;
            if (!worldNPCs.TryGetValue(statePair.Key, out ped) ||
                ped == null ||
                !ped.Exists() ||
                ped.IsDeadOrDying)
            {
                idsToRemove.Add(statePair.Key);
            }
        }

        for (int i = 0; i < idsToRemove.Count; i++)
            _npcBehaviors.Remove(idsToRemove[i]);
    }

    private NPCBehaviorState EnsureBehaviorState(string npcId, Ped npcPed)
    {
        NPCBehaviorState behavior;
        if (_npcBehaviors.TryGetValue(npcId, out behavior))
            return behavior;

        behavior = new NPCBehaviorState
        {
            NpcId = npcId,
            Role = DetermineNPCRole(npcId, npcPed),
            CurrentBehavior = "Idle",
            BehaviorTimer = 0,
            TargetLocation = Vector3.Zero,
            HasProcessedAction = false,
            CashOnHand = 0m,
            BanksVisited = 0,
            DepositsWithdrawn = 0
        };

        _npcBehaviors.Add(npcId, behavior);
        return behavior;
    }

    private void UpdateNPCBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior == null || npcPed == null || !npcPed.Exists())
            return;

        behavior.BehaviorTimer--;
        if (behavior.BehaviorTimer <= 0)
            AssignNewBehavior(npcId, npcPed, behavior);

        ExecuteBehavior(npcId, npcPed, behavior);

        WorldEntityData entity = _registry.GetEntity(npcId);
        if (entity != null)
        {
            entity.Loyalty = Clamp(entity.Loyalty, 0, 100);
            _registry.UpdateEntity(entity);
        }
    }

    private void AssignNewBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (string.IsNullOrWhiteSpace(behavior.Role))
            behavior.Role = DetermineNPCRole(npcId, npcPed);

        int roll = _random.Next(0, 100);

        switch (behavior.Role)
        {
            case "Banker":
                behavior.CurrentBehavior = roll < 40
                    ? "VisitBank"
                    : roll < 75 ? "ManageFunds" : "Idle";
                break;

            case "Shop Owner":
            case "Gunsmith":
                behavior.CurrentBehavior = roll < 30
                    ? "TransferFundsToBank"
                    : roll < 65 ? "WorkAtShop"
                    : roll < 85 ? "InteractWithOthers"
                    : "Idle";
                break;

            case "Bartender":
                behavior.CurrentBehavior = roll < 55
                    ? "WorkAtSaloon"
                    : roll < 75 ? "VisitBank" : "Idle";
                break;

            case "Traveler":
                behavior.CurrentBehavior = roll < 40
                    ? "Travel"
                    : roll < 65 ? "VisitShop"
                    : roll < 85 ? "Rest"
                    : "Idle";
                break;

            case "Merchant":
                behavior.CurrentBehavior = roll < 35
                    ? "TransferGoodsToBank"
                    : roll < 65 ? "InteractWithOthers"
                    : roll < 85 ? "VisitBank"
                    : "Idle";
                break;

            default:
                behavior.CurrentBehavior = roll < 65 ? "Idle" : "Travel";
                break;
        }

        behavior.BehaviorTimer = _random.Next(45, 151);
        behavior.TargetLocation =
            SelectTargetLocation(behavior.CurrentBehavior, behavior.Role);
        behavior.HasProcessedAction = false;
        behavior.InteractionTargetId = null;
    }

    private void ExecuteBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
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
                ExecuteTravelBehavior(npcPed, behavior);
                break;

            case "Rest":
                ExecuteRestBehavior(npcPed);
                break;

            case "InteractWithOthers":
                ExecuteInteractionBehavior(npcId, npcPed, behavior);
                break;

            case "TransferGoodsToBank":
                ExecuteTransferGoodsBehavior(npcPed, behavior);
                break;

            default:
                ExecuteIdleBehavior(npcPed);
                break;
        }
    }

    private void ExecuteVisitBankBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomLocation(_bankLocations);

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        if (!behavior.HasProcessedAction)
        {
            ProcessBankTransaction(npcId, behavior);
            behavior.HasProcessedAction = true;
        }

        if (behavior.BehaviorTimer < 10)
            behavior.BehaviorTimer = 0;
    }

    private void ExecuteManageFundsBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        _bankSystem.EnsureAccount(npcId, GetNPCDisplayName(npcId));

        if (_random.Next(0, 100) < 30)
        {
            decimal earnedCash = _random.Next(10, 100);
            behavior.CashOnHand += earnedCash;
            behavior.RecordMemory(
                "Earned " + earnedCash + " cash from business activity.");
        }

        ExecuteIdleBehavior(npcPed);
    }

    private void ExecuteTransferFundsBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomLocation(_bankLocations);

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        if (behavior.HasProcessedAction)
            return;

        if (behavior.CashOnHand > 0m)
        {
            decimal transferAmount = behavior.CashOnHand * 0.30m;
            if (transferAmount < 1m)
                transferAmount = behavior.CashOnHand;

            _bankSystem.EnsureAccount(npcId, GetNPCDisplayName(npcId));
            _bankSystem.DepositCash(npcId, transferAmount);
            behavior.CashOnHand -= transferAmount;
            behavior.BanksVisited++;
            behavior.RecordMemory(
                "Deposited " + transferAmount + " cash into a bank account.");
        }
        else
        {
            behavior.RecordMemory(
                "Visited a bank but had no business cash to deposit.");
        }

        behavior.HasProcessedAction = true;
    }

    private void ExecuteTransferGoodsBehavior(
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomLocation(_shopLocations);

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        if (!behavior.HasProcessedAction)
        {
            behavior.RecordMemory(
                "Completed a goods transfer between business locations.");
            behavior.HasProcessedAction = true;
        }
    }

    private void ExecuteWorkBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
        {
            behavior.TargetLocation = behavior.Role == "Bartender"
                ? SelectRandomLocation(_saloonLocations)
                : SelectRandomLocation(_shopLocations);
        }

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        if (_random.Next(0, 100) < 25)
        {
            decimal earned = _random.Next(5, 30);
            behavior.CashOnHand += earned;
            _bankSystem.EnsureAccount(npcId, GetNPCDisplayName(npcId));
            behavior.RecordMemory("Earned " + earned + " cash while working.");
        }
    }

    private void ExecuteVisitShopBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomLocation(_shopLocations);

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        if (behavior.HasProcessedAction)
            return;

        decimal purchaseCost = _random.Next(5, 50);

        if (behavior.CashOnHand < purchaseCost)
        {
            BankAccountData account = _bankSystem.GetAccount(npcId);
            if (account != null && account.Cash >= purchaseCost)
            {
                if (_bankSystem.WithdrawCash(npcId, purchaseCost))
                {
                    behavior.DepositsWithdrawn++;
                    behavior.RecordMemory(
                        "Withdrew " + purchaseCost + " cash for a purchase.");
                    behavior.RecordMemory(
                        "Spent " + purchaseCost + " cash at a shop.");
                }
                else
                {
                    behavior.RecordMemory(
                        "Could not withdraw cash for a shop purchase.");
                }
            }
            else
            {
                behavior.RecordMemory(
                    "Visited a shop but could not afford a purchase.");
            }
        }
        else
        {
            behavior.CashOnHand -= purchaseCost;
            behavior.RecordMemory(
                "Spent " + purchaseCost + " cash at a shop.");
        }

        behavior.HasProcessedAction = true;
    }

    private void ExecuteTravelBehavior(
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        if (behavior.TargetLocation == Vector3.Zero)
            behavior.TargetLocation = SelectRandomTravelLocation();

        if (Vector3.Distance(npcPed.Position, behavior.TargetLocation) >
            LocationArrivalDistance)
        {
            MoveNPCToward(npcPed, behavior.TargetLocation);
            return;
        }

        behavior.BehaviorTimer = 0;
    }

    private void ExecuteRestBehavior(Ped npcPed)
    {
        if (npcPed != null && npcPed.Exists())
            npcPed.Task.StandStill(-1);
    }

    private void ExecuteInteractionBehavior(
        string npcId,
        Ped npcPed,
        NPCBehaviorState behavior)
    {
        Ped otherPed = null;
        string otherNpcId = null;
        float closestDistance = float.MaxValue;

        foreach (KeyValuePair<string, Ped> pair in _currentWorldNPCs)
        {
            if (pair.Key == npcId ||
                pair.Value == null ||
                !pair.Value.Exists() ||
                pair.Value.IsDeadOrDying)
            {
                continue;
            }

            float distance =
                Vector3.Distance(npcPed.Position, pair.Value.Position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                otherPed = pair.Value;
                otherNpcId = pair.Key;
            }
        }

        if (otherPed == null)
        {
            ExecuteIdleBehavior(npcPed);
            return;
        }

        behavior.InteractionTargetId = otherNpcId;

        if (closestDistance > InteractionDistance)
        {
            MoveNPCToward(npcPed, otherPed.Position);
            return;
        }

        if (!behavior.HasProcessedAction)
        {
            behavior.RecordMemory("Spoke with another person in the world.");

            NPCBehaviorState otherBehavior;
            if (_npcBehaviors.TryGetValue(otherNpcId, out otherBehavior))
            {
                otherBehavior.RecordMemory(
                    "Spoke with " + GetNPCDisplayName(npcId) + ".");
            }

            behavior.HasProcessedAction = true;
        }

        npcPed.Task.StandStill(-1);
    }

    private void ExecuteIdleBehavior(Ped npcPed)
    {
        if (npcPed != null && npcPed.Exists())
            npcPed.Task.StandStill(-1);
    }

    private void ProcessBankTransaction(
        string npcId,
        NPCBehaviorState behavior)
    {
        BankAccountData account = _bankSystem.EnsureAccount(
            npcId,
            GetNPCDisplayName(npcId));

        if (behavior.CashOnHand > 0m)
        {
            decimal depositAmount = behavior.CashOnHand;
            _bankSystem.DepositCash(npcId, depositAmount);
            behavior.CashOnHand = 0m;
            behavior.BanksVisited++;
            behavior.RecordMemory(
                "Deposited " + depositAmount + " cash at a bank.");
            return;
        }

        if (account.Cash > 0m)
        {
            decimal withdrawalAmount =
                Math.Min(account.Cash, _random.Next(5, 50));

            if (_bankSystem.WithdrawCash(npcId, withdrawalAmount))
            {
                behavior.CashOnHand += withdrawalAmount;
                behavior.DepositsWithdrawn++;
                behavior.RecordMemory(
                    "Withdrew " + withdrawalAmount + " cash from a bank.");
                return;
            }
        }

        behavior.BanksVisited++;
        behavior.RecordMemory("Visited a bank and checked the account.");
    }

    private string DetermineNPCRole(string npcId, Ped npcPed)
    {
        string identity = npcId ?? string.Empty;
        WorldEntityData entity = _registry.GetEntity(npcId);

        if (entity != null)
        {
            identity += " " + (entity.DisplayName ?? string.Empty);
            identity += " " + (entity.EntityType ?? string.Empty);
            identity += " " + (entity.LocationId ?? string.Empty);
        }

        if (npcPed != null && npcPed.Exists())
            identity += " " + GetNPCDisplayName(npcId);

        foreach (KeyValuePair<string, string> role in _npcRoles)
        {
            if (identity.IndexOf(
                    role.Key,
                    StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return role.Value;
            }
        }

        if (identity.IndexOf("bank", StringComparison.OrdinalIgnoreCase) >= 0)
            return "Banker";

        if (identity.IndexOf("shop", StringComparison.OrdinalIgnoreCase) >= 0)
            return "Shop Owner";

        if (identity.IndexOf("store", StringComparison.OrdinalIgnoreCase) >= 0)
            return "Shop Owner";

        return "Traveler";
    }

    private string GetNPCDisplayName(string npcId)
    {
        WorldEntityData entity = _registry.GetEntity(npcId);

        if (entity != null && !string.IsNullOrWhiteSpace(entity.DisplayName))
            return entity.DisplayName;

        return string.IsNullOrWhiteSpace(npcId) ? "Unknown NPC" : npcId;
    }

    private Vector3 SelectTargetLocation(string behavior, string role)
    {
        switch (behavior)
        {
            case "VisitBank":
            case "TransferFundsToBank":
                return SelectRandomLocation(_bankLocations);

            case "VisitShop":
            case "WorkAtShop":
            case "TransferGoodsToBank":
                return SelectRandomLocation(_shopLocations);

            case "WorkAtSaloon":
                return SelectRandomLocation(_saloonLocations);

            case "Travel":
                return SelectRandomTravelLocation();

            default:
                if (role == "Banker")
                    return SelectRandomLocation(_bankLocations);
                return Vector3.Zero;
        }
    }

    private Vector3 SelectRandomTravelLocation()
    {
        int choice = _random.Next(0, 3);

        if (choice == 0)
            return SelectRandomLocation(_bankLocations);

        if (choice == 1)
            return SelectRandomLocation(_shopLocations);

        return SelectRandomLocation(_saloonLocations);
    }

    private Vector3 SelectRandomLocation(
        Dictionary<string, Vector3> locations)
    {
        if (locations == null || locations.Count == 0)
            return Vector3.Zero;

        int selectedIndex = _random.Next(0, locations.Count);
        int index = 0;

        foreach (KeyValuePair<string, Vector3> location in locations)
        {
            if (index == selectedIndex)
                return location.Value;

            index++;
        }

        return Vector3.Zero;
    }

    private void MoveNPCToward(Ped npcPed, Vector3 targetLocation)
    {
        if (npcPed == null || !npcPed.Exists())
            return;

        if (Vector3.Distance(npcPed.Position, targetLocation) <=
            LocationArrivalDistance)
        {
            return;
        }

        RDR2.Native.TASK.TASK_GO_TO_COORD_ANY_MEANS(
            npcPed.Handle,
            targetLocation.X,
            targetLocation.Y,
            targetLocation.Z,
            1.0f,
            0,
            false,
            0,
            0.0f);
    }

    private void GenerateWorldEvents()
    {
        if (_worldEventCooldown > 0)
        {
            _worldEventCooldown--;
            return;
        }

        foreach (KeyValuePair<string, NPCBehaviorState> pair in _npcBehaviors)
        {
            NPCBehaviorState behavior = pair.Value;

            if (behavior == null ||
                (behavior.Role != "Banker" &&
                 behavior.Role != "Merchant" &&
                 behavior.Role != "Shop Owner"))
            {
                continue;
            }

            bool sponsorAlreadyHasAvailableJob = false;
            List<JobData> availableJobs = _jobSystem.GetAvailableJobs();

            for (int i = 0; i < availableJobs.Count; i++)
            {
                JobData job = availableJobs[i];
                if (job != null && job.SponsorId == pair.Key)
                {
                    sponsorAlreadyHasAvailableJob = true;
                    break;
                }
            }

            if (sponsorAlreadyHasAvailableJob)
                continue;

            WorldEntityData sponsor = _registry.GetEntity(pair.Key);
            string sponsorName =
                sponsor != null &&
                !string.IsNullOrWhiteSpace(sponsor.DisplayName)
                    ? sponsor.DisplayName
                    : "A local business owner";

            string locationId = GetNearestBankLocationId(
                behavior.TargetLocation == Vector3.Zero
                    ? SelectRandomLocation(_bankLocations)
                    : behavior.TargetLocation);

            JobData generatedJob = _jobSystem.CreateJob(
                "Protection",
                "Protect a bank-coach deposit",
                sponsorName +
                    " needs help protecting a coach carrying business funds to the bank.",
                pair.Key,
                "bank_coach",
                locationId,
                _random.Next(100, 301),
                0m,
                5,
                false);

            if (generatedJob != null)
            {
                _jobSystem.AddObjective(
                    generatedJob.JobId,
                    "Escort the bank coach safely to its destination.");

                behavior.RecordMemory(
                    "A bank-coach protection job became available.");
                _worldEventCooldown = WorldEventCooldownUpdates;
                return;
            }
        }

        _worldEventCooldown = 15;
    }

    private string GetNearestBankLocationId(Vector3 position)
    {
        string nearestLocationId = "valentine_bank";
        float nearestDistance = float.MaxValue;

        foreach (KeyValuePair<string, Vector3> location in _bankLocations)
        {
            float distance = Vector3.Distance(position, location.Value);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestLocationId = location.Key;
            }
        }

        return nearestLocationId;
    }

    private static int Clamp(int value, int minimum, int maximum)
    {
        if (value < minimum)
            return minimum;

        if (value > maximum)
            return maximum;

        return value;
    }
}

public class NPCBehaviorState
{
    private const int MaximumMemoryCount = 20;

    public string NpcId { get; set; }
    public string Role { get; set; }
    public string CurrentBehavior { get; set; }
    public int BehaviorTimer { get; set; }
    public Vector3 TargetLocation { get; set; }
    public bool HasProcessedAction { get; set; }
    public string InteractionTargetId { get; set; }
    public decimal CashOnHand { get; set; }
    public int BanksVisited { get; set; }
    public int DepositsWithdrawn { get; set; }
    public List<string> Memories { get; private set; }

    public NPCBehaviorState()
    {
        Memories = new List<string>();
    }

    public void RecordMemory(string memory)
    {
        if (string.IsNullOrWhiteSpace(memory))
            return;

        Memories.Add(memory);

        while (Memories.Count > MaximumMemoryCount)
            Memories.RemoveAt(0);
    }
}