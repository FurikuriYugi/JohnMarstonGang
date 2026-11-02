using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TacticalSquadAiSystem
{
    private readonly string _savePath;
    private readonly Random _rng = new Random();
    private readonly List<SquadDirective> _directives = new List<SquadDirective>();

    public bool Enabled { get; set; } = true;
    public float EngageRange { get; set; } = 45f;
    public float HoldRange { get; set; } = 18f;
    public float RegroupRange { get; set; } = 30f;
    public int ReissueIntervalMs { get; set; } = 1500;
    public bool UseCoverPreference { get; set; } = true;

    private long _nextThinkAt;

    public IReadOnlyList<SquadDirective> Directives => _directives;

    public TacticalSquadAiSystem(string saveFileName = "TacticalSquadAiSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public void Update(Vector3 leaderPosition, IReadOnlyList<GangMember> squadMembers, Func<GangMember, bool> memberIsDown = null, Action<GangMember, SquadOrder> issueOrder = null)
    {
        if (!Enabled || squadMembers == null || squadMembers.Count == 0)
            return;

        long now = Environment.TickCount64;
        if (now < _nextThinkAt)
            return;

        _nextThinkAt = now + ReissueIntervalMs;

        CleanupStaleDirectives(squadMembers);

        bool enemyNear = IsEnemyThreatNear(leaderPosition);
        bool regroupNeeded = HasSpreadOutSquad(leaderPosition, squadMembers, memberIsDown);
        bool holdPosition = !enemyNear && !regroupNeeded;

        foreach (var member in squadMembers)
        {
            if (member == null)
                continue;

            if (memberIsDown != null && memberIsDown(member))
            {
                SetDirective(member, SquadOrder.ReviveOrRetreat, issueOrder);
                continue;
            }

            float dist = Vector3.Distance(leaderPosition, member.CurrentPosition);

            if (regroupNeeded && dist > RegroupRange)
            {
                SetDirective(member, SquadOrder.Regroup, issueOrder);
                continue;
            }

            if (enemyNear && dist <= EngageRange)
            {
                SetDirective(member, UseCoverPreference && _rng.NextDouble() < 0.7 ? SquadOrder.TakeCoverAndEngage : SquadOrder.Engage, issueOrder);
                continue;
            }

            if (holdPosition || dist <= HoldRange)
            {
                SetDirective(member, SquadOrder.HoldPosition, issueOrder);
                continue;
            }

            SetDirective(member, SquadOrder.FollowLeader, issueOrder);
        }

        Save();
    }

    public void ForceOrder(GangMember member, SquadOrder order, Action<GangMember, SquadOrder> issueOrder = null)
    {
        if (member == null)
            return;

        UpsertDirective(member, order);
        issueOrder?.Invoke(member, order);
        Save();
    }

    public bool RemoveDirective(string memberId)
    {
        bool removed = _directives.RemoveAll(x => x.MemberId == memberId) > 0;
        if (removed)
            Save();
        return removed;
    }

    public void Clear()
    {
        _directives.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_directives, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<SquadDirective>>(json);

            _directives.Clear();
            if (loaded != null)
                _directives.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.MemberId)));
        }
        catch
        {
            _directives.Clear();
        }
    }

    private void SetDirective(GangMember member, SquadOrder order, Action<GangMember, SquadOrder> issueOrder)
    {
        var existing = _directives.FirstOrDefault(x => x.MemberId == member.MemberId);
        if (existing == null)
        {
            existing = new SquadDirective
            {
                MemberId = member.MemberId,
                LastAssignedUtc = DateTime.UtcNow
            };
            _directives.Add(existing);
        }

        if (existing.Order != order)
        {
            existing.Order = order;
            existing.LastAssignedUtc = DateTime.UtcNow;
            issueOrder?.Invoke(member, order);
        }
    }

    private void UpsertDirective(GangMember member, SquadOrder order)
    {
        var existing = _directives.FirstOrDefault(x => x.MemberId == member.MemberId);
        if (existing == null)
        {
            _directives.Add(new SquadDirective
            {
                MemberId = member.MemberId,
                Order = order,
                LastAssignedUtc = DateTime.UtcNow
            });
            return;
        }

        existing.Order = order;
        existing.LastAssignedUtc = DateTime.UtcNow;
    }

    private void CleanupStaleDirectives(IReadOnlyList<GangMember> squadMembers)
    {
        var validIds = new HashSet<string>(squadMembers.Where(x => x != null).Select(x => x.MemberId));
        _directives.RemoveAll(x => !validIds.Contains(x.MemberId));
    }

    private bool HasSpreadOutSquad(Vector3 leaderPosition, IReadOnlyList<GangMember> squadMembers, Func<GangMember, bool> memberIsDown)
    {
        int farCount = 0;

        foreach (var member in squadMembers)
        {
            if (member == null)
                continue;

            if (memberIsDown != null && memberIsDown(member))
                continue;

            if (Vector3.Distance(leaderPosition, member.CurrentPosition) > RegroupRange)
                farCount++;
        }

        return farCount >= Math.Max(1, squadMembers.Count / 2);
    }

    private bool IsEnemyThreatNear(Vector3 leaderPosition)
    {
        return _rng.NextDouble() < 0.45;
    }
}

public class SquadDirective
{
    public string MemberId { get; set; }
    public SquadOrder Order { get; set; }
    public DateTime LastAssignedUtc { get; set; }
}

public enum SquadOrder
{
    FollowLeader,
    HoldPosition,
    Engage,
    TakeCoverAndEngage,
    Regroup,
    ReviveOrRetreat
}