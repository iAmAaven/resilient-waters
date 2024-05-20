using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageItem : MonoBehaviour
{
    public Package packageInfo;
    public GameObject outline;
    public GameObject packageTrackerPrefab;
    public GameObject destinationIsland;
    private bool canPickup = false;
    private bool pickedUp = false;
    private IslandPackageManager islandPackageManager;
    public GameObject graphics;
    public Collider2D triggerCollider;
    [HideInInspector] public GameObject receiverNPC;
    [HideInInspector] public GameObject tracker;

    private Transform playerPos;
    private GameObject packageTracker;
    private BoatMovement boatMovement;
    private DayCycle dayCycle;
    private int deliveryHours = 0;
    private DeliveryUI deliveryUI;
    void Start()
    {
        deliveryUI = FindObjectOfType<DeliveryUI>();
        boatMovement = FindObjectOfType<BoatMovement>();
        playerPos = GameObject.FindWithTag("Player").transform;
        packageTracker = Instantiate(packageTrackerPrefab, playerPos.position, Quaternion.identity);
        packageTracker.GetComponent<PackageTracker>().target = transform;

        islandPackageManager = FindObjectOfType<IslandPackageManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
        {
            outline.SetActive(true);
            canPickup = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
        {
            outline.SetActive(false);
            canPickup = false;
        }
    }

    void Update()
    {
        if (canPickup && pickedUp == false && Input.GetButtonDown("Interact"))
        {
            PickedUp();
        }
    }

    void PickedUp()
    {
        Destroy(packageTracker);
        destinationIsland = islandPackageManager.FindDestination(packageInfo.destination);
        receiverNPC = destinationIsland.GetComponentInChildren<IslandPickupPoint>().SpawnReceiver(packageInfo, this);

        packageInfo.PackagePickedUp();
        FindObjectOfType<Boat>().AddNewPackage(this);
        pickedUp = true;

        StartCoroutine(DeliveryTimer());
        DisablePackageItem();
    }

    void DisablePackageItem()
    {
        graphics.SetActive(false);
        triggerCollider.enabled = false;
    }
    void PackageNotDeliveredOnTime()
    {
        FindObjectOfType<Boat>().RemovePackage(this);
        Destroy(receiverNPC);
        Destroy(tracker);

        if (packageTracker != null)
            Destroy(packageTracker);

        if (packageInfo.gameObject != null)
        {
            Destroy(packageInfo.gameObject);
        }
        FindObjectOfType<ControlUI>().packageDealer.DealNewPackage();
        deliveryUI.ClearPackageInfo();
        Destroy(gameObject);
    }
    IEnumerator DeliveryTimer()
    {
        // dayCycle = FindObjectOfType<DayCycle>();
        // float speed = dayCycle.timeSpeed;
        float speed = 4;

        while (true)
        {
            if (packageInfo.packageDelivered)
            {
                break;
            }

            Debug.Log("Delivery started. Current delivery hour: " + deliveryHours);
            yield return new WaitForSeconds(speed * 6);

            deliveryHours++;
            if (deliveryHours >= packageInfo.deliveryTime)
            {
                PackageNotDeliveredOnTime();
                break;
            }
        }
    }
}
