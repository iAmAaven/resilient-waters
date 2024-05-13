using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDealer : MonoBehaviour
{
    public GameObject packagePrefab;

    private Boat boat;
    void Start()
    {
        boat = FindObjectOfType<Boat>();
        for (int i = 0; i < boat.boatCapacity; i++)
        {
            Instantiate(packagePrefab, transform);
        }
    }

    public void DealNewPackage()
    {
        Instantiate(packagePrefab, transform);
    }
}
