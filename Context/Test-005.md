<!-- Test lines 1001-1250 of 1468 -->
```csharp

        _iloTargetHandle = -1;
    }

    private void LogFocusStateWhenChanged(bool isFreeFocusing)
    {
        if (!_hasLoggedFocusState ||
            _lastLoggedFocusState != isFreeFocusing)
        {
            Log(
                "STATE: IsFreeFocusing=" +
                isFreeFocusing);

            _lastLoggedFocusState = isFreeFocusing;
            _hasLoggedFocusState = true;
        }
    }

    private void LogTargetWhenChanged(int targetHandle)
    {
        if (_lastLoggedTargetHandle != targetHandle)
        {
            Log(
                "STATE: eligible unarmed interaction-lock-on " +
                "target=" +
                (targetHandle > 0
                    ? targetHandle.ToString()
                    : "none"));

            _lastLoggedTargetHandle = targetHandle;
        }
    }

    private void LogFirstSuccessfulAmbientActivation(
        int targetHandle,
        int promptGroup)
    {
        if (_ambientActivationLogged)
        {
            return;
        }

        _ambientActivationLogged = true;

        Log(
            "AMBIENT GROUP ACTIVATION INVOKED: method=" +
            DescribeMethod(_setAmbientGroupThisFrameMethod) +
            "; target=" +
            targetHandle +
            "; group=" +
            promptGroup +
            "; radius=" +
            TestPromptRadius +
            "; p2=" +
            AmbientPromptP2 +
            "; tabAmount=" +
            AmbientPromptTabAmount +
            "; flags=" +
            AmbientPromptFlags +
            ".");
    }

    private bool _ambientActivationLogged;

    private void LogOnce(string message)
    {
        if (_ambientActivationLogged)
        {
            return;
        }

        _ambientActivationLogged = true;
        Log(message);
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
    {
        Assembly assembly = typeof(Game).Assembly;
        Type[] types;

        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            types = exception.Types;
        }

        foreach (Type type in types)
        {
            if (type == null ||
                type.Namespace == null ||
                !type.Namespace.StartsWith(
                    "RDR2.Native",
                    StringComparison.Ordinal))
            {
                continue;
            }

            MethodInfo[] methods;

            try
            {
                methods =
                    type.GetMethods(
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Static);
            }
            catch
            {
                continue;
            }

            foreach (MethodInfo method in methods)
            {
                if (string.Equals(
                        method.Name,
                        methodName,
                        StringComparison.Ordinal) &&
                    method.GetParameters().Length ==
                        parameterCount)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static object InvokeNative(
        MethodInfo method,
        params object[] values)
    {
        ParameterInfo[] parameters =
            method.GetParameters();

        if (parameters.Length != values.Length)
        {
            throw new InvalidOperationException(
                "Native argument count mismatch for " +
                DescribeMethod(method) +
                ".");
        }

        object[] converted =
            new object[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            converted[i] =
                ConvertArgument(
                    values[i],
                    parameters[i].ParameterType);
        }

        return method.Invoke(null, converted);
    }

    private static void ConvertArgumentsInPlace(
        object[] arguments,
        ParameterInfo[] parameters)
    {
        if (arguments.Length != parameters.Length)
        {
            throw new InvalidOperationException(
                "Argument count does not match method signature.");
        }

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] =
                ConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType);
        }
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
        if (value == null)
        {
            return targetType.IsValueType
                ? Activator.CreateInstance(targetType)
                : null;
        }

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            targetType = nullableType;
        }

        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(
                targetType,
                value);
        }

        return Convert.ChangeType(
            value,
            targetType);
    }

    private static object GetPromptMember(
        object prompt,
        string memberName)
    {
        Type type = prompt.GetType();

        PropertyInfo property =
            type.GetProperty(
                memberName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (property != null &&
            property.CanRead)
        {
            return property.GetValue(prompt, null);
        }

        FieldInfo field =
            type.GetField(
                memberName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        return field == null
            ? null
            : field.GetValue(prompt);
    }

    private static void SetPromptProperty(
```
