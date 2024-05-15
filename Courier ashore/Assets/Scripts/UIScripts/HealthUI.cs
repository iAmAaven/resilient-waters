using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;

    public void RefreshHealth(int health, int maxHealth)
    {
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;
    }
}
