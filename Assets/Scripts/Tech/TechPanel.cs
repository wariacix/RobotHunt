using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechPanel : MonoBehaviour
{
    [SerializeField] private Image techImage;
    [SerializeField] private TextMeshProUGUI techText;
    [SerializeField] public Transform leftPoint, rightPoint;
    [SerializeField] public Tech tech;
    [Space]
    [SerializeField] public Button button;
    [SerializeField] private Slider researchProgressBar;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject selectedOverlay;
    [SerializeField] public GameObject blockedOverlay;
    [SerializeField] private TextMeshProUGUI progressStatusText;
    [Space]
    [SerializeField][AllowNull] private List<TechPanel> techPanelsConnectedTo;
    [HideInInspector][AllowNull] public List<TechPanel> techPanelsConnectedFrom = new List<TechPanel>();
    [Space]
    [HideInInspector] public Vector2 basePosition;

    private List<LineRenderer> lines = new List<LineRenderer>();

    #region Unity Methods
    private void Start()
    {
        int index = TechManager.Instance.techList.FindIndex(o => o == tech);
        tech = Instantiate(tech);
        techText.text = tech.techName;
        techImage.sprite = tech.sprite;
        TechManager.Instance.techList[index] = tech;
        basePosition = transform.position;
        researchProgressBar.maxValue = tech.maxProgress;
        for (int i = 0; i < techPanelsConnectedTo.Count; i++)
        {
            if (techPanelsConnectedTo[i] != null)
            {
                GameObject line = Instantiate(new GameObject(), rightPoint.transform);
                lines.Add(line.AddComponent<LineRenderer>());
                techPanelsConnectedTo[i].techPanelsConnectedFrom.Add(this);
            }
        }
    }

    private void OnEnable()
    {
        basePosition = transform.position;
    }

    private void Update()
    {
        if (techPanelsConnectedTo != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].startColor = Color.green;
                lines[i].endColor = Color.green;
                lines[i].positionCount = 4;
                lines[i].SetPosition(0, new Vector3(rightPoint.position.x, rightPoint.position.y, -1.5f));
                lines[i].SetPosition(1, new Vector3((rightPoint.position.x + techPanelsConnectedTo[i].leftPoint.position.x) / 2, rightPoint.position.y, -1.5f));
                lines[i].SetPosition(2, new Vector3((rightPoint.position.x + techPanelsConnectedTo[i].leftPoint.position.x) / 2, techPanelsConnectedTo[i].leftPoint.position.y, -1.5f));
                lines[i].SetPosition(3, new Vector3(techPanelsConnectedTo[i].leftPoint.position.x, techPanelsConnectedTo[i].leftPoint.position.y, -1.5f));
                lines[i].widthMultiplier = 0.125f;
            }
        }

        researchProgressBar.value = tech.progress;
        progressStatusText.text = tech.progress + "/" + tech.maxProgress;

        if (tech.isCompleted) checkMark.SetActive(true);
        else checkMark.SetActive(false);

        if (TechManager.Instance.selectedTech == tech) selectedOverlay.SetActive(true);
        else selectedOverlay.SetActive(false);


        bool isEverythingCompletedFlag = true;
        for (int i = 0; i < techPanelsConnectedFrom.Count; i++)
        {
            if (!techPanelsConnectedFrom[i].tech.isCompleted) isEverythingCompletedFlag = false;
        }
        if (!isEverythingCompletedFlag)
        {
            blockedOverlay.SetActive(true);
        }
        else blockedOverlay.SetActive(false);
    }
    #endregion

    #region Public Methods
    public void OnClickPanel()
    {
        bool arePrequisitiesMet = true;
        for (int i = 0; i < techPanelsConnectedFrom.Count; i++)
        {
            if (!techPanelsConnectedFrom[i].tech.isCompleted) arePrequisitiesMet = false;
        }
        if (arePrequisitiesMet)
        {
            TechManager.Instance.OnResearchChange(tech);
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
