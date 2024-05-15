using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryButtons : MonoBehaviour
{
    public Button[] deliveryInfoButtons;
    public GameObject accepted, pickedUp, delivered;
    public Slider denySlider;

    private int deniesLeft;
    private DeliveryUI deliveryUI;
    private Package _activePackage;
    private IslandPackageManager islandPackageManager;

    void Start()
    {
        deniesLeft = (int)denySlider.maxValue;

        islandPackageManager = FindObjectOfType<IslandPackageManager>();
        deliveryUI = FindObjectOfType<DeliveryUI>();
        if (deliveryUI != null && deliveryUI.activePackage == null)
        {
            DisableButtons();
        }
    }

    public void ScanPackage()
    {
        _activePackage = deliveryUI.activePackage;
        if (_activePackage.isContraband)
        {
            _activePackage.contraband = "yes";
            deliveryUI.contrabandText.text = _activePackage.contraband;
            deliveryUI.packageGFX.sprite = _activePackage.scannedContrabandGFX[_activePackage.randomSpriteIndex];
        }
        else
        {
            _activePackage.contraband = "no";
            deliveryUI.contrabandText.text = _activePackage.contraband;
            deliveryUI.packageGFX.sprite = _activePackage.scannedGFX[_activePackage.randomSpriteIndex];
        }
    }
    public void DenyPackage()
    {
        _activePackage = deliveryUI.activePackage;
        CreditManager creditManager = FindObjectOfType<CreditManager>();

        if (creditManager.credits >= 3 && deniesLeft > 0)
        {
            deniesLeft--;
            denySlider.value = deniesLeft;
            creditManager.AddCredits(-3);
            FindObjectOfType<PackageDealer>().DealNewPackage();
            Destroy(_activePackage.gameObject);
            deliveryUI.ClearPackageInfo();
        }
    }
    public void AcceptPackage()
    {
        _activePackage = deliveryUI.activePackage;
        DisableButtons();
        _activePackage.packageAccepted = true;
        accepted.SetActive(true);
        islandPackageManager.DistributePackageToIsland(_activePackage.pickUpPoint, _activePackage);
    }
    public void DemandMorePay()
    {
        _activePackage = deliveryUI.activePackage;
        if (_activePackage.contraband == "yes" && _activePackage.demandedMorePay == false)
        {
            _activePackage.paycheck = (int)Math.Round(_activePackage.paycheck * 1.5, 0);
            deliveryUI.paycheckText.text = _activePackage.paycheck + "";
            _activePackage.demandedMorePay = true;
        }
    }
    public void CashPackageIn()
    {
        _activePackage = deliveryUI.activePackage;
        FindObjectOfType<CreditManager>().AddCredits(_activePackage.paycheck);
        delivered.SetActive(false);
        deliveryUI.ClearPackageInfo();
        FindObjectOfType<PackageDealer>().DealNewPackage();
        Destroy(_activePackage.gameObject);
    }

    void DisableButtons()
    {
        foreach (Button button in deliveryInfoButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
