using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();
    private readonly TacticalSquadAiSystem _tacticalAi = new TacticalSquadAiSystem();

    private const string SaveFileName = "JohnMarstonGang_Save.xml";

    public IReadOnlyList<GangMember> Members => _members;

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

                        manager._members.Add(new GangMember
                        {
                            Name = string.IsNullOrWhiteSpace(memberData.Name) ? "Member" : memberData.Name,
                            Relationship = memberData.Relationship,
                            CombatStyle = (CombatStyle)memberData.CombatStyle,
                            IsFollowing = memberData.IsFollowing
                        });
                    }
                }
            }
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
        if (string.IsNullOrWhiteSpace(name))
            name = "Member";

        if (_members.Any(x => x != null && x.Name == name))
            return;

        _members.Add(new GangMember
        {
            Name = name,
            Relationship = relationship,
            CombatStyle = style,
            IsFollowing = following
        });
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