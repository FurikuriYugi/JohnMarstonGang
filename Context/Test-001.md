<!-- Test lines 1-250 of 1255 -->
```csharp
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using RDR2;

public class Test : Script
{
    // INPUT_INTERACT_OPTION1 in the InteractionLockOn context.
    // The control reference maps this action to G by default.
    private const int RecruitInputActionHash = unchecked((int)0x760A9C6F);
    private const int PromptTextLiteralString = 10;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExist;
    private MethodInfo _isPedHuman;
    private MethodInfo _getTargetPromptGroup;
    private MethodInfo _promptRegisterBegin;
    private MethodInfo _promptSetControlAction;
    private MethodInfo _promptSetText;
    private MethodInfo _promptSetStandardMode;
    private MethodInfo _promptSetEnabled;
    private MethodInfo _promptSetVisible;
    private MethodInfo _promptSetGroup;
    private MethodInfo _promptRegisterEnd;
    private MethodInfo _promptHasStandardModeCompleted;
    private MethodInfo _promptDelete;
    private MethodInfo _varString;

    private int _targetHandle;
    private int _lastLoggedTarget = -1;
    private int _promptHandle = -1;
    private int _promptGroup = -1;

    private bool _completedForTarget;
    private bool _setupFailedForTarget;
    private bool _completionWarningLogged;
    private bool _errorLogged;

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Native lock-on G Recruit prompt test initialized ===");
        Log(
            "TEST ONLY: create a temporary G Recruit action prompt " +
            "for the focused human NPC.");
        Log(
            "INPUT ACTION: INPUT_INTERACT_OPTION1 " +
            "(0x760A9C6F; InteractionLockOn context; default G).");
        Log(
            "PROMPT CREATION: native register-begin/set-control/" +
            "set-text/register-end sequence.");
        Log(
            "PROMPT GROUP: assign the native prompt to the focused " +
            "entity's target prompt group.");
        Log(
            "ILO registration is deliberately omitted; this test " +
            "uses the already-focused human target.");
        Log("No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log(
            "Unarmed test conditions: stand close to a human NPC, " +
            "right-mouse focus them, and check for G Recruit.");
        Log(
            "Native wrapper signatures and in-game rendering are " +
            "logged separately; successful calls do not prove display.");

        ResolveMethods();

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

        if (_promptHandle <= 0)
        {
            return;
        }

        CheckPromptCompletion();
    }

    private bool TryGetFocusedHuman(out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        int foundHandle = 0;
        bool found;

        unsafe
        {
            found = _getPlayerTargetEntity(
                Game.Player.Handle,
                &foundHandle);
        }

        if (!found ||
            foundHandle <= 0 ||
            _doesEntityExist == null ||
            _isPedHuman == null)
        {
            return false;
        }

        object existsResult = InvokeNative(
            _doesEntityExist,
            foundHandle);

        bool exists =
            existsResult != null &&
            Convert.ToBoolean(existsResult);

        if (!exists ||
            foundHandle == Game.Player.Character.Handle)
        {
            return false;
        }

        object humanResult = InvokeNative(
            _isPedHuman,
            foundHandle);

        bool isHuman =
            humanResult != null &&
            Convert.ToBoolean(humanResult);

        if (!isHuman)
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
            RequirePromptSetupWrappers();

            object groupResult = InvokeNative(
                _getTargetPromptGroup,
                targetHandle);

            _promptGroup = Convert.ToInt32(groupResult);

            if (_promptGroup <= 0)
            {
                throw new InvalidOperationException(
                    "Target returned an invalid prompt group: " +
                    _promptGroup);
            }

            object beginResult = InvokeNative(
                _promptRegisterBegin);

            _promptHandle = Convert.ToInt32(beginResult);

            if (_promptHandle <= 0)
            {
                throw new InvalidOperationException(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid " +
                    "prompt handle.");
            }

            Log(
                "PROMPT REGISTER BEGIN: handle=" +
                _promptHandle);

            InvokeNative(
                _promptSetControlAction,
                _promptHandle,
                RecruitInputActionHash);

```
