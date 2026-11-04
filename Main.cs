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
            return;

        _gangManager = new GangManager();
        JsonSaveSystem.LoadToGangManager(_gangManager);

        if (_gangManager.WorldState != null && _gangManager.WorldState.Companions != null)
        {
            _companions = _gangManager.WorldState.Companions;
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

        SyncWorldState();

        UpdateCompanions();
        _gangManager.ApplyTacticalContext(_currentContext);
    }

    public static void OnShutdown()
    {
        if (_gangManager == null)
            return;

        SyncWorldState();
        JsonSaveSystem.SaveFromGangManager(_gangManager);
    }

    public static GangManager GetGangManager()
    {
        if (_gangManager == null)
        {
            Initialize();
        }

        return _gangManager;
    }

    public static void SetTacticalContext(TacticalContext context)
    {
        _currentContext = context ?? new TacticalContext();
    }

    public static TacticalContext GetTacticalContext()
    {
        return _currentContext;
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

    private static void UpdateCompanions()
    {
        for (int i = 0; i < _companions.Count; i++)
        {
            CompanionData companion = _companions[i];
            if (companion == null)
                continue;

            CompanionSystem.UpdateCompanion(companion, _currentContext);
        }
    }

    private static void SyncWorldState()
    {
        if (_gangManager == null)
            return;

        if (_gangManager.WorldState == null)
        {
            _gangManager.WorldState = new WorldState();
        }

        _gangManager.WorldState.Companions = _companions ?? new List<CompanionData>();
    }
}