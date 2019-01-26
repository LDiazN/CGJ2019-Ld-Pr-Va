using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public int initialTime = 10;
    public int actualTime = 0;
    public int timeFruit = 3;
    public Text timeCount;

    private GameRestart gameRestart;
    private PointCount pointCount;

    void Start()
    {
        gameRestart = GetComponent<GameRestart>();
        pointCount = GetComponent<PointCount>();

        actualTime = initialTime;
        timeCount.text = actualTime.ToString();
        InvokeRepeating("DecreaseTime",1f,1f);
    }

    void DecreaseTime()
    {
        if (pointCount.points == pointCount.totalPoints)
        {
            CancelInvoke();
            Debug.Log("Win");
            return;
        }

        if (actualTime > 0)
        {
            actualTime--;
            timeCount.text = actualTime.ToString();
        }
        else
        {
            CancelInvoke();

            if (pointCount.points < pointCount.totalPoints)
            {
                gameRestart.RestartGame();
            }
            else
            {
                Debug.Log("Win");
                return;
            }
        }
    }

    public void IncreaseTime()
    {
        actualTime += timeFruit;
        timeCount.text = actualTime.ToString();
    }
}
