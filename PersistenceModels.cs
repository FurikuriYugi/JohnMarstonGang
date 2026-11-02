// PersistenceModels.cs
using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class GangSaveData
{
    [DataMember] public List<GangMemberData> Members { get; set; } = new List<GangMemberData>();
}

[DataContract]
public class GangMemberData
{
    [DataMember] public string Name { get; set; }
    [DataMember] public int Relationship { get; set; }
    [DataMember] public int CombatStyle { get; set; }
    [DataMember] public bool IsFollowing { get; set; }
}

[DataContract]
public class BankSaveData
{
    [DataMember] public List<BankAccountData> Accounts { get; set; } = new List<BankAccountData>();
}

[DataContract]
public class BankAccountData
{
    [DataMember] public string OwnerId { get; set; }
    [DataMember] public decimal Cash { get; set; }
    [DataMember] public decimal Gold { get; set; }
}