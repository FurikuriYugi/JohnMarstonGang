<!-- Test lines 751-1000 of 1246 -->
```csharp
        out object result)
    {
        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            arguments);

        if (method == null)
        {
            LogErrorOnce(
                "native_missing_" + nativeName,
                "NATIVE WRAPPER NOT FOUND: " + nativeName +
                "; argumentCount=" + arguments.Length);
            result = null;
            return false;
        }

        return TryInvokeMethod(method, arguments, out result);
    }

    private bool TryInvokeMethod(
        MethodInfo method,
        object[] arguments,
        out object result)
    {
        result = null;

        try
        {
            ParameterInfo[] parameters = method.GetParameters();
            bool hasParamArray =
                parameters.Length > 0 &&
                Attribute.IsDefined(
                    parameters[parameters.Length - 1],
                    typeof(ParamArrayAttribute));

            object[] invokeArguments;

            if (hasParamArray)
            {
                int fixedParameterCount = parameters.Length - 1;

                if (arguments.Length < fixedParameterCount)
                {
                    LogErrorOnce(
                        "native_count_" + method.Name,
                        "NATIVE ARGUMENT COUNT MISMATCH: " +
                        FormatMethodSignature(method));
                    return false;
                }

                invokeArguments = new object[parameters.Length];

                for (int i = 0; i < fixedParameterCount; i++)
                {
                    if (!TryConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType,
                        out invokeArguments[i]))
                    {
                        LogArgumentConversionFailure(method, i);
                        return false;
                    }
                }

                Type elementType =
                    parameters[parameters.Length - 1]
                        .ParameterType
                        .GetElementType();

                int extraArgumentCount =
                    arguments.Length - fixedParameterCount;

                Array extraArguments =
                    Array.CreateInstance(elementType, extraArgumentCount);

                for (int i = 0; i < extraArgumentCount; i++)
                {
                    if (!TryConvertArgument(
                        arguments[fixedParameterCount + i],
                        elementType,
                        out object convertedArgument))
                    {
                        LogArgumentConversionFailure(
                            method,
                            fixedParameterCount + i);
                        return false;
                    }

                    extraArguments.SetValue(convertedArgument, i);
                }

                invokeArguments[parameters.Length - 1] = extraArguments;
            }
            else
            {
                if (parameters.Length != arguments.Length)
                {
                    LogErrorOnce(
                        "native_count_" + method.Name,
                        "NATIVE ARGUMENT COUNT MISMATCH: " +
                        FormatMethodSignature(method));
                    return false;
                }

                invokeArguments = new object[arguments.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (!TryConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType,
                        out invokeArguments[i]))
                    {
                        LogArgumentConversionFailure(method, i);
                        return false;
                    }
                }
            }

            result = method.Invoke(null, invokeArguments);
            return true;
        }
        catch (Exception exception)
        {
            LogErrorOnce(
                "native_invoke_" + method.Name,
                "NATIVE INVOKE ERROR: " +
                FormatMethodSignature(method) +
                "; " + GetInnermostMessage(exception));
            return false;
        }
    }

    private void LogArgumentConversionFailure(
        MethodInfo method,
        int parameterIndex)
    {
        ParameterInfo[] parameters = method.GetParameters();

        LogErrorOnce(
            "native_conversion_" + method.Name + "_" + parameterIndex,
            "NATIVE ARGUMENT CONVERSION FAILED: " +
            method.Name +
            "; parameter=" + parameterIndex +
            "; signature=" + FormatMethodSignature(method));
    }

    private MethodInfo FindCompatibleNativeMethod(
        string nativeName,
        object[] arguments)
    {
        Assembly apiAssembly = typeof(Game).Assembly;
        Type[] apiTypes;

        try
        {
            apiTypes = apiAssembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            List<Type> loadedTypes = new List<Type>();

            if (exception.Types != null)
            {
                foreach (Type type in exception.Types)
                {
                    if (type != null)
                    {
                        loadedTypes.Add(type);
                    }
                }
            }

            apiTypes = loadedTypes.ToArray();
        }

        string normalizedWanted = NormalizeNativeName(nativeName);

        foreach (Type type in apiTypes)
        {
            if (type == null)
            {
                continue;
            }

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (method.ContainsGenericParameters ||
                    !string.Equals(
                        NormalizeNativeName(method.Name),
                        normalizedWanted,
                        StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();
                bool hasParamArray =
                    parameters.Length > 0 &&
                    Attribute.IsDefined(
                        parameters[parameters.Length - 1],
                        typeof(ParamArrayAttribute));

                if (!hasParamArray &&
                    parameters.Length != arguments.Length)
                {
                    continue;
                }

                if (hasParamArray &&
                    arguments.Length < parameters.Length - 1)
                {
                    continue;
                }

                bool compatible = true;
                int fixedParameterCount = hasParamArray
                    ? parameters.Length - 1
                    : parameters.Length;

                for (int i = 0; i < fixedParameterCount; i++)
                {
                    if (!CanConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType))
                    {
                        compatible = false;
                        break;
                    }
                }

                if (!compatible)
                {
                    continue;
                }

                if (hasParamArray)
                {
                    Type elementType =
                        parameters[parameters.Length - 1]
                            .ParameterType
                            .GetElementType();

                    for (int i = fixedParameterCount;
                        i < arguments.Length;
```
