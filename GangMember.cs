using System;
using System.Collections.Generic;

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public CompanionType CompanionType { get; set; }
    public GangMemberRole Role { get; set; }

    public bool IsActive { get; set; }
    public bool IsShopping { get; set; }
    public bool IsStealthing { get; set; }
    public bool IsInCombatMode { get; set; }

    public int Loyalty { get; set; }
    public int Trust { get; set; }
    public int Honor { get; set; }
    public int Money { get; set; }
    public int Experience { get; set; }

    public GangMode CurrentGangMode { get; set; }
    public GangTaskType CurrentTask { get; set; }
    public string CurrentTargetId { get; set; }
    public string CurrentTaskDescription { get; set; }

    public List<string> KnownGoals { get; set; } = new List<string>();
    public List<string> Memories { get; set; } = new List<string>();
    public List<string> Skills { get; set; } = new List<string>();

    public string CurrentWeaponPreference { get; set; }

    public void UpdateState()
    {
        if (Loyalty < 0) Loyalty = 0;
        if (Loyalty > 100) Loyalty = 100;

        if (Trust < 0) Trust = 0;
        if (Trust > 100) Trust = 100;

        if (Honor < -100) Honor = -100;
        if (Honor > 100) Honor = 100;

        if (Experience < 0) Experience = 0;
    }

    public string GetSuggestedWeapon()
    {
        if (IsStealthing || CurrentGangMode == GangMode.Stealth)
        {
            if (Skills.Contains("Bow"))
                return "Bow";

            if (Skills.Contains("Knife"))
                return "Knife";

            return "Bow";
        }

        if (IsInCombatMode || CurrentGangMode == GangMode.Combat)
        {
            if (Role == GangMemberRole.Scout)
                return "Repeater";

            if (Role == GangMemberRole.Enforcer)
                return "Rifle";

            if (Role == GangMemberRole.Leader)
                return "Revolver";

            return "Rifle";
        }

        if (IsShopping || CurrentGangMode == GangMode.Shopping)
            return "Sidearm";

        return CurrentWeaponPreference ?? "Revolver";
    }

    public GangMemberSaveData ToSaveData()
    {
        return new GangMemberSaveData
        {
            MemberId = MemberId,
            Name = Name,
            CompanionType = CompanionType,
            Role = Role,
            IsActive = IsActive,
            IsShopping = IsShopping,
            IsStealthing = IsStealthing,
            IsInCombatMode = IsInCombatMode,
            Loyalty = Loyalty,
            Trust = Trust,
            Honor = Honor,
            Money = Money,
            Experience = Experience,
            CurrentGangMode = CurrentGangMode,
            CurrentTask = CurrentTask,
            CurrentTargetId = CurrentTargetId,
            CurrentTaskDescription = CurrentTaskDescription,
            KnownGoals = new List<string>(KnownGoals ?? new List<string>()),
            Memories = new List<string>(Memories ?? new List<string>()),
            Skills = new List<string>(Skills ?? new List<string>()),
            CurrentWeaponPreference = CurrentWeaponPreference
        };
    }

    public void FromSaveData(GangMemberSaveData data)
    {
        if (data == null)
            return;

        MemberId = data.MemberId;
        Name = data.Name;
        CompanionType = data.CompanionType;
        Role = data.Role;
        IsActive = data.IsActive;
        IsShopping = data.IsShopping;
        IsStealthing = data.IsStealthing;
        IsInCombatMode = data.IsInCombatMode;
        Loyalty = data.Loyalty;
        Trust = data.Trust;
        Honor = data.Honor;
        Money = data.Money;
        Experience = data.Experience;
        CurrentGangMode = data.CurrentGangMode;
        CurrentTask = data.CurrentTask;
        CurrentTargetId = data.CurrentTargetId;
        CurrentTaskDescription = data.CurrentTaskDescription;
        KnownGoals = data.KnownGoals ?? new List<string>();
        Memories = data.Memories ?? new List<string>();
        Skills = data.Skills ?? new List<string>();
        CurrentWeaponPreference = data.CurrentWeaponPreference;
    }
}