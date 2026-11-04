using System;
using System.Collections.Generic;

public static class CompanionSystem
{
    public static CompanionData CreateHumanCompanion(string name, string role)
    {
        return new CompanionData
        {
            CompanionId = Guid.NewGuid().ToString("N"),
            Name = name,
            Species = "Human",
            IsHuman = true,
            Role = role,
            Personality = "Neutral",
            Loyalty = 50,
            SkillCombat = 50,
            SkillStealth = 50,
            SkillLooting = 50,
            SkillSupport = 50,
            CurrentCombatStyle = "Ranged Medium",
            CurrentTask = "Idle",
            IsRecruitable = true,
            IsFollowingPlayer = true
        };
    }

    public static CompanionData CreateAnimalCompanion(string name, string species)
    {
        return new CompanionData
        {
            CompanionId = Guid.NewGuid().ToString("N"),
            Name = name,
            Species = species,
            IsHuman = false,
            Role = "Animal Companion",
            Personality = "Loyal",
            Loyalty = 75,
            SkillCombat = 35,
            SkillStealth = 65,
            SkillLooting = 0,
            SkillSupport = 25,
            CurrentCombatStyle = "Stealth",
            CurrentTask = "Follow",
            IsRecruitable = true,
            IsFollowingPlayer = true
        };
    }

    public static void UpdateCompanion(CompanionData companion, TacticalContext context)
    {
        if (companion == null)
            return;

        if (!companion.IsRecruitable)
            companion.IsRecruitable = companion.Loyalty >= 25;

        companion.CurrentTask = ResolveCurrentTask(companion, context);
        companion.CurrentCombatStyle = ResolveCombatStyle(companion, context);
    }

    public static string ResolveCurrentTask(CompanionData companion, TacticalContext context)
    {
        if (companion == null)
            return "Idle";

        if (context == null)
            return "Idle";

        if (context.IsInCombat)
        {
            if (context.IsStealth)
                return "Stealth Assist";

            return "Combat Assist";
        }

        if (context.IsShopping)
            return "Run Errands";

        if (context.IsTraveling)
            return "Follow Player";

        if (context.IsRobbing)
            return "Heist Support";

        if (context.IsInCamp)
            return "Camp Duty";

        return "Idle";
    }

    public static string ResolveCombatStyle(CompanionData companion, TacticalContext context)
    {
        if (companion == null)
            return "Ranged Medium";

        if (context != null && context.IsStealth)
        {
            if (companion.SkillStealth >= companion.SkillCombat)
                return "Stealth";

            return "Stealth";
        }

        if (companion.SkillCombat >= 75)
            return "Ranged Long";

        if (companion.SkillCombat >= 55)
            return "Ranged Medium";

        if (companion.SkillCombat >= 35)
            return "Ranged Short";

        return "Lethal Melee";
    }

    public static void AddMemory(CompanionData companion, string memory)
    {
        if (companion == null || string.IsNullOrWhiteSpace(memory))
            return;

        if (companion.Memories == null)
            companion.Memories = new List<string>();

        companion.Memories.Add(memory);
    }

    public static void AddGoal(CompanionData companion, string goal)
    {
        if (companion == null || string.IsNullOrWhiteSpace(goal))
            return;

        if (companion.Goals == null)
            companion.Goals = new List<string>();

        companion.Goals.Add(goal);
    }

    public static void GiveItem(CompanionData companion, string item)
    {
        if (companion == null || string.IsNullOrWhiteSpace(item))
            return;

        if (companion.InventoryItems == null)
            companion.InventoryItems = new List<string>();

        companion.InventoryItems.Add(item);
    }

    public static void AdjustLoyalty(CompanionData companion, int amount)
    {
        if (companion == null)
            return;

        companion.Loyalty += amount;

        if (companion.Loyalty < 0)
            companion.Loyalty = 0;

        if (companion.Loyalty > 100)
            companion.Loyalty = 100;
    }

    public static bool IsReadyForTask(CompanionData companion)
    {
        if (companion == null)
            return false;

        return companion.IsFollowingPlayer && companion.Loyalty >= 25;
    }
}