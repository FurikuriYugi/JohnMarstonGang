// TacticalSquadAiSystem.cs
using System;
using System.Collections.Generic;

public class TacticalSquadAiSystem
{
    private readonly Random _random = new Random();

    public void UpdateGangTactics(IEnumerable<GangMember> members, TacticalContext context)
    {
        if (members == null) return;

        foreach (var member in members)
        {
            if (member == null) continue;
            UpdateMemberTactics(member, context);
        }
    }

    public void UpdateMemberTactics(GangMember member, TacticalContext context)
    {
        if (member == null) return;

        var desiredStyle = DetermineCombatStyle(member, context);
        member.CombatStyle = desiredStyle;
        member.IsFollowing = ShouldFollowPlayer(context);
    }

    public CombatStyle DetermineCombatStyle(GangMember member, TacticalContext context)
    {
        if (context == null)
            return member.CombatStyle;

        if (context.IsStealthMode)
        {
            if (member.Relationship >= 50)
                return CombatStyle.Stealth;

            return CombatStyle.NonLethalMelee;
        }

        if (context.IsCloseQuarters)
        {
            if (context.IsHostileNearby)
                return member.Relationship < 0 ? CombatStyle.LethalMelee : CombatStyle.RangedShort;

            return CombatStyle.RangedShort;
        }

        if (context.IsLongRangeCombat)
            return CombatStyle.RangedLong;

        if (context.IsMediumRangeCombat)
            return CombatStyle.RangedMedium;

        if (context.IsShoppingOrTown)
            return CombatStyle.RangedShort;

        return member.CombatStyle;
    }

    public bool ShouldFollowPlayer(TacticalContext context)
    {
        if (context == null)
            return true;

        if (context.IsShoppingOrTown)
            return true;

        if (context.IsStealthMode)
            return true;

        if (context.IsCombatActive)
            return true;

        return context.FollowMode;
    }

    public GangRole DetermineRole(GangMember member, TacticalContext context)
    {
        if (member == null)
            return GangRole.Follower;

        if (context != null && context.IsCombatActive)
        {
            if (member.CombatStyle == CombatStyle.RangedLong)
                return GangRole.Support;
            if (member.CombatStyle == CombatStyle.Stealth || member.CombatStyle == CombatStyle.NonLethalMelee)
                return GangRole.Scout;
            if (member.CombatStyle == CombatStyle.LethalMelee)
                return GangRole.Brawler;

            return GangRole.Follower;
        }

        if (context != null && context.IsShoppingOrTown)
            return GangRole.Supplier;

        return GangRole.Follower;
    }

    public TacticalOrder DetermineOrder(TacticalContext context)
    {
        if (context == null)
            return TacticalOrder.Follow;

        if (context.IsStealthMode)
            return TacticalOrder.HoldAndStealth;

        if (context.IsCombatActive)
            return TacticalOrder.Assault;

        if (context.IsShoppingOrTown)
            return TacticalOrder.HoldPosition;

        if (context.IsTraveling)
            return TacticalOrder.Follow;

        return TacticalOrder.Follow;
    }

    public void ApplyFormationSpacing(IEnumerable<GangMember> members, TacticalContext context)
    {
        if (members == null || context == null)
            return;

        var index = 0;
        foreach (var member in members)
        {
            if (member == null) continue;
            var role = DetermineRole(member, context);

            if (role == GangRole.Scout)
                member.IsFollowing = true;
            else if (role == GangRole.Supplier)
                member.IsFollowing = false;
            else if (role == GangRole.Support)
                member.IsFollowing = true;

            index++;
        }
    }

    public CombatStyle SelectStyleForThreatLevel(int threatLevel, bool stealthPreferred)
    {
        if (stealthPreferred)
            return threatLevel > 5 ? CombatStyle.Stealth : CombatStyle.NonLethalMelee;

        if (threatLevel <= 2)
            return CombatStyle.RangedShort;

        if (threatLevel <= 4)
            return CombatStyle.RangedMedium;

        if (threatLevel <= 6)
            return CombatStyle.RangedLong;

        return CombatStyle.RangedLong;
    }

