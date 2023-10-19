using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : NetworkBehaviour
{
    [HideInInspector] public static WaveManager Instance;

    [SerializeField] private List<Wave> waves;
    public List<Wave> Waves
    {
        get { return waves; }
    }

    [HideInInspector] public SyncList<GameObject> enemies = new SyncList<GameObject>();

    [HideInInspector][SyncVar] public int waveId = 1;
    [HideInInspector][SyncVar] public float waveTimer = 0f;

    [SyncVar] private int enemyId = 0;
    [SyncVar] private float enemyTimer = 0f;

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
    [ServerCallback]
    private void RegenerateHealth()
    {
        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (NetworkServer.connections[i].identity.gameObject != null)
            {
                PlayerShop playerShop = NetworkServer.connections[i].identity.gameObject.GetComponent<PlayerShop>();
                playerShop.gold += 10 * (waveId + 1);
                ServerSpawnMarkers(10 * (waveId + 1), NetworkServer.connections[i].identity.transform.position);
                ClientSpawnMarkers(10 * (waveId + 1), NetworkServer.connections[i].identity.transform.position);

                ClientAddSciencePoints(TechManager.Instance.sciencePointsPerWave);

                Health hpSystem = NetworkServer.connections[i].identity.gameObject.GetComponent<Health>();
                if (hpSystem.hp < hpSystem.maxhp)
                {
                    GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
                    Marker markerScr = marker.GetComponent<Marker>();
                    RectTransform markerRect = marker.GetComponent<RectTransform>();
                    markerRect.position = NetworkServer.connections[i].identity.gameObject.transform.position;
                    markerScr.SetString((Mathf.Clamp(hpSystem.hp + 25, 0, hpSystem.maxhp) - hpSystem.hp).ToString());
                    markerScr.color = Color.green;
                    hpSystem.hp += 25;
                    if (hpSystem.hp > hpSystem.maxhp) hpSystem.hp = hpSystem.maxhp;
                }
                if (hpSystem.hp > hpSystem.maxhp) hpSystem.hp = hpSystem.maxhp;
            }
        }
    }

    [ServerCallback]
    private void Spawn(Vector3 where)
    {
        GameObject enemy = Instantiate(waves[waveId].spawnables[enemyId].enemyPrefab, where, Quaternion.identity);
        NetworkServer.Spawn(enemy);
        enemies.Add(enemy);
    }

    [ClientRpc]
    private void ClientSpawnMarkers(int reward, Vector3 position)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = position;
        markerScr.SetString("+" + (int)(reward) + "G");
        markerScr.color = Color.yellow;
    }

    [ClientRpc]
    private void ClientAddSciencePoints(float amount)
    {
        if (TechManager.Instance != null) TechManager.Instance.AddSciencePoints(amount);
    }

    [ServerCallback]
    private void ServerSpawnMarkers(int reward, Vector3 position)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = position;
        markerScr.SetString("+" + (int)(reward) + "G");
        markerScr.color = Color.yellow;
    }
    #endregion
}
