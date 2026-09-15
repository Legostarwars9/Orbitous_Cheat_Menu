using System.Collections.Generic;
using UnityEngine;

public static class EnemyCheats
{
    public static waveManager GetWaveManager()
    {
        player p = player.Instance;

        if (p == null)
            return null;

        return Object.FindObjectOfType<waveManager>();
    }

    public static List<GameObject> GetEnemies()
    {
        waveManager waves =
            GetWaveManager();

        if (waves == null ||
            waves.enemyPool == null)
        {
            return new List<GameObject>();
        }

        return waves.enemyPool;
    }

    public static List<GameObject> GetMinibosses()
    {
        waveManager waves =
            GetWaveManager();

        List<GameObject> result =
            new List<GameObject>();

        if (waves == null ||
            waves.minibossPool == null)
        {
            return result;
        }

        foreach (GameObject obj in waves.minibossPool)
        {
            if (obj != null)
                result.Add(obj);
        }

        return result;
    }

    public static void SpawnEnemy(
        int index,
        int amount
    )
    {
        waveManager waves =
            GetWaveManager();

        player p =
            player.Instance;

        if (waves == null ||
            p == null ||
            waves.spawner == null ||
            waves.enemyPool == null ||
            waves.enemyPool.Count == 0)
        {
            return;
        }

        index = Mathf.Clamp(
            index,
            0,
            waves.enemyPool.Count - 1
        );

        GameObject prefab =
            waves.enemyPool[index];

        for (int i = 0; i < amount; i++)
        {
            Vector2 position =
                (Vector2)p.transform.position +
                spawner.RandomPos(
                    1000f,
                    2000f
                );

            waves.spawner.Spawn(
                prefab,
                position
            );
        }
    }

    public static void SpawnMiniboss(
        int index
    )
    {
        waveManager waves =
            GetWaveManager();

        player p =
            player.Instance;

        if (waves == null ||
            p == null ||
            waves.spawner == null ||
            waves.minibossPool == null ||
            waves.minibossPool.Length == 0)
        {
            return;
        }

        index = Mathf.Clamp(
            index,
            0,
            waves.minibossPool.Length - 1
        );

        GameObject prefab =
            waves.minibossPool[index];

        Vector2 position =
            (Vector2)p.transform.position +
            spawner.RandomPos(
                1000f,
                2000f
            );

        GameObject obj =
            waves.spawner.Spawn(
                prefab,
                position
            );

        if (obj != null)
        {
            kickStart kick =
                obj.GetComponent<kickStart>();

            if (kick != null)
            {
                kick.parent = waves.blackHole;
            }
        }
    }
}