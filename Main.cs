// Main.cs
using RDR2;
using System;
using System.Windows.Forms;

public class Main : Script
{
    private GangManager _gang;
    private BankSystem _bank;
    private WagonSystem _wagonSystem;
    private LockSystem _lockSystem;
    private DynamiteSystem _dynamiteSystem;
    private JobSystem _jobSystem;
    private InteractionSystem _interactionSystem;

    public Main()
    {
        _gang = GangManager.Load();
        _bank = BankSystem.Load();
        _wagonSystem = new WagonSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _jobSystem = new JobSystem();
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem, _wagonSystem, _bank, _jobSystem);

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            var context = new TacticalContext
            {
                IsCombatActive = false,
                IsHostileNearby = false,
                IsStealthMode = false,
                IsShoppingOrTown = false,
                IsTraveling = true,
                IsCloseQuarters = false,
                IsMediumRangeCombat = false,
                IsLongRangeCombat = false,
                FollowMode = true
            };

            _gang.UpdateWithContext(context);
        }
        catch
        {
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.F5)
            {
                _gang.Save();
                _bank.Save();
            }

            if (e.KeyCode == Keys.F6)
                _gang.RecruitNearby();

            if (e.KeyCode == Keys.F7)
                _gang.FollowPlayer();

            if (e.KeyCode == Keys.F8)
                _gang.Wait();

            if (e.KeyCode == Keys.F9)
                _gang.CycleCombatStyle();

            if (e.KeyCode == Keys.F10)
            {
                _gang = GangManager.Load();
                _bank = BankSystem.Load();
            }

            if (e.KeyCode == Keys.F11)
            {
                var context = new TacticalContext
                {
                    IsCombatActive = true,
                    IsHostileNearby = true,
                    IsStealthMode = false,
                    IsShoppingOrTown = false,
                    IsTraveling = false,
                    IsCloseQuarters = true,
                    IsMediumRangeCombat = false,
                    IsLongRangeCombat = false,
                    FollowMode = true
                };

                _gang.UpdateWithContext(context);
            }

            if (e.KeyCode == Keys.F12)
            {
                var context = new TacticalContext
                {
                    IsCombatActive = false,
                    IsHostileNearby = false,
                    IsStealthMode = true,
                    IsShoppingOrTown = false,
                    IsTraveling = false,
                    IsCloseQuarters = true,
                    IsMediumRangeCombat = false,
                    IsLongRangeCombat = false,
                    FollowMode = true
                };

                _gang.UpdateWithContext(context);
            }
        }
        catch
        {
        }
    }
}