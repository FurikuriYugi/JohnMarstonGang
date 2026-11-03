using System;
using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMemberData> Members { get; set; } = new List<GangMemberData>();
    public BankSaveData Bank { get; set; } = new BankSaveData();
    public WagonSaveData Wagons { get; set; } = new WagonSaveData();
    public List<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
    public List<JobAssignment> JobAssignments { get; set; } = new List<JobAssignment>();
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
    public List<BankVaultRecord> VaultRecords { get; set; } = new List<BankVaultRecord>();
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

public class BankVaultRecord
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
    public string Location { get; set; }
    public string EmployerName { get; set; }
    public string Description { get; set; }
    public decimal CashReward { get; set; }
    public decimal GoldReward { get; set; }
    public int HonorChange { get; set; }
    public bool IsIllegal { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public class JobAssignment
{
    public string JobId { get; set; }
    public string AssignedTo { get; set; }
    public DateTime AcceptedAt { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }
}