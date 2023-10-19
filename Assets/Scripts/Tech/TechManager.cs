using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    [HideInInspector] public static TechManager Instance;
    [HideInInspector] public Tech selectedTech;
    [HideInInspector] public float freeSciencePoints = 0;
    [HideInInspector] public float sciencePointsPerWave = 25;
    public List<Tech> techList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedTech = techList[0];
    }

    public void AddSciencePoints(float amount)
    {
        selectedTech.progress += amount;

        if (freeSciencePoints > 0)
        {
            selectedTech.progress += freeSciencePoints;
            freeSciencePoints = 0;
        }

        if (selectedTech.progress >= selectedTech.maxProgress)
        {
            freeSciencePoints += selectedTech.progress - selectedTech.maxProgress;
            selectedTech.progress = selectedTech.maxProgress;
            selectedTech.isCompleted = true;
            for (int i = 0; i < TechTreeManager.Instance.techPanels.Count; i++)
            {
                if (TechTreeManager.Instance.techPanels[i].tech == selectedTech)
                {
                    if (TechTreeManager.Instance.techPanels[i].gameObject.GetComponent<RootTechReward>() != null)
                    {
                        TechTreeManager.Instance.techPanels[i].gameObject.GetComponent<RootTechReward>().AssignTechReward();
                    }
                }
            }
        }
    }

    public void OnResearchChange(Tech tech)
    {
        selectedTech = tech;
        AddSciencePoints(0);
    }
}
