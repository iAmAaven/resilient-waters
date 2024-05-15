using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseShift : MonoBehaviour
{
    public void ShiftChoice(string choice)
    {
        PlayerPrefs.SetString("CurrentShift", choice);
        SceneManager.LoadScene("ResilientWaters");
    }
}
