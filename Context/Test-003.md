<!-- Test lines 501-750 of 1255 -->
```csharp
                1);

        _getTargetPromptGroup =
            FindNativeMethod(
                "_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
                1);

        _promptRegisterBegin =
            FindNativeMethod(
                "_UI_PROMPT_REGISTER_BEGIN",
                0);

        _promptSetControlAction =
            FindNativeMethod(
                "_UI_PROMPT_SET_CONTROL_ACTION",
                2);

        _promptSetText =
            FindNativeMethod(
                "_UI_PROMPT_SET_TEXT",
                2);

        _promptSetStandardMode =
            FindNativeMethod(
                "_UI_PROMPT_SET_STANDARD_MODE",
                2);

        _promptSetEnabled =
            FindNativeMethod(
                "_UI_PROMPT_SET_ENABLED",
                2);

        _promptSetVisible =
            FindNativeMethod(
                "_UI_PROMPT_SET_VISIBLE",
                2);

        _promptSetGroup =
            FindNativeMethod(
                "_UI_PROMPT_SET_GROUP",
                3);

        _promptRegisterEnd =
            FindNativeMethod(
                "_UI_PROMPT_REGISTER_END",
                1);

        _promptHasStandardModeCompleted =
            FindNativeMethod(
                "_UI_PROMPT_HAS_STANDARD_MODE_COMPLETED",
                1);

        _promptDelete =
            FindNativeMethod(
                "_UI_PROMPT_DELETE",
                1);

        LogResolvedMethod(
            "DOES ENTITY EXIST",
            _doesEntityExist);

        LogResolvedMethod(
            "IS PED HUMAN",
            _isPedHuman);

        LogResolvedMethod(
            "TARGET PROMPT GROUP",
            _getTargetPromptGroup);

        LogResolvedMethod(
            "PROMPT REGISTER BEGIN",
            _promptRegisterBegin);

        LogResolvedMethod(
            "PROMPT SET CONTROL ACTION",
            _promptSetControlAction);

        LogResolvedMethod(
            "PROMPT SET TEXT",
            _promptSetText);

        LogResolvedMethod(
            "PROMPT SET STANDARD MODE",
            _promptSetStandardMode);

        LogResolvedMethod(
            "PROMPT SET ENABLED",
            _promptSetEnabled);

        LogResolvedMethod(
            "PROMPT SET VISIBLE",
            _promptSetVisible);

        LogResolvedMethod(
            "PROMPT SET GROUP",
            _promptSetGroup);

        LogResolvedMethod(
            "PROMPT REGISTER END",
            _promptRegisterEnd);

        LogResolvedMethod(
            "PROMPT HAS STANDARD MODE COMPLETED",
            _promptHasStandardModeCompleted);

        LogResolvedMethod(
            "PROMPT DELETE",
            _promptDelete);

        ResolveVarString();

        if (_promptHasStandardModeCompleted == null)
        {
            Log(
                "NOTE: Standard-mode completion wrapper is not " +
                "required to create the prompt. Completion detection " +
                "will be skipped if it remains unavailable.");
        }
    }

    private void ResolveVarString()
    {
        Type expectedReturnType = typeof(string);

        if (_promptSetText != null)
        {
            ParameterInfo[] textParameters =
                _promptSetText.GetParameters();

            if (textParameters.Length >= 2)
            {
                expectedReturnType =
                    textParameters[1].ParameterType;
            }
        }

        object[] sampleArguments =
        {
            PromptTextLiteralString,
            "LITERAL_STRING",
            "Recruit"
        };

        MethodInfo[] candidates =
            FindNativeMethods("VAR_STRING");

        MethodInfo bestMethod = null;
        int bestScore = int.MaxValue;

        for (int i = 0; i < candidates.Length; i++)
        {
            MethodInfo candidate = candidates[i];

            MethodInfo closedCandidate =
                CloseGenericMethod(
                    candidate,
                    expectedReturnType);

            if (closedCandidate == null)
            {
                continue;
            }

            object[] convertedArguments;

            if (!TryBuildArguments(
                closedCandidate.GetParameters(),
                sampleArguments,
                out convertedArguments))
            {
                continue;
            }

            int score = ScoreVarStringCandidate(
                closedCandidate,
                expectedReturnType);

            if (score < bestScore)
            {
                bestScore = score;
                bestMethod = closedCandidate;
            }
        }

        _varString = bestMethod;

        if (_varString != null)
        {
            Log(
                "VAR STRING: " +
                DescribeMethod(_varString));

            return;
        }

        Log(
            "ERROR: No compatible VAR_STRING wrapper was resolved. " +
            "Matching-name candidate count=" +
            candidates.Length + ".");

        for (int i = 0; i < candidates.Length; i++)
        {
            Log(
                "VAR STRING CANDIDATE: " +
                DescribeMethod(candidates[i]));
        }
    }

    private static int ScoreVarStringCandidate(
        MethodInfo method,
        Type expectedReturnType)
    {
        int score = 0;

        if (method.ReturnType != expectedReturnType)
        {
            score += 10;
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        if (parameters.Length == 3)
        {
            score -= 2;
        }

        if (parameters.Length > 0 &&
            IsParamArray(parameters[parameters.Length - 1]))
        {
            score += 1;
        }

        return score;
    }

    private void LogResolvedMethod(
        string label,
        MethodInfo method)
    {
        Log(
            method == null
                ? "ERROR: " + label + " wrapper not found."
                : label + ": " + DescribeMethod(method));
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
    {
```
