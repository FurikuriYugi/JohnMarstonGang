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
    private bool _targetWasFocused;
    private bool _hasScannedForTarget;
    private DateTime _lastTargetScanUtc;
    private bool _generalErrorLogged;
    private bool _targetResolutionErrorLogged;

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

        Log("=== ILO approach-prompt timing test initialized ===");
        Log("This test registers the ILO on the nearest human NPC within " +
            TargetSelectionDistance + " metres, before the NPC is aimed at.");
        Log("The ILO radius is " + IloRadius +
            " metres and its approach-prompt argument is true.");
        Log("The test observes focus and INPUT_INTERACT_OPTION1, but does not " +
            "create a UI prompt, bind a control, recruit an NPC, or change gang data.");
        Log("Registration arguments: radius=" + IloRadius +
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

        RDR2.UI.Screen.PrintSubtitle(
            "ILO test ready. Walk near a human NPC.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateNearbyTargetIfNeeded();
            SampleFocusAndGControl();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "ILO test error. Check Test_Log.txt.");
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
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void SampleFocusAndGControl()
    {
        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" + _trackedTargetHandle);
            ClearTrackedTarget();
            return;
        }

        int focusedTargetHandle = GetCurrentHumanTargetHandle();
        bool targetIsFocused =
            focusedTargetHandle == _trackedTargetHandle;

        if (targetIsFocused != _targetWasFocused)
        {
            _targetWasFocused = targetIsFocused;

            Log(targetIsFocused
                ? "TRACKED TARGET FOCUS BEGIN: target=" + _trackedTargetHandle
                : "TRACKED TARGET FOCUS END: target=" + _trackedTargetHandle);

            if (targetIsFocused)
            {
                LogGControlAvailability(_trackedTargetHandle);
            }
        }

        if (!targetIsFocused)
        {
            return;
        }

        bool pressed =
            RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                ControlPadIndex,
                InputInteractOption1);

        if (pressed)
        {
            Log("G CANDIDATE JUST PRESSED: target=" +
                _trackedTargetHandle +
                "; name=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; note=observed only; no recruitment action was executed");
        }
    }

    private int GetCurrentHumanTargetHandle()
    {
        try
        {
            if (Game.Player.IsFreeFocusing)
            {
                int focusedHandle = 0;
                bool foundFocusedTarget;

                unsafe
                {
                    foundFocusedTarget =
                        RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                            Game.Player.Handle,
                            &focusedHandle);
                }

                if (foundFocusedTarget &&
                    IsHumanEntity(focusedHandle))
                {
                    return focusedHandle;
                }
            }

            Entity aimedEntity = Game.Player.GetFreeAimEntity();

            if (aimedEntity != null && aimedEntity.Exists())
            {
                int aimedHandle = aimedEntity.Handle;

                if (IsHumanEntity(aimedHandle))
                {
                    return aimedHandle;
                }
            }
        }
        catch (Exception exception)
        {
            if (!_targetResolutionErrorLogged)
            {
                _targetResolutionErrorLogged = true;
                Log("TARGET RESOLUTION ERROR: " +
                    GetInnermostMessage(exception));
            }
        }

        return 0;
    }

    private void LogGControlAvailability(int targetHandle)
    {
        try
        {
            bool enabled =
                RDR2.Native.PAD.IS_CONTROL_ENABLED(
                    ControlPadIndex,
                    InputInteractOption1);

            Log("G CANDIDATE CONTROL: target=" + targetHandle +
                "; name=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; enabled=" + enabled +
                "; note=reported G mapping; not bound by the ILO native");
        }
        catch (Exception exception)
        {
            Log("G CONTROL AVAILABILITY ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void RegisterIloForTarget(int targetHandle)
    {
        if (_registerIloMethod == null)
        {
            return;
        }

        try
        {
            object result = _registerIloMethod.Invoke(
                null,
                new object[]
                {
                    targetHandle,
                    IloPromptText,
                    IloRadius,
                    IloFocusDistance,
                    IloFlags,
                    IloUnknownFloat1,
                    IloUnknownFloat2,
                    IloCustomPromptHandle,
                    IloShowApproachPrompt,
                    IloUnknownInt
                });

            bool registered = result is bool && (bool)result;

            Log("ILO REGISTER RESULT: target=" + targetHandle +
                "; returned=" + FormatResult(result));

            _iloRegisteredTargetHandle = targetHandle;
            _iloRegistrationReturnedTrue = registered;

            if (registered)
            {
                Log("ILO REGISTERED: target=" + targetHandle +
                    "; text=\"" + IloPromptText + "\"" +
                    "; registeredBeforeFocus=true" +
                    "; note=true return does not prove the prompt rendered");
            }
        }
        catch (Exception exception)
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;

            Log("ILO REGISTER ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
        }
    }

    private void ClearTrackedTarget()
    {
        if (_trackedTargetHandle > 0)
        {
            Log("HUMAN TARGET END: target=" + _trackedTargetHandle);
        }

        UnregisterIlo();

        _trackedTargetHandle = 0;
        _targetWasFocused = false;
    }

    private void UnregisterIlo()
    {
        if (_iloRegisteredTargetHandle <= 0 ||
            !_iloRegistrationReturnedTrue)
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
            return;
        }

        if (_unregisterIloMethod == null)
        {
            Log("ILO CLEANUP SKIPPED: no compatible unregister wrapper; target=" +
                _iloRegisteredTargetHandle);

            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
            return;
        }

        int targetHandle = _iloRegisteredTargetHandle;

        try
        {
            object result = _unregisterIloMethod.Invoke(
                null,
                new object[]
                {
                    targetHandle
                });

            Log("ILO UNREGISTER RESULT: target=" + targetHandle +
                "; returned=" + FormatResult(result));
        }
        catch (Exception exception)
        {
            Log("ILO UNREGISTER ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
        }
        finally
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
        }
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        Type[] parameterTypes)
    {
        Assembly apiAssembly = typeof(Game).Assembly;
        Type[] apiTypes;

        try
        {
            apiTypes = apiAssembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            System.Collections.Generic.List<Type> loadedTypes =
                new System.Collections.Generic.List<Type>();

            if (exception.Types != null)
            {
                foreach (Type type in exception.Types)
                {
                    if (type != null)
                    {
                        loadedTypes.Add(type);
                    }
                }
            }

            apiTypes = loadedTypes.ToArray();
        }

        foreach (Type type in apiTypes)
        {
            if (type == null)
            {
                continue;
            }

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (!string.Equals(
                    method.Name,
                    methodName,
                    StringComparison.Ordinal))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length != parameterTypes.Length)
                {
                    continue;
                }

                bool matches = true;

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType != parameterTypes[i])
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private void LogResolvedMethod(
        string nativeName,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("NATIVE WRAPPER " + nativeName + ": not found");
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();
        string signature = method.DeclaringType.FullName +
            "." + method.Name + "(";

        for (int i = 0; i < parameters.Length; i++)
        {
            if (i > 0)
            {
                signature += ", ";
            }

            signature += parameters[i].ParameterType.FullName;
        }

        signature += ") -> " + method.ReturnType.FullName;

        Log("NATIVE WRAPPER " + nativeName + ": " + signature);
    }

    private static string FormatResult(object result)
    {
        if (result == null)
        {
            return "null";
        }

        return result.GetType().FullName + ":" + result;
    }

    private static string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.GetType().FullName + ": " + current.Message;
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }
}