using System;

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public GangMemberRole Role { get; set; }
    public GangCombatStyle CombatStyle { get; set; }
    public GangOrder CurrentOrder { get; set; }

    public float Health { get; set; }
    public float Loyalty { get; set; }
    public float Skill { get; set; }
    public bool IsAlive { get; set; }

    public GangMember()
    {
        MemberId = Guid.NewGuid().ToString("N");
        Name = "Unnamed";
        Role = GangMemberRole.None;
        CombatStyle = GangCombatStyle.RangedShort;
        CurrentOrder = GangOrder.Follow;
        Health = 100f;
        Loyalty = 0.5f;
        Skill = 0.5f;
        IsAlive = true;
    }

    public GangMember(string name) : this()
    {
        Name = name;
    }
}