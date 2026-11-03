using System;

public enum GangMemberRole
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
    Quartermaster,
    Gunner,
    Assistant
}

public enum CompanionType
{
    Human,
    Horse,
    Dog,
    Bird,
    Other
}

public enum CombatStyle
{
    NonLethalMelee,
    LethalMelee,
    ShortRange,
    MediumRange,
    LongRange,
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

public enum GangMood
{
    Loyal,
    Neutral,
    Nervous,
    Aggressive,
    Tired
}

public enum CompanionDisposition
{
    Loyal,
    Neutral,
    Wary,
    Hostile,
    Familiar
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

public static class GangTypeHelpers
{
    public static CombatStyle ParseCombatStyle(string value)
    {
        if (Enum.TryParse(value, true, out CombatStyle style))
            return style;

        return CombatStyle.ShortRange;
    }

    public static string ToSaveString(CombatStyle style)
    {
        return style.ToString();
    }
}