using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour
{

    public TextMeshProUGUI deliveryTimeText,
        pickUpPointText, destinationText,
        paycheckText, contrabandText;

    public GameObject[] statusObjects;
    public string[] places;
    public Image packageGFX;
    public Package activePackage;

    public void UpdatePackageInfo(int delivery, string pickUp, string dest, int pay, string cont, Sprite gfx)
    {
        deliveryTimeText.text = delivery + " h";
        pickUpPointText.text = pickUp;
        destinationText.text = dest;
        paycheckText.text = pay + "";
        contrabandText.text = cont;
        packageGFX.sprite = gfx;
        packageGFX.color = new Color(1, 1, 1, 1);
    }

    public void ClearPackageInfo()
    {
        deliveryTimeText.text = "";
        pickUpPointText.text = "";
        destinationText.text = "";
        paycheckText.text = "";
        contrabandText.text = "";
        packageGFX.sprite = null;
        packageGFX.color = new Color(0, 0, 0, 0);

        foreach (GameObject statusObject in statusObjects)
        {
            statusObject.SetActive(false);
        }
    }
}
