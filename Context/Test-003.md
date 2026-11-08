<!-- Test lines 501-750 of 1130 -->
```csharp

        if (!TryConvertToInt(groupResult, out int groupId))
        {
            LogPromptErrorOnce(
                "Target prompt-group native returned an unexpected value: " +
                FormatResult(groupResult));
            return;
        }

        _focusedPromptGroupId = groupId;

        Log("TARGET PROMPT GROUP: target=" + targetHandle +
            "; group=" + _focusedPromptGroupId);

        if (!TryInvokeNative(
            "UI_PROMPT_SET_GROUP",
            new object[]
            {
                _focusedPromptHandle,
                _focusedPromptGroupId,
                0
            },
            out object setGroupResult))
        {
            LogPromptErrorOnce(
                "Could not attach the custom prompt to the target's prompt group.");
            _focusedPromptGroupId = 0;
            return;
        }

        Log("PROMPT ATTACHED TO TARGET GROUP: prompt=" +
            _focusedPromptHandle +
            "; target=" + targetHandle +
            "; group=" + _focusedPromptGroupId +
            "; result=" + FormatResult(setGroupResult));

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, true },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, true },
            out object visibleResult);

        _promptWasPressed = false;

        Log("FOCUSED PROMPT ENABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        RDR2.UI.Screen.PrintSubtitle(
            "G prompt attached to focused NPC.");
    }

    private void DetachFocusedPrompt()
    {
        if (_focusedPromptHandle <= 0)
        {
            _focusedPromptGroupId = 0;
            _promptWasPressed = false;
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

        Log("FOCUSED PROMPT DISABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        _focusedPromptGroupId = 0;
        _promptWasPressed = false;
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

    private void LogGControlAvailability(int targetHandle)
    {
        try
        {
            bool enabled =
                RDR2.Native.PAD.IS_CONTROL_ENABLED(
                    ControlPadIndex,
                    InputInteractOption1);

            Log("G CANDIDATE CONTROL: target=" + targetHandle +
                "; name=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; enabled=" + enabled +
                "; note=reported G mapping");
        }
        catch (Exception exception)
        {
            Log("G CONTROL AVAILABILITY ERROR: " +
                GetInnermostMessage(exception));
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
                    "; registeredBeforeFocus=true" +
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

        DetachFocusedPrompt();
        UnregisterIlo();

        _trackedTargetHandle = 0;
        _targetWasFocused = false;
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
                new object[] { targetHandle });

            Log("ILO UNREGISTER RESULT: target=" + targetHandle +
                "; returned=" + FormatResult(result));
        }
        catch (Exception exception)
        {
            Log("ILO UNREGISTER ERROR: target=" + targetHandle +
                "; " + GetInnermostMessage(exception));
        }
```
