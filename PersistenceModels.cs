using System.Collections.Generic;

public class SaveRoot
{
    public List<GangMember> GangMembers { get; set; } = new List<GangMember>();
    public List<BankAccountRecord> BankAccounts { get; set; } = new List<BankAccountRecord>();
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

public class LockRecord
{
    public string TargetId { get; set; }
    public string TargetType { get; set; }
    public bool IsLocked { get; set; }
    public bool IsBroken { get; set; }
    public bool CanBePicked { get; set; }
    public bool CanBeBlasted { get; set; }
}

public class DestructibleRecord
{
    public string TargetId { get; set; }
    public string TargetType { get; set; }
    public bool IsDestructible { get; set; }
    public bool IsDestroyed { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public bool CanBeBlasted { get; set; }
    public bool CanBePicked { get; set; }
}