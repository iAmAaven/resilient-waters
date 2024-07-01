using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    [TextArea] public string dialogueText;
    public char[] endChars;
    public char[] pauseChars;

    [Header("Typing speed")]
    public float charDelay = 0.015f;
    public float endCharDelay = 0.1f;
    public float pauseCharDelay = 0.05f;
    [Header("Automatic dialogue closing")]
    public bool willClose = false;
    public float closeAfter;

    // HIDDEN
    [HideInInspector] public bool forceStop = false;

    // PRIVATES
    private DialogueSounds dialogueSounds;
    private AudioSource oneShotAudio;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }
    void OnEnable()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();
        dialogueSounds = FindObjectOfType<DialogueSounds>();

        // dialogueText = textMeshPro.text;

        StartRevealing();
    }

    private IEnumerator RevealText()
    {
        textMeshPro.ForceMeshUpdate();
        var textInfo = textMeshPro.textInfo;
        int totalCharacters = textInfo.characterCount;

        textMeshPro.maxVisibleCharacters = 0;

        for (int i = 0; i < totalCharacters; i++)
        {
            totalCharacters = textInfo.characterCount;

            if (forceStop == true)
                break;


            if (i % 8 == 0)
                oneShotAudio.PlayOneShot(dialogueSounds.blipSounds[Random.Range(0, dialogueSounds.blipSounds.Length)]);

            textMeshPro.maxVisibleCharacters = i + 1;

            // Updates the alpha of the current character
            var character = textInfo.characterInfo[i];
            if (character.isVisible)
            {
                int materialIndex = character.materialReferenceIndex;
                int vertexIndex = character.vertexIndex;

                Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
                Color32 color = vertexColors[vertexIndex];
                color.a = 255;  // Sets alpha to fully visible
                vertexColors[vertexIndex + 0] = color;
                vertexColors[vertexIndex + 1] = color;
                vertexColors[vertexIndex + 2] = color;
                vertexColors[vertexIndex + 3] = color;

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }


            if (pauseChars.Contains(character.character))
            {
                yield return new WaitForSeconds(pauseCharDelay);
            }
            else if (endChars.Contains(character.character))
            {
                yield return new WaitForSeconds(endCharDelay);
            }
            else
            {
                yield return new WaitForSeconds(charDelay);
            }
        }

        if (willClose)
        {
            yield return new WaitForSeconds(closeAfter);
            gameObject.SetActive(false);
        }

        forceStop = false;
    }

    public void StartRevealing()
    {
        for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
        {
            textMeshPro.maxVisibleCharacters = i + 1;

            // Updates the alpha of the current character
            var character = textMeshPro.textInfo.characterInfo[i];
            if (character.isVisible)
            {
                int materialIndex = character.materialReferenceIndex;
                int vertexIndex = character.vertexIndex;

                Color32[] vertexColors = textMeshPro.textInfo.meshInfo[materialIndex].colors32;
                Color32 color = vertexColors[vertexIndex];
                color.a = 0;  // Sets alpha to fully invisible

                vertexColors[vertexIndex + 0] = color;
                vertexColors[vertexIndex + 1] = color;
                vertexColors[vertexIndex + 2] = color;
                vertexColors[vertexIndex + 3] = color;

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }

        StartCoroutine(RevealText());
    }
}
