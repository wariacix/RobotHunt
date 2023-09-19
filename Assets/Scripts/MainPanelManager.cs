using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject techTreeObject, shopObject;

    public void OnClickShopButton()
    {
        ChangeEnabledObject(shopObject, techTreeObject);
    }

    public void OnClickTechButton()
    {
        ChangeEnabledObject(techTreeObject, shopObject);
    }

    public void ChangeEnabledObject(GameObject objectToEnable, GameObject objectToDisable)
    {
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
    }
}
