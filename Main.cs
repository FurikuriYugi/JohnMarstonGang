// Main.cs
using RDR2;
using System;
using System.Windows.Forms;

public class Main : Script
{
    private GangManager _gang;
    private BankSystem _bank;

    public Main()
    {
        _gang = GangManager.Load();
        _bank = BankSystem.Load();

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            _gang.Update();
        }
        catch
        {
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.F6) _gang.RecruitNearby();
            if (e.KeyCode == Keys.F7) _gang.FollowPlayer();
            if (e.KeyCode == Keys.F8) _gang.Wait();
            if (e.KeyCode == Keys.F9) _gang.CycleCombatStyle();
            if (e.KeyCode == Keys.F5)
            {
                _gang.Save();
                _bank.Save();
            }
            if (e.KeyCode == Keys.F10)
            {
                _gang = GangManager.Load();
                _bank = BankSystem.Load();
            }
        }
        catch
        {
        }
    }
}