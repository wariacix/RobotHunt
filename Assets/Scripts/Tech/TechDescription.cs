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
            isAvailableText.text = "Locked";
        }
        else if (isAvailable == true && isResearched == false)
        {
            isAvailableText.text = "Available";
        }
        else if (isAvailable == true && isResearched == true)
        {
            isAvailableText.text = "Researched";
        }
    }
}
