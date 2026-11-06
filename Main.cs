using System;
using System.Collections.Generic;
using System.Reflection;
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

    private readonly Dictionary<string, Ped> _runtimeCompanionPeds =
        new Dictionary<string, Ped>();

    private List<Ped> _cachedNearbyEnemies = new List<Ped>();
    private SaveRoot _saveRoot;
    private int _tickCounter;

    private RDR2.UI.Prompt _recruitmentPrompt;
    private int _recruitmentPromptTargetHandle = -1;
    private int _recruitmentPromptGroup = -1;
    private int _recruitmentPromptNativeHandle = -1;
    private bool _recruitmentPromptAttached;
    private bool _promptHandleFailureReported;

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

        _tacticalDecisionSystem = new TacticalDecisionSystem(
            _entityRegistry,
            _tacticalSquadAiSystem);

        _coreAISystem = new CoreAISystem(
            _entityRegistry,
            _tacticalDecisionSystem,
            _bankSystem,
            _jobSystem);

        _interactionSystem = new InteractionSystem(
            _lockSystem,
            _dynamiteSystem);

        _commandSystem = new CommandSystem();

        _jobSystem.JobCompleted += _gangManagementSystem.OnJobCompleted;

        Tick += OnTick;
        KeyDown += OnKeyDown;

        LoadAll();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F9)
        {
            ToggleCompanionFollowState();
            e.Handled = true;
        }
    }

    private void UpdateRecruitmentPrompt()
    {
        Ped targetPed = GetInteractionLockonTargetPed();

        if (!IsRecruitmentCandidate(targetPed))
        {
            ClearRecruitmentPrompt();
            return;
        }

        if (_recruitmentPrompt == null ||
            _recruitmentPromptTargetHandle != targetPed.Handle)
        {
            ClearRecruitmentPrompt();
            CreateRecruitmentPrompt(targetPed);
        }

        if (_recruitmentPrompt == null)
        {
            return;
        }

        TryAttachRecruitmentPromptToTarget(targetPed);

        if (!_recruitmentPrompt.HasCompleted)
        {
            return;
        }

        int acceptedTargetHandle = _recruitmentPromptTargetHandle;

        ClearRecruitmentPrompt();

        Ped currentTarget = GetInteractionLockonTargetPed();

        if (currentTarget != null &&
            currentTarget.Exists() &&
            currentTarget.Handle == acceptedTargetHandle &&
            IsRecruitmentCandidate(currentTarget))
        {
            RecruitPed(currentTarget);
        }
    }

    private void CreateRecruitmentPrompt(Ped targetPed)
    {
        if (targetPed == null || !targetPed.Exists())
        {
            return;
        }

        /*
         * INPUT_INTERACT_OPTION1 is documented in ResearchNotes.md as
         * 0x760A9C6F (the default G action). Casting the hash avoids
         * relying on an unverified enum member spelling for this first test.
         */
        RDR2.eInputType recruitmentInput =
            unchecked((RDR2.eInputType)0x760A9C6F);

        _recruitmentPrompt = RDR2.UI.Prompt.CreatePrompt(
            recruitmentInput,
            RDR2.UI.eUseContextMode.Press,
            "Recruit",
            0,
            0,
            0,
            0,
            default(RDR2.UI.PromptTimingEvent));

        if (_recruitmentPrompt == null)
        {
            return;
        }

        _recruitmentPrompt.Text = "Recruit";
        _recruitmentPrompt.Enabled = true;
        _recruitmentPrompt.Visible = true;

        _recruitmentPromptTargetHandle = targetPed.Handle;
        _recruitmentPromptGroup = -1;
        _recruitmentPromptNativeHandle =
            TryGetNativePromptHandle(_recruitmentPrompt);
        _recruitmentPromptAttached = false;
        _promptHandleFailureReported = false;
    }

    private void TryAttachRecruitmentPromptToTarget(Ped targetPed)
    {
        if (_recruitmentPrompt == null ||
            targetPed == null ||
            !targetPed.Exists() ||
            targetPed.Handle != _recruitmentPromptTargetHandle)
        {
            return;
        }

        if (_recruitmentPromptNativeHandle < 0)
        {
            _recruitmentPromptNativeHandle =
                TryGetNativePromptHandle(_recruitmentPrompt);
        }

        if (_recruitmentPromptGroup < 0)
        {
            /*
             * Test candidate for the v2.2.0 native wrapper name/signature.
             * The entity-associated group is used; no ambient group is
             * substituted.
             */
            _recruitmentPromptGroup =
                RDR2.Native.UI._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                    targetPed.Handle);
        }

        if (_recruitmentPromptNativeHandle < 0 ||
            _recruitmentPromptGroup < 0)
        {
            _recruitmentPrompt.Enabled = false;
            _recruitmentPrompt.Visible = false;

            if (!_promptHandleFailureReported)
            {
                _promptHandleFailureReported = true;
                RDR2.UI.Screen.PrintSubtitle(
                    "Recruit prompt could not be attached to the target.");
            }

            return;
        }

        if (_recruitmentPromptAttached)
        {
            return;
        }

        /*
         * Test candidate for the v2.2.0 native wrapper name/signature.
         * Tab index 0 is the documented test value.
         */
        RDR2.Native.UI._UI_PROMPT_SET_GROUP(
            _recruitmentPromptNativeHandle,
            _recruitmentPromptGroup,
            0);

        _recruitmentPromptAttached = true;
        _recruitmentPrompt.Enabled = true;
        _recruitmentPrompt.Visible = true;
    }

    private static int TryGetNativePromptHandle(RDR2.UI.Prompt prompt)
    {
        if (prompt == null)
        {
            return -1;
        }

        const BindingFlags flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        string[] likelyNames =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "_handle",
            "m_handle",
            "_promptHandle",
            "m_promptHandle"
        };

        Type promptType = prompt.GetType();

        foreach (string name in likelyNames)
        {
            PropertyInfo property = promptType.GetProperty(name, flags);

            if (property != null &&
                property.GetIndexParameters().Length == 0)
            {
                try
                {
                    int value = Convert.ToInt32(
                        property.GetValue(prompt, null));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Continue trying other likely handle members.
                }
            }

            FieldInfo field = promptType.GetField(name, flags);

            if (field != null)
            {
                try
                {
                    int value = Convert.ToInt32(field.GetValue(prompt));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Continue trying other likely handle members.
                }
            }
        }

        /*
         * Fallback for the prototype: inspect integer members if the API
         * exposes the native prompt handle under an undocumented name.
         */
        foreach (FieldInfo field in promptType.GetFields(flags))
        {
            if (field.FieldType != typeof(int) &&
                field.FieldType != typeof(uint))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(field.GetValue(prompt));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible fields.
            }
        }

        foreach (PropertyInfo property in promptType.GetProperties(flags))
        {
            if (property.GetIndexParameters().Length != 0 ||
                (property.PropertyType != typeof(int) &&
                 property.PropertyType != typeof(uint)))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(
                    property.GetValue(prompt, null));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible properties.
            }
        }

        return -1;
    }

    private void ClearRecruitmentPrompt()
    {
        if (_recruitmentPrompt != null)
        {
            _recruitmentPrompt.Enabled = false;
            _recruitmentPrompt.Visible = false;

            if (_recruitmentPromptAttached &&
                _recruitmentPromptNativeHandle >= 0 &&
                _recruitmentPromptGroup >= 0)
            {
                /*
                 * Test candidate for the v2.2.0 native wrapper name and
                 * parameter order. Remove the action prompt from its
                 * previous target group before discarding its state.
                 */
                RDR2.Native.UI.PROMPT_REMOVE_GROUP(
                    _recruitmentPromptNativeHandle,
                    _recruitmentPromptGroup);
            }
        }

        _recruitmentPrompt = null;
        _recruitmentPromptTargetHandle = -1;
        _recruitmentPromptGroup = -1;
        _recruitmentPromptNativeHandle = -1;
        _recruitmentPromptAttached = false;
        _promptHandleFailureReported = false;
    }

    private Ped GetInteractionLockonTargetPed()
    {
        return Game.Player.GetInteractionLockonTargetEntity() as Ped;
    }

    private void ToggleCompanionFollowState(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        WorldEntityData entity =
            _entityRegistry.GetEntity(companionId);

        CompanionData companion =
            _companionSystem.GetCompanion(companionId);

        bool shouldFollow = entity != null
            ? !entity.IsFollowingPlayer
            : companion != null && !companion.IsFollowingPlayer;

        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow &&
            (playerPed == null || !playerPed.Exists()))
        {
            return;
        }

        string task = shouldFollow ? "FollowPlayer" : "Stay";

        if (entity != null)
        {
            entity.IsFollowingPlayer = shouldFollow;
            entity.CurrentTask = task;
        }

        if (companion != null)
        {
            companion.IsFollowingPlayer = shouldFollow;
            companion.CurrentTask = task;
        }

        _gangManagementSystem.ApplyTacticalContext(
            companionId,
            companion != null ? companion.CurrentCombatStyle : null,
            task,
            shouldFollow);

        companionPed.AlwaysKeepTask = true;

        if (shouldFollow)
        {
            companionPed.Task.FollowToOffsetOfEntity(
                playerPed,
                new Vector3(0f, -2f, 0f),
                2f,
                -1,
                3f,
                true);
        }
        else
        {
            companionPed.Task.StandStill(-1);
        }

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            shouldFollow
                ? "Gang member is following you."
                : "Gang member will stay here.");
    }

    private void DismissCompanion(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        companionPed.Task.ClearTasks(true);
        companionPed.AlwaysKeepTask = false;

        _runtimeCompanionPeds.Remove(companionId);
        _companionSystem.RemoveCompanion(companionId);
        _entityRegistry.RemoveEntity(companionId);

        if (_gangManagementSystem.MemberExists(companionId))
        {
            _gangManagementSystem.RemoveMember(companionId);
        }

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle("Gang member dismissed.");
    }

    private bool IsRecruitmentCandidate(Ped ped)
    {
        if (ped == null ||
            !ped.Exists() ||
            ped.IsPlayer ||
            (!ped.IsHuman && !ped.IsAnimal) ||
            ped.IsDeadOrDying ||
            ped.IsInVehicle ||
            ped.IsInCombat ||
            ped.IsFleeing ||
            IsRuntimeCompanion(ped))
        {
            return false;
        }

        Ped playerPed = Game.Player.Character;

        return playerPed != null &&
               playerPed.Exists() &&
               ped.Position.DistanceTo(playerPed.Position) <= 5f;
    }

    private void RecruitPed(Ped targetPed)
    {
        if (!IsRecruitmentCandidate(targetPed))
        {
            RDR2.UI.Screen.PrintSubtitle(
                "That person cannot be recruited right now.");
            return;
        }

        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        string companionId = Guid.NewGuid().ToString("N");
        string name =
            targetPed.IsHuman ? "Gang Member" : "Animal Companion";
        string role =
            targetPed.IsHuman ? "Companion" : "Animal Companion";
        string combatStyle =
            targetPed.IsHuman ? "Ranged Medium" : "Animal Support";

        CompanionData companion = new CompanionData
        {
            CompanionId = companionId,
            Name = name,
            Species = targetPed.IsHuman ? "Human" : "Animal",
            IsHuman = targetPed.IsHuman,
            Role = role,
            Personality = "Undetermined",
            Loyalty = 50,
            CurrentCombatStyle = combatStyle,
            CurrentTask = "FollowPlayer",
            IsRecruitable = false,
            IsFollowingPlayer = true
        };

        WorldEntityData entity = new WorldEntityData
        {
            EntityId = companionId,
            EntityType = targetPed.IsHuman ? "Human" : "Animal",
            DisplayName = name,
            FactionId = "PlayerGang",
            IsAlive = true,
            IsRecruitable = false,
            IsFollowingPlayer = true,
            CurrentTask = companion.CurrentTask,
            CombatStyle = companion.CurrentCombatStyle,
            Loyalty = companion.Loyalty
        };

        GangMember gangMember = new GangMember
        {
            MemberId = companionId,
            Name = name,
            Role = role,
            CombatStyle = combatStyle,
            CurrentTask = "FollowPlayer",
            Loyalty = companion.Loyalty,
            IsFollowingPlayer = true
        };

        _companionSystem.RegisterCompanion(companion);
        _entityRegistry.RegisterEntity(entity);
        _gangManagementSystem.RegisterMember(gangMember);
        _runtimeCompanionPeds[companionId] = targetPed;

        targetPed.AlwaysKeepTask = true;
        targetPed.Task.FollowToOffsetOfEntity(
            playerPed,
            new Vector3(0f, -2f, 0f),
            2f,
            -1,
            3f,
            true);

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            targetPed.IsHuman
                ? "Gang member recruited. They will follow you."
                : "Animal companion recruited. They will follow you.");
    }

    private bool IsRuntimeCompanion(Ped ped)
    {
        if (ped == null)
        {
            return false;
        }

        foreach (Ped companionPed in _runtimeCompanionPeds.Values)
        {
            if (companionPed != null &&
                companionPed.Exists() &&
                companionPed.Handle == ped.Handle)
            {
                return true;
            }
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
            {
                continue;
            }

            hasRuntimeCompanion = true;

            WorldEntityData entity =
                _entityRegistry.GetEntity(pair.Key);

            if (entity != null && entity.IsFollowingPlayer)
            {
                hasFollowingCompanion = true;
            }
        }

        if (!hasRuntimeCompanion)
        {
            RDR2.UI.Screen.PrintSubtitle(
                "No recruited companions are available in this session.");
            return;
        }

        bool shouldFollow = !hasFollowingCompanion;
        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow &&
            (playerPed == null || !playerPed.Exists()))
        {
            RDR2.UI.Screen.PrintSubtitle("Unable to find the player.");
            return;
        }

        foreach (KeyValuePair<string, Ped> pair in _runtimeCompanionPeds)
        {
            Ped companionPed = pair.Value;

            if (companionPed == null || !companionPed.Exists())
            {
                continue;
            }

            string task = shouldFollow ? "FollowPlayer" : "Stay";

            WorldEntityData entity =
                _entityRegistry.GetEntity(pair.Key);

            if (entity != null)
            {
                entity.IsFollowingPlayer = shouldFollow;
                entity.CurrentTask = task;
            }

            CompanionData companion =
                _companionSystem.GetCompanion(pair.Key);

            if (companion != null)
            {
                companion.IsFollowingPlayer = shouldFollow;
                companion.CurrentTask = task;
            }

            _gangManagementSystem.ApplyTacticalContext(
                pair.Key,
                companion != null ? companion.CurrentCombatStyle : null,
                task,
                shouldFollow);

            companionPed.AlwaysKeepTask = true;

            if (shouldFollow)
            {
                companionPed.Task.FollowToOffsetOfEntity(
                    playerPed,
                    new Vector3(0f, -2f, 0f),
                    2f,
                    -1,
                    3f,
                    true);
            }
            else
            {
                companionPed.Task.StandStill(-1);
            }
        }

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            shouldFollow
                ? "Gang members are following you."
                : "Gang members will stay here.");
    }

    private void OnTick(object sender, EventArgs e)
    {
        UpdateRecruitmentPrompt();

        _tickCounter++;

        if (_tickCounter % 30 == 0)
        {
            _cachedNearbyEnemies = GetNearbyCombatPeds();
        }

        Ped playerPed = Game.Player.Character;

        if (playerPed != null && playerPed.Exists())
        {
            _coreAISystem.Update(
                playerPed,
                _runtimeCompanionPeds,
                playerPed.IsInCombat,
                _cachedNearbyEnemies);
        }

        if (_tickCounter % 30 != 0)
        {
            return;
        }

        UpdateEntityAI();
        SaveAll();
    }

    private List<Ped> GetNearbyCombatPeds()
    {
        List<Ped> nearbyEnemies = new List<Ped>();
        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            return nearbyEnemies;
        }

        Ped[] worldPeds = World.GetAllPeds();

        if (worldPeds == null)
        {
            return nearbyEnemies;
        }

        foreach (Ped ped in worldPeds)
        {
            if (ped == null ||
                !ped.Exists() ||
                ped.Handle == playerPed.Handle ||
                ped.IsDeadOrDying ||
                !ped.IsInCombat ||
                IsRuntimeCompanion(ped))
            {
                continue;
            }

            if (ped.Position.DistanceTo(playerPed.Position) > 50f)
            {
                continue;
            }

            nearbyEnemies.Add(ped);
        }

        return nearbyEnemies;
    }

    private void LoadAll()
    {
        _saveRoot = JsonSaveSystem.Load();

        if (_saveRoot == null)
        {
            _saveRoot = new SaveRoot();
        }

        _gangManagementSystem.LoadFromSaveRoot(_saveRoot);
        _jobSystem.LoadFromSaveRoot(_saveRoot);

        if (_saveRoot.World != null &&
            _saveRoot.World.Companions != null)
        {
            _companionSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Companions));
        }

        if (_saveRoot.BankAccounts != null)
        {
            _bankSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.BankAccounts));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Wagons != null)
        {
            _wagonSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Wagons));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.WeaponPlatforms != null)
        {
            _weaponSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.WeaponPlatforms));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Locks != null)
        {
            _lockSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Locks));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Destructibles != null)
        {
            _dynamiteSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.Destructibles));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.TacticalOrders != null)
        {
            _tacticalSquadAiSystem.LoadState(
                JsonConvert.SerializeObject(
                    _saveRoot.World.TacticalOrders));
        }

        if (_saveRoot.World != null &&
            _saveRoot.World.Commands != null)
        {
            _commandSystem.LoadState(
                JsonConvert.SerializeObject(_saveRoot.World.Commands));
        }

        if (_saveRoot.WorldEntities != null)
        {
            _entityRegistry.LoadState(
                JsonConvert.SerializeObject(_saveRoot.WorldEntities));
        }

        LoadCompanionAIStates();
        SyncInteractionRecordsFromSave();
    }

    private void LoadCompanionAIStates()
    {
        if (_saveRoot == null ||
            _saveRoot.World == null ||
            _saveRoot.World.CompanionAIStates == null)
        {
            return;
        }

        foreach (CompanionAIStateData savedState
                 in _saveRoot.World.CompanionAIStates)
        {
            if (savedState == null ||
                string.IsNullOrWhiteSpace(savedState.CompanionId))
            {
                continue;
            }

            CompanionAIState state = new CompanionAIState
            {
                EntityId = savedState.CompanionId,
                Personality =
                    string.IsNullOrWhiteSpace(savedState.Personality)
                        ? "Neutral"
                        : savedState.Personality,
                Morale = savedState.Morale,
                Fatigue = savedState.Fatigue,
                Hunger = savedState.Hunger,
                Thirst = savedState.Thirst,
                PrefersStealth = savedState.PrefersStealth,
                CurrentGoal = savedState.CurrentGoal,
                LastActivityTimestamp =
                    savedState.LastActivityTimestamp,
                ExperiencePoints = savedState.ExperiencePoints,
                SkillProgression =
                    savedState.SkillProgression ??
                    new Dictionary<string, int>(),
                CompletedJobs =
                    savedState.CompletedJobs ??
                    new List<string>(),
                TotalEarnings = savedState.TotalEarnings,
                BanksVisited = savedState.BanksVisited,
                DepositsWithdrawn = savedState.DepositsWithdrawn
            };

            state.Morale = Clamp(state.Morale, 0, 100);
            state.Fatigue = Clamp(state.Fatigue, 0, 100);
            state.Hunger = Clamp(state.Hunger, 0, 100);
            state.Thirst = Clamp(state.Thirst, 0, 100);

            _coreAISystem.SetAIState(
                savedState.CompanionId,
                state);
        }
    }

    private void SyncInteractionRecordsFromSave()
    {
        if (_saveRoot == null || _saveRoot.World == null)
        {
            return;
        }

        if (_saveRoot.World.Locks != null)
        {
            foreach (LockRecord record in _saveRoot.World.Locks)
            {
                if (record != null)
                {
                    _interactionSystem.RegisterLock(record);
                }
            }
        }

        if (_saveRoot.World.Destructibles != null)
        {
            foreach (DestructibleRecord record
                     in _saveRoot.World.Destructibles)
            {
                if (record != null)
                {
                    _interactionSystem.RegisterDestructible(record);
                }
            }
        }
    }

    private void SyncWorldStateFromSystems()
    {
        if (_saveRoot == null)
        {
            _saveRoot = new SaveRoot();
        }

        if (_saveRoot.World == null)
        {
            _saveRoot.World = new WorldState();
        }

        _gangManagementSystem.WriteToSaveRoot(_saveRoot);

        _saveRoot.World.Companions =
            _companionSystem.GetCompanions();

        _saveRoot.BankAccounts =
            _bankSystem.GetAllAccounts();

        _jobSystem.WriteToSaveRoot(_saveRoot);

        _saveRoot.World.Wagons =
            _wagonSystem.GetWagons();

        _saveRoot.World.WeaponPlatforms =
            _weaponSystem.GetPlatforms();

        _saveRoot.World.Locks =
            _lockSystem.GetLocks();

        _saveRoot.World.Destructibles =
            _dynamiteSystem.GetDestructibles();

        _saveRoot.World.TacticalOrders =
            _tacticalSquadAiSystem.GetOrders();

        _saveRoot.World.Commands =
            _commandSystem.GetCommands();

        _saveRoot.WorldEntities =
            _entityRegistry.GetEntities();

        List<CompanionAIStateData> savedAIStates =
            new List<CompanionAIStateData>();

        foreach (WorldEntityData entity
                 in _entityRegistry.GetByFaction("PlayerGang"))
        {
            if (entity == null ||
                string.IsNullOrWhiteSpace(entity.EntityId))
            {
                continue;
            }

            CompanionAIState state =
                _coreAISystem.GetAIState(entity.EntityId);

            if (state == null)
            {
                continue;
            }

            savedAIStates.Add(new CompanionAIStateData
            {
                CompanionId = entity.EntityId,
                Personality = state.Personality,
                Morale = state.Morale,
                Fatigue = state.Fatigue,
                Hunger = state.Hunger,
                Thirst = state.Thirst,
                PrefersStealth = state.PrefersStealth,
                CurrentGoal = state.CurrentGoal,
                LastActivityTimestamp = state.LastActivityTimestamp,
                ExperiencePoints = state.ExperiencePoints,
                SkillProgression =
                    state.SkillProgression ??
                    new Dictionary<string, int>(),
                CompletedJobs =
                    state.CompletedJobs ??
                    new List<string>(),
                TotalEarnings = state.TotalEarnings,
                BanksVisited = state.BanksVisited,
                DepositsWithdrawn = state.DepositsWithdrawn
            });
        }

        _saveRoot.World.CompanionAIStates = savedAIStates;
    }

    private void UpdateEntityAI()
    {
        _commandSystem.ProcessActiveCommands(
            _tacticalDecisionSystem);

        foreach (WorldEntityData entity in _entityRegistry.GetEntities())
        {
            if (entity == null || !entity.IsAlive)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(entity.CombatStyle))
            {
                entity.CombatStyle =
                    _tacticalDecisionSystem.ResolveCombatStyle(
                        "Normal",
                        false,
                        false,
                        false,
                        false);
            }

            if (entity.IsFollowingPlayer &&
                string.IsNullOrWhiteSpace(entity.CurrentTask))
            {
                entity.CurrentTask = "FollowPlayer";
            }

            if (entity.IsRecruitable && entity.Loyalty < 0)
            {
                entity.Loyalty = 0;
            }

            if (entity.SupportSkill < 0)
            {
                entity.SupportSkill = 0;
            }

            if (entity.CombatSkill < 0)
            {
                entity.CombatSkill = 0;
            }

            if (entity.StealthSkill < 0)
            {
                entity.StealthSkill = 0;
            }

            _gangManagementSystem.ApplyTacticalContext(
                entity.EntityId,
                entity.CombatStyle,
                entity.CurrentTask,
                entity.IsFollowingPlayer);
        }

        foreach (TacticalOrderData order
                 in _tacticalSquadAiSystem.GetOrders())
        {
            if (order == null || order.IsCompleted)
            {
                continue;
            }

            _tacticalDecisionSystem.ApplyOrderToSquad(order);
        }
    }

    private void SaveAll()
    {
        SyncWorldStateFromSystems();
        JsonSaveSystem.Save(_saveRoot);
    }

    private static int Clamp(int value, int minimum, int maximum)
    {
        return Math.Max(minimum, Math.Min(maximum, value));
    }
}