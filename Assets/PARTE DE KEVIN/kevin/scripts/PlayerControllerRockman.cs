﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerRockman : MonoBehaviour
{
    public AudioClip helice;
    public AudioClip crashAudio;
    public string sceneName = "rocketman_final";
    public Text timeText;
    public Text boomText;
    public float force = 200;
    public float playTimeInSeconds = 30;
    private float elapsedTime;
    private Rigidbody2D mRigidbody;
    private bool crash = false;
    private bool changeScene = false;
    private bool initGame=false;
    private float defaultGravity;
    private Animator animator;
    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        defaultGravity = mRigidbody.gravityScale;
        mRigidbody.gravityScale=0;
        boomText.text = "clip to start";
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!initGame )
        {
            if(Input.GetButtonUp("Fire1"))
            {
                initGame = true;
                boomText.text = "Boom!";
                MyRestart();
            }
            else
            {
                return;
            }
        }

        if (!crash && Input.GetButtonDown("Fire1"))
        {
            mRigidbody.AddForce(force * Vector2.up,ForceMode2D.Impulse);
        }

        if (elapsedTime <= 0 && !crash)
        {
            if(!changeScene)
            {
                changeScene = true;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
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
        audioSource.Pause();
        audioSource.clip = helice;
        audioSource.loop = true;
        audioSource.Play();
        animator.SetBool("crash", false);
        elapsedTime = playTimeInSeconds;
        gameObject.transform.position = Vector3.zero;
        crash = false;
        mRigidbody.gravityScale = defaultGravity;
        boomText.gameObject.SetActive(false);
    }



    IEnumerator GameOver()
    {
        audioSource.Pause();
        audioSource.clip = crashAudio;
        audioSource.loop = false;
        audioSource.Play();
        animator.SetBool("crash", true);
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