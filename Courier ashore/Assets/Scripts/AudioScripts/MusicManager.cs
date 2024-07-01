using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DeathRescue" || scene.name == "PassOutRescue"
            || scene.name == "FinaleFailure" || scene.name == "FinaleSuccess"
            || scene.name == "FinaleCutScene")
        {
            DestroyMusicManager();
        }
    }

    public void DestroyMusicManager()
    {
        instance = null;
        Destroy(gameObject);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
