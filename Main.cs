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

    public static Wagon CreateWagon(string name, string ownerId, WagonType type)
    {
        if (_gangManager == null)
            _gangManager = new GangManager();

        var wagon = _gangManager.Wagons.CreateWagon(name, ownerId, type);
        _gangManager.Save();
        return wagon;
    }

    public static bool ClaimWagon(string wagonId, string ownerId)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.ClaimWagon(wagonId, ownerId);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static bool ReleaseWagon(string wagonId, string ownerId = null)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.ReleaseWagon(wagonId, ownerId);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static bool HitchWagon(string wagonId)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.HitchWagon(wagonId);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static bool UnhitchWagon(string wagonId)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.UnhitchWagon(wagonId);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static bool StoreItemInWagon(string wagonId, string itemName, int amount)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.StoreItem(wagonId, itemName, amount);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static bool RemoveItemFromWagon(string wagonId, string itemName, int amount)
    {
        if (_gangManager == null)
            return false;

        var result = _gangManager.Wagons.RemoveItem(wagonId, itemName, amount);
        if (result)
            _gangManager.Save();

        return result;
    }

    public static void MarkWagonDestroyed(string wagonId)
    {
        if (_gangManager == null)
            return;

        _gangManager.Wagons.MarkDestroyed(wagonId);
        _gangManager.Save();
    }

    public static void RepairWagon(string wagonId)
    {
        if (_gangManager == null)
            return;

        _gangManager.Wagons.RepairWagon(wagonId);
        _gangManager.Save();
    }
}