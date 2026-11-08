using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    // INPUT_INTERACT_OPTION1 in the InteractionLockOn context.
    // The existing test reports its default keyboard mapping as G.
    private const uint RecruitInputActionHash = 0x760A9C6F;
    private const int PromptTextLiteralString = 10;

    // ILO setup parameters for this diagnostic test.
    // The text is not intended to appear as an approach prompt because p8 is false.
    private const float IloRadius = 2.0f;
    private const float IloFocusDistance = 2.0f;
    private const int IloFlags = 0;
    private const float IloUnknownFloat5 = 0.0f;
    private const float IloUnknownFloat6 = 0.0f;
    private const int IloPromptHandle = 0;
    private const bool IloShowApproachPrompt = false;
    private const int IloUnknownInt9 = -1;

    private const string PromptOrderingMethodName =
        "_UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE";

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _targetHandle;
    private int _lastLoggedTarget = -1;
    private int _promptHandle = -1;
    private int _promptGroup = -1;
    private int _iloRegisteredTarget = -1;

    private bool _iloRegistrationAttemptedForTarget;
    private bool _completedForTarget;
    private bool _setupFailedForTarget;
    private bool _completionWarningLogged;
    private bool _errorLogged;

    private MethodInfo _promptHasStandardModeCompleted;
    private MethodInfo _promptSetOrderingAsInputType;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Native lock-on G Recruit prompt ordering test initialized ===");
        Log("TEST ONLY: create a temporary G Recruit action prompt for the focused human NPC.");
        Log("INPUT ACTION: INPUT_INTERACT_OPTION1 (0x760A9C6F; InteractionLockOn context; reported default G).");
        Log("PROMPT TEXT: direct VAR_STRING wrapper call using an explicit InputArgument array.");
        Log("PROMPT ORDERING TEST: call _UI_PROMPT_SET_ORDERING_AS_INPUT_TYPE during prompt registration.");
        Log("PROMPT GROUP: assign the native prompt to the focused entity's target prompt group after registration ends.");
        Log("ILO TEST: register an ILO for the already-focused human target; log its Boolean result.");
        Log("ILO APPROACH PROMPT: disabled for this test; no separate approach text is intended.");
        Log("No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log("Unarmed test conditions: stand close to a human NPC, right-mouse focus them, and check for G Recruit.");
        Log("Native setup success does not prove that the prompt rendered.");

        ResolvePromptOrderingMethod();
        ResolveOptionalCompletionMethod();

        RDR2.UI.Screen.PrintSubtitle(
            "Unarmed: focus a human NPC to test G Recruit.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdatePrompt();
        }
        catch (Exception exception)
        {
            if (!_errorLogged)
            {
                _errorLogged = true;
                Log("ERROR: " + GetExceptionMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt test error. Check Test_Log.txt.");
            }

            _setupFailedForTarget = true;
            ClearPrompt();
        }
    }

    private void UpdatePrompt()
    {
        int focusedTarget = 0;

        if (Game.Player.IsFreeFocusing)
        {
            TryGetFocusedHuman(out focusedTarget);
        }

        if (focusedTarget != _targetHandle)
        {
            ClearPrompt();

            _targetHandle = focusedTarget;
            _completedForTarget = false;
            _setupFailedForTarget = false;
            _completionWarningLogged = false;
            _iloRegistrationAttemptedForTarget = false;

            if (_lastLoggedTarget != focusedTarget)
            {
                Log(
                    focusedTarget > 0
                        ? "FOCUSED HUMAN TARGET: " + focusedTarget
                        : "FOCUSED HUMAN TARGET: none");

                _lastLoggedTarget = focusedTarget;
            }
        }

        if (_targetHandle <= 0 ||
            _completedForTarget ||
            _setupFailedForTarget)
        {
            return;
        }

        if (_promptHandle <= 0)
        {
            CreateTargetPrompt(_targetHandle);
        }

        if (_promptHandle > 0)
        {
            CheckPromptCompletion();
        }
    }

    private bool TryGetFocusedHuman(out int targetHandle)
    {
        targetHandle = 0;

        if (!Game.Player.IsFreeFocusing)
        {
            return false;
        }

        int foundHandle = 0;
        bool found;

        unsafe
        {
            found = RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                Game.Player.Handle,
                &foundHandle);
        }

        if (!found ||
            foundHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(foundHandle) ||
            foundHandle == Game.Player.Character.Handle ||
            !RDR2.Native.PED.IS_PED_HUMAN(foundHandle))
        {
            return false;
        }

        targetHandle = foundHandle;
        return true;
    }

    private void CreateTargetPrompt(int targetHandle)
    {
        try
        {
            if (_promptSetOrderingAsInputType == null)
            {
                throw new InvalidOperationException(
                    "The installed HUD wrapper did not expose a compatible " +
                    PromptOrderingMethodName +
                    " method. No prompt-visibility result can be tested.");
            }

            TryRegisterIlo(targetHandle);

            _promptGroup =
                RDR2.Native.HUD._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                    targetHandle);

            if (_promptGroup <= 0)
            {
                throw new InvalidOperationException(
                    "Target returned an invalid prompt group: " +
                    _promptGroup);
            }

            _promptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_promptHandle <= 0)
            {
                throw new InvalidOperationException(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid prompt handle.");
            }

            Log(
                "PROMPT REGISTER BEGIN: handle=" +
                _promptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _promptHandle,
                RecruitInputActionHash);

            RDR2.Native.InputArgument[] textArguments =
            {
                "LITERAL_STRING",
                "Recruit"
            };

            string promptText =
                RDR2.Native.MISC.VAR_STRING(
                    PromptTextLiteralString,
                    textArguments);

            if (promptText == null)
            {
                throw new InvalidOperationException(
                    "VAR_STRING returned null.");
            }

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _promptHandle,
                promptText);

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _promptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _promptHandle,
                true);

            // External prompt research says this ordering control must be set
            // while the prompt is being registered or immediately afterward.
            SetPromptOrderingAsInputType(_promptHandle);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            Log(
                "PROMPT REGISTER END: target=" + targetHandle +
                "; handle=" + _promptHandle);

            // Keep the previous test's group-assignment order unchanged so
            // this run changes only the ordering setting.
            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                _promptGroup,
                0);

            Log(
                "PROMPT GROUP ASSIGNED: target=" + targetHandle +
                "; handle=" + _promptHandle +
                "; targetGroup=" + _promptGroup);

            Log(
                "PROMPT CONTROL: INPUT_INTERACT_OPTION1 " +
                "(0x760A9C6F; reported default G in InteractionLockOn).");

            Log(
                "PROMPT TEXT: VAR_STRING(10, " +
                "InputArgument[] { LITERAL_STRING, Recruit }).");

            Log(
                "IN-GAME CHECK: record whether G Recruit is visible " +
                "in the focused NPC's lock-on menu. Successful native " +
                "calls alone do not prove rendering.");
        }
        catch (Exception exception)
        {
            _setupFailedForTarget = true;

            Log(
                "PROMPT SETUP FAILED: " +
                GetExceptionMessage(exception));

            ClearPrompt();
        }
    }

    private void SetPromptOrderingAsInputType(int promptHandle)
    {
        ParameterInfo[] parameters =
            _promptSetOrderingAsInputType.GetParameters();

        object inputTypeArgument =
            Convert.ChangeType(
                RecruitInputActionHash,
                parameters[1].ParameterType);

        object nativeResult =
            _promptSetOrderingAsInputType.Invoke(
                null,
                new object[]
                {
                    promptHandle,
                    inputTypeArgument
                });

        string returnText =
            nativeResult == null
                ? "void"
                : nativeResult.ToString();

        Log(
            "PROMPT ORDERING SET: method=" +
            GetMethodSignature(_promptSetOrderingAsInputType) +
            "; input=INPUT_INTERACT_OPTION1 (0x760A9C6F)" +
            "; nativeReturn=" + returnText);
    }

    private void TryRegisterIlo(int targetHandle)
    {
        if (_iloRegistrationAttemptedForTarget)
        {
            return;
        }

        _iloRegistrationAttemptedForTarget = true;

        try
        {
            Log(
                "ILO REGISTER BEGIN: target=" +
                targetHandle);

            bool registered =
                RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                    targetHandle,
                    "Recruit",
                    IloRadius,
                    IloFocusDistance,
                    IloFlags,
                    IloUnknownFloat5,
                    IloUnknownFloat6,
                    IloPromptHandle,
                    IloShowApproachPrompt,
                    IloUnknownInt9);

            Log(
                "ILO REGISTER RESULT: target=" +
                targetHandle +
                "; returned=" +
                registered);

            if (registered)
            {
                _iloRegisteredTarget = targetHandle;
            }
        }
        catch (Exception exception)
        {
            Log(
                "ILO REGISTER FAILED: target=" +
                targetHandle +
                "; " +
                GetExceptionMessage(exception));
        }
    }

    private void ResolvePromptOrderingMethod()
    {
        try
        {
            MethodInfo[] methods =
                typeof(RDR2.Native.HUD).GetMethods(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static);

            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];

                if (!string.Equals(
                    method.Name,
                    PromptOrderingMethodName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters =
                    method.GetParameters();

                if (parameters.Length != 2 ||
                    parameters[0].ParameterType != typeof(int) ||
                    !IsSupportedInputTypeParameter(
                        parameters[1].ParameterType))
                {
                    continue;
                }

                _promptSetOrderingAsInputType = method;
                break;
            }
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT ORDERING LOOKUP FAILED: " +
                GetExceptionMessage(exception));
        }

        if (_promptSetOrderingAsInputType == null)
        {
            Log(
                "NOTE: Compatible " +
                PromptOrderingMethodName +
                " wrapper was not resolved. The prompt test will stop " +
                "before prompt registration for a focused target.");
        }
        else
        {
            Log(
                "PROMPT ORDERING WRAPPER RESOLVED: " +
                GetMethodSignature(_promptSetOrderingAsInputType));
        }
    }

    private static bool IsSupportedInputTypeParameter(Type parameterType)
    {
        return parameterType == typeof(int) ||
               parameterType == typeof(uint) ||
               parameterType == typeof(long) ||
               parameterType == typeof(ulong);
    }

    private static string GetMethodSignature(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        string[] parameterNames = new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterNames[i] =
                parameters[i].ParameterType.Name;
        }

        return method.DeclaringType.FullName +
               "." +
               method.Name +
               "(" +
               string.Join(", ", parameterNames) +
               ")";
    }

    private void ResolveOptionalCompletionMethod()
    {
        try
        {
            MethodInfo[] methods =
                typeof(RDR2.Native.HUD).GetMethods(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static);

            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];

                if (!string.Equals(
                    method.Name,
                    "_UI_PROMPT_HAS_STANDARD_MODE_COMPLETED",
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters =
                    method.GetParameters();

                if (parameters.Length == 1 &&
                    parameters[0].ParameterType == typeof(int))
                {
                    _promptHasStandardModeCompleted = method;
                    break;
                }
            }
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT COMPLETION LOOKUP FAILED: " +
                GetExceptionMessage(exception));
        }

        if (_promptHasStandardModeCompleted == null)
        {
            Log(
                "NOTE: Standard-mode completion wrapper was not resolved. " +
                "Completion detection will be skipped; the prompt will " +
                "remain active until focus changes.");
        }
        else
        {
            Log(
                "PROMPT COMPLETION CHECK: optional wrapper resolved.");
        }
    }

    private void CheckPromptCompletion()
    {
        if (_promptHasStandardModeCompleted == null)
        {
            if (!_completionWarningLogged)
            {
                _completionWarningLogged = true;

                Log(
                    "PROMPT COMPLETION CHECK SKIPPED: wrapper unavailable. " +
                    "Prompt remains active until focus changes; no " +
                    "recruitment is performed.");
            }

            return;
        }

        try
        {
            object completedResult =
                _promptHasStandardModeCompleted.Invoke(
                    null,
                    new object[]
                    {
                        _promptHandle
                    });

            bool completed =
                completedResult != null &&
                Convert.ToBoolean(completedResult);

            if (!completed)
            {
                return;
            }

            _completedForTarget = true;

            Log(
                "TEST PROMPT COMPLETED: native standard-mode completion " +
                "reported true; no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "G Recruit test completed; no recruitment performed.");

            ClearPrompt();
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT COMPLETION CHECK FAILED; prompt is being kept " +
                "active: " +
                GetExceptionMessage(exception));

            _promptHasStandardModeCompleted = null;
            _completionWarningLogged = true;
        }
    }

    private void ClearPrompt()
    {
        if (_promptHandle > 0)
        {
            int handleToDelete = _promptHandle;
            _promptHandle = -1;

            try
            {
                RDR2.Native.HUD._UI_PROMPT_DELETE(
                    handleToDelete);

                Log(
                    "PROMPT DELETE: handle=" +
                    handleToDelete);
            }
            catch (Exception exception)
            {
                Log(
                    "PROMPT DELETE FAILED: handle=" +
                    handleToDelete +
                    "; " +
                    GetExceptionMessage(exception));
            }
        }

        _promptGroup = -1;

        if (_iloRegisteredTarget > 0)
        {
            int targetToUnregister = _iloRegisteredTarget;
            _iloRegisteredTarget = -1;

            try
            {
                bool unregistered =
                    RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(
                        targetToUnregister);

                Log(
                    "ILO UNREGISTER RESULT: target=" +
                    targetToUnregister +
                    "; returned=" +
                    unregistered);
            }
            catch (Exception exception)
            {
                Log(
                    "ILO UNREGISTER FAILED: target=" +
                    targetToUnregister +
                    "; " +
                    GetExceptionMessage(exception));
            }
        }
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }

    private static string GetExceptionMessage(Exception exception)
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

        return current.GetType().FullName +
               ": " +
               current.Message;
    }
}