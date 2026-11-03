using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class GangSaveData
{
    [DataMember(Order = 1)]
    public List<GangMemberData> Members { get; set; } = new List<GangMemberData>();

    [DataMember(Order = 2)]
    public List<WagonData> Wagons { get; set; } = new List<WagonData>();
}

[DataContract]
public class GangMemberData
{
    [DataMember(Order = 1)]
    public string Name { get; set; }

    [DataMember(Order = 2)]
    public int Relationship { get; set; }

    [DataMember(Order = 3)]
    public int CombatStyle { get; set; }

    [DataMember(Order = 4)]
    public bool IsFollowing { get; set; }
}

[DataContract]
public class WagonData
{
    [DataMember(Order = 1)]
    public string WagonId { get; set; }

    [DataMember(Order = 2)]
    public string Name { get; set; }

    [DataMember(Order = 3)]
    public string OwnerId { get; set; }

    [DataMember(Order = 4)]
    public string HorseId { get; set; }

    [DataMember(Order = 5)]
    public int Type { get; set; }

    [DataMember(Order = 6)]
    public bool IsOwned { get; set; }

    [DataMember(Order = 7)]
    public bool IsHitched { get; set; }

    [DataMember(Order = 8)]
    public bool IsPersistent { get; set; }

    [DataMember(Order = 9)]
    public bool IsDestroyed { get; set; }

    [DataMember(Order = 10)]
    public List<WagonCargoData> Cargo { get; set; } = new List<WagonCargoData>();
}

[DataContract]
public class WagonCargoData
{
    [DataMember(Order = 1)]
    public string ItemName { get; set; }

    [DataMember(Order = 2)]
    public int Amount { get; set; }
}