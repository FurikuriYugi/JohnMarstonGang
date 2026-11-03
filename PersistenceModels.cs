using System.Collections.Generic;

public class GangSaveData
{
    public List<GangMemberSaveData> Members { get; set; }
    public BankSaveData Bank { get; set; }
    public WagonSaveData Wagons { get; set; }
    public List<JobOffer> JobOffers { get; set; }
    public List<JobAssignment> JobAssignments { get; set; }
    public List<CompanionSaveData> Companions { get; set; }
}

public class GangMemberSaveData
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType CompanionType { get; set; }
    public GangMemberRole Role { get; set; }

    public bool IsActive { get; set; }
    public bool IsShopping { get; set; }
    public bool IsStealthing { get; set; }
    public bool IsInCombatMode { get; set; }

    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Honor { get; set; }
    public int Money { get; set; }
    public int Experience { get; set; }

    public GangMode CurrentGangMode { get; set; }
    public GangTaskType CurrentTask { get; set; }
    public string CurrentTargetId { get; set; }
    public string CurrentTaskDescription { get; set; }

    public List<string> KnownGoals { get; set; }
    public List<string> Memories { get; set; }
    public List<string> Skills { get; set; }
    public string CurrentWeaponPreference { get; set; }
}

public class BankSaveData
{
    public List<BankAccountSaveData> Accounts { get; set; }
    public List<BankVaultSaveData> Vaults { get; set; }
    public List<BusinessLedgerSaveData> BusinessLedgers { get; set; }
}

public class BankAccountSaveData
{
    public string OwnerId { get; set; }
    public int Cash { get; set; }
    public int Gold { get; set; }
    public int Trust { get; set; }
    public int Suspicion { get; set; }
}

public class BankVaultSaveData
{
    public string BankName { get; set; }
    public int StoredCash { get; set; }
    public int StoredGold { get; set; }
    public int RecentWithdrawals { get; set; }
}

public class BusinessLedgerSaveData
{
    public string OwnerId { get; set; }
    public string BusinessName { get; set; }
    public int CashOnHand { get; set; }
    public int GoldOnHand { get; set; }
}

public class WagonSaveData
{
    public List<WagonRecord> Wagons { get; set; }
}

public class CompanionSaveData
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Experience { get; set; }
    public List<string> Goals { get; set; }
    public List<string> Memories { get; set; }
}

public enum GangMemberRole
{
    Leader,
    Enforcer,
    Scout,
    Support,
    Worker
}

public enum GangMode
{
    Default,
    Stealth,
    Combat,
    Shopping,
    Escort,
    Job
}

public enum GangTaskType
{
    None,
    FollowPlayer,
    Guard,
    Shop,
    GatherSupplies,
    Escort,
    Attack,
    StealthMove,
    Transport
}

public enum CompanionType
{
    Human,
    Animal
}