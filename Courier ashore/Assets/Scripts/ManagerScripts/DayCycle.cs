using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DayCycle : MonoBehaviour
{
    public int dayShiftStart, dayShiftEnd, dayShiftPassOutHour;
    public int nightShiftStart, nightShiftEnd, nightShiftPassOutHour;
    public int currentHour, currentMinute;
    public float timeSpeed;
    public TextMeshProUGUI timeText;
    public bool shiftStarted = false;
    public bool shiftFinished = false;
    private float timer = 0f;
    private string currentShift;
    [Header("Lighting")]
    public Light2D sunlight;
    public Color32 nightColor;
    public Color32 dayColor;

    void Start()
    {
        currentShift = PlayerPrefs.GetString("CurrentShift");
        if (string.IsNullOrEmpty(currentShift))
        {
            currentShift = "Day";
            PlayerPrefs.SetString("CurrentShift", currentShift);
        }

        StartCurrentShift(currentShift);
    }

    void Update()
    {
        if (Time.time >= timer)
        {
            RefreshTime();
            timer = Time.time + timeSpeed;
        }
    }

    void StartCurrentShift(string shift)
    {
        currentMinute = 0;
        shiftFinished = false;
        shiftStarted = true;

        if (shift == "Day")
        {
            currentHour = dayShiftStart;
            timeText.text = currentHour + ":" + "00";
        }
        else
        {
            currentHour = nightShiftStart;
            timeText.text = currentHour + ":" + "00";
        }
    }

    void RefreshTime()
    {
        currentMinute += 10;

        if (currentMinute >= 60)
        {
            currentHour++;
            if (currentHour >= 24)
            {
                currentHour = 0;
            }

            currentMinute = 0;
            timeText.text = currentHour + ":" + currentMinute + "0";

            if (currentShift == "Day")
            {
                if (currentHour == dayShiftPassOutHour)
                {
                    NovaPassedOut();
                    return;
                }
                if (currentHour >= dayShiftEnd && shiftFinished == false)
                {
                    StartCoroutine(LightChange(timeSpeed * 18, dayColor, nightColor));
                    shiftFinished = true;
                }
            }
            else if (currentShift == "Night")
            {
                if (currentHour == nightShiftPassOutHour)
                {
                    NovaPassedOut();
                }
                if (currentHour >= nightShiftEnd && shiftFinished == false)
                {
                    StartCoroutine(LightChange(timeSpeed * 18, nightColor, dayColor));
                    shiftFinished = true;
                }
            }

            return;
        }

        timeText.text = currentHour + ":" + currentMinute;
    }

    void NovaPassedOut()
    {
        Debug.Log("Nova passed out");
        SceneManager.LoadScene("HomeIsland");
    }

    IEnumerator LightChange(float lightFadeDuration, Color32 startColor, Color32 targetColor)
    {
        float elapsedCycle = 0f;

        while (elapsedCycle < lightFadeDuration)
        {
            sunlight.color = Color.Lerp(startColor, targetColor, elapsedCycle / lightFadeDuration);
            elapsedCycle += Time.deltaTime;
            yield return null;
        }
        sunlight.color = targetColor;
    }
}
