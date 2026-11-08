using System;
using System.IO;
using RDR2;

public class Test : Script
{
    private const int ControlPadIndex = 0;
    private const int StateSampleIntervalMilliseconds = 250;
    private const int FocusLostCaptureMilliseconds = 4000;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private readonly ControlProbe[] _controls =
    {
        new ControlProbe("INPUT_AIM", 0xF84FA74Fu, "aim candidate"),
        new ControlProbe("INPUT_INTERACT_LOCKON", 0xF8982F00u, "focus/lock-on"),
        new ControlProbe("INPUT_INTERACT_LOCKON_ROB", 0x9FA5AD07u, "rob candidate"),
        new ControlProbe("INPUT_INTERACT_LOCKON_POS", 0xF63A17F9u, "positive interaction candidate"),
        new ControlProbe("INPUT_INTERACT_LOCKON_NEG", 0x26A18F47u, "negative interaction candidate"),
        new ControlProbe("INPUT_INTERACT_OPTION1", 0x760A9C6Fu, "G in reported InteractionLockOn mapping"),
        new ControlProbe("INPUT_INTERACT_OPTION2", 0x84543902u, "H in reported InteractionLockOn mapping"),
        new ControlProbe("INPUT_EMOTE_GREET", 0x72BAD5AAu, "greet/emote candidate"),
        new ControlProbe("INPUT_EMOTE_TAUNT", 0x470DC190u, "taunt/emote candidate"),
        new ControlProbe("INPUT_EMOTE_ACTION", 0x13C42BB2u, "emote action candidate"),
        new ControlProbe("INPUT_INTERACT_LOCKON_Y", 0x09A92B8Bu, "lock-on action candidate"),
        new ControlProbe("INPUT_INTERACT_LOCKON_A", 0xD10A3A36u, "lock-on action candidate"),
        new ControlProbe("INPUT_CONTEXT_X", 0xE3BF959Bu, "unverified earlier-test candidate")
    };

    private int _lastFocusedTarget;
    private int _sessionTarget;
    private DateTime _captureUntil;
    private DateTime _nextStateSample;
    private string _lastTargetState;
    private bool _generalErrorLogged;

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Read-only NPC interaction sniffer initialized ===");
        Log("Focus an NPC and perform one visible built-in interaction at a time.");
        Log("This tool does not create prompts, register ILOs, suppress controls, or trigger actions.");
        Log("The current-control-context wrapper was unavailable in the prior API scan; this tool does not use it.");
        Log("Use this log together with your notes about the visible option and NPC response.");

