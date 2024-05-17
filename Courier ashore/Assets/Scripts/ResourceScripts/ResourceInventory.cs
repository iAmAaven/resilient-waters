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

    [Header("Stone")]
    public int stoneAmount;
    public int minStone;
    public int maxStone;
    public TextMeshProUGUI stoneText;

    [Header("Coal")]
    public int coalAmount;
    public int minCoal;
    public int maxCoal;
    public TextMeshProUGUI coalText;

    [Header("Iron")]
    public int ironAmount;
    public int minIron;
    public int maxIron;
    public TextMeshProUGUI ironText;

    [Header("Gold")]
    public int goldAmount;
    public int minGold;
    public int maxGold;
    public TextMeshProUGUI goldText;

    [Header("Gem")]
    public int gemAmount;
    public int minGem;
    public int maxGem;
    public TextMeshProUGUI gemText;

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
        woodAmount += Random.Range(minWood, maxWood + 1);
        stoneAmount = RandomChanceAtResource(stoneAmount, stoneChance, 1, (maxStone + 1) / 2, "stone");
        coalAmount = RandomChanceAtResource(coalAmount, coalChance, 1, (maxCoal + 1) / 2, "coal");
        goldAmount = RandomChanceAtResource(goldAmount, goldChance, minGold, maxGold + 1, "gold");

        gemAmount = RandomChanceAtResource(gemAmount, 0.05f, minGem, maxGem + 1, "gem");
        RefreshResources();
    }
    public void DealBigRockResources(float coalChance, float ironChance, float goldChance)
    {
        stoneAmount += Random.Range(minStone, maxStone + 1);
        coalAmount = RandomChanceAtResource(coalAmount, coalChance, minCoal, maxCoal + 1, "coal");
        ironAmount = RandomChanceAtResource(ironAmount, ironChance, minIron, maxIron + 1, "iron");
        goldAmount = RandomChanceAtResource(goldAmount, goldChance, minGold, maxGold + 1, "gold");

        gemAmount = RandomChanceAtResource(gemAmount, 0.05f, minGem, maxGem + 1, "gem");
        RefreshResources();
    }

    int RandomChanceAtResource(int amountOfResource, float chance, int minAmount, int maxAmount, string type)
    {
        int resourceAmountBefore = amountOfResource;
        int addedAmountOfResource;

        float randomChanceAtResource = Random.Range(0f, 1f);

        if (randomChanceAtResource < chance)
        {
            amountOfResource += Random.Range(minAmount, maxAmount);
            addedAmountOfResource = amountOfResource - resourceAmountBefore;

            Debug.Log(addedAmountOfResource + " " + type + " given!");
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
}