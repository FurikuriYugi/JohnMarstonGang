using System;

public enum GangRole
{
    None,
    Recruit,
    Scout,
    Gunman,
    Hunter,
    Guard,
    Driver,
    Trader,
    Medic,
    Quartermaster
}

public enum CombatStyle
{
    NonLethalMelee,
    LethalMelee,
    RangedShort,
    RangedMedium,
    RangedLong,
    Stealth
}

public enum GangOrderType
{
    Follow,
    HoldPosition,
    Guard,
    Scout,
    Attack,
    Defend,
    Loot,
    SupplyRun,
    StayClose,
    Wait
}

public enum JobType
{
    Legal,
    Illegal,
    Protection,
    Escort,
    Robbery,
    Delivery,
    Hunting,
    Gathering,
    Investigation
}

public enum CompanionDisposition
{
    Loyal,
    Neutral,
    Wary,
    Hostile,
    Familiar
}

public static class GangTypeHelpers
{
    public static CombatStyle ParseCombatStyle(string value)
    {
        if (Enum.TryParse(value, true, out CombatStyle style))
            return style;

        return CombatStyle.RangedShort;
    }

    public static string ToSaveString(CombatStyle style)
    {
        return style.ToString();
    }
}