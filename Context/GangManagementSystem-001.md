<!-- GangManagementSystem.cs lines 1-250 of 1186 -->
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class GangManagementSystem
{
    private readonly Dictionary<string, GangMember> gangMembers =
        new Dictionary<string, GangMember>(StringComparer.Ordinal);
    private readonly Dictionary<string, Hideout> hideouts =
        new Dictionary<string, Hideout>(StringComparer.Ordinal);
    private readonly Dictionary<string, PendingJobReward> pendingRewards =
        new Dictionary<string, PendingJobReward>(StringComparer.Ordinal);
    private readonly Dictionary<string, GangMilestone> milestones =
        new Dictionary<string, GangMilestone>(StringComparer.Ordinal);
    private readonly GangJobPayoutSystem payoutSystem;
    private readonly BankSystem bankSystem;

    public string ActiveHideoutId { get; set; }

    public GangManagementSystem()
        : this(new GangJobPayoutSystem(), new BankSystem())
    {
    }

    public GangManagementSystem(
        GangJobPayoutSystem payoutSystem,
        BankSystem bankSystem)
    {
        this.payoutSystem = payoutSystem ??
            throw new ArgumentNullException(nameof(payoutSystem));
        this.bankSystem = bankSystem ??
            throw new ArgumentNullException(nameof(bankSystem));
    }

    #region Gang Member Management

    public void AddMember(GangMember member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        if (string.IsNullOrWhiteSpace(member.MemberId))
        {
            throw new ArgumentException(
                "A gang member must have a non-empty member ID.",
                nameof(member));
        }

        if (gangMembers.ContainsKey(member.MemberId))
        {
            throw new InvalidOperationException(
                $"Gang member {member.MemberId} already exists.");
        }

        gangMembers.Add(member.MemberId, member);
    }

    // Retains GangManager's upsert behavior for existing callers.
    public void RegisterMember(GangMember member)
    {
        if (member == null || string.IsNullOrWhiteSpace(member.MemberId))
        {
            return;
        }

        gangMembers[member.MemberId] = member;
    }

    public void RemoveMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId) ||
            !gangMembers.Remove(memberId))
        {
            throw new KeyNotFoundException(
                $"Gang member {memberId} not found.");
        }
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
        {
            return null;
        }

        GangMember member;
        return gangMembers.TryGetValue(memberId, out member)
            ? member
            : null;
    }

    public List<GangMember> GetMembers()
    {
        return gangMembers.Values.ToList();
    }

    public List<GangMember> GetAllMembers()
    {
        return GetMembers();
    }

    public List<GangMember> GetMembersByRole(string role)
    {
        return gangMembers.Values
            .Where(member => member != null && member.Role == role)
            .ToList();
    }

    public int GetMemberCount()
    {
        return gangMembers.Count;
    }

    public bool MemberExists(string memberId)
    {
        return !string.IsNullOrWhiteSpace(memberId) &&
            gangMembers.ContainsKey(memberId);
    }

    public void UpdateMemberRole(string memberId, string newRole)
    {
        GangMember member = GetRequiredMember(memberId);
        member.Role = newRole;
    }

    public void UpdateMemberLoyalty(string memberId, int loyaltyChange)
    {
        GangMember member = GetRequiredMember(memberId);
        member.Loyalty = Clamp(member.Loyalty + loyaltyChange, 0, 100);
    }

    public void UpdateMemberTask(string memberId, string task)
    {
        GangMember member = GetRequiredMember(memberId);
        member.CurrentTask = task;
    }

    // Absorbed from GangManager.
    public void ApplyTacticalContext(
        string memberId,
        string combatStyle,
        string currentTask,
        bool followingPlayer)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
        {
            return;
        }

        member.CombatStyle = combatStyle;
        member.CurrentTask = currentTask;
        member.IsFollowingPlayer = followingPlayer;
    }

    private GangMember GetRequiredMember(string memberId)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
        {
            throw new KeyNotFoundException(
                $"Gang member {memberId} not found.");
        }

        return member;
    }

    #endregion

    #region Hideout Management

    public void CreateHideout(
        string hideoutId,
        string locationName,
        float positionX,
        float positionY,
        float positionZ,
        string stage)
    {
        if (string.IsNullOrWhiteSpace(hideoutId))
        {
            throw new ArgumentException(
                "A hideout must have a non-empty ID.",
                nameof(hideoutId));
        }

        if (hideouts.ContainsKey(hideoutId))
        {
            throw new InvalidOperationException(
                $"Hideout {hideoutId} already exists.");
        }

        hideouts.Add(hideoutId, new Hideout
        {
            HideoutId = hideoutId,
            LocationName = locationName,
            PositionX = positionX,
            PositionY = positionY,
            PositionZ = positionZ,
            Stage = stage,
            ChestInventory = CreateEmptyChest(),
            CreatedAtUtc = DateTime.UtcNow
        });
    }

    public Hideout GetHideout(string hideoutId)
    {
        if (string.IsNullOrWhiteSpace(hideoutId))
        {
            return null;
        }

        Hideout hideout;
        return hideouts.TryGetValue(hideoutId, out hideout)
            ? hideout
            : null;
    }

    public List<Hideout> GetAllHideouts()
    {
        return hideouts.Values.ToList();
    }

    public Hideout GetActiveHideout()
    {
        return GetHideout(ActiveHideoutId);
    }

    public void SetActiveHideout(string hideoutId)
    {
        if (!hideouts.ContainsKey(hideoutId))
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }

        ActiveHideoutId = hideoutId;
    }

    public void UpgradeHideoutStage(string hideoutId, string newStage)
    {
        Hideout hideout = GetHideout(hideoutId);
        if (hideout == null)
        {
            throw new KeyNotFoundException(
                $"Hideout {hideoutId} not found.");
        }
```
