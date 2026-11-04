using System.Collections.Generic;

public class GangMember
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CompanionType { get; set; }
    public string Role { get; set; }
    public string Alignment { get; set; }
    public string CombatStyle { get; set; }
    public string TacticalState { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public int Hunger { get; set; } = 100;
    public int Health { get; set; } = 100;
    public int BehaviorPriority { get; set; }
    public string ModelName { get; set; }
    public List<string> Weapons { get; set; } = new List<string>();
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
}