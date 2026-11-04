using System;

public class Main
{
    private static GangManager _gangManager;

    public static void Initialize()
    {
        _gangManager = new GangManager();
        JsonSaveSystem.LoadToGangManager(_gangManager);
    }

    public static void OnTick()
    {
        if (_gangManager == null)
        {
            _gangManager = new GangManager();
            JsonSaveSystem.LoadToGangManager(_gangManager);
        }

        // future gameplay update hooks go here
    }

    public static void OnShutdown()
    {
        if (_gangManager != null)
        {
            JsonSaveSystem.SaveFromGangManager(_gangManager);
        }
    }

    public static GangManager GetGangManager()
    {
        return _gangManager;
    }
}