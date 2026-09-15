using UnityEngine;
public static class BossCheats
{
    public static bossManager GetBossManager()
    {
        return bossManager.Instance;
    }

    public static void Summon(int id)
    {
        bossManager boss =
            GetBossManager();

        if (boss == null)
            return;

        switch (id)
        {
            case 0:
                SummonPlant(boss);
                break;

            case 1:
                boss.SummonWormBoss();
                break;

            case 2:
                boss.SummonHunterBoss();
                break;

            case 3:
                boss.SummonPlanetBoss();
                break;

            case 4:
                boss.SummonShadowBoss();
                break;

            case 5:
                SummonMat(boss);
                break;
        }
    }

    private static void SummonPlant(
        bossManager boss
    )
    {
        player p =
            player.Instance;

        if (p == null)
            return;

        Vector2 position =
            (Vector2)p.transform.position +
            spawner.RandomPos(1500f);

        GameObject planet =
            boss.plantBossPlanetAtmosphere;

        if (planet == null)
        {
            Debug.Log(
                "[Orbitous Cheats] Plant boss requires a planet."
            );

            return;
        }

        boss.SummonPlantBoss(
            position,
            p.rb.velocity,
            planet
        );
    }

    private static void SummonMat(
        bossManager boss
    )
    {
        player p =
            player.Instance;

        if (p == null)
            return;

        Vector2 position =
            (Vector2)p.transform.position +
            spawner.RandomPos(1500f);

        boss.SummonMatBoss(
            position,
            p.rb.velocity
        );
    }
}