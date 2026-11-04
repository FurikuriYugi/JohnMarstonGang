using System.Collections.Generic;

public class SaveRoot
{
    public List<GangMember> GangMembers { get; set; } = new List<GangMember>();
    public List<CompanionData> Companions { get; set; } = new List<CompanionData>();
    public List<BankAccountData> BankAccounts { get; set; } = new List<BankAccountData>();
    public List<JobData> Jobs { get; set; } = new List<JobData>();
    public List<WagonData> Wagons { get; set; } = new List<WagonData>();
}

public class BankAccountRecord
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Debt { get; set; }
    public bool IsSuspicious { get; set; }
}

public class BankAccountData
{
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Debt { get; set; }
    public bool IsSuspicious { get; set; }
}

public class JobRecord
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Faction { get; set; }
    public decimal PayoutCash { get; set; }
    public decimal PayoutGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsCompleted { get; set; }
}

public class JobData
{
    public string JobId { get; set; }
    public string Title { get; set; }
    public string Faction { get; set; }
    public decimal PayoutCash { get; set; }
    public decimal PayoutGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsCompleted { get; set; }
}

public class CompanionData
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Role { get; set; }
    public string CombatStyle { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Skills { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public List<string> Inventory { get; set; } = new List<string>();
}

public class WagonData
{
    public string WagonId { get; set; }
    public string OwnerId { get; set; }
    public bool IsOwned { get; set; }
    public bool IsHitched { get; set; }
    public List<string> StoredItems { get; set; } = new List<string>();
    public List<string> AttachedHorses { get; set; } = new List<string>();
}