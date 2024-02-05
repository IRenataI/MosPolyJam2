using TMPro;
using UnityEngine;

public class DialogueEnterTrigger : MonoBehaviour
{
    [SerializeField] private int dialogueIndex;
    [TextArea] [SerializeField] private string dialogueDescriptionText;
    [SerializeField] private TMP_Text dialogueDescription;

    private void Start()
    {
        DialogueManager.Instance.StartDialogue(dialogueIndex);
        dialogueDescription.text = dialogueDescriptionText;
    }
}
