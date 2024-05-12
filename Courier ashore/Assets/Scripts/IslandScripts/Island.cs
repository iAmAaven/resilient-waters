using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    public Canvas harvestCanvas;
    public GameObject whiteOutline;
    public Slider harvestProgressBar;
    public bool ableToHarvest = false;
    public float harvestRate = 0.5f;
    private bool harvested = false;
    private bool harvesting = false;
    private bool increasing = false, decreasing = false;

    void Start()
    {
        harvestCanvas.worldCamera = Camera.main;
        harvestProgressBar.gameObject.SetActive(false);
        whiteOutline.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
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

            harvesting = false;
            harvestProgressBar.value = 0;
            harvestProgressBar.gameObject.SetActive(false);
            ableToHarvest = false;
            Debug.Log("Can harvest: " + ableToHarvest);
        }
    }

    void Update()
    {
        if (ableToHarvest && harvested == false)
        {
            if (Input.GetButtonDown("Interact") && harvesting == false)
            {
                harvesting = true;
                StopCoroutine(DecreaseHarvest());
                if (increasing == false)
                {
                    StartCoroutine(IncreaseHarvest());
                }
            }
            if (Input.GetButtonUp("Interact") && harvesting == true)
            {
                harvesting = false;
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

            harvestProgressBar.value += 0.5f;

            if (harvestProgressBar.value >= harvestProgressBar.maxValue)
            {
                harvestProgressBar.gameObject.SetActive(false);
                Destroy(whiteOutline);
                harvested = true;
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

            harvestProgressBar.value -= 0.5f;

            if (harvestProgressBar.value <= harvestProgressBar.minValue)
            {
                break;
            }

            yield return new WaitForSeconds(harvestRate);
        }
        decreasing = false;
    }
}
