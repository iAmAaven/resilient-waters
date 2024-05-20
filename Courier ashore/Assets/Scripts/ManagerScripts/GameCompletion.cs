using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("GameCompleted", "yes");
    }
}
