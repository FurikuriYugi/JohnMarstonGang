using System;
using System.Collections.Generic;

public static class CommandSystem
{
    public static string ParseCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Unknown";

        string text = input.Trim().ToLowerInvariant();

        if (text.Contains("follow"))
            return "Follow";

        if (text.Contains("hold") || text.Contains("stay"))
            return "Hold";

        if (text.Contains("stealth"))
            return "Stealth";

        if (text.Contains("attack") || text.Contains("combat"))
            return "Attack";

        if (text.Contains("loot"))
            return "Loot";

        if (text.Contains("guard"))
            return "Guard";

        if (text.Contains("shop"))
            return "Shop";

        if (text.Contains("travel"))
            return "Travel";

        if (text.Contains("camp"))
            return "Camp";

        return "Unknown";
    }

    public static void ExecuteCommand(GangManager manager, TacticalContext context, string command)
    {
        if (manager == null)
            return;

        string parsed = ParseCommand(command);

        List<GangMember> members = manager.GetAllMembers();

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            ApplyCommandToMember(member, context, parsed);
        }
    }

    public static void ApplyCommandToMember(GangMember member, TacticalContext context, string command)
    {
        if (member == null)
            return;

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
                break;

            case "Travel":
                member.CurrentTask = "Travel";
                member.IsFollowingPlayer = true;
                break;

            case "Camp":
                member.CurrentTask = "Camp Duty";
                break;

            default:
                break;
        }

        if (context != null)
        {
            if (context.IsStealth && command == "Attack")
                member.CombatStyle = "Stealth";

            if (context.IsInCombat && command == "Guard")
                member.BehaviorPriority += 5;
        }
    }

    public static void BroadcastTask(GangManager manager, string task)
    {
        if (manager == null || string.IsNullOrWhiteSpace(task))
            return;

        List<GangMember> members = manager.GetAllMembers();

        for (int i = 0; i < members.Count; i++)
        {
            GangMember member = members[i];
            if (member == null)
                continue;

            member.CurrentTask = task;
        }
    }
}