using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator cameraAnim;

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("HomeIsland");
    }
    public void Play()
    {
        cameraAnim.SetTrigger("Play");
    }
    public void GoToMenu()
    {
        cameraAnim.SetTrigger("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
