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
}

public class BankSaveData
{
    public decimal CashOnHand { get; set; }
    public decimal GoldOnHand { get; set; }
    public Dictionary<string, decimal> CashAccounts { get; set; } = new Dictionary<string, decimal>();
    public Dictionary<string, decimal> GoldAccounts { get; set; } = new Dictionary<string, decimal>();
}

public class WagonSaveData
{
    public string WagonId { get; set; }
    public string WagonName { get; set; }
    public string OwnerId { get; set; }
    public WagonType Type { get; set; }
    public Dictionary<string, int> StoredItems { get; set; } = new Dictionary<string, int>();
}