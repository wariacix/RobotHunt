using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{
    public GameObject hitEffect;
    public int bulletDamage;
    public Color effectColor;

    public GameObject audioObject;

    [HideInInspector] public uint playerNetId;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(20, 20);
    }

    [ServerCallback]
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            Physics2D.IgnoreLayerCollision(collision.gameObject.layer, gameObject.layer);
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Health enemyHp = collision.gameObject.GetComponent<Health>();
                enemyHp.Hit(bulletDamage, playerNetId);
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            SpriteRenderer sprEff = effect.GetComponent<SpriteRenderer>();
            sprEff.color = effectColor;
            NetworkIdentity identity = effect.GetComponent<NetworkIdentity>();
            NetworkServer.Spawn(effect, identity.assetId);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
                Marker markerScr = marker.GetComponent<Marker>();
                RectTransform markerRect = marker.GetComponent<RectTransform>();
                markerRect.position = transform.position;
                markerScr.SetString("-" + bulletDamage);
                markerScr.color = Color.red;
                ClientCreateHitMarker();
            }
            Destroy(effect, 0.4f);
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    protected void ClientCreateHitMarker()
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = transform.position;
        markerScr.SetString("-" + bulletDamage);
        markerScr.color = Color.red;
    }
}