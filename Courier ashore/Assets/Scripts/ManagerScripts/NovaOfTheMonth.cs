using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovaOfTheMonth : MonoBehaviour
{
    public Image image;
    public Sprite emptySprite, novaSprite;
    public GameObject interviewButton;

    void Start()
    {
        if (PlayerPrefs.GetString("GameCompleted") == "yes")
        {
            interviewButton.SetActive(false);
            image.sprite = novaSprite;
        }
        else
        {
            interviewButton.SetActive(true);
            image.sprite = emptySprite;
        }
    }
}
