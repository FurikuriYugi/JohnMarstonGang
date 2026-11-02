// Main.cs
using System;
using System.Windows.Forms;
using RDR2;
using RDR2.Native;
using RDR2.UI;
using ScriptHookRDRNetAPI;

public class Main : Script
{
    private GangManager _gang;
    private const string SavePath = "scripts\\JohnsGangSave.json";

    public Main()
    {
        Interval = 0;
        Tick += OnTick;
        KeyDown += OnKeyDown;
        _gang = GangManager.Load(SavePath);
    }

    private void OnTick(object sender, EventArgs e)
    {
        _gang.Update();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            Ped nearest = GangManager.GetNearestRecruitablePed(8.0f);
            if (nearest != null)
            {
                _gang.Recruit(nearest);
                UI.Notify("Recruited.");
            }
        }

        if (e.KeyCode == Keys.F6)
        {
            _gang.IssueOrder(GangOrder.FollowPlayer);
            UI.Notify("Gang: follow.");
        }

        if (e.KeyCode == Keys.F7)
        {
            _gang.IssueOrder(GangOrder.WaitHere);
            UI.Notify("Gang: wait.");
        }

        if (e.KeyCode == Keys.F8)
        {
            _gang.SetCombatStyle(CombatStyle.Stealth);
            UI.Notify("Gang combat style: stealth.");
        }

        if (e.KeyCode == Keys.F9)
        {
            _gang.Save(SavePath);
            UI.Notify("Saved.");
        }

        if (e.KeyCode == Keys.F10)
        {
            _gang = GangManager.Load(SavePath);
            UI.Notify("Loaded.");
        }
    }
}