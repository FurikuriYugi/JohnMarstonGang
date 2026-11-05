using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public sealed class GangJobVoteOption
{
    public string JobId { get; set; }
    public string JobName { get; set; }
    public string Style { get; set; }
    public decimal ExpectedCash { get; set; }
    public int Risk { get; set; }
    public int HonorChange { get; set; }
    public int GangBenefit { get; set; }
    public int MinimumGangMembers { get; set; }
    public List<string> Tags { get; set; }

    public GangJobVoteOption()
    {
        Tags = new List<string>();
    }
}

public sealed class GangJobMemberVoteProfile
{
    public string MemberId { get; set; }
    public int Greed { get; set; }
    public int RiskTolerance { get; set; }
    public int HonorAlignment { get; set; }
    public int Loyalty { get; set; }
    public int AvailableGangMembers { get; set; }
    public List<string> PreferredTags { get; set; }

    public GangJobMemberVoteProfile()
    {
        PreferredTags = new List<string>();
    }
}

public sealed class GangJobVoteRecord
{
    public string VoterId { get; set; }
    public string JobId { get; set; }
    public DateTime CastUtc { get; set; }
    public bool IsAutomatic { get; set; }
}

public sealed class GangJobPollData
{
    public string PollId { get; set; }
    public string InitiatedBy { get; set; }
    public DateTime CreatedUtc { get; set; }
    public bool IsClosed { get; set; }
    public string WinnerJobId { get; set; }
    public List<GangJobVoteOption> Options { get; set; }
    public List<GangJobVoteRecord> Votes { get; set; }

    public GangJobPollData()
    {
        Options = new List<GangJobVoteOption>();
        Votes = new List<GangJobVoteRecord>();
    }
}

public sealed class GangJobVotingState
{
    public List<GangJobPollData> Polls { get; set; }

    public GangJobVotingState()
    {
        Polls = new List<GangJobPollData>();
    }
}

public sealed class GangJobVotingSystem
{
    private readonly List<GangJobPollData> _polls =
        new List<GangJobPollData>();

    public GangJobPollData GetActivePoll()
    {
        GangJobPollData poll = _polls
            .Where(x => x != null && !x.IsClosed)
            .OrderByDescending(x => x.CreatedUtc)
            .FirstOrDefault();

        return poll == null ? null : ClonePoll(poll);
    }

    public List<GangJobPollData> GetAllPolls()
    {
        return _polls
            .Where(x => x != null)
            .Select(ClonePoll)
            .ToList();
    }

    public string StartPoll(
        IEnumerable<GangJobVoteOption> options,
        string initiatedBy)
    {
        if (_polls.Any(x => x != null && !x.IsClosed) ||
            options == null)
        {
            return null;
        }

        List<GangJobVoteOption> validOptions =
            new List<GangJobVoteOption>();
        HashSet<string> seenJobIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (GangJobVoteOption option in options)
        {
            if (option == null ||
                string.IsNullOrWhiteSpace(option.JobId) ||
                string.IsNullOrWhiteSpace(option.JobName) ||
                !seenJobIds.Add(option.JobId))
            {
                continue;
            }

            validOptions.Add(CloneOption(option));
        }

        if (validOptions.Count == 0)
            return null;

        GangJobPollData poll = new GangJobPollData
        {
            PollId = Guid.NewGuid().ToString("N"),
            InitiatedBy = initiatedBy,
            CreatedUtc = DateTime.UtcNow,
            IsClosed = false,
            Options = validOptions
        };

        _polls.Add(poll);
        return poll.PollId;
    }

    public bool CastVote(
        string pollId,
        string voterId,
        string jobId)
    {
        return RecordVote(pollId, voterId, jobId, false);
    }

    public bool CastAutomaticVote(
        string pollId,
        GangJobMemberVoteProfile member)
    {
        GangJobPollData poll = FindPoll(pollId);
        if (poll == null ||
            poll.IsClosed ||
            member == null ||
            string.IsNullOrWhiteSpace(member.MemberId))
        {
            return false;
        }

        int availableGangMembers =
            Math.Max(0, member.AvailableGangMembers);

        List<GangJobVoteOption> eligibleOptions = poll.Options
            .Where(x => x != null &&
                        x.MinimumGangMembers <= availableGangMembers)
            .ToList();

        if (eligibleOptions.Count == 0)
            return false;

        decimal highestExpectedCash = eligibleOptions
            .Select(x => Math.Max(0m, x.ExpectedCash))
            .DefaultIfEmpty(0m)
            .Max();

        GangJobVoteOption preferredOption = eligibleOptions
            .OrderByDescending(x => CalculatePreferenceScore(
                x,
                member,
                highestExpectedCash))
            .ThenBy(x => x.JobId, StringComparer.Ordinal)
            .FirstOrDefault();

        if (preferredOption == null)
            return false;

        return RecordVote(
            pollId,
            member.MemberId,
            preferredOption.JobId,
            true);
    }

