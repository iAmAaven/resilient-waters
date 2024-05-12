using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public bool canPlayerMove = false;
    public GameObject menus;
    void Update()
    {
        if (Input.GetButtonDown("OpenMenu"))
        {
            menus.SetActive(!menus.activeSelf);
            if (menus.activeSelf)
            {
                canPlayerMove = true;
            }
            else
            {
                canPlayerMove = false;
            }
        }
    }
}
