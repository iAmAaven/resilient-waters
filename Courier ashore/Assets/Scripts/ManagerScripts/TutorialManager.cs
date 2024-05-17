using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialStart;
    public bool isTutorialOn = false;
    public void StartTutorial()
    {
        isTutorialOn = true;
        tutorialStart.SetActive(true);
        Time.timeScale = 0;
    }
    public void FinishTutorial()
    {
        Time.timeScale = 1;
        isTutorialOn = false;
    }
}
