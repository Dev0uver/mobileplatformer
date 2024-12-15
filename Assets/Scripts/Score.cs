using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static float seconds = 0.0f;
    public static float minutes = 0.0f;

    Text text;
    Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.transform.GetChild(0).GetComponent<Text>();
        timeText = gameObject.transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString();
        seconds += Time.deltaTime;
        if (seconds >= 60 && minutes < 99)
        {
            seconds = 0;
            minutes++;
        }

        timeText.text = parseTime();
    }

    public static string parseTime()
    {
        string timeString = "";
        timeString += minutes.ToString() + ":";
        if (seconds < 10)
        {
            timeString += "0";
        }
        timeString += ((float)Math.Floor(seconds)).ToString();

        return timeString;
    }
}