        RDR2.UI.Screen.PrintSubtitle(
            "Interaction sniffer ready. Focus an NPC; test one action at a time.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateFocusSession();
            SampleCurrentSession();
        }
        catch (Exception exception)
        {
            if (!_generalErrorLogged)
            {
                _generalErrorLogged = true;
                Log("SNIFFER ERROR: " + GetInnermostMessage(exception));
                RDR2.UI.Screen.PrintSubtitle(
                    "Interaction sniffer error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdateFocusSession()
    {
        int focusedTarget = 0;

        if (Game.Player.IsFreeFocusing)
        {
            TryGetFocusedHuman(out focusedTarget);
        }

        if (focusedTarget == _lastFocusedTarget)
        {
            if (focusedTarget > 0)
            {
                _captureUntil = DateTime.Now.AddMilliseconds(
                    FocusLostCaptureMilliseconds);
            }

            return;
        }

        if (_lastFocusedTarget > 0)
        {
            Log("FOCUS END: target=" + _lastFocusedTarget +
                "; sampling briefly for response changes.");
            _captureUntil = DateTime.Now.AddMilliseconds(
                FocusLostCaptureMilliseconds);
        }

        _lastFocusedTarget = focusedTarget;

        if (focusedTarget > 0)
        {
            _sessionTarget = focusedTarget;
            _captureUntil = DateTime.Now.AddMilliseconds(
                FocusLostCaptureMilliseconds);
            _nextStateSample = DateTime.MinValue;
            _lastTargetState = null;

            Log("FOCUS BEGIN: target=" + focusedTarget);
            LogControlAvailability(focusedTarget);
            CaptureTargetState(focusedTarget, "focus-begin");
        }
        else if (_sessionTarget > 0)
        {
            Log("FOCUSED HUMAN TARGET: none; previous target=" +
                _sessionTarget + "; response sampling continues briefly.");
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

    private void SampleCurrentSession()
    {
        if (_sessionTarget <= 0)
        {
            return;
        }

        DateTime now = DateTime.Now;

        if (now > _captureUntil)
        {
            Log("CAPTURE END: target=" + _sessionTarget);
            _sessionTarget = 0;
            _lastTargetState = null;
            return;
        }

        SampleControlInputs(_sessionTarget);

        if (now >= _nextStateSample)
        {
            CaptureTargetState(_sessionTarget, "periodic");
            _nextStateSample = now.AddMilliseconds(
                StateSampleIntervalMilliseconds);
        }
    }

    private void LogControlAvailability(int targetHandle)
    {
        for (int i = 0; i < _controls.Length; i++)
        {
            ControlProbe control = _controls[i];

            try
            {
                bool enabled = RDR2.Native.PAD.IS_CONTROL_ENABLED(
                    ControlPadIndex,
                    control.Hash);

                Log("CONTROL AVAILABLE: target=" + targetHandle +
                    "; name=" + control.Name +
                    "; hash=0x" + control.Hash.ToString("X8") +
                    "; enabled=" + enabled +
                    "; note=" + control.Note);
            }
            catch (Exception exception)
            {
                Log("CONTROL AVAILABILITY ERROR: name=" + control.Name +
                    "; " + GetInnermostMessage(exception));
            }
        }
    }

    private void SampleControlInputs(int targetHandle)
    {
        for (int i = 0; i < _controls.Length; i++)
        {
            ControlProbe control = _controls[i];

            try
            {
                bool pressed = RDR2.Native.PAD.IS_CONTROL_PRESSED(
                    ControlPadIndex,
                    control.Hash);

                if (!control.HasPressedSample ||
                    control.LastPressed != pressed)
                {
                    control.HasPressedSample = true;
                    control.LastPressed = pressed;

                    Log("CONTROL STATE: target=" + targetHandle +
                        "; name=" + control.Name +
                        "; hash=0x" + control.Hash.ToString("X8") +
                        "; pressed=" + pressed +
                        "; note=" + control.Note);
                }

                if (RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                    ControlPadIndex,
                    control.Hash))
                {
                    Log("CONTROL JUST PRESSED: target=" + targetHandle +
                        "; name=" + control.Name +
                        "; hash=0x" + control.Hash.ToString("X8") +
                        "; note=" + control.Note);

                    CaptureTargetState(
                        targetHandle,
                        "control-" + control.Name);
                }
            }
            catch (Exception exception)
            {
                Log("CONTROL PROBE ERROR: name=" + control.Name +
                    "; " + GetInnermostMessage(exception));
            }
        }
    }

    private void CaptureTargetState(int targetHandle, string reason)
    {
        try
        {
            bool exists =
                RDR2.Native.ENTITY.DOES_ENTITY_EXIST(targetHandle);

            bool human =
                exists &&
                RDR2.Native.PED.IS_PED_HUMAN(targetHandle);

            string state =
                "target=" + targetHandle +
                "; exists=" + exists +
                "; human=" + human +
                "; health=" +
                RDR2.Native.ENTITY.GET_ENTITY_HEALTH(targetHandle) +
                "; inCombatWithAny=" +
                RDR2.Native.PED.IS_PED_IN_COMBAT(targetHandle, 0) +
                "; fleeing=" +
                RDR2.Native.PED.IS_PED_FLEEING(targetHandle) +
                "; shooting=" +
                RDR2.Native.PED.IS_PED_SHOOTING(targetHandle) +
                "; ragdoll=" +
                RDR2.Native.PED.IS_PED_RAGDOLL(targetHandle);

            if (!string.Equals(
                state,
                _lastTargetState,
                StringComparison.Ordinal))
            {
                Log("TARGET STATE CHANGE: reason=" + reason +
                    "; " + state);
                _lastTargetState = state;
            }
        }
        catch (Exception exception)
        {
            Log("TARGET STATE ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
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

    private sealed class ControlProbe
    {
        public readonly string Name;
        public readonly uint Hash;
        public readonly string Note;
        public bool HasPressedSample;
        public bool LastPressed;

        public ControlProbe(string name, uint hash, string note)
        {
            Name = name;
            Hash = hash;
            Note = note;
        }
    }
}