<!-- Test lines 1-250 of 1468 -->
```csharp
using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int RecruitInputHash = unchecked((int)0x760A9C6F);
    private const float TestPromptRadius = 3.0f;
    private const int AmbientPromptP2 = 2;
    private const int AmbientPromptTabAmount = 1;
    private const int AmbientPromptFlags = 0;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExistMethod;
    private MethodInfo _isPedHumanMethod;
    private MethodInfo _registerInteractionLockonPromptMethod;
    private MethodInfo _unregisterInteractionLockonPromptMethod;
    private MethodInfo _setAmbientGroupThisFrameMethod;
    private MethodInfo _varStringMethod;

    private object _recruitPrompt;
    private int _recruitPromptHandle = -1;
    private int _recruitPromptGroup = -1;
    private int _targetHandle;
    private int _iloTargetHandle = -1;

    private bool _iloRegistrationAttempted;
    private bool _registrationAttemptedForTarget;
    private bool _promptAttached;
    private bool _selectionHandledForTarget;
    private bool _errorReported;
    private bool _hasLoggedFocusState;
    private bool _lastLoggedFocusState;

    private int _lastLoggedTargetHandle = -1;
    private string _lastPromptState;

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        ClearLog();

        Log("=== Test.cs Initialized ===");
        Log("Log file: " + _logPath);
        Log(
            "TEST: Holster your weapon, stand close to a human NPC, " +
            "and right-mouse focus them.");
        Log(
            "Observe the separate 'Recruit test' lock-on text and " +
            "'G Recruit' action prompt.");
        Log(
            "Press G only if 'G Recruit' is visible. G is a test " +
            "placeholder; it will not recruit or change gang data.");
        Log(
            "The ambient prompt group is invoked every tick while " +
            "the focused target and attached action prompt are valid.");
        Log(
            "Native invocation, prompt properties, and visible " +
            "rendering are logged as separate evidence.");

        ResolvePlayerTargetEntityWrapper();
        ResolveTargetValidationMethods();
        ResolveInteractionLockonPromptMethods();
        ResolveAmbientPromptMethods();

        RDR2.UI.Screen.PrintSubtitle(
            "Holster weapon; focus a human NPC to test Recruit.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateTargetAndPrompts();
        }
        catch (Exception exception)
        {
            if (!_errorReported)
            {
                _errorReported = true;

                string message =
                    "Recruit prompt test error: " +
                    GetExceptionMessage(exception);

                RDR2.UI.Screen.PrintSubtitle(message);
                Log("ERROR: " + message);
                Log("StackTrace: " + exception.StackTrace);
            }
        }
    }

    private void UpdateTargetAndPrompts()
    {
        bool isFreeFocusing = Game.Player.IsFreeFocusing;
        LogFocusStateWhenChanged(isFreeFocusing);

        int observedTargetHandle = 0;

        if (isFreeFocusing)
        {
            int nativeTargetHandle;

            if (TryGetPlayerTargetEntity(out nativeTargetHandle) &&
                nativeTargetHandle > 0 &&
                IsHumanNpc(nativeTargetHandle))
            {
                observedTargetHandle = nativeTargetHandle;
            }
        }

        LogTargetWhenChanged(observedTargetHandle);

        if (observedTargetHandle != _targetHandle)
        {
            ClearCurrentTargetPrompts();

            _targetHandle = observedTargetHandle;
            _iloRegistrationAttempted = false;
            _registrationAttemptedForTarget = false;
            _selectionHandledForTarget = false;
            _lastPromptState = null;

            if (_targetHandle > 0)
            {
                Log(
                    "INTERACTION-LOCK-ON TARGET: human NPC handle " +
                    _targetHandle);
            }
            else
            {
                Log("INTERACTION-LOCK-ON TARGET: none");
            }
        }

        if (_targetHandle <= 0)
        {
            return;
        }

        if (!_iloRegistrationAttempted)
        {
            _iloRegistrationAttempted = true;
            RegisterInteractionLockonPrompt(_targetHandle);
        }

        if (!_registrationAttemptedForTarget &&
            !_selectionHandledForTarget)
        {
            _registrationAttemptedForTarget = true;
            CreateAndAttachRecruitPrompt(_targetHandle);
        }

        if (_promptAttached &&
            _recruitPrompt != null &&
            _recruitPromptGroup >= 0)
        {
            ActivateAmbientPromptGroupThisFrame(
                _targetHandle,
                _recruitPromptGroup);
        }

        LogPromptStateWhenChanged();
        CheckPromptSelection();
    }

    private void RegisterInteractionLockonPrompt(int targetHandle)
    {
        Log(
            "REGISTERING TEST ILO PROMPT for target " +
            targetHandle);

        if (_registerInteractionLockonPromptMethod == null)
        {
            Log(
                "ERROR: REGISTER_INTERACTION_LOCKON_PROMPT wrapper " +
                "was not found.");
            return;
        }

        try
        {
            ParameterInfo[] parameters =
                _registerInteractionLockonPromptMethod.GetParameters();

            if (parameters.Length != 10)
            {
                throw new InvalidOperationException(
                    "Expected 10 parameters; found " +
                    parameters.Length +
                    ".");
            }

            object[] arguments =
            {
                targetHandle,
                "Recruit test",
                TestPromptRadius,
                TestPromptRadius,
                0,
                0.0f,
                0.0f,
                0,
                true,
                -1
            };

            ConvertArgumentsInPlace(arguments, parameters);

            object result =
                _registerInteractionLockonPromptMethod.Invoke(
                    null,
                    arguments);

            _iloTargetHandle = targetHandle;

            if (_registerInteractionLockonPromptMethod.ReturnType ==
                typeof(bool))
            {
                bool succeeded =
                    result != null && Convert.ToBoolean(result);

                Log("ILO NATIVE RESULT: " + succeeded);
            }
            else
            {
                Log(
                    "ILO NATIVE CALL RETURNED; wrapper has no bool result.");
            }

            Log(
                "ILO TEST ARGUMENTS: radius=" +
                TestPromptRadius +
                "; p3=" +
                TestPromptRadius +
                "; flag=0; p5=0; p6=0; prompt=0; p8=true; p9=-1");
        }
        catch (Exception exception)
        {
            _iloTargetHandle = -1;
```
