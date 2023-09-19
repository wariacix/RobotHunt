using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Camera CameraInstance;
    public Canvas CanvasInstance;
    public Image ammoImg;
    public TextMeshProUGUI ammoText, hpText, baseText;
    public TextMeshProUGUI waveTimeUIText;
    public TextMeshProUGUI goldUIText;
    public static UIManager Instance;
    [HideInInspector] public bool IsBuying = false;
    [SerializeField] private GameObject shopObject, techObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsBuying)
        {
            shopObject.SetActive(true);
        }
        else
        {
            techObject.SetActive(false);
            shopObject.SetActive(false);
        }
    }
}
