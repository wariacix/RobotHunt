using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TechDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI techNameText;
    [SerializeField] private TextMeshProUGUI isAvailableText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void UpdateTexts(string techName, string isAvailable, string description)
    {
        techNameText.text = techName;
        isAvailableText.text = isAvailable;
        descriptionText.text = description;
    }
}
