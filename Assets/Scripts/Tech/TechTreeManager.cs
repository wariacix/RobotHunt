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

    [HideInInspector] public GameObject DescriptionObject => descriptionObject;

    [SerializeField] private Transform mainTransform;
    [SerializeField] private GameObject descriptionObject;

    private Vector2 offset = new Vector2(0,0);
    private Vector2 lastMousePosition = new Vector2 (999999,999999);

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        offset = new Vector2(0, 0);
        lastMousePosition = new Vector2(999999, 999999);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lastMousePosition == new Vector2(999999, 999999))
            {
                lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                offset += new Vector2(Input.mousePosition.x - lastMousePosition.x, Input.mousePosition.y - lastMousePosition.y) * 0.016f;
                lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        else
        {
            lastMousePosition = new Vector2(999999, 999999);
        }

        transform.position = mainTransform.position + (Vector3)offset + new Vector3(0, 0, -1);

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        descriptionObject.SetActive(false);
        if (hit.transform != null)
        {
            if (hit.transform.CompareTag("TechPanel"))
            {
                descriptionObject.SetActive(true);
                Vector3 techPanelPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                techPanelPos.z = 0;
                descriptionObject.transform.position = techPanelPos + new Vector3(0.5f, -0.5f);
                TechPanel hitPanel = hit.transform.GetComponent<TechPanel>();
                descriptionObject.GetComponent<TechDescription>().UpdateDescriptionTexts(hitPanel.tech.techName, hitPanel.tech.techDescription, !hitPanel.blockedOverlay.activeSelf, hitPanel.tech.isCompleted);
            }
        }


        freeResearchPointsText.text = TechManager.Instance.freeSciencePoints.ToString();
        selectedTechNameText.text = TechManager.Instance.selectedTech.techName + " (" + TechManager.Instance.selectedTech.progress + "/" + TechManager.Instance.selectedTech.maxProgress + ")";
    }
}
