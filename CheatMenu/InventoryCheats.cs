using System;
using System.Collections.Generic;
using System.Reflection;

public static class InventoryCheats
{
    private static readonly BindingFlags Flags =
        BindingFlags.Instance |
        BindingFlags.NonPublic;

    private static shooterWithLoadout GetShooter()
    {
        return WeaponCheats.GetShooter();
    }

    public static List<string> GetProjectileNames()
    {
        shooterWithLoadout shooter =
            GetShooter();

        List<string> names =
            new List<string>();

        if (shooter == null ||
            shooter.projectiles == null)
        {
            return names;
        }

        foreach (projectile proj in shooter.projectiles)
        {
            if (proj == null)
            {
                names.Add("NULL");
            }
            else
            {
                names.Add(proj.gameObject.name);
            }
        }

        return names;
    }

    public static List<string> GetModifierNames()
    {
        shooterWithLoadout shooter =
            GetShooter();

        List<string> names =
            new List<string>();

        if (shooter == null)
            return names;

        FieldInfo field =
            typeof(shooterWithLoadout)
                .GetField(
                    "modItems",
                    Flags
                );

        itemPool pool =
            field?.GetValue(shooter)
            as itemPool;

        if (pool == null ||
            pool.names == null)
        {
            return names;
        }

        foreach (string name in pool.names)
        {
            names.Add(name);
        }

        return names;
    }

    public static void GiveProjectile(int id)
    {
        shooterWithLoadout shooter =
            GetShooter();

        if (shooter == null ||
            shooter.projectiles == null ||
            id < 0 ||
            id >= shooter.projectiles.Length)
        {
            return;
        }

        FieldInfo field =
            typeof(shooterWithLoadout)
                .GetField(
                    "projectilesInventory",
                    Flags
                );

        List<int> inventory =
            field?.GetValue(shooter)
            as List<int>;

        if (inventory == null)
            return;

        inventory.Add(id);

        shooter.AddProjectile(id);
    }

    public static void GiveModifier(int id)
    {
        shooterWithLoadout shooter =
            GetShooter();

        if (shooter == null)
            return;

        FieldInfo field =
            typeof(shooterWithLoadout)
                .GetField(
                    "modificationsInventory",
                    Flags
                );

        List<int> inventory =
            field?.GetValue(shooter)
            as List<int>;

        if (inventory == null)
            return;

        inventory.Add(id);

        shooter.AddModifier(id);
    }
}