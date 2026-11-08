<!-- Test lines 251-500 of 1130 -->
```csharp
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void SampleFocusAndPrompt()
    {
        if (_trackedTargetHandle <= 0)
        {
            DetachFocusedPrompt();
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" + _trackedTargetHandle);
            ClearTrackedTarget();
            return;
        }

        int focusedTargetHandle = GetCurrentHumanTargetHandle();
        bool targetIsFocused =
            focusedTargetHandle == _trackedTargetHandle;

        if (targetIsFocused != _targetWasFocused)
        {
            _targetWasFocused = targetIsFocused;

            if (targetIsFocused)
            {
                Log("TRACKED TARGET FOCUS BEGIN: target=" +
                    _trackedTargetHandle);

                LogGControlAvailability(_trackedTargetHandle);
                AttachFocusedPrompt(_trackedTargetHandle);
            }
            else
            {
                Log("TRACKED TARGET FOCUS END: target=" +
                    _trackedTargetHandle);

                DetachFocusedPrompt();
            }
        }

        if (!targetIsFocused ||
            _focusedPromptHandle <= 0)
        {
            return;
        }

        bool promptPressed;

        if (!TryInvokeNative(
            "UI_PROMPT_IS_PRESSED",
            new object[] { _focusedPromptHandle },
            out object pressedResult))
        {
            return;
        }

        try
        {
            promptPressed = Convert.ToBoolean(pressedResult);
        }
        catch
        {
            LogPromptErrorOnce(
                "UI_PROMPT_IS_PRESSED returned an unexpected value: " +
                FormatResult(pressedResult));
            return;
        }

        if (promptPressed && !_promptWasPressed)
        {
            Log("FOCUSED PROMPT PRESSED: target=" +
                _trackedTargetHandle +
                "; prompt=" + _focusedPromptHandle +
                "; control=INPUT_INTERACT_OPTION1" +
                "; hash=0x" +
                InputInteractOption1.ToString("X8") +
                "; note=baseline prompt only; no recruitment action executed");
        }

        _promptWasPressed = promptPressed;
    }

    private int GetCurrentHumanTargetHandle()
    {
        try
        {
            if (Game.Player.IsFreeFocusing)
            {
                int focusedHandle = 0;
                bool foundFocusedTarget;

                unsafe
                {
                    foundFocusedTarget =
                        RDR2.Native.PLAYER.GET_PLAYER_TARGET_ENTITY(
                            Game.Player.Handle,
                            &focusedHandle);
                }

                if (foundFocusedTarget &&
                    IsHumanEntity(focusedHandle))
                {
                    return focusedHandle;
                }
            }

            Entity aimedEntity = Game.Player.GetFreeAimEntity();

            if (aimedEntity != null && aimedEntity.Exists())
            {
                int aimedHandle = aimedEntity.Handle;

                if (IsHumanEntity(aimedHandle))
                {
                    return aimedHandle;
                }
            }
        }
        catch (Exception exception)
        {
            if (!_targetResolutionErrorLogged)
            {
                _targetResolutionErrorLogged = true;
                Log("TARGET RESOLUTION ERROR: " +
                    GetInnermostMessage(exception));
            }
        }

        return 0;
    }

    private void CreateFocusedPrompt()
    {
        if (!TryInvokeNative(
            "UI_PROMPT_REGISTER_BEGIN",
            new object[0],
            out object promptResult))
        {
            LogPromptErrorOnce(
                "Could not find a compatible UI_PROMPT_REGISTER_BEGIN wrapper.");
            return;
        }

        if (!TryConvertToInt(promptResult, out _focusedPromptHandle) ||
            _focusedPromptHandle <= 0)
        {
            LogPromptErrorOnce(
                "UI_PROMPT_REGISTER_BEGIN returned an invalid handle: " +
                FormatResult(promptResult));
            _focusedPromptHandle = 0;
            return;
        }

        Log("FOCUSED PROMPT HANDLE: " + _focusedPromptHandle);

        if (!TryInvokeNative(
            "UI_PROMPT_SET_CONTROL_ACTION",
            new object[]
            {
                _focusedPromptHandle,
                InputInteractOption1
            },
            out object controlResult))
        {
            LogPromptErrorOnce(
                "Could not set the focused prompt control action.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT CONTROL ACTION SET: " +
            "INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8") +
            "; result=" + FormatResult(controlResult));

        if (!TryInvokeNative(
            "UI_PROMPT_SET_TEXT",
            new object[]
            {
                _focusedPromptHandle,
                FocusedPromptTextKey
            },
            out object textResult))
        {
            LogPromptErrorOnce(
                "Could not set prompt text using the registered game string key. " +
                "Check the resolved UI_PROMPT_SET_TEXT wrapper signature.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT TEXT SET: key=\"" + FocusedPromptTextKey +
            "\"; result=" + FormatResult(textResult));

        if (!TryInvokeNative(
            "UI_PROMPT_REGISTER_END",
            new object[] { _focusedPromptHandle },
            out object registerEndResult))
        {
            LogPromptErrorOnce(
                "Could not finish UI_PROMPT_REGISTER_END.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT REGISTER END: result=" +
            FormatResult(registerEndResult));

        TryInvokeNative(
            "UI_PROMPT_SET_ENABLED",
            new object[] { _focusedPromptHandle, false },
            out object enabledResult);

        TryInvokeNative(
            "UI_PROMPT_SET_VISIBLE",
            new object[] { _focusedPromptHandle, false },
            out object visibleResult);

        Log("FOCUSED PROMPT CREATED: prompt=" +
            _focusedPromptHandle +
            "; initialEnabled=false; initialVisible=false");
    }

    private void AttachFocusedPrompt(int targetHandle)
    {
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
                "Could not find a compatible " +
                "UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY wrapper.");
            return;
        }
```
