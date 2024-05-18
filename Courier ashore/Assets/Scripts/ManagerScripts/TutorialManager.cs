using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialStart;
    private BoatMovement boatMovement;
    public bool isTutorialOn = false;
    public void StartTutorial()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
        boatMovement.canPlayerMove = false;
        isTutorialOn = true;
        tutorialStart.SetActive(true);
    }
    public void FinishTutorial()
    {
        boatMovement.canPlayerMove = true;
        isTutorialOn = false;
    }
}
