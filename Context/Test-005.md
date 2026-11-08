<!-- Test lines 1001-1124 of 1124 -->
```csharp
    }

    private static bool IsNumericType(Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
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
        try
        {
            converted = Convert.ToInt32(value);
            return true;
        }
        catch
        {
            converted = 0;
            return false;
        }
    }

    private static string FormatMethodSignature(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        List<string> parameterTypes = new List<string>();

        foreach (ParameterInfo parameter in parameters)
        {
            parameterTypes.Add(parameter.ParameterType.FullName);
        }

        string declaringType = method.DeclaringType == null
            ? "<unknown type>"
            : method.DeclaringType.FullName;

        return declaringType + "." + method.Name +
            "(" + string.Join(", ", parameterTypes.ToArray()) + ")" +
            " : " + method.ReturnType.FullName;
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

    private void LogPromptErrorOnce(string message)
    {
        if (_promptErrorLogged)
        {
            return;
        }

        _promptErrorLogged = true;
        Log("PROMPT ERROR: " + message);
    }

    private void LogProbeErrorOnce(
        string key,
        string message)
    {
        if (!_probeErrorsLogged.Add(key))
        {
            return;
        }

        Log("PROBE ERROR: " + message);
    }

    private void LogNativeErrorOnce(
        string key,
        string message)
    {
        if (!_nativeErrorsLogged.Add(key))
        {
            return;
        }

        Log(message);
    }

    private void Log(string message)
    {
        File.AppendAllText(
            _logPath,
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message + Environment.NewLine);
    }
}
```
