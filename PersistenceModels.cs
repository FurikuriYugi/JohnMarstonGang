using System;
using System.Collections.Generic;

public class SaveRoot
{
    public int SaveVersion { get; set; } = 2;
    public WorldState World { get; set; } = new WorldState();
    public List<GangMember> GangMembers { get; set; } = new List<GangMember>();
    public List<BankAccountData> BankAccounts { get; set; } = new List<BankAccountData>();
}

public class WorldState
{
    public List<CompanionData> Companions { get; set; } = new List<CompanionData>();
    public List<JobData> ActiveJobs { get; set; } = new List<JobData>();
    public List<WagonData> Wagons { get; set; } = new List<WagonData>();
    public List<WeaponPlatformData> WeaponPlatforms { get; set; } = new List<WeaponPlatformData>();
    public List<FactionRelationData> FactionRelations { get; set; } = new List<FactionRelationData>();
    public List<LocationStateData> Locations { get; set; } = new List<LocationStateData>();
    public List<EntityStateData> Entities { get; set; } = new List<EntityStateData>();
}

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string CombatStyle { get; set; }
    public string CurrentTask { get; set; }
    public int Loyalty { get; set; }
    public int CombatSkill { get; set; }
    public int SupportSkill { get; set; }
    public int StealthSkill { get; set; }
    public int Hunger { get; set; }
    public bool IsFollowingPlayer { get; set; }
    public List<string> InventoryItems { get; set; } = new List<string>();
}

public class CompanionData
{
    public string CompanionId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public bool IsHuman { get; set; }
    public string Role { get; set; }
    public string Personality { get; set; }
    public int Loyalty { get; set; }
    public int SkillCombat { get; set; }
    public int SkillStealth { get; set; }
    public int SkillLooting { get; set; }
    public int SkillSupport { get; set; }
    public string CurrentCombatStyle { get; set; }
    public string CurrentTask { get; set; }
    public List<string> InventoryItems { get; set; } = new List<string>();
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Goals { get; set; } = new List<string>();
    public bool IsRecruitable { get; set; }
    public bool IsFollowingPlayer { get; set; }
}

public class JobData
{
    public string JobId { get; set; }
    public string JobType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SponsorId { get; set; }
    public string TargetId { get; set; }
    public string LocationId { get; set; }
    public decimal RewardCash { get; set; }
    public decimal RewardGold { get; set; }
    public int HonorChange { get; set; }
    public bool IsIllegal { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }
    public List<string> Objectives { get; set; } = new List<string>();
    public List<string> Participants { get; set; } = new List<string>();
    public bool IsAvailable { get; set; }
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
    public DateTime LastTransactionUtc { get; set; }
}

public class WagonData
{
    public string WagonId { get; set; }
    public string OwnerId { get; set; }
    public string DisplayName { get; set; }
    public bool IsOwned { get; set; }
    public bool IsStoredNearPlayer { get; set; }
    public bool IsHitchable { get; set; } = true;
    public List<string> StoredItems { get; set; } = new List<string>();
    public List<string> AttachedHorses { get; set; } = new List<string>();
    public List<string> AttachedWeapons { get; set; } = new List<string>();
}

public class WeaponPlatformData
{
    public string PlatformId { get; set; }
    public string PlatformType { get; set; }
    public string OwnerId { get; set; }
    public bool IsPortable { get; set; }
    public bool RequiresGunner { get; set; }
    public bool RequiresAssistant { get; set; }
    public int ReloadCooldownSeconds { get; set; }
    public string CurrentGunnerId { get; set; }
    public string CurrentAssistantId { get; set; }
    public string CurrentAmmoType { get; set; }
}

public class FactionRelationData
{
    public string FactionA { get; set; }
    public string FactionB { get; set; }
    public int Trust { get; set; }
    public int Fear { get; set; }
    public int Respect { get; set; }
    public int Suspicion { get; set; }
    public bool AtWar { get; set; }
}

public class LocationStateData
{
    public string LocationId { get; set; }
    public string LocationName { get; set; }
    public bool IsRobbed { get; set; }
    public bool IsDestroyed { get; set; }
    public decimal StoredCash { get; set; }
    public decimal StoredGold { get; set; }
    public List<string> UnlockedDoors { get; set; } = new List<string>();
    public List<string> DestroyedObjects { get; set; } = new List<string>();
}

public class EntityStateData
{
    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public string DisplayName { get; set; }
    public bool IsAlive { get; set; }
    public bool IsHostile { get; set; }
    public bool IsRecruitable { get; set; }
    public string CurrentState { get; set; }
    public string HomeLocationId { get; set; }
    public List<string> InventoryItems { get; set; } = new List<string>();
}

public class LockRecord
{
    public string TargetId { get; set; }
    public bool IsLocked { get; set; }
    public bool IsBroken { get; set; }
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