// GangManager.cs
using Newtonsoft.Json;
using RDR2;
using RDR2.Native;
using RDR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

public class GangManager
{
    private readonly List<GangMember> _members = new List<GangMember>();
    private GangOrder _order = GangOrder.FollowPlayer;
    private CombatStyle _style = CombatStyle.RangedShort;

    public static GangManager Load(string path)
    {
        try
        {
            if (!File.Exists(path))
                return new GangManager();

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GangManager>(json) ?? new GangManager();
        }
        catch
        {
            return new GangManager();
        }
    }

    public void Save(string path)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        catch
        {
        }
    }

    public void Recruit(Ped ped)
    {
        if (ped == null || !ped.Exists())
            return;

        if (_members.Any(x => x.PedHandle == ped.Handle))
            return;

        ped.Task.ClearAll();
        ped.BlockPermanentEvents = true;
        ped.IsPersistent = true;

        _members.Add(new GangMember(ped));
    }

    public void IssueOrder(GangOrder order)
    {
        _order = order;
    }

    public void SetCombatStyle(CombatStyle style)
    {
        _style = style;
        foreach (GangMember member in _members)
        {
            if (member.Ped != null && member.Ped.Exists())
                member.Style = style;
        }
    }

    public void Update()
    {
        Ped player = Game.Player.Character;
        if (player == null || !player.Exists())
            return;

        Cleanup();

        foreach (GangMember member in _members)
        {
            Ped ped = member.Ped;
            if (ped == null || !ped.Exists())
                continue;

            if (ped.IsDead)
                continue;

            ApplyStyle(ped, member.Style);

            if (_order == GangOrder.WaitHere)
            {
                ped.Task.StandStill(-1);
                continue;
            }

            if (_order == GangOrder.FollowPlayer)
            {
                float dist = ped.Position.DistanceTo(player.Position);
                if (dist > 4.0f)
                {
                    ped.Task.FollowToOffsetFromEntity(
                        player,
                        new Vector3(1.5f, -1.5f, 0f),
                        1.5f,
                        -1,
                        2.0f,
                        true
                    );
                }
            }
        }
    }

    private void ApplyStyle(Ped ped, CombatStyle style)
    {
        switch (style)
        {
            case CombatStyle.NonLethalMelee:
                GiveWeaponIfMissing(ped, WeaponHash.Unarmed);
                break;

            case CombatStyle.LethalMelee:
                GiveWeaponIfMissing(ped, WeaponHash.Knife);
                break;

            case CombatStyle.RangedShort:
                GiveWeaponIfMissing(ped, WeaponHash.RevolverCattleman);
                break;

            case CombatStyle.RangedMedium:
                GiveWeaponIfMissing(ped, WeaponHash.Bow);
                break;

            case CombatStyle.RangedLong:
                GiveWeaponIfMissing(ped, WeaponHash.RifleBoltAction);
                break;

            case CombatStyle.Stealth:
                GiveWeaponIfMissing(ped, WeaponHash.Knife);
                break;
        }
    }

    private static void GiveWeaponIfMissing(Ped ped, WeaponHash weapon)
    {
        if (ped.Weapons == null)
            return;

        if (!ped.Weapons.HasWeapon(weapon))
            ped.Weapons.Give(weapon, 1, true, true);
    }

    private void Cleanup()
    {
        _members.RemoveAll(m =>
        {
            Ped ped = m.Ped;
            return ped == null || !ped.Exists() || ped.IsDead;
        });
    }

    public static Ped GetNearestRecruitablePed(float radius)
    {
        Ped player = Game.Player.Character;
        if (player == null || !player.Exists())
            return null;

        Ped nearest = null;
        float best = radius;

        foreach (Ped ped in World.GetAllPeds())
        {
            if (ped == null || !ped.Exists())
                continue;

            if (ped == player)
                continue;

            if (ped.IsDead)
                continue;

            float dist = ped.Position.DistanceTo(player.Position);
            if (dist < best)
            {
                best = dist;
                nearest = ped;
            }
        }

        return nearest;
    }
}