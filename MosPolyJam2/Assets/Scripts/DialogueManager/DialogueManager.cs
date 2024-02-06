using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public struct Dialogue
    {
        public string Name;
        [TextArea(3, 10)] public string[] Sentences;
        public SoundsRefsSO AudioClips;
        public UnityEvent OnDialogueEnded;
    }

    public static DialogueManager Instance { get; private set; }

    [SerializeField] private TMP_Text messageText;

    private Dialogue currentDialogue;
    private int sentenceIndex;

    [SerializeField] private bool initialStart;
    [SerializeField] private List<Dialogue> dialogueList = new List<Dialogue>();

    public UnityEvent OnDialogueEnded { get; private set; }

    private bool isTyping;
    private AudioSource source;

    private void Awake()
    {
        Instance = this;

        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LockCursor(true);

        if (initialStart)
        {
            StartDialogue(0);
        }
    }

    public void LockCursor(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        sentenceIndex = -1;

        SetNextPhrase();
    }

    public void StartDialogue(int DialogueID)
    {
        currentDialogue = dialogueList[DialogueID];
        sentenceIndex = -1;

        SetNextPhrase();
    }

    public void SetNextPhrase()
    {
        StopAllCoroutines();

        if (isTyping)
        {
            messageText.text = currentDialogue.Sentences[sentenceIndex];
            isTyping = false;
            source.Stop();
            return;
        }

        messageText.text = "";

        sentenceIndex++;
        if (sentenceIndex >= currentDialogue.Sentences.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(ShowTextWithAudio(currentDialogue.Sentences[sentenceIndex], currentDialogue.AudioClips.sounds[sentenceIndex].clip));
    }

    private IEnumerator ShowTextWithAudio(string textToType, AudioClip audioClip)
    {
        isTyping = true;

        source.clip = audioClip;
        source.Play();

        float timePerCharacter = audioClip.length / textToType.Length;

        foreach (char letter in textToType)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(timePerCharacter);
        }

        source.Stop();

        isTyping = false;
        SetNextPhrase();
    }

    private void EndDialogue()
    {
        currentDialogue.OnDialogueEnded?.Invoke();
    }
}