using Mirror;
using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoughtPlaceable : NetworkBehaviour
{
    public int price;
    private bool isPlaceable = true;
    [HideInInspector] public uint ownerPlayerId;
    public int prefabIndex;

    private void Awake()
    {
        Turret turret = gameObject.GetComponent<Turret>();
        if (turret != null)
        {
            turret.enabled = false;
            turret.ownerPlayerId = ownerPlayerId;
        }
    }

    [ClientCallback]
    private void Update()
    {
        transform.position = UIManager.Instance.CameraInstance.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (isPlaceable == true)
        {
            spriteRenderer.color = Color.green;

            if (Input.GetButtonDown("Fire1"))
            {
                NetworkClient.localPlayer.gameObject.GetComponent<PlayerShop>().isPlacing = false;
                //NavMeshSurface surface = GameManager.Instance.NavMeshInstance.GetComponent<NavMeshSurface>();
                //surface.BuildNavMeshAsync();
                spriteRenderer.color = Color.white;
                SpawnCmd(prefabIndex);
                Destroy(gameObject.GetComponent<BoughtPlaceable>());
            }
        }
        else if (isPlaceable == false)
        {
            spriteRenderer.color = Color.red;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerShop>().gold += price;
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerShop>().isPlacing = false;
            Destroy(gameObject);
        }
    }

    [Command (requiresAuthority = false)]
    void SpawnCmd(int prefabIndex)
    {
        GameObject prefab = Instantiate(NetworkManager.singleton.spawnPrefabs[prefabIndex]);

        Turret turret = prefab.GetComponent<Turret>();
        if (turret != null)
        {
            turret.enabled = true;
        }

        NetworkServer.Spawn(prefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlaceable = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlaceable = true;
    }
}
