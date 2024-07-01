using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinaleCredits : MonoBehaviour
{
    public int finaleCredits;
    public TextMeshProUGUI creditsText;
    public FinaleTimer finaleTimer;
    private bool failed = false;
    public DeliveryUI deliveryUI;
    public SceneChange sceneChange;

    public void AddFinaleCredits()
    {
        finaleCredits += deliveryUI.activePackage.paycheck;
        creditsText.text = "" + finaleCredits;
        if (finaleCredits >= 1000)
        {
            FinaleSuccess();
        }
    }
    public void FinaleSuccess()
    {
        if (failed == false)
        {
            if (sceneChange != null)
            {
                sceneChange.ChangeScene("FinaleSuccess");
            }
            else
            {
                SceneManager.LoadScene("FinaleSuccess");
            }
        }
    }
    public void FinaleFailure()
    {
        if (failed == false)
        {
            failed = true;
            if (sceneChange != null)
            {
                sceneChange.ChangeScene("FinaleFailure");
            }
            else
            {
                SceneManager.LoadScene("FinaleFailure");
            }
        }
    }
}