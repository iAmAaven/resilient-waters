using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [Header("Waterguns")]
    public Transform waterGunSpawnpoint;
    public GameObject levelOneWaterGun, levelTwoWaterGun, levelThreeWaterGun;

    [Header("Boats")]
    public GameObject levelOneBoatGFX;
    public GameObject levelTwoBoatGFX;
    public GameObject levelThreeBoatGFX;

    [Header("Package Stuff")]
    public int boatCapacity;
    public GameObject receiverTrackerPrefab;
    public List<PackageItem> packageItems = new List<PackageItem>();
    public bool isBeingChased;
    public bool isCarryingContraband = false;

    void Start()
    {
        int boatLevel = PlayerPrefs.GetInt("BoatLevel", 1);
        switch (boatLevel)
        {
            case 1:
                boatCapacity = 3;
                levelOneBoatGFX.SetActive(true);
                break;
            case 2:
                boatCapacity = 4;
                levelTwoBoatGFX.SetActive(true);
                break;
            case 3:
                boatCapacity = 5;
                levelThreeBoatGFX.SetActive(true);
                break;
        }
        SpawnWaterGun();
    }

    public void AddNewPackage(PackageItem packageItem)
    {
        if (packageItems.Count < boatCapacity)
        {
            packageItems.Add(packageItem);
            if (packageItem.packageInfo.isContraband == true)
            {
                isCarryingContraband = true;
                FindObjectOfType<ContrabandManager>().CarryingContraband(isCarryingContraband);
            }
            GameObject newTracker = Instantiate(receiverTrackerPrefab, transform.position, Quaternion.identity);
            packageItem.tracker = newTracker;
            newTracker.GetComponent<PackageTracker>().target = packageItem.receiverNPC.transform;
        }
    }
    public void RemovePackage(PackageItem packageItem)
    {
        packageItems.Remove(packageItem);

        if (packageItems.Count > 0)
        {
            foreach (PackageItem package in packageItems)
            {
                if (package.packageInfo.isContraband == true)
                {
                    isCarryingContraband = true;
                    break;
                }
                isCarryingContraband = false;
            }
        }
        else
        {
            isCarryingContraband = false;
        }

        FindObjectOfType<ContrabandManager>().CarryingContraband(isCarryingContraband);
    }

    void SpawnWaterGun()
    {
        int waterGunLevel = PlayerPrefs.GetInt("WaterGunLevel", 1);
        if (waterGunLevel == 1)
        {
            Instantiate(levelOneWaterGun, waterGunSpawnpoint.position, Quaternion.identity, waterGunSpawnpoint);
        }
        else if (waterGunLevel == 2)
        {
            Instantiate(levelTwoWaterGun, waterGunSpawnpoint.position, Quaternion.identity, waterGunSpawnpoint);
        }
        else
        {
            Instantiate(levelThreeWaterGun, waterGunSpawnpoint.position, Quaternion.identity, waterGunSpawnpoint);
        }
    }
}