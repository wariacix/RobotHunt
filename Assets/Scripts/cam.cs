using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.AdditiveLevels;

public class cam : NetworkBehaviour
{
    public static cam Instance;
    public Transform pos;

    private void Awake()
    {
        Instance = this;
    }

    public override void OnStartLocalPlayer()
    {
        Camera mainCam = gameObject.GetComponent<Camera>();
        if (mainCam != null)
        {
            Camera.SetupCurrent(mainCam);
        }
        else
            Debug.LogWarning("PlayerCamera: Could not find a camera in scene with 'MainCamera' tag.");
    }

    void Update()
    {
        transform.position = pos.position + Vector3.back * 10f;
    }
}
