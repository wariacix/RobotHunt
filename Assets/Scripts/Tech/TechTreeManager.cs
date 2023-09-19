using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeManager : MonoBehaviour
{
    public static TechTreeManager Instance;
    [SerializeField] private TextMeshProUGUI freeResearchPointsText;
    [SerializeField] private TextMeshProUGUI selectedTechNameText;
    [SerializeField] public List<TechPanel> techPanels;
    [SerializeField] private GameObject descriptionObject;

    private Vector2 offset = new Vector2(0,0);
    private Vector2 lastPos = new Vector2 (999999,999999);

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        offset = new Vector2(0, 0);
        lastPos = new Vector2(999999, 999999);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lastPos == new Vector2(999999, 999999))
            {
                lastPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                offset += new Vector2(Input.mousePosition.x - lastPos.x, Input.mousePosition.y - lastPos.y) * 0.016f;
                lastPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        else
        {
            lastPos = new Vector2(999999, 999999);
        }

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        bool panelFlag = false;
        for (int i = 0; i < techPanels.Count; i++)
        {
            if (hit.transform == techPanels[i].transform)
            {
                panelFlag = true;
            }
        }
        if (panelFlag == true) descriptionObject.SetActive(true);
        else descriptionObject.SetActive(false);

        foreach (TechPanel panel in techPanels)
        {
            panel.transform.position = (Vector3)panel.basePosition + (Vector3)offset + new Vector3(0,0,-1);
        }

        freeResearchPointsText.text = TechManager.Instance.freeSciencePoints.ToString();
        selectedTechNameText.text = TechManager.Instance.selectedTech.techName + " (" + TechManager.Instance.selectedTech.progress + "/" + TechManager.Instance.selectedTech.maxProgress + ")";
    }
}
