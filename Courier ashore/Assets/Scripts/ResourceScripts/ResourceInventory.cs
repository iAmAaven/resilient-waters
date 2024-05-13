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
    }

    public void DealResources(bool isIsland)
    {
        if (isIsland)
        {
            woodAmount += Random.Range(minWood, maxWood + 1);
            stoneAmount = RandomChanceAtResource(stoneAmount, 0.75f, 1, (maxStone + 1) / 2, "stone");
            coalAmount = RandomChanceAtResource(coalAmount, 0.5f, 1, (maxCoal + 1) / 2, "coal");
            goldAmount = RandomChanceAtResource(goldAmount, 0.25f, minGold, maxGold + 1, "gold");
        }
        else
        {
            stoneAmount += Random.Range(minStone, maxStone + 1);
            coalAmount = RandomChanceAtResource(coalAmount, 0.75f, minCoal, maxCoal + 1, "coal");
            ironAmount = RandomChanceAtResource(ironAmount, 0.5f, minIron, maxIron + 1, "iron");
            goldAmount = RandomChanceAtResource(goldAmount, 0.33f, minGold, maxGold + 1, "gold");
        }

        gemAmount = RandomChanceAtResource(gemAmount, 0.1f, minGem, maxGem + 1, "gem");
        RefreshResources();
    }

    int RandomChanceAtResource(int amountOfResource, float chance, int minAmount, int maxAmount, string type)
    {
        int resourceAmountBefore = amountOfResource;
        float randomChanceAtResource = Random.Range(0f, 1f);
        if (randomChanceAtResource < chance)
        {
            amountOfResource += Random.Range(minAmount, maxAmount);
            Debug.Log(amountOfResource - resourceAmountBefore + " " + type + " given!");
        }

        return amountOfResource;
    }
}
