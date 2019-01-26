using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    private PointCount pointCount;

    void Start()
    {
        pointCount = GetComponent<PointCount>();
    }



    public void RestartGame()
    {
        if (pointCount.points != pointCount.totalPoints)
        {
            Invoke("SceneLoad",1f);
        }
    }

    void SceneLoad()
    {
        SceneManager.LoadScene("TombOfTheMaskTest");
    }
}
