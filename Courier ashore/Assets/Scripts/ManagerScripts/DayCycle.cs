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

    [Header("Lighting")]
    public Light2D sunlight;
    public Color32 nightColor;
    public Color32 dayColor;


    // PRIVATES
    private float timer = 0f;
    private string currentShift;
    private bool isTextFlashing = false;
    private BoatMovement boatMovement;
    private TutorialManager tutorialManager;
    private bool isTransitioning = false;

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        timer = timeSpeed;
        boatMovement = FindObjectOfType<BoatMovement>();
        currentShift = PlayerPrefs.GetString("CurrentShift", "Day");
        PlayerPrefs.SetString("CurrentShift", currentShift);

        StartCurrentShift(currentShift);
    }

    void Update()
    {
        if (tutorialManager.isTutorialOn == false && Time.time >= timer)
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
            sunlight.color = dayColor;
            currentHour = dayShiftStart;
            timeText.text = currentHour + ":" + "00";
        }
        else
        {
            sunlight.color = nightColor;
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
                if (boatMovement.playerPassedOut == false && currentHour == dayShiftPassOutHour && isTransitioning == false)
                {
                    NovaPassedOut();
                    return;
                }
                else if (currentHour == dayShiftPassOutHour - 1 && isTextFlashing == false)
                {
                    StartCoroutine(TextFlashing());
                }
                if (currentHour >= dayShiftEnd && shiftFinished == false)
                {
                    StartCoroutine(LightChange(timeSpeed * 18, dayColor, nightColor));
                    shiftFinished = true;
                }
            }
            else if (currentShift == "Night")
            {
                if (currentHour == nightShiftPassOutHour && isTransitioning == false)
                {
                    NovaPassedOut();
                }
                else if (currentHour == nightShiftPassOutHour - 1 && isTextFlashing == false)
                {
                    StartCoroutine(TextFlashing());
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
        FindObjectOfType<PassOut>().PassedOut();
        isTransitioning = true;
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

    IEnumerator TextFlashing()
    {
        isTextFlashing = true;

        for (int i = 0; i < 20; i++)
        {
            timeText.color = new Color32(0, 0, 0, 0);
            yield return new WaitForSeconds(0.1f);
            timeText.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
        }
        timeText.color = new Color32(255, 255, 255, 255);
        isTextFlashing = false;
    }
}
