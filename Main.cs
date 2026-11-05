using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using RDR2;
using RDR2.Math;

public class Main : Script
{
    private readonly GangManager _gangManager;
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
    private readonly InteractionSystem _interactionSystem;
    private readonly CommandSystem _commandSystem;
    private readonly RDR2.UI.TextElement _interactionMenuText;
    private readonly Dictionary<string, Ped> _runtimeCompanionPeds = new Dictionary<string, Ped>();

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
        _gangManager = new GangManager();
        _companionSystem = new CompanionSystem();
        _bankSystem = new BankSystem();
        _jobSystem = new JobSystem();
        _lockSystem = new LockSystem();
        _dynamiteSystem = new DynamiteSystem();
        _weaponSystem = new WeaponSystem();
        _wagonSystem = new WagonSystem();
        _tacticalSquadAiSystem = new TacticalSquadAiSystem();
        _entityRegistry = new WorldEntityRegistry();
        _tacticalDecisionSystem = new TacticalDecisionSystem(_entityRegistry, _tacticalSquadAiSystem);
        _interactionSystem = new InteractionSystem(_lockSystem, _dynamiteSystem);
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

        Tick += OnTick;
        KeyDown += OnKeyDown;

        LoadAll();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F7)
        {
            if (!_interactionMenuVisible)
                UpdateInteractionMenu();

            if (_interactionMenuVisible)
                ExecuteInteractionMenuOption();
            else
                RDR2.UI.Screen.PrintSubtitle("Aim at a nearby person or animal to interact with the gang.");

            e.Handled = true;
        }
        else if (_interactionMenuVisible && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
        {
            _selectedInteractionOption = e.KeyCode == Keys.Down
                ? (_selectedInteractionOption + 1) % GetInteractionOptionCount()
                : (_selectedInteractionOption + GetInteractionOptionCount() - 1) % GetInteractionOptionCount();

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
        _interactionMenuText.Position = new System.Drawing.PointF(RDR2.UI.Screen.ScaledWidth * 0.68f, 360f);
        _interactionMenuText.ScaledDraw();
    }

    private string BuildInteractionMenuCaption()
    {
        if (string.IsNullOrEmpty(_interactionTargetCompanionId))
        {
            string targetType = _interactionTargetPed.IsHuman ? "PERSON" : "ANIMAL";
            string action = _interactionTargetPed.IsHuman ? "Recruit" : "Recruit Animal";
            return targetType + "\n" +
                   (_selectedInteractionOption == 0 ? "> " : " ") + action + "\n\n" +
                   "Up/Down: Select Enter/F7: Choose";
        }

        WorldEntityData entity = _entityRegistry.GetEntity(_interactionTargetCompanionId);
        string followOption = entity != null && entity.IsFollowingPlayer ? "Stay" : "Follow";

        return "GANG MEMBER\n" +
               (_selectedInteractionOption == 0 ? "> " : " ") + followOption + "\n" +
               (_selectedInteractionOption == 1 ? "> " : " ") + "Dismiss\n\n" +
               "Up/Down: Select Enter/F7: Choose";
    }

    private int GetInteractionOptionCount()
    {
        return string.IsNullOrEmpty(_interactionTargetCompanionId) ? 1 : 2;
    }

    private string GetRuntimeCompanionId(Ped ped)
    {
        if (ped == null || !ped.Exists())
            return null;

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            if (pair.Value != null && pair.Value.Exists() && pair.Value.Handle == ped.Handle)
                return pair.Key;
        }

        return null;
    }

    private Ped GetInteractionTargetPed()
    {
        Ped targetPed = GetInteractionLockonTargetPed();
        if (IsInteractionMenuTarget(targetPed))
            return targetPed;

        targetPed = Game.Player.GetFreeAimEntity() as Ped;
        return IsInteractionMenuTarget(targetPed) ? targetPed : null;
    }

    private unsafe Ped GetInteractionLockonTargetPed()
    {
        int entityHandle = 0;
        bool hasTarget = RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
            Game.Player.Handle,
            &entityHandle,
            false,
            false);

        if (!hasTarget || entityHandle == 0)
            return null;

        return Entity.FromHandle(entityHandle) as Ped;
    }

    private bool IsInteractionMenuTarget(Ped ped)
    {
        return GetRuntimeCompanionId(ped) != null || IsRecruitmentCandidate(ped);
    }

    private void ExecuteInteractionMenuOption()
    {
        Ped currentTarget = GetInteractionTargetPed();

        if (!_interactionMenuVisible || _interactionTargetPed == null || currentTarget == null ||
            !currentTarget.Exists() || currentTarget.Handle != _interactionTargetHandle)
            return;

        if (string.IsNullOrEmpty(_interactionTargetCompanionId))
        {
            if (_selectedInteractionOption == 0)
                RecruitPed(currentTarget);

            return;
        }

        if (_selectedInteractionOption == 0)
            ToggleCompanionFollowState(_interactionTargetCompanionId, currentTarget);
        else if (_selectedInteractionOption == 1)
            DismissCompanion(_interactionTargetCompanionId, currentTarget);
    }

    private void ToggleCompanionFollowState(string companionId, Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) || companionPed == null || !companionPed.Exists())
            return;

        WorldEntityData entity = _entityRegistry.GetEntity(companionId);
        CompanionData companion = _companionSystem.GetCompanion(companionId);

        bool shouldFollow = entity != null ? !entity.IsFollowingPlayer : companion != null && !companion.IsFollowingPlayer;
        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow && (playerPed == null || !playerPed.Exists()))
            return;

        if (entity != null)
        {
            entity.IsFollowingPlayer = shouldFollow;
            entity.CurrentTask = shouldFollow ? "FollowPlayer" : "Stay";
        }

        if (companion != null)
        {
            companion.IsFollowingPlayer = shouldFollow;
            companion.CurrentTask = shouldFollow ? "FollowPlayer" : "Stay";
        }

        companionPed.AlwaysKeepTask = true;

        if (shouldFollow)
            companionPed.Task.FollowToOffsetOfEntity(playerPed, new Vector3(0f, -2f, 0f), 2f, -1, 3f, true);
        else
            companionPed.Task.StandStill(-1);

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle(shouldFollow ? "Gang member is following you." : "Gang member will stay here.");
    }

    private void DismissCompanion(string companionId, Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) || companionPed == null || !companionPed.Exists())
            return;

        companionPed.Task.ClearTasks(true);
        companionPed.AlwaysKeepTask = false;
        _runtimeCompanionPeds.Remove(companionId);
        _companionSystem.RemoveCompanion(companionId);
        _entityRegistry.RemoveEntity(companionId);
        SaveAll();
        RDR2.UI.Screen.PrintSubtitle("Gang member dismissed.");
    }

    private bool IsRecruitmentCandidate(Ped ped)
    {
        if (ped == null || !ped.Exists() || ped.IsPlayer || (!ped.IsHuman && !ped.IsAnimal) || ped.IsDeadOrDying ||
            ped.IsInVehicle || ped.IsInCombat || ped.IsFleeing || IsRuntimeCompanion(ped))
            return false;

        Ped playerPed = Game.Player.Character;
        return playerPed != null && playerPed.Exists() && ped.Position.DistanceTo(playerPed.Position) <= 5f;
    }

    private void RecruitPed(Ped targetPed)
    {
        if (!IsRecruitmentCandidate(targetPed))
        {
            RDR2.UI.Screen.PrintSubtitle("That person cannot be recruited right now.");
            return;
        }

        Ped playerPed = Game.Player.Character;
        if (playerPed == null || !playerPed.Exists())
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        string companionId = Guid.NewGuid().ToString("N");

        CompanionData companion = new CompanionData
        {
            CompanionId = companionId,
            Name = targetPed.IsHuman ? "Gang Member" : "Animal Companion",
            Species = targetPed.IsHuman ? "Human" : "Animal",
            IsHuman = targetPed.IsHuman,
            Role = targetPed.IsHuman ? "Companion" : "Animal Companion",
            Personality = "Undetermined",
            Loyalty = 50,
            CurrentCombatStyle = targetPed.IsHuman ? "Ranged Medium" : "Animal Support",
            CurrentTask = "FollowPlayer",
            IsRecruitable = false,
            IsFollowingPlayer = true
        };

        WorldEntityData entity = new WorldEntityData
        {
            EntityId = companionId,
            EntityType = targetPed.IsHuman ? "Human" : "Animal",
            DisplayName = companion.Name,
            FactionId = "PlayerGang",
            IsAlive = true,
            IsRecruitable = false,
            IsFollowingPlayer = true,
            CurrentTask = companion.CurrentTask,
            CombatStyle = companion.CurrentCombatStyle,
            Loyalty = companion.Loyalty
        };

        _companionSystem.RegisterCompanion(companion);
        _entityRegistry.RegisterEntity(entity);
        _runtimeCompanionPeds.Add(companionId, targetPed);

        targetPed.AlwaysKeepTask = true;
        targetPed.Task.FollowToOffsetOfEntity(playerPed, new Vector3(0f, -2f, 0f), 2f, -1, 3f, true);

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle(targetPed.IsHuman ? "Gang member recruited. They will follow you." : "Animal companion recruited. They will follow you.");
    }

    private bool IsRuntimeCompanion(Ped ped)
    {
        foreach (Ped companionPed in _runtimeCompanionPeds.Values)
        {
            if (companionPed != null && companionPed.Exists() && companionPed.Handle == ped.Handle)
                return true;
        }

        return false;
    }

    private void ToggleCompanionFollowState()
    {
        bool hasFollowingCompanion = false;
        bool hasRuntimeCompanion = false;

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            if (pair.Value == null || !pair.Value.Exists())
                continue;

            hasRuntimeCompanion = true;

            WorldEntityData entity = _entityRegistry.GetEntity(pair.Key);
            if (entity != null && entity.IsFollowingPlayer)
                hasFollowingCompanion = true;
        }

        if (!hasRuntimeCompanion)
        {
            RDR2.UI.Screen.PrintSubtitle("No recruited companions are available in this session.");
            return;
        }

        bool shouldFollow = !hasFollowingCompanion;
        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow && (playerPed == null || !playerPed.Exists()))
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            Ped companionPed = pair.Value;
            if (companionPed == null || !companionPed.Exists())
                continue;

            WorldEntityData entity = _entityRegistry.GetEntity(pair.Key);
            if (entity != null)
            {
                entity.IsFollowingPlayer = shouldFollow;
                entity.CurrentTask = shouldFollow ? "FollowPlayer" : "Stay";
            }

            CompanionData companion = _companionSystem.GetCompanion(pair.Key);
            if (companion != null)
            {
                companion.IsFollowingPlayer = shouldFollow;
                companion.CurrentTask = shouldFollow ? "FollowPlayer" : "Stay";
            }

            companionPed.AlwaysKeepTask = true;

            if (shouldFollow)
                companionPed.Task.FollowToOffsetOfEntity(playerPed, new Vector3(0f, -2f, 0f), 2f, -1, 3f, true);
            else
                companionPed.Task.StandStill(-1);
        }

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle(shouldFollow ? "Gang members are following you." : "Gang members will stay here.");
    }

    private void OnTick(object sender, EventArgs e)
    {
        UpdateInteractionMenu();

        _tickCounter++;
        if (_tickCounter % 30 != 0)
            return;

        SyncWorldStateFromSystems();
        UpdateEntityAI();
        SaveAll();
    }

    private void LoadAll()
    {
        _saveRoot = JsonSaveSystem.Load();
        if (_saveRoot == null)
            _saveRoot = new SaveRoot();

        _gangManager.ImportSaveData(_saveRoot);
        _jobSystem.LoadFromSaveRoot(_saveRoot);

        if (_saveRoot.World != null && _saveRoot.World.Companions != null)
            _companionSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Companions));

        if (_saveRoot.BankAccounts != null)
            _bankSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.BankAccounts));

        if (_saveRoot.World != null && _saveRoot.World.Wagons != null)
            _wagonSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Wagons));

        if (_saveRoot.World != null && _saveRoot.World.WeaponPlatforms != null)
            _weaponSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.WeaponPlatforms));

        if (_saveRoot.World != null && _saveRoot.World.Locks != null)
            _lockSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Locks));

        if (_saveRoot.World != null && _saveRoot.World.Destructibles != null)
            _dynamiteSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Destructibles));

        if (_saveRoot.World != null && _saveRoot.World.TacticalOrders != null)
            _tacticalSquadAiSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.TacticalOrders));

        if (_saveRoot.World != null && _saveRoot.World.Commands != null)
            _commandSystem.LoadState(JsonConvert.SerializeObject(_saveRoot.World.Commands));

        if (_saveRoot.WorldEntities != null)
            _entityRegistry.LoadState(JsonConvert.SerializeObject(_saveRoot.WorldEntities));

        SyncInteractionRecordsFromSave();
    }

    private void SyncInteractionRecordsFromSave()
    {
        if (_saveRoot == null || _saveRoot.World == null)
            return;

        if (_saveRoot.World.Locks != null)
        {
            foreach (LockRecord record in _saveRoot.World.Locks)
            {
                if (record != null)
                    _interactionSystem.RegisterLock(record);
            }
        }

        if (_saveRoot.World.Destructibles != null)
        {
            foreach (DestructibleRecord record in _saveRoot.World.Destructibles)
            {
                if (record != null)
                    _interactionSystem.RegisterDestructible(record);
            }
        }
    }

    private void SyncWorldStateFromSystems()
    {
        if (_saveRoot == null)
            _saveRoot = new SaveRoot();

        if (_saveRoot.World == null)
            _saveRoot.World = new WorldState();

        _saveRoot.GangMembers = _gangManager.GetMembers();
        _saveRoot.World.Companions = _companionSystem.GetCompanions();
        _saveRoot.BankAccounts = _bankSystem.GetAllAccounts();
        _jobSystem.WriteToSaveRoot(_saveRoot);
        _saveRoot.World.Wagons = _wagonSystem.GetWagons();
        _saveRoot.World.WeaponPlatforms = _weaponSystem.GetPlatforms();
        _saveRoot.World.Locks = _lockSystem.GetLocks();
        _saveRoot.World.Destructibles = _dynamiteSystem.GetDestructibles();
        _saveRoot.World.TacticalOrders = _tacticalSquadAiSystem.GetOrders();
        _saveRoot.World.Commands = _commandSystem.GetCommands();
        _saveRoot.WorldEntities = _entityRegistry.GetEntities();
    }

    private void UpdateEntityAI()
    {
        _commandSystem.ProcessActiveCommands(_tacticalDecisionSystem);

        foreach (WorldEntityData entity in _entityRegistry.GetEntities())
        {
            if (entity == null || !entity.IsAlive)
                continue;

            if (string.IsNullOrWhiteSpace(entity.CombatStyle))
                entity.CombatStyle = _tacticalDecisionSystem.ResolveCombatStyle("Normal", false, false, false, false);

            if (entity.IsFollowingPlayer && string.IsNullOrWhiteSpace(entity.CurrentTask))
                entity.CurrentTask = "FollowPlayer";

            if (entity.IsRecruitable && entity.Loyalty < 0)
                entity.Loyalty = 0;

            if (entity.SupportSkill < 0)
                entity.SupportSkill = 0;

            if (entity.CombatSkill < 0)
                entity.CombatSkill = 0;

            if (entity.StealthSkill < 0)
                entity.StealthSkill = 0;
        }

        foreach (TacticalOrderData order in _tacticalSquadAiSystem.GetOrders())
        {
            if (order == null || order.IsCompleted)
                continue;

            _tacticalDecisionSystem.ApplyOrderToSquad(order);
        }
    }

    private void SaveAll()
    {
        SyncWorldStateFromSystems();
        JsonSaveSystem.Save(_saveRoot);
    }
}