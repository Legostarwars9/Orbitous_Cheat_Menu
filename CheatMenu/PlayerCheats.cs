using UnityEngine;

public static class PlayerCheats
{
    public static bool infiniteHealth;
    public static bool infiniteFuel;
    public static bool infiniteBoost;

    public static player GetPlayer()
    {
        return player.Instance;
    }

    public static void SetHealth(float value)
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.hp = Mathf.Clamp(
            value,
            0f,
            p.hpMax
        );
    }

    public static void SetFuel(float value)
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.fuel = Mathf.Clamp(
            value,
            0f,
            p.fuelCap
        );
    }

    public static void FullHealth()
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.hp = p.hpMax;
        p.dead = false;
    }

    public static void FullFuel()
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.fuel = p.fuelCap;
    }

    public static void FullBoost()
    {
        player p = GetPlayer();

        if (p == null)
            return;

        p.boostDuration = p.boostCap;
    }

    public static void Update()
    {
        player p = GetPlayer();

        if (p == null)
            return;

        if (infiniteHealth)
        {
            p.hp = p.hpMax;
            p.dead = false;
        }

        if (infiniteFuel)
        {
            p.fuel = p.fuelCap;
        }

        if (infiniteBoost)
        {
            p.boostDuration = p.boostCap;
        }
    }
}