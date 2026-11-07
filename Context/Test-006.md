<!-- Test lines 1251-1468 of 1468 -->
```csharp
        object prompt,
        string propertyName,
        object value)
    {
        Type type = prompt.GetType();

        PropertyInfo property =
            type.GetProperty(
                propertyName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (property != null &&
            property.CanWrite)
        {
            property.SetValue(
                prompt,
                ConvertArgument(
                    value,
                    property.PropertyType),
                null);
            return;
        }

        FieldInfo field =
            type.GetField(
                propertyName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        if (field != null)
        {
            field.SetValue(
                prompt,
                ConvertArgument(
                    value,
                    field.FieldType));
            return;
        }

        throw new InvalidOperationException(
            "Prompt member '" +
            propertyName +
            "' was not found or is not writable.");
    }

    private static bool GetPromptBooleanProperty(
        object prompt,
        string propertyName)
    {
        object value =
            GetPromptMember(
                prompt,
                propertyName);

        return value != null &&
            Convert.ToBoolean(value);
    }

    private static int TryGetNativePromptHandle(
        object prompt)
    {
        string[] memberNames =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "Id"
        };

        foreach (string memberName in memberNames)
        {
            object value =
                GetPromptMember(
                    prompt,
                    memberName);

            if (value != null)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    // Try the next candidate member.
                }
            }
        }

        Type type = prompt.GetType();

        MethodInfo[] methods =
            type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

        foreach (MethodInfo method in methods)
        {
            if (method.GetParameters().Length != 0 ||
                method.ReturnType != typeof(int))
            {
                continue;
            }

            if (method.Name.IndexOf(
                    "Handle",
                    StringComparison.OrdinalIgnoreCase) >= 0)
            {
                try
                {
                    return Convert.ToInt32(
                        method.Invoke(prompt, null));
                }
                catch
                {
                    // Continue looking for another handle accessor.
                }
            }
        }

        return -1;
    }

    private static string DescribeMethod(MethodInfo method)
    {
        if (method == null)
        {
            return "null";
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        string[] parameterNames =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterNames[i] =
                parameters[i].ParameterType.FullName;
        }

        return method.DeclaringType.FullName +
            "." +
            method.Name +
            "(" +
            string.Join(", ", parameterNames) +
            ") -> " +
            method.ReturnType.FullName;
    }

    private static string GetExceptionMessage(
        Exception exception)
    {
        if (exception is TargetInvocationException &&
            exception.InnerException != null)
        {
            return exception.InnerException.Message;
        }

        return exception.Message;
    }

    private void LogResolvedOrMissing(
        string label,
        MethodInfo method)
    {
        if (method == null)
        {
            Log("WARNING: " + label + " was not found.");
            return;
        }

        Log(
            label +
            " RESOLVED: " +
            DescribeMethod(method));
    }

    private void ClearLog()
    {
        try
        {
            File.WriteAllText(
                _logPath,
                string.Empty);
        }
        catch
        {
            // Logging errors should not prevent the test script loading.
        }
    }

    private void Log(string message)
    {
        string line =
            "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " +
            message;

        try
        {
            File.AppendAllText(
                _logPath,
                line +
                Environment.NewLine);
        }
        catch
        {
            // Keep test behavior running if the log file is unavailable.
        }
    }
}
```
