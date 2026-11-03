using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    public List<GangMember> Members { get; private set; } = new List<GangMember>();

    public GangMember AddMember(string name, GangType type)
    {
        GangMember member = new GangMember(name, type);
        Members.Add(member);
        return member;
    }

    public void RemoveMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return;

        Members.RemoveAll(m => m != null && m.MemberId == memberId);
    }

    public GangMember GetMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return null;

        return Members.FirstOrDefault(m => m != null && m.MemberId == memberId);
    }

    public void UpdateSupplies()
    {
        foreach (GangMember member in Members)
        {
            if (member == null)
                continue;

            if (member.NeedsSupplies)
                member.AddGoal("Find food, ammo, or medicine.");
        }
    }

    public List<GangMemberData> CreateSaveData()
    {
        List<GangMemberData> data = new List<GangMemberData>();

        foreach (GangMember member in Members)
        {
            if (member == null)
                continue;

            data.Add(member.ToData());
        }

        return data;
    }

    public void LoadFromSave(List<GangMemberData> data)
    {
        Members.Clear();

        if (data == null)
            return;

        foreach (GangMemberData item in data)
        {
            GangMember member = GangMember.FromData(item);
            if (member != null)
                Members.Add(member);
        }
    }
}