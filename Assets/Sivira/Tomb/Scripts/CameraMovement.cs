using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smooth = 1f;

    void Update()
    {
        Vector3 look = new Vector3(target.transform.position.x,target.transform.position.y,-10);

        transform.position = Vector3.Lerp(transform.position, look, Time.deltaTime * smooth);
    }
}
