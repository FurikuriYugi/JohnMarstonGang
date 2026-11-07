<!-- Test lines 751-1000 of 1255 -->
```csharp
        MethodInfo[] methods =
            FindNativeMethods(methodName);

        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];

            if (method.ContainsGenericParameters)
            {
                continue;
            }

            if (method.GetParameters().Length == parameterCount)
            {
                return method;
            }
        }

        return null;
    }

    private static MethodInfo[] FindNativeMethods(
        string methodName)
    {
        Type[] types;

        try
        {
            types = typeof(Game).Assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            types = exception.Types;
        }

        if (types == null)
        {
            return new MethodInfo[0];
        }

        string normalizedName =
            methodName.TrimStart('_');

        System.Collections.Generic.List<MethodInfo> matches =
            new System.Collections.Generic.List<MethodInfo>();

        for (int i = 0; i < types.Length; i++)
        {
            Type type = types[i];

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
                methods = type.GetMethods(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static);
            }
            catch
            {
                continue;
            }

            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo method = methods[j];

                if (string.Equals(
                    method.Name.TrimStart('_'),
                    normalizedName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    matches.Add(method);
                }
            }
        }

        return matches.ToArray();
    }

    private static MethodInfo CloseGenericMethod(
        MethodInfo method,
        Type expectedReturnType)
    {
        if (method == null)
        {
            return null;
        }

        if (!method.ContainsGenericParameters)
        {
            return method;
        }

        if (!method.IsGenericMethodDefinition)
        {
            return null;
        }

        Type[] genericParameters =
            method.GetGenericArguments();

        if (genericParameters.Length == 0 ||
            expectedReturnType == null)
        {
            return null;
        }

        Type[] genericArguments =
            new Type[genericParameters.Length];

        for (int i = 0; i < genericArguments.Length; i++)
        {
            genericArguments[i] = expectedReturnType;
        }

        try
        {
            return method.MakeGenericMethod(genericArguments);
        }
        catch
        {
            return null;
        }
    }

    private static object InvokeNative(
        MethodInfo method,
        params object[] arguments)
    {
        if (method == null)
        {
            throw new InvalidOperationException(
                "Attempted to invoke an unresolved native wrapper.");
        }

        object[] convertedArguments;

        if (!TryBuildArguments(
            method.GetParameters(),
            arguments,
            out convertedArguments))
        {
            throw new InvalidOperationException(
                "Native argument mismatch for " +
                DescribeMethod(method) +
                "; supplied=" + arguments.Length + ".");
        }

        try
        {
            return method.Invoke(
                null,
                convertedArguments);
        }
        catch (TargetInvocationException exception)
        {
            throw new InvalidOperationException(
                "Native invocation failed for " +
                DescribeMethod(method) +
                ": " +
                GetExceptionMessage(exception));
        }
    }

    private static bool TryBuildArguments(
        ParameterInfo[] parameters,
        object[] suppliedArguments,
        out object[] convertedArguments)
    {
        convertedArguments = null;

        if (parameters == null)
        {
            return false;
        }

        if (suppliedArguments == null)
        {
            suppliedArguments = new object[0];
        }

        bool hasParamArray =
            parameters.Length > 0 &&
            IsParamArray(parameters[parameters.Length - 1]);

        int fixedParameterCount =
            hasParamArray
                ? parameters.Length - 1
                : parameters.Length;

        if (suppliedArguments.Length < fixedParameterCount)
        {
            return false;
        }

        if (!hasParamArray &&
            suppliedArguments.Length > parameters.Length)
        {
            return false;
        }

        convertedArguments =
            new object[parameters.Length];

        for (int i = 0; i < fixedParameterCount; i++)
        {
            try
            {
                convertedArguments[i] = ConvertArgument(
                    suppliedArguments[i],
                    parameters[i].ParameterType);
            }
            catch
            {
                convertedArguments = null;
                return false;
            }
        }

        if (hasParamArray)
        {
            Type arrayType =
                parameters[parameters.Length - 1].ParameterType;

            Type elementType =
                arrayType.GetElementType();

            int remainingCount =
                suppliedArguments.Length - fixedParameterCount;

            Array packedArguments =
                Array.CreateInstance(
                    elementType,
                    remainingCount);

            for (int i = 0; i < remainingCount; i++)
            {
                try
                {
```
