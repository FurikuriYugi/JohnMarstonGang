// GangManager.cs
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;

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
                var data = (GangSaveData)serializer.ReadObject(fs);

                if (data != null && data.Members != null)
                {
                    foreach (var memberData in data.Members)
                    {
                        manager._members.Add(new GangMember
                        {
                            Name = memberData.Name,
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
        var context = new TacticalContext
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

        _tacticalAi.UpdateGangTactics(_members, context);
        _tacticalAi.ApplyFormationSpacing(_members, context);

        foreach (var member in _members)
        {
            member.Update();
        }
    }

    public void UpdateWithContext(TacticalContext context)
    {
        _tacticalAi.UpdateGangTactics(_members, context);
        _tacticalAi.ApplyFormationSpacing(_members, context);

        foreach (var member in _members)
        {
            member.Update();
        }
    }

    public void RecruitNearby()
    {
        var newMember = new GangMember
        {
            Name = "Recruit",
            Relationship = 0,
            CombatStyle = CombatStyle.RangedMedium,
            IsFollowing = true
        };

        _members.Add(newMember);
    }

    public void Recruit(string name, int relationship, CombatStyle style, bool following)
    {
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
            member.IsFollowing = true;
        }
    }

    public void Wait()
    {
        foreach (var member in _members)
        {
            member.IsFollowing = false;
        }
    }

    public void SetCombatStyleForAll(CombatStyle style)
    {
        foreach (var member in _members)
        {
            member.CombatStyle = style;
        }
    }

    public void CycleCombatStyle()
    {
        foreach (var member in _members)
        {
            member.CombatStyle = member.CombatStyle.Next();
        }
    }
}