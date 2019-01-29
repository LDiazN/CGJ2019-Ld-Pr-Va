using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyAdventure : MonoBehaviour
{ 

    //This script asigns a id to the key
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerAudioManager>().Play("bip");
            FindObjectOfType<keyManager>().Addkey();
            PlayerController.doorKeys[id] = true;
        }



        Destroy(this.gameObject);
    }
}
