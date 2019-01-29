using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject[] items = new GameObject[3];
    public int orientation;

    //The keys that the player has.
    public static Dictionary<int, bool> doorKeys;
    public keyAdventure[] keys;

    //The player walking speed;
    [Range(1,20)]
    public float walkSpeed;

    //The player states:
    public enum PlayerStates {walking,wait, end, uping, dowing};

    //The current player state:
    public static PlayerStates currentState;

    //The player rigidbody:
    Rigidbody2D rb2d;

    //El gameObject del objetivo:
    public static PlayerController playerObject;

    //Input:
    Vector2 movement;

    //Items variables
    public int itemCount = 0;
    public int requiredItemCount = 4;
    public Text itemText;

    // Start is called before the first frame update
    void Start()
    {
        itemText.text = "Items: 0";
        //doorKeys = new Dictionary<int, bool>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerObject = this;
        movement = Vector2.zero;
        currentState = PlayerStates.wait;
        orientation = 1;

        //initialize keys:
        /*for(int i = 0; i<keys.Length; i++)
        {
            if (doorKeys.ContainsKey(keys[i].id))
            {
                doorKeys[keys[i].id] = false;
            }
            else
            {
                doorKeys.Add(keys[i].id, false);
            }
        } */
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb2d.velocity = walkSpeed * movement;

        if (currentState != PlayerStates.end)
        {
            if (movement.x == 0 && movement.y == 0)
            {
                currentState = PlayerStates.wait;
            }
            else if (movement.x == 0 && movement.y != 0)
            {
                if (movement.y == 1)
                {
                    currentState = PlayerStates.uping;
                    orientation = 0;

                }
                else
                {
                    currentState = PlayerStates.dowing;
                    orientation = 1;
                }

            }
            else if (movement.y == 0 && movement.x != 0)
            {
                currentState = PlayerStates.walking;
                orientation = 2;
                transform.localScale = new Vector3((-1) * movement.x, transform.localScale.y, transform.localScale.z);
            }
        }


        Debug.Log(movement.x == 0 && movement.y == 0);
        /*if (currentState == PlayerStates.walking) {
             movement.x = Input.GetAxisRaw("Horizontal");
             movement.y = Input.GetAxisRaw("Vertical");
             transform.localScale = new Vector3(transform.localScale.x * movement.x, transform.localScale.y, transform.localScale.z);
             rb2d.velocity = walkSpeed * movement;




         }*/
    }

     Vector2 Movement()
    {
        Vector2 direction;
        float x;
        float y;
        RaycastHit2D hit;

        direction = Vector2.zero;
        currentState = PlayerStates.wait;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0)
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
            currentState = PlayerStates.walking;

        {
            hit = Physics2D.Raycast(transform.position, x * Vector2.right,1f);
            if (hit == false)
            {
                direction.x = x;
            }
            else
            {
                direction.x = 0;
            }

            hit = Physics2D.Raycast(transform.position, y * Vector2.up, 1f);
            if (hit == false)
            {
                direction.y = y;
            }
            else
            {
                direction.y = 0;
            }


        }

        return (direction);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "room")
        {
            CameraControllerAdventure.gameCam.transform.Translate(-CameraControllerAdventure.gameCam.transform.position + collision.gameObject.transform.position);
            transform.Translate(movement);
            CameraControllerAdventure.gameCam.transform.Translate(10 * Vector3.back);

        }
        else if (collision.gameObject.tag == "Exit_Adv" && itemCount >= requiredItemCount)
        {
            EndGame();
        }
        else if (collision.gameObject.tag == "Item_Adv")
        {
            TakeItem();
            Destroy(collision.gameObject,0.2f);
        }

    }

    public void TakeItem()
    {
        FindObjectOfType<PlayerAudioManager>().Play("bip");
        itemCount += 1;
        if (itemCount < requiredItemCount)
        {
            itemText.text = "Items: " + itemCount;
        }
        else
        {
            itemText.text = "Items: Ready";
        }
    }

    public void EndGame()
    {
        currentState = PlayerStates.end;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        //GetComponent<Animator>().Play("Exit");

        foreach (GameObject item in items)
        {
            item.GetComponent<Animator>().Play("Win");    
        }

        FindObjectOfType<PlayerAudioManager>().Play("exit");

        Invoke("NextScene",2f);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
