using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    private void Update()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(rb.velocity * 130f * Time.deltaTime);   
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            Physics2D.IgnoreLayerCollision(collision.gameObject.layer, gameObject.layer);
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            SpriteRenderer sprEff = effect.GetComponent<SpriteRenderer>();
            sprEff.color = effectColor;
            Explosion effectExplosion = effect.GetComponent<Explosion>();
            effectExplosion.damage = bulletDamage;
            effectExplosion.playerNetId = playerNetId;
            Destroy(effect, 1.75f);
            Destroy(gameObject);
        }
    }
}
