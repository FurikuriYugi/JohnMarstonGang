<!-- Test lines 251-500 of 1246 -->
```csharp
            if (targetIsFocused)
            {
                Log("TRACKED TARGET FOCUS BEGIN: target=" +
                    _trackedTargetHandle);
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
            _focusedPromptHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        SampleInputProbes();
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

        Log("PROMPT CONTROL ACTION SET: INPUT_INTERACT_OPTION1; hash=0x" +
            InputInteractOption1.ToString("X8") +
            "; result=" + FormatResult(controlResult));

        if (!TryInvokeNative(
            "VAR_STRING",
            new object[]
            {
                VarStringType,
                VarStringFormat,
                PromptText
            },
            out object textResult))
        {
            LogPromptErrorOnce(
                "Could not create the literal prompt text with VAR_STRING.");
            DeleteFocusedPrompt();
            return;
        }

        Log("VAR_STRING RESULT: type=" +
            (textResult == null ? "null" : textResult.GetType().FullName) +
            "; value=" + FormatResult(textResult));

        if (!TryInvokeNative(
            "UI_PROMPT_SET_TEXT",
            new object[]
            {
                _focusedPromptHandle,
                textResult
            },
            out object setTextResult))
        {
            LogPromptErrorOnce(
                "Could not set the prompt text from the VAR_STRING result.");
            DeleteFocusedPrompt();
            return;
        }

        Log("PROMPT TEXT SET: literal=\"" + PromptText +
            "\"; result=" + FormatResult(setTextResult));

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
            "; text=\"" + PromptText +
            "\"; initialEnabled=false; initialVisible=false");
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
```
