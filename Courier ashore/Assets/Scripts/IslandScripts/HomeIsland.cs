using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeIsland : MonoBehaviour
{
    public bool canGoAshore = false;
    public GameObject goHomeElement;
    private DayCycle dayCycle;
    private BoatMovement boatMovement;

    void Start()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
        dayCycle = FindObjectOfType<DayCycle>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player" && dayCycle.shiftFinished)
        {
            goHomeElement.SetActive(true);
            canGoAshore = true;
            Debug.Log("Can go ashore: " + canGoAshore);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player" && dayCycle.shiftFinished)
        {
            goHomeElement.SetActive(false);
            canGoAshore = false;
            Debug.Log("Can go ashore: " + canGoAshore);
        }
    }

    void Update()
    {
        if (canGoAshore && Input.GetButtonDown("Interact")
            && dayCycle.shiftFinished == true
            && boatMovement.playerPassedOut == false
            && boatMovement.canPlayerMove == true)
        {
            // TODO: ADD SMOOTH TRANSITION
            GoHome();
        }
    }

    void GoHome()
    {
        SceneManager.LoadScene("HomeIsland");
    }
}
