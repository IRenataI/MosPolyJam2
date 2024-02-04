using System.Collections;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(AudioSource))]
public class Novel : MonoBehaviour
{
    [System.Serializable]
    public struct Dialogue
    {
        [TextArea] public string[] Text;
        public SoundsRefsSO AudioClips;
    }

    [SerializeField] private TMP_Text messageText;

    private string[] currentDialogue;
    private int dialogueIndex;
    private SoundsRefsSO currentAudioClips;

    [SerializeField] private bool haveInitialDialog;
    [SerializeField] private Dialogue initialDialogue;

    private bool isTyping;
    private AudioSource source;

    private Action onDialogueEnd;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (haveInitialDialog)
        {
            StartDialog(initialDialogue);
        }
    }

    public void StartDialog(Dialogue dialogue, Action onDialogueEnd = null)
    {
        currentDialogue = dialogue.Text;
        dialogueIndex = -1;

        this.onDialogueEnd = onDialogueEnd;

        SetNextPhrase();
    }

    public void SetNextPhrase()
    {
        StopAllCoroutines();

        if (isTyping)
        {
            messageText.text = currentDialogue[dialogueIndex];
            isTyping = false;
            source.Stop();
            return;
        }

        messageText.text = "";

        dialogueIndex++;
        if (dialogueIndex >= currentDialogue.Length)
        {
            onDialogueEnd?.Invoke();
            return;
        }

        StartCoroutine(ShowTextWithAudio(currentDialogue[dialogueIndex], currentAudioClips.sounds[dialogueIndex].clip));
    }

    private IEnumerator ShowTextWithAudio(string textToType, AudioClip audioClip)
    {
        isTyping = true;

        messageText.text += textToType;

        source.clip = audioClip;
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);

        isTyping = false;
    }
}
