using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    private PointCount pointCount;
    private float dist = 0f;
    private GameObject player;

    void Start()
    {
        pointCount = GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<PointCount>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dist = (player.transform.position - transform.position).magnitude;

        if (dist <= 0.5f) {
            pointCount.IncreasePoints();
            Destroy(gameObject);
        }
    }
}