    public bool ClosePoll(string pollId)
    {
        GangJobPollData poll = FindPoll(pollId);
        if (poll == null || poll.IsClosed)
            return false;

        poll.IsClosed = true;
        poll.WinnerJobId = FindWinnerJobId(poll);
        return !string.IsNullOrWhiteSpace(poll.WinnerJobId);
    }

    public int GetVoteCount(string pollId, string jobId)
    {
        GangJobPollData poll = FindPoll(pollId);
        if (poll == null || string.IsNullOrWhiteSpace(jobId))
            return 0;

        return poll.Votes.Count(
            x => x != null && x.JobId == jobId);
    }

    public string SaveState()
    {
        GangJobVotingState state = new GangJobVotingState
        {
            Polls = _polls
                .Where(x => x != null)
                .Select(ClonePoll)
                .ToList()
        };

        return JsonConvert.SerializeObject(
            state,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            _polls.Clear();
            return;
        }

        GangJobVotingState loadedState;
        try
        {
            loadedState =
                JsonConvert.DeserializeObject<GangJobVotingState>(json);
        }
        catch (JsonException)
        {
            return;
        }

        if (loadedState == null || loadedState.Polls == null)
        {
            _polls.Clear();
            return;
        }

        List<GangJobPollData> validPolls =
            new List<GangJobPollData>();
        HashSet<string> seenPollIds =
            new HashSet<string>(StringComparer.Ordinal);

        foreach (GangJobPollData poll in loadedState.Polls)
        {
            if (poll == null ||
                string.IsNullOrWhiteSpace(poll.PollId) ||
                !seenPollIds.Add(poll.PollId))
            {
                continue;
            }

            NormalizePoll(poll);
            validPolls.Add(poll);
        }

        // Keep at most one open poll. If a save contains several,
        // preserve the newest and close the older ones.
        List<GangJobPollData> openPolls = validPolls
            .Where(x => !x.IsClosed)
            .OrderByDescending(x => x.CreatedUtc)
            .ToList();

        for (int i = 1; i < openPolls.Count; i++)
        {
            openPolls[i].IsClosed = true;
            openPolls[i].WinnerJobId = FindWinnerJobId(openPolls[i]);
        }

        _polls.Clear();
        _polls.AddRange(validPolls);
    }

    private bool RecordVote(
        string pollId,
        string voterId,
        string jobId,
        bool isAutomatic)
    {
        GangJobPollData poll = FindPoll(pollId);
        if (poll == null ||
            poll.IsClosed ||
            string.IsNullOrWhiteSpace(voterId) ||
            string.IsNullOrWhiteSpace(jobId) ||
            !poll.Options.Any(x => x != null && x.JobId == jobId))
        {
            return false;
        }

        GangJobVoteRecord existingVote = poll.Votes.FirstOrDefault(
            x => x != null && x.VoterId == voterId);

        if (existingVote == null)
        {
            poll.Votes.Add(new GangJobVoteRecord
            {
                VoterId = voterId,
                JobId = jobId,
                CastUtc = DateTime.UtcNow,
                IsAutomatic = isAutomatic
            });
        }
        else
        {
            existingVote.JobId = jobId;
            existingVote.CastUtc = DateTime.UtcNow;
            existingVote.IsAutomatic = isAutomatic;
        }

        return true;
    }

    private GangJobPollData FindPoll(string pollId)
    {
        if (string.IsNullOrWhiteSpace(pollId))
            return null;

        return _polls.FirstOrDefault(
            x => x != null && x.PollId == pollId);
    }

    private string FindWinnerJobId(GangJobPollData poll)
    {
        if (poll == null || poll.Options == null)
            return null;

        List<GangJobVoteOption> optionsWithVotes = poll.Options
            .Where(x => x != null)
            .Select(x => new
            {
                Option = x,
                VoteCount = poll.Votes.Count(
                    v => v != null && v.JobId == x.JobId)
            })
            .Where(x => x.VoteCount > 0)
            .OrderByDescending(x => x.VoteCount)
            .ThenBy(x => x.Option.JobId, StringComparer.Ordinal)
            .Select(x => x.Option)
            .ToList();

        return optionsWithVotes.Count == 0
            ? null
            : optionsWithVotes[0].JobId;
    }

