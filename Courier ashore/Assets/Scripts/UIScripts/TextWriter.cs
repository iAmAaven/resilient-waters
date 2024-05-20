using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    [TextArea] public string dialogueText;
    public char[] endChars;
    public char[] pauseChars;

    [Header("Typing speed")]
    public float timeBetweenChars;
    public float pauseAfterEndChar;
    public float pauseAfterPauseChar;
    [Header("Automatic dialogue closing")]
    public bool willClose = false;
    public float closeAfter;

    // PRIVATES
    [HideInInspector] public TextMeshProUGUI textMesh;
    private DialogueSounds dialogueSounds;
    private AudioSource oneShotAudio;
    private string processedText;

    void OnEnable()
    {
        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();
        dialogueSounds = FindObjectOfType<DialogueSounds>();
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
        processedText = "";
        for (int i = 0; i < dialogueText.Length; i++)
        {
            if (i % 3 == 0)
            {
                oneShotAudio.PlayOneShot(dialogueSounds.blipSounds[Random.Range(0, dialogueSounds.blipSounds.Length)]);
            }

            processedText += dialogueText[i];
            textMesh.text = processedText;
            yield return new WaitForSeconds(timeBetweenChars);

            if (endChars.Contains(dialogueText[i]))
            {
                yield return new WaitForSeconds(pauseAfterEndChar);
            }
            else if (pauseChars.Contains(dialogueText[i]))
            {
                yield return new WaitForSeconds(pauseAfterPauseChar);
            }
        }

        if (willClose)
        {
            yield return new WaitForSeconds(closeAfter);
            gameObject.SetActive(false);
        }
    }
}
