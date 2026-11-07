<!-- Test lines 251-500 of 1468 -->
```csharp

            Log(
                "ERROR: ILO registration failed for target " +
                targetHandle +
                ": " +
                GetExceptionMessage(exception));
        }
    }

    private void CreateAndAttachRecruitPrompt(int targetHandle)
    {
        Log(
            "REGISTERING TEMPORARY G RECRUIT ACTION for target " +
            targetHandle);

        try
        {
            _recruitPrompt = CreatePromptObject();

            if (_recruitPrompt == null)
            {
                throw new InvalidOperationException(
                    "Prompt.CreatePrompt returned null.");
            }

            _recruitPromptHandle =
                TryGetNativePromptHandle(_recruitPrompt);

            if (_recruitPromptHandle < 0)
            {
                throw new InvalidOperationException(
                    "Could not resolve the native prompt handle.");
            }

            _recruitPromptGroup =
                RDR2.Native.HUD
                    ._UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY(
                        targetHandle);

            if (_recruitPromptGroup < 0)
            {
                throw new InvalidOperationException(
                    "The target entity returned an invalid prompt group.");
            }

            SetPromptProperty(
                _recruitPrompt,
                "Text",
                "Recruit");

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _recruitPromptHandle,
                _recruitPromptGroup,
                0);

            _promptAttached = true;

            SetPromptProperty(
                _recruitPrompt,
                "Enabled",
                true);

            SetPromptProperty(
                _recruitPrompt,
                "Visible",
                true);

            Log(
                "CUSTOM ACTION ATTACHED: prompt handle " +
                _recruitPromptHandle +
                "; target " +
                targetHandle +
                "; target prompt group " +
                _recruitPromptGroup);

            Log(
                "AMBIENT GROUP TEST: will invoke the attached target " +
                "group every tick; radius=" +
                TestPromptRadius +
                "; p2=" +
                AmbientPromptP2 +
                "; tabAmount=" +
                AmbientPromptTabAmount +
                "; flags=" +
                AmbientPromptFlags +
                ".");

            Log(
                "IN-GAME CHECK REQUIRED: separately confirm whether " +
                "'Recruit test' and 'G Recruit' are visible. Prompt " +
                "properties do not prove rendering.");
        }
        catch (Exception exception)
        {
            Log(
                "ERROR: Recruit action setup failed for target " +
                targetHandle +
                ": " +
                GetExceptionMessage(exception));

            RDR2.UI.Screen.PrintSubtitle(
                "Recruit test setup failed. Check Test_Log.txt.");

            ClearRecruitActionPrompt();
            UnregisterInteractionLockonPrompt();
        }
    }

    private object CreatePromptObject()
    {
        Assembly assembly = typeof(Game).Assembly;

        Type promptType =
            assembly.GetType("RDR2.UI.Prompt", false);

        if (promptType == null)
        {
            throw new InvalidOperationException(
                "RDR2.UI.Prompt type was not found in the loaded API.");
        }

        MethodInfo createPromptMethod = null;

        MethodInfo[] methods =
            promptType.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            if (!string.Equals(
                method.Name,
                "CreatePrompt",
                StringComparison.Ordinal))
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length == 8 &&
                parameters[2].ParameterType == typeof(string))
            {
                createPromptMethod = method;
                break;
            }
        }

        if (createPromptMethod == null)
        {
            throw new InvalidOperationException(
                "The eight-parameter Prompt.CreatePrompt method " +
                "was not found.");
        }

        ParameterInfo[] promptParameters =
            createPromptMethod.GetParameters();

        object[] arguments =
            BuildPromptCreateArguments(promptParameters);

        return createPromptMethod.Invoke(null, arguments);
    }

    private static object[] BuildPromptCreateArguments(
        ParameterInfo[] parameters)
    {
        object[] arguments = new object[parameters.Length];

        arguments[0] =
            ConvertArgument(
                RecruitInputHash,
                parameters[0].ParameterType);

        if (!parameters[1].ParameterType.IsEnum)
        {
            throw new InvalidOperationException(
                "The second CreatePrompt parameter is not an enum; " +
                "expected the Press use-context value.");
        }

        arguments[1] =
            Enum.Parse(
                parameters[1].ParameterType,
                "Press",
                true);

        arguments[2] = "Recruit";

        for (int i = 3; i <= 6; i++)
        {
            arguments[i] =
                ConvertArgument(
                    0,
                    parameters[i].ParameterType);
        }

        Type timingType = parameters[7].ParameterType;

        arguments[7] =
            timingType.IsValueType
                ? Activator.CreateInstance(timingType)
                : null;

        return arguments;
    }

    private void ActivateAmbientPromptGroupThisFrame(
        int targetHandle,
        int promptGroup)
    {
        if (_setAmbientGroupThisFrameMethod == null)
        {
            return;
        }

        if (_varStringMethod == null)
        {
            LogOnce(
                "AMBIENT ACTIVATION SKIPPED: VAR_STRING wrapper is " +
                "unavailable.");
            return;
        }

        try
        {
            ParameterInfo[] parameters =
                _setAmbientGroupThisFrameMethod.GetParameters();

            if (parameters.Length != 7)
            {
                throw new InvalidOperationException(
                    "Expected seven ambient-group parameters; found " +
                    parameters.Length +
                    ".");
            }

            string groupLabel = CreateVarString("Recruit");

            object[] arguments =
            {
                targetHandle,
                TestPromptRadius,
                AmbientPromptP2,
                AmbientPromptTabAmount,
                promptGroup,
                groupLabel,
                AmbientPromptFlags
            };
```
