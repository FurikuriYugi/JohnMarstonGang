using System;
using System.Reflection;
using RDR2;

public class NativeLockOnPromptTest : Script
{
    private const int ReportIntervalMilliseconds = 750;

    private DateTime _nextReportUtc = DateTime.MinValue;
    private bool _startupReported;
    private bool _exceptionReported;

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
            if (!_exceptionReported)
            {
                _exceptionReported = true;
                RDR2.UI.Screen.PrintSubtitle(
                    string.Format(
                        "Target test error: {0}",
                        exception.Message));
            }
        }
    }

    private void UpdateDiagnostic()
    {
        if (!_startupReported)
        {
            _startupReported = true;
            RDR2.UI.Screen.PrintSubtitle(
                "Target test loaded. No prompt or recruitment action is active.");
        }

        if (DateTime.UtcNow < _nextReportUtc)
        {
            return;
        }

        _nextReportUtc =
            DateTime.UtcNow.AddMilliseconds(
                ReportIntervalMilliseconds);

        ReportTargetStatus();
    }

    private void ReportTargetStatus()
    {
        int targetHandle = 0;
        int playerIndex = RDR2.Native.PLAYER.PLAYER_ID();

        bool nativeFound;
        unsafe
        {
            nativeFound =
                RDR2.Native.PLAYER.GET_ENTITY_PLAYER_IS_FREE_AIMING_AT(
                    playerIndex,
                    &targetHandle);
        }


        Entity helperFreeAimTarget =
            Game.Player.GetFreeAimEntity();

        Entity helperInteractionTarget =
            Game.Player.GetInteractionLockonTargetEntity();

        int nativeTargetHandle =
            nativeFound
                ? GetExistingEntityHandle(targetHandle)
                : 0;

        string nativeType =
            nativeFound
                ? targetHandle.ToString()
                : "n/a";

        RDR2.UI.Screen.PrintSubtitle(
            string.Format(
                "Native hit={0} ped={1} type={2} | Player focus={3} aim={4} lock={5}",
                FormatBoolean(nativeFound),
                FormatHandle(nativeTargetHandle),
                nativeType,
                GetFreeFocusingState(),
                FormatHandle(
                    GetExistingEntityHandle(helperFreeAimTarget)),
                FormatHandle(
                    GetExistingEntityHandle(helperInteractionTarget))));
    }

    private static int GetExistingEntityHandle(Entity entity)
    {
        if (entity == null || !entity.Exists())
        {
            return 0;
        }

        return entity.Handle;
    }

    private static string FormatHandle(int handle)
    {
        return handle > 0
            ? handle.ToString()
            : "none";
    }

    private static string FormatBoolean(bool value)
    {
        return value
            ? "1"
            : "0";
    }

    private static string GetFreeFocusingState()
    {
        object player = Game.Player;

        if (player == null)
        {
            return "unavailable";
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
                return FormatBoolean(
                    (bool)property.GetValue(player, null));
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
                return FormatBoolean(
                    (bool)method.Invoke(player, null));
            }
        }
        catch
        {
            return "read-error";
        }

        return "not-exposed";
    }
}