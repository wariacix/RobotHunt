using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhp = 100;
    public int hp = 100;


    public void Hit(int damage)
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

                PlayerShop shop = GameManager.Instance.PlayerObject.GetComponent<PlayerShop>();
                shop.gold += (int)(enemy.reward);

                SpawnMarkers(enemy.reward);
            }
            Destroy(gameObject);
        }
    }

    private void SpawnMarkers(int reward)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = transform.position;
        markerScr.SetString("+" + reward + "G");
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
