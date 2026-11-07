<!-- Test lines 1001-1250 of 1255 -->
```csharp
                    object convertedValue = ConvertArgument(
                        suppliedArguments[fixedParameterCount + i],
                        elementType);

                    packedArguments.SetValue(
                        convertedValue,
                        i);
                }
                catch
                {
                    convertedArguments = null;
                    return false;
                }
            }

            convertedArguments[parameters.Length - 1] =
                packedArguments;

            return true;
        }

        for (int i = suppliedArguments.Length;
            i < parameters.Length;
            i++)
        {
            if (!parameters[i].IsOptional)
            {
                convertedArguments = null;
                return false;
            }

            convertedArguments[i] =
                parameters[i].DefaultValue;
        }

        return true;
    }

    private static bool IsParamArray(
        ParameterInfo parameter)
    {
        return parameter != null &&
            parameter.GetCustomAttributes(
                typeof(ParamArrayAttribute),
                false).Length > 0;
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
        if (targetType == null)
        {
            throw new InvalidOperationException(
                "Native parameter type is unknown.");
        }

        if (value == null)
        {
            return targetType.IsValueType
                ? Activator.CreateInstance(targetType)
                : null;
        }

        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(targetType, value);
        }

        if (targetType == typeof(IntPtr))
        {
            long numericValue =
                Convert.ToInt64(value);

            return new IntPtr(numericValue);
        }

        if (targetType == typeof(UIntPtr))
        {
            ulong numericValue =
                Convert.ToUInt64(value);

            return new UIntPtr(numericValue);
        }

        if (targetType == typeof(uint))
        {
            return unchecked(
                (uint)Convert.ToInt64(value));
        }

        if (targetType == typeof(ulong))
        {
            return Convert.ToUInt64(value);
        }

        if (targetType == typeof(int))
        {
            return unchecked(
                (int)Convert.ToInt64(value));
        }

        if (targetType == typeof(long))
        {
            return Convert.ToInt64(value);
        }

        Type nullableType =
            Nullable.GetUnderlyingType(targetType);

        if (nullableType != null)
        {
            return Convert.ChangeType(
                value,
                nullableType);
        }

        return Convert.ChangeType(
            value,
            targetType);
    }

    private void ClearPrompt()
    {
        int oldPromptHandle = _promptHandle;

        _promptHandle = -1;
        _promptGroup = -1;

        if (oldPromptHandle <= 0)
        {
            return;
        }

        if (_promptDelete == null)
        {
            Log(
                "PROMPT DELETE SKIPPED: delete wrapper unavailable; " +
                "handle=" + oldPromptHandle);

            return;
        }

        try
        {
            InvokeNative(
                _promptDelete,
                oldPromptHandle);

            Log(
                "PROMPT DELETE: handle=" +
                oldPromptHandle);
        }
        catch (Exception exception)
        {
            Log(
                "PROMPT DELETE FAILED: " +
                GetExceptionMessage(exception));
        }
    }

    private static string DescribeMethod(
        MethodInfo method)
    {
        if (method == null)
        {
            return "<null method>";
        }

        ParameterInfo[] parameters =
            method.GetParameters();

        string[] types =
            new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            types[i] =
                parameters[i].ParameterType.FullName;
        }

        string genericInfo = string.Empty;

        if (method.IsGenericMethod)
        {
            Type[] genericArguments =
                method.GetGenericArguments();

            string[] genericNames =
                new string[genericArguments.Length];

            for (int i = 0; i < genericArguments.Length; i++)
            {
                genericNames[i] =
                    genericArguments[i].FullName ??
                    genericArguments[i].Name;
            }

            genericInfo =
                "<" +
                string.Join(", ", genericNames) +
                ">";
        }

        string declaringType =
            method.DeclaringType == null
                ? "<unknown type>"
                : method.DeclaringType.FullName;

        string returnType =
            method.ReturnType == null
                ? "<unknown return type>"
                : method.ReturnType.FullName;

        return declaringType +
            "." +
            method.Name +
            genericInfo +
            "(" +
            string.Join(", ", types) +
            ") -> " +
            returnType;
    }

    private static string GetExceptionMessage(
        Exception exception)
    {
        TargetInvocationException invocationException =
            exception as TargetInvocationException;

        return invocationException != null &&
            invocationException.InnerException != null
                ? invocationException.InnerException.Message
                : exception.Message;
    }

    private void Log(string message)
    {
        string line =
            "[" +
            DateTime.Now.ToString("HH:mm:ss.fff") +
            "] " +
            message +
            Environment.NewLine;

```
