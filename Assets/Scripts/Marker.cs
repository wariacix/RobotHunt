using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI markerText;
    [SerializeField] private float speed = 0.0002f;
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] public Color color;

    private float clock = 0f;

    public void SetString(string text)
    {
        markerText.text = text;
    }

    private void Update()
    {
        gameObject.transform.Translate(new Vector2(0, speed * Time.deltaTime));
        color.a = -Mathf.Lerp(0.25f, lifeTime, clock) + 1; //transparency ma spadaæ wraz z wzrostem clocka
        markerText.color = color;
        clock += Time.deltaTime;
        if (clock > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
