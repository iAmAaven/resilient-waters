using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassOut : MonoBehaviour
{
    public GameObject passedOut;
    public GameObject[] otherCanvases;
    private BoatMovement boatMovement;

    public void PassedOut()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
        boatMovement.playerPassedOut = true;
        foreach (GameObject canvas in otherCanvases)
        {
            canvas.SetActive(false);
        }
        passedOut.SetActive(true);
        Invoke("GoToGomeIsland", 4f);
    }

    void GoToGomeIsland()
    {
        SceneManager.LoadScene("HomeIsland");
    }
}
