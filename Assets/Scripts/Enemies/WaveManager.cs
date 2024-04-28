using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [HideInInspector] public static WaveManager Instance;

    [SerializeField] private List<Wave> waves;
    public List<Wave> Waves
    {
        get { return waves; }
    }

    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();

    [HideInInspector] public int waveId = 1;
    [HideInInspector] public float waveTimer = 0f;

    private int enemyId = 0;
    private float enemyTimer = 0f;

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        waveTimer = waves[0].waitTime;
        enemyTimer = waves[0].delay;
    }

    private void FixedUpdate()
    {
        if (waveTimer > 0f) waveTimer -= Time.fixedDeltaTime;
        else if (waveTimer <= 0f)
        {
            if (enemyTimer > 0f) enemyTimer -= Time.fixedDeltaTime;
            else if (enemyTimer <= 0f)
            {
                if (waveId <= waves.Count)
                {
                    if (waves[waveId].spawnables.Count > enemyId)
                    {
                        Spawn(waves[waveId].spawnables[enemyId].spawnPoint.transform.position);
                        enemyId++;
                        enemyTimer = waves[waveId].delay;
                    }
                    else if (waves[waveId].spawnables.Count <= enemyId)
                    {
                        RegenerateHealth(); //Health regenerates every wave
                        waveId++;
                        enemyId = 0;
                        if (waveId <= waves.Count) waveTimer = waves[waveId].waitTime;
                    }
                }
            }
        }
    }
    #endregion

    #region Private Methods
    private void RegenerateHealth()
    {
        PlayerShop playerShop = GameManager.Instance.PlayerObject.GetComponent<PlayerShop>();
        playerShop.gold += 10 * (waveId + 1);
        SpawnRegenMarkers(10 * (waveId + 1), GameManager.Instance.PlayerObject.transform.position);

        AddSciencePoints(TechManager.Instance.sciencePointsPerWave);

        Health hpSystem = GameManager.Instance.PlayerObject.GetComponent<Health>();
        if (hpSystem.hp < hpSystem.maxhp)
        {
            GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
            Marker markerScr = marker.GetComponent<Marker>();
            RectTransform markerRect = marker.GetComponent<RectTransform>();
            markerRect.position = GameManager.Instance.PlayerObject.transform.position;
            markerScr.SetString((Mathf.Clamp(hpSystem.hp + 25, 0, hpSystem.maxhp) - hpSystem.hp).ToString());
            markerScr.color = Color.green;
            hpSystem.hp += 25;
            if (hpSystem.hp > hpSystem.maxhp) hpSystem.hp = hpSystem.maxhp;
        }
        if (hpSystem.hp > hpSystem.maxhp) hpSystem.hp = hpSystem.maxhp;
    }

    private void Spawn(Vector3 where)
    {
        GameObject enemy = Instantiate(waves[waveId].spawnables[enemyId].enemyPrefab, where, Quaternion.identity);
        enemies.Add(enemy);
    }

    private void SpawnRegenMarkers(int reward, Vector3 position)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = position;
        markerScr.SetString("+" + (int)(reward) + "G");
        markerScr.color = Color.yellow;
    }

    private void AddSciencePoints(float amount)
    {
        if (TechManager.Instance != null) TechManager.Instance.AddSciencePoints(amount);
    }
    #endregion
}
