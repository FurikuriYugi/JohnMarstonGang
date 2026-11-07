using System;
using System.Collections.Generic;
using System.Reflection;
using RDR2;

public class NativeLockOnPromptTest : Script
{
    private const int ReportIntervalMilliseconds = 2000;

    private readonly List<string> _nativeSignatureMessages =
        new List<string>();

    private DateTime _nextReportUtc = DateTime.MinValue;
    private int _nextSignatureIndex;
    private bool _showStatusNext = true;
    private bool _startupReported;

    public NativeLockOnPromptTest()
    {
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            UpdateDiagnostic();
        }
        catch (Exception exception)
        {
            RDR2.UI.Screen.PrintSubtitle(
                string.Format(
                    "Target diagnostic exception: {0}",
                    exception.Message));
        }
    }

    private void UpdateDiagnostic()
    {
        if (!_startupReported)
        {
            _startupReported = true;
            BuildNativeSignatureMessages();

            RDR2.UI.Screen.PrintSubtitle(
                "Target-only diagnostic loaded. No prompt or recruitment action is active.");
        }

        if (DateTime.UtcNow < _nextReportUtc)
        {
            return;
        }

        _nextReportUtc =
            DateTime.UtcNow.AddMilliseconds(
                ReportIntervalMilliseconds);

        if (_showStatusNext || _nativeSignatureMessages.Count == 0)
        {
            ReportTargetStatus();
            _showStatusNext = false;
            return;
        }

        ReportNextNativeSignature();
        _showStatusNext = true;
    }

    private void ReportTargetStatus()
    {
        Ped interactionTarget =
            Game.Player.GetInteractionLockonTargetEntity() as Ped;

        Ped freeAimTarget =
            Game.Player.GetFreeAimEntity() as Ped;

        int interactionTargetHandle =
            GetExistingPedHandle(interactionTarget);

        int freeAimTargetHandle =
            GetExistingPedHandle(freeAimTarget);

        string freeFocusingState =
            GetFreeFocusingState();

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Targets: interaction={0}; freeAim={1}; IsFreeFocusing={2}.",
                FormatHandle(interactionTargetHandle),
                FormatHandle(freeAimTargetHandle),
                freeFocusingState));
    }

    private void BuildNativeSignatureMessages()
    {
        Type playerNativeType = typeof(RDR2.Native.PLAYER);

        MethodInfo[] methods =
            playerNativeType.GetMethods(
                BindingFlags.Public |
                BindingFlags.Static);

        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo method = methods[i];

            if (!IsTargetGetterName(method.Name))
            {
                continue;
            }

            _nativeSignatureMessages.Add(
                FormatMethodSignature(method));
        }

        if (_nativeSignatureMessages.Count == 0)
        {
            _nativeSignatureMessages.Add(
                "PLAYER wrapper has no public target-getter methods with the expected names.");
        }
    }

    private static bool IsTargetGetterName(string methodName)
    {
        return string.Equals(
                   methodName,
                   "GET_PLAYER_TARGET_ENTITY",
                   StringComparison.OrdinalIgnoreCase) ||
               string.Equals(
                   methodName,
                   "GET_PLAYER_INTERACTION_TARGET_ENTITY",
                   StringComparison.OrdinalIgnoreCase);
    }

    private static string FormatMethodSignature(MethodInfo method)
    {
        ParameterInfo[] parameters =
            method.GetParameters();

        List<string> parameterDescriptions =
            new List<string>();

        for (int i = 0; i < parameters.Length; i++)
        {
            ParameterInfo parameter = parameters[i];
            Type parameterType = parameter.ParameterType;
            string direction = string.Empty;

            if (parameterType.IsByRef)
            {
                direction = parameter.IsOut ? "out " : "ref ";
                parameterType = parameterType.GetElementType();
            }

            parameterDescriptions.Add(
                string.Format(
                    "{0}{1} {2}",
                    direction,
                    parameterType.Name,
                    parameter.Name));
        }

        return string.Format(
            "{0} {1}({2})",
            method.ReturnType.Name,
            method.Name,
            string.Join(", ", parameterDescriptions.ToArray()));
    }

    private void ReportNextNativeSignature()
    {
        if (_nativeSignatureMessages.Count == 0)
        {
            return;
        }

        string message =
            _nativeSignatureMessages[_nextSignatureIndex];

        _nextSignatureIndex++;

        if (_nextSignatureIndex >= _nativeSignatureMessages.Count)
        {
            _nextSignatureIndex = 0;
        }

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "PLAYER API: {0}",
                message));
    }

    private static string GetFreeFocusingState()
    {
        object player = Game.Player;

        if (player == null)
        {
            return "player-unavailable";
        }

        const BindingFlags flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        Type playerType = player.GetType();

        try
        {
            PropertyInfo property =
                playerType.GetProperty(
                    "IsFreeFocusing",
                    flags);

            if (property != null &&
                property.PropertyType == typeof(bool))
            {
                return Convert.ToString(
                    property.GetValue(player, null));
            }

            MethodInfo method =
                playerType.GetMethod(
                    "IsFreeFocusing",
                    flags,
                    null,
                    Type.EmptyTypes,
                    null);

            if (method != null &&
                method.ReturnType == typeof(bool))
            {
                return Convert.ToString(
                    method.Invoke(player, null));
            }
        }
        catch
        {
            return "read-error";
        }

        return "not-exposed";
    }

    private static int GetExistingPedHandle(Ped ped)
    {
        if (ped == null || !ped.Exists())
        {
            return 0;
        }

        return ped.Handle;
    }

    private static string FormatHandle(int handle)
    {
        return handle > 0
            ? handle.ToString()
            : "none";
    }
}