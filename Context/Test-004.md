<!-- Test lines 751-1000 of 1292 -->
```csharp
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

    private void ResolveProbeMethodIfNeeded(
        string nativeName,
        object[] arguments)
    {
        if (_probeMethods.ContainsKey(nativeName))
        {
            return;
        }

        MethodInfo method = ResolveNative(nativeName, arguments);
        _probeMethods[nativeName] = method;

        if (method == null)
        {
            Log("PROBE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + arguments.Length);
        }
    }

    private void SampleProbe(
        string nativeName,
        object[] arguments,
        bool isPulse)
    {
        ResolveProbeMethodIfNeeded(nativeName, arguments);

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
            value = Convert.ToBoolean(
                result,
                CultureInfo.InvariantCulture);
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
            "; wrapperInvoked=" + deleted +
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
        out object result)
    {
        MethodInfo method = ResolveNative(nativeName, arguments);

        if (method == null)
        {
            LogErrorOnce(
                "native_missing_" + nativeName,
                "NATIVE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + arguments.Length);

            result = null;
            return false;
        }

        return TryInvokeMethod(method, arguments, out result);
    }

    private bool TryInvokeMethod(
        MethodInfo method,
        object[] arguments,
        out object result)
    {
        result = null;

        try
        {
            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length != arguments.Length)
            {
                LogErrorOnce(
                    "native_count_" + method.Name,
                    "NATIVE ARGUMENT COUNT MISMATCH: " +
                    FormatMethodSignature(method));
                return false;
            }

            object[] invokeArguments =
                new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!TryConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType,
                    out invokeArguments[i]))
                {
                    LogErrorOnce(
                        "native_conversion_" + method.Name + "_" + i,
                        "NATIVE ARGUMENT CONVERSION FAILED: " +
                        method.Name +
                        "; parameter=" + i +
                        "; signature=" +
                        FormatMethodSignature(method));
                    return false;
                }
            }

            result = method.Invoke(null, invokeArguments);
            return true;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "native_invoke_" + method.Name,
                "NATIVE INVOKE ERROR: " +
                FormatMethodSignature(method) +
                "; " + GetInnermostMessage(exception));
            return false;
```
