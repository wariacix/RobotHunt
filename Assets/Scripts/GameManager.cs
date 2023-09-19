using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [HideInInspector] public List<GameObject> PlayerInstances;
    public List<Button> buttons;
    public GameObject BaseInstance;
    public GameObject NavMeshInstance;
    public static GameManager Instance;
    public GameObject MarkerPrefab;
    [HideInInspector] public GameObject LocalPlayerObject;
    [SerializeField] private float waveTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(19, 20);
        Physics2D.IgnoreLayerCollision(20, 19);
        Physics2D.IgnoreLayerCollision(19, 0);
    }

    public static void Exit()
    {
        NetworkClient.Disconnect();
        Application.Quit();
    }


    private void Update()
    {
        UIManager.Instance.goldUIText.text = NetworkClient.localPlayer.gameObject.GetComponent<PlayerShop>().gold.ToString();
        if (WaveManager.Instance.waveTimer <= 0)
        {
            UIManager.Instance.waveTimeUIText.text = "Wave " + (WaveManager.Instance.waveId + 1) + ": Spawning enemies...";
        }
        else
        {
            UIManager.Instance.waveTimeUIText.text = "Wave " + (WaveManager.Instance.waveId + 1) + " in:" + (Mathf.Round(WaveManager.Instance.waveTimer * 10f) / 10f) + "s";
        }

        if (Input.mouseScrollDelta.y > 0 && UIManager.Instance.CameraInstance.orthographicSize > 5)
        {
            UIManager.Instance.CameraInstance.orthographicSize -= 1;
        }
        else if (Input.mouseScrollDelta.y < 0 && UIManager.Instance.CameraInstance.orthographicSize < 11)
        {
            UIManager.Instance.CameraInstance.orthographicSize += 1;
        }
    }
}
