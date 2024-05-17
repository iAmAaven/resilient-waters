using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCount : MonoBehaviour
{
    public int dayCount;
    public bool isResilientWaters = false;
    public TextMeshProUGUI dayText;

    void Start()
    {
        dayCount = PlayerPrefs.GetInt("DayCount", 0);

        if (isResilientWaters == true)
        {
            AddDay();
        }
        else
        {
            if (dayText != null)
                dayText.text = "DAY " + dayCount;
        }
    }
    public void AddDay()
    {
        if (dayCount == 0)
        {
            FindObjectOfType<TutorialManager>().StartTutorial();
        }
        dayCount++;
        PlayerPrefs.SetInt("DayCount", dayCount);
    }
}
