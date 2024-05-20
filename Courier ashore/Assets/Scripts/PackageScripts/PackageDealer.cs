using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDealer : MonoBehaviour
{
    public GameObject packagePrefab, shiftFinishedPrefab;
    public bool isFinale = false;
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
        if (dayCycle != null || isFinale == true)
        {
            string shift = PlayerPrefs.GetString("CurrentShift", "Day");

            if (isFinale == true || (dayCycle != null && dayCycle.shiftFinished == false))
            {
                GameObject newPackage = Instantiate(packagePrefab, transform);
                if (shift == "Day")
                {
                    newPackage.GetComponent<Package>().paycheckMultiplier = 1;
                }
                else
                {
                    newPackage.GetComponent<Package>().paycheckMultiplier = 1.5;
                }
            }
            else
            {
                Instantiate(shiftFinishedPrefab, transform);
            }
        }
    }
}
