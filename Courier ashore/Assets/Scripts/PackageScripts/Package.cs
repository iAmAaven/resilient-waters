using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    [Header("Package info")]
    public int deliveryTime;
    public string pickUpPoint;
    public string destination;

    [Header("Paycheck info")]
    public int paycheck;
    public double paycheckMultiplier = 1;

    [Header("Contraband info")]
    public string contraband = "n/a";
    public bool isContraband = false;

    [Header("Sprites")]
    [HideInInspector] public Sprite finalGFX;
    public Sprite[] packageGFX, scannedGFX, contrabandGFX, scannedContrabandGFX;

    [Header("References")]
    public GameObject checkmark;
    public GameObject newMarker;
    public GameObject fastDelivery;


    // STATUS MANAGEMENT OF THE PACKAGE
    [HideInInspector] public bool demandedMorePay = false;
    [HideInInspector] public bool packageAccepted = false;
    [HideInInspector] public bool packagePickedUp = false;
    [HideInInspector] public bool packageDelivered = false;
    [HideInInspector] public PackageItem thisPackageItem;


    // OTHER HIDDEN VARIABLES
    [HideInInspector] public int randomSpriteIndex;


    // PRIVATES
    private DeliveryUI deliveryUI;
    private DeliveryButtons deliveryButtons;
    private IslandPackageManager islandPackageManager;

    void Start()
    {
        deliveryButtons = FindObjectOfType<DeliveryButtons>();
        islandPackageManager = FindObjectOfType<IslandPackageManager>();
        deliveryUI = FindObjectOfType<DeliveryUI>();
        RandomPackage();
    }
    public void RandomPackage()
    {
        RandomDeliveryTime();
        RandomPickUpPoint();
        RandomDestination();
        RandomizeContraband();
        RandomPaycheck();
        RandomSprite();
    }
    void RandomDeliveryTime()
    {
        float randomChance = Random.Range(0f, 1f);
        if (randomChance < 0.1f)
        {
            deliveryTime = 2;
            fastDelivery.SetActive(true);
        }
        else
        {
            deliveryTime = Random.Range(6, 13);
        }
    }
    void RandomPickUpPoint()
    {
        pickUpPoint = deliveryUI.places[Random.Range(0, deliveryUI.places.Length)];
    }
    void RandomDestination()
    {
        destination = deliveryUI.places[Random.Range(0, deliveryUI.places.Length)];
        if (destination == pickUpPoint)
        {
            RandomDestination();
        }
    }
    void RandomPaycheck()
    {
        if (deliveryTime == 2)
        {
            paycheck = (int)(Random.Range(35, 46) * paycheckMultiplier);
        }
        else
        {
            paycheck = (int)(Random.Range(10, 26) * paycheckMultiplier);
        }
    }
    void RandomizeContraband()
    {
        float randomChance = Random.Range(0f, 1f);
        if (randomChance < 0.25f)
        {
            isContraband = true;
        }
        else
        {
            isContraband = false;
        }
    }
    void RandomSprite()
    {
        randomSpriteIndex = Random.Range(0, packageGFX.Length);

        if (isContraband)
        {
            finalGFX = contrabandGFX[randomSpriteIndex];
        }
        else
        {
            finalGFX = packageGFX[randomSpriteIndex];
        }
    }

    public void PackagePickedUp()
    {
        packagePickedUp = true;

        if (deliveryUI.activePackage == this)
        {
            deliveryButtons.pickedUp.SetActive(true);
            deliveryButtons.delivered.SetActive(false);
            deliveryButtons.accepted.SetActive(false);
        }
    }
    public void PackageDelivered()
    {
        packageDelivered = true;
        checkmark.SetActive(true);

        if (deliveryUI.activePackage == this)
        {
            deliveryButtons.delivered.SetActive(true);
            deliveryButtons.pickedUp.SetActive(false);
            deliveryButtons.accepted.SetActive(false);
        }
    }
    // BUTTON SCRIPTS

    public void ActivatePackage()
    {
        if (deliveryUI.activePackage != this)
        {
            newMarker.SetActive(false);
            deliveryUI.activePackage = this;

            if (packageAccepted == false)
            {
                foreach (Button button in deliveryButtons.deliveryInfoButtons)
                {
                    button.gameObject.SetActive(true);
                }
                deliveryButtons.accepted.SetActive(false);
                deliveryButtons.pickedUp.SetActive(false);
                deliveryButtons.delivered.SetActive(false);
            }
            else
            {
                foreach (Button button in deliveryButtons.deliveryInfoButtons)
                {
                    button.gameObject.SetActive(false);
                }

                if (packagePickedUp == false)
                {
                    deliveryButtons.accepted.SetActive(true);
                    deliveryButtons.pickedUp.SetActive(false);
                    deliveryButtons.delivered.SetActive(false);
                }
                else
                {
                    deliveryButtons.pickedUp.SetActive(true);
                    deliveryButtons.delivered.SetActive(false);
                    deliveryButtons.accepted.SetActive(false);
                }

                if (packageDelivered)
                {
                    deliveryButtons.delivered.SetActive(true);
                    deliveryButtons.pickedUp.SetActive(false);
                    deliveryButtons.accepted.SetActive(false);
                }
            }

            deliveryUI.UpdatePackageInfo(deliveryTime, pickUpPoint, destination, paycheck, contraband, finalGFX);
        }
    }
}
