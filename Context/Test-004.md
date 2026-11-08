<!-- Test lines 751-1000 of 1130 -->
```csharp
        finally
        {
            _iloRegisteredTargetHandle = 0;
            _iloRegistrationReturnedTrue = false;
        }
    }

    private void OnAborted(object sender, EventArgs e)
    {
        try
        {
            ClearTrackedTarget();
            DeleteFocusedPrompt();
        }
        catch (Exception exception)
        {
            Log("SCRIPT CLEANUP ERROR: " +
                GetInnermostMessage(exception));
        }
    }

    private bool TryInvokeNative(
        string nativeName,
        object[] arguments,
        out object result)
    {
        result = null;

        MethodInfo method = FindCompatibleNativeMethod(
            nativeName,
            arguments);

        if (method == null)
        {
            Log("NATIVE WRAPPER NOT FOUND: " +
                nativeName +
                "; argumentCount=" + arguments.Length);
            return false;
        }

        try
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] convertedArguments = new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!TryConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType,
                    out convertedArguments[i]))
                {
                    Log("NATIVE ARGUMENT CONVERSION FAILED: " +
                        nativeName +
                        "; parameter=" + i +
                        "; type=" + parameters[i].ParameterType.FullName);
                    return false;
                }
            }

            result = method.Invoke(null, convertedArguments);
            return true;
        }
        catch (Exception exception)
        {
            Log("NATIVE INVOKE ERROR: " + nativeName +
                "; " + GetInnermostMessage(exception));
            return false;
        }
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
            System.Collections.Generic.List<Type> loadedTypes =
                new System.Collections.Generic.List<Type>();

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
                if (!string.Equals(
                    NormalizeNativeName(method.Name),
                    normalizedWanted,
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length != arguments.Length)
                {
                    continue;
                }

                bool compatible = true;

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (!CanConvertArgument(
                        arguments[i],
                        parameters[i].ParameterType))
                    {
                        compatible = false;
                        break;
                    }
                }

                if (compatible)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static string NormalizeNativeName(string name)
    {
        return name.Replace("_", string.Empty);
    }

    private static bool CanConvertArgument(
        object value,
        Type targetType)
    {
        if (value == null)
        {
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
        }

        Type effectiveType =
            Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (effectiveType.IsAssignableFrom(value.GetType()))
        {
            return true;
        }

        if (effectiveType.IsEnum)
        {
            return IsNumericType(value.GetType());
        }

        if (IsNumericType(effectiveType) &&
            IsNumericType(value.GetType()))
        {
            return true;
        }

        if (effectiveType == typeof(IntPtr) ||
            effectiveType == typeof(UIntPtr))
        {
            return IsNumericType(value.GetType());
        }

        return effectiveType == typeof(string) &&
            value is string;
    }

    private static bool TryConvertArgument(
        object value,
        Type targetType,
        out object converted)
    {
        converted = null;

        if (value == null)
        {
            if (!targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null)
            {
                return true;
            }

            return false;
        }

        Type effectiveType =
            Nullable.GetUnderlyingType(targetType) ?? targetType;

        try
        {
            if (effectiveType.IsAssignableFrom(value.GetType()))
            {
                converted = value;
                return true;
            }

            if (effectiveType.IsEnum)
            {
                converted = Enum.ToObject(effectiveType, value);
                return true;
            }

            if (effectiveType == typeof(IntPtr))
            {
                converted = new IntPtr(Convert.ToInt64(value));
                return true;
            }

            if (effectiveType == typeof(UIntPtr))
            {
                converted = new UIntPtr(Convert.ToUInt64(value));
                return true;
            }

            if (effectiveType == typeof(string) &&
                value is string)
            {
                converted = value;
```
