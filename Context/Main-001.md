<!-- Main lines 1-250 of 1135 -->
```csharp
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
```
