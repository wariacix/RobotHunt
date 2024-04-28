using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float damageTimeOffset;
    [HideInInspector] public int damage;
    private List<GameObject> touchedObjects = new List<GameObject>();
    private float clock = 0f;
    [HideInInspector] public uint playerNetId;

    private void Update()
    {
        clock += Time.deltaTime;

        transform.position += (Vector3) new Vector2(0, 0.35f * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !touchedObjects.Contains(other.gameObject) && clock >= damageTimeOffset)
        {
            touchedObjects.Add(other.gameObject);
            Health hpComponent = other.gameObject.GetComponent<Health>();
            hpComponent.Hit(damage);
            GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
            Marker markerScr = marker.GetComponent<Marker>();
            RectTransform markerRect = marker.GetComponent<RectTransform>();
            markerRect.position = other.transform.position;
            markerScr.SetString("-" + damage);
            markerScr.color = Color.red;
            CreateHitMarker(other.transform.position);
        }
    }

    protected void CreateHitMarker(Vector3 position)
    {
        GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
        Marker markerScr = marker.GetComponent<Marker>();
        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.position = position;
        markerScr.SetString("-" + damage);
        markerScr.color = Color.red;
    }
}
