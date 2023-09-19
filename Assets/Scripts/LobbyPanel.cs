using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : NetworkBehaviour
{
    [SerializeField] public GameObject ReadyObject;
    [SerializeField] public Button ReadyButton;
    [SerializeField] public GameObject ReadyIndicator;
    [SerializeField] public Sprite indicatorReady, indicatorNotReady;
    [SerializeField] public GameObject KickObject;
    [SerializeField] public Button KickButton;
    [HideInInspector][SyncVar] public bool isReady = false;

    private void Update()
    {
        if (isReady == true)
        {
            Image renderer = ReadyIndicator.gameObject.GetComponent<Image>();
            renderer.sprite = indicatorReady;
        }
        else if (isReady == false)
        {
            Image renderer = ReadyIndicator.gameObject.GetComponent<Image>();
            renderer.sprite = indicatorNotReady;
        }
    }

    [ClientCallback]
    public void OnClick()
    {
        for (int i = 0; i < LobbyManager.Instance.LobbyPanels.Count; i++)
        {
            if (LobbyManager.Instance.LobbyPanels[i] == gameObject)
            {
                LobbyManager.Instance.LobbyPlayers[i].OnClickPlayer();
            }
        }
        Debug.Log("Click!");
    }
}
