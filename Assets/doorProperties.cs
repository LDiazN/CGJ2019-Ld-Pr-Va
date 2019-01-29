using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorProperties : MonoBehaviour
{
    //This scripst assigns a id to the door.
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag == "Player");
        if (collision.gameObject.tag == "Player" && PlayerController.doorKeys[id])
        {
            FindObjectOfType<keyManager>().RemoveKey();
            Destroy(this.gameObject);
        }
    }
}
