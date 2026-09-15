using System.Reflection;
using UnityEngine;

public static class WeaponCheats
{
    public static bool noHeat;
    public static bool noCooldown;

    private static readonly BindingFlags Flags =
        BindingFlags.Instance |
        BindingFlags.NonPublic;

    public static shooterWithLoadout GetShooter()
    {
        player p = player.Instance;

        if (p == null)
            return null;

        return p.shooter;
    }

    public static void Update()
    {
        shooterWithLoadout shooter =
            GetShooter();

        if (shooter == null)
            return;

        if (noHeat)
        {
            ClearHeat();
        }

        if (noCooldown)
        {
            ClearCooldowns();
        }
    }

    public static void ClearHeat()
    {
        shooterWithLoadout shooter =
            GetShooter();

        if (shooter == null)
            return;

        FieldInfo heatField =
            typeof(shooterWithLoadout)
                .GetField(
                    "heat",
                    Flags
                );

        FieldInfo overheatField =
            typeof(shooterWithLoadout)
                .GetField(
                    "overheated",
                    Flags
                );

        float[] heat =
            heatField?.GetValue(shooter)
            as float[];

        bool[] overheated =
            overheatField?.GetValue(shooter)
            as bool[];

        if (heat != null)
        {
            for (int i = 0; i < heat.Length; i++)
            {
                heat[i] = 0f;
            }
        }

        if (overheated != null)
        {
            for (int i = 0; i < overheated.Length; i++)
            {
                overheated[i] = false;
            }
        }
    }

    public static void ClearCooldowns()
    {
        shooterWithLoadout shooter =
            GetShooter();

        if (shooter == null)
            return;

        FieldInfo delayField =
            typeof(shooterWithLoadout)
                .GetField(
                    "shootDelay",
                    Flags
                );

        float[] delays =
            delayField?.GetValue(shooter)
            as float[];

        if (delays != null)
        {
            for (int i = 0; i < delays.Length; i++)
            {
                delays[i] = 0f;
            }
        }

        shooter.nextDelay = 0f;
    }
}