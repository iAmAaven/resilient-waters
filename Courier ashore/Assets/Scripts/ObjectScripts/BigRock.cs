using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigRock : MonoBehaviour
{
    public Canvas harvestCanvas;
    public GameObject whiteOutline;
    public Slider harvestProgressBar;
    public bool ableToHarvest = false;
    public float harvestRate = 0.5f;
    private bool harvested = false;
    private bool harvesting = false;
    private bool increasing = false, decreasing = false;
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
                BigRockHarvested();
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

    void BigRockHarvested()
    {
        boatMovement.canPlayerMove = true;
        boatMovement.isPlayerHarvesting = false;
        harvestProgressBar.gameObject.SetActive(false);
        Destroy(whiteOutline);
        harvested = true;
        FindObjectOfType<ResourceInventory>().DealResources(false);
    }
}
