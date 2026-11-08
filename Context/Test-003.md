<!-- Test lines 501-738 of 738 -->
```csharp

        SampleProbe(
            "UI_PROMPT_IS_JUST_PRESSED",
            RDR2.Native.HUD._UI_PROMPT_IS_JUST_PRESSED(
                _focusedPromptHandle),
            true);

        SampleProbe(
            "IS_CONTROL_ENABLED",
            RDR2.Native.PAD.IS_CONTROL_ENABLED(
                ControlPadIndex,
                InputInteractOption1),
            false);

        SampleProbe(
            "IS_CONTROL_PRESSED",
            RDR2.Native.PAD.IS_CONTROL_PRESSED(
                ControlPadIndex,
                InputInteractOption1),
            false);

        SampleProbe(
            "IS_CONTROL_JUST_PRESSED",
            RDR2.Native.PAD.IS_CONTROL_JUST_PRESSED(
                ControlPadIndex,
                InputInteractOption1),
            true);

        DateTime now = DateTime.UtcNow;

        if (_lastInputHeartbeatUtc == DateTime.MinValue ||
            (now - _lastInputHeartbeatUtc).TotalMilliseconds >=
            InputHeartbeatIntervalMilliseconds)
        {
            _lastInputHeartbeatUtc = now;

            Log("INPUT HEARTBEAT: promptActive=" +
                GetProbeValue("UI_PROMPT_IS_ACTIVE") +
                "; promptPressed=" +
                GetProbeValue("UI_PROMPT_IS_PRESSED") +
                "; promptJustPressed=" +
                GetProbeValue("UI_PROMPT_IS_JUST_PRESSED") +
                "; controlEnabled=" +
                GetProbeValue("IS_CONTROL_ENABLED") +
                "; controlPressed=" +
                GetProbeValue("IS_CONTROL_PRESSED") +
                "; controlJustPressed=" +
                GetProbeValue("IS_CONTROL_JUST_PRESSED"));
        }
    }

    private void SampleProbe(
        string nativeName,
        bool value,
        bool isPulse)
    {
        _currentProbeValues[nativeName] = value;

        if (isPulse)
        {
            if (value)
            {
                Log("INPUT EVENT: " + nativeName +
                    "=true; target=" + _trackedTargetHandle +
                    "; prompt=" + _focusedPromptHandle);
            }

            return;
        }

        bool previousValue;

        if (!_lastProbeValues.TryGetValue(
            nativeName,
            out previousValue) ||
            previousValue != value)
        {
            Log("INPUT STATE: " + nativeName +
                "=" + value +
                "; target=" + _trackedTargetHandle +
                "; prompt=" + _focusedPromptHandle);

            _lastProbeValues[nativeName] = value;
        }
    }

    private string GetProbeValue(string nativeName)
    {
        bool value;

        if (_currentProbeValues.TryGetValue(nativeName, out value))
        {
            return value.ToString();
        }

        return "not-sampled";
    }

    private void DetachFocusedPrompt()
    {
        _ambientActivationLoggedForTarget = false;

        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
            return;
        }

        try
        {
            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                false);

            if (_focusedPromptGroupId != 0)
            {
                RDR2.Native.HUD._UI_PROMPT_REMOVE_GROUP(
                    _focusedPromptHandle,
                    (ulong)_focusedPromptGroupId);

                Log("PROMPT GROUP DETACH: prompt=" +
                    _focusedPromptHandle +
                    "; group=" + _focusedPromptGroupId);
            }

            Log("FOCUSED PROMPT DISABLED: prompt=" +
                _focusedPromptHandle);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "prompt_detach",
                "PROMPT DETACH ERROR: " +
                GetInnermostMessage(exception));
        }

        _focusedPromptGroupId = 0;
        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
    }

    private void ClearTrackedTarget()
    {
        if (_trackedTargetHandle > 0)
        {
            Log("HUMAN TARGET END: target=" +
                _trackedTargetHandle);
        }

        DetachFocusedPrompt();
        _trackedTargetHandle = 0;
        _targetWasFocused = false;
    }

    private void DeleteFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            return;
        }

        try
        {
            DetachFocusedPrompt();

            RDR2.Native.HUD._UI_PROMPT_DELETE(
                _focusedPromptHandle);

            Log("FOCUSED PROMPT DELETE: prompt=" +
                _focusedPromptHandle);
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "prompt_delete",
                "PROMPT DELETE ERROR: " +
                GetInnermostMessage(exception));
        }

        _focusedPromptHandle = 0;
        _focusedPromptGroupId = 0;
    }

    private void OnAborted(object sender, EventArgs e)
    {
        try
        {
            ClearTrackedTarget();
            DeleteFocusedPrompt();
        }
        catch (Exception exception)
        {
            Log("SCRIPT CLEANUP ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void LogPromptErrorOnce(string message)
    {
        LogErrorOnce("prompt_error", "PROMPT ERROR: " + message);
    }

    private void LogErrorOnce(string key, string message)
    {
        if (_loggedErrors.Add(key))
        {
            Log(message);
        }
    }

    private void Log(string message)
    {
        string line =
            DateTime.Now.ToString(
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture) +
            " " + message +
            Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }

    private string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }
}
```
