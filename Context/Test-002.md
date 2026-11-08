<!-- Test lines 251-500 of 1292 -->
```csharp

            float distance = ped.Position.DistanceTo(
                playerPed.Position);

            if (ped.Handle == _trackedTargetHandle &&
                distance <= TargetRetentionDistance)
            {
                retainedPed = ped;
                continue;
            }

            if (distance <= TargetSelectionDistance &&
                distance < nearestDistance)
            {
                nearestPed = ped;
                nearestDistance = distance;
            }
        }

        return retainedPed ?? nearestPed;
    }

    private bool IsHumanEntity(int entityHandle)
    {
        if (entityHandle <= 0 ||
            !RDR2.Native.ENTITY.DOES_ENTITY_EXIST(entityHandle))
        {
            return false;
        }

        return RDR2.Native.PED.IS_PED_HUMAN(entityHandle);
    }

    private void SampleFocusAndInput()
    {
        if (_trackedTargetHandle <= 0)
        {
            return;
        }

        if (!RDR2.Native.ENTITY.DOES_ENTITY_EXIST(_trackedTargetHandle))
        {
            Log("TARGET NO LONGER EXISTS: target=" +
                _trackedTargetHandle);

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

        ActivateAmbientPromptGroupThisFrame();
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
                "Could not find or invoke UI_PROMPT_REGISTER_BEGIN.");
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
                "Could not set INPUT_INTERACT_OPTION1 on the prompt.");

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
                "Could not create literal prompt text with VAR_STRING.");

            DeleteFocusedPrompt();
            return;
        }

        Log("VAR_STRING RESULT: type=" +
            (textResult == null
                ? "null"
                : textResult.GetType().FullName) +
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

        bool standardModeSet = TryInvokeNative(
            "UI_PROMPT_SET_STANDARD_MODE",
            new object[]
            {
                _focusedPromptHandle,
                true
            },
            out object standardModeResult);

        Log("PROMPT STANDARD MODE: wrapperInvoked=" +
            standardModeSet +
            "; result=" + FormatResult(standardModeResult));

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
```
