using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosaderoController : MonoBehaviour
{

    public enum PlayerStates {Walking, Dialog, Interacting}
    public PlayerStates playerState = PlayerStates.Walking;
    public float playerSpeed = 3f;

    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Animator>().Play("posadero_escaleras_up");   
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.Equals(PlayerStates.Walking))
        {
            float velX = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(velX * playerSpeed, rb2d.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Stairs Trigger") & Input.GetKeyDown(KeyCode.E))
        {
            transform.Find("Posadero").GetComponent<Animator>().Play("posadero_escaleras_up");
            playerState = PlayerStates.Interacting;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Stairs Trigger"))
        {

        }
    }

    public void RecoverWalk()
    {
        playerState = PlayerStates.Walking;
    }
}
