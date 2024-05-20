using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    [Header("Wood")]
    public int woodAmount;
    public int minWood;
    public int maxWood;
    public TextMeshProUGUI woodText;
    public GameObject woodGottenText;

    [Header("Stone")]
    public int stoneAmount;
    public int minStone;
    public int maxStone;
    public TextMeshProUGUI stoneText;
    public GameObject stoneGottenText;

    [Header("Coal")]
    public int coalAmount;
    public int minCoal;
    public int maxCoal;
    public TextMeshProUGUI coalText;
    public GameObject coalGottenText;

    [Header("Iron")]
    public int ironAmount;
    public int minIron;
    public int maxIron;
    public TextMeshProUGUI ironText;
    public GameObject ironGottenText;

    [Header("Gold")]
    public int goldAmount;
    public int minGold;
    public int maxGold;
    public TextMeshProUGUI goldText;
    public GameObject goldGottenText;

    [Header("Gem")]
    public int gemAmount;
    public int minGem;
    public int maxGem;
    public TextMeshProUGUI gemText;
    public GameObject gemGottenText;

    void Start()
    {
        woodAmount = LoadResources("Wood");
        stoneAmount = LoadResources("Stone");
        coalAmount = LoadResources("Coal");
        ironAmount = LoadResources("Iron");
        goldAmount = LoadResources("Gold");
        gemAmount = LoadResources("Gem");

        RefreshResources();
    }
    public void RefreshResources()
    {
        woodText.text = "" + woodAmount;
        stoneText.text = "" + stoneAmount;
        coalText.text = "" + coalAmount;
        ironText.text = "" + ironAmount;
        goldText.text = "" + goldAmount;
        gemText.text = "" + gemAmount;

        SaveResources("Wood", woodAmount);
        SaveResources("Stone", stoneAmount);
        SaveResources("Coal", coalAmount);
        SaveResources("Iron", ironAmount);
        SaveResources("Gold", goldAmount);
        SaveResources("Gem", gemAmount);
    }

    public void DealIslandResources(float stoneChance, float coalChance, float goldChance)
    {
        int randomWood = Random.Range(minWood, maxWood + 1);
        woodAmount += randomWood;
        StartCoroutine(PopUpResource(woodGottenText, randomWood));

        stoneAmount = RandomChanceAtResource(stoneAmount, stoneChance, 1, (maxStone + 1) / 2, "stone", stoneGottenText);
        coalAmount = RandomChanceAtResource(coalAmount, coalChance, 1, (maxCoal + 1) / 2, "coal", coalGottenText);
        goldAmount = RandomChanceAtResource(goldAmount, goldChance, minGold, maxGold + 1, "gold", goldGottenText);

        gemAmount = RandomChanceAtResource(gemAmount, 0.10f, minGem, maxGem + 1, "gem", gemGottenText);
        RefreshResources();
    }
    public void DealBigRockResources(float coalChance, float ironChance, float goldChance)
    {
        int randomStone = Random.Range(minStone, maxStone + 1);
        stoneAmount += randomStone;
        StartCoroutine(PopUpResource(stoneGottenText, randomStone));

        coalAmount = RandomChanceAtResource(coalAmount, coalChance, minCoal, maxCoal + 1, "coal", coalGottenText);
        ironAmount = RandomChanceAtResource(ironAmount, ironChance, minIron, maxIron + 1, "iron", ironGottenText);
        goldAmount = RandomChanceAtResource(goldAmount, goldChance, minGold, maxGold + 1, "gold", goldGottenText);

        gemAmount = RandomChanceAtResource(gemAmount, 0.10f, minGem, maxGem + 1, "gem", gemGottenText);
        RefreshResources();
    }

    int RandomChanceAtResource(int amountOfResource, float chance, int minAmount, int maxAmount, string type, GameObject resourcePopUpText)
    {
        int resourceAmountBefore = amountOfResource;
        int addedAmountOfResource;

        float randomChanceAtResource = Random.Range(0f, 1f);

        if (randomChanceAtResource < chance)
        {
            amountOfResource += Random.Range(minAmount, maxAmount);
            addedAmountOfResource = amountOfResource - resourceAmountBefore;

            StartCoroutine(PopUpResource(resourcePopUpText, addedAmountOfResource));
        }

        return amountOfResource;
    }

    void SaveResources(string prefName, int resourceAmount)
    {
        PlayerPrefs.SetInt(prefName, resourceAmount);
    }
    int LoadResources(string prefName)
    {
        return PlayerPrefs.GetInt(prefName);
    }

    IEnumerator PopUpResource(GameObject popUpText, int amountGotten)
    {
        popUpText.SetActive(true);
        popUpText.GetComponentInChildren<TextMeshProUGUI>().text = "+" + amountGotten;
        yield return new WaitForSeconds(3f);
        popUpText.gameObject.SetActive(false);
    }
}