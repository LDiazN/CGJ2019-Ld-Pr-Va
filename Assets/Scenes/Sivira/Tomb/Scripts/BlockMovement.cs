using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float speed = 0.5f;

    private Transform aPoint;
    private Transform bPoint;
    private Transform currentPoint;
    private float dist = 0f;

    void Start()
    {
        aPoint = gameObject.transform.GetChild(0);
        bPoint = gameObject.transform.GetChild(1);
        aPoint.parent = null;
        bPoint.parent = null;

        currentPoint = aPoint;
        transform.position = currentPoint.position;
    }

    void Update()
    {
        dist = (currentPoint.position - transform.position).magnitude;

        transform.position = Vector3.MoveTowards(transform.position,currentPoint.position,speed * Time.deltaTime);

        if (dist <= 0.1f)
        {
            if (currentPoint == aPoint)
            {
                currentPoint = bPoint;
            }
            else
            {
                currentPoint = aPoint;
            }
        }
    }
}
