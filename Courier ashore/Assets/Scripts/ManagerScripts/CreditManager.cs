using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public int credits;
    public TextMeshProUGUI creditText;
    void Start()
    {
        int savedCredits = PlayerPrefs.GetInt("Credits");
        if (savedCredits != 0)
        {
            credits = savedCredits;
            AddCredits(0);
        }
    }

    public void AddCredits(int creditIncrease)
    {
        credits += creditIncrease;
        if (credits < 0)
        {
            credits = 0;
        }
        creditText.text = "" + credits;
        PlayerPrefs.SetInt("Credits", credits);
    }
}
