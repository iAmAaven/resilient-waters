using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryButtons : MonoBehaviour
{
    public Button[] deliveryInfoButtons;
    public GameObject accepted;
    private DeliveryUI deliveryUI;
    private Package _activePackage;
    private IslandPackageManager islandPackageManager;

    void Start()
    {
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
    }
    public void AcceptPackage()
    {
        DisableButtons();
        accepted.SetActive(true);

        // islandPackageManager
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

    void DisableButtons()
    {
        foreach (Button button in deliveryInfoButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