    public string GetBehaviorLabel(CombatStyle style)
    {
        switch (style)
        {
            case CombatStyle.NonLethalMelee: return "NonLethalMelee";
            case CombatStyle.LethalMelee: return "LethalMelee";
            case CombatStyle.RangedShort: return "RangedShort";
            case CombatStyle.RangedMedium: return "RangedMedium";
            case CombatStyle.RangedLong: return "RangedLong";
            case CombatStyle.Stealth: return "Stealth";
            default: return "Unknown";
        }
    }

    public TacticalContext BuildContext(
        bool isCombatActive,
        bool isHostileNearby,
        bool isStealthMode,
        bool isShoppingOrTown,
        bool isTraveling,
        bool isCloseQuarters,
        bool isMediumRangeCombat,
        bool isLongRangeCombat,
        bool followMode)
    {
        return new TacticalContext
        {
            IsCombatActive = isCombatActive,
            IsHostileNearby = isHostileNearby,
            IsStealthMode = isStealthMode,
            IsShoppingOrTown = isShoppingOrTown,
            IsTraveling = isTraveling,
            IsCloseQuarters = isCloseQuarters,
            IsMediumRangeCombat = isMediumRangeCombat,
            IsLongRangeCombat = isLongRangeCombat,
            FollowMode = followMode
        };
    }

    public bool TryChooseSupportAction(TacticalContext context, out SupportAction action)
    {
        action = SupportAction.None;

        if (context == null)
            return false;

        if (context.IsShoppingOrTown)
        {
            action = SupportAction.Supplier;
            return true;
        }

        if (context.IsCombatActive && context.IsStealthMode)
        {
            action = SupportAction.Suppress;
            return true;
        }

        if (context.IsCombatActive)
        {
            action = SupportAction.Cover;
            return true;
        }

        if (context.IsTraveling)
        {
            action = SupportAction.Regroup;
            return true;
        }

        return false;
    }

    public int GetSpacingOffset(GangMember member, TacticalContext context)
    {
        if (member == null)
            return 0;

        var baseOffset = 2;

        if (context != null && context.IsStealthMode)
            baseOffset = 4;

        if (member.CombatStyle == CombatStyle.RangedLong)
            baseOffset += 2;

        if (member.CombatStyle == CombatStyle.LethalMelee || member.CombatStyle == CombatStyle.NonLethalMelee)
            baseOffset -= 1;

        return Math.Max(1, baseOffset);
    }

    public bool ShouldUseStealthWeapon(CombatStyle style)
    {
        return style == CombatStyle.Stealth || style == CombatStyle.NonLethalMelee;
    }

    public int GetAggressionScore(GangMember member, TacticalContext context)
    {
        if (member == null)
            return 0;

        var score = 0;

        if (member.Relationship < 0)
            score += 2;
        if (member.Relationship > 50)
            score += 1;

        if (context != null)
        {
            if (context.IsCombatActive) score += 2;
            if (context.IsStealthMode) score -= 1;
            if (context.IsShoppingOrTown) score -= 1;
            if (context.IsHostileNearby) score += 1;
        }

        if (member.CombatStyle == CombatStyle.RangedLong) score += 1;
        if (member.CombatStyle == CombatStyle.LethalMelee) score += 2;

        return score;
    }
}

public class TacticalContext
{
    public bool IsCombatActive { get; set; }
    public bool IsHostileNearby { get; set; }
    public bool IsStealthMode { get; set; }
    public bool IsShoppingOrTown { get; set; }
    public bool IsTraveling { get; set; }
    public bool IsCloseQuarters { get; set; }
    public bool IsMediumRangeCombat { get; set; }
    public bool IsLongRangeCombat { get; set; }
    public bool FollowMode { get; set; } = true;
}

public enum GangRole
{
    Follower = 0,
    Scout = 1,
    Support = 2,
    Supplier = 3,
    Brawler = 4
}

public enum TacticalOrder
{
    Follow = 0,
    HoldPosition = 1,
    HoldAndStealth = 2,
    Assault = 3,
    Regroup = 4
}

public enum SupportAction
{
    None = 0,
    Cover = 1,
    Suppress = 2,
    Regroup = 3,
    Supplier = 4
}