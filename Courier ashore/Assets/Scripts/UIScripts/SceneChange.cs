using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator transitionAnim;
    string nameOfScene;
    public void ChangeScene(string sceneName)
    {
        transitionAnim.SetTrigger("ChangeScene");
        nameOfScene = sceneName;
        Invoke("Change", 1.5f);
    }
    void Change()
    {
        SceneManager.LoadScene(nameOfScene);
    }
}
