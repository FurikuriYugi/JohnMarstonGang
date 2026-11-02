using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GangManager
{
    private const string SavePath = @"scripts\JohnMorstonGang\gang.json";

    public List<GangMember> Members { get; private set; }
    public GangOrder CurrentOrder { get; private set; }
    public GangCombatStyle CurrentCombatStyle { get; private set; }

    public GangManager()
    {
        Members = new List<GangMember>();
        CurrentOrder = GangOrder.Follow;
        CurrentCombatStyle = GangCombatStyle.RangedShort;
    }

    public void Update()
    {
        foreach (var member in Members)
        {
            if (member == null || !member.IsAlive)
                continue;

            member.CurrentOrder = CurrentOrder;
            member.CombatStyle = CurrentCombatStyle;
        }
    }

    public void RecruitNearby()
    {
        // placeholder: hook nearby NPC recruitment later
    }

    public void OrderFollow()
    {
        CurrentOrder = GangOrder.Follow;
    }

    public void OrderWait()
    {
        CurrentOrder = GangOrder.Wait;
    }

    public void CycleCombatStyle()
    {
        var values = Enum.GetValues(typeof(GangCombatStyle)).Cast<GangCombatStyle>().ToList();
        int index = values.IndexOf(CurrentCombatStyle);
        index = (index + 1) % values.Count;
        CurrentCombatStyle = values[index];
    }

    public void AddMember(GangMember member)
    {
        if (member == null)
            return;

        if (Members.Any(m => m.MemberId == member.MemberId))
            return;

        Members.Add(member);
    }

    public void RemoveMember(string memberId)
    {
        Members.RemoveAll(m => m.MemberId == memberId);
    }

    public GangMember GetMember(string memberId)
    {
        return Members.FirstOrDefault(m => m.MemberId == memberId);
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath));

        var data = new GangSaveData
        {
            Members = Members,
            CurrentOrder = CurrentOrder,
            CurrentCombatStyle = CurrentCombatStyle
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(SavePath, json);
    }

    public static GangManager Load()
    {
        if (!File.Exists(SavePath))
            return new GangManager();

        try
        {
            var json = File.ReadAllText(SavePath);
            var data = JsonSerializer.Deserialize<GangSaveData>(json);

            var manager = new GangManager();
            if (data != null)
            {
                manager.Members = data.Members ?? new List<GangMember>();
                manager.CurrentOrder = data.CurrentOrder;
                manager.CurrentCombatStyle = data.CurrentCombatStyle;
            }

            return manager;
        }
        catch
        {
            return new GangManager();
        }
    }

    private class GangSaveData
    {
        public List<GangMember> Members { get; set; }
        public GangOrder CurrentOrder { get; set; }
        public GangCombatStyle CurrentCombatStyle { get; set; }
    }
}