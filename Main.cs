using RDR2;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

public class Main : Script
{
    private static GangManager _gangManager;

    public Main()
    {
        _gangManager = GangManager.Load();
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

    public static void SaveGang()
    {
        if (_gangManager != null)
            _gangManager.Save();
    }

    public static void RecruitMember(string name, int relationship, CombatStyle style, bool following)
    {
        if (_gangManager == null)
            _gangManager = new GangManager();

        _gangManager.Recruit(name, relationship, style, following);
        _gangManager.Save();
    }

    public static void FollowPlayer()
    {
        if (_gangManager == null)
            return;

        _gangManager.FollowPlayer();
        _gangManager.Save();
    }

    public static void WaitGang()
    {
        if (_gangManager == null)
            return;

        _gangManager.Wait();
        _gangManager.Save();
    }

    public static void CycleCombatStyles()
    {
        if (_gangManager == null)
            return;

        _gangManager.CycleCombatStyle();
        _gangManager.Save();
    }

    public static void SetCombatStyleForAll(CombatStyle style)
    {
        if (_gangManager == null)
            return;

        _gangManager.SetCombatStyleForAll(style);
        _gangManager.Save();
    }
}