<!-- Test lines 251-500 of 738 -->
```csharp
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
        try
        {
            _focusedPromptHandle =
                RDR2.Native.HUD._UI_PROMPT_REGISTER_BEGIN();

            if (_focusedPromptHandle <= 0)
            {
                LogPromptErrorOnce(
                    "UI_PROMPT_REGISTER_BEGIN returned an invalid handle: " +
                    _focusedPromptHandle);

                _focusedPromptHandle = 0;
                return;
            }

            Log("FOCUSED PROMPT HANDLE: " + _focusedPromptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_CONTROL_ACTION(
                _focusedPromptHandle,
                InputInteractOption1);

            Log("PROMPT CONTROL ACTION SET: INPUT_INTERACT_OPTION1; hash=0x" +
                InputInteractOption1.ToString("X8"));

            IntPtr textResult =
                RDR2.Native.MISC.VAR_STRING<string>(
                    VarStringType,
                    VarStringFormat,
                    PromptText);

            Log("VAR_STRING RESULT: type=" +
                textResult.GetType().FullName +
                "; value=" + textResult);

            RDR2.Native.HUD._UI_PROMPT_SET_TEXT(
                _focusedPromptHandle,
                textResult);

            Log("PROMPT TEXT SET: literal=\"" + PromptText + "\"");

            RDR2.Native.HUD._UI_PROMPT_SET_STANDARD_MODE(
                _focusedPromptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_REGISTER_END(
                _focusedPromptHandle);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                false);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                false);

            Log("FOCUSED PROMPT CREATED: prompt=" +
                _focusedPromptHandle +
                "; text=\"" + PromptText +
                "\"; initialEnabled=false; initialVisible=false");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt creation failed: " +
                GetInnermostMessage(exception));

            DeleteFocusedPrompt();
        }
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

        try
        {
            int groupId = Convert.ToInt32(
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle),
                CultureInfo.InvariantCulture);

            if (groupId == 0)
            {
                LogPromptErrorOnce(
                    "Target prompt-group native returned an invalid value: " +
                    groupId);
                return;
            }

            _focusedPromptGroupId = groupId;

            Log("TARGET PROMPT GROUP: target=" + targetHandle +
                "; group=" + _focusedPromptGroupId);

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _focusedPromptHandle,
                _focusedPromptGroupId,
                0);

            RDR2.Native.HUD._UI_PROMPT_SET_ENABLED(
                _focusedPromptHandle,
                true);

            RDR2.Native.HUD._UI_PROMPT_SET_VISIBLE(
                _focusedPromptHandle,
                true);

            _lastProbeValues.Clear();
            _currentProbeValues.Clear();
            _lastInputHeartbeatUtc = DateTime.MinValue;

            Log("PROMPT ATTACHED TO TARGET GROUP: prompt=" +
                _focusedPromptHandle +
                "; target=" + targetHandle +
                "; group=" + _focusedPromptGroupId);

            Log("FOCUSED PROMPT ENABLED: prompt=" +
                _focusedPromptHandle);

            RDR2.UI.Screen.PrintSubtitle(
                "Recruit prompt attached. Check for G Recruit.");
        }
        catch (Exception exception)
        {
            LogPromptErrorOnce(
                "Prompt attachment failed: " +
                GetInnermostMessage(exception));
            _focusedPromptGroupId = 0;
        }
    }

    private void ActivateAmbientPromptGroupThisFrame()
    {
        if (_trackedTargetHandle <= 0 ||
            _focusedPromptGroupId == 0)
        {
            return;
        }

        try
        {
            RDR2.Native.HUD
                ._UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME(
                    _trackedTargetHandle,
                    AmbientGroupRadius,
                    AmbientGroupParameter2,
                    AmbientGroupTabCount,
                    (uint)_focusedPromptGroupId,
                    PromptGroupName,
                    AmbientGroupFlags);

            if (!_ambientActivationLoggedForTarget)
            {
                _ambientActivationLoggedForTarget = true;

                Log("AMBIENT GROUP ACTIVATION INVOKED: target=" +
                    _trackedTargetHandle +
                    "; radius=" + AmbientGroupRadius +
                    "; p2=" + AmbientGroupParameter2 +
                    "; tabCount=" + AmbientGroupTabCount +
                    "; group=" + _focusedPromptGroupId +
                    "; groupName=\"" + PromptGroupName +
                    "\"; flags=" + AmbientGroupFlags);
            }
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "ambient_group_activation",
                "AMBIENT GROUP ACTIVATION ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private void SampleInputProbes()
    {
        SampleProbe(
            "UI_PROMPT_IS_ACTIVE",
            RDR2.Native.HUD._UI_PROMPT_IS_ACTIVE(
                _focusedPromptHandle),
            false);

        SampleProbe(
            "UI_PROMPT_IS_PRESSED",
            RDR2.Native.HUD._UI_PROMPT_IS_PRESSED(
                _focusedPromptHandle),
            false);
```
