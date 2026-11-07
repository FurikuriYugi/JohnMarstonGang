using System;
using System.IO;
using System.Reflection;
using RDR2;

public class Test : Script
{
    // Display-only diagnostic control action.
    // INPUT_CONTEXT_X is commonly mapped to R; it is not the desired G binding.
    private const int DiagnosticInputHash = unchecked((int)0xE3BF959B);

    // Test-only ILO registration values.
    // Some parameter meanings are documented externally, not verified in-game.
    private const float IloRadius = 2.0f;
    private const float IloFocusDistance = 2.0f;
    private const int IloFlags = 0;
    private const float IloParameter5 = 0.0f;
    private const float IloParameter6 = 0.0f;
    private const int IloPromptHandle = 0;
    private const bool ShowApproachPrompt = false;
    private const int IloParameter9 = -1;

    private readonly string _logPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Test_Log.txt");

    private PlayerTargetEntityNative _getPlayerTargetEntity;
    private MethodInfo _doesEntityExist;
    private MethodInfo _isPedHuman;
    private MethodInfo _getTargetPromptGroup;
    private MethodInfo _registerInteractionLockonPrompt;
    private MethodInfo _unregisterInteractionLockonPrompt;

    private object _prompt;
    private int _promptHandle = -1;
    private int _promptGroup = -1;
    private int _targetHandle;
    private int _iloTargetHandle;
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

        Log("=== Prompt input-action display test initialized ===");
        Log("TEST ONLY: show a Recruit prompt for the focused human NPC.");
        Log(
            "DIAGNOSTIC INPUT: INPUT_CONTEXT_X (0xE3BF959B; " +
            "commonly mapped to keyboard R; not the intended G binding).");
        Log(
            "HYPOTHESIS: using a recognized prompt input action will show " +
            "whether the custom prompt creation and target-group path renders.");
        Log(
            "ILO registration values and target-group assignment remain " +
            "unchanged from the previous test.");
        Log("No ambient-group or active-group activation is performed.");
        Log("No recruitment or gang-data changes are performed.");
        Log(
            "ILO test values: radius=2.0; focusDistance=2.0; flags=0; " +
            "p5=0.0; p6=0.0; prompt=0; p8=false; p9=-1.");
        Log(
            "These native parameter meanings are not all verified against " +
            "the installed API.");
        Log(
            "This run logs the Boolean return values from ILO registration " +
            "and cleanup. A true return does not prove prompt rendering.");
        Log(
            "Holster your weapon, stand close to a human NPC, and focus them.");

        ResolveMethods();

        RDR2.UI.Screen.PrintSubtitle(
            "Focus a human NPC to test the Recruit prompt.");

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

            Log(
                "Diagnostic prompt completed; test only, " +
                "no recruitment performed.");

            RDR2.UI.Screen.PrintSubtitle(
                "Diagnostic prompt completed; no recruitment performed.");

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
        bool found;

        unsafe
        {
            found = _getPlayerTargetEntity(
                Game.Player.Handle,
                &foundHandle);
        }

        if (!found ||
            foundHandle <= 0 ||
            _doesEntityExist == null ||
            _isPedHuman == null)
        {
            return false;
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
            RegisterInteractionLockonPrompt(targetHandle);

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
                "PROMPT INPUT ACTION: INPUT_CONTEXT_X (0xE3BF959B); " +
                "the displayed keyboard mapping may vary by settings/context.");

            Log(
                "GROUP MODE: action prompt assigned to target group; " +
                "no ambient-group or active-group activation native called.");

            Log(
                "IN-GAME CHECK: confirm whether the Recruit prompt is visible. " +
                "Prompt properties and native return values alone do not " +
                "prove rendering.");
        }
        catch (Exception exception)
        {
            _setupFailedForTarget = true;

            Log(
                "PROMPT SETUP FAILED: " +
                GetExceptionMessage(exception));

            ClearPrompt();
        }
    }

    private void RegisterInteractionLockonPrompt(int targetHandle)
    {
        if (_registerInteractionLockonPrompt == null)
        {
            throw new InvalidOperationException(
                "REGISTER_INTERACTION_LOCKON_PROMPT wrapper was not found.");
        }

        if (_unregisterInteractionLockonPrompt == null)
        {
            throw new InvalidOperationException(
                "UNREGISTER_INTERACTION_LOCKON_PROMPT wrapper was not found; " +
                "refusing to register an ILO that this test cannot clean up.");
        }

        object[] arguments =
        {
            targetHandle,
            "Recruit",
            IloRadius,
            IloFocusDistance,
            IloFlags,
            IloParameter5,
            IloParameter6,
            IloPromptHandle,
            ShowApproachPrompt,
            IloParameter9
        };

        Log(
            "ILO REGISTER CALL: " +
            DescribeMethod(_registerInteractionLockonPrompt));

        object result = _registerInteractionLockonPrompt.Invoke(
            null,
            arguments);

        bool returnedTrue =
            result != null && Convert.ToBoolean(result);

        // Keep the handle so cleanup is attempted even if registration
        // reports false; the test should not leave an uncertain registration
        // behind.
        _iloTargetHandle = targetHandle;

        Log(
            "ILO REGISTER RESULT: target=" + targetHandle +
            "; returned=" + returnedTrue +
            "; raw=" + (result == null ? "null" : result.ToString()));
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
            DiagnosticInputHash,
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

            Log(
                "PLAYER TARGET: " +
                DescribeMethod(targetMethod));
        }
        else
        {
            Log(
                "ERROR: GET_PLAYER_TARGET_ENTITY wrapper not found.");
        }

        _doesEntityExist =
            FindNativeMethod("DOES_ENTITY_EXIST", 1);

        _isPedHuman =
            FindNativeMethod("IS_PED_HUMAN", 1);

        _getTargetPromptGroup =
            FindNativeMethod(
                "_UI_PROMPT_GET_GROUP_ID_FOR_TARGET_ENTITY",
                1);

        _registerInteractionLockonPrompt =
            FindNativeMethod(
                "REGISTER_INTERACTION_LOCKON_PROMPT",
                10);

        _unregisterInteractionLockonPrompt =
            FindNativeMethod(
                "UNREGISTER_INTERACTION_LOCKON_PROMPT",
                1);

        LogResolvedMethod(
            "REGISTER ILO",
            _registerInteractionLockonPrompt);

        LogResolvedMethod(
            "UNREGISTER ILO",
            _unregisterInteractionLockonPrompt);

        LogResolvedMethod(
            "TARGET PROMPT GROUP",
            _getTargetPromptGroup);
    }

    private void LogResolvedMethod(
        string label,
        MethodInfo method)
    {
        Log(
            method == null
                ? "ERROR: " + label + " wrapper not found."
                : label + ": " + DescribeMethod(method));
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
            "Prompt member '" + name +
            "' was not found or writable.");
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

        if (_iloTargetHandle > 0)
        {
            try
            {
                if (_unregisterInteractionLockonPrompt != null)
                {
                    object result =
                        _unregisterInteractionLockonPrompt.Invoke(
                            null,
                            new object[] { _iloTargetHandle });

                    bool returnedTrue =
                        result != null && Convert.ToBoolean(result);

                    Log(
                        "ILO UNREGISTER RESULT: target=" +
                        _iloTargetHandle +
                        "; returned=" + returnedTrue +
                        "; raw=" +
                        (result == null ? "null" : result.ToString()));
                }
            }
            catch (Exception exception)
            {
                Log(
                    "ILO UNREGISTER FAILED: " +
                    GetExceptionMessage(exception));
            }

            _iloTargetHandle = 0;
        }
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