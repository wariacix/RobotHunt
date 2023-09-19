using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Wave", menuName = "Game/New Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] public float waitTime;
    [SerializeField] public float delay = 1.2f;
    [SerializeField] public List<Spawnable> spawnables;
}

[Serializable]
public class Spawnable
{
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] public GameObject enemyPrefab;
}