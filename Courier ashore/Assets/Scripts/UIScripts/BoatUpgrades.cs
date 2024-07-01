using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatUpgrades : MonoBehaviour
{
    /*
        
        THIS CODE IS A F*NG MESS. DON'T USE THIS CODE AS LEARNING MATERIAL–IT'LL ONLY HURT YOU.
        THERE WERE PROBABLY A MILLION BETTER WAYS TO ACHIEVE THE SAME THING I'VE DONE HERE,
        BUT I'LL USE THE "IT WAS FOR A GAME JAM AND MADE WITH SERIOUS SLEEP DEPRIVATION" EXCUSE FOR THIS ONE.

        I'VE TRIED MY BEST AT COMMENTING THE CODE.

    */

    [Header("Upgrade levels")]
    public int boatSpeedLevel = 1;
    public TextMeshProUGUI boatSpeedLevelText;
    public int waterGunLevel = 1;
    public TextMeshProUGUI waterGunLevelText;
    public int boatDurabilityLevel = 1;
    public TextMeshProUGUI boatDurabilityLevelText;
    public int boatLevel = 1;
    public TextMeshProUGUI boatLevelText;
    public int maxLevel = 2;

    [Header("Repair boat")]
    public int repairRequiredCredits = 5;
    public int repairRequiredWood;
    public TextMeshProUGUI repairRequiredWoodText;
    public TextWriter repairDialogueText;

    [Header("Boat speed")]
    public int speedRequiredCredits;
    public int speedRequiredWood, speedRequiredCoal, speedRequiredGold;
    public TextMeshProUGUI speedRequiredWoodText, speedRequiredCoalText, speedRequiredGoldText, speedRequiredCreditsText;

    [Header("Water gun")]
    public int gunRequiredCredits;
    public int gunRequiredWood, gunRequiredIron, gunRequiredGold;
    public TextMeshProUGUI gunRequiredWoodText, gunRequiredIronText, gunRequiredGoldText, gunRequiredCreditsText;

    [Header("Boat durability")]
    public int durabRequiredCredits;
    public int durabRequiredWood, durabRequiredStone, durabRequiredIron;
    public TextMeshProUGUI durabRequiredWoodText, durabRequiredStoneText, durabRequiredIronText, durabRequiredCreditsText;

    [Header("Buy a new boat")]
    public int buyBoatRequiredCredits;
    public int buyBoatRequiredWood, buyBoatRequiredStone, buyBoatRequiredCoal,
        buyBoatRequiredIron, buyBoatRequiredGold, buyBoatRequiredGem;

    public TextMeshProUGUI buyBoatRequiredWoodText, buyBoatRequiredStoneText,
        buyBoatRequiredCoalText, buyBoatRequiredIronText, buyBoatRequiredGoldText,
        buyBoatRequiredGemText, buyBoatRequiredCreditsText;

    [Header("References")]
    public ResourceInventory resourceInventory;
    public CreditManager creditManager;


    // 
    void OnEnable()
    {
        LoadAllRequirements();
    }

    public void RepairBoat()
    {
        repairRequiredWood = PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10 - PlayerPrefs.GetInt("BoatHP", 10);

        if (repairRequiredWood > 0
            && resourceInventory.woodAmount >= repairRequiredWood
            && creditManager.credits >= repairRequiredCredits)
        {
            resourceInventory.woodAmount -= repairRequiredWood;
            creditManager.credits -= repairRequiredCredits;

            PlayerPrefs.SetInt("BoatHP", PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10);
            repairRequiredWoodText.text = repairRequiredWood + "";

            repairDialogueText.forceStop = true;
            repairDialogueText.textMeshPro.text = "There you go! Good as new...";
            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources or boat is at max HP");
        }
    }
    public void CheckOutBoat()
    {
        int boatHP = PlayerPrefs.GetInt("BoatHP", 10);

        if (boatHP == PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10)
        {
            repairDialogueText.textMeshPro.text = "Your boat seems fine to me...";
        }
        else
        {
            repairDialogueText.textMeshPro.text = "Woah!\n\nYour boat has taken some damage...\n\nLet me fix it for you";
        }
    }
    public void UpgradeBoatSpeed()
    {
        if (resourceInventory.woodAmount >= speedRequiredWood
            && resourceInventory.coalAmount >= speedRequiredCoal
            && resourceInventory.goldAmount >= speedRequiredGold
            && creditManager.credits >= speedRequiredCredits
            && boatSpeedLevel < maxLevel && boatSpeedLevel < 3)
        {

            resourceInventory.woodAmount -= speedRequiredWood;
            resourceInventory.coalAmount -= speedRequiredCoal;
            resourceInventory.goldAmount -= speedRequiredGold;
            creditManager.credits -= speedRequiredCredits;

            boatSpeedLevel++;

            PlayerPrefs.SetFloat("BoatSpeed", 5f + boatSpeedLevel);
            PlayerPrefs.SetInt("BoatSpeedLevel", boatSpeedLevel);

            speedRequiredWood = RaisedRequirement(speedRequiredWood, speedRequiredWoodText);
            speedRequiredCoal = RaisedRequirement(speedRequiredCoal, speedRequiredCoalText);
            speedRequiredGold = RaisedRequirement(speedRequiredGold, speedRequiredGoldText);
            speedRequiredCredits = RaisedRequirement(speedRequiredCredits, speedRequiredCreditsText);

            if (boatSpeedLevel < maxLevel)
            {
                boatSpeedLevelText.text = "LVL " + boatSpeedLevel;
            }
            else
            {
                boatSpeedLevelText.text = "MAX";
            }

            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources");
        }
    }
    public void UpgradeWaterGun()
    {
        if (resourceInventory.woodAmount >= gunRequiredWood
           && resourceInventory.ironAmount >= gunRequiredIron
           && resourceInventory.goldAmount >= gunRequiredGold
           && creditManager.credits >= gunRequiredCredits
           && waterGunLevel < maxLevel && waterGunLevel < 3)
        {
            resourceInventory.woodAmount -= gunRequiredWood;
            resourceInventory.ironAmount -= gunRequiredIron;
            resourceInventory.goldAmount -= gunRequiredGold;
            creditManager.credits -= gunRequiredCredits;

            waterGunLevel++;
            PlayerPrefs.SetInt("WaterGunLevel", waterGunLevel);

            gunRequiredWood = RaisedRequirement(gunRequiredWood, gunRequiredWoodText);
            gunRequiredIron = RaisedRequirement(gunRequiredIron, gunRequiredIronText);
            gunRequiredGold = RaisedRequirement(gunRequiredGold, gunRequiredGoldText);
            gunRequiredCredits = RaisedRequirement(gunRequiredCredits, gunRequiredCreditsText);

            if (waterGunLevel < maxLevel)
            {
                waterGunLevelText.text = "LVL " + waterGunLevel;
            }
            else
            {
                waterGunLevelText.text = "MAX";
            }

            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources");
        }
    }
    public void UpgradeBoatDurability()
    {
        if (resourceInventory.woodAmount >= durabRequiredWood
           && resourceInventory.stoneAmount >= durabRequiredStone
           && resourceInventory.ironAmount >= durabRequiredIron
           && creditManager.credits >= durabRequiredCredits
           && boatDurabilityLevel < maxLevel && boatDurabilityLevel < 3)
        {
            resourceInventory.woodAmount -= durabRequiredWood;
            resourceInventory.stoneAmount -= durabRequiredStone;
            resourceInventory.ironAmount -= durabRequiredIron;
            creditManager.credits -= durabRequiredCredits;

            boatDurabilityLevel++;
            PlayerPrefs.SetInt("BoatDurabilityLevel", boatDurabilityLevel);
            PlayerPrefs.SetInt("BoatHP", boatDurabilityLevel * 10);

            durabRequiredWood = RaisedRequirement(durabRequiredWood, durabRequiredWoodText);
            durabRequiredStone = RaisedRequirement(durabRequiredStone, durabRequiredStoneText);
            durabRequiredIron = RaisedRequirement(durabRequiredIron, durabRequiredIronText);
            durabRequiredCredits = RaisedRequirement(durabRequiredCredits, durabRequiredCreditsText);

            if (boatDurabilityLevel < maxLevel)
            {
                boatDurabilityLevelText.text = "LVL " + boatDurabilityLevel;
            }
            else
            {
                boatDurabilityLevelText.text = "MAX";
            }

            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources");
        }
    }
    public void BuyNewBoat()
    {
        if (maxLevel <= 3
        && resourceInventory.woodAmount >= buyBoatRequiredWood
        && resourceInventory.stoneAmount >= buyBoatRequiredStone
        && resourceInventory.coalAmount >= buyBoatRequiredCoal
        && resourceInventory.ironAmount >= buyBoatRequiredIron
        && resourceInventory.goldAmount >= buyBoatRequiredGold
        && resourceInventory.gemAmount >= buyBoatRequiredGem
        && creditManager.credits >= buyBoatRequiredCredits)
        {
            resourceInventory.woodAmount -= buyBoatRequiredWood;
            resourceInventory.stoneAmount -= buyBoatRequiredStone;
            resourceInventory.coalAmount -= buyBoatRequiredCoal;
            resourceInventory.ironAmount -= buyBoatRequiredIron;
            resourceInventory.goldAmount -= buyBoatRequiredGold;
            resourceInventory.gemAmount -= buyBoatRequiredGem;
            creditManager.credits -= buyBoatRequiredCredits;

            boatLevel++;
            maxLevel++;

            PlayerPrefs.SetInt("BoatLevel", boatLevel);
            PlayerPrefs.SetInt("MaxBoatLevel", maxLevel);

            buyBoatRequiredWood = RaisedRequirement(buyBoatRequiredWood, buyBoatRequiredWoodText);
            buyBoatRequiredStone = RaisedRequirement(buyBoatRequiredStone, buyBoatRequiredStoneText);
            buyBoatRequiredCoal = RaisedRequirement(buyBoatRequiredCoal, buyBoatRequiredCoalText);
            buyBoatRequiredIron = RaisedRequirement(buyBoatRequiredIron, buyBoatRequiredIronText);
            buyBoatRequiredGold = RaisedRequirement(buyBoatRequiredGold, buyBoatRequiredGoldText);
            buyBoatRequiredGem = RaisedRequirement(buyBoatRequiredGem, buyBoatRequiredGemText);
            buyBoatRequiredCredits = RaisedRequirement(buyBoatRequiredCredits, buyBoatRequiredCreditsText);

            // if (boatLevel < 3)
            // {
            //     boatLevelText.text = "LVL " + boatLevel;
            // }
            // else
            // {
            //     boatLevelText.text = "MAX";
            // }

            RefreshRequirementTexts();
        }
        else
        {
            Debug.Log("Not enough resources");
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
        repairRequiredWoodText.text = PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10 - PlayerPrefs.GetInt("BoatHP", 10) + "";

        if (boatSpeedLevel < maxLevel && maxLevel < 4)
        {
            boatSpeedLevelText.text = "LVL " + boatSpeedLevel;
            speedRequiredWoodText.text = speedRequiredWood + "";
            speedRequiredCoalText.text = speedRequiredCoal + "";
            speedRequiredGoldText.text = speedRequiredGold + "";
            speedRequiredCreditsText.text = speedRequiredCredits + "";
        }
        else
        {
            if (maxLevel == 4 && boatSpeedLevel < 3)
            {
                boatSpeedLevelText.text = "LVL " + boatSpeedLevel;
                speedRequiredWoodText.text = speedRequiredWood + "";
                speedRequiredCoalText.text = speedRequiredCoal + "";
                speedRequiredGoldText.text = speedRequiredGold + "";
                speedRequiredCreditsText.text = speedRequiredCredits + "";
            }
            else
            {
                boatSpeedLevelText.text = "MAX";
                speedRequiredWoodText.text = "MAX";
                speedRequiredCoalText.text = "MAX";
                speedRequiredGoldText.text = "MAX";
                speedRequiredCreditsText.text = "MAX";
            }
        }

        if (waterGunLevel < maxLevel && maxLevel < 4)
        {
            waterGunLevelText.text = "LVL " + waterGunLevel;
            gunRequiredWoodText.text = gunRequiredWood + "";
            gunRequiredIronText.text = gunRequiredIron + "";
            gunRequiredGoldText.text = gunRequiredGold + "";
            gunRequiredCreditsText.text = gunRequiredCredits + "";
        }
        else
        {
            if (maxLevel == 4 && waterGunLevel < 3)
            {
                waterGunLevelText.text = "LVL " + waterGunLevel;
                gunRequiredWoodText.text = gunRequiredWood + "";
                gunRequiredIronText.text = gunRequiredIron + "";
                gunRequiredGoldText.text = gunRequiredGold + "";
                gunRequiredCreditsText.text = gunRequiredCredits + "";
            }
            else
            {
                waterGunLevelText.text = "MAX";
                gunRequiredWoodText.text = "MAX";
                gunRequiredIronText.text = "MAX";
                gunRequiredGoldText.text = "MAX";
                gunRequiredCreditsText.text = "MAX";
            }

        }

        if (boatDurabilityLevel < maxLevel && maxLevel < 4)
        {
            boatDurabilityLevelText.text = "LVL " + boatDurabilityLevel;
            durabRequiredWoodText.text = durabRequiredWood + "";
            durabRequiredStoneText.text = durabRequiredStone + "";
            durabRequiredIronText.text = durabRequiredIron + "";
            durabRequiredCreditsText.text = durabRequiredCredits + "";
        }
        else
        {
            if (maxLevel == 4 && boatDurabilityLevel < 3)
            {
                boatDurabilityLevelText.text = "LVL " + boatDurabilityLevel;
                durabRequiredWoodText.text = durabRequiredWood + "";
                durabRequiredStoneText.text = durabRequiredStone + "";
                durabRequiredIronText.text = durabRequiredIron + "";
                durabRequiredCreditsText.text = durabRequiredCredits + "";
            }
            else
            {
                boatDurabilityLevelText.text = "MAX";
                durabRequiredWoodText.text = "MAX";
                durabRequiredStoneText.text = "MAX";
                durabRequiredIronText.text = "MAX";
                durabRequiredCreditsText.text = "MAX";
            }
        }

        if (boatLevel < 3)
        {
            boatLevelText.text = "LVL " + boatLevel;
            buyBoatRequiredWoodText.text = buyBoatRequiredWood + "";
            buyBoatRequiredStoneText.text = buyBoatRequiredStone + "";
            buyBoatRequiredCoalText.text = buyBoatRequiredCoal + "";
            buyBoatRequiredIronText.text = buyBoatRequiredIron + "";
            buyBoatRequiredGoldText.text = buyBoatRequiredGold + "";
            buyBoatRequiredGemText.text = buyBoatRequiredGem + "";
            buyBoatRequiredCreditsText.text = buyBoatRequiredCredits + "";
        }
        else
        {
            boatLevelText.text = "MAX";
            buyBoatRequiredWoodText.text = "MAX";
            buyBoatRequiredStoneText.text = "MAX";
            buyBoatRequiredCoalText.text = "MAX";
            buyBoatRequiredIronText.text = "MAX";
            buyBoatRequiredGoldText.text = "MAX";
            buyBoatRequiredGemText.text = "MAX";
            buyBoatRequiredCreditsText.text = "MAX";
        }
    }
    public int LoadRequirement(string upgradeLevel, int requiredResource, TextMeshProUGUI levelText)
    {
        int resourceRequired = requiredResource;

        if (PlayerPrefs.GetInt(upgradeLevel) == 1 || PlayerPrefs.GetInt(upgradeLevel) == 0)
        {
            levelText.text = "LVL " + 1;
        }
        else if (PlayerPrefs.GetInt(upgradeLevel) == 2 && PlayerPrefs.GetInt(upgradeLevel) < maxLevel)
        {
            levelText.text = "LVL " + 2;
            resourceRequired = (int)(requiredResource * 1.15);
        }
        else if (PlayerPrefs.GetInt(upgradeLevel) >= 3 && PlayerPrefs.GetInt(upgradeLevel) < maxLevel)
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
        maxLevel = PlayerPrefs.GetInt("MaxBoatLevel", 2);
        boatSpeedLevel = PlayerPrefs.GetInt("BoatSpeedLevel", 1);
        waterGunLevel = PlayerPrefs.GetInt("WaterGunLevel", 1);
        boatDurabilityLevel = PlayerPrefs.GetInt("BoatDurabilityLevel", 1);
        boatLevel = PlayerPrefs.GetInt("BoatLevel", 1);

        speedRequiredWood = LoadRequirement("BoatSpeedLevel", speedRequiredWood, boatSpeedLevelText);
        speedRequiredCoal = LoadRequirement("BoatSpeedLevel", speedRequiredCoal, boatSpeedLevelText);
        speedRequiredGold = LoadRequirement("BoatSpeedLevel", speedRequiredGold, boatSpeedLevelText);
        speedRequiredCredits = LoadRequirement("BoatSpeedLevel", speedRequiredCredits, boatSpeedLevelText);

        gunRequiredWood = LoadRequirement("WaterGunLevel", gunRequiredWood, waterGunLevelText);
        gunRequiredIron = LoadRequirement("WaterGunLevel", gunRequiredIron, waterGunLevelText);
        gunRequiredGold = LoadRequirement("WaterGunLevel", gunRequiredGold, waterGunLevelText);
        gunRequiredCredits = LoadRequirement("WaterGunLevel", gunRequiredCredits, waterGunLevelText);

        durabRequiredWood = LoadRequirement("BoatDurabilityLevel", durabRequiredWood, boatDurabilityLevelText);
        durabRequiredStone = LoadRequirement("BoatDurabilityLevel", durabRequiredStone, boatDurabilityLevelText);
        durabRequiredIron = LoadRequirement("BoatDurabilityLevel", durabRequiredIron, boatDurabilityLevelText);
        durabRequiredCredits = LoadRequirement("BoatDurabilityLevel", durabRequiredCredits, boatDurabilityLevelText);

        buyBoatRequiredWood = LoadRequirement("BoatLevel", buyBoatRequiredWood, boatLevelText);
        buyBoatRequiredStone = LoadRequirement("BoatLevel", buyBoatRequiredStone, boatLevelText);
        buyBoatRequiredCoal = LoadRequirement("BoatLevel", buyBoatRequiredCoal, boatLevelText);
        buyBoatRequiredIron = LoadRequirement("BoatLevel", buyBoatRequiredIron, boatLevelText);
        buyBoatRequiredGold = LoadRequirement("BoatLevel", buyBoatRequiredGold, boatLevelText);
        buyBoatRequiredGem = LoadRequirement("BoatLevel", buyBoatRequiredGem, boatLevelText);
        buyBoatRequiredCredits = LoadRequirement("BoatLevel", buyBoatRequiredCredits, boatLevelText);

        RefreshRequirementTexts();
    }
}
