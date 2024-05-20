using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassOut : MonoBehaviour
{
    public GameObject passedOut, boatDestroyed;
    public GameObject[] otherCanvases;
    public bool isFinale = false;
    private BoatMovement boatMovement;
    private CreditManager creditManager;

    void Start()
    {
        creditManager = FindObjectOfType<CreditManager>();
        boatMovement = FindObjectOfType<BoatMovement>();
    }

    public void PassedOut()
    {
        if (boatMovement.playerPassedOut == false)
        {
            PlayerPrefs.SetInt("Credits", (int)(creditManager.credits * 0.75));

            boatMovement.playerPassedOut = true;
            foreach (GameObject canvas in otherCanvases)
            {
                canvas.SetActive(false);
            }
            passedOut.SetActive(true);
            Invoke("GoToPassOutScene", 4f);
        }
    }
    public void BoatDestroyed()
    {
        if (boatMovement.playerPassedOut == false)
        {
            PlayerPrefs.SetInt("Credits", (int)(creditManager.credits * 0.5));

            boatMovement = FindObjectOfType<BoatMovement>();
            boatMovement.canPlayerMove = false;
            boatMovement.playerPassedOut = true;

            foreach (GameObject canvas in otherCanvases)
            {
                canvas.SetActive(false);
            }
            passedOut.SetActive(false);
            boatDestroyed.SetActive(true);

            if (isFinale == false)
            {
                Invoke("GoToDeathRescueScene", 4f);
            }
            else
            {
                Invoke("FinaleFailure", 4f);
            }
        }
    }

    void GoToPassOutScene()
    {
        SceneManager.LoadScene("PassOutRescue");
    }
    void GoToDeathRescueScene()
    {
        SceneManager.LoadScene("DeathRescue");
    }
    void FinaleFailure()
    {
        SceneManager.LoadScene("FinaleFailure");
    }
}
