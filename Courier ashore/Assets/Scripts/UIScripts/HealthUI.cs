using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;

    public void RefreshHealth()
    {
        healthSlider.maxValue = PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10;
        healthSlider.value = PlayerPrefs.GetInt("BoatHP", 10);
    }
}
