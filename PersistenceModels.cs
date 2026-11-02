using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMember> Members { get; set; } = new List<GangMember>();
    public GangOrder CurrentOrder { get; set; } = GangOrder.Follow;
    public GangCombatStyle CurrentCombatStyle { get; set; } = GangCombatStyle.RangedShort;
}