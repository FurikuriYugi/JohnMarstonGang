using System;
using System.Collections.Generic;

public class GangMember
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public CompanionType CompanionType { get; set; } = CompanionType.Human;
    public GangMemberRole Role { get; set; } = GangMemberRole.Fighter;
    public RelationshipAlignment Alignment { get; set; } = RelationshipAlignment.Neutral;
    public CombatStyle CombatStyle { get; set; } = CombatStyle.RangedShort;
    public TacticalState TacticalState { get; set; } = TacticalState.Idle;

    public int Loyalty { get; set; } = 50;
    public int Experience { get; set; }
    public int Health { get; set; } = 100;
    public int Hunger { get; set; }
    public int AmmoReserve { get; set; } = 50;

    public bool NeedsFood { get; set; }
    public bool NeedsAmmo { get; set; }
    public bool NeedsMedicine { get; set; }

    public string ModelName { get; set; } = string.Empty;
    public List<string> Weapons { get; set; } = new List<string>();
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();

    public void UpdateCombatStyleFromRole()
    {
        switch (Role)
        {
            case GangMemberRole.Scout:
                CombatStyle = CombatStyle.Stealth;
                break;
            case GangMemberRole.Hunter:
                CombatStyle = CombatStyle.RangedMedium;
                break;
            case GangMemberRole.Guard:
                CombatStyle = CombatStyle.RangedShort;
                break;
            case GangMemberRole.Medic:
                CombatStyle = CombatStyle.RangedShort;
                break;
            case GangMemberRole.Driver:
                CombatStyle = CombatStyle.RangedShort;
                break;
            case GangMemberRole.Gunner:
                CombatStyle = CombatStyle.RangedLong;
                break;
            case GangMemberRole.Engineer:
                CombatStyle = CombatStyle.RangedShort;
                break;
            default:
                CombatStyle = CombatStyle.RangedShort;
                break;
        }
    }

    public void ApplyNeedTick()
    {
        Hunger = Math.Min(100, Hunger + 1);
        if (Hunger >= 70)
            NeedsFood = true;

        if (AmmoReserve <= 10)
            NeedsAmmo = true;

        if (Health <= 35)
            NeedsMedicine = true;
    }

    public void Feed()
    {
        Hunger = 0;
        NeedsFood = false;
    }

    public void ResupplyAmmo(int amount)
    {
        AmmoReserve += Math.Max(0, amount);
        if (AmmoReserve > 10)
            NeedsAmmo = false;
    }

    public void Heal(int amount)
    {
        Health = Math.Min(100, Health + Math.Max(0, amount));
        if (Health > 35)
            NeedsMedicine = false;
    }
}