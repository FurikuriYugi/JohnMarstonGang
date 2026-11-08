<!-- Test lines 1001-1250 of 1292 -->
```csharp
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
                if (method.ContainsGenericParameters ||
                    !string.Equals(
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

                for (int i = 0; i < parameters.Length; i++)
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

    private bool CanConvertArgument(
        object value,
        Type targetType)
    {
        object convertedValue;

        return TryConvertArgument(
            value,
            targetType,
            out convertedValue);
    }

    private bool TryConvertArgument(
        object value,
        Type targetType,
        out object convertedValue)
    {
        convertedValue = null;

        if (targetType == null)
        {
            return false;
        }

        if (targetType.IsByRef)
        {
            targetType = targetType.GetElementType();
        }

        if (value == null)
        {
            return !targetType.IsValueType ||
                Nullable.GetUnderlyingType(targetType) != null;
        }

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            targetType = nullableType;
        }

        try
        {
            if (targetType.IsInstanceOfType(value))
            {
                convertedValue = value;
                return true;
            }

            if (targetType.IsEnum)
            {
                convertedValue = Enum.ToObject(
                    targetType,
                    Convert.ToInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            if (targetType == typeof(IntPtr))
            {
                convertedValue = new IntPtr(
                    Convert.ToInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            if (targetType == typeof(UIntPtr))
            {
                convertedValue = new UIntPtr(
                    Convert.ToUInt64(
                        value,
                        CultureInfo.InvariantCulture));
                return true;
            }

            convertedValue = Convert.ChangeType(
                value,
                targetType,
                CultureInfo.InvariantCulture);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool TryConvertToInt(object value, out int result)
    {
        result = 0;

        if (value == null)
        {
            return false;
        }

        try
        {
            result = Convert.ToInt32(
                value,
                CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string NormalizeNativeName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        return name
            .Replace("_", string.Empty)
            .Replace(" ", string.Empty)
            .ToUpperInvariant();
    }

    private string FormatMethodSignature(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        List<string> parameterNames = new List<string>();

        foreach (ParameterInfo parameter in parameters)
        {
            parameterNames.Add(
                parameter.ParameterType.FullName);
        }

        return method.DeclaringType.FullName +
            "." + method.Name +
            "(" + string.Join(", ", parameterNames.ToArray()) + ")";
    }

    private string FormatResult(object value)
    {
        if (value == null)
        {
            return "null";
        }

        try
        {
            return Convert.ToString(
                value,
                CultureInfo.InvariantCulture);
        }
        catch
        {
            return value.ToString();
        }
    }

    private void LogPromptErrorOnce(string message)
```
