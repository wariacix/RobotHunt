using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : NetworkBehaviour
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

    [ClientCallback]
    private void Update()
    {
        if (isLocalPlayer == true && UIManager.Instance.IsBuying == false)
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

                    int bulletPrefabIndex = NetworkManager.singleton.spawnPrefabs.FindIndex(o => o == weapons[i].bullets[0]);

                    if (weapons[i].isSelected && weapons[i].isAutomatic && Input.GetButton("Fire1"))
                    {
                        if (weapons[i].shootClock >= weapons[i].reloadTime && weapons[i].ammo > 0)
                        {
                            DecrementAmmo(i);
                            weapons[i].shootClock = 0;
                            Shoot(bulletPrefabIndex, weapons[i].bullets.Count, weapons[i].bulletSpeed);
                        }
                    }
                    else if (weapons[i].isSelected && Input.GetButtonDown("Fire1"))
                    {
                        if (weapons[i].ammo > 0)
                        {
                            DecrementAmmo(i);
                            Shoot(bulletPrefabIndex, weapons[i].bullets.Count, weapons[i].bulletSpeed);
                        }
                    }
                    weapons[i].shootClock += Time.deltaTime;
                }
            }
        }
    }

    [Command]
    private void DecrementAmmo(int index)
    {
        weapons[index].ammo--;
    }

    [ClientCallback]
    public void Shoot(int bulletPrefabIndex, int numberOfBullets, float bulletSpeed)
    {
        uint playerNetId = NetworkClient.localPlayer.netId;
        CommandShoot(bulletPrefabIndex, numberOfBullets, bulletSpeed, playerNetId);
    }

    [Command]
    public void CommandShoot(int bulletPrefabIndex, int numberOfBullets, float bulletSpeed, uint playerNetId)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bulletPrefab = NetworkManager.singleton.spawnPrefabs[bulletPrefabIndex].gameObject;
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[i].up * bulletSpeed, ForceMode2D.Impulse);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.playerNetId = playerNetId;
            GameObject audioObjectInstantiated = Instantiate(bulletScript.audioObject, firePoints[i].position, firePoints[i].rotation);
            NetworkServer.Spawn(audioObjectInstantiated);
            NetworkServer.Spawn(bullet);
        }
    }

    public void ServerShoot(int bulletPrefabIndex, int numberOfBullets, float bulletSpeed, uint playerNetId)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bulletPrefab = NetworkManager.singleton.spawnPrefabs[bulletPrefabIndex].gameObject;
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[i].up * bulletSpeed, ForceMode2D.Impulse);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.playerNetId = playerNetId;
            GameObject audioObjectInstantiated = Instantiate(bulletScript.audioObject, firePoints[i].position, firePoints[i].rotation);
            NetworkServer.Spawn(audioObjectInstantiated);
            NetworkServer.Spawn(bullet);
        }
    }
}
