using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    private const int RecruitInputHash = unchecked((int)0x760A9C6F);

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExist;
    private MethodInfo _isPedHuman;
    private MethodInfo _getTargetPromptGroup;

    private object _prompt;
    private int _promptHandle = -1;
    private int _promptGroup = -1;
    private int _targetHandle;
    private int _lastLoggedTarget = -1;
    private bool _completedForTarget;
    private bool _setupFailedForTarget;
    private bool _errorLogged;

    private unsafe delegate bool PlayerTargetEntityNative(
        int playerHandle,
        int* entityHandle);

    public Test()
    {
        File.WriteAllText(_logPath, string.Empty);

        Log("=== Focused interaction-option test initialized ===");
        Log("TEST ONLY: show a G Recruit option for the focused human NPC.");
        Log("HYPOTHESIS: target-group assignment alone displays the prompt.");
        Log("No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log("Holster your weapon, stand close to a human NPC, and focus them.");

        ResolveMethods();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC to test the G Recruit option.");

        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdatePrompt();
        }
        catch (Exception exception)
        {
            if (!_errorLogged)
            {
                _errorLogged = true;
                Log("ERROR: " + GetExceptionMessage(exception));

                RDR2.UI.Screen.PrintSubtitle(
                    "Prompt test error. Check Test_Log.txt.");
            }
        }
    }

    private void UpdatePrompt()
    {
        int focusedTarget = 0;

        if (Game.Player.IsFreeFocusing)
        {
            TryGetFocusedHuman(out focusedTarget);
        }

        if (focusedTarget != _targetHandle)
        {
            ClearPrompt();
            _targetHandle = focusedTarget;
            _completedForTarget = false;
            _setupFailedForTarget = false;

            if (_lastLoggedTarget != focusedTarget)
            {
                Log(
                    focusedTarget > 0
                        ? "FOCUSED HUMAN TARGET: " + focusedTarget
                        : "FOCUSED HUMAN TARGET: none");

                _lastLoggedTarget = focusedTarget;
            }
        }

        if (_targetHandle <= 0 ||
            _completedForTarget ||
            _setupFailedForTarget)
        {
            return;
        }

        if (_prompt == null)
        {
            CreateTargetPrompt(_targetHandle);
        }

        if (_prompt == null)
        {
            return;
        }

        if (GetBooleanMember(_prompt, "HasCompleted"))
        {
            _completedForTarget = true;
            Log("G prompt completed; test only, no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "G prompt completed; no recruitment performed.");

            ClearPrompt();
        }
    }

    private bool TryGetFocusedHuman(out int targetHandle)
    {
        targetHandle = 0;

        if (_getPlayerTargetEntity == null)
        {
            return false;
        }

        int foundHandle = 0;

        unsafe
        {
            bool found = _getPlayerTargetEntity(
                Game.Player.Handle,
                &foundHandle);

            if (!found ||
                foundHandle <= 0 ||
                _doesEntityExist == null ||
                _isPedHuman == null)
            {
                return false;
            }
        }

        bool exists = Convert.ToBoolean(
            _doesEntityExist.Invoke(
                null,
                new object[] { foundHandle }));

        if (!exists ||
            foundHandle == Game.Player.Character.Handle)
        {
            return false;
        }

        bool isHuman = Convert.ToBoolean(
            _isPedHuman.Invoke(
                null,
                new object[] { foundHandle }));

        if (!isHuman)
        {
            return false;
        }

        targetHandle = foundHandle;
        return true;
    }

    private void CreateTargetPrompt(int targetHandle)
    {
        try
        {
            if (_getTargetPromptGroup == null)
            {
                throw new InvalidOperationException(
                    "Target prompt-group wrapper was not found.");
            }

            _promptGroup = Convert.ToInt32(
                _getTargetPromptGroup.Invoke(
                    null,
                    new object[] { targetHandle }));

            if (_promptGroup < 0)
            {
                throw new InvalidOperationException(
                    "Target returned an invalid prompt group.");
            }

            _prompt = CreatePromptObject();
            _promptHandle = GetPromptHandle(_prompt);

            if (_promptHandle < 0)
            {
                throw new InvalidOperationException(
                    "Could not read the prompt handle.");
            }

            SetMember(_prompt, "Text", "Recruit");

            RDR2.Native.HUD._UI_PROMPT_SET_GROUP(
                _promptHandle,
                _promptGroup,
                0);

            SetMember(_prompt, "Enabled", true);
            SetMember(_prompt, "Visible", true);

            Log(
                "PROMPT CREATED: target=" + targetHandle +
                "; handle=" + _promptHandle +
                "; targetGroup=" + _promptGroup);

            Log(
                "GROUP MODE: prompt assigned to target group; " +
                "no group-activation native called.");

            Log(
                "IN-GAME CHECK: confirm whether G Recruit is visible. " +
                "Prompt properties alone do not prove rendering.");
        }
        catch (Exception exception)
        {
            _setupFailedForTarget = true;
            Log("PROMPT SETUP FAILED: " + GetExceptionMessage(exception));
            ClearPrompt();
        }
    }

    private object CreatePromptObject()
    {
        Type promptType =
            typeof(Game).Assembly.GetType("RDR2.UI.Prompt", false);

        if (promptType == null)
        {
            throw new InvalidOperationException(
                "RDR2.UI.Prompt type was not found.");
        }

        MethodInfo createMethod = null;

        MethodInfo[] methods = promptType.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            ParameterInfo[] parameters = method.GetParameters();

            if (method.Name == "CreatePrompt" &&
                parameters.Length == 8 &&
                parameters[2].ParameterType == typeof(string))
            {
                createMethod = method;
                break;
            }
        }

        if (createMethod == null)
        {
            throw new InvalidOperationException(
                "Eight-parameter Prompt.CreatePrompt was not found.");
        }

        ParameterInfo[] createParameters =
            createMethod.GetParameters();

        object[] arguments = new object[8];

        arguments[0] = ConvertArgument(
            RecruitInputHash,
            createParameters[0].ParameterType);

        arguments[1] = Enum.Parse(
            createParameters[1].ParameterType,
            "Press",
            true);

        arguments[2] = "Recruit";

        for (int i = 3; i <= 6; i++)
        {
            arguments[i] = ConvertArgument(
                0,
                createParameters[i].ParameterType);
        }

        Type timingType = createParameters[7].ParameterType;

        arguments[7] = timingType.IsValueType
            ? Activator.CreateInstance(timingType)
            : null;

        object prompt = createMethod.Invoke(null, arguments);

        if (prompt == null)
        {
            throw new InvalidOperationException(
                "Prompt.CreatePrompt returned null.");
        }

        return prompt;
    }

    private void ResolveMethods()
    {
        MethodInfo targetMethod =
            FindNativeMethod("GET_PLAYER_TARGET_ENTITY", 2);

        if (targetMethod != null)
        {
            _getPlayerTargetEntity =
                (PlayerTargetEntityNative)Delegate.CreateDelegate(
                    typeof(PlayerTargetEntityNative),
                    targetMethod);

            Log("PLAYER TARGET: " + DescribeMethod(targetMethod));
        }
        else
        {
            Log("ERROR: GET_PLAYER_TARGET_ENTITY wrapper not found.");
        }

        _doesEntityExist =
            FindNativeMethod("DOES_ENTITY_EXIST", 1);

        _isPedHuman =
            FindNativeMethod("IS_PED_HUMAN", 1);

        _getTargetPromptGroup =
            FindNativeMethod(
                "_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
                1);
    }

    private static MethodInfo FindNativeMethod(
        string methodName,
        int parameterCount)
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

        foreach (Type type in types)
        {
            if (type == null ||
                type.Namespace == null ||
                !type.Namespace.StartsWith(
                    "RDR2.Native",
                    StringComparison.Ordinal))
            {
                continue;
            }

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (method.Name == methodName &&
                    method.GetParameters().Length == parameterCount)
                {
                    return method;
                }
            }
        }

        return null;
    }

    private static int GetPromptHandle(object prompt)
    {
        string[] names =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "Id"
        };

        foreach (string name in names)
        {
            object value = GetMember(prompt, name);

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

        return -1;
    }

    private static object GetMember(
        object instance,
        string name)
    {
        Type type = instance.GetType();

        PropertyInfo property = type.GetProperty(
            name,
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);

        if (property != null && property.CanRead)
        {
            return property.GetValue(instance, null);
        }

        FieldInfo field = type.GetField(
            name,
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);

        return field == null
            ? null
            : field.GetValue(instance);
    }

    private static void SetMember(
        object instance,
        string name,
        object value)
    {
        Type type = instance.GetType();

        PropertyInfo property = type.GetProperty(
            name,
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);

        if (property != null && property.CanWrite)
        {
            property.SetValue(
                instance,
                ConvertArgument(value, property.PropertyType),
                null);

            return;
        }

        FieldInfo field = type.GetField(
            name,
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);

        if (field != null)
        {
            field.SetValue(
                instance,
                ConvertArgument(value, field.FieldType));

            return;
        }

        throw new InvalidOperationException(
            "Prompt member '" + name + "' was not found or writable.");
    }

    private static bool GetBooleanMember(
        object instance,
        string name)
    {
        object value = GetMember(instance, name);

        return value != null &&
            Convert.ToBoolean(value);
    }

    private static object ConvertArgument(
        object value,
        Type targetType)
    {
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

        return Convert.ChangeType(value, targetType);
    }

    private void ClearPrompt()
    {
        if (_prompt is IDisposable)
        {
            try
            {
                ((IDisposable)_prompt).Dispose();
            }
            catch
            {
                // Cleanup must not break the test loop.
            }
        }

        _prompt = null;
        _promptHandle = -1;
        _promptGroup = -1;
    }

    private static string DescribeMethod(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        string[] types = new string[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            types[i] = parameters[i].ParameterType.FullName;
        }

        return method.DeclaringType.FullName + "." +
            method.Name + "(" + string.Join(", ", types) + ") -> " +
            method.ReturnType.FullName;
    }

    private static string GetExceptionMessage(Exception exception)
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
            "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " +
            message + Environment.NewLine;

        File.AppendAllText(_logPath, line);
    }
}