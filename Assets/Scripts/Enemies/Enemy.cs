using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public int damage;
    public int reward;
    public float followRange;
    [SerializeField] private GameObject hpBar;
    private Slider hpSlider;
    private Health hpComponent;
    private RectTransform hpRectTransform;
    private float hitClock;
    private Vector3 target;

    private void Update()
    {
        hitClock += Time.deltaTime;
        Quaternion.LookRotation(GameManager.Instance.PlayerInstances[0].transform.position);
        gameObject.transform.rotation = new Quaternion(0, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

        hpRectTransform.position = transform.position + new Vector3(0,0.55f,0);
        hpSlider.value = hpComponent.hp;
    }

    private void Start()
    {
        hpBar = Instantiate(hpBar, UIManager.Instance.CanvasInstance.transform);
        hpComponent = GetComponent<Health>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
        hpSlider = hpBar.GetComponent<Slider>();
        hpRectTransform = hpBar.GetComponent<RectTransform>();

        hpSlider.maxValue = hpComponent.maxhp;
    }

    private void OnDestroy()
    {
        Destroy(hpBar);
    }

    private void FixedUpdate()
    {
        CalculateTarget();
        Agent.SetDestination(target);
    }

    private void CalculateTarget()
    {
        float distance = 0f;
        Vector3 chosenPos = new Vector2(99999, 99999);

        if (Vector3.Distance(GameManager.Instance.PlayerObject.transform.position, transform.position) > distance && Vector3.Distance(GameManager.Instance.PlayerObject.transform.position, transform.position) < followRange)
        {
            distance = Vector3.Distance(GameManager.Instance.PlayerObject.transform.position, transform.position);
            chosenPos = GameManager.Instance.PlayerObject.transform.position;
        }

        if (Vector3.Distance(chosenPos, transform.position) < Vector3.Distance(transform.position, GameManager.Instance.BaseInstance.transform.position))
        {
            target = chosenPos;
        }
        else
        {
            target = GameManager.Instance.BaseInstance.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hitClock > 0.35f)
        {
            hitClock = 0;
            Health hpSystem = collision.gameObject.GetComponent<Health>();
            hpSystem.Hit(damage);
            GameObject marker = Instantiate(GameManager.Instance.MarkerPrefab, UIManager.Instance.CanvasInstance.transform);
            Marker markerScr = marker.GetComponent<Marker>();
            RectTransform markerRect = marker.GetComponent<RectTransform>();
            markerRect.position = transform.position;
            markerScr.SetString("-" + damage);
            markerScr.color = Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            Health hpSystem = collision.gameObject.GetComponent<Health>();
            hpSystem.Hit(damage * 2);
            WaveManager.Instance.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
