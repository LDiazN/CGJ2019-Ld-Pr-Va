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
        if (!isMoving)
        {
            float valueH = Input.GetAxisRaw("Horizontal");
            float valueV = Input.GetAxisRaw("Vertical");

            if (valueH != 0 || valueV != 0)
            {
                isMoving = true;

                if (valueH != 0)
                {
                    dir = Vector2.right * valueH;
                }

                if (valueV != 0)
                {
                    dir = Vector2.up * valueV;
                }
            }
        }
        else
        {
            Vector2 origin = new Vector2(transform.position.x + (dir.x * padding), transform.position.y + (dir.y * padding));
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, float.MaxValue);
            Debug.DrawLine(origin, dir * (float.MaxValue), Color.red);

            if (hit && hit.collider.CompareTag("block"))
            {
                if (hit.collider.CompareTag("block"))
                {
                    float dist = (hit.point - origin).magnitude;

                    if (dist >= 0.01f)
                    {
                        transform.position = Vector3.MoveTowards(origin, hit.point - (dir * padding), speed);
                    }
                    else
                    {
                        isMoving = false;
                    }
                }
            }
        }

    }
}
