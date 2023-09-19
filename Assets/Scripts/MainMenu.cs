using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject singleplayerMenu, multiplayerMenu, connectMenu;
    [SerializeField] private TMP_InputField connectField;

    public void OnClickSingleplayer()
    {
        if (singleplayerMenu.activeSelf == false) singleplayerMenu.SetActive(true);
        else singleplayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
    }

    public void OnClickSingleplayerStartGame()
    {
        NetworkManager.singleton.maxConnections = 1;
        NetworkManager.singleton.StartHost();
    }

    public void OnClickMultiplayer()
    {
        singleplayerMenu.SetActive(false);
        if (multiplayerMenu.activeSelf == false) multiplayerMenu.SetActive(true);
        else multiplayerMenu.SetActive(false);
    }

    public void OnClickMultiplayerStartGame()
    {
        NetworkManager.singleton.maxConnections = 16;
        NetworkManager.singleton.StartHost();
    }
    public void OnClickMultiplayerConnect()
    {
        connectMenu.SetActive(true);
    }

    public void OnClickEnterIp()
    {
        NetworkManager.singleton.networkAddress = connectField.text;
        if (connectField.text == null || !connectField.text.Contains("."))
        {
            Debug.Log("Invalid IP");
            return;
        }
        NetworkManager.singleton.StartClient();
    }

    public void OnClickCancel()
    {
        singleplayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
    }
    public void OnClickCancelConnect()
    {
        connectMenu.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
