using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public GameObject menus, miniMap, health;
    private BoatMovement boatMovement;

    void Start()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
    }
    void Update()
    {
        if (Input.GetButtonDown("OpenMenu"))
        {
            ToggleMenus();
        }
    }

    public void ToggleMenus()
    {
        if (boatMovement != null && boatMovement.isPlayerHarvesting == false)
        {
            menus.SetActive(!menus.activeSelf);
            miniMap.SetActive(!menus.activeSelf);
            health.SetActive(!menus.activeSelf);

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
