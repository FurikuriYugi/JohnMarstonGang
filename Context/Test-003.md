<!-- Test lines 501-750 of 1468 -->
```csharp

            ConvertArgumentsInPlace(arguments, parameters);

            _setAmbientGroupThisFrameMethod.Invoke(
                null,
                arguments);

            LogFirstSuccessfulAmbientActivation(
                targetHandle,
                promptGroup);
        }
        catch (Exception exception)
        {
            LogOnce(
                "ERROR: Ambient prompt-group activation failed: " +
                GetExceptionMessage(exception));
        }
    }

    private string CreateVarString(string text)
    {
        if (_varStringMethod == null)
        {
            throw new InvalidOperationException(
                "The VAR_STRING wrapper was not resolved.");
        }

        ParameterInfo[] parameters =
            _varStringMethod.GetParameters();

        if (parameters.Length != 2 ||
            !parameters[1].ParameterType.IsArray)
        {
            throw new InvalidOperationException(
                "Expected VAR_STRING(int, InputArgument[]); found " +
                DescribeMethod(_varStringMethod) +
                ".");
        }

        Type inputArgumentType =
            parameters[1].ParameterType.GetElementType();

        Array inputArguments =
            Array.CreateInstance(inputArgumentType, 2);

        inputArguments.SetValue(
            ConvertToInputArgument(
                "LITERAL_STRING",
                inputArgumentType),
            0);

        inputArguments.SetValue(
            ConvertToInputArgument(
                text,
                inputArgumentType),
            1);

        object[] invokeArguments =
        {
            ConvertArgument(10, parameters[0].ParameterType),
            inputArguments
        };

        object result =
            _varStringMethod.Invoke(
                null,
                invokeArguments);

        if (!(result is string))
        {
            throw new InvalidOperationException(
                "VAR_STRING did not return a string.");
        }

        return (string)result;
    }

    private static object ConvertToInputArgument(
        object value,
        Type inputArgumentType)
    {
        if (inputArgumentType.IsInstanceOfType(value))
        {
            return value;
        }

        MethodInfo[] methods =
            inputArgumentType.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            if (method.Name != "op_Implicit" &&
                method.Name != "op_Explicit")
            {
                continue;
            }

            if (method.ReturnType != inputArgumentType)
            {
                continue;
            }

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length == 1 &&
                parameters[0].ParameterType.IsInstanceOfType(value))
            {
                return method.Invoke(
                    null,
                    new object[] { value });
            }
        }

        throw new InvalidOperationException(
            "Could not convert " +
            value.GetType().FullName +
            " to " +
            inputArgumentType.FullName +
            " using an InputArgument conversion operator.");
    }

    private void CheckPromptSelection()
    {
        if (_recruitPrompt == null ||
            !_promptAttached ||
            _selectionHandledForTarget)
        {
            return;
        }

        if (!GetPromptBooleanProperty(
            _recruitPrompt,
            "HasCompleted"))
        {
            return;
        }

        _selectionHandledForTarget = true;

        Log(
            "G PROMPT COMPLETED — TEST ONLY; target handle " +
            _targetHandle +
            "; no recruitment or gang-data changes made.");

        RDR2.UI.Screen.PrintSubtitle(
            "G prompt completed; no recruitment performed.");

        ClearCurrentTargetPrompts();
    }

    private void LogPromptStateWhenChanged()
    {
        if (_recruitPrompt == null)
        {
            return;
        }

        bool enabled =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "Enabled");

        bool visible =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "Visible");

        bool completed =
            GetPromptBooleanProperty(
                _recruitPrompt,
                "HasCompleted");

        string state =
            "Enabled=" + enabled +
            "; Visible=" + visible +
            "; HasCompleted=" + completed;

        if (!string.Equals(
            state,
            _lastPromptState,
            StringComparison.Ordinal))
        {
            Log(
                "PROMPT STATE: handle=" +
                _recruitPromptHandle +
                "; target=" +
                _targetHandle +
                "; group=" +
                _recruitPromptGroup +
                "; " +
                state);

            _lastPromptState = state;
        }
    }

    private void ResolvePlayerTargetEntityWrapper()
    {
        MethodInfo method =
            FindNativeMethod(
                "GET_PLAYER_TARGET_ENTITY",
                2);

        if (method == null)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY wrapper " +
                "was not found.");
            return;
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        bool signatureMatches =
            method.ReturnType == typeof(bool) &&
            parameters[0].ParameterType == typeof(int) &&
            parameters[1].ParameterType.IsPointer &&
            parameters[1].ParameterType.GetElementType() ==
                typeof(int);

        if (!signatureMatches)
        {
            Log(
                "ERROR: PLAYER.GET_PLAYER_TARGET_ENTITY signature " +
                "did not match bool(int, int*). Found: " +
                DescribeMethod(method));
            return;
        }

        try
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)
                Delegate.CreateDelegate(
                    typeof(PlayerTargetEntityNative),
                    method);

            Log(
                "PLAYER TARGET WRAPPER RESOLVED: " +
                DescribeMethod(method));
        }
        catch (Exception exception)
        {
            _getPlayerTargetEntity = null;

            Log(
```
