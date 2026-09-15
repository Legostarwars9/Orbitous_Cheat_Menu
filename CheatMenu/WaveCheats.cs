using UnityEngine;

public static class WaveCheats
{
    public static waveManager GetWaveManager()
    {
        return Object.FindObjectOfType<waveManager>();
    }

    public static void StartWave()
    {
        waveManager waves =
            GetWaveManager();

        if (waves == null)
            return;

        if (waves.enemySizes == null ||
            waves.enemySizes.Length == 0)
        {
            waves.IntroduceWaves();
            return;
        }

        waves.StartWave();
    }

    public static void StopWaves()
    {
        waveManager waves =
            GetWaveManager();

        if (waves == null)
            return;

        waves.EndWaves();
        waves.spawnEnemies = false;
        waves.enemiesLeftToSpawn = 0;
    }

    public static void ResetWaveCycle()
    {
        waveManager waves =
            GetWaveManager();

        if (waves == null)
            return;

        waves.ResetWaveCycle();
    }

    public static void SpawnRandomEnemy()
    {
        waveManager waves =
            GetWaveManager();

        if (waves == null)
            return;

        waves.SpawnRandomEnemy();
    }
}