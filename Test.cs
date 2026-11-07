using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    // INPUT_INTERACT_OPTION1 in the InteractionLockOn context.
    // The test uses its reported default keyboard mapping: G.
    private const uint RecruitInputActionHash = 0x760A9C6F;
    private const int PromptTextLiteralString = 10;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _targetHandle;
    private int _lastLoggedTarget = -1;
    private int _promptHandle = -1;
    private int _promptGroup = -1;

    private bool _completedForTarget;
    private bool _setupFailedForTarget;
    private bool _completionWarningLogged;
    private bool _errorLogged;

    private MethodInfo _promptHasStandardModeCompleted;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Native lock-on G Recruit prompt test initialized ===");
        Log("TEST ONLY: create a temporary G Recruit action prompt for the focused human NPC.");
        Log("INPUT ACTION: INPUT_INTERACT_OPTION1 (0x760A9C6F; InteractionLockOn context; default G).");
        Log("PROMPT TEXT: direct VAR_STRING wrapper call using an explicit InputArgument array.");
        Log("PROMPT GROUP: assign the native prompt to the focused entity's target prompt group.");
        Log("ILO registration is omitted; this test uses the already-focused human target.");
        Log("No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log("Unarmed test conditions: stand close to a human NPC, right-mouse focus them, and check for G Recruit.");
        Log("Native setup success does not prove that the prompt rendered in game.");

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

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                _promptGroup,
                0);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _promptHandle);

            Log(
                "PROMPT REGISTERED: target=" + targetHandle +
                "; handle=" + _promptHandle +
                "; targetGroup=" + _promptGroup);

            Log(
                "PROMPT CONTROL: INPUT_INTERACT_OPTION1 " +
                "(0x760A9C6F; default G in InteractionLockOn).");

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
                    new object[] { _promptHandle });

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