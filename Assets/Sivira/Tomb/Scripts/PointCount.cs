using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCount : MonoBehaviour
{
    public int points = 0;
    public int totalPoints = 30;
    public Text scorePoints;

    public void IncreasePoints()
    {
        if (points <= totalPoints)
        {
            points++;
            scorePoints.text = points.ToString();
        }
    }
}
