using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyManager : MonoBehaviour
{

    public RawImage[] keys;
    int howManyKeys;
    // Start is called before the first frame update
    void Start()
    {
        howManyKeys = 0;
        for(int i = 0; i<keys.Length; i++)
        {
            keys[i].gameObject.SetActive(false);
        }
    }

    public  void Addkey()
    {
        keys[howManyKeys].gameObject.SetActive(true);
        howManyKeys += 1;
    }

    public void RemoveKey()
    {
        keys[howManyKeys - 1].gameObject.SetActive(false);
        howManyKeys -= 1;
    }


}
