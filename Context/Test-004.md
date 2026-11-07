<!-- Test lines 751-1000 of 1468 -->
```csharp
                "ERROR: Could not create player-target delegate: " +
                GetExceptionMessage(exception));
        }
    }

    private void ResolveTargetValidationMethods()
    {
        _doesEntityExistMethod =
            FindNativeMethod(
                "DOES_ENTITY_EXIST",
                1);

        _isPedHumanMethod =
            FindNativeMethod(
                "IS_PED_HUMAN",
                1);

        if (_doesEntityExistMethod == null)
        {
            Log(
                "ERROR: ENTITY.DOES_ENTITY_EXIST wrapper was not found.");
        }

        if (_isPedHumanMethod == null)
        {
            Log("ERROR: PED.IS_PED_HUMAN wrapper was not found.");
        }
    }

    private void ResolveInteractionLockonPromptMethods()
    {
        _registerInteractionLockonPromptMethod =
            FindNativeMethod(
                "REGISTER_INTERACTION_LOCKON_PROMPT",
                10);

        _unregisterInteractionLockonPromptMethod =
            FindNativeMethod(
                "UNREGISTER_INTERACTION_LOCKON_PROMPT",
                1);

        LogResolvedOrMissing(
            "ILO REGISTER WRAPPER",
            _registerInteractionLockonPromptMethod);

        LogResolvedOrMissing(
            "ILO UNREGISTER WRAPPER",
            _unregisterInteractionLockonPromptMethod);
    }

    private void ResolveAmbientPromptMethods()
    {
        _setAmbientGroupThisFrameMethod =
            FindNativeMethod(
                "_UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                7);

        if (_setAmbientGroupThisFrameMethod == null)
        {
            _setAmbientGroupThisFrameMethod =
                FindNativeMethod(
                    "UI_PROMPT_SET_AMBIENT_GROUP_THIS_FRAME",
                    7);
        }

        _varStringMethod =
            FindNativeMethod(
                "VAR_STRING",
                2);

        LogResolvedOrMissing(
            "AMBIENT GROUP WRAPPER",
            _setAmbientGroupThisFrameMethod);

        LogResolvedOrMissing(
            "VAR_STRING WRAPPER",
            _varStringMethod);

        if (_varStringMethod != null)
        {
            ParameterInfo[] parameters =
                _varStringMethod.GetParameters();

            if (parameters.Length != 2 ||
                !parameters[1].ParameterType.IsArray)
            {
                Log(
                    "ERROR: VAR_STRING wrapper does not match " +
                    "int, InputArgument[]. Ambient activation will " +
                    "not be attempted.");

                _varStringMethod = null;
            }
        }
    }

    private unsafe bool TryGetPlayerTargetEntity(
        out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        int localTargetHandle = 0;

        bool found =
            _getPlayerTargetEntity(
                Game.Player.Handle,
                &localTargetHandle);

        targetHandle = localTargetHandle;

        return found && targetHandle > 0;
    }

    private bool IsHumanNpc(int entityHandle)
    {
        if (entityHandle <= 0 ||
            _doesEntityExistMethod == null ||
            _isPedHumanMethod == null)
        {
            return false;
        }

        try
        {
            bool exists =
                Convert.ToBoolean(
                    InvokeNative(
                        _doesEntityExistMethod,
                        entityHandle));

            if (!exists)
            {
                return false;
            }

            if (entityHandle ==
                Game.Player.Character.Handle)
            {
                return false;
            }

            return Convert.ToBoolean(
                InvokeNative(
                    _isPedHumanMethod,
                    entityHandle));
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: Human-target validation failed for handle " +
                entityHandle +
                ": " +
                GetExceptionMessage(exception));

            return false;
        }
    }

    private void ClearCurrentTargetPrompts()
    {
        ClearRecruitActionPrompt();
        UnregisterInteractionLockonPrompt();

        _iloRegistrationAttempted = false;
        _registrationAttemptedForTarget = false;
        _selectionHandledForTarget = false;
        _lastPromptState = null;
    }

    private void ClearRecruitActionPrompt()
    {
        if (_recruitPrompt != null)
        {
            TryDisposePrompt(_recruitPrompt);
        }

        _recruitPrompt = null;
        _recruitPromptHandle = -1;
        _recruitPromptGroup = -1;
        _promptAttached = false;
    }

    private void TryDisposePrompt(object prompt)
    {
        try
        {
            if (prompt is IDisposable)
            {
                ((IDisposable)prompt).Dispose();
                return;
            }

            MethodInfo disposeMethod =
                prompt.GetType().GetMethod(
                    "Dispose",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance,
                    null,
                    Type.EmptyTypes,
                    null);

            if (disposeMethod != null)
            {
                disposeMethod.Invoke(prompt, null);
            }
        }
        catch (Exception exception)
        {
            Log(
                "WARNING: Prompt cleanup failed: " +
                GetExceptionMessage(exception));
        }
    }

    private void UnregisterInteractionLockonPrompt()
    {
        if (_iloTargetHandle <= 0 ||
            _unregisterInteractionLockonPromptMethod == null)
        {
            _iloTargetHandle = -1;
            return;
        }

        try
        {
            object result =
                InvokeNative(
                    _unregisterInteractionLockonPromptMethod,
                    _iloTargetHandle);

            Log(
                "ILO PROMPT UNREGISTERED for target " +
                _iloTargetHandle +
                "; result=" +
                (result == null ? "null" : result.ToString()));
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: ILO unregistration failed for target " +
                _iloTargetHandle +
                ": " +
                GetExceptionMessage(exception));
        }
```
