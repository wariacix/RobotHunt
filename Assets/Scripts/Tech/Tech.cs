using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Tech", menuName = "Game/New Tech")]
public class Tech : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string techName;
    [SerializeField] public string techDescription;
    [SerializeField] public float progress = 0;
    [SerializeField] public float maxProgress = 100;
    [SerializeField] public bool isCompleted = false;
}
