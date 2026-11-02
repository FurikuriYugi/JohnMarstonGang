// ExpandedMain.cs
using System;
using System.Windows.Forms;
using RDR2;
using RDR2.UI;
using ScriptHookRDRNetAPI;

public class ExpandedMain : Script
{
    private GangManager _gang;
    private BankSystem _bank;
    private WagonSystem _wagons;
    private JobSystem _jobs;

    public ExpandedMain()
    {
        Interval = 0;
        Tick += OnTick;
        KeyDown += OnKeyDown;

        _gang = GangManager.Load("scripts\\JohnsGangSave.json");
        _bank = new BankSystem();
        _bank.Load();
        _wagons = new WagonSystem();
        _wagons.Load();
        _jobs = new JobSystem();
        _jobs.GenerateWorldJobs();
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
            UI.Notify("Follow.");
        }

        if (e.KeyCode == Keys.F7)
        {
            _gang.IssueOrder(GangOrder.WaitHere);
            UI.Notify("Wait.");
        }

        if (e.KeyCode == Keys.F8)
        {
            _gang.SetCombatStyle(CombatStyle.Stealth);
            UI.Notify("Stealth.");
        }

        if (e.KeyCode == Keys.F9)
        {
            _gang.Save("scripts\\JohnsGangSave.json");
            _bank.Save();
            _wagons.Save();
            _jobs.Save();
            UI.Notify("Saved all.");
        }

        if (e.KeyCode == Keys.F10)
        {
            _gang = GangManager.Load("scripts\\JohnsGangSave.json");
            _bank.Load();
            _wagons.Load();
            UI.Notify("Loaded all.");
        }
    }
}