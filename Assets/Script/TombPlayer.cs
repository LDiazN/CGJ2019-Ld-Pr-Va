using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombPlayer : MonoBehaviour
{
    public float speed = 4f;
    public Vector2 dir = Vector2.zero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector2.right;
        }
        transform.Translate(Collisions(dir) * Time.deltaTime);
    }

    Vector2 Collisions(Vector2 dire)
    {
        Vector2 final;
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        if (dir == Vector2.right)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(bound.max.x, bound.center.y), dire, speed);
            if (hit && hit.collider.CompareTag("Wall"))
            {
                final = dir * hit.distance;
                dir = Vector2.zero;
                return final;
            }
            return dir * speed;
        }
        return dir * speed;
    }
}
