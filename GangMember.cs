public class GangMember
{
    public string Name { get; set; } = "Member";
    public int Relationship { get; set; }
    public CombatStyle CombatStyle { get; set; } = CombatStyle.RangedMedium;
    public bool IsFollowing { get; set; }

    public string AssignedWagonId { get; set; }
    public WagonRole WagonRole { get; set; } = WagonRole.None;

    public void AssignToWagon(string wagonId, WagonRole role)
    {
        AssignedWagonId = wagonId;
        WagonRole = role;
    }

    public void ClearWagonAssignment()
    {
        AssignedWagonId = null;
        WagonRole = WagonRole.None;
    }

    public void Update()
    {
        if (IsFollowing)
        {
        }
    }
}

public enum WagonRole
{
    None = 0,
    Driver = 1,
    Gunner = 2,
    Loader = 3,
    Guard = 4
}