using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    
    public PlayableDirector firstScene;
    public GameObject dialogManager;

    private bool loadingScene = false;
    double firstSceneTime;
    float timer = 0f;

    private void Awake()
    {
        firstSceneTime = firstScene.duration;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= firstSceneTime && !loadingScene)
        {
            loadingScene = true;
            
            dialogManager.SetActive(true);
            dialogManager.GetComponent<Dialogs>().StartDiag();
            
        }
    }
}
