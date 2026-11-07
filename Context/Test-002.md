<!-- Test lines 251-500 of 1255 -->
```csharp
            object textResult = InvokeNative(
                _varString,
                PromptTextLiteralString,
                "LITERAL_STRING",
                "Recruit");

            if (textResult == null)
            {
                throw new InvalidOperationException(
                    "VAR_STRING returned null.");
            }

            InvokeNative(
                _promptSetText,
                _promptHandle,
                textResult);

            InvokeNative(
                _promptSetStandardMode,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetEnabled,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetVisible,
                _promptHandle,
                true);

            InvokeNative(
                _promptSetGroup,
                _promptHandle,
                _promptGroup,
                0);

            InvokeNative(
                _promptRegisterEnd,
                _promptHandle);

            Log(
                "PROMPT REGISTERED: target=" + targetHandle +
                "; handle=" + _promptHandle +
                "; targetGroup=" + _promptGroup);

            Log(
                "PROMPT CONTROL: INPUT_INTERACT_OPTION1 " +
                "(0x760A9C6F; default G in InteractionLockOn).");

            Log(
                "PROMPT TEXT: VAR_STRING(10, LITERAL_STRING, Recruit).");

            if (_promptHasStandardModeCompleted == null)
            {
                Log(
                    "PROMPT COMPLETION CHECK: wrapper unavailable; " +
                    "the prompt will remain until focus changes. " +
                    "Visibility can still be tested in-game.");
            }
            else
            {
                Log(
                    "PROMPT COMPLETION CHECK: standard-mode completion " +
                    "wrapper resolved.");
            }

            Log(
                "IN-GAME CHECK: confirm whether G Recruit is visible " +
                "in the focused NPC's lock-on menu. Native return " +
                "values alone do not prove rendering.");
        }
        catch (Exception exception)
        {
            _setupFailedForTarget = true;

            Log(
                "PROMPT SETUP FAILED: " +
                GetExceptionMessage(exception));

            ClearPrompt();
        }
    }

    private void RequirePromptSetupWrappers()
    {
        if (_getTargetPromptGroup == null)
        {
            throw new InvalidOperationException(
                "Target prompt-group native was not resolved.");
        }

        if (_promptRegisterBegin == null)
        {
            throw new InvalidOperationException(
                "Prompt register-begin native was not resolved.");
        }

        if (_promptSetControlAction == null)
        {
            throw new InvalidOperationException(
                "Prompt set-control-action native was not resolved.");
        }

        if (_varString == null)
        {
            throw new InvalidOperationException(
                "No compatible VAR_STRING wrapper was resolved.");
        }

        if (_promptSetText == null)
        {
            throw new InvalidOperationException(
                "Prompt set-text native was not resolved.");
        }

        if (_promptSetStandardMode == null)
        {
            throw new InvalidOperationException(
                "Prompt set-standard-mode native was not resolved.");
        }

        if (_promptSetEnabled == null)
        {
            throw new InvalidOperationException(
                "Prompt set-enabled native was not resolved.");
        }

        if (_promptSetVisible == null)
        {
            throw new InvalidOperationException(
                "Prompt set-visible native was not resolved.");
        }

        if (_promptSetGroup == null)
        {
            throw new InvalidOperationException(
                "Prompt set-group native was not resolved.");
        }

        if (_promptRegisterEnd == null)
        {
            throw new InvalidOperationException(
                "Prompt register-end native was not resolved.");
        }

        if (_promptDelete == null)
        {
            throw new InvalidOperationException(
                "Prompt delete native was not resolved.");
        }
    }

    private void CheckPromptCompletion()
    {
        if (_promptHasStandardModeCompleted == null)
        {
            if (!_completionWarningLogged)
            {
                _completionWarningLogged = true;

                Log(
                    "PROMPT COMPLETION CHECK SKIPPED: wrapper is " +
                    "unavailable. Prompt remains active until focus " +
                    "changes; no recruitment is performed.");
            }

            return;
        }

        try
        {
            object completedResult = InvokeNative(
                _promptHasStandardModeCompleted,
                _promptHandle);

            bool completed =
                completedResult != null &&
                Convert.ToBoolean(completedResult);

            if (!completed)
            {
                return;
            }

            _completedForTarget = true;

            Log(
                "TEST PROMPT COMPLETED: G action detected by native " +
                "standard-mode completion; no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "G Recruit test completed; no recruitment performed.");

            ClearPrompt();
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT COMPLETION CHECK FAILED; prompt is being kept " +
                "active: " +
                GetExceptionMessage(exception));

            _promptHasStandardModeCompleted = null;
            _completionWarningLogged = true;
        }
    }

    private void ResolveMethods()
    {
        MethodInfo targetMethod =
            FindNativeMethod(
                "GET_PLAYER_TARGET_ENTITY",
                2);

        if (targetMethod != null)
        {
            try
            {
                _getPlayerTargetEntity =
                    (PlayerTargetEntityNative)Delegate.CreateDelegate(
                        typeof(PlayerTargetEntityNative),
                        targetMethod);

                Log(
                    "PLAYER TARGET: " +
                    DescribeMethod(targetMethod));
            }
            catch (Exception exception)
            {
                Log(
                    "ERROR: Could not bind player-target delegate: " +
                    GetExceptionMessage(exception));
            }
        }
        else
        {
            Log(
                "ERROR: GET_PLAYER_TARGET_ENTITY wrapper not found.");
        }

        _doesEntityExist =
            FindNativeMethod(
                "DOES_ENTITY_EXIST",
                1);

        _isPedHuman =
            FindNativeMethod(
                "IS_PED_HUMAN",
```
