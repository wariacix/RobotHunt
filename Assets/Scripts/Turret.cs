using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootingComponent))]
public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turretTop;
    [SerializeField] private float shootDistance = 7f;
    private ShootingComponent shootingComponent;
    [HideInInspector] public uint ownerPlayerId;

    private void Awake()
    {
        shootingComponent = gameObject.GetComponent<ShootingComponent>();
        shootingComponent.weapons[0] = Instantiate(shootingComponent.weapons[0]);
        shootingComponent.weapons[0].firePoints[0] = shootingComponent.weapons[0].firePoints[0];
        shootingComponent.weapons[0].isSelected = true;
    }

    private void Update()
    {
        float distance = 0f;
        GameObject targetedEnemy = null;
        if (WaveManager.Instance.enemies.Count == 0) return;
        foreach (GameObject enemy in WaveManager.Instance.enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) > distance && Vector3.Distance(enemy.transform.position, transform.position) <= shootDistance)
            {
                targetedEnemy = enemy;
                distance = Vector3.Distance(enemy.transform.position, transform.position);
            }
        }

        if (targetedEnemy != null)
        {
            Vector3 direction = targetedEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
            turretTop.transform.rotation = rotation;
            RotateTurret(direction);
            if (shootingComponent.weapons[0].shootClock >= shootingComponent.weapons[0].reloadTime)
            {
                shootingComponent.weapons[0].shootClock = 0;
                shootingComponent.Shoot(shootingComponent.weapons[0].bullets, shootingComponent.weapons[0].bulletSpeed);
            }
            shootingComponent.weapons[0].shootClock += Time.deltaTime;
        }
        else shootingComponent.weapons[0].shootClock += Time.deltaTime;
    }

    private void RotateTurret(Vector3 targetedPosition)
    {
        float angle = Mathf.Atan2(targetedPosition.y, targetedPosition.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
        turretTop.transform.rotation = rotation;
    }
}
