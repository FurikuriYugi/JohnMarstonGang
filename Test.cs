using System;
using System.Globalization;
using System.IO;
using RDR2;

public class Test : Script
{
    private const string PromptText = "Recruit";

    // Fixed diagnostic values. Do not press G during this test.
    private const float IloRadius = 3.0f;
    private const float IloFocusDistance = 3.0f;
    private const int IloFlag = 0;
    private const float IloP5 = 0.0f;
    private const float IloP6 = 0.0f;
    private const bool ShowIloApproachPrompt = true;
    private const int IloP9 = -1;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private int _targetHandle;
    private bool _iloRegistrationAttempted;
    private bool _iloRegistered;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== G Recruit ILO-only display test ===");
        Log("Prompt label: " + PromptText);
        Log("No standard HUD prompt is created.");
        Log("ILO prompt argument: 0");
        Log(
            "ILO settings: radius=" + IloRadius +
            "; focusDistance=" + IloFocusDistance +
            "; flag=" + IloFlag +
            "; p5=" + IloP5 +
            "; p6=" + IloP6 +
            "; showApproachPrompt=" + ShowIloApproachPrompt +
            "; p9=" + IloP9);
        Log("Display test only. Do not press G; no recruitment is performed.");

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void OnTick(object sender, EventArgs e)
    {
        bool targetFound;
        bool targetIsHuman;
        int targetHandle;

        bool lookupSucceeded = TryGetFocusedHuman(
            out targetFound,
            out targetHandle,
            out targetIsHuman);

        if (!lookupSucceeded ||
            !targetFound ||
            targetHandle <= 0 ||
            !targetIsHuman)
        {
            DeleteCurrentIlo();
            return;
        }

        if (targetHandle != _targetHandle)
        {
            DeleteCurrentIlo();
            RegisterIloForTarget(targetHandle);
        }
    }

    private void RegisterIloForTarget(int targetHandle)
    {
        _targetHandle = targetHandle;
        _iloRegistrationAttempted = true;
        _iloRegistered = false;

        try
        {
            _iloRegistered =
                RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
                    targetHandle,
                    PromptText,
                    IloRadius,
                    IloFocusDistance,
                    IloFlag,
                    IloP5,
                    IloP6,
                    0,
                    ShowIloApproachPrompt,
                    IloP9);

            Log(
                "ILO REGISTER RESULT: target=" +
                targetHandle +
                "; promptArgument=0" +
                "; returned=" +
                _iloRegistered);
        }
        catch (Exception exception)
        {
            Log(
                "ILO REGISTER ERROR: target=" +
                targetHandle +
                "; " +
                InnermostMessage(exception));
        }
    }

    private bool TryGetFocusedHuman(
        out bool targetFound,
        out int targetHandle,
        out bool targetIsHuman)
    {
        targetFound = false;
        targetHandle = 0;
        targetIsHuman = false;

        try
        {
            int focusedTargetHandle = 0;

            unsafe
            {
                targetFound =
                    RDR2.Native.PLAYER.GET_PLAYER_INTERACTION_TARGET_ENTITY(
                        Game.Player.Handle,
                        &focusedTargetHandle,
                        true,
                        true);
            }

            if (!targetFound || focusedTargetHandle <= 0)
            {
                return true;
            }

            targetHandle = focusedTargetHandle;
            targetIsHuman =
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            Log(
                "FOCUSED TARGET: target=" +
                targetHandle +
                "; human=" +
                targetIsHuman);

            return true;
        }
        catch (Exception exception)
        {
            Log(
                "TARGET LOOKUP ERROR: " +
                InnermostMessage(exception));
            return false;
        }
    }

    private void DeleteCurrentIlo()
    {
        if (_targetHandle <= 0)
        {
            _iloRegistrationAttempted = false;
            _iloRegistered = false;
            return;
        }

        int targetHandle = _targetHandle;

        if (_iloRegistrationAttempted)
        {
            try
            {
                bool unregistered =
                    RDR2.Native.MISC.UNREGISTER_INTERACTION_LOCKON_PROMPT(
                        targetHandle);

                Log(
                    "ILO UNREGISTER RESULT: target=" +
                    targetHandle +
                    "; returned=" +
                    unregistered);
            }
            catch (Exception exception)
            {
                Log(
                    "ILO UNREGISTER ERROR: target=" +
                    targetHandle +
                    "; " +
                    InnermostMessage(exception));
            }
        }

        _targetHandle = 0;
        _iloRegistrationAttempted = false;
        _iloRegistered = false;
    }

    private void OnAborted(object sender, EventArgs e)
    {
        DeleteCurrentIlo();
    }

    private string InnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture) +
            " " +
            message +
            Environment.NewLine);
    }
}