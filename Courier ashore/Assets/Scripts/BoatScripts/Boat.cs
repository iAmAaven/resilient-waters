using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public int boatCapacity;
    public GameObject receiverTrackerPrefab;
    public List<PackageItem> packageItems = new List<PackageItem>();

    public void AddNewPackage(PackageItem packageItem)
    {
        if (packageItems.Count < boatCapacity)
        {
            packageItems.Add(packageItem);
            GameObject newTracker = Instantiate(receiverTrackerPrefab, transform.position, Quaternion.identity);
            packageItem.tracker = newTracker;
            newTracker.GetComponent<PackageTracker>().target = packageItem.receiverNPC.transform;
        }
    }
    public void RemovePackage(PackageItem packageItem)
    {
        packageItems.Remove(packageItem);
    }
}