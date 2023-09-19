using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SyncVar] public int maxhp = 100;
    [SyncVar] public int hp = 100;

    [ServerCallback]
    private void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            maxhp += (int)(maxhp * 1f * (NetworkServer.connections.Count - 1));
            hp += (int)(hp * 1f * (NetworkServer.connections.Count - 1));
        }
    }

    [ServerCallback]
    public void Hit(int damage)
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        hp -= damage;
        if (hp < 1)
        {
            if (gameObject.tag == "Enemy")
            {
                Enemy enemy = gameObject.GetComponent<Enemy>();
                WaveManager.Instance.enemies.Remove(this.gameObject);

                ClientSpawnMarkers(enemy.reward, NetworkServer.connections.Count);
                ServerSpawnMarkers(enemy.reward, NetworkServer.connections.Count);
            }
            Destroy(gameObject);
        }
    }

    [ServerCallback]
    public void Hit(int damage, uint playerNetId)
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        hp -= damage;
        if (hp <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                Enemy enemy = gameObject.GetComponent<Enemy>();
                WaveManager.Instance.enemies.Remove(this.gameObject);
                for (int i = 0; i < NetworkServer.connections.Count; i++)
                {
                    if (NetworkServer.connections[i].identity.netId == playerNetId)
                    {
                        PlayerShop shop = NetworkServer.connections[i].identity.gameObject.GetComponent<PlayerShop>();
                        shop.gold += (int)(0.5f * enemy.reward + (enemy.reward * 0.5f * (NetworkServer.connections.Count)));
                    }
                }
                ClientSpawnMarkers(enemy.reward, NetworkServer.connections.Count);
                ServerSpawnMarkers(enemy.reward, NetworkServer.connections.Count);
            }
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    private void ClientSpawnMarkers(int reward, int connections)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = transform.position;
        markerScr.SetString("+" + (int)(0.5f * reward + (reward * 0.5f * (connections))) + "G");
        markerScr.color = Color.yellow;
    }

    [ServerCallback]
    private void ServerSpawnMarkers(int reward, int connections)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = transform.position;
        markerScr.SetString("+" + (int)(0.5f * reward + (reward * 0.5f * (connections))) + "G");
        markerScr.color = Color.yellow;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hp < 1)
        {
            if (gameObject.tag == "Enemy")
            {
                WaveManager.Instance.enemies.Remove(this.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
