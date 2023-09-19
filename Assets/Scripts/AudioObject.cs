using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : NetworkBehaviour
{
    private float timeToDestroy = 0;

    [ServerCallback]
    private void Update()
    {
        if (timeToDestroy < 2.5f)
        {
            timeToDestroy += Time.deltaTime;
        }
        else
        {
            RpcDestroy();
        }
    }

    [ClientRpc]
    private void RpcDestroy()
    {
        Destroy(gameObject);
    }
}