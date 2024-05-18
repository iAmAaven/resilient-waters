using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rescue : MonoBehaviour
{
    public bool playerDied = false;
    void Start()
    {
        if (playerDied)
        {
            PlayerPrefs.SetInt("BoatHP", PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10 / 2);
        }

        Invoke("GoToHomeIsland", 25f);
    }
    public void GoToHomeIsland()
    {
        SceneManager.LoadScene("HomeIsland");
    }
}
