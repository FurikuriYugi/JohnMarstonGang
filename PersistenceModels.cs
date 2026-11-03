// PersistenceModels.cs
using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMemberSaveData> Members { get; set; }
    public BankSaveData Bank { get; set; }
    public List<WagonSaveData> Wagons { get; set; }
    public List<JobOffer> JobOffers { get; set; }
    public List<JobAssignment> JobAssignments { get; set; }
    public List<CompanionSaveData> Companions { get; set; }
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
    public decimal VaultCash { get; set; }
    public decimal VaultGold { get; set; }
    public Dictionary<string, decimal> CashAccounts { get; set; } = new Dictionary<string, decimal>();
    public Dictionary<string, decimal> GoldAccounts { get; set; } = new Dictionary<string, decimal>();
}

public class WagonSaveData
{
    public List<WagonSaveEntry> Wagons { get; set; } = new List<WagonSaveEntry>();
}

public class WagonSaveEntry
{
    public string WagonId { get; set; }
    public string WagonName { get; set; }
    public string OwnerId { get; set; }
    public WagonType Type { get; set; }
    public Dictionary<string, int> StoredItems { get; set; } = new Dictionary<string, int>();
}

public class CompanionRecord
{
    public string Name { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public CombatStyle PreferredStyle { get; set; }
    public string LastKnownMood { get; set; }
}

public class CompanionSaveData
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public CompanionType Type { get; set; }
    public string Personality { get; set; }
    public int Loyalty { get; set; }
    public int SkillCombat { get; set; }
    public int SkillStealth { get; set; }
    public int SkillHandling { get; set; }
    public int SkillTrading { get; set; }
    public string WeaponChoice { get; set; }
    public string Outfit { get; set; }
    public decimal PersonalCash { get; set; }
    public decimal PersonalGold { get; set; }
    public List<string> Memories { get; set; }
    public List<string> Goals { get; set; }
}