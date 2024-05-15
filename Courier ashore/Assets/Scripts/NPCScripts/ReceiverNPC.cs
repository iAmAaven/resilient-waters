using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverNPC : MonoBehaviour
{
    public PackageItem packageForThisNPC;
    private bool canGivePackage = false;
    private bool packageGiven = false;
    private BoatMovement boatMovement;

    void Start()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.tag == "Player")
        {
            canGivePackage = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.tag == "Player")
        {
            canGivePackage = false;
        }
    }

    void Update()
    {
        if (canGivePackage && packageGiven == false && Input.GetButtonDown("Interact") && boatMovement.canPlayerMove == true)
        {
            GivePackage();
        }
    }

    void GivePackage()
    {
        packageGiven = true;
        Destroy(packageForThisNPC.tracker);
        packageForThisNPC.packageInfo.PackageDelivered();
        FindObjectOfType<Boat>().RemovePackage(packageForThisNPC);
        Destroy(gameObject);
    }
}
