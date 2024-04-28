using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    private float timeToDestroy = 0;

    private void Update()
    {
        if (timeToDestroy < 2.5f)
        {
            timeToDestroy += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}