using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public GameObject menus, miniMap, health, clock;
    public PackageDealer packageDealer;
    private BoatMovement boatMovement;
    private TutorialManager tutorialManager;

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        boatMovement = FindObjectOfType<BoatMovement>();
    }
    void Update()
    {
        if (Input.GetButtonDown("OpenMenu") && tutorialManager.isTutorialOn == false)
        {
            ToggleMenus();
        }
    }

    public void ToggleMenus()
    {
        if (boatMovement != null && boatMovement.isPlayerHarvesting == false && boatMovement.playerPassedOut == false)
        {
            menus.SetActive(!menus.activeSelf);
            miniMap.SetActive(!menus.activeSelf);
            health.SetActive(!menus.activeSelf);
            clock.SetActive(!menus.activeSelf);

            if (menus.activeSelf)
            {
                boatMovement.canPlayerMove = false;
            }
            else
            {
                boatMovement.canPlayerMove = true;
            }
        }
    }
}
