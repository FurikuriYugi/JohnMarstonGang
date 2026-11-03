using System;
using System.Collections.Generic;
using System.Linq;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();

    public List<GangMember> Members => _members;

    public GangMember AddMember(string name, CompanionType companionType, GangMemberRole role)
    {
        GangMember member = new GangMember
        {
            MemberId = Guid.NewGuid().ToString(),
            Name = name,
            CompanionType = companionType,
            Role = role,
            IsActive = true,
            Loyalty = 50,
            Trust = 50
        };

        _members.Add(member);
        return member;
    }

    public void RemoveMember(string memberId)
    {
        GangMember member = _members.FirstOrDefault(m => m.MemberId == memberId);
        if (member != null)
            _members.Remove(member);
    }

    public GangMember GetMember(string memberId)
    {
        return _members.FirstOrDefault(m => m.MemberId == memberId);
    }

    public IEnumerable<GangMember> GetActiveMembers()
    {
        return _members.Where(m => m.IsActive);
    }

    public void UpdateAll()
    {
        foreach (GangMember member in _members)
        {
            member.UpdateState();
        }
    }

    public void AssignTask(string memberId, GangTaskType taskType, string targetId = null, string description = null)
    {
        GangMember member = GetMember(memberId);
        if (member == null)
            return;

        member.CurrentTask = taskType;
        member.CurrentTargetId = targetId;
        member.CurrentTaskDescription = description;
    }

    public void SetGangMode(GangMode mode)
    {
        foreach (GangMember member in _members)
        {
            member.CurrentGangMode = mode;
        }
    }

    public void SetStealthMode(bool enabled)
    {
        foreach (GangMember member in _members)
        {
            member.IsStealthing = enabled;
            if (enabled)
                member.CurrentGangMode = GangMode.Stealth;
        }
    }

    public void SetCombatMode(bool enabled)
    {
        foreach (GangMember member in _members)
        {
            member.IsInCombatMode = enabled;
            if (enabled)
                member.CurrentGangMode = GangMode.Combat;
        }
    }

    public void SetShoppingMode(bool enabled)
    {
        foreach (GangMember member in _members)
        {
            member.IsShopping = enabled;
            if (enabled)
                member.CurrentGangMode = GangMode.Shopping;
        }
    }

    public void DistributeReward(int amount)
    {
        if (_members.Count == 0 || amount <= 0)
            return;

        int share = amount / _members.Count;

        foreach (GangMember member in _members)
        {
            member.Money += share;
            member.Loyalty += 1;
            member.Trust += 1;
        }
    }

    public void AdjustHonorForGang(int amount)
    {
        foreach (GangMember member in _members)
        {
            member.Honor += amount;
        }
    }

    public List<GangMemberSaveData> CreateSaveData()
    {
        List<GangMemberSaveData> saveData = new List<GangMemberSaveData>();

        foreach (GangMember member in _members)
        {
            saveData.Add(member.ToSaveData());
        }

        return saveData;
    }

    public void LoadFromSave(List<GangMemberSaveData> saveData)
    {
        _members.Clear();

        if (saveData == null)
            return;

        foreach (GangMemberSaveData data in saveData)
        {
            GangMember member = new GangMember();
            member.FromSaveData(data);
            _members.Add(member);
        }
    }
}