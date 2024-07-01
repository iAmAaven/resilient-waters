using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinaleTimer : MonoBehaviour
{
    public float speed = 1;
    public int minutesToCompleteIn;
    public int minutes, seconds = 60;
    public TextMeshProUGUI timerText;
    public FinaleCredits finaleCredits;

    private float timer = 0f;

    void Start()
    {
        minutes = minutesToCompleteIn - 1;
    }
    void Update()
    {
        if (Time.time >= timer)
        {
            RefreshFinaleTime();
            timer = Time.time + speed;
        }
    }

    void RefreshFinaleTime()
    {
        if (minutes <= 0 && seconds <= 0)
        {
            timerText.text = "0:00";
            finaleCredits.FinaleFailure();
            return;
        }

        seconds--;
        if (seconds < 0)
        {
            seconds = 59;
            minutes--;
        }

        if (seconds < 10 && seconds > 0)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else if (seconds == 0)
        {
            timerText.text = minutes + ":" + seconds + "0";
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
    }
}
