using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject singleplayerMenu;
    [SerializeField] private TMP_InputField connectField;

    public void OnClickSingleplayer()
    {
        if (singleplayerMenu.activeSelf == false) singleplayerMenu.SetActive(true);
        else singleplayerMenu.SetActive(false);
    }

    public void OnClickSingleplayerStartGame()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }

    public void OnClickCancel()
    {
        singleplayerMenu.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
