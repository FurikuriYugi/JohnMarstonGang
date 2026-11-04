using System.Collections.Generic;

public class SaveRoot
{
    public List<GangMember> GangMembers { get; set; } = new List<GangMember>();
    public List<CompanionRecord> Companions { get; set; } = new List<CompanionRecord>();
    public List<WagonRecord> Wagons { get; set; } = new List<WagonRecord>();
    public List<BankAccountRecord> BankAccounts { get; set; } = new List<BankAccountRecord>();
    public List<JobRecord> Jobs { get; set; } = new List<JobRecord>();
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