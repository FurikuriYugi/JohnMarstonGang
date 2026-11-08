<!-- Test lines 1001-1246 of 1246 -->
```csharp
                        i++)
                    {
                        if (!CanConvertArgument(
                            arguments[i],
                            elementType))
                        {
                            compatible = false;
                            break;
                        }
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

        return false;
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
    }

    private static bool IsNumericType(Type type)
    {
        Type effectiveType =
            Nullable.GetUnderlyingType(type) ?? type;

        switch (Type.GetTypeCode(effectiveType))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
                return true;

            default:
                return false;
        }
    }

    private static bool TryConvertToInt(
        object value,
        out int converted)
    {
        converted = 0;

        if (value == null)
        {
            return false;
        }

        try
        {
            if (value is IntPtr intPtr)
            {
                converted = intPtr.ToInt32();
                return true;
            }

            if (value is UIntPtr uintPtr)
            {
                converted = unchecked((int)uintPtr.ToUInt32());
                return true;
            }

            converted = Convert.ToInt32(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LogPromptErrorOnce(string message)
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT ERROR: " + message);
    }

    private void LogErrorOnce(string key, string message)
    {
        if (_loggedErrors.Add(key))
        {
            Log(message);
        }
    }

    private void Log(string message)
    {
        string line = "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " + message + Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }

    private static string FormatResult(object result)
    {
        if (result == null)
        {
            return "null";
        }

        return result.GetType().FullName + ":" + result;
    }

    private static string FormatMethodSignature(MethodInfo method)
    {
        List<string> parameterTypes = new List<string>();

        foreach (ParameterInfo parameter in method.GetParameters())
        {
            parameterTypes.Add(parameter.ParameterType.FullName);
        }

        return method.DeclaringType.FullName +
            "." + method.Name +
            "(" + string.Join(", ", parameterTypes.ToArray()) +
            ") : " + method.ReturnType.FullName;
    }

    private static string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.Message;
    }
}
```
