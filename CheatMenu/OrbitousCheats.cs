using BepInEx;
using UnityEngine;

[BepInPlugin("legostarwars9.orbitouscheats", "Orbitous Cheats", "0.1.0")]
public class OrbitousCheats : BaseUnityPlugin
{
    internal static OrbitousCheats Instance;

    private CheatMenu menu;

    private void Awake()
    {
        Instance = this;

        GameObject obj = new GameObject("OrbitousCheats");
        DontDestroyOnLoad(obj);

        menu = obj.AddComponent<CheatMenu>();

        Logger.LogInfo("Orbitous Cheats loaded.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            menu.Toggle();
        }
        PlayerCheats.Update();
        WeaponCheats.Update();
    }

    private void OnDestroy()
    {
        if (menu != null)
        {
            Destroy(menu.gameObject);
        }
    }
}