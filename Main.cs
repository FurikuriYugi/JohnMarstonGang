using RDR2;
using System;

public class Main : Script
{
    private static GangManager _gangManager;
    private static CompanionSystem _companionSystem;

    public Main()
    {
        _companionSystem = new CompanionSystem();
        _gangManager = new GangManager(_companionSystem);
        _gangManager.SyncAllMembers();

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (_gangManager != null)
            _gangManager.Update();
    }

    public static GangManager GetGangManager()
    {
        return _gangManager;
    }

    public static CompanionSystem GetCompanionSystem()
    {
        return _companionSystem;
    }

    public static void SaveAll()
    {
        if (_gangManager != null)
            _gangManager.Save();

        if (_companionSystem != null)
            _companionSystem.Save();
    }

    public static void RecruitMember(string name, int relationship, CombatStyle style, bool following)
    {
        if (_gangManager == null)
        {
            _companionSystem = new CompanionSystem();
            _gangManager = new GangManager(_companionSystem);
        }

        _gangManager.Recruit(name, relationship, style, following);
        SaveAll();
    }

    public static void FollowPlayer()
    {
        if (_gangManager == null)
            return;

        _gangManager.FollowPlayer();
        SaveAll();
    }

    public static void WaitGang()
    {
        if (_gangManager == null)
            return;

        _gangManager.Wait();
        SaveAll();
    }

    public static void CycleCombatStyles()
    {
        if (_gangManager == null)
            return;

        _gangManager.CycleCombatStyle();
        SaveAll();
    }

    public static void SetCombatStyleForAll(CombatStyle style)
    {
        if (_gangManager == null)
            return;

        _gangManager.SetCombatStyleForAll(style);
        SaveAll();
    }

    public static void AddMemberMemory(string name, string memory)
    {
        if (_gangManager == null)
            return;

        _gangManager.AddMemberMemory(name, memory);
        SaveAll();
    }

    public static void SetMemberGoal(string name, string goal)
    {
        if (_gangManager == null)
            return;

        _gangManager.SetMemberGoal(name, goal);
        SaveAll();
    }

    public static void UpdateMemberRelationship(string name, int delta)
    {
        if (_gangManager == null)
            return;

        _gangManager.UpdateMemberRelationship(name, delta);
        SaveAll();
    }
}