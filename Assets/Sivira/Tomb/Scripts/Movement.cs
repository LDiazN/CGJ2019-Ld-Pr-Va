using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 3f;
    public float padding = 0.25f;

    private Vector2 dir = Vector2.zero;
    private bool isMoving = false;

    void Update()
    {
        Vector2 origin = new Vector2(transform.position.x + (dir.x * padding), transform.position.y + (dir.y * padding));
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, float.MaxValue);
        Debug.DrawLine(origin + (dir * padding), dir * (2f), Color.red);
        Debug.DrawLine(origin - (dir * padding), dir * (2f), Color.red);

        if (!isMoving)
        {
            float valueH = Input.GetAxisRaw("Horizontal");
            float valueV = Input.GetAxisRaw("Vertical");

            if (valueH != 0 || valueV != 0)
            {
                transform.parent = null;
                if (valueH != 0)
                {
                    dir = Vector2.right * valueH;
                    isMoving = true;
                }

                if (valueV != 0)
                {
                    dir = Vector2.up * valueV;
                    isMoving = true;
                }
            }

        }
        else
        {
            if (hit)
            {
                if (hit.collider.CompareTag("block") || hit.collider.CompareTag("MovibleBlock"))
                {
                    float dist = (hit.point - origin).magnitude;

                    if (dist >= 0.01f)
                    {
                        transform.position = Vector3.MoveTowards(origin, hit.point - (dir * padding), speed);
                    }
                    else
                    {
                        isMoving = false;
                        transform.parent = hit.collider.gameObject.transform;
                    }
                }
            }
        }

    }
}
