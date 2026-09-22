using System;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    private bool visible;

    private Rect windowRect =
        new Rect(20f, 20f, 470f, 700f);

    private int tab;

    private float healthValue = 100f;
    private float fuelValue = 100f;
    private int scrapAmount = 1000;
    private int enemyAmount = 1;
    private int selectedEnemy;
    private int selectedMiniboss;
    private int selectedProjectile;
    private int selectedModifier;
    private int selectedBoss;

    private string[] tabs =
    {
        "Player",
        "Weapons",
        "Enemies",
        "Waves",
        "Inventory",
        "Bosses"
    };

    public void Toggle()
    {
        visible = !visible;

        if (visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnGUI()
    {
        if (!visible)
            return;

        windowRect = GUI.Window(
            7654321,
            windowRect,
            DrawWindow,
            "Orbitous Cheats"
        );
    }

    private void DrawWindow(int id)
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < tabs.Length; i++)
        {
            if (GUILayout.Button(tabs[i], GUILayout.Height(30f)))
            {
                tab = i;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        switch (tab)
        {
            case 0:
                DrawPlayer();
                break;

            case 1:
                DrawWeapons();
                break;

            case 2:
                DrawEnemies();
                break;

            case 3:
                DrawWaves();
                break;

            case 4:
                DrawInventory();
                break;

            case 5:
                DrawBosses();
                break;
        }

        GUILayout.Space(10f);

        GUILayout.Label("F10 - Toggle menu");

        GUI.DragWindow(
            new Rect(0f, 0f, 10000f, 25f)
        );
    }

    private void DrawPlayer()
    {
        player p = PlayerCheats.GetPlayer();

        if (p == null)
        {
            GUILayout.Label("Player not found.");
            return;
        }

        GUILayout.Label(
            "Health: " +
            p.hp.ToString("0.0") +
            " / " +
            p.hpMax.ToString("0.0")
        );

        GUILayout.Label(
            "Fuel: " +
            p.fuel.ToString("0.0") +
            " / " +
            p.fuelCap.ToString("0.0")
        );

        healthValue = FloatField(
            "Health",
            healthValue
        );

        if (GUILayout.Button("Set Health"))
            PlayerCheats.SetHealth(healthValue);

        fuelValue = FloatField(
            "Fuel",
            fuelValue
        );

        if (GUILayout.Button("Set Fuel"))
            PlayerCheats.SetFuel(fuelValue);

        GUILayout.Space(10f);

        if (GUILayout.Button("Full Health"))
            PlayerCheats.FullHealth();

        if (GUILayout.Button("Full Fuel"))
            PlayerCheats.FullFuel();

        

        GUILayout.Space(10f);

        PlayerCheats.infiniteHealth =
            GUILayout.Toggle(
                PlayerCheats.infiniteHealth,
                "Infinite Health"
            );

        PlayerCheats.infiniteFuel =
            GUILayout.Toggle(
                PlayerCheats.infiniteFuel,
                "Infinite Fuel"
            );

        PlayerCheats.infiniteBoost =
            GUILayout.Toggle(
                PlayerCheats.infiniteBoost,
                "Infinite Boost"
            );
    }

    private void DrawWeapons()
    {
        shooterWithLoadout shooter =
            WeaponCheats.GetShooter();

        if (shooter == null)
        {
            GUILayout.Label("Weapon system not found.");
            return;
        }

        GUILayout.Label(
            "Heat Loss Speed: " +
            shooter.heatLossSpeed.ToString("0.00")
        );

        shooter.heatLossSpeed =
            FloatField(
                "Heat Loss",
                shooter.heatLossSpeed
            );

        WeaponCheats.noHeat =
            GUILayout.Toggle(
                WeaponCheats.noHeat,
                "No Heat"
            );

        WeaponCheats.noCooldown =
            GUILayout.Toggle(
                WeaponCheats.noCooldown,
                "No Cooldown"
            );
        
    }

    private void DrawEnemies()
    {
        List<GameObject> enemies =
            EnemyCheats.GetEnemies();

        if (enemies.Count == 0)
        {
            GUILayout.Label("No regular enemy prefabs found.");
        }
        else
        {
            selectedEnemy = Mathf.Clamp(
                selectedEnemy,
                0,
                enemies.Count - 1
            );

            GUILayout.Label(
                "Enemy: " +
                enemies[selectedEnemy].name
            );

            if (GUILayout.Button(
                "Previous Enemy"
            ))
            {
                selectedEnemy--;

                if (selectedEnemy < 0)
                    selectedEnemy = enemies.Count - 1;
            }

            if (GUILayout.Button(
                "Next Enemy"
            ))
            {
                selectedEnemy++;

                if (selectedEnemy >= enemies.Count)
                    selectedEnemy = 0;
            }

            enemyAmount = Mathf.Max(
                1,
                IntField(
                    "Amount",
                    enemyAmount
                )
            );

            if (GUILayout.Button("Spawn Enemy"))
            {
                EnemyCheats.SpawnEnemy(
                    selectedEnemy,
                    enemyAmount
                );
            }
        }

        GUILayout.Space(15f);

        List<GameObject> minibosses =
            EnemyCheats.GetMinibosses();

        if (minibosses.Count > 0)
        {
            selectedMiniboss = Mathf.Clamp(
                selectedMiniboss,
                0,
                minibosses.Count - 1
            );

            GUILayout.Label(
                "Miniboss: " +
                minibosses[selectedMiniboss].name
            );

            if (GUILayout.Button("Previous Miniboss"))
            {
                selectedMiniboss--;

                if (selectedMiniboss < 0)
                    selectedMiniboss = minibosses.Count - 1;
            }

            if (GUILayout.Button("Next Miniboss"))
            {
                selectedMiniboss++;

                if (selectedMiniboss >= minibosses.Count)
                    selectedMiniboss = 0;
            }

            if (GUILayout.Button("Spawn Miniboss"))
            {
                EnemyCheats.SpawnMiniboss(
                    selectedMiniboss
                );
            }
        }
    }

    private void DrawWaves()
    {
        waveManager waves =
            WaveCheats.GetWaveManager();

        if (waves == null)
        {
            GUILayout.Label("Wave manager not found.");
            return;
        }

        GUILayout.Label(
            "Current Wave: " +
            waves.wave
        );

        GUILayout.Label(
            "Enemies Remaining: " +
            waves.enemiesLeftToSpawn
        );

        GUILayout.Label(
            "Time Until Next Wave: " +
            waves.secondsUntilNext
        );

        waves.spawnEnemies =
            GUILayout.Toggle(
                waves.spawnEnemies,
                "Enable Enemy Spawning"
            );

        if (GUILayout.Button("Start Wave"))
            WaveCheats.StartWave();

        if (GUILayout.Button("Stop Waves"))
            WaveCheats.StopWaves();

        if (GUILayout.Button("Reset Wave Cycle"))
            WaveCheats.ResetWaveCycle();

        GUILayout.Space(10f);

        if (GUILayout.Button("Spawn Random Enemy"))
            WaveCheats.SpawnRandomEnemy();
    }

    private void DrawInventory()
    {
        shooterWithLoadout shooter =
            WeaponCheats.GetShooter();

        if (shooter == null)
        {
            GUILayout.Label("Inventory not found.");
            return;
        }

        List<string> projectiles =
            InventoryCheats.GetProjectileNames();

        if (projectiles.Count > 0)
        {
            selectedProjectile = Mathf.Clamp(
                selectedProjectile,
                0,
                projectiles.Count - 1
            );

            GUILayout.Label(
                "Projectile: " +
                projectiles[selectedProjectile]
            );

            if (GUILayout.Button("Previous Projectile"))
            {
                selectedProjectile--;

                if (selectedProjectile < 0)
                    selectedProjectile = projectiles.Count - 1;
            }

            if (GUILayout.Button("Next Projectile"))
            {
                selectedProjectile++;

                if (selectedProjectile >= projectiles.Count)
                    selectedProjectile = 0;
            }

            if (GUILayout.Button("Give Projectile"))
            {
                InventoryCheats.GiveProjectile(
                    selectedProjectile
                );
            }
        }

        GUILayout.Space(15f);

        List<string> modifiers =
            InventoryCheats.GetModifierNames();

        if (modifiers.Count > 0)
        {
            selectedModifier = Mathf.Clamp(
                selectedModifier,
                0,
                modifiers.Count - 1
            );

            GUILayout.Label(
                "Modifier: " +
                modifiers[selectedModifier]
            );

            if (GUILayout.Button("Previous Modifier"))
            {
                selectedModifier--;

                if (selectedModifier < 0)
                    selectedModifier = modifiers.Count - 1;
            }

            if (GUILayout.Button("Next Modifier"))
            {
                selectedModifier++;

                if (selectedModifier >= modifiers.Count)
                    selectedModifier = 0;
            }

            if (GUILayout.Button("Give Modifier"))
            {
                InventoryCheats.GiveModifier(
                    selectedModifier
                );
            }
        }
        GUILayout.Space(10);
        GUILayout.Label("Money");

        GUILayout.Label("Current Scraps: " + InventoryCheats.GetScraps());

        scrapAmount = IntField("Amount", scrapAmount);

        if (GUILayout.Button("Give Scraps"))
        {
            InventoryCheats.GiveScraps(scrapAmount);
        }
        GUILayout.Space(10);
        GUILayout.Label("Upgrades", GUI.skin.box);

        List<string> upgrades = InventoryCheats.GetUpgradeNames();

        for (int i = 0; i < upgrades.Count; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(upgrades[i], GUILayout.Width(140));

            float value = InventoryCheats.GetUpgradeValue(i);

            string valueText = value.ToString("0.##");
            string newValueText = GUILayout.TextField(valueText);

            if (float.TryParse(newValueText, out float newValue))
            {
                if (Mathf.Abs(newValue - value) > 0.001f)
                {
                    InventoryCheats.SetUpgradeValue(i, newValue);
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    private void DrawBosses()
    {
        string[] bosses =
        {
            "Plant",
            "Worm",
            "Hunter",
            "Planet",
            "Shadow",
            "Materiaphage"
        };

        GUILayout.Label(
            "Boss: " +
            bosses[selectedBoss]
        );

        if (GUILayout.Button("Previous Boss"))
        {
            selectedBoss--;

            if (selectedBoss < 0)
                selectedBoss = bosses.Length - 1;
        }

        if (GUILayout.Button("Next Boss"))
        {
            selectedBoss++;

            if (selectedBoss >= bosses.Length)
                selectedBoss = 0;
        }

        if (GUILayout.Button("Summon Boss"))
        {
            BossCheats.Summon(selectedBoss);
        }
    }

    private float FloatField(
        string label,
        float value
    )
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(label, GUILayout.Width(120f));

        string text =
            GUILayout.TextField(
                value.ToString(),
                GUILayout.Width(100f)
            );

        float result;

        if (!float.TryParse(
            text,
            out result
        ))
        {
            result = value;
        }

        GUILayout.EndHorizontal();

        return result;
    }

    private int IntField(
        string label,
        int value
    )
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(label, GUILayout.Width(120f));

        string text =
            GUILayout.TextField(
                value.ToString(),
                GUILayout.Width(100f)
            );

        int result;

        if (!int.TryParse(
            text,
            out result
        ))
        {
            result = value;
        }

        GUILayout.EndHorizontal();

        return result;
    }
}