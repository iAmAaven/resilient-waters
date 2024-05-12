using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    public int deliveryTime;
    public string pickUpPoint;
    public string destination;
    public int paycheck;
    public double paycheckMultiplier = 1;
    public string contraband = "n/a";
    public bool isContraband = false;
    public Sprite[] packageGFX, scannedGFX, contrabandGFX, scannedContrabandGFX;
    public Sprite finalGFX;
    public bool demandedMorePay = false;

    private DeliveryUI deliveryUI;
    private IslandPackageManager islandPackageManager;
    private DeliveryButtons deliveryButtons;
    [HideInInspector] public int randomSpriteIndex;

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

    // BUTTON SCRIPTS

    public void ActivatePackage()
    {
        if (deliveryUI.activePackage != this)
        {
            deliveryUI.activePackage = this;

            foreach (Button button in deliveryButtons.deliveryInfoButtons)
            {
                button.gameObject.SetActive(true);
            }

            deliveryUI.UpdatePackageInfo(deliveryTime, pickUpPoint, destination, paycheck, contraband, finalGFX);
        }
    }
}
