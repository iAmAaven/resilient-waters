using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public int credits;
    public TextMeshProUGUI creditText;

    public void AddCredits(int creditIncrease)
    {
        credits += creditIncrease;
        if (credits < 0)
        {
            credits = 0;
        }
        creditText.text = "" + credits;
    }
}
