// GangMember.cs
using Newtonsoft.Json;
using RDR2;

public class GangMember
{
    public int PedHandle { get; set; }
    public string Name { get; set; }
    public CombatStyle Style { get; set; }
    public float Loyalty { get; set; }
    public float Skill { get; set; }

    [JsonIgnore]
    public Ped Ped => new Ped(PedHandle);

    public GangMember()
    {
    }

    public GangMember(Ped ped)
    {
        PedHandle = ped.Handle;
        Name = "Recruit";
        Style = CombatStyle.RangedShort;
        Loyalty = 50f;
        Skill = 50f;
    }
}