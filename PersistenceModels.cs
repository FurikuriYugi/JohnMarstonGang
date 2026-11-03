using System;
using System.Collections.Generic;

public class PlayerGangSaveData
{
    public int Version { get; set; } = 1;
    public string PlayerName { get; set; } = string.Empty;
    public decimal Money { get; set; }
    public decimal Gold { get; set; }
    public List<GangMemberSaveData> GangMembers { get; set; } = new List<GangMemberSaveData>();
    public List<WagonSaveData> Wagons { get; set; } = new List<WagonSaveData>();
    public BankSaveData Bank { get; set; } = new BankSaveData();
    public List<JobSaveData> ActiveJobs { get; set; } = new List<JobSaveData>();
    public List<WorldFactionRecordSaveData> WorldRecords { get; set; } = new List<WorldFactionRecordSaveData>();
}

public class GangMemberSaveData
{
    public string Name { get; set; } = string.Empty;
    public CompanionType CompanionType { get; set; }
    public GangMemberRole Role { get; set; }
    public RelationshipAlignment Alignment { get; set; }
    public CombatStyle CombatStyle { get; set; }
    public TacticalState TacticalState { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public List<string> Weapons { get; set; } = new List<string>();
    public List<string> MemoryTags { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
}

public class WagonSaveData
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ModelName { get; set; } = string.Empty;
    public bool Owned { get; set; }
    public bool Hitchable { get; set; } = true;
    public float Health { get; set; } = 100f;
    public List<string> InventoryItems { get; set; } = new List<string>();
}

public class BankSaveData
{
    public decimal VaultCash { get; set; }
    public decimal VaultGold { get; set; }
    public decimal PlayerDepositCash { get; set; }
    public decimal PlayerDepositGold { get; set; }
    public List<BankAccountSaveData> Accounts { get; set; } = new List<BankAccountSaveData>();
}

public class BankAccountSaveData
{
    public string OwnerId { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public int Suspicion { get; set; }
    public int Reputation { get; set; }
}

public class JobSaveData
{
    public string JobId { get; set; } = Guid.NewGuid().ToString("N");
    public JobType JobType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PayoutCash { get; set; }
    public decimal PayoutGold { get; set; }
    public int HonorChange { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}

public class WorldFactionRecordSaveData
{
    public string FactionName { get; set; } = string.Empty;
    public int Trust { get; set; }
    public int Fear { get; set; }
    public int Suspicion { get; set; }
}