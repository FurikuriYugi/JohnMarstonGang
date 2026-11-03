using System;
using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMemberData> Members { get; set; } = new List<GangMemberData>();
    public BankSaveData Bank { get; set; } = new BankSaveData();
    public WagonSaveData Wagons { get; set; } = new WagonSaveData();
    public List<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
    public List<JobAssignment> JobAssignments { get; set; } = new List<JobAssignment>();
    public List<CompanionRecordData> Companions { get; set; } = new List<CompanionRecordData>();
}

public class GangMemberData
{
    public string Name { get; set; }
    public string ModelName { get; set; }
    public string Role { get; set; }
    public float Health { get; set; }
    public float Stamina { get; set; }
    public float DeadEye { get; set; }
    public int Honor { get; set; }
    public bool IsAlive { get; set; }
    public bool IsFollowing { get; set; }
    public string CombatStyle { get; set; }
    public List<string> Goals { get; set; } = new List<string>();
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> InventoryItems { get; set; } = new List<string>();
}

public class BankSaveData
{
    public List<BankAccountData> Accounts { get; set; } = new List<BankAccountData>();
    public List<BankVaultRecordData> VaultRecords { get; set; } = new List<BankVaultRecordData>();
}

public class BankAccountData
{
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Debt { get; set; }
    public bool IsSuspicious { get; set; }
}

public class BankVaultRecordData
{
    public string BankName { get; set; }
    public decimal CashStored { get; set; }
    public decimal GoldStored { get; set; }
    public DateTime LastUpdated { get; set; }
}

public class WagonSaveData
{
    public List<WagonData> Wagons { get; set; } = new List<WagonData>();
}

public class WagonData
{
    public string WagonId { get; set; }
    public string ModelName { get; set; }
    public string OwnerName { get; set; }
    public bool IsOwned { get; set; }
    public bool IsPersistent { get; set; }
    public bool IsHookedToHorse { get; set; }
    public List<string> Contents { get; set; } = new List<string>();
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public class JobOffer
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LocationName { get; set; }
    public decimal PaymentCash { get; set; }
    public decimal PaymentGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsCriminal { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public enum JobStatus
{
    Active = 0,
    Completed = 1,
    Failed = 2
}

public class JobAssignment
{
    public string AssignmentId { get; set; }
    public string JobId { get; set; }
    public string Title { get; set; }
    public string GangMemberName { get; set; }
    public JobStatus Status { get; set; }
    public decimal PaymentCash { get; set; }
    public decimal PaymentGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsCriminal { get; set; }
}

public class CompanionRecordData
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Experience { get; set; }
    public string CurrentMood { get; set; }
    public string PreferredCombatStyle { get; set; }
    public string LastKnownWeapon { get; set; }
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
}