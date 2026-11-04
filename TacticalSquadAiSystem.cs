using System.Collections.Generic;
using Newtonsoft.Json;

public class TacticalSquadAiSystem
{
    public void UpdateSquadTactics(List<GangMember> members, TacticalContext context)
    {
        if (members == null || context == null)
            return;

        foreach (GangMember member in members)
        {
            if (member == null)
                continue;

            ApplyTactic(member, context);
        }
    }

    public void ApplyTactic(GangMember member, TacticalContext context)
    {
        if (member == null || context == null)
            return;

        if (context.IsStealth)
        {
            member.CombatStyle = "Stealth";
            member.TacticalState = "Careful";
            member.CurrentTask = "Hold position and avoid detection";
            return;
        }

        if (context.IsInCombat)
        {
            member.TacticalState = "Combat";
            member.CurrentTask = "Engage threats";

            if (member.BehaviorPriority >= 75)
                member.CombatStyle = "Ranged Medium";
            else if (member.BehaviorPriority >= 50)
                member.CombatStyle = "Ranged Short";
            else
                member.CombatStyle = "Lethal Melee";

            return;
        }

        if (context.IsShopping)
        {
            member.TacticalState = "Support";
            member.CurrentTask = "Assist player with errands";
            member.CombatStyle = "Ranged Short";
            return;
        }

        if (context.IsTraveling)
        {
            member.TacticalState = "Follow";
            member.CurrentTask = "Stay close to the player";
            return;
        }

        member.TacticalState = "Idle";
        member.CurrentTask = "Wait";
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(new TacticalContext(), Formatting.Indented);
    }

    public TacticalContext LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return new TacticalContext();

        TacticalContext context = JsonConvert.DeserializeObject<TacticalContext>(json);
        return context ?? new TacticalContext();
    }
}

public class TacticalContext
{
    public bool IsInCombat { get; set; }
    public bool IsStealth { get; set; }
    public bool IsTraveling { get; set; }
    public bool IsShopping { get; set; }
    public string CurrentObjective { get; set; }
}