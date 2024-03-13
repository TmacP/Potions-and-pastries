using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class DialogueBehavoir : MonoBehaviour
{

    [SerializeField] public NPCDialogueState State;
    [SerializeField] public GameObject DialogueUI;
    [SerializeField] public TextMeshProUGUI DialogueText;
    [SerializeField] public float DialogueDisplayTime = 3.0f;
    [SerializeField] public int DialogueDisplayCounter = 0;

// reference to ink
    public BasicInkExample basicInkExample; //

    public void TryDialogue()
    {
        if(basicInkExample != null)
        {
            basicInkExample.ContinueStory(); // WE CONTINUE THE STORY WHEN TALK TO NPC
        }
        DialogueData Dialogue = DialogueManager.instance.GetDialogue(State);
        if(Dialogue != null)
        {
            GameEventManager.instance.ShowDialogueQuip(Dialogue);
            if (DialogueUI != null && DialogueText != null)
            {
                DialogueUI.SetActive(true);
                DialogueText.SetText(Dialogue.Dialogue);
                DialogueDisplayCounter++;
                Invoke("EndQuipDialogue", DialogueDisplayTime);
            }
            else
            {
                //Debug.Log(Dialogue.Dialogue);
                //Debug.Log("Null Dialogue UI Elements");
            }
        }
        else
        {
            Debug.Log("Null Dialogue Return");
        }
    }


    public void EndQuipDialogue()
    {
        DialogueDisplayCounter--;
        if(DialogueDisplayCounter <= 0)
        {
            DialogueUI.SetActive(false);
        }
    }
 
}
