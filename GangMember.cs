using System;
using System.Collections.Generic;

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public GangMemberRole Role { get; set; }
    public CombatStyle PreferredCombatStyle { get; set; }
    public GangMood Mood { get; set; }
    public int Loyalty { get; set; }
    public int SkillCombat { get; set; }
    public int SkillStealth { get; set; }
    public int SkillHandling { get; set; }
    public int SkillTrading { get; set; }
    public string WeaponChoice { get; set; }
    public string Outfit { get; set; }
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public List<string> InventoryItems { get; set; } = new List<string>();
    public bool FollowingPlayer { get; set; }
    public bool IsActive { get; set; }
    public bool IsInCombat { get; set; }
    public bool IsStealthing { get; set; }
    public bool IsShopping { get; set; }
    public bool IsOnJob { get; set; }
    public bool NeedsSupplies { get; set; }
    public bool IsDowned { get; set; }
    public bool IsAlive { get; set; } = true;
    public string CurrentTask { get; set; }

    public GangMember()
    {
        MemberId = Guid.NewGuid().ToString("N");
        Mood = GangMood.Neutral;
        Loyalty = 50;
        PreferredCombatStyle = CombatStyle.ShortRange;
        Type = CompanionType.Human;
        Role = GangMemberRole.Recruit;
        IsActive = true;
        IsAlive = true;
    }

    public void AddMemory(string memory)
    {
        if (!string.IsNullOrWhiteSpace(memory))
            Memories.Add(memory);
    }

    public void AddGoal(string goal)
    {
        if (!string.IsNullOrWhiteSpace(goal) && !Goals.Contains(goal))
            Goals.Add(goal);
    }

    public void AddItem(string itemName)
    {
        if (!string.IsNullOrWhiteSpace(itemName))
            InventoryItems.Add(itemName);
    }

    public bool RemoveItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            return false;

        return InventoryItems.Remove(itemName);
    }

    public void AdjustLoyalty(int amount)
    {
        Loyalty += amount;
        if (Loyalty < 0) Loyalty = 0;
        if (Loyalty > 100) Loyalty = 100;
    }

    public void SetCombatStyle(CombatStyle style, string weaponChoice = null)
    {
        PreferredCombatStyle = style;
        if (!string.IsNullOrWhiteSpace(weaponChoice))
            WeaponChoice = weaponChoice;
    }

    public void Recruit(string reason = null)
    {
        IsActive = true;
        IsAlive = true;
        FollowingPlayer = true;
        Mood = GangMood.Neutral;

        if (!string.IsNullOrWhiteSpace(reason))
            AddMemory("Recruited: " + reason);
    }

    public void Dismiss()
    {
        IsActive = false;
        FollowingPlayer = false;
        IsInCombat = false;
        IsStealthing = false;
        CurrentTask = "Dismissed";
        AddMemory("Dismissed from active duty.");
    }

    public void AssignOrder(GangOrderType order)
    {
        CurrentTask = order.ToString();

        switch (order)
        {
            case GangOrderType.Follow:
            case GangOrderType.StayClose:
                FollowingPlayer = true;
                IsShopping = false;
                break;
            case GangOrderType.HoldPosition:
            case GangOrderType.Wait:
                FollowingPlayer = false;
                break;
            case GangOrderType.Guard:
                FollowingPlayer = false;
                Role = GangMemberRole.Guard;
                break;
            case GangOrderType.Scout:
                FollowingPlayer = false;
                Role = GangMemberRole.Scout;
                break;
            case GangOrderType.Loot:
            case GangOrderType.SupplyRun:
                FollowingPlayer = false;
                break;
            case GangOrderType.Attack:
            case GangOrderType.Defend:
                IsInCombat = true;
                break;
        }

        AddMemory("Order assigned: " + order);
    }

    public bool WantsSupplies()
    {
        return NeedsSupplies || InventoryItems.Count == 0 || PersonalCash < 5m;
    }

    public bool ShouldUseStealth()
    {
        return PreferredCombatStyle == CombatStyle.Stealth || Mood == GangMood.Nervous || IsStealthing;
    }

    public string GetSuggestedWeapon()
    {
        if (ShouldUseStealth())
            return "Bow";

        switch (PreferredCombatStyle)
        {
            case CombatStyle.NonLethalMelee:
                return "Lasso";
            case CombatStyle.LethalMelee:
                return "Knife";
            case CombatStyle.ShortRange:
                return "Revolver";
            case CombatStyle.MediumRange:
                return "Repeater";
            case CombatStyle.LongRange:
                return "Rifle";
            default:
                return string.IsNullOrWhiteSpace(WeaponChoice) ? "Revolver" : WeaponChoice;
        }
    }
}