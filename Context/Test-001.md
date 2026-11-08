<!-- Test lines 1-250 of 1246 -->
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const uint InputInteractOption1 = 0x760A9C6Fu;
    private const int TargetScanIntervalMilliseconds = 500;
    private const int InputHeartbeatIntervalMilliseconds = 2000;
    private const float TargetSelectionDistance = 8.0f;
    private const float TargetRetentionDistance = 9.0f;

    private const int VarStringType = 10;
    private const string VarStringFormat = "LITERAL_STRING";
    private const string PromptText = "Recruit";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly Dictionary<string, MethodInfo> _probeMethods =
        new Dictionary<string, MethodInfo>();

    private readonly Dictionary<string, bool> _lastProbeValues =
        new Dictionary<string, bool>();

    private readonly Dictionary<string, bool> _currentProbeValues =
        new Dictionary<string, bool>();

    private readonly HashSet<string> _loggedErrors =
        new HashSet<string>();

    private int _trackedTargetHandle;
    private int _focusedPromptHandle;
    private int _focusedPromptGroupId;

    private bool _targetWasFocused;
    private bool _hasScannedForTarget;
    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;
    private bool _promptErrorLogged;

    private DateTime _lastTargetScanUtc;
    private DateTime _lastInputHeartbeatUtc;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused NPC custom Recruit prompt test initialized ===");
        Log("Prompt text source: VAR_STRING(" +
            VarStringType + ", \"" + VarStringFormat +
            "\", \"" + PromptText + "\").");
        Log("Input control: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8"));
        Log("The prompt is attached to the focused NPC target's prompt group.");
        Log("This test does not recruit, change gang membership, or trigger an animation.");

        ResolveProbe("UI_PROMPT_IS_ACTIVE", new object[] { 0 });
        ResolveProbe("UI_PROMPT_IS_PRESSED", new object[] { 0 });
        ResolveProbe("UI_PROMPT_IS_JUST_PRESSED", new object[] { 0 });
        ResolveProbe(
            "IS_CONTROL_ENABLED",
            new object[] { ControlPadIndex, InputInteractOption1 });
        ResolveProbe(
            "IS_CONTROL_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 });
        ResolveProbe(
            "IS_CONTROL_JUST_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 });

        CreateFocusedPrompt();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC. Check for G Recruit, then tap G once.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateNearbyTargetIfNeeded();
            SampleFocusAndInput();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Recruit prompt test error. Check Test_Log.txt.");
            }
        }
    }

    private void ResolveProbe(string nativeName, object[] sampleArguments)
    {
        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            sampleArguments);

        _probeMethods[nativeName] = method;

        if (method == null)
        {
            Log("PROBE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + sampleArguments.Length);
            return;
        }

        Log("PROBE WRAPPER: " + nativeName +
            "; signature=" + FormatMethodSignature(method));
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

        Log("HUMAN TARGET BEGIN: target=" +
            _trackedTargetHandle + "; distance=" + distance);
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

        return retainedPed ?? nearestPed;
    }

    private bool IsHumanEntity(int entityHandle)
    {
        if (entityHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void SampleFocusAndInput()
    {
        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" +
                _trackedTargetHandle);
            ClearTrackedTarget();
            return;
        }

        int focusedTargetHandle = GetCurrentHumanTargetHandle();
        bool targetIsFocused =
            focusedTargetHandle == _trackedTargetHandle;

        if (targetIsFocused != _targetWasFocused)
        {
            _targetWasFocused = targetIsFocused;

```
