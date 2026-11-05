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
    private bool _previousRmbState = false;  // Track previous RMB state to detect rising edge (click)

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
            ToggleCompanionFollowState();
    }

    private void UpdateInteractionMenu()
    {
        Ped targetPed = GetInteractionTargetPed();
        string companionId = GetRuntimeCompanionId(targetPed);
        if (targetPed == null || !targetPed.Exists())
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

        // Use native lock-on prompts instead of TextElement overlay
        RegisterNativeInteractionPrompts(targetPed, companionId);
    }

    private void RegisterNativeInteractionPrompts(Ped targetPed, string companionId)
    {
        if (targetPed == null || !targetPed.Exists())
            return;

        float interactionRadius = 2.5f;
        bool isCompanion = !string.IsNullOrEmpty(companionId);

        if (!isCompanion)
        {
            // Non-companion: Show "Recruit" or "Recruit Animal"
            string recruitLabel = targetPed.IsHuman ? "Recruit" : "Recruit Animal";
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                recruitLabel,
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,  // p8: enable lock-on UI
                0
            );
        }
        else
        {
            // Companion: Cycle through actions based on _selectedInteractionOption
            // This allows Up/Down keys to navigate between Follow/Stay and Dismiss
            WorldEntityData entity = _entityRegistry.GetEntity(companionId);
            string displayLabel;

            if (_selectedInteractionOption == 0)
            {
                // First action: Follow/Stay
                displayLabel = entity != null && entity.IsFollowingPlayer ? "Stay" : "Follow";
            }
            else
            {
                // Second action: Dismiss
                displayLabel = "Dismiss";
            }

            // Register the selected prompt for this frame
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                displayLabel,
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,  // p8: enable lock-on UI
                0
            );

            // Optional: Also show both prompts simultaneously if the native supports it
            // This is experimental; if only one prompt shows, the Up/Down cycling above handles it.
            // Uncomment below to test if multiple prompts render:
            /*
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                "Dismiss",
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,
                0
            );
            */
        }
    }

    private string BuildInteractionMenuCaption()
    {
        if (string.IsNullOrEmpty(_interactionTargetCompanionId))
        {
            string targetType = _interactionTargetPed.IsHuman ? "PERSON" : "ANIMAL";
            string action = _interactionTargetPed.IsHuman ? "Recruit" : "Recruit Animal";
            return targetType + "\n" +
                (_selectedInteractionOption == 0 ? "> " : "  ") + action + "\n\n" +
                "Up/Down: Select   Enter/F7: Choose";
        }

        WorldEntityData entity = _entityRegistry.GetEntity(_interactionTargetCompanionId);
        string followOption = entity != null && entity.IsFollowingPlayer ? "Stay" : "Follow";
        return "GANG MEMBER\n" +
            (_selectedInteractionOption == 0 ? "> " : "  ") + followOption + "\n" +
            (_selectedInteractionOption == 1 ? "> " : "  ") + "Dismiss\n\n" +
            "Up/Down: Select   Enter/F7: Choose";
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

        // Detect Right Mouse Button (RMB) press for native prompt interaction
        DetectNativePromptInput();

        _tickCounter++;

        if (_tickCounter % 30 != 0)
            return;

        SyncWorldStateFromSystems();
        UpdateEntityAI();
        SaveAll();
    }

    private void DetectNativePromptInput()
    {
        // Check if interaction menu is visible
        if (!_interactionMenuVisible)
        {
            _previousRmbState = false;  // Reset state when menu not visible
            return;
        }

        // RMB input detection using Windows.Forms - detect rising edge (click, not hold)
        MouseButtons mouseButtons = Control.MouseButtons;
        bool currentRmbState = (mouseButtons & MouseButtons.Right) == MouseButtons.Right;

        // Only execute if RMB transitions from NOT pressed to pressed (rising edge)
        if (currentRmbState && !_previousRmbState)
        {
            // RMB click detected - execute the current interaction option
            ExecuteInteractionMenuOption();
        }

        // Update state for next frame
        _previousRmbState = currentRmbState;
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

    // ========== REGISTER_INTERACTION_LOCKON_PROMPT Test Implementation ==========
    // This method demonstrates how to use the native REGISTER_INTERACTION_LOCKON_PROMPT
    // native for native Rockstar-style lock-on interaction menus with button glyphs.
    //
    // Call this in OnTick when you have a valid target entity.
    // The native automatically renders Rockstar-style UI including button states.
    //
    // Expected behavior:
    // - Displays native lock-on prompt with glyph for interaction
    // - Player can RMB (Right Mouse Button) to trigger interaction
    // - Prompts show native-style button labels (no custom text overlays)
    private void TestNativeLockonPrompt()
    {
        Ped targetPed = GetInteractionTargetPed();
        if (targetPed == null || !targetPed.Exists())
            return;

        // Interaction radius in game units (similar to proximity for lock-on)
        float interactionRadius = 2.5f;

        // Single action example: "Recruit"
        // REGISTER_INTERACTION_LOCKON_PROMPT parameters:
        // p0: Entity handle (the NPC/target)
        // p1: Action label/text ("Recruit", "Loot", etc.)
        // p2: Interaction radius (float)
        // p3: Usually same as p2 (radius)
        // p4: Unknown - type/flags (try 1 or 0)
        // p5: Unknown - positioning offset (try 0.0f)
        // p6: Unknown - behavior/animation (try 0.0f)
        // p7: Unknown - timeout/priority (try 0)
        // p8: Boolean - TRUE for lock-on prompt with RMB, FALSE for other
        // p9: Unknown - group/category (try 0)
        RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
            targetPed.Handle,           // p0: entity
            "Recruit",                  // p1: label
            interactionRadius,          // p2: radius
            interactionRadius,          // p3: radius (same as p2)
            1,                          // p4: type/flags
            0.0f,                       // p5: offset/param
            0.0f,                       // p6: offset/param
            0,                          // p7: timeout/priority
            true,                       // p8: LOCK-ON PROMPT (key parameter - true for native UI)
            0                           // p9: group/category
        );

        // TODO: Detect button press - likely via:
        // - GET_LAST_INPUT_COMMAND_WAS_ACCEPTED
        // - Or a new native like _IS_INTERACTION_LOCKON_PROMPT_PRESSED
        // - Or check if player is in RMB interaction state
        // This requires further investigation in-game or via native exploration.

        // For now, you can keep keyboard fallback (F7 key) to execute the action
    }

    // Multi-action variant: Register multiple prompts for same entity
    // This might require calling REGISTER_INTERACTION_LOCKON_PROMPT multiple times
    // or using a different parameter set to support multiple actions.
    private void TestNativeLockonPromptMultiAction()
    {
        Ped targetPed = GetInteractionTargetPed();
        if (targetPed == null || !targetPed.Exists())
            return;

        float interactionRadius = 2.5f;

        string companionId = GetRuntimeCompanionId(targetPed);
        bool isCompanion = !string.IsNullOrEmpty(companionId);

        if (!isCompanion)
        {
            // Not a companion - show Recruit prompt
            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                "Recruit",
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,  // p8: enable lock-on
                0
            );
        }
        else
        {
            // Companion - show Follow/Stay and Dismiss
            // Attempt 1: Call native twice (may or may not work)
            WorldEntityData entity = _entityRegistry.GetEntity(companionId);
            string followLabel = entity != null && entity.IsFollowingPlayer ? "Stay" : "Follow";

            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                followLabel,
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,
                0
            );

            RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                targetPed.Handle,
                "Dismiss",
                interactionRadius,
                interactionRadius,
                1, 0.0f, 0.0f, 0,
                true,
                0
            );
            // Note: Unknown if this creates multiple prompts or overwrites the first.
            // If it overwrites, you may need a different native or approach for multi-action.
        }
    }
}