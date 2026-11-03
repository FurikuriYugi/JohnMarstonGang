// PersistenceModels.cs
using System;
using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMemberSaveData> Members { get; set; } = new List<GangMemberSaveData>();
    public BankSaveData Bank { get; set; } = new BankSaveData();
    public List<WagonSaveData> Wagons { get; set; } = new List<WagonSaveData>();
    public List<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
    public List<JobAssignment> JobAssignments { get; set; } = new List<JobAssignment>();
    public List<CompanionSaveData> Companions { get; set; } = new List<CompanionSaveData>();
}

public class GangMemberSaveData
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public GangMemberRole Role { get; set; }
    public CombatStyle PreferredCombatStyle { get; set; }
    public GangMood Mood { get; set; }
    public int Loyalty { get; set; }
    public int SkillCombat { get; set; }
    public int SkillStealth { get; set; }
    public int SkillHandling { get; set; }
    public int SkillTrading { get; set; }
    public string WeaponChoice { get; set; }
    public string Outfit { get; set; }
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public bool FollowingPlayer { get; set; }
    public bool IsActive { get; set; }
    public bool IsInCombat { get; set; }
    public bool IsStealthing { get; set; }
    public bool IsShopping { get; set; }
    public bool IsOnJob { get; set; }
    public bool NeedsSupplies { get; set; }
}

public class BankSaveData
{
    public List<BankAccountSaveData> Accounts { get; set; } = new List<BankAccountSaveData>();
    public decimal VaultCash { get; set; }
    public decimal VaultGold { get; set; }
    public decimal TotalDepositedCash { get; set; }
    public decimal TotalDepositedGold { get; set; }
}

public class BankAccountSaveData
{
    public string AccountId { get; set; }
    public string OwnerId { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public bool IsSuspicious { get; set; }
    public decimal LifetimeDepositedCash { get; set; }
    public decimal LifetimeDepositedGold { get; set; }
}

public class WagonSaveData
{
    public string WagonId { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public WagonType Type { get; set; }
    public bool IsOwned { get; set; }
    public bool IsPersistent { get; set; }
    public List<WagonInventoryItemSaveData> Inventory { get; set; } = new List<WagonInventoryItemSaveData>();
}

public class WagonInventoryItemSaveData
{
    public string ItemName { get; set; }
    public int Amount { get; set; }
}

public class CompanionSaveData
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public int Trust { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public GangMood CurrentMood { get; set; }
    public CombatStyle PreferredCombatStyle { get; set; }
    public string LastKnownWeapon { get; set; }
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
}