    private decimal CalculatePreferenceScore(
        GangJobVoteOption option,
        GangJobMemberVoteProfile member,
        decimal highestExpectedCash)
    {
        int greed = Clamp(member.Greed, 0, 100);
        int riskTolerance = Clamp(member.RiskTolerance, 0, 100);
        int honorAlignment = Clamp(member.HonorAlignment, 0, 100);
        int loyalty = Clamp(member.Loyalty, 0, 100);

        decimal cashScore = highestExpectedCash <= 0m
            ? 0m
            : 100m * Math.Max(0m, option.ExpectedCash) /
              highestExpectedCash;

        int riskScore =
            100 - Math.Abs(Clamp(option.Risk, 0, 100) - riskTolerance);

        int honorPreference;
        if (option.HonorChange > 0)
        {
            honorPreference = honorAlignment;
        }
        else if (option.HonorChange < 0)
        {
            honorPreference = 100 - honorAlignment;
        }
        else
        {
            honorPreference = 50;
        }

        int gangBenefit = Clamp(option.GangBenefit, 0, 100);
        int loyaltyScore =
            (loyalty * gangBenefit) +
            ((100 - loyalty) * (100 - gangBenefit));
        loyaltyScore /= 100;

        int tagScore = CalculateTagScore(
            option.Tags,
            member.PreferredTags);

        decimal cashWeight = 10m + (greed * 0.25m);
        decimal riskWeight = 15m + (riskTolerance * 0.15m);
        decimal honorWeight = 15m + (honorAlignment * 0.15m);
        decimal gangWeight = 10m + (loyalty * 0.20m);
        const decimal tagWeight = 10m;

        decimal weightedScore =
            (cashScore * cashWeight) +
            (riskScore * riskWeight) +
            (honorPreference * honorWeight) +
            (loyaltyScore * gangWeight) +
            (tagScore * tagWeight);

        decimal totalWeight =
            cashWeight + riskWeight + honorWeight +
            gangWeight + tagWeight;

        return totalWeight <= 0m
            ? 0m
            : weightedScore / totalWeight;
    }

    private int CalculateTagScore(
        List<string> jobTags,
        List<string> preferredTags)
    {
        if (jobTags == null ||
            preferredTags == null ||
            jobTags.Count == 0 ||
            preferredTags.Count == 0)
        {
            return 50;
        }

        int matches = jobTags.Count(
            tag => !string.IsNullOrWhiteSpace(tag) &&
                   preferredTags.Any(
                       preferredTag => string.Equals(
                           tag,
                           preferredTag,
                           StringComparison.OrdinalIgnoreCase)));

        return matches > 0 ? 100 : 0;
    }

    private void NormalizePoll(GangJobPollData poll)
    {
        if (poll.Options == null)
            poll.Options = new List<GangJobVoteOption>();

        if (poll.Votes == null)
            poll.Votes = new List<GangJobVoteRecord>();

        HashSet<string> validJobIds =
            new HashSet<string>(StringComparer.Ordinal);
        List<GangJobVoteOption> validOptions =
            new List<GangJobVoteOption>();

        foreach (GangJobVoteOption option in poll.Options)
        {
            if (option == null ||
                string.IsNullOrWhiteSpace(option.JobId) ||
                string.IsNullOrWhiteSpace(option.JobName) ||
                !validJobIds.Add(option.JobId))
            {
                continue;
            }

            validOptions.Add(CloneOption(option));
        }

        poll.Options = validOptions;

        HashSet<string> seenVoterIds =
            new HashSet<string>(StringComparer.Ordinal);
        List<GangJobVoteRecord> validVotes =
            new List<GangJobVoteRecord>();

        foreach (GangJobVoteRecord vote in poll.Votes)
        {
            if (vote == null ||
                string.IsNullOrWhiteSpace(vote.VoterId) ||
                !validJobIds.Contains(vote.JobId))
            {
                continue;
            }

            if (!seenVoterIds.Add(vote.VoterId))
            {
                validVotes.RemoveAll(
                    x => x.VoterId == vote.VoterId);
            }

            validVotes.Add(vote);
        }

        poll.Votes = validVotes;

        if (!string.IsNullOrWhiteSpace(poll.WinnerJobId) &&
            !validJobIds.Contains(poll.WinnerJobId))
        {
            poll.WinnerJobId = null;
        }
    }

    private GangJobVoteOption CloneOption(
        GangJobVoteOption option)
    {
        return new GangJobVoteOption
        {
            JobId = option.JobId,
            JobName = option.JobName,
            Style = option.Style,
            ExpectedCash = Math.Max(0m, option.ExpectedCash),
            Risk = Clamp(option.Risk, 0, 100),
            HonorChange = Clamp(option.HonorChange, -100, 100),
            GangBenefit = Clamp(option.GangBenefit, 0, 100),
            MinimumGangMembers = Math.Max(0, option.MinimumGangMembers),
            Tags = option.Tags == null
                ? new List<string>()
                : option.Tags
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
        };
    }

    private GangJobPollData ClonePoll(GangJobPollData poll)
    {
        return new GangJobPollData
        {
            PollId = poll.PollId,
            InitiatedBy = poll.InitiatedBy,
            CreatedUtc = poll.CreatedUtc,
            IsClosed = poll.IsClosed,
            WinnerJobId = poll.WinnerJobId,
            Options = poll.Options == null
                ? new List<GangJobVoteOption>()
                : poll.Options
                    .Where(x => x != null)
                    .Select(CloneOption)
                    .ToList(),
            Votes = poll.Votes == null
                ? new List<GangJobVoteRecord>()
                : poll.Votes
                    .Where(x => x != null)
                    .Select(x => new GangJobVoteRecord
                    {
                        VoterId = x.VoterId,
                        JobId = x.JobId,
                        CastUtc = x.CastUtc,
                        IsAutomatic = x.IsAutomatic
                    })
                    .ToList()
        };
    }

    private int Clamp(int value, int minimum, int maximum)
    {
        if (value < minimum)
            return minimum;

        if (value > maximum)
            return maximum;

        return value;
    }
}