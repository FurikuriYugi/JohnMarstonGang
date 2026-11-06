using System;
using System.Collections.Generic;

public sealed class GangBankAccountProgressionState
{
    public bool GangAccountUnlocked { get; set; }

    public Dictionary<string, int> BankerInteractionCounts { get; set; }
        = new Dictionary<string, int>();

    public Dictionary<string, int> BankerTrustScores { get; set; }
        = new Dictionary<string, int>();

    public HashSet<string> CompletedStoryMilestones { get; set; }
        = new HashSet<string>();
}

public sealed class GangBankAccountProgression
{
    private readonly HashSet<string> _bankerNpcIds;
    private readonly HashSet<string> _requiredStoryMilestones;
    private readonly int _requiredInteractions;
    private readonly int _requiredTrustScore;

    public GangBankAccountProgressionState State { get; private set; }

    public bool GangAccountUnlocked
    {
        get { return State.GangAccountUnlocked; }
    }

    public GangBankAccountProgression(
        GangBankAccountProgressionState state,
        IEnumerable<string> bankerNpcIds,
        IEnumerable<string> requiredStoryMilestones,
        int requiredInteractions,
        int requiredTrustScore)
    {
        if (requiredInteractions < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requiredInteractions),
                "At least one meaningful banker interaction is required.");
        }

        if (requiredTrustScore < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requiredTrustScore),
                "The required banker trust score must be at least one.");
        }

        _bankerNpcIds = NormalizeSet(bankerNpcIds, nameof(bankerNpcIds));
        _requiredStoryMilestones = NormalizeSet(
            requiredStoryMilestones,
            nameof(requiredStoryMilestones));

        State = state ?? new GangBankAccountProgressionState();
        _requiredInteractions = requiredInteractions;
        _requiredTrustScore = requiredTrustScore;

        NormalizeState();
        TryUnlockGangAccount();
    }

    /// <summary>
    /// Records a meaningful interaction with a configured bank-connected NPC.
    /// Call this only after an actual interaction choice, not merely when the
    /// player targets or approaches an NPC.
    /// Returns true only if this interaction newly unlocks the gang account.
    /// </summary>
    public bool RecordBankerInteraction(string bankerNpcId, int trustChange)
    {
        string normalizedNpcId = NormalizeId(bankerNpcId);

        if (!_bankerNpcIds.Contains(normalizedNpcId))
        {
            return false;
        }

        int currentCount;
        if (!State.BankerInteractionCounts.TryGetValue(
                normalizedNpcId,
                out currentCount))
        {
            currentCount = 0;
        }

        if (currentCount < int.MaxValue)
        {
            State.BankerInteractionCounts[normalizedNpcId] = currentCount + 1;
        }

        int currentTrust;
        if (!State.BankerTrustScores.TryGetValue(
                normalizedNpcId,
                out currentTrust))
        {
            currentTrust = 0;
        }

        long updatedTrust = (long)currentTrust + trustChange;

        if (updatedTrust > int.MaxValue)
        {
            updatedTrust = int.MaxValue;
        }
        else if (updatedTrust < int.MinValue)
        {
            updatedTrust = int.MinValue;
        }

        State.BankerTrustScores[normalizedNpcId] = (int)updatedTrust;

        return TryUnlockGangAccount();
    }

    /// <summary>
    /// Records a completed gang-story milestone. Only the configured required
    /// milestones contribute to unlocking the gang account.
    /// Returns true only if this milestone newly unlocks the gang account.
    /// </summary>
    public bool CompleteStoryMilestone(string milestoneId)
    {
        string normalizedMilestoneId = NormalizeId(milestoneId);

        if (!_requiredStoryMilestones.Contains(normalizedMilestoneId))
        {
            return false;
        }

        State.CompletedStoryMilestones.Add(normalizedMilestoneId);
        return TryUnlockGangAccount();
    }

    public bool HasQualifiedBankerRelationship()
    {
        foreach (string bankerNpcId in _bankerNpcIds)
        {
            int interactions;
            int trust;

            State.BankerInteractionCounts.TryGetValue(
                bankerNpcId,
                out interactions);

            State.BankerTrustScores.TryGetValue(
                bankerNpcId,
                out trust);

            if (interactions >= _requiredInteractions &&
                trust >= _requiredTrustScore)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasRequiredStoryProgression()
    {
        foreach (string milestoneId in _requiredStoryMilestones)
        {
            if (!State.CompletedStoryMilestones.Contains(milestoneId))
            {
                return false;
            }
        }

        return true;
    }

    private bool TryUnlockGangAccount()
    {
        if (State.GangAccountUnlocked)
        {
            return false;
        }

        if (!HasQualifiedBankerRelationship() ||
            !HasRequiredStoryProgression())
        {
            return false;
        }

        State.GangAccountUnlocked = true;
        return true;
    }

    private void NormalizeState()
    {
        if (State.BankerInteractionCounts == null)
        {
            State.BankerInteractionCounts = new Dictionary<string, int>();
        }

        if (State.BankerTrustScores == null)
        {
            State.BankerTrustScores = new Dictionary<string, int>();
        }

        if (State.CompletedStoryMilestones == null)
        {
            State.CompletedStoryMilestones = new HashSet<string>();
        }

        State.BankerInteractionCounts =
            NormalizeDictionary(State.BankerInteractionCounts);

        State.BankerTrustScores =
            NormalizeDictionary(State.BankerTrustScores);

        State.CompletedStoryMilestones =
            NormalizeSet(State.CompletedStoryMilestones, nameof(State.CompletedStoryMilestones));
    }

    private static Dictionary<string, int> NormalizeDictionary(
        Dictionary<string, int> source)
    {
        var normalized = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> entry in source)
        {
            string key = NormalizeId(entry.Key);

            if (key.Length > 0)
            {
                normalized[key] = entry.Value;
            }
        }

        return normalized;
    }

    private static HashSet<string> NormalizeSet(
        IEnumerable<string> values,
        string parameterName)
    {
        if (values == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        var normalized = new HashSet<string>();

        foreach (string value in values)
        {
            string id = NormalizeId(value);

            if (id.Length > 0)
            {
                normalized.Add(id);
            }
        }

        if (normalized.Count == 0)
        {
            throw new ArgumentException(
                "At least one identifier must be provided.",
                parameterName);
        }

        return normalized;
    }

    private static string NormalizeId(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Trim().ToUpperInvariant();
    }
}