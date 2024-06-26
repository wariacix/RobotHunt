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
        GameManager.Instance.buttons[0].onClick.AddListener(delegate { BuyAmmo(100, 90, 0); });
        GameManager.Instance.buttons[1].onClick.AddListener(delegate { BuyAmmo(150, 90, 1); });
        GameManager.Instance.buttons[2].onClick.AddListener(delegate { BuyAmmo(250, 20, 2); });
        GameManager.Instance.buttons[3].onClick.AddListener(delegate { BuyAmmo(350, 10, 3); });
        GameManager.Instance.buttons[4].onClick.AddListener(delegate { BuyAmmo(400, 5, 4); });
        GameManager.Instance.buttons[5].onClick.AddListener(Buy6);
        GameManager.Instance.buttons[6].onClick.AddListener(Buy7);
        GameManager.Instance.buttons[7].onClick.AddListener(Buy8);
    }

    [ClientCallback]
    public void BuyAmmo(int price, int amount, int weaponIndex)
    {
        if (gold >= price)
        {
            ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
            if (controller.weapons.Count > weaponIndex)
            {
                AddAmmoCmd(weaponIndex, amount);
                RemoveGoldCmd(price);
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
    private void AddAmmoCmd(int weaponIndex, int amount)
    {
        ShootingComponent controller = gameObject.GetComponent<ShootingComponent>();
        controller.weapons[weaponIndex].ammo += amount;
    }

    [Command]
    void RemoveGoldCmd(int gold)
    {
        this.gold -= gold;
    }
}