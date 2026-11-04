using System.Collections.Generic;

public class SaveRoot
{
    public List<GangMember> GangMembers { get; set; }
    public List<CompanionData> Companions { get; set; }
    public List<BankAccountData> BankAccounts { get; set; }
    public List<JobData> Jobs { get; set; }
    public List<WagonData> Wagons { get; set; }
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