using System;
using System.Collections.Generic;

public class GangManager
{
    public List<GangMember> GangMembers { get; set; }
    public List<BankAccountData> BankAccounts { get; set; }
    public WorldState WorldState { get; set; }

    public GangManager()
    {
        GangMembers = new List<GangMember>();
        BankAccounts = new List<BankAccountData>();
        WorldState = new WorldState();
    }

    public SaveRoot ExportSaveData()
    {
        SaveRoot data = new SaveRoot
        {
            SaveVersion = 1,
            World = WorldState ?? new WorldState(),
            GangMembers = GangMembers ?? new List<GangMember>(),
            BankAccounts = BankAccounts ?? new List<BankAccountData>()
        };

        return data;
    }

    public void ImportSaveData(SaveRoot data)
    {
        if (data == null)
        {
            GangMembers = new List<GangMember>();
            BankAccounts = new List<BankAccountData>();
            WorldState = new WorldState();
            return;
        }

        GangMembers = data.GangMembers ?? new List<GangMember>();
        BankAccounts = data.BankAccounts ?? new List<BankAccountData>();
        WorldState = data.World ?? new WorldState();
    }

    public void AddGangMember(GangMember member)
    {
        if (member == null)
        {
            return;
        }

        GangMembers.Add(member);
    }

    public void RemoveGangMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
        {
            return;
        }

        GangMembers.RemoveAll(x => x != null && x.MemberId == memberId);
    }

    public GangMember GetGangMember(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
        {
            return null;
        }

        for (int i = 0; i < GangMembers.Count; i++)
        {
            GangMember member = GangMembers[i];
            if (member != null && member.MemberId == memberId)
            {
                return member;
            }
        }

        return null;
    }

    public BankAccountData GetBankAccount(string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
        {
            return null;
        }

        for (int i = 0; i < BankAccounts.Count; i++)
        {
            BankAccountData account = BankAccounts[i];
            if (account != null && account.OwnerId == ownerId)
            {
                return account;
            }
        }

        return null;
    }

    public BankAccountData GetOrCreateBankAccount(string ownerId, string ownerName)
    {
        BankAccountData account = GetBankAccount(ownerId);
        if (account != null)
        {
            return account;
        }

        account = new BankAccountData
        {
            OwnerId = ownerId,
            OwnerName = ownerName,
            Cash = 0m,
            Gold = 0m,
            CreditLimit = 0m,
            Debt = 0m,
            IsSuspicious = false,
            LastTransactionUtc = DateTime.UtcNow
        };

        BankAccounts.Add(account);
        return account;
    }
}

public class GangMember
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int Loyalty { get; set; }
    public int Experience { get; set; }
    public int CombatSkill { get; set; }
    public int StealthSkill { get; set; }
    public int SupportSkill { get; set; }
    public string PreferredCombatStyle { get; set; }
    public bool IsAlive { get; set; }
    public bool IsFollowingPlayer { get; set; }
    public bool IsRecruitable { get; set; }
    public List<string> InventoryItems { get; set; }
    public List<string> Memories { get; set; }
    public List<string> Goals { get; set; }

    public GangMember()
    {
        InventoryItems = new List<string>();
        Memories = new List<string>();
        Goals = new List<string>();
        IsAlive = true;
        IsFollowingPlayer = false;
        IsRecruitable = true;
    }
}