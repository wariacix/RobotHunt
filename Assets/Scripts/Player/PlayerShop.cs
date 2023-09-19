using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class PlayerShop : NetworkBehaviour
{
    [SerializeField] private List<GameObject> shop;
    [HideInInspector][SyncVar] public int gold = 0;
    public bool isPlacing;

    private void Start()
    {
        if (!isLocalPlayer) return;
        GameManager.Instance.buttons[0].onClick.AddListener(Buy1);
        GameManager.Instance.buttons[1].onClick.AddListener(Buy2);
        GameManager.Instance.buttons[2].onClick.AddListener(Buy3);
        GameManager.Instance.buttons[3].onClick.AddListener(Buy4);
        GameManager.Instance.buttons[4].onClick.AddListener(Buy5);
        GameManager.Instance.buttons[5].onClick.AddListener(Buy6);
        GameManager.Instance.buttons[6].onClick.AddListener(Buy7);
        GameManager.Instance.buttons[7].onClick.AddListener(Buy8);
    }

    [ClientCallback]
    public void Buy1()
    {
        if (gold >= 100)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > 0)
            {
                controller.weapons[0].ammo += 90;
                RemoveGoldCmd(100);
            }
        }
    }
    [ClientCallback]
    public void Buy2()
    {
        if (gold >= 150)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > 1)
            {
                controller.weapons[1].ammo += 90;
                RemoveGoldCmd(150);
            }
        }
    }
    [ClientCallback]
    public void Buy3()
    {
        if (gold >= 250)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > 2)
            {
                controller.weapons[2].ammo += 20;
                RemoveGoldCmd(250);
            }
        }
    }
    [ClientCallback]
    public void Buy4()
    {
        if (gold >= 350)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > 3)
            {
                controller.weapons[3].ammo += 10;
                RemoveGoldCmd(350);
            }
        }
    }
    [ClientCallback]
    public void Buy5()
    {
        if (gold >= 400)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > 4)
            {
                controller.weapons[4].ammo += 5;
                RemoveGoldCmd(400);
            }
        }
    }
    [ClientCallback]
    public void Buy6()
    {
        if (gold >= 800 && isPlacing == false)
        {
            isPlacing = true;
            GameObject prefab = Instantiate(shop[0]);
            BoughtPlaceable placeable = prefab.AddComponent<BoughtPlaceable>();
            placeable.price = 800;
            placeable.ownerPlayerId = NetworkClient.localPlayer.netId;
            placeable.prefabIndex = NetworkManager.singleton.spawnPrefabs.FindIndex(o => o == shop[0]);
            RemoveGoldCmd(800);
        }
    }
    [ClientCallback]
    public void Buy7()
    {
        if (gold >= 1650 && isPlacing == false)
        {
            isPlacing = true;
            GameObject prefab = Instantiate(shop[1]);
            BoughtPlaceable placeable = prefab.AddComponent<BoughtPlaceable>();
            placeable.price = 1650;
            placeable.ownerPlayerId = NetworkClient.localPlayer.netId;
            placeable.prefabIndex = NetworkManager.singleton.spawnPrefabs.FindIndex(o => o == shop[1]);
            RemoveGoldCmd(1650);
        }
    }
    [ClientCallback]
    public void Buy8()
    {
        if (gold >= 2500 && isPlacing == false)
        {
            isPlacing = true;
            GameObject prefab = Instantiate(shop[2]);
            BoughtPlaceable placeable = prefab.AddComponent<BoughtPlaceable>();
            placeable.price = 2500;
            placeable.ownerPlayerId = NetworkClient.localPlayer.netId;
            placeable.prefabIndex = NetworkManager.singleton.spawnPrefabs.FindIndex(o => o == shop[2]);
            RemoveGoldCmd(2500);
        }
    }

    [Command]
    void RemoveGoldCmd(int gold)
    {
        this.gold -= gold;
    }
}