using System;
using System.Collections.Generic;

public static class CommandSystem
{
    public static string ParseCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Unknown";
        }

        string text = input.Trim().ToLowerInvariant();

        if (text.Contains("follow"))
        {
            return "Follow";
        }

        if (text.Contains("hold") || text.Contains("stay"))
        {
            return "Hold";
        }

        if (text.Contains("stealth"))
        {
            return "Stealth";
        }

        if (text.Contains("attack") || text.Contains("combat"))
        {
            return "Attack";
        }

        if (text.Contains("loot"))
        {
            return "Loot";
        }

        if (text.Contains("guard"))
        {
            return "Guard";
        }

        if (text.Contains("shop"))
        {
            return "Shop";
        }

        if (text.Contains("travel"))
        {
            return "Travel";
        }

        if (text.Contains("camp"))
        {
            return "Camp";
        }

        if (text.Contains("heist"))
        {
            return "Heist";
        }

        if (text.Contains("job"))
        {
            return "Job";
        }

        return "Unknown";
    }

    public static void ExecuteCommand(GangManager manager, TacticalContext context, string command)
    {
        if (manager == null)
        {
            return;
        }

        string parsed = ParseCommand(command);
        List<GangMember> members = manager.GetAllMembers();

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
            {
                continue;
            }

            ApplyCommandToMember(member, context, parsed);
        }
    }

    public static void ApplyCommandToMember(GangMember member, TacticalContext context, string command)
    {
        if (member == null)
        {
            return;
        }

        switch (command)
        {
            case "Follow":
                member.IsFollowingPlayer = true;
                member.CurrentTask = "Follow Player";
                break;

            case "Hold":
                member.IsFollowingPlayer = false;
                member.CurrentTask = "Hold Position";
                break;

            case "Stealth":
                member.CombatStyle = "Stealth";
                member.CurrentTask = "Stealth Mode";
                break;

            case "Attack":
                member.CurrentTask = "Attack";
                break;

            case "Loot":
                member.CurrentTask = "Loot";
                break;

            case "Guard":
                member.CurrentTask = "Guard Area";
                break;

            case "Shop":
                member.CurrentTask = "Shopping Support";
                member.IsFollowingPlayer = true;
                break;

            case "Travel":
                member.CurrentTask = "Travel";
                member.IsFollowingPlayer = true;
                break;

            case "Camp":
                member.CurrentTask = "Camp Duty";
                break;

            case "Heist":
                member.CurrentTask = "Heist Support";
                break;

            case "Job":
                member.CurrentTask = "Job Assignment";
                break;

            default:
                break;
        }

        if (context != null)
        {
            if (context.IsStealth && command == "Attack")
            {
                member.CombatStyle = "Stealth";
            }

            if (context.IsInCombat && command == "Guard")
            {
                member.BehaviorPriority += 5;
            }

            if (context.IsUsingArtillery && command == "Attack")
            {
                member.CurrentTask = "Artillery Support";
            }

            if (context.IsShopping && command == "Shop")
            {
                member.CurrentTask = "Shopping Support";
            }

            if (context.IsRobbing && command == "Heist")
            {
                member.CurrentTask = "Heist Support";
            }
        }
    }

    public static void BroadcastTask(GangManager manager, string task)
    {
        if (manager == null || string.IsNullOrWhiteSpace(task))
        {
            return;
        }

        List<GangMember> members = manager.GetAllMembers();

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
            {
                continue;
            }

            member.CurrentTask = task;
        }
    }

    public static void SetMemberFollowState(GangManager manager, string memberId, bool follow)
    {
        if (manager == null || string.IsNullOrWhiteSpace(memberId))
        {
            return;
        }

        GangMember member = manager.GetMember(memberId);
        if (member == null)
        {
            return;
        }

        CompanionSystem.SetFollowState(CreateCompanionProxy(member), follow);
        member.IsFollowingPlayer = follow;
        member.CurrentTask = follow ? "Follow Player" : "Hold Position";
    }

    public static void AssignShoppingNeeds(GangManager manager, string memberId, string shoppingNeed)
    {
        if (manager == null || string.IsNullOrWhiteSpace(memberId))
        {
            return;
        }

        GangMember member = manager.GetMember(memberId);
        if (member == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(shoppingNeed))
        {
            return;
        }

        if (member.MemoryTags == null)
        {
            member.MemoryTags = new List<string>();
        }

        member.MemoryTags.Add("Needs: " + shoppingNeed);
        member.CurrentTask = "Shopping Support";
    }

    private static CompanionData CreateCompanionProxy(GangMember member)
    {
        CompanionData companion = new CompanionData
        {
            CompanionId = member.MemberId,
            Name = member.Name,
            Species = "Human",
            IsHuman = true,
            Role = member.Role,
            Personality = "Neutral",
            Loyalty = member.Loyalty,
            SkillCombat = member.CombatSkill,
            SkillStealth = member.StealthSkill,
            SkillLooting = 50,
            SkillSupport = member.SupportSkill,
            CurrentCombatStyle = member.CombatStyle,
            CurrentTask = member.CurrentTask,
            IsRecruitable = true,
            IsFollowingPlayer = member.IsFollowingPlayer,
            InventoryItems = member.InventoryItems ?? new List<string>(),
            Memories = new List<string>(),
            Goals = member.Goals ?? new List<string>()
        };

        return companion;
    }
}