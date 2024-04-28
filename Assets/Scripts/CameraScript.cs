using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;
    public Transform pos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Camera mainCam = gameObject.GetComponent<Camera>();
        if (mainCam != null)
        {
            Camera.SetupCurrent(mainCam);
        }
    }

    void Update()
    {
        transform.position = pos.position + Vector3.back * 10f;
    }
}
