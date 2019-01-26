using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public int maxTime = 10;
    public int actualTime = 0;
    public Text timeCount;

    private GameRestart gameRestart;

    void Start()
    {
        gameRestart = GetComponent<GameRestart>();

        actualTime = maxTime;
        timeCount.text = actualTime.ToString();
        InvokeRepeating("DecreaseTime",1f,1f);
    }

    void DecreaseTime()
    {
        if (actualTime > 0 && actualTime <= maxTime)
        {
            actualTime--;
            timeCount.text = actualTime.ToString();
        }
        else
        {
            CancelInvoke();
            gameRestart.RestartGame();
        }
    }
}
