using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HarvestUpgrades : MonoBehaviour
{
    [Header("Harvest upgrade")]
    public TextMeshProUGUI harvestLevelText;
    public int harvestLevel;
    public int harvestRequiredCredits;
    public int harvestRequiredWood, harvestRequiredStone, harvestRequiredCoal,
        harvestRequiredIron, harvestRequiredGold, harvestRequiredGem;

    public TextMeshProUGUI harvestRequiredWoodText, harvestRequiredStoneText,
        harvestRequiredCoalText, harvestRequiredIronText, harvestRequiredGoldText,
        harvestRequiredGemText, harvestRequiredCreditsText;

    public int maxUpgradeLevel = 3;

    [Header("References")]
    public ResourceInventory resourceInventory;
    public CreditManager creditManager;

    void OnEnable()
    {
        harvestLevel = PlayerPrefs.GetInt("HarvestLevel", 1);
        LoadAllRequirements();
    }

    public void UpgradeHarvestSpeed()
    {
        if (harvestLevel < maxUpgradeLevel
            && creditManager.credits >= harvestRequiredCredits
            && resourceInventory.woodAmount >= harvestRequiredWood
            && resourceInventory.stoneAmount >= harvestRequiredStone
            && resourceInventory.coalAmount >= harvestRequiredCoal
            && resourceInventory.ironAmount >= harvestRequiredIron
            && resourceInventory.goldAmount >= harvestRequiredGold
            && resourceInventory.gemAmount >= harvestRequiredGem)
        {
            resourceInventory.woodAmount -= harvestRequiredWood;
            resourceInventory.stoneAmount -= harvestRequiredStone;
            resourceInventory.coalAmount -= harvestRequiredCoal;
            resourceInventory.ironAmount -= harvestRequiredIron;
            resourceInventory.goldAmount -= harvestRequiredGold;
            resourceInventory.gemAmount -= harvestRequiredGem;
            creditManager.credits -= harvestRequiredCredits;

            harvestLevel++;
            PlayerPrefs.SetInt("HarvestLevel", harvestLevel);

            harvestRequiredWood = RaisedRequirement(harvestRequiredWood, harvestRequiredWoodText);
            harvestRequiredStone = RaisedRequirement(harvestRequiredStone, harvestRequiredStoneText);
            harvestRequiredCoal = RaisedRequirement(harvestRequiredCoal, harvestRequiredCoalText);
            harvestRequiredIron = RaisedRequirement(harvestRequiredIron, harvestRequiredIronText);
            harvestRequiredGold = RaisedRequirement(harvestRequiredGold, harvestRequiredGoldText);
            harvestRequiredGem = RaisedRequirement(harvestRequiredGem, harvestRequiredGemText);
            harvestRequiredCredits = RaisedRequirement(harvestRequiredCredits, harvestRequiredCreditsText);

            if (harvestLevel >= maxUpgradeLevel)
            {
                harvestLevelText.text = "MAX";
                harvestRequiredWoodText.text = "MAX";
                harvestRequiredStoneText.text = "MAX";
                harvestRequiredCoalText.text = "MAX";
                harvestRequiredIronText.text = "MAX";
                harvestRequiredGoldText.text = "MAX";
                harvestRequiredGemText.text = "MAX";
                harvestRequiredCreditsText.text = "MAX";
                return;
            }
            else
            {
                harvestLevelText.text = "LVL " + harvestLevel;
            }

            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources or max level");
        }
    }
    private int RaisedRequirement(int required, TextMeshProUGUI requiredText)
    {
        required = (int)(required * 1.15);
        requiredText.text = "" + required;
        return required;
    }

    public void RefreshRequirementTexts()
    {
        harvestRequiredWoodText.text = harvestRequiredWood + "";
        harvestRequiredStoneText.text = harvestRequiredStone + "";
        harvestRequiredCoalText.text = harvestRequiredCoal + "";
        harvestRequiredIronText.text = harvestRequiredIron + "";
        harvestRequiredGoldText.text = harvestRequiredGold + "";
        harvestRequiredGemText.text = harvestRequiredGem + "";
        harvestRequiredCreditsText.text = harvestRequiredCredits + "";
    }
    public int LoadRequirement(string upgradeLevel, int requiredResource, TextMeshProUGUI levelText)
    {
        int resourceRequired = requiredResource;

        if (PlayerPrefs.GetInt(upgradeLevel) == 1 || PlayerPrefs.GetInt(upgradeLevel) == 0)
        {
            levelText.text = "LVL " + 1;
        }
        else if (PlayerPrefs.GetInt(upgradeLevel) == 2 && PlayerPrefs.GetInt(upgradeLevel) < maxUpgradeLevel)
        {
            levelText.text = "LVL " + 2;
            resourceRequired = (int)(requiredResource * 1.15);
        }
        else if (PlayerPrefs.GetInt(upgradeLevel) >= 3 && PlayerPrefs.GetInt(upgradeLevel) < maxUpgradeLevel)
        {
            levelText.text = "MAX";
            resourceRequired = (int)(requiredResource * 1.15 * 1.15);
        }
        else
        {
            levelText.text = "MAX";
        }
        return resourceRequired;
    }
    public void LoadAllRequirements()
    {
        harvestRequiredWood = LoadRequirement("HarvestLevel", harvestRequiredWood, harvestLevelText);
        harvestRequiredStone = LoadRequirement("HarvestLevel", harvestRequiredStone, harvestLevelText);
        harvestRequiredCoal = LoadRequirement("HarvestLevel", harvestRequiredCoal, harvestLevelText);
        harvestRequiredIron = LoadRequirement("HarvestLevel", harvestRequiredIron, harvestLevelText);
        harvestRequiredGold = LoadRequirement("HarvestLevel", harvestRequiredGold, harvestLevelText);
        harvestRequiredGem = LoadRequirement("HarvestLevel", harvestRequiredGem, harvestLevelText);
        harvestRequiredCredits = LoadRequirement("HarvestLevel", harvestRequiredCredits, harvestLevelText);

        RefreshRequirementTexts();
    }
}
