using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] public List<Weapon> weapons;
    [SerializeField] public List<Transform> firePoints;

    private void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i] = Instantiate(weapons[i]);
            weapons[i].isSelected = false;
        }
        foreach (var weapon in weapons)
        {
            for (int i = 0; i < weapon.firePoints.Count; i++)
            {
                weapon.firePoints[i] = firePoints[i];
            }
        }
        weapons[0].isSelected = true;
    }

    private void Update()
    {
        if (UIManager.Instance.IsBuying == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (Weapon weapon in weapons)
                {
                    weapon.isSelected = false;
                }
                weapons[0].isSelected = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (Weapon weapon in weapons)
                {
                    weapon.isSelected = false;
                }
                weapons[1].isSelected = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (Weapon weapon in weapons)
                {
                    weapon.isSelected = false;
                }
                weapons[2].isSelected = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (Weapon weapon in weapons)
                {
                    weapon.isSelected = false;
                }
                weapons[3].isSelected = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (Weapon weapon in weapons)
                {
                    weapon.isSelected = false;
                }
                weapons[4].isSelected = true;
            }
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].isSelected)
                {
                    UIManager.Instance.ammoImg.sprite = weapons[i].ammoSprite;
                    UIManager.Instance.ammoText.color = weapons[i].textColor;
                    UIManager.Instance.ammoText.text = weapons[i].ammo.ToString();

                    if (weapons[i].isSelected && weapons[i].isAutomatic && Input.GetButton("Fire1"))
                    {
                        if (weapons[i].shootClock >= weapons[i].reloadTime && weapons[i].ammo > 0)
                        {
                            weapons[i].ammo--;
                            weapons[i].shootClock = 0;
                            Shoot(weapons[i].bullets, weapons[i].bulletSpeed);
                        }
                    }
                    else if (weapons[i].isSelected && Input.GetButtonDown("Fire1"))
                    {
                        if (weapons[i].ammo > 0)
                        {
                            weapons[i].ammo--;
                            Shoot(weapons[i].bullets, weapons[i].bulletSpeed);
                        }
                    }
                    weapons[i].shootClock += Time.deltaTime;
                }
            }
        }
    }

    public void Shoot(List<GameObject> bulletPrefab, float bulletSpeed)
    {
        for (int i = 0; i < bulletPrefab.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab[i], firePoints[i].position, firePoints[i].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[i].up * bulletSpeed, ForceMode2D.Impulse);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            GameObject audioObjectInstantiated = Instantiate(bulletScript.audioObject, firePoints[i].position, firePoints[i].rotation);
        }
    }
}
