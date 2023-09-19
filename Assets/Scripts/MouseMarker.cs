using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMarker : MonoBehaviour
{
    void Update()
    {
        transform.position = UIManager.Instance.CameraInstance.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 50f);
    }
}
