using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/New Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] public int ammo = 60;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float reloadTime;
    [SerializeField] public List<GameObject> bullets;
    [SerializeField] public List<Transform> firePoints;
    [SerializeField] public Sprite ammoSprite;
    [SerializeField] public bool isAutomatic;
    [SerializeField] public Color textColor;

    [HideInInspector] public float shootClock = 1000;

    [HideInInspector] public bool isSelected = false;

    public Weapon(Weapon weaponToCopy)
    {
        ammo = weaponToCopy.ammo;
        bulletSpeed = weaponToCopy.bulletSpeed;
        reloadTime = weaponToCopy.reloadTime;
        bullets = weaponToCopy.bullets;
        firePoints = weaponToCopy.firePoints;
        ammoSprite = weaponToCopy.ammoSprite;
        isAutomatic = weaponToCopy.isAutomatic;
        isSelected = weaponToCopy.isSelected;
        textColor = weaponToCopy.textColor;
    }
}
