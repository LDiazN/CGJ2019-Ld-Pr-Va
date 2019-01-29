using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysScript : MonoBehaviour
{
    public Image[] keys;
    [HideInInspector]
    public int keysQuantity = 0;

    private void Awake()
    {
        foreach (Image key in keys)
        {
            key.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        keysQuantity = 0;
        AddKey();
        AddKey();
        AddKey();
        RemoveKey();
    }

    public void AddKey()
    {
        keys[keysQuantity].gameObject.SetActive(true);
        keysQuantity += 1;
    }

    public void RemoveKey()
    {
        keys[keysQuantity-1].gameObject.SetActive(false);
        keysQuantity -= 1;
    }
}
