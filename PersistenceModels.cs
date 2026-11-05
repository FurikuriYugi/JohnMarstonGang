using System;
using System.Collections.Generic;

public class SaveRoot
{
    public int SaveVersion { get; set; } = 5;
    public WorldState World { get; set; } = new WorldState();
    public List<GangMember> GangMembers { get; set; } = new List<GangMember>();
    public List<BankAccountData> BankAccounts { get; set; } = new List<BankAccountData>();
    public List<WorldEntityData> WorldEntities { get; set; } = new List<WorldEntityData>();
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
    public List<CommandData> Commands { get; set; } = new List<CommandData>();
    public List<LockRecord> Locks { get; set; } = new List<LockRecord>();
    public List<DestructibleRecord> Destructibles { get; set; } = new List<DestructibleRecord>();
    public List<TacticalOrderData> TacticalOrders { get; set; } = new List<TacticalOrderData>();
    public List<BankVaultData> BankVaults { get; set; } = new List<BankVaultData>();
    public List<TrainLootData> TrainLoot { get; set; } = new List<TrainLootData>();
    public List<StageCoachLootData> StageCoachLoot { get; set; } = new List<StageCoachLootData>();
    public List<ObjectiveStateData> ObjectiveStates { get; set; } = new List<ObjectiveStateData>();
    public List<CompanionAIStateData> CompanionAIStates { get; set; } = new List<CompanionAIStateData>();
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

public class CompanionAIStateData
{
    public string CompanionId { get; set; }
    public string Personality { get; set; }
    public int Morale { get; set; }
    public int Fatigue { get; set; }
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    public bool PrefersStealth { get; set; }
    public string CurrentGoal { get; set; }
    public long LastActivityTimestamp { get; set; }
    public int ExperiencePoints { get; set; }
    public Dictionary<string, int> SkillProgression { get; set; } = new Dictionary<string, int>();
    public List<string> CompletedJobs { get; set; } = new List<string>();
    public decimal TotalEarnings { get; set; }
    public int BanksVisited { get; set; }
    public int DepositsWithdrawn { get; set; }
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

public class BankVaultData
{
    public string VaultId { get; set; }
    public string BankId { get; set; }
    public string LocationId { get; set; }
    public decimal StoredCash { get; set; }
    public decimal StoredGold { get; set; }
    public decimal RobbedCash { get; set; }
    public decimal RobbedGold { get; set; }
    public int SuspicionLevel { get; set; }
    public bool HasBeenRobbed { get; set; }
}

public class TrainLootData
{
    public string TrainId { get; set; }
    public string LocationId { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public int JewelCount { get; set; }
    public List<string> Papers { get; set; } = new List<string>();
    public bool IsLocked { get; set; }
    public bool IsCracked { get; set; }
    public bool IsBlasted { get; set; }
}

public class StageCoachLootData
{
    public string CoachId { get; set; }
    public string LocationId { get; set; }
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public int JewelCount { get; set; }
    public List<string> Papers { get; set; } = new List<string>();
    public bool IsLocked { get; set; }
    public bool IsCracked { get; set; }
    public bool IsBlasted { get; set; }
}

public class ObjectiveStateData
{
    public string ObjectiveId { get; set; }
    public string RelatedJobId { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }
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

public class WorldEntityData
{
    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public string DisplayName { get; set; }
    public string FactionId { get; set; }
    public string LocationId { get; set; }
    public bool IsAlive { get; set; }
    public bool IsHostile { get; set; }
    public bool IsRecruitable { get; set; }
    public bool IsFollowingPlayer { get; set; }
    public string CurrentTask { get; set; }
    public string CombatStyle { get; set; }
    public int Loyalty { get; set; }
    public int CombatSkill { get; set; }
    public int StealthSkill { get; set; }
    public int SupportSkill { get; set; }
    public List<string> InventoryItems { get; set; } = new List<string>();
    public List<string> KnownGoals { get; set; } = new List<string>();
}

public class TacticalOrderData
{
    public string OrderId { get; set; }
    public string SquadId { get; set; }
    public string IssuerId { get; set; }
    public string TargetId { get; set; }
    public string CommandType { get; set; }
    public string CombatStyle { get; set; }
    public string CurrentTask { get; set; }
    public bool IsStealthMode { get; set; }
    public bool IsFollowMode { get; set; }
    public bool IsCompleted { get; set; }
    public List<string> AssignedMemberIds { get; set; } = new List<string>();
    public List<string> Notes { get; set; } = new List<string>();
}

public class CommandData
{
    public string CommandId { get; set; }
    public string IssuerId { get; set; }
    public string SquadId { get; set; }
    public string TargetId { get; set; }
    public string CommandType { get; set; }
    public string CombatStyle { get; set; }
    public string CurrentTask { get; set; }
    public bool IsStealthMode { get; set; }
    public bool IsFollowMode { get; set; }
    public bool IsCompleted { get; set; }
    public string LinkedOrderId { get; set; }
    public List<string> AssignedMemberIds { get; set; } = new List<string>();
    public List<string> Notes { get; set; } = new List<string>();
}

public class LockRecord
{
    public string LockId { get; set; }
    public string LocationId { get; set; }
    public string LockType { get; set; }
    public string LockName { get; set; }
    public bool IsLocked { get; set; }
    public bool IsPickedOpen { get; set; }
    public bool IsBlownOpen { get; set; }
    public int DifficultyRating { get; set; }
    public DateTime LastAttemptUtc { get; set; }
    public int FailedAttempts { get; set; }
}

public class DestructibleRecord
{
    public string DestructibleId { get; set; }
    public string LocationId { get; set; }
    public string DestructibleName { get; set; }
    public string DestructibleType { get; set; }
    public bool IsDestroyed { get; set; }
    public int HealthPoints { get; set; }
    public int MaxHealthPoints { get; set; }
    public bool CanBeBlasted { get; set; }
    public int BlastResistance { get; set; }
    public DateTime LastDamagedUtc { get; set; }
}