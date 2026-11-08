using System;
using System.Collections.Generic;
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

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly MethodInfo _registerIloMethod;
    private readonly MethodInfo _unregisterIloMethod;

    private int _trackedTargetHandle;
    private int _iloRegisteredTargetHandle;
    private bool _iloRegistrationReturnedTrue;
    private bool _gControlEnabled;
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

        Log("=== Native ILO recruitment-label test initialized ===");
        Log("This test registers the native ILO prompt text \"Recruit\" on the currently aimed/focused human NPC.");
        Log("This test does not create a UI prompt, assign a prompt group, suppress controls, recruit an NPC, or change gang data.");
        Log("The ILO native does not take a G/control argument. INPUT_INTERACT_OPTION1 is observed as a candidate input only.");
        Log("ILO registration arguments: radius=" + IloRadius +
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
            Log("ILO SETUP UNAVAILABLE: no compatible register wrapper was found in the loaded RDR2 API assembly.");
        }

        if (_unregisterIloMethod == null)
        {
            Log("ILO CLEANUP UNAVAILABLE: no compatible unregister wrapper was found in the loaded RDR2 API assembly.");
        }

        RDR2.UI.Screen.PrintSubtitle(
            "Native ILO test ready. Aim at a human NPC.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateTrackedTarget();
            SampleGControl();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("TEST ERROR: " + GetInnermostMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Native ILO test error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdateTrackedTarget()
    {
        int targetHandle = GetCurrentHumanTargetHandle();

        if (targetHandle == _trackedTargetHandle)
        {
            return;
        }

        ClearTrackedTarget();

        _trackedTargetHandle = targetHandle;

        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        Log("HUMAN TARGET BEGIN: target=" + _trackedTargetHandle +
            "; freeFocusing=" + Game.Player.IsFreeFocusing);

        LogGControlAvailability(_trackedTargetHandle);
        RegisterIloForTarget(_trackedTargetHandle);
    }

    private int GetCurrentHumanTargetHandle()
    {
        int targetHandle = 0;

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

                if (foundFocusedTarget && IsHumanEntity(focusedHandle))
                {
                    return focusedHandle;
                }
            }

            Entity aimedEntity = Game.Player.GetFreeAimEntity();

            if (aimedEntity != null && aimedEntity.Exists())
            {
                targetHandle = aimedEntity.Handle;

                if (IsHumanEntity(targetHandle))
                {
                    return targetHandle;
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

    private bool IsHumanEntity(int entityHandle)
    {
        if (entityHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        Ped playerPed = Game.Player.Character;

        if (playerPed != null &&
            playerPed.Exists() &&
            playerPed.Handle == entityHandle)
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void LogGControlAvailability(int targetHandle)
    {
        try
        {
            _gControlEnabled =
                RDR2.Native.PAD.IS_CONTROL_ENABLED(
                    ControlPadIndex,
                    InputInteractOption1);

            Log("G CANDIDATE CONTROL: target=" + targetHandle +
                "; name=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; enabled=" + _gControlEnabled +
                "; note=reported G mapping; not bound by the ILO native");
        }
        catch (Exception exception)
        {
            _gControlEnabled = false;

            Log("G CONTROL AVAILABILITY ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void SampleGControl()
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
                "; controlEnabled=" + _gControlEnabled +
                "; note=observed only; no recruitment action was executed");
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
        _gControlEnabled = false;
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
            List<Type> loadedTypes = new List<Type>();

            if (exception.Types != null)
            {
                for (int i = 0; i < exception.Types.Length; i++)
                {
                    if (exception.Types[i] != null)
                    {
                        loadedTypes.Add(exception.Types[i]);
                    }
                }
            }

            apiTypes = loadedTypes.ToArray();
        }

        for (int typeIndex = 0;
            typeIndex < apiTypes.Length;
            typeIndex++)
        {
            Type type = apiTypes[typeIndex];

            if (type == null)
            {
                continue;
            }

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            for (int methodIndex = 0;
                methodIndex < methods.Length;
                methodIndex++)
            {
                MethodInfo method = methods[methodIndex];

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

                for (int parameterIndex = 0;
                    parameterIndex < parameters.Length;
                    parameterIndex++)
                {
                    if (parameters[parameterIndex].ParameterType !=
                        parameterTypes[parameterIndex])
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
        string label,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("NATIVE WRAPPER " + label + ": unavailable");
            return;
        }

        Log("NATIVE WRAPPER " + label + ": " +
            FormatMethodSignature(method));
    }

    private static string FormatMethodSignature(MethodInfo method)
    {
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
        return signature;
    }

    private static string FormatResult(object result)
    {
        return result == null
            ? "null"
            : result.GetType().FullName + ":" +
              Convert.ToString(result);
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }

    private static string GetInnermostMessage(Exception exception)
    {
        if (exception == null)
        {
            return "Unknown error.";
        }

        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.GetType().FullName + ": " + current.Message;
    }
}