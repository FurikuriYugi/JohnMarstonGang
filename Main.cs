using System;
using System.Windows.Forms;
using RDR2;

public class Main : Script
{
    private GangManager _gang;

    public Main()
    {
        Interval = 0;
        Tick += OnTick;
        KeyDown += OnKeyDown;

        try
        {
            _gang = GangManager.Load();
            RDR2.UI.Screen.PrintSubtitle("JohnMorstonGang loaded.");
        }
        catch (Exception ex)
        {
            RDR2.UI.Screen.PrintSubtitle("Load failed: " + ex.Message);
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (_gang == null) return;

        try
        {
            _gang.Update();
        }
        catch (Exception ex)
        {
            RDR2.UI.Screen.PrintSubtitle("Tick error: " + ex.Message);
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (_gang == null) return;

        try
        {
            if (e.KeyCode == Keys.F6)
            {
                _gang.RecruitNearby();
                RDR2.UI.Screen.PrintSubtitle("Recruit nearby.");
            }
            else if (e.KeyCode == Keys.F7)
            {
                _gang.OrderFollow();
                RDR2.UI.Screen.PrintSubtitle("Follow.");
            }
            else if (e.KeyCode == Keys.F8)
            {
                _gang.OrderWait();
                RDR2.UI.Screen.PrintSubtitle("Wait.");
            }
            else if (e.KeyCode == Keys.F9)
            {
                _gang.CycleCombatStyle();
                RDR2.UI.Screen.PrintSubtitle("Combat style changed.");
            }
            else if (e.KeyCode == Keys.F5)
            {
                _gang.Save();
                RDR2.UI.Screen.PrintSubtitle("Saved.");
            }
            else if (e.KeyCode == Keys.F10)
            {
                _gang = GangManager.Load();
                RDR2.UI.Screen.PrintSubtitle("Reloaded.");
            }
        }
        catch (Exception ex)
        {
            RDR2.UI.Screen.PrintSubtitle("Key error: " + ex.Message);
        }
    }
}