using System;
using System.Collections.Generic;

public class Main
{
    private static GangManager _gangManager;
    private static List<CompanionData> _companions = new List<CompanionData>();
    private static TacticalContext _currentContext = new TacticalContext();
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _gangManager = new GangManager();
        JsonSaveSystem.LoadToGangManager(_gangManager);

        if (_gangManager.WorldState != null && _gangManager.WorldState.Companions != null)
        {
            _companions = _gangManager.WorldState.Companions;
        }

        if (_companions == null)
        {
            _companions = new List<CompanionData>();
        }

        _initialized = true;
    }

    public static void OnTick()
    {
        if (!_initialized)
        {
            Initialize();
        }

        if (_gangManager == null)
        {
            _gangManager = new GangManager();
        }

        if (_currentContext == null)
        {
            _currentContext = new TacticalContext();
        }

        UpdateCompanions();
        SyncWorldState();
        _gangManager.ApplyTacticalContext(_currentContext);
    }

    public static void OnShutdown()
    {
        if (_gangManager == null)
        {
            return;
        }

        SyncWorldState();
        JsonSaveSystem.SaveFromGangManager(_gangManager);
    }

    public static GangManager GetGangManager()
    {
        if (!_initialized)
        {
            Initialize();
        }

        return _gangManager;
    }

    public static TacticalContext GetTacticalContext()
    {
        if (_currentContext == null)
        {
            _currentContext = new TacticalContext();
        }

        return _currentContext;
    }

    public static void SetTacticalContext(TacticalContext context)
    {
        _currentContext = context ?? new TacticalContext();
    }

    public static CompanionData RecruitCompanion(string name, string role)
    {
        Initialize();

        CompanionData companion = CompanionSystem.CreateHumanCompanion(name, role);
        _companions.Add(companion);

        SyncWorldState();
        return companion;
    }

    public static CompanionData RecruitAnimalCompanion(string name, string species)
    {
        Initialize();

        CompanionData companion = CompanionSystem.CreateAnimalCompanion(name, species);
        _companions.Add(companion);

        SyncWorldState();
        return companion;
    }

    public static void IssueCommand(string command)
    {
        Initialize();

        CommandSystem.ExecuteCommand(_gangManager, _currentContext, command);
        UpdateCompanions();
        SyncWorldState();
    }

    public static List<CompanionData> GetCompanions()
    {
        Initialize();
        return new List<CompanionData>(_companions);
    }

    public static void AddMemoryToCompanion(string companionId, string memory)
    {
        Initialize();

        CompanionData companion = FindCompanion(companionId);
        if (companion == null)
        {
            return;
        }

        CompanionSystem.AddMemory(companion, memory);
        SyncWorldState();
    }

    public static void AddGoalToCompanion(string companionId, string goal)
    {
        Initialize();

        CompanionData companion = FindCompanion(companionId);
        if (companion == null)
        {
            return;
        }

        CompanionSystem.AddGoal(companion, goal);
        SyncWorldState();
    }

    public static void GiveItemToCompanion(string companionId, string item)
    {
        Initialize();

        CompanionData companion = FindCompanion(companionId);
        if (companion == null)
        {
            return;
        }

        CompanionSystem.GiveItem(companion, item);
        SyncWorldState();
    }

    public static void AdjustCompanionLoyalty(string companionId, int amount)
    {
        Initialize();

        CompanionData companion = FindCompanion(companionId);
        if (companion == null)
        {
            return;
        }

        CompanionSystem.AdjustLoyalty(companion, amount);
        SyncWorldState();
    }

    public static void RegisterGangMember(GangMember member)
    {
        Initialize();

        if (member == null)
        {
            return;
        }

        _gangManager.RegisterMember(member);
    }

    public static List<string> GetRecommendedActionsForGangMember(string memberId)
    {
        Initialize();

        GangMember member = _gangManager.GetMember(memberId);
        if (member == null)
        {
            return new List<string>();
        }

        return TacticalSquadAiSystem.BuildRecommendedActions(member, _currentContext);
    }

    private static void UpdateCompanions()
    {
        if (_companions == null)
        {
            _companions = new List<CompanionData>();
            return;
        }

        for (int i = 0; i < _companions.Count; i++)
        {
            CompanionData companion = _companions[i];
            if (companion == null)
            {
                continue;
            }

            CompanionSystem.UpdateCompanion(companion, _currentContext);
        }
    }

    private static void SyncWorldState()
    {
        if (_gangManager == null)
        {
            return;
        }

        if (_gangManager.WorldState == null)
        {
            _gangManager.WorldState = new WorldState();
        }

        if (_companions == null)
        {
            _companions = new List<CompanionData>();
        }

        _gangManager.WorldState.Companions = _companions;
    }

    private static CompanionData FindCompanion(string companionId)
    {
        if (string.IsNullOrWhiteSpace(companionId) || _companions == null)
        {
            return null;
        }

        for (int i = 0; i < _companions.Count; i++)
        {
            CompanionData companion = _companions[i];
            if (companion != null && companion.CompanionId == companionId)
            {
                return companion;
            }
        }

        return null;
    }
}