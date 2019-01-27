using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerRockman : MonoBehaviour
{
    public string sceneName = "rocketman_final";
    public Text timeText;
    public Text boomText;
    public float force = 200;
    public float playTimeInSeconds = 30;
    private float elapsedTime;
    private Rigidbody2D mRigidbody;
    private bool crash = false;
    private bool changeScene = false;
    private float defaultGravity;
    

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        defaultGravity = mRigidbody.gravityScale;
        MyRestart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!crash && Input.GetButtonDown("Fire1"))
        {
            mRigidbody.AddForce(force * Vector2.up,ForceMode2D.Impulse);
        }

        if (elapsedTime <= 0 && !crash)
        {
            if(!changeScene)
            {
                changeScene = true;
                SceneManager.LoadSceneAsync(sceneName);
            }
        }

        elapsedTime -= Time.deltaTime;
    }

    void OnGUI()
    {
        if (!crash)
        {
            timeText.text = elapsedTime.ToString("F2");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(elapsedTime > 0 && !crash)
        {
            crash = true;
            StartCoroutine(GameOver());
        }
    }
    
    void MyRestart()
    {
        elapsedTime = playTimeInSeconds;
        gameObject.transform.position = Vector3.zero;
        crash = false;
        mRigidbody.gravityScale = defaultGravity;
        boomText.gameObject.SetActive(false);
    }

    IEnumerator GameOver()
    {
        boomText.gameObject.SetActive(true);
        mRigidbody.gravityScale = 0;
        Vector3 lastPosition = transform.position;
        float timer = 2.0f;
        while(timer > 0)
        {
            transform.position = lastPosition;
            timer -= Time.deltaTime;
            yield return null;
        }
        
        MyRestart();
        StopCoroutine(GameOver());
    }
}
