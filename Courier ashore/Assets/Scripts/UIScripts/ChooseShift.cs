using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseShift : MonoBehaviour
{
    public Animator transitionAnim;
    private string sceneName;
    private bool isTransitioning = false;
    public void ShiftChoice(string choice)
    {
        PlayerPrefs.SetString("CurrentShift", choice);

        if (PlayerPrefs.GetInt("BoatLevel", 1) >= 3
            && PlayerPrefs.GetInt("BoatDurabilityLevel", 1) >= 3
            && PlayerPrefs.GetInt("WaterGunLevel", 1) >= 3
            && PlayerPrefs.GetInt("BoatSpeedLevel", 1) >= 3)
        {
            if (PlayerPrefs.GetString("GameCompleted") == "yes")
            {
                if (isTransitioning == false)
                {
                    isTransitioning = true;
                    sceneName = "ResilientWaters";
                    transitionAnim.SetTrigger("ChangeScene");
                    Invoke("Change", 1.5f);
                }
            }
            else
            {
                if (isTransitioning == false)
                {
                    isTransitioning = true;
                    sceneName = "FinaleCutScene";
                    transitionAnim.SetTrigger("ChangeScene");
                    Invoke("Change", 1.5f);
                }
            }
        }
        else
        {
            if (isTransitioning == false)
            {
                isTransitioning = true;
                sceneName = "ResilientWaters";
                transitionAnim.SetTrigger("ChangeScene");
                Invoke("Change", 1.5f);
            }
        }
    }

    void Change()
    {
        SceneManager.LoadScene(sceneName);
    }
}
