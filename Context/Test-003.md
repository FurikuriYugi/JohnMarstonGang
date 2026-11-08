<!-- Test lines 501-750 of 1246 -->
```csharp
        _currentProbeValues.Clear();
        _lastInputHeartbeatUtc = DateTime.MinValue;

        Log("FOCUSED PROMPT ENABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        RDR2.UI.Screen.PrintSubtitle(
            "G Recruit prompt attached. Tap G once.");
    }

    private void DetachFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
            return;
        }

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, false },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, false },
            out object visibleResult);

        if (_focusedPromptGroupId != 0)
        {
            bool removed = TryInvokeNative(
                "UI_PROMPT_REMOVE_GROUP",
                new object[]
                {
                    _focusedPromptHandle,
                    _focusedPromptGroupId
                },
                out object removeResult);

            Log("PROMPT GROUP DETACH: prompt=" +
                _focusedPromptHandle +
                "; group=" + _focusedPromptGroupId +
                "; wrapperFound=" + removed +
                "; result=" + FormatResult(removeResult));
        }

        if (_focusedPromptGroupId != 0)
        {
            Log("FOCUSED PROMPT DISABLED: prompt=" +
                _focusedPromptHandle +
                "; enabledResult=" + FormatResult(enabledResult) +
                "; visibleResult=" + FormatResult(visibleResult));
        }

        _focusedPromptGroupId = 0;
        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
    }

    private void SampleInputProbes()
    {
        SampleProbe(
            "UI_PROMPT_IS_ACTIVE",
            new object[] { _focusedPromptHandle },
            false);

        SampleProbe(
            "UI_PROMPT_IS_PRESSED",
            new object[] { _focusedPromptHandle },
            false);

        SampleProbe(
            "UI_PROMPT_IS_JUST_PRESSED",
            new object[] { _focusedPromptHandle },
            true);

        SampleProbe(
            "IS_CONTROL_ENABLED",
            new object[] { ControlPadIndex, InputInteractOption1 },
            false);

        SampleProbe(
            "IS_CONTROL_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 },
            false);

        SampleProbe(
            "IS_CONTROL_JUST_PRESSED",
            new object[] { ControlPadIndex, InputInteractOption1 },
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
        object[] arguments,
        bool isPulse)
    {
        if (!_probeMethods.TryGetValue(
            nativeName,
            out MethodInfo method) ||
            method == null)
        {
            return;
        }

        if (!TryInvokeMethod(method, arguments, out object result))
        {
            return;
        }

        bool value;

        try
        {
            value = Convert.ToBoolean(result);
        }
        catch
        {
            LogErrorOnce(
                "probe_" + nativeName,
                "Probe returned an unexpected value: " +
                FormatResult(result));
            return;
        }

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
        if (_currentProbeValues.TryGetValue(
            nativeName,
            out bool value))
        {
            return value.ToString();
        }

        if (!_probeMethods.ContainsKey(nativeName) ||
            _probeMethods[nativeName] == null)
        {
            return "wrapper-unavailable";
        }

        return "not-sampled";
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

        DetachFocusedPrompt();

        bool deleted = TryInvokeNative(
            "UI_PROMPT_DELETE",
            new object[] { _focusedPromptHandle },
            out object deleteResult);

        Log("FOCUSED PROMPT DELETE: prompt=" +
            _focusedPromptHandle +
            "; wrapperFound=" + deleted +
            "; result=" + FormatResult(deleteResult));

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

    private bool TryInvokeNative(
        string nativeName,
        object[] arguments,
```
