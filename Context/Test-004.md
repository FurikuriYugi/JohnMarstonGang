<!-- Test lines 751-1000 of 1124 -->
```csharp
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

            if (parameters.Length != arguments.Length)
            {
                LogNativeErrorOnce(
                    method.Name,
                    "NATIVE ARGUMENT COUNT MISMATCH: " +
                    FormatMethodSignature(method));
                return false;
            }

            object[] convertedArguments =
                new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!TryConvertArgument(
                    arguments[i],
                    parameters[i].ParameterType,
                    out convertedArguments[i]))
                {
                    LogNativeErrorOnce(
                        method.Name + "_parameter_" + i,
                        "NATIVE ARGUMENT CONVERSION FAILED: " +
                        method.Name +
                        "; parameter=" + i +
                        "; type=" +
                        parameters[i].ParameterType.FullName);
                    return false;
                }
            }

            result = method.Invoke(null, convertedArguments);
            return true;
        }
        catch (Exception exception)
        {
            LogNativeErrorOnce(
                method.Name,
                "NATIVE INVOKE ERROR: " +
                FormatMethodSignature(method) +
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
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
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
                return true;
            }

            if (IsNumericType(effectiveType) &&
                IsNumericType(value.GetType()))
            {
                converted = Convert.ChangeType(value, effectiveType);
                return true;
            }
        }
        catch
        {
            return false;
        }

        return false;
```
