<!-- Test lines 1-250 of 1130 -->
```csharp
using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;

    private const string IloPromptText = "Recruit";
    private const float IloRadius = 2.5f;
    private const float IloFocusDistance = 2.5f;
    private const int IloFlags = 0;
    private const float IloUnknownFloat1 = 0.0f;
    private const float IloUnknownFloat2 = 0.0f;
    private const int IloCustomPromptHandle = 0;
    private const bool IloShowApproachPrompt = true;
    private const int IloUnknownInt = -1;

    // This registered game string is a baseline, not the final "Recruit" label.
    // The API has a known issue with custom prompt text created through VAR_STRING.
    private const string FocusedPromptTextKey = "WARDROBE_INSPECT_PROMPT";

    private const float TargetSelectionDistance = 8.0f;
    private const float TargetRetentionDistance = 9.0f;
    private const int TargetScanIntervalMilliseconds = 500;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly MethodInfo _registerIloMethod;
    private readonly MethodInfo _unregisterIloMethod;

    private int _trackedTargetHandle;
    private int _iloRegisteredTargetHandle;
    private bool _iloRegistrationReturnedTrue;

    private int _focusedPromptHandle;
    private int _focusedPromptGroupId;
    private bool _promptWasPressed;
    private bool _targetWasFocused;

    private bool _hasScannedForTarget;
    private DateTime _lastTargetScanUtc;

    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;
    private bool _promptErrorLogged;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        _registerIloMethod = FindNativeMethod(
            "REGISTER_INTERACTION_LOCKON_PROMPT",
            new[]
            {
                typeof(int),
                typeof(string),
                typeof(float),
                typeof(float),
                typeof(int),
                typeof(float),
                typeof(float),
                typeof(int),
                typeof(bool),
                typeof(int)
            });

        _unregisterIloMethod = FindNativeMethod(
            "UNREGISTER_INTERACTION_LOCKON_PROMPT",
            new[]
            {
                typeof(int)
            });

        Log("=== Focused NPC prompt-group baseline test initialized ===");
        Log("This test registers the ILO on the nearest human NPC within " +
            TargetSelectionDistance + " metres before focus.");
        Log("The ILO approach text is \"" + IloPromptText + "\".");
        Log("While the NPC is focused, the test tries to attach a G prompt " +
            "using the target entity's prompt group.");
        Log("The focused prompt uses the registered text key \"" +
            FocusedPromptTextKey + "\", not custom Recruit text.");
        Log("No recruitment action is executed and no gang data is changed.");

        Log("ILO arguments: radius=" + IloRadius +
            "; focusDistance=" + IloFocusDistance +
            "; flags=" + IloFlags +
            "; unknownFloat1=" + IloUnknownFloat1 +
            "; unknownFloat2=" + IloUnknownFloat2 +
            "; customPromptHandle=" + IloCustomPromptHandle +
            "; showApproachPrompt=" + IloShowApproachPrompt +
            "; unknownInt=" + IloUnknownInt);

        LogResolvedMethod(
            "REGISTER_INTERACTION_LOCKON_PROMPT",
            _registerIloMethod);

        LogResolvedMethod(
            "UNREGISTER_INTERACTION_LOCKON_PROMPT",
            _unregisterIloMethod);

        if (_registerIloMethod == null)
        {
            Log("ILO SETUP UNAVAILABLE: no compatible register wrapper was found.");
        }

        if (_unregisterIloMethod == null)
        {
            Log("ILO CLEANUP UNAVAILABLE: no compatible unregister wrapper was found.");
        }

        CreateFocusedPrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Prompt test ready. Focus a human NPC.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateNearbyTargetIfNeeded();
            SampleFocusAndPrompt();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));

                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt test error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdateNearbyTargetIfNeeded()
    {
        DateTime now = DateTime.UtcNow;

        if (_hasScannedForTarget &&
            (now - _lastTargetScanUtc).TotalMilliseconds <
            TargetScanIntervalMilliseconds)
        {
            return;
        }

        _hasScannedForTarget = true;
        _lastTargetScanUtc = now;

        Ped selectedPed = FindNearbyHumanTarget();
        int selectedHandle = 0;

        if (selectedPed != null && selectedPed.Exists())
        {
            selectedHandle = selectedPed.Handle;
        }

        if (selectedHandle == _trackedTargetHandle)
        {
            return;
        }

        ClearTrackedTarget();

        if (selectedHandle <= 0)
        {
            return;
        }

        _trackedTargetHandle = selectedHandle;

        float distance = selectedPed.Position.DistanceTo(
            Game.Player.Character.Position);

        Log("HUMAN TARGET BEGIN: target=" + _trackedTargetHandle +
            "; distance=" + distance +
            "; freeFocusing=" + Game.Player.IsFreeFocusing +
            "; registeredBeforeFocus=true");

        RegisterIloForTarget(_trackedTargetHandle);
    }

    private Ped FindNearbyHumanTarget()
    {
        Ped playerPed = Game.Player.Character;

        if (playerPed == null || !playerPed.Exists())
        {
            return null;
        }

        Ped[] worldPeds = World.GetAllPeds();

        if (worldPeds == null)
        {
            return null;
        }

        Ped retainedPed = null;
        Ped nearestPed = null;
        float nearestDistance = TargetSelectionDistance + 1.0f;

        foreach (Ped ped in worldPeds)
        {
            if (ped == null ||
                !ped.Exists() ||
                ped.Handle == playerPed.Handle ||
                ped.IsDeadOrDying ||
                !IsHumanEntity(ped.Handle))
            {
                continue;
            }

            float distance = ped.Position.DistanceTo(playerPed.Position);

            if (ped.Handle == _trackedTargetHandle &&
                distance <= TargetRetentionDistance)
            {
                retainedPed = ped;
                continue;
            }

            if (distance <= TargetSelectionDistance &&
                distance < nearestDistance)
            {
                nearestPed = ped;
                nearestDistance = distance;
            }
        }

        if (retainedPed != null)
        {
            return retainedPed;
        }

        return nearestPed;
    }

    private bool IsHumanEntity(int entityHandle)
    {
        if (entityHandle <= 0 ||
```
