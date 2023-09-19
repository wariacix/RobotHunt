using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> LobbyPanels;
    public List<LobbyPlayer> LobbyPlayers = new List<LobbyPlayer>();
    public static LobbyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < LobbyPanels.Count; i++)
        {
            LobbyPanels[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < LobbyPlayers.Count; i++)
        {
            LobbyPanels[i].SetActive(true);
            LobbyPlayers[i].OwnedPanel = LobbyPanels[i];
        }
    }
}