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
            Instantiate(packagePrefab, transform);
        }
    }

    public void DealNewPackage()
    {
        if (dayCycle != null)
        {
            if (dayCycle.shiftFinished == false)
            {
                Instantiate(packagePrefab, transform);
            }
            else
            {
                Instantiate(shiftFinishedPrefab, transform);
            }
        }
    }
}
