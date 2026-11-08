<!-- Test lines 1001-1130 of 1130 -->
```csharp
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

        return effectiveType == typeof(byte) ||
            effectiveType == typeof(sbyte) ||
            effectiveType == typeof(short) ||
            effectiveType == typeof(ushort) ||
            effectiveType == typeof(int) ||
            effectiveType == typeof(uint) ||
            effectiveType == typeof(long) ||
            effectiveType == typeof(ulong) ||
            effectiveType == typeof(float) ||
            effectiveType == typeof(double) ||
            effectiveType == typeof(decimal);
    }

    private static bool TryConvertToInt(
        object value,
        out int converted)
    {
        converted = 0;

        try
        {
            if (value == null)
            {
                return false;
            }

            converted = Convert.ToInt32(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LogResolvedMethod(
        string nativeName,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("NATIVE WRAPPER " + nativeName + ": not found");
            return;
        }

        ParameterInfo[] parameters = method.GetParameters();
        string signature =
            method.DeclaringType.FullName +
            "." + method.Name + "(";

        for (int i = 0; i < parameters.Length; i++)
        {
            if (i > 0)
            {
                signature += ", ";
            }

            signature += parameters[i].ParameterType.FullName;
        }

        signature += ") -> " + method.ReturnType.FullName;

        Log("NATIVE WRAPPER " + nativeName + ": " + signature);
    }

    private void LogPromptErrorOnce(string message)
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT SETUP/TEST ERROR: " + message);
    }

    private static string FormatResult(object result)
    {
        if (result == null)
        {
            return "null";
        }

        return result.GetType().FullName + ":" + result;
    }

    private static string GetInnermostMessage(Exception exception)
    {
        Exception current = exception;

        while (current.InnerException != null)
        {
            current = current.InnerException;
        }

        return current.GetType().FullName + ": " + current.Message;
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message +
            Environment.NewLine);
    }
}
```
