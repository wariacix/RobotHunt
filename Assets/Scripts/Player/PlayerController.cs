using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShootingComponent))]
public class PlayerController : MonoBehaviour
{
    [Range(0.1f, 12.0f)]
    public float movingSpeed = 5f;

    public Rigidbody2D playerRb;

    Vector2 movement;
    Vector2 mousePos;

    private void Awake()
    {
        CameraScript.Instance.pos = transform;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        ShootingComponent shooting = gameObject.GetComponent<ShootingComponent>();


        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float rotAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRb.rotation = rotAngle;

        mousePos = UIManager.Instance.CameraInstance.ScreenToWorldPoint(Input.mousePosition);

        Health health = gameObject.GetComponent<Health>();
        UIManager.Instance.hpText.text = health.hp + "/" + health.maxhp;
        Health baseHealth = GameManager.Instance.BaseInstance.GetComponent<Health>();
        UIManager.Instance.baseText.text = baseHealth.hp + "/" + baseHealth.maxhp;

        if (UIManager.Instance.IsBuying == true && (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            UIManager.Instance.IsBuying = false;
        }
        else if ((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.E)))
        {
            UIManager.Instance.IsBuying = true;
        }
    }

    void FixedUpdate()
    {
        if (movement.x != 0 | movement.y != 0)
        {
            playerRb.MovePosition(playerRb.position + movement * movingSpeed * Time.fixedDeltaTime);
        }
        else
        {
            playerRb.velocity = new Vector2(0f, 0f);
            playerRb.angularVelocity = 0f;
        }
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    playerRb.velocity = new Vector2(0f, 0f);
    //}
}
