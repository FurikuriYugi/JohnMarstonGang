<!-- Main lines 1-250 of 1034 -->
```csharp
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using RDR2;
using RDR2.Math;

public class Main : Script
{
    private readonly GangManagementSystem _gangManagementSystem;
    private readonly CompanionSystem _companionSystem;
    private readonly BankSystem _bankSystem;
    private readonly JobSystem _jobSystem;
    private readonly LockSystem _lockSystem;
    private readonly DynamiteSystem _dynamiteSystem;
    private readonly WeaponSystem _weaponSystem;
    private readonly WagonSystem _wagonSystem;
    private readonly TacticalSquadAiSystem _tacticalSquadAiSystem;
    private readonly WorldEntityRegistry _entityRegistry;
    private readonly TacticalDecisionSystem _tacticalDecisionSystem;
    private readonly CoreAISystem _coreAISystem;
    private readonly InteractionSystem _interactionSystem;
    private readonly CommandSystem _commandSystem;
    private readonly RDR2.UI.TextElement _interactionMenuText;

    private readonly Dictionary<string, Ped> _runtimeCompanionPeds =
        new Dictionary<string, Ped>();

    private List<Ped> _cachedNearbyEnemies = new List<Ped>();
    private SaveRoot _saveRoot;
    private int _tickCounter;
    private Ped _interactionTargetPed;
    private string _interactionTargetCompanionId;
    private int _interactionTargetHandle = -1;
    private int _selectedInteractionOption;
    private bool _interactionMenuVisible;

    public Main()
    {
        _saveRoot = new SaveRoot();

        _bankSystem = new BankSystem();
        _gangManagementSystem = new GangManagementSystem(
            new GangJobPayoutSystem(),
            _bankSystem);

        _companionSystem = new CompanionSystem();
        _jobSystem = new JobSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _weaponSystem = new WeaponSystem();
        _wagonSystem = new WagonSystem();
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();
        _entityRegistry = new WorldEntityRegistry();

        _tacticalDecisionSystem =
            new TacticalDecisionSystem(
                _entityRegistry,
                _tacticalSquadAiSystem);

        _coreAISystem = new CoreAISystem(
            _entityRegistry,
            _tacticalDecisionSystem,
            _bankSystem,
            _jobSystem);

        _interactionSystem =
            new InteractionSystem(_lockSystem, _dynamiteSystem);

        _commandSystem = new CommandSystem();

        _interactionMenuText = new RDR2.UI.TextElement(
            string.Empty,
            new System.Drawing.PointF(0f, 0f),
            0.45f,
            System.Drawing.Color.White,
            RDR2.UI.Alignment.Left,
            true,
            true);

        _interactionMenuText.Enabled = true;

        _jobSystem.JobCompleted += _gangManagementSystem.OnJobCompleted;

        Tick += OnTick;
        KeyDown += OnKeyDown;

        LoadAll();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F7)
        {
            if (!_interactionMenuVisible)
            {
                UpdateInteractionMenu();
            }

            if (_interactionMenuVisible)
            {
                ExecuteInteractionMenuOption();
            }
            else
            {
                RDR2.UI.Screen.PrintSubtitle(
                    "Aim at a nearby person or animal to interact with the gang.");
            }

            e.Handled = true;
        }
        else if (_interactionMenuVisible &&
                 (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
        {
            int optionCount = GetInteractionOptionCount();

            _selectedInteractionOption =
                e.KeyCode == Keys.Down
                    ? (_selectedInteractionOption + 1) % optionCount
                    : (_selectedInteractionOption + optionCount - 1) %
                      optionCount;

            e.Handled = true;
        }
        else if (_interactionMenuVisible && e.KeyCode == Keys.Enter)
        {
            ExecuteInteractionMenuOption();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.F9)
        {
            ToggleCompanionFollowState();
        }
    }

    private void UpdateInteractionMenu()
    {
        Ped targetPed = GetInteractionTargetPed();
        string companionId = GetRuntimeCompanionId(targetPed);

        if (targetPed == null)
        {
            _interactionMenuVisible = false;
            _interactionTargetPed = null;
            _interactionTargetCompanionId = null;
            _interactionTargetHandle = -1;
            return;
        }

        if (targetPed.Handle != _interactionTargetHandle)
        {
            _interactionTargetHandle = targetPed.Handle;
            _selectedInteractionOption = 0;
        }

        _interactionMenuVisible = true;
        _interactionTargetPed = targetPed;
        _interactionTargetCompanionId = companionId;
        _interactionMenuText.Caption = BuildInteractionMenuCaption();
        _interactionMenuText.Position = new System.Drawing.PointF(
            RDR2.UI.Screen.ScaledWidth * 0.68f,
            360f);

        _interactionMenuText.ScaledDraw();
    }

    private string BuildInteractionMenuCaption()
    {
        if (string.IsNullOrEmpty(_interactionTargetCompanionId))
        {
            string targetType =
                _interactionTargetPed.IsHuman ? "PERSON" : "ANIMAL";

            string action =
                _interactionTargetPed.IsHuman
                    ? "Recruit"
                    : "Recruit Animal";

            return targetType + "\n" +
                   (_selectedInteractionOption == 0 ? "> " : " ") +
                   action + "\n\n" +
                   "Up/Down: Select Enter/F7: Choose";
        }

        WorldEntityData entity =
            _entityRegistry.GetEntity(_interactionTargetCompanionId);

        string followOption =
            entity != null && entity.IsFollowingPlayer
                ? "Stay"
                : "Follow";

        return "GANG MEMBER\n" +
               (_selectedInteractionOption == 0 ? "> " : " ") +
               followOption + "\n" +
               (_selectedInteractionOption == 1 ? "> " : " ") +
               "Dismiss\n\n" +
               "Up/Down: Select Enter/F7: Choose";
    }

    private int GetInteractionOptionCount()
    {
        return string.IsNullOrEmpty(_interactionTargetCompanionId) ? 1 : 2;
    }

    private string GetRuntimeCompanionId(Ped ped)
    {
        if (ped == null || !ped.Exists())
        {
            return null;
        }

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            if (pair.Value != null &&
                pair.Value.Exists() &&
                pair.Value.Handle == ped.Handle)
            {
                return pair.Key;
            }
        }

        return null;
    }

    private Ped GetInteractionTargetPed()
    {
        Ped targetPed = GetInteractionLockonTargetPed();

        if (IsInteractionMenuTarget(targetPed))
        {
            return targetPed;
        }

        targetPed = Game.Player.GetFreeAimEntity() as Ped;

        return IsInteractionMenuTarget(targetPed) ? targetPed : null;
    }

    private unsafe Ped GetInteractionLockonTargetPed()
    {
        int entityHandle = 0;

        bool hasTarget =
            RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
                Game.Player.Handle,
                &entityHandle,
                false,
                false);

```
