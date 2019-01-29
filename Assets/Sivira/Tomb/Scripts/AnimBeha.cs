using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBeha : MonoBehaviour
{
    public Sprite[] waits = new Sprite[3];

    private PlayerController playerController;
    private Animator anim;
    private SpriteRenderer render; 

    void Start()
    {
        playerController = this.gameObject.transform.parent.GetComponent<PlayerController>();
        anim = this.GetComponent<Animator>();
        render = this.GetComponent<SpriteRenderer>();
    }

    void Update()//0 frente , 1 atras, 2 lado
    {
        if (playerController.orientation == 0)
        {
            render.sprite = waits[0];
        }
        else if (playerController.orientation == 1)
        {
            render.sprite = waits[1];
        }
        else
        {
            render.sprite = waits[2];
        }


        if (PlayerController.currentState == PlayerController.PlayerStates.wait)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("Uping", false);
            anim.SetBool("Dowing", false);
        }
        else if (PlayerController.currentState == PlayerController.PlayerStates.walking)
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("Uping", false);
            anim.SetBool("Dowing", false);
        }
        else if (PlayerController.currentState == PlayerController.PlayerStates.uping)
        {
            anim.SetBool("Uping", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("Dowing", false);
        }
        else if(PlayerController.currentState == PlayerController.PlayerStates.dowing)
        {
            anim.SetBool("Dowing", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("Uping", false);
        }
    }
}
