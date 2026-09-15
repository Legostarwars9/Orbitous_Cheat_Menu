using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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

    public static player GetPlayer()
    {
        return player.Instance;
    }

    public static List<string> GetUpgradeNames()
    {
        return new List<string>
        {
            "Max Health",
            "Fuel Capacity",
            "Boost Recharge",
            "Heat Loss",
            "Solar Panels",
            "Sub Weapon Slots",
            "Fuel Insurance",
            "Mass",
            "Solar Sail"
        };
    }

    public static float GetUpgradeValue(int id)
    {
        player p = GetPlayer();

        if (p == null)
            return 0f;

        shooterWithLoadout shooter = p.shooter;

        switch (id)
        {
            case 0:
                return p.hpMax;

            case 1:
                return p.fuelCap;

            case 2:
                return shooter != null
                    ? shooter.boostRechargeSpeed
                    : 0f;

            case 3:
                return shooter != null
                    ? shooter.heatLossSpeed
                    : 0f;

            case 4:
                return p.solarPanels;

            case 5:
                return shooter != null
                    ? shooter.subSlotCount
                    : 0f;

            case 6:
                return p.fuelInsuranceLevel;

            case 7:
                Rigidbody2D rb = GetPlayerRigidbody(p);

                return rb != null
                    ? rb.mass
                    : 0f;

            case 8:
                return p.solarSailLevel;

            default:
                return 0f;
        }
    }

    public static void SetUpgradeValue(int id, float value)
    {
        player p = GetPlayer();

        if (p == null)
            return;

        shooterWithLoadout shooter = p.shooter;

        switch (id)
        {
            case 0:
                p.hpMax = Mathf.Max(0f, value);
                break;

            case 1:
                p.fuelCap = Mathf.Max(0f, value);
                break;

            case 2:
                if (shooter != null)
                    shooter.boostRechargeSpeed = Mathf.Max(0f, value);
                break;

            case 3:
                if (shooter != null)
                    shooter.heatLossSpeed = Mathf.Max(0f, value);
                break;

            case 4:
                p.solarPanels =
                    Mathf.Max(
                        0,
                        Mathf.RoundToInt(value)
                    );
                break;

            case 5:
                if (shooter != null)
                {
                    shooter.subSlotCount =
                        Mathf.Max(
                            0,
                            Mathf.RoundToInt(value)
                        );
                }
                break;

            case 6:
                p.fuelInsuranceLevel =
                    Mathf.Max(
                        0,
                        Mathf.RoundToInt(value)
                    );
                break;

            case 7:
                Rigidbody2D rb = GetPlayerRigidbody(p);

                if (rb != null)
                {
                    rb.mass =
                        Mathf.Max(
                            0.01f,
                            value
                        );
                }

                break;

            case 8:
                p.solarSailLevel =
                    Mathf.Max(
                        0,
                        Mathf.RoundToInt(value)
                    );
                break;
        }
    }

    private static Rigidbody2D GetPlayerRigidbody(player p)
    {
        upgradeStats upgrades =
            p.GetComponent<upgradeStats>();

        if (upgrades != null &&
            upgrades.playerRb != null)
        {
            return upgrades.playerRb;
        }

        return p.GetComponent<Rigidbody2D>();
    }
    public static int GetScraps()
    {
        player p = GetPlayer();

        if (p == null)
            return 0;

        return p.scraps;
    }

    public static void GiveScraps(int amount)
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.scraps += amount;
    }
}