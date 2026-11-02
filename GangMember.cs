// GangMember.cs
public class GangMember
{
    public string Name { get; set; } = "Member";
    public int Relationship { get; set; }
    public CombatStyle CombatStyle { get; set; } = CombatStyle.RangedMedium;
    public bool IsFollowing { get; set; }

    public void Update()
    {
        if (IsFollowing)
        {
        }
    }
}