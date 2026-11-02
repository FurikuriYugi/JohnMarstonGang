// PersistenceModels.cs
using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class GangSaveData
{
    [DataMember]
    public List<GangMemberData> Members { get; set; } = new List<GangMemberData>();

    [DataMember]
    public BankSaveData Bank { get; set; } = new BankSaveData();
}

[DataContract]
public class GangMemberData
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public int Relationship { get; set; }

    [DataMember]
    public int CombatStyle { get; set; }

    [DataMember]
    public bool IsFollowing { get; set; }
}

[DataContract]
public class BankSaveData
{
    [DataMember]
    public decimal PlayerCash { get; set; }

    [DataMember]
    public decimal PlayerGold { get; set; }

    [DataMember]
    public List<BankAccountData> Accounts { get; set; } = new List<BankAccountData>();

    [DataMember]
    public List<BankTransactionData> Transactions { get; set; } = new List<BankTransactionData>();
}

[DataContract]
public class BankAccountData
{
    [DataMember]
    public string OwnerId { get; set; }

    [DataMember]
    public string OwnerName { get; set; }

    [DataMember]
    public decimal Cash { get; set; }

    [DataMember]
    public decimal Gold { get; set; }

    [DataMember]
    public bool IsBusinessAccount { get; set; }
}

[DataContract]
public class BankTransactionData
{
    [DataMember]
    public string TransactionId { get; set; }

    [DataMember]
    public string FromId { get; set; }

    [DataMember]
    public string ToId { get; set; }

    [DataMember]
    public decimal CashAmount { get; set; }

    [DataMember]
    public decimal GoldAmount { get; set; }

    [DataMember]
    public string Reason { get; set; }
}