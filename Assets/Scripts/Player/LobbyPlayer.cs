using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : NetworkBehaviour
{
    public GameObject OwnedPanel;

    private void Start()
    {
        LobbyManager.Instance.LobbyPlayers.Add(this);
    }

    private void OnDestroy()
    {
        LobbyManager.Instance.LobbyPlayers.Remove(this);
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            int index = LobbyManager.Instance.LobbyPlayers.FindIndex(o => o == this);
            for (int i = 0; i < LobbyManager.Instance.LobbyPanels.Count; i++)
            {
                if (index != i)
                {
                    LobbyManager.Instance.LobbyPanels[i].GetComponent<LobbyPanel>().ReadyObject.SetActive(false);
                }
                else
                {
                    LobbyManager.Instance.LobbyPanels[i].GetComponent<LobbyPanel>().ReadyObject.SetActive(true);
                }
            }

            if (NetworkClient.activeHost == false)
            {
                for (int i = 0; i < LobbyManager.Instance.LobbyPanels.Count; i++)
                {
                    LobbyManager.Instance.LobbyPanels[i].GetComponent<LobbyPanel>().KickObject.SetActive(false);
                }
            }
        }
    }

    public void OnClickPlayer()
    {
        SetReady();
    }

    [Command]
    private void SetReady()
    {
        LobbyPanel panel = OwnedPanel.GetComponent<LobbyPanel>();
        if (panel.isReady) panel.isReady = false;
        else panel.isReady = true;
    }
}