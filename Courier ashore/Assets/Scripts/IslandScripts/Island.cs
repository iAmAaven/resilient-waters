using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [Header("Harvest stats")]
    public float harvestRate = 0.5f;
    public bool ableToHarvest = false;

    [Header("Resource chances")]
    public float stoneChance;
    public float coalChance;
    public float goldChance;


    [Header("References")]
    public Canvas harvestCanvas;
    public GameObject whiteOutline;
    public Slider harvestProgressBar;


    // PRIVATES

    private bool harvested = false,     // Keeps track whether the island has already been harvested or not
                                        // when false, this island can be harvested by pressing "Interact"
    harvesting = false,                 // Keeps track whether the player is currently harvesting the island
                                        // when false, the progress bar decreases, if not already at zero.
    increasing = false,                 // Keeps track whether the coroutine "IncreaseHarvest()" is running
                                        // when false, the coroutine can be started by pressing "Interact"
    decreasing = false;                 // Keeps track whether the coroutine "DecreaseHarvest()" is running
                                        // when false, the coroutine can be started by pressing "Interact"

    private BoatMovement boatMovement;

    void Start()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
        harvestCanvas.worldCamera = Camera.main;
        harvestProgressBar.gameObject.SetActive(false);
        whiteOutline.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player" && harvested == false)
        {
            if (whiteOutline != null)
                whiteOutline.SetActive(true);

            if (harvested == false)
                harvestProgressBar.gameObject.SetActive(true);

            ableToHarvest = true;
            Debug.Log("Can harvest: " + ableToHarvest);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
        {
            if (whiteOutline != null)
                whiteOutline.SetActive(false);

            boatMovement.canPlayerMove = true;
            boatMovement.isPlayerHarvesting = false;
            harvesting = false;
            harvestProgressBar.value = 0;
            harvestProgressBar.gameObject.SetActive(false);
            ableToHarvest = false;
            Debug.Log("Can harvest: " + ableToHarvest);
        }
    }

    void Update()
    {
        if (boatMovement != null && ableToHarvest && harvested == false)
        {
            if (Input.GetButtonDown("Interact") && harvesting == false && boatMovement.canPlayerMove == true)
            {
                harvesting = true;
                boatMovement.canPlayerMove = false;
                StopCoroutine(DecreaseHarvest());
                if (increasing == false)
                {
                    StartCoroutine(IncreaseHarvest());
                }
            }
            if (Input.GetButtonUp("Interact") && harvesting == true)
            {
                harvesting = false;
                boatMovement.canPlayerMove = true;
                StopCoroutine(IncreaseHarvest());
                if (decreasing == false)
                {
                    StartCoroutine(DecreaseHarvest());
                }
            }
        }
    }

    IEnumerator IncreaseHarvest()
    {
        increasing = true;
        while (true)
        {
            if (harvesting == false)
                break;

            boatMovement.isPlayerHarvesting = true;
            harvestProgressBar.value += 0.5f;

            if (harvestProgressBar.value >= harvestProgressBar.maxValue)
            {
                IslandHarvested();
                break;
            }

            yield return new WaitForSeconds(harvestRate);
        }
        increasing = false;
    }
    IEnumerator DecreaseHarvest()
    {
        decreasing = true;
        while (true)
        {
            if (harvesting == true)
                break;

            boatMovement.isPlayerHarvesting = false;
            harvestProgressBar.value -= 0.5f;

            if (harvestProgressBar.value <= harvestProgressBar.minValue)
            {
                break;
            }

            yield return new WaitForSeconds(harvestRate);
        }
        decreasing = false;
    }

    void IslandHarvested()
    {
        boatMovement.canPlayerMove = true;
        boatMovement.isPlayerHarvesting = false;
        harvestProgressBar.gameObject.SetActive(false);
        Destroy(whiteOutline);
        harvested = true;
        FindObjectOfType<ResourceInventory>().DealIslandResources(stoneChance, coalChance, goldChance);
    }
}