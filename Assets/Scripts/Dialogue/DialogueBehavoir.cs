using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBehavoir : MonoBehaviour, IInteractable
{

    [SerializeField] public NPCDialogueState State;
    [SerializeField] public GameObject DialogueUI;
    [SerializeField] public TextMeshProUGUI DialogueText;

    public string InteractionPrompt => "Speak";

    public bool TryInteract(InteractorBehavoir InInteractor)
    {
        TryDialogue();
        return true;
    }

    public void TryDialogue()
    {
        DialogueData Dialogue = DialogueManager.instance.GetDialogue(State);
        if(Dialogue != null)
        {
            DialogueUI.SetActive(true);
            DialogueText.SetText(Dialogue.Dialogue);
        }
        else
        {
            Debug.Log("Null Dialogue Return");
        }
    }

    
}
