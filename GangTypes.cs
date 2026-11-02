// GangTypes.cs
public enum CombatStyle
{
    NonLethalMelee = 0,
    LethalMelee = 1,
    RangedShort = 2,
    RangedMedium = 3,
    RangedLong = 4,
    Stealth = 5
}

public static class CombatStyleExtensions
{
    public static CombatStyle Next(this CombatStyle style)
    {
        switch (style)
        {
            case CombatStyle.NonLethalMelee: return CombatStyle.LethalMelee;
            case CombatStyle.LethalMelee: return CombatStyle.RangedShort;
            case CombatStyle.RangedShort: return CombatStyle.RangedMedium;
            case CombatStyle.RangedMedium: return CombatStyle.RangedLong;
            case CombatStyle.RangedLong: return CombatStyle.Stealth;
            default: return CombatStyle.NonLethalMelee;
        }
    }
}