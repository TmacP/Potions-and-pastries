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
            GameEventManager.instance.ShowDialogueQuip(Dialogue);
            if (DialogueUI != null && DialogueText != null)
            {
                DialogueUI.SetActive(true);
                DialogueText.SetText(Dialogue.Dialogue);
            }
            else
            {
                Debug.Log(Dialogue.Dialogue);
                Debug.Log("Null Dialogue UI Elements");
            }
        }
        else
        {
            Debug.Log("Null Dialogue Return");
        }

        //This will be moved in the future - its just a quick fix!!!
        GameEventManager.instance.NPCRecieveOrder();
    }

    
}
