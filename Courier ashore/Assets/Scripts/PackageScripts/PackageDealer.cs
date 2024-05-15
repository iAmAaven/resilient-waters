using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDealer : MonoBehaviour
{
    public GameObject packagePrefab, shiftFinishedPrefab;

    private Boat boat;
    private DayCycle dayCycle;
    void Start()
    {
        dayCycle = FindObjectOfType<DayCycle>();
        boat = FindObjectOfType<Boat>();
        for (int i = 0; i < boat.boatCapacity; i++)
        {
            DealNewPackage();
        }
    }

    public void DealNewPackage()
    {
        if (dayCycle != null)
        {
            string shift = PlayerPrefs.GetString("CurrentShift", "Day");
            GameObject newPackage;

            if (dayCycle.shiftFinished == false)
            {
                newPackage = Instantiate(packagePrefab, transform);
            }
            else
            {
                newPackage = Instantiate(shiftFinishedPrefab, transform);
            }

            if (shift == "Day")
            {
                newPackage.GetComponent<Package>().paycheckMultiplier = 1;
            }
            else
            {
                newPackage.GetComponent<Package>().paycheckMultiplier = 1.5;
            }
        }
    }
}
