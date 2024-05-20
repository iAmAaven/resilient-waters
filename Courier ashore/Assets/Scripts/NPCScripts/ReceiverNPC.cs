using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverNPC : MonoBehaviour
{
    public GameObject cue;

    [HideInInspector] public PackageItem packageForThisNPC;
    private bool canGivePackage = false;
    private bool packageGiven = false;
    private BoatMovement boatMovement;

    void Start()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        cue.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.tag == "Player")
        {
            canGivePackage = true;
            cue.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.tag == "Player")
        {
            canGivePackage = false;
            cue.SetActive(false);
        }
    }

    void Update()
    {
        if (canGivePackage && packageGiven == false && Input.GetButtonDown("Interact"))
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
