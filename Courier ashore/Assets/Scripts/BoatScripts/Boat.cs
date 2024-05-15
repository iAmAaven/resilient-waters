using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [Header("Watergun")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate;

    [Header("Package Stuff")]
    public int boatCapacity;
    public GameObject receiverTrackerPrefab;
    public List<PackageItem> packageItems = new List<PackageItem>();
    public bool isBeingChased;


    private float timer = 0f;

    void Update()
    {
        if (Time.time >= timer && Input.GetButton("Fire1"))
        {
            ShootWatergun();
            timer = Time.time + fireRate;
        }
    }

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

    void ShootWatergun()
    {
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, transform.localRotation);
        Vector2 direction = shootPoint.up;
        newBullet.GetComponent<PlayerBullet>().shootDirection = direction;
    }
}