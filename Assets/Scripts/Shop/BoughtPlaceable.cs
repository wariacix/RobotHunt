using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoughtPlaceable : MonoBehaviour
{
    public int price;
    private bool isPlaceable = true;

    private void Awake()
    {
        Turret turret = gameObject.GetComponent<Turret>();
        if (turret != null)
        {
            turret.enabled = false;
        }
    }

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
                GameManager.Instance.PlayerObject.GetComponent<PlayerShop>().isPlacing = false;
                //NavMeshSurface surface = GameManager.Instance.NavMeshInstance.GetComponent<NavMeshSurface>();
                //surface.BuildNavMeshAsync();
                spriteRenderer.color = Color.white;
                Turret turret = gameObject.GetComponent<Turret>();
                if (turret != null)
                {
                    turret.enabled = true;
                }
                Destroy(gameObject.GetComponent<BoughtPlaceable>());
            }
        }
        else if (isPlaceable == false)
        {
            spriteRenderer.color = Color.red;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            GameManager.Instance.PlayerObject.GetComponent<PlayerShop>().gold += price;
            GameManager.Instance.PlayerObject.GetComponent<PlayerShop>().isPlacing = false;
            Destroy(gameObject);
        }
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
