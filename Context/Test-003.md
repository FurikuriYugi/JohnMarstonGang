<!-- Test lines 501-750 of 1292 -->
```csharp
            new object[] { _focusedPromptHandle, false },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, false },
            out object visibleResult);

        Log("FOCUSED PROMPT CREATED: prompt=" +
            _focusedPromptHandle +
            "; text=\"" + PromptText +
            "\"; initialEnabled=false; initialVisible=false");
    }

    private void AttachFocusedPrompt(int targetHandle)
    {
        _ambientActivationLoggedForTarget = false;

        if (_focusedPromptHandle <= 0)
        {
            LogPromptErrorOnce(
                "Cannot attach prompt because prompt creation failed.");
            return;
        }

        if (!TryInvokeNative(
            "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
            new object[] { targetHandle },
            out object groupResult))
        {
            LogPromptErrorOnce(
                "Could not find or invoke " +
                "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY.");
            return;
        }

        if (!TryConvertToInt(groupResult, out int groupId) ||
            groupId == 0)
        {
            LogPromptErrorOnce(
                "Target prompt-group native returned an invalid value: " +
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
                "Could not attach the prompt to the target's prompt group.");

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

        _lastProbeValues.Clear();
        _currentProbeValues.Clear();
        _lastInputHeartbeatUtc = DateTime.MinValue;

        Log("FOCUSED PROMPT ENABLED: prompt=" +
            _focusedPromptHandle +
            "; enabledResult=" + FormatResult(enabledResult) +
            "; visibleResult=" + FormatResult(visibleResult));

        RDR2.UI.Screen.PrintSubtitle(
            "Recruit prompt attached. Check for G Recruit.");
    }

    private void ActivateAmbientPromptGroupThisFrame()
    {
        if (_trackedTargetHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        if (!_nativeMethods.ContainsKey(
            "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME"))
        {
            ResolveNative(
                "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                new object[]
                {
                    _trackedTargetHandle,
                    AmbientGroupRadius,
                    AmbientGroupParameter2,
                    AmbientGroupTabCount,
                    _focusedPromptGroupId,
                    0UL,
                    AmbientGroupFlags
                });
        }

        if (!TryInvokeNative(
            "VAR_STRING",
            new object[]
            {
                VarStringType,
                VarStringFormat,
                PromptGroupName
            },
            out object groupNameResult))
        {
            LogPromptErrorOnce(
                "Could not create the ambient prompt-group name.");
            return;
        }

        bool invoked = TryInvokeNative(
            "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
            new object[]
            {
                _trackedTargetHandle,
                AmbientGroupRadius,
                AmbientGroupParameter2,
                AmbientGroupTabCount,
                _focusedPromptGroupId,
                groupNameResult,
                AmbientGroupFlags
            },
            out object activationResult);

        if (!invoked)
        {
            return;
        }

        if (!_ambientActivationLoggedForTarget)
        {
            _ambientActivationLoggedForTarget = true;

            Log("AMBIENT GROUP ACTIVATION INVOKED: target=" +
                _trackedTargetHandle +
                "; radius=" + AmbientGroupRadius +
                "; p2=" + AmbientGroupParameter2 +
                "; tabCount=" + AmbientGroupTabCount +
                "; group=" + _focusedPromptGroupId +
                "; flags=" + AmbientGroupFlags +
                "; result=" + FormatResult(activationResult));
        }
    }

    private void DetachFocusedPrompt()
    {
        _ambientActivationLoggedForTarget = false;

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
                "; wrapperInvoked=" + removed +
                "; result=" + FormatResult(removeResult));

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

```
