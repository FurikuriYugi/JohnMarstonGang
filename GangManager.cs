using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();
    private readonly TacticalSquadAiSystem _tacticalAi = new TacticalSquadAiSystem();
    private readonly CompanionSystem _companionSystem;

    private const string SaveFileName = "JohnMarstonGang_Save.xml";

    public IReadOnlyList<GangMember> Members => _members;
    public CompanionSystem CompanionSystem => _companionSystem;

    public GangManager()
    {
        _companionSystem = new CompanionSystem();
    }

    public GangManager(CompanionSystem companionSystem)
    {
        _companionSystem = companionSystem ?? new CompanionSystem();
    }

    public static GangManager Load()
    {
        var manager = new GangManager();

        if (!File.Exists(SaveFileName))
            return manager;

        try
        {
            using (var fs = File.OpenRead(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(GangSaveData));
                var data = serializer.ReadObject(fs) as GangSaveData;

                if (data != null && data.Members != null)
                {
                    foreach (var memberData in data.Members)
                    {
                        if (memberData == null)
                            continue;

                        var member = new GangMember
                        {
                            Name = string.IsNullOrWhiteSpace(memberData.Name) ? "Member" : memberData.Name,
                            Relationship = memberData.Relationship,
                            CombatStyle = (CombatStyle)memberData.CombatStyle,
                            IsFollowing = memberData.IsFollowing
                        };

                        manager._members.Add(member);
                        manager._companionSystem.RegisterMember(member);
                    }
                }
            }

            manager._companionSystem.Load();
        }
        catch
        {
        }

        return manager;
    }

    public void Save()
    {
        try
        {
            var data = new GangSaveData
            {
                Members = _members.Select(m => new GangMemberData
                {
                    Name = m.Name,
                    Relationship = m.Relationship,
                    CombatStyle = (int)m.CombatStyle,
                    IsFollowing = m.IsFollowing
                }).ToList()
            };

            using (var fs = File.Create(SaveFileName))
            {
                var serializer = new DataContractSerializer(typeof(GangSaveData));
                serializer.WriteObject(fs, data);
            }

            if (_companionSystem != null)
                _companionSystem.Save();
        }
        catch
        {
        }
    }

    public void Update()
    {
        UpdateWithContext(GetDefaultContext());
    }

    public void UpdateWithContext(TacticalContext context)
    {
        if (context == null)
            context = GetDefaultContext();

        _tacticalAi.UpdateGangTactics(_members, context);
        _tacticalAi.ApplyFormationSpacing(_members, context);

        foreach (var member in _members)
        {
            if (member != null)
                member.Update();
        }
    }

    public void RecruitNearby()
    {
        Recruit("Recruit", 0, CombatStyle.RangedMedium, true);
    }

    public void Recruit(string name, int relationship, CombatStyle style, bool following)
    {
        var memberName = string.IsNullOrWhiteSpace(name) ? "Member" : name;

        if (_members.Any(x => x != null && x.Name == memberName))
            return;

        var member = new GangMember
        {
            Name = memberName,
            Relationship = relationship,
            CombatStyle = style,
            IsFollowing = following
        };

        _members.Add(member);

        if (_companionSystem != null)
            _companionSystem.RegisterMember(member);
    }

    public void FollowPlayer()
    {
        foreach (var member in _members)
        {
            if (member != null)
                member.IsFollowing = true;
        }
    }

    public void Wait()
    {
        foreach (var member in _members)
        {
            if (member != null)
                member.IsFollowing = false;
        }
    }

    public void SetCombatStyleForAll(CombatStyle style)
    {
        foreach (var member in _members)
        {
            if (member != null)
                member.CombatStyle = style;
        }
    }

    public void CycleCombatStyle()
    {
        foreach (var member in _members)
        {
            if (member != null)
                member.CombatStyle = member.CombatStyle.Next();
        }
    }

    public GangMember GetMember(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _members.FirstOrDefault(m => m != null && m.Name == name);
    }

    public bool RemoveMember(string name)
    {
        var member = GetMember(name);
        if (member == null)
            return false;

        return _members.Remove(member);
    }

    public void UpdateMemberRelationship(string name, int delta)
    {
        var member = GetMember(name);
        if (member == null)
            return;

        member.Relationship += delta;

        if (_companionSystem != null)
        {
            _companionSystem.UpdateLoyalty(member.Name, delta / 2);
            _companionSystem.UpdateMorale(member.Name, delta / 2);
        }
    }

    public void SetMemberGoal(string name, string goal)
    {
        if (_companionSystem != null)
            _companionSystem.SetGoal(name, goal);
    }

    public void AddMemberMemory(string name, string memory)
    {
        if (_companionSystem != null)
            _companionSystem.AddMemory(name, memory);
    }

    public void SyncAllMembers()
    {
        if (_companionSystem == null)
            return;

        foreach (var member in _members)
        {
            if (member != null)
                _companionSystem.RegisterMember(member);
        }
    }

    private TacticalContext GetDefaultContext()
    {
        return new TacticalContext
        {
            IsCombatActive = false,
            IsHostileNearby = false,
            IsStealthMode = false,
            IsShoppingOrTown = false,
            IsTraveling = true,
            IsCloseQuarters = false,
            IsMediumRangeCombat = false,
            IsLongRangeCombat = false,
            FollowMode = true
        };
    }
}