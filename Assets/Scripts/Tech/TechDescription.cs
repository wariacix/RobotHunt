using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TechDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI techNameText;
    [SerializeField] private TextMeshProUGUI isAvailableText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void UpdateDescriptionTexts(string techName, string description, bool isAvailable, bool isResearched)
    {
        techNameText.text = techName;
        descriptionText.text = description;
        if (isAvailable == false && isResearched == false)
        {
            isAvailableText.color = new Color(0.75f, 0.75f, 0.75f);
            isAvailableText.faceColor = new Color(0.75f, 0.75f, 0.75f);
            isAvailableText.text = "Locked";
        }
        else if (isAvailable == true && isResearched == false)
        {
            isAvailableText.color = new Color(1, 1, 1);
            isAvailableText.faceColor = new Color(1, 1, 1);
            isAvailableText.text = "Available";
        }
        else if (isAvailable == true && isResearched == true)
        {
            isAvailableText.color = new Color(0.1f, 0.9f, 0.1f);
            isAvailableText.faceColor = new Color(0.1f, 0.9f, 0.1f);
            isAvailableText.text = "Researched";
        }
    }
}